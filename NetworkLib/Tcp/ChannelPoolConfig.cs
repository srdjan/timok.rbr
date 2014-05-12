//using System;
//using System.Configuration;
//using System.Diagnostics;
//using System.Net;
//using System.Text;

//namespace Timok.Core.NetworkLib.Tcp {
//  public sealed class ChannelPoolConfig {
//    public IPAddress IP;
//    public int Port;
//    public string Login;
//    public string Password;
//    public TimeSpan ConnectionTimeout;
//    public int ReceiveTimeout;
//    public ChannelPoolRole Role;
//    public int Capacity;

//    public ChannelPoolConfig(ChannelPoolRole pRole) {
//      Role = pRole;

//      //-- ip address 
//      IP = null;
//      var _serverAddress = ConfigurationManager.AppSettings[pRole + "PoolAddress"];
//      if(!string.IsNullOrEmpty(_serverAddress)) {
//        IP = IPAddress.Parse(_serverAddress);
//      }

//      var _logMessage = new StringBuilder();
//      _logMessage.Append("\r\nAddress: " + IP);

//      //-- port
//      Port = 0;
//      var _serverPort = ConfigurationManager.AppSettings[pRole + "PoolPort"];
//      if(!string.IsNullOrEmpty(_serverPort)) {
//        Port = int.Parse(_serverPort);
//      }
//      _logMessage.Append("\r\nPort: " + Port);
			
//      //-- get LoginId from config file:
//      Login = string.Empty;
//      var _loginId = ConfigurationManager.AppSettings[pRole + "PoolLogin"];
//      if(!string.IsNullOrEmpty(_loginId)) {
//        Login = _loginId;
//      }
//      _logMessage.Append("\r\nLoginId: " + Login);

//      //-- get Password from config file:
//      Password = string.Empty;
//      var _password = ConfigurationManager.AppSettings[pRole + "PoolPassword"];
//      if(!string.IsNullOrEmpty(_password)) {
//        Password = _password;
//      }
//      _logMessage.Append("\r\nPassword: " + Password);
 
//      //--defaultConnectionTimeout
//      ConnectionTimeout = new TimeSpan(0);
//      var _connTimeout = ConfigurationManager.AppSettings[pRole + "PoolConnTimeout"];
//      if(_connTimeout != null && Utils.IsNumeric(_connTimeout)) {
//        ConnectionTimeout = TimeSpan.FromSeconds(int.Parse(_connTimeout));
//      }
//      _logMessage.Append("\r\nConnectionTimeout: " + ConnectionTimeout.Seconds);

//      //--defaultReceiveTimeout
//      ReceiveTimeout = 0;
//      var _recvTimeout = ConfigurationManager.AppSettings[pRole + "PoolReceiveTimeout"];
//      if(_recvTimeout != null && Utils.IsNumeric(_recvTimeout)) {
//        ReceiveTimeout = int.Parse(_recvTimeout);
//      }
//      _logMessage.Append("\r\nConnectionTimeout: " + ConnectionTimeout.Seconds);

//      //--Pool capacity
//      Capacity = 1;
//      var _poolCapacity = ConfigurationManager.AppSettings[pRole + "PoolCapacity"];
//      if(!string.IsNullOrEmpty(_poolCapacity)) {
//        Capacity = int.Parse(_poolCapacity);
//      }
//      _logMessage.Append("\r\nConnection Pool capacity: " + Capacity);

//      EventLog.WriteEntry("Application", "\r\nChannelPoolConfig.Ctor:" + _logMessage, EventLogEntryType.Information, 1);
//    }

//    public override string ToString() {
//      return IP + ":" + Port;
//    }
//  }										
//}