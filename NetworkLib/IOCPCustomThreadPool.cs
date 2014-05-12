using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace Timok.NetworkLib {
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public unsafe struct OVERLAPPED {
		uint* ulpInternal;
		uint* ulpInternalHigh;
		int lOffset;
		int lOffsetHigh;
		uint hEvent;
	}

	public delegate void DISPATCHER(uint iValue);

	public sealed class IOCPCustomThreadPool {
		[DllImport("Kernel32", CharSet = CharSet.Auto)]
		static extern unsafe uint CreateIoCompletionPort(uint hFile, uint hExistingCompletionPort, uint* puiCompletionKey, uint uiNumberOfConcurrentThreads);

		[DllImport("Kernel32", CharSet = CharSet.Auto)]
		static extern unsafe bool CloseHandle(uint hobject);

		[DllImport("Kernel32", CharSet = CharSet.Auto)]
		static extern unsafe bool PostQueuedCompletionStatus(uint hCompletionPort, uint uiSizeOfArgument, uint* puiUserArg, OVERLAPPED* pOverlapped);

		[DllImport("Kernel32", CharSet = CharSet.Auto)]
		static extern unsafe bool GetQueuedCompletionStatus(uint hCompletionPort, uint* pSizeOfArgument, uint* puiUserArg, OVERLAPPED** ppOverlapped, uint uiMilliseconds);

		//------------------------------------- Constants --------------------------------------------
		//-- represents the Win32 Invalid Handle Value Macro
		const uint INVALID_HANDLE_VALUE = 0xffffffff;

		//-- represents the Win32 INFINITE Macro </summary>
		const uint INIFINITE = 0xffffffff;

		//-- tells the IOCP Function to shutdown </summary>
		const int SHUTDOWN_IOCPTHREAD = 0x7fffffff;

		//-------------------------------------- Fields -----------------------------------------------
		//-- Contains the IO Completion Port Thread Pool handle for this instance
		readonly uint handle;

		//-- The maximum number of threads that may be running at the same time
		readonly int maxConcurrency;

		//-- The minimal number of threads the thread pool maintains
		readonly int minThreadsInPool;

		//-- The maximum number of threads the thread pool maintains
		readonly int maxThreadsInPool;

		//-- A serialization object to protect the class state
		readonly object padlock;

		//-- A reference to a user specified function to be call by the thread pool
		readonly DISPATCHER userFunction;

		//-- Flag to indicate if the class is disposing
		bool isDisposed;

		//-- Current number of threads in the thread pool
		int numberOfThreadsInPool;
		public int NumberOfThreadsInPool { get { return numberOfThreadsInPool; } }

		void IncrementNumberOfThreadsInPool() {
			Interlocked.Increment(ref numberOfThreadsInPool);
			return;
		}

		void DecrementNumberOfThreadsInPool() {
			Interlocked.Decrement(ref numberOfThreadsInPool);
			return;
		}

		//-- Active number of threads in the thread pool
		int numberOfActiveThreadsInPool;
		public int NumberOfActiveThreadsInPool { get { return numberOfActiveThreadsInPool; } }

		void IncrementNumberOfActiveThreadsInPool() {
			Interlocked.Increment(ref numberOfActiveThreadsInPool);
			return;
		}

		void DecrementNumberOfActiveThreadsInPool() {
			Interlocked.Decrement(ref numberOfActiveThreadsInPool);
			return;
		}

		//-- Current number of Work posted in the thread pool
		int numberOfWorkItemsInPool;
		public int NumberOfWorkItemsInPool { get { return numberOfWorkItemsInPool; } }

		void IncrementNumberWorkItemsInPool() {
			Interlocked.Increment(ref numberOfWorkItemsInPool);
			return;
		}

		void DecrementNumberOfWorkItemsInPool() {
			Interlocked.Decrement(ref numberOfWorkItemsInPool);
			return;
		}

		//------------------------------------------------------------------------------------------
		// Constructor, Finalize, and Dispose
		/// <summary> Constructor </summary>
		/// <param name = "pMaxConcurrency"> Max number of running threads allowed </param>
		/// <param name = "pMinThreadsInPool"> Min number of threads in the pool </param>
		/// <param name = "pMaxThreadsInPool"> Max number of threads in the pool </param>
		/// <param name = "pUserFunction"> Reference to a function to call to perform work </param>
		/// <exception cref = "Exception"> Unhandled Exception </exception>
		public IOCPCustomThreadPool(int pMaxConcurrency, int pMinThreadsInPool, int pMaxThreadsInPool, DISPATCHER pUserFunction) {
			try {
				maxConcurrency = pMaxConcurrency;
				minThreadsInPool = pMinThreadsInPool;
				maxThreadsInPool = pMaxThreadsInPool;
				userFunction = pUserFunction;

				numberOfThreadsInPool = 0;
				numberOfActiveThreadsInPool = 0;
				numberOfWorkItemsInPool = 0;

				padlock = new object();

				isDisposed = false;

				// Create an IO Completion Port for Thread Pool use
				unsafe {
					handle = CreateIoCompletionPort(INVALID_HANDLE_VALUE, 0, null, (uint)maxConcurrency);
				}
				if (handle == 0) {
					throw new Exception("Unable To Create IO Completion Port");
				}

				// Allocate and start the Minimum number of threads specified
				ThreadStart _tsThread = iocpFunction;
				for (int _i = 0; _i < minThreadsInPool; ++_i) {
					Thread _thread = new Thread(_tsThread);
					_thread.Name = string.Format("IOCP_{0}", _i);
					Thread.AllocateNamedDataSlot(_thread.Name);
					_thread.Start();

					IncrementNumberOfThreadsInPool();
				}
			}
			catch (Exception _ex) {
				throw new Exception(string.Format("IOCPCustomThreadPool, Unhandled Exception: {0}", _ex));
			}
		}

		//------------------------------------------------------------------------------------------
		//-- Finalize called by the GC </summary>
		~IOCPCustomThreadPool() {
			if (!isDisposed) {
				Dispose();
			}
		}

		//------------------------------------------------------------------------------------------
		//-- Called when the object will be shutdown. It will wait for all of the work to be completed inside the queue before completing
		public void Dispose() {
			try {
				isDisposed = true;

				// Shutdown all thread in the pool
				int _numberOfThreadsInPool = numberOfThreadsInPool;
				for (int _thread = 0; _thread < _numberOfThreadsInPool; _thread++) {
					unsafe {
						PostQueuedCompletionStatus(handle, 4, (uint*)SHUTDOWN_IOCPTHREAD, null);
					}
				}

				// Wait here until all the threads are gone
				while (numberOfThreadsInPool != 0) {
					Thread.Sleep(100);
				}
				unsafe {
					CloseHandle(handle);
				}
			}
			catch (Exception _ex) {
				try {
					EventLog.WriteEntry("Application", string.Format("\r\nIOCPCustomThreadPool Dispose exception:{0}", _ex), EventLogEntryType.Error, 1);
				}
				catch { }
			}
		}

		//------------------------------ Public -----------------------------------------------------
		//-- IOCP Worker Function that calls the specified user function
		// <param name="pValue"> SimType: A value to be passed with the event </param>
		// <exception cref = "Exception"> Unhandled Exception </exception>
		public void PostEvent(uint pValue) {
			if (isDisposed) {
				return;
			}
			unsafe {	// Post an event into the IOCP Thread Pool
				PostQueuedCompletionStatus(handle, 4, (uint*)pValue, null);
			}
			IncrementNumberWorkItemsInPool();

			threadPoolMaintanance();
		}

		//----------------------------------- Private --------------------------------------------------------
		//-- IOCP Worker Function that calls the specified user function
		void iocpFunction() {
			uint _numberOfBytes;
			uint _value;

			try {
				while (true) {
					unsafe { // Wait for an event
						OVERLAPPED* _overlapped;
						GetQueuedCompletionStatus(handle, &_numberOfBytes, &_value, &_overlapped, INIFINITE);
					}

					DecrementNumberOfWorkItemsInPool();

					if (_value == SHUTDOWN_IOCPTHREAD) {
						break;
					}

					IncrementNumberOfActiveThreadsInPool();

					try { userFunction(_value); }
					catch { }

					DecrementNumberOfActiveThreadsInPool();
				}
			}
			catch { }

			DecrementNumberOfThreadsInPool();
		}

		void threadPoolMaintanance() {
			// If we have less than max threads currently in the pool Should we add a new thread to the pool
			lock (padlock) {
				try {
					if (numberOfThreadsInPool < maxThreadsInPool) {
						if (numberOfActiveThreadsInPool == numberOfThreadsInPool) {
							if (isDisposed == false) {
								ThreadStart _threadStart = iocpFunction;
								Thread thThread = new Thread(_threadStart);
								thThread.Name = "IOCP " + thThread.GetHashCode();
								thThread.Start();
								IncrementNumberOfThreadsInPool();
							}
						}
					}
					else {
						//TODO: Log: (CurThreadsInPool >= MaxThreadsInPool)
					}
				}
				catch { }
			}
		}
	}
}

//using System;
//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using System.Threading;

//namespace Timok.Core {
//  /// <summary> This is the WIN32 OVERLAPPED structure </summary>
//  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
//  public unsafe struct OVERLAPPED {
//    uint* ulpInternal;
//    uint* ulpInternalHigh;
//    int lOffset;
//    int lOffsetHigh;
//    uint  hEvent;
//  }

//  //------------------------------------------------------------------------------------------
//  // Delegate Function Types
//  /// <summary> DelType: This is the type of user function to be supplied for the thread pool </summary>
//  public delegate void DISPATCHER(int iValue);

//  /// <summary> This class provides the ability to create a thread pool to manage work.  The
//  ///           class abstracts the Win32 IOCompletionPort API so it requires the use of
//  ///           unmanaged code.  Unfortunately the .NET framework does not provide this functionality </summary>
//  public sealed class IOCPCustomThreadPool {

//    /// <summary> Win32Func: Create an IO Completion Port Thread Pool </summary>
//    [DllImport("Kernel32", CharSet=CharSet.Auto)]
//    private unsafe static extern uint CreateIoCompletionPort(uint hFile, uint hExistingCompletionPort, uint* puiCompletionKey, uint uiNumberOfConcurrentThreads);

//    /// <summary> Win32Func: Closes an IO Completion Port Thread Pool </summary>
//    [DllImport("Kernel32", CharSet=CharSet.Auto)]
//    private unsafe static extern bool CloseHandle(uint hobject);

//    /// <summary> Win32Func: Posts a context based event into an IO Completion Port Thread Pool </summary>
//    [DllImport("Kernel32", CharSet=CharSet.Auto)]
//    private unsafe static extern bool PostQueuedCompletionStatus(uint hCompletionPort, uint uiSizeOfArgument, uint* puiUserArg, OVERLAPPED* pOverlapped);

//    /// <summary> Win32Func: Waits on a context based event from an IO Completion Port Thread Pool.
//    ///           All threads in the pool wait in this Win32 Function </summary>
//    [DllImport("Kernel32", CharSet=CharSet.Auto)]
//    private unsafe static extern bool GetQueuedCompletionStatus(uint hCompletionPort, uint* pSizeOfArgument, uint* puiUserArg, OVERLAPPED** ppOverlapped, uint uiMilliseconds);

//    //------------------------------------------------------------------------------------------
//    // Constants
//    /// <summary> SimTypeConst: This represents the Win32 Invalid Handle Value Macro </summary>
//    private const uint INVALID_HANDLE_VALUE = 0xffffffff;

//    /// <summary> SimTypeConst: This represents the Win32 INFINITE Macro </summary>
//    private const uint INIFINITE = 0xffffffff;

//    /// <summary> SimTypeConst: This tells the IOCP Function to shutdown </summary>
//    private const int SHUTDOWN_IOCPTHREAD = 0x7fffffff;

//    //------------------------------------------------------------------------------------------
//    // Private Properties
//    private uint m_hHandle;
//    /// <summary> SimType: Contains the IO Completion Port Thread Pool handle for this instance </summary>
//    private uint Handle { get { return m_hHandle; } set { m_hHandle = value; } }

//    private int m_uiMaxConcurrency;
//    /// <summary> SimType: The maximum number of threads that may be running at the same time </summary>
//    private int MaxConcurrency { get { return m_uiMaxConcurrency; } set { m_uiMaxConcurrency = value; } }

//    private int m_iMinThreadsInPool;
//    /// <summary> SimType: The minimal number of threads the thread pool maintains </summary>
//    private int MinThreadsInPool { get { return m_iMinThreadsInPool; } set { m_iMinThreadsInPool = value; } }

//    private int m_iMaxThreadsInPool;
//    /// <summary> SimType: The maximum number of threads the thread pool maintains </summary>
//    private int MaxThreadsInPool { get { return m_iMaxThreadsInPool; } set { m_iMaxThreadsInPool = value; } }

//    private object m_pCriticalSection;
//    /// <summary> RefType: A serialization object to protect the class state </summary>
//    private object CriticalSection { get { return m_pCriticalSection; } set { m_pCriticalSection = value; } }

//    private DISPATCHER m_pfnUserFunction;
//    /// <summary> DelType: A reference to a user specified function to be call by the thread pool </summary>
//    private DISPATCHER UserFunction { get { return m_pfnUserFunction; } set { m_pfnUserFunction = value; } }
   
//    private bool m_bDisposeFlag;
//    /// <summary> SimType: Flag to indicate if the class is disposing </summary>
//    private bool IsDisposed { get { return m_bDisposeFlag; } set { m_bDisposeFlag = value; } }

//    /// <summary> SimType: The current number of threads in the thread pool </summary>
//    private int m_iCurThreadsInPool;

//    //------------------------------------------------------------------------------------------
//    // Public Properties
//    public int CurThreadsInPool { get { return m_iCurThreadsInPool; } set { m_iCurThreadsInPool = value; } }
//    /// <summary> SimType: Increment current number of threads in the thread pool </summary>
//    private int IncCurThreadsInPool() { return Interlocked.Increment(ref m_iCurThreadsInPool); }
//    /// <summary> SimType: Decrement current number of threads in the thread pool </summary>
//    private int DecCurThreadsInPool() { return Interlocked.Decrement(ref m_iCurThreadsInPool); }
//    private int m_iActThreadsInPool;
//    /// <summary> SimType: The current number of active threads in the thread pool </summary>
//    public int ActThreadsInPool { get { return m_iActThreadsInPool; } set { m_iActThreadsInPool = value; } }
//    /// <summary> SimType: Increment current number of active threads in the thread pool </summary>
//    private int IncActThreadsInPool() { return Interlocked.Increment(ref m_iActThreadsInPool); }
//    /// <summary> SimType: Decrement current number of active threads in the thread pool </summary>
//    private int DecActThreadsInPool() { return Interlocked.Decrement(ref m_iActThreadsInPool); }
//    private int m_iCurWorkInPool;
//    /// <summary> SimType: The current number of Work posted in the thread pool </summary>
//    public int CurWorkInPool { get { return m_iCurWorkInPool; } set { m_iCurWorkInPool = value; } }
//    /// <summary> SimType: Increment current number of Work posted in the thread pool </summary>
//    private int IncCurWorkInPool() { return Interlocked.Increment(ref m_iCurWorkInPool); }
//    /// <summary> SimType: Decrement current number of Work posted in the thread pool </summary>
//    private int DecCurWorkInPool() { return Interlocked.Decrement(ref m_iCurWorkInPool); }

//    //------------------------------------------------------------------------------------------
//    // Constructor, Finalize, and Dispose
//    /// <summary> Constructor </summary>
//    /// <param name = "pMaxConcurrency"> SimType: Max number of running threads allowed </param>
//    /// <param name = "pMinThreadsInPool"> SimType: Min number of threads in the pool </param>
//    /// <param name = "pMaxThreadsInPool"> SimType: Max number of threads in the pool </param>
//    /// <param name = "pUserFunction"> DelType: Reference to a function to call to perform work </param>
//    /// <exception cref = "Exception"> Unhandled Exception </exception>
//    public IOCPCustomThreadPool(int pMaxConcurrency, int pMinThreadsInPool, int pMaxThreadsInPool, DISPATCHER pUserFunction) {
//      try {
//        // Set initial class state
//        MaxConcurrency = pMaxConcurrency;
//        MinThreadsInPool = pMinThreadsInPool;
//        MaxThreadsInPool = pMaxThreadsInPool;
//        UserFunction = pUserFunction;

//        // Init the thread counters
//        CurThreadsInPool = 0;
//        ActThreadsInPool = 0;
//        CurWorkInPool = 0;

//        // Initialize the Monitor object
//        CriticalSection = new object();

//        // Set the disposing flag to false
//        IsDisposed = false;

//        unsafe {	// Create an IO Completion Port for Thread Pool use
//          Handle = CreateIoCompletionPort(INVALID_HANDLE_VALUE, 0, null, (uint) MaxConcurrency);
//        }
//        if (Handle == 0) {
//          throw new Exception("Unable To Create IO Completion Port");
//        }

//        // Allocate and start the Minimum number of threads specified
//        ThreadStart _tsThread = new ThreadStart(IOCPFunction);
//        for (int _i = 0; _i < MinThreadsInPool; ++_i) {
//          Thread _thread = new Thread(_tsThread);
//          _thread.Name = string.Format("IOCP_{0}", _i);
//          Thread.AllocateNamedDataSlot(_thread.Name);
//          _thread.Start();

//          // Increment the thread pool count
//          IncCurThreadsInPool();
//        }
//      }
//      catch (Exception _ex) {
//        throw new Exception(string.Format("IOCPCustomThreadPool, Unhandled Exception: {0}", _ex));
//      }
//    }

//    //------------------------------------------------------------------------------------------
//    /// <summary> Finalize called by the GC </summary>
//    ~IOCPCustomThreadPool() {
//      if (!IsDisposed)
//        Dispose();
//      }

//    //------------------------------------------------------------------------------------------
//    /// <summary> Called when the object will be shutdown.  This
//    ///           function will wait for all of the work to be completed
//    ///           inside the queue before completing </summary>
//    public void Dispose() {
//      try {
//        IsDisposed = true;

//        // Get the current number of threads in the pool
//        int iCurThreadsInPool = CurThreadsInPool;

//        // Shutdown all thread in the pool
//        for (int iThread = 0; iThread < iCurThreadsInPool; ++iThread) {
//          unsafe {
//            bool _bret = PostQueuedCompletionStatus(Handle, 4, (uint*) SHUTDOWN_IOCPTHREAD, null);
//          }
//        }

//        // Wait here until all the threads are gone
//        while (CurThreadsInPool != 0) Thread.Sleep(100);
//        unsafe {
//          CloseHandle(Handle);
//        }
//      }
//      catch (Exception _ex) {
//        try {
//          EventLog.WriteEntry("Application", string.Format("\r\nIOCPCustomThreadPool Dispose exception:{0}", _ex), EventLogEntryType.Error, 1);
//        }
//        catch { }
		
//      }
//    }

//    //------------------------------------------------------------------------------------------
//    // Private Methods
//    /// <summary> IOCP Worker Function that calls the specified user function </summary>
//    private void IOCPFunction() {
//      uint uiNumberOfBytes;
//      int iValue;

//      try {
//        while (true) {
//          unsafe {	// Wait for an event
//            OVERLAPPED* pOv;
//            GetQueuedCompletionStatus(Handle, &uiNumberOfBytes, (uint*) &iValue, &pOv, INIFINITE);
//          }

//          // Decrement the number of events in queue
//          DecCurWorkInPool();

//          // Was this thread told to shutdown
//          if (iValue == SHUTDOWN_IOCPTHREAD) {
//            break;
//          }

//          // Increment the number of active threads
//          IncActThreadsInPool();
//          try {		// Call the user function
//            UserFunction(iValue);
//          }
//          catch {	}

//          // Get a lock
//          Monitor.Enter(CriticalSection);

//          // If we have less than max threads currently in the pool
//          try {
//            if (CurThreadsInPool < MaxThreadsInPool) {							// Should we add a new thread to the pool
//              if (ActThreadsInPool == CurThreadsInPool) {
//                if (IsDisposed == false) {													// Create a thread and start it
//                  ThreadStart tsThread = new ThreadStart(IOCPFunction);
//                  Thread thThread = new Thread(tsThread);
//                  thThread.Name = "IOCP " + thThread.GetHashCode();
//                  thThread.Start();
//                  // Increment the thread pool count
//                  IncCurThreadsInPool();
//                }
//              }
//            }
//          }
//          catch {	}

//          // Relase the lock and increment the number of active threads
//          Monitor.Exit(CriticalSection);
//          DecActThreadsInPool();
//        }
//      }
//      catch {		}

//      // Decrement the thread pool count
//      DecCurThreadsInPool();
//    }

//    //------------------------------------------------------------------------------------------
//    // Public Methods
//    /// <summary> IOCP Worker Function that calls the specified user function </summary>
//    /// <param name="iValue"> SimType: A value to be passed with the event </param>
//    /// <exception cref = "Exception"> Unhandled Exception </exception>
//    public void PostEvent(int iValue) {
//      try {
//        if (IsDisposed) {
//          // Only add work if we are not disposing
//          return;
//        }
//        unsafe {
//          // Post an event into the IOCP Thread Pool
//          PostQueuedCompletionStatus(Handle, 4, (uint*) iValue, null);
//        }

//        // Increment the number of item of work
//        IncCurWorkInPool();

//        // Get a lock
//        Monitor.Enter(CriticalSection);

//        // If we have less than max threads currently in the pool
//        // Should we add a new thread to the pool
//        try {
//          if (CurThreadsInPool < MaxThreadsInPool) {
//            if (ActThreadsInPool == CurThreadsInPool) {
//              if (IsDisposed == false) {
//                ThreadStart tsThread = new ThreadStart(IOCPFunction);
//                Thread thThread = new Thread(tsThread);
//                thThread.Name = "IOCP " + thThread.GetHashCode();
//                thThread.Start();
//                IncCurThreadsInPool();
//              }
//            }
//          }
//          else {
//            //TODO: Log: (CurThreadsInPool >= MaxThreadsInPool)
//          }
//        }
//        catch {}
//        Monitor.Exit(CriticalSection);
//      }
//      catch (Exception) {
//        throw;
//      }
//    }

//    //*****************************************
//    /// <summary> IOCP Worker Function that calls the specified user function </summary>
//    /// <exception cref = "Exception"> Unhandled Exception </exception>
//    public void PostEvent() {
//      if (IsDisposed) {
//        // Only add work if we are not disposing
//        return;
//      }

//      unsafe {
//        // Post an event into the IOCP Thread Pool
//        PostQueuedCompletionStatus(Handle, 0, null, null);
//      }

//      // Increment the number of item of work
//      IncCurWorkInPool();

//      // Get a lock
//      Monitor.Enter(CriticalSection);

//      // If we have less than max threads currently in the pool
//      // Should we add a new thread to the pool
//      try {
//        if (CurThreadsInPool < MaxThreadsInPool) {
//          if (ActThreadsInPool == CurThreadsInPool) {
//            if (IsDisposed == false) {
//              ThreadStart tsThread = new ThreadStart(IOCPFunction);
//              Thread thThread = new Thread(tsThread);
//              thThread.Name = "IOCP " + thThread.GetHashCode();
//              thThread.Start();
//              IncCurThreadsInPool();
//            }
//          }
//        }
//        else {
//          //TODO: Log: (CurThreadsInPool >= MaxThreadsInPool)
//        }
//      }
//      catch {}
//      Monitor.Exit(CriticalSection);
//    }
//  }
//}