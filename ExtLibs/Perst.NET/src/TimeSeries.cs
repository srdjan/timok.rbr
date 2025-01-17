namespace Perst
{
    using System;
	using System.Collections;

    /// <summary>
    /// Interface for timeseries element.
    /// You should derive your time series element from this class
    /// and implement Time getter method.
    /// </summary>
    public interface TimeSeriesTick 
    { 
        /// <summary>
        /// Get time series element timestamp (100 nanoseconds)
        /// </summary>
        long Time {get;}
    }

    /// <summary>
    /// Abstract base class for time series block.
    /// Progammer has to define its own block class derived from this class
    /// containign array of time series elements and providing accessors to the array elements 
    /// and Ticks getter method to access this whole array.
    /// </summary>
    public abstract class TimeSeriesBlock : Persistent 
    { 
        public long timestamp;
        public int  used;

        /// <summary>
        /// Get time series elements stored in this block.
        /// Returns preallocated array of time series element. Only <code>used</code>
        /// items of this array actually contains time series elements.
        /// </summary>
        public abstract Array Ticks{get;}

        /// <summary>
        /// Array elements accessor. 
        /// </summary>
        public abstract TimeSeriesTick this[int i] {get; set;}
    }
    
    /// <summary>
    /// <p>
    /// Time series interface. Time series class is used for efficient
    /// handling of time series data. Ussually time series contains a very large number
    /// if relatively small elements which are ussually acessed in sucessive order. 
    /// To avoid overhead of loading from the disk each particular time series element, 
    /// this class group several subsequent time series elements together and store them 
    /// as single object (block).
    /// </p><p> 
    /// As far as C# currently has no templates and
    /// Perst need to know format of block class, it is responsibity of prgorammer
    /// to create block implementation derived from TimeSeriesBlock class
    /// and containing array of time series elements. Size of this array specifies
    /// the size of the block.
    /// </p>
    /// </summary>
    public interface TimeSeries : IPersistent, IResource, IEnumerable 
    {    
        /// <summary>
        /// Add new tick to time series
        /// </summary>
        /// <param name="tick">new time series element</param>
        void Add(TimeSeriesTick tick);    

        /// <summary>
        /// Get forward iterator for time series elements belonging to the specified range
        /// </summary>
        /// <param name="from">inclusive time of the begging of interval</param>
        /// <param name="till">inclusive time of the ending of interval</param>
        /// <returns>forward iterator within specified range</returns>
        IEnumerator GetEnumerator(DateTime from, DateTime till);

        /// <summary>
        /// Get iterator through all time series elements
        /// </summary>
        /// <param name="order">direction of iteration</param>
        /// <returns>iterator in specified direction</returns>
        IEnumerator GetEnumerator(IterationOrder order);

        /// <summary>
        /// Get forward iterator for time series elements belonging to the specified range
        /// </summary>
        /// <param name="from">inclusive time of the begging of interval</param>
        /// <param name="till">inclusive time of the ending of interval</param>
        /// <param name="order">direction of iteration</param>
        /// <returns>iterator within specified range in specified direction</returns>
        IEnumerator GetEnumerator(DateTime from, DateTime till, IterationOrder order);

        /// <summary>
        /// Get forward iterator for time series elements belonging to the specified range
        /// </summary>
        /// <param name="from">inclusive time of the begging of interval</param>
        /// <param name="till">inclusive time of the ending of interval</param>
        /// <returns>forward iterator within specified range</returns>
        IEnumerable Range(DateTime from, DateTime till);

        /// <summary>
        /// Get iterator through all time series elements
        /// </summary>
        /// <param name="order">direction of iteration</param>
        /// <returns>iterator in specified direction</returns>
        IEnumerable Range(IterationOrder order);

        /// <summary>
        /// Get forward iterator for time series elements belonging to the specified range
        /// </summary>
        /// <param name="from">inclusive time of the begging of interval</param>
        /// <param name="till">inclusive time of the ending of interval</param>
        /// <param name="order">direction of iteration</param>
        /// <returns>iterator within specified range in specified direction</returns>
        IEnumerable Range(DateTime from, DateTime till, IterationOrder order);

        /// <summary>
        /// Get forward iterator for time series elements with timestamp greater or equal than specified
        /// </summary>
        /// <param name="from">inclusive time of the begging of interval</param>
        /// <returns>forward iterator</returns>
        IEnumerable From(DateTime from);

        /// <summary>
        /// Get backward iterator for time series elements with timestamp less or equal than specified
        /// </summary>
        /// <param name="till">inclusive time of the eding of interval</param>
        /// <returns>backward iterator</returns>
        IEnumerable Till(DateTime till);

        /// <summary>
        /// Get backward iterator for time series elements 
        /// </summary>
        /// <returns>backward iterator</returns>
        IEnumerable Reverse();

        /// <summary>
        /// Get timestamp of first time series element
        /// </summary>
        /// <exception cref="Perst.StorageError">StorageError(StorageError.ErrorClass.KEY_NOT_FOUND) if time series is empy</exception>
        DateTime FirstTime {get;}

        /// <summary>
        /// Get timestamp of last time series element
        /// </summary>
        /// <exception cref="Perst.StorageError">StorageError(StorageError.ErrorClass.KEY_NOT_FOUND) if time series is empy</exception>
        DateTime LastTime {get;}

        /// <summary>
        /// Get number of elements in time series
        /// </summary>
        long Count {get;}

        /// <summary> 
        /// Get tick for specified data
        /// </summary>
        /// <param name="timestamp">time series element timestamp</param>
        TimeSeriesTick this[DateTime timestamp] 
        {
            /// <summary> 
            /// Get tick for specified data
            /// </summary>
            /// <returns>time series element for the specified timestamp or null
            /// if no such element was found</returns>
            get;
        }
    
        /// <summary>
        /// Check if data is available in time series for the specified time
        /// </summary>
        /// <param name="timestamp">time series element timestamp</param>
        /// <returns><code>true</code> if there is element in time series with such timestamp, 
        /// <code>false</code> otherwise</returns>
        bool Contains(DateTime timestamp);

        /// <summary>
        /// Remove time series elements belonging to the specified range
        /// </summary>
        /// <param name="from">inclusive time of the begging of interval</param>
        /// <param name="till">inclusive time of the ending of interval</param>
        /// <returns>number of removed elements</returns>
        long Remove(DateTime from, DateTime till);

        /// <summary>
        /// Remove time series elements with timestamp greater or equal then specified
        /// </summary>
        /// <param name="from">inclusive time of the begging of interval</param>
        /// <returns>number of removed elements</returns>
        long RemoveFrom(DateTime from);

        /// <summary>
        /// Remove time series elements with timestamp less or equal then specified
        /// </summary>
        /// <param name="till">inclusive time of the ending of interval</param>
        /// <returns>number of removed elements</returns>
        long RemoveTill(DateTime till);

        /// <summary>
        /// Remove all time series elements
        /// </summary>
        /// <returns>number of removed elements</returns>
        long RemoveAll();
    }
}
