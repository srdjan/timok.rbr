//using System;
//using System.Collections;
//using System.ComponentModel;
//using Timok.Core.Logger;
//using Timok.Core.Logging;

//namespace Timok.Core.NetworkLib.Tcp {
//  //==
//  public enum ChannelPoolRole {
//    [Description("Primary")] Primary,
//    [Description("Secondary")] Secondary
//  }

//  //==
//  public class ChannelPool : IDisposable {
//    static readonly object padlock = new object();
//    readonly ArrayList pool;
//    int available;
//    readonly string serverIPAndPort;
//    bool statusIsOk;

//    public string ServerIPAndPort {
//      get { return serverIPAndPort; }
//    }

//    readonly int capacity;
//    public int Capacity {
//      get { return capacity; }
//    }

//    readonly ChannelPoolRole role;
//    public ChannelPoolRole Role {
//      get { return role; }
//    }

//    public bool StatusIsOk {
//      get { return statusIsOk; }
//    }

//    //-- Constructor
//    public ChannelPool(ChannelPoolConfig pConfig) {
//      serverIPAndPort = pConfig.ToString();
//      role = pConfig.Role;
//      capacity = pConfig.Capacity;
//      pool = ArrayList.Synchronized(new ArrayList(capacity));
//      available = 0;
//      statusIsOk = true;
//    }

//    public void Dispose() {
//      IPoolableChannel _channel;
//      lock (padlock) {
//        for (var _i = 0; _i < capacity; _i++) {
//          _channel = (IPoolableChannel) pool[_i];
//          _channel.Disconnect();
//        }
//      }
//    }

//    public void Add(IPoolableChannel pChannel) {
//      try {
//        lock (padlock) {
//          pool.Add(pChannel);
//          available++;
//        }
//      }
//      catch (Exception _ex) {
//        T.LogRbr(LogSeverity.Critical, "ChannelPool.Add", string.Format("Exception\r\n{0}", _ex));
//      }
//    }

//    //--- Send and receive, message = header(containing body length) + body
//    public string SendAndReceive(bool pIsHeartbeat, string pRequestMessage, int pHeaderLength) {
//      IPoolableChannel _channel = null;
//      try {
//        //- if heartbeat, borrow zero channel
//        _channel = borrow(pIsHeartbeat);
//        if (_channel == null) {
//          throw new Exception(role + "Pool, All Channels in USE");
//        }

//        //-- send request
//        _channel.Send(pRequestMessage);

//        //-- receive header first
//        var _responseHeader = _channel.Receive(pHeaderLength);
//        var _messageLength = int.Parse(_responseHeader);

//        // receive response message body and return it with preappended header:
//        return _responseHeader + _channel.Receive(_messageLength);
//      }
//      catch (ConnectionTimeoutException) {
//        statusIsOk = false;
//        throw;
//      }
//      finally {
//        if (_channel != null) {
//          release(_channel);
//        }
//      }
//    }

//    //---------------------------------- private -----------------------------------------------------------
//    IPoolableChannel borrow(bool pIsHeartbeat) {
//      lock (padlock) {
//        var _numberOfChannelsToTry = capacity;
//        if (pIsHeartbeat) {
//          // for heartbits
//          _numberOfChannelsToTry = 1;
//        }

//        for (var _i = 0; _i < _numberOfChannelsToTry; _i++) {
//          var _channel = (IPoolableChannel) pool[_i];
//          if (_channel.Acquire()) {
//            available--;
//            T.LogRbr(LogSeverity.Debug, "ChannelPool.borrow", string.Format("({0}) | {1} OK: AvailablePool size: {2}", _channel.Index, role, available));
//            return _channel;
//          }
//        }
//      }
//      T.LogRbr(LogSeverity.Critical, "ChannelPool.borrow", string.Format(" {0} All USED, AvailablePool size={1}", role, available));
//      return null;
//    }

//    void release(IPoolableChannel pChannel) {
//      lock (padlock) {
//        if (pChannel.Release()) {
//          available++;
//          T.LogRbr(LogSeverity.Debug, "ChannelPool.borrow", string.Format("({0}) | {1} OK: Available Pool size={2}", pChannel.Index, role, available));
//          return;
//        }
//        T.LogRbr(LogSeverity.Debug, "ChannelPool.borrow", string.Format("({0}) | {1} ERROR, Available Pool size={2}", pChannel.Index, role, available));
//      }
//    }
//  }
//}