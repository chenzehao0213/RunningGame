/***************************************************************
 * Copyright 2016 By Zhang Minglin
 * Author: Zhang Minglin
 * Create: 2016/02/23
 * Note  : Pomelo网络客户端底层（负责创建连接服务器、关闭服务器等操作）
***************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using Pomelo.DotNetClient;

namespace Pomelo
{
    /// <summary>
    ///   
    /// </summary>
    public class ClientNetwork : System.IDisposable
    {
        /// <summary>
        ///   消息可缓存总量
        /// </summary>
        public const int MESSAGE_BUFFER_COUNT = 128;

        /// <summary>
        ///   消息类
        /// </summary>
        /// 
        public class Message
        {
            //消息类别
            public emNetworkMessageType MessageType = emNetworkMessageType.ON_NORMAL;
            //服务端发送方法的名字
            public string Route = null;
            //响应数据
            public JsonObject Data = null;
            //响应
            public System.Action<JsonObject> Handler = null;

            public Message(emNetworkMessageType msg_type)
            {
                MessageType = msg_type;
            }

            public Message(emNetworkMessageType msg_type, JsonObject data)
            {
                MessageType = msg_type;
                Data = data;
            }

            public Message(emNetworkMessageType msg_type
                            , string route
                            , JsonObject data)
            {
                MessageType = msg_type;
                Route = route;
                Data = data;
            }

            public Message(emNetworkMessageType msg_type
                            , string route
                            , JsonObject data
                            , System.Action<JsonObject> handler)
            {
                MessageType = msg_type;
                Route = route;
                Data = data;
                Handler = handler;
            }

            public override string ToString()
            {
                string str = "";
                str += "MessageType:" + MessageType.ToString();
                if (!string.IsNullOrEmpty(Route))
                    str += " | Route:" + Route.ToString();
                if (Data != null)
                    str += " | Data:" + Data.ToString();
                return str;
            }
        }

        /// <summary>
        ///   
        /// </summary>
        private bool disposed_ = false;

        /// <summary>
        ///   主机IP
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        ///   端口
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        ///   
        /// </summary>
        public PomeloClient NetworkLayer { get; private set; }

        /// <summary>
        ///   是否已连接成功
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        ///   连接ID
        /// </summary>
        public long SessionID;

        /// <summary>
        ///   连接UID
        /// </summary>
        public long SessionUID;

        /// <summary>
        ///   单帧处理数据总量
        /// </summary>
        public int FrameWorkCount;

        /// <summary>
        ///   连接事件
        /// </summary>
        public System.Action OnConnectHandler;

        /// <summary>
        ///   断开连接事件
        /// </summary>
        public System.Action OnDisconnectHandler;

        /// <summary>
        ///   超时事件
        /// </summary>
        public System.Action OnTimeOutHandler;

        /// <summary>
        ///   错误事件
        /// </summary>
        public System.Action OnErrorHandler;

        /// <summary>
        /// 登录所需的数据
        /// </summary>
        private JsonObject login_data_;

        /// <summary>
        ///   
        /// </summary>
        public ClientNetwork()
        {

        }

        /// <summary>
        ///   
        /// </summary>
        public ClientNetwork(string host, int port, int frame_work_count = 30)
        {
            Host = host;
            Port = port;
            FrameWorkCount = frame_work_count;
            IsConnected = false;
        }

        /// <summary>
        ///   
        /// </summary>
        ~ClientNetwork()
        {
            Dispose(false);
        }

        /// <summary>
        ///   
        /// </summary>
        public void Run()
        {
            RunHandlerMessage();
        }

        /// <summary>
        ///   
        /// </summary>
        public void Connect(JsonObject login_data, System.Action<JsonObject> callback)
        {
            if (IsConnected)
                return;

            login_data_ = login_data;
            NetworkLayer = new PomeloClient();
            NetworkLayer.NetWorkStateChangedEvent += GateNetWorkStateChangedEvent;
            NetworkLayer.initClient(Host, Port, () =>
            {
                NetworkLayer.connect(null, (data) =>
                {
                    NetworkLayer.request("gate.gateHandler.queryEntry", (result) =>
                    {
                        GateQueryEntryHandler(result, callback);
                    });
                });
            });
        }

        /// <summary>
        ///   断开
        /// </summary>
        public void Disconnect()
        {
            if (IsConnected && NetworkLayer != null)
                NetworkLayer.disconnect();
        }

        /// <summary>
        ///   通知
        /// </summary>
        public void Notify(string route, JsonObject msg)
        {
            if (IsConnected && NetworkLayer != null)
                NetworkLayer.notify(route, msg);
        }

        /// <summary>
        ///   请求
        /// </summary>
        public void Request(string route, System.Action<JsonObject> action)
        {
            if (NetworkLayer != null)
            {
                NetworkLayer.request(route, (data) =>
                {
                    PushMessage(new Message(emNetworkMessageType.ON_NORMAL
                                            , route
                                            , data
                                            , action));
                });
            }
        }

        /// <summary>
        ///   
        /// </summary>
        public void Request(string route, JsonObject msg, System.Action<JsonObject> action)
        {
            if (NetworkLayer != null)
            {
                Debug.Log("Request() - " + route);
                NetworkLayer.request(route, msg, (data) =>
                {
                    PushMessage(new Message(emNetworkMessageType.ON_NORMAL
                                            , route
                                            , data
                                            , action));
                });
            }
        }

        /// <summary>
        ///   
        /// </summary>
        void AddOnEvent(string eventName, System.Action<JsonObject> action)
        {
            if (NetworkLayer != null)
                NetworkLayer.addOnEvent(eventName, action);
        }

        /// <summary>
        ///   
        /// </summary>
        void RemoveOnEvent(string eventName, System.Action<JsonObject> action)
        {
            if (NetworkLayer != null)
                NetworkLayer.removeOnEvent(eventName, action);
        }

        /// <summary>
        ///   
        /// </summary>
        void NetWorkStateChangedEvent(NetWorkState state)
        {
            if (state == NetWorkState.ERROR)       //错误
            {
                PushMessage(new Message(emNetworkMessageType.ON_ERROR));
            }
            else if (state == NetWorkState.DISCONNECTED)       //断开的
            {
                PushMessage(new Message(emNetworkMessageType.ON_DISCONNECT));
            }
            else if (state == NetWorkState.TIMEOUT)       //超时
            {
                PushMessage(new Message(emNetworkMessageType.ON_TIMEOUT));
            }
        }

        /// <summary>
        ///   在连接
        /// </summary>
        void OnConnect()
        {
            IsConnected = true;
            if (OnConnectHandler != null)
                OnConnectHandler();
        }

        /// <summary>
        ///   在断开
        /// </summary>
        void OnDisconnect()
        {
            IsConnected = false;
            SessionID = 0;
            SessionUID = 0;
            if (OnDisconnectHandler != null)
                OnDisconnectHandler();
        }

        /// <summary>
        ///   
        /// </summary>
        void OnTimeOut()
        {
            IsConnected = false;
            SessionID = 0;
            SessionUID = 0;
            if (OnTimeOutHandler != null)
                OnTimeOutHandler();
        }

        /// <summary>
        ///   
        /// </summary>
        void OnError()
        {
            IsConnected = false;
            SessionID = 0;
            SessionUID = 0;
            if (OnErrorHandler != null)
                OnErrorHandler();
        }

        #region Connect's process
        /// <summary>
        ///   
        /// </summary>
        void GateNetWorkStateChangedEvent(NetWorkState state)
        {
            if (state == NetWorkState.ERROR)
            {
                PushMessage(new Message(emNetworkMessageType.ON_ERROR));
            }
            else if (state == NetWorkState.TIMEOUT)
            {
                PushMessage(new Message(emNetworkMessageType.ON_TIMEOUT));
            }
        }

        /// <summary>
        ///   
        /// </summary>
        void GateQueryEntryHandler(JsonObject result, System.Action<JsonObject> callback)
        {
            if (System.Convert.ToInt32(result["code"]) == (int)emNetworkCode.SUCCEEDED)
            {
                NetworkLayer.disconnect();

                string host = (string)result["host"];
                int port = System.Convert.ToInt32(result["port"]);
                NetworkLayer = new PomeloClient();
                Debug.Log("connecting Connector[" + host + " " + port + "]");

                //侦听网络状态更改事件
                NetworkLayer.NetWorkStateChangedEvent += NetWorkStateChangedEvent;
                NetworkLayer.initClient(host, port);
                NetworkLayer.connect(null, (data) =>
                {
                    ConnectConnectorHandler(callback);
                });
            }
        }

        /// <summary>
        ///   connector server is connected
        ///   连接服务器连接
        /// </summary>
        void ConnectConnectorHandler(System.Action<JsonObject> callback)
        {
            //模块事件调度
            Dispatch();
            if (NetworkLayer != null)
            {
                Request("connector.entryHandler.entry", login_data_
                                , (result) =>
                                {
                                    ConnectorEnterHandler(result, callback);
                                });
            }
        }

        /// <summary>
        ///   
        /// </summary>
        void ConnectorEnterHandler(JsonObject result, System.Action<JsonObject> callback)
        {
            if (callback != null)
                callback(result);

            int code = System.Convert.ToInt32(result["code"]);
            if (code == (int)emNetworkCode.SUCCEEDED)
            {
                //绑定连接ID
                SessionID = System.Convert.ToInt32(result["sessionId"]);
                SessionUID = System.Convert.ToInt32(result["sessionUid"]);

                PushMessage(new Message(emNetworkMessageType.ON_CONNECT));
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed_)
                return;

            if (NetworkLayer != null)
            {
                NetworkLayer.Dispose();
                NetworkLayer = null;
            }

            this.disposed_ = true;
        }
        #endregion

        #region Message
        /// <summary>
        ///   消息缓存
        /// </summary>
        private List<Message> message_buffer_ = new List<Message>();

        /// <summary>
        ///   
        /// </summary>
        void PushMessage(Message msg)
        {
            lock (message_buffer_)
            {
                if (message_buffer_.Count < MESSAGE_BUFFER_COUNT)
                {
                    message_buffer_.Add(msg);
                }
                else
                {
                    //断开连接
                    NetworkLayer.disconnect();
                }
            }
        }

        /// <summary>
        ///   运行处理消息流程
        /// </summary>
        void RunHandlerMessage()
        {
            if (message_buffer_.Count > 0)
            {
                List<Message> buffer = null;
                lock (message_buffer_)
                {
                    int count = message_buffer_.Count < FrameWorkCount ? message_buffer_.Count : FrameWorkCount;
                    if (count > 0)
                    {
                        buffer = message_buffer_.GetRange(0, count);        //获取 单帧 处理消息数量
                        message_buffer_.RemoveRange(0, count);
                    }
                }
                if (buffer != null)
                {
                    for (int index = 0; index < buffer.Count; ++index)
                    {
                        Message current = buffer[index];
                        if (current.MessageType == emNetworkMessageType.ON_NORMAL)         //普通消息
                        {
                            HandlerMessage(current);
                        }
                        else if (current.MessageType == emNetworkMessageType.ON_CONNECT)   //连接通知
                        {
                            OnConnect();
                        }
                        else if (current.MessageType == emNetworkMessageType.ON_DISCONNECT)   //断开连接通知
                        {
                            OnDisconnect();
                        }
                        else if (current.MessageType == emNetworkMessageType.ON_ERROR)       //错误通知
                        {
                            OnError();
                        }
                        else if (current.MessageType == emNetworkMessageType.ON_TIMEOUT)    //超时连接通知
                        {
                            OnTimeOut();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///   处理消息
        /// </summary>
        void HandlerMessage(Message message)
        {
            try
            {
                if (!string.IsNullOrEmpty(message.Route))
                {
                    if (message.Handler != null)
                    {
                        message.Handler(message.Data);
                    }
                    else if (handlers_.ContainsKey(message.Route))
                    {
                        if (handlers_[message.Route] != null)
                        {
                            var list = handlers_[message.Route];
                            for (int i = 0; i < list.Count; ++i)
                            {
                                if (list[i] != null)
                                    list[i](message.Data);
                            }

                        }

                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("HandlerMessage() occured error!"
                                + "\nClientNetwork.Message:" + message.ToString()
                                + "\nException:\n" + ex.Message);
            }
        }
        #endregion

        #region Handler
        /// <summary>
        ///   事件集
        /// </summary>
        private Dictionary<string, System.Action<JsonObject>> events_ = new Dictionary<string, System.Action<JsonObject>>();
        /// <summary>
        ///   消息处理缓存
        /// </summary>
        private Dictionary<string, List<System.Action<JsonObject>>> handlers_ = new Dictionary<string, List<System.Action<JsonObject>>>();

        /// <summary>
        ///   注册处理接口
        /// </summary>
        public void RegisterHandler(string route, System.Action<JsonObject> handler)
        {
            List<System.Action<JsonObject>> list = null;
            if (handlers_.TryGetValue(route, out list))
            {
                if (!list.Contains(handler))
                    list.Add(handler);
            }
            else
            {
                //注册处理接口
                list = new List<System.Action<JsonObject>>();
                list.Add(handler);
                handlers_.Add(route, list);

                //增加监听
                System.Action<JsonObject> on_event = delegate (JsonObject data)
                {
                    PushMessage(new Message(emNetworkMessageType.ON_NORMAL
                                                , route
                                                , data));
                };
                events_.Add(route, on_event);
                AddOnEvent(route, on_event);
            }
        }

        /// <summary>
        ///   注销处理接口
        /// </summary>
        public void UnregisterHandler(string route, System.Action<JsonObject> handler)
        {
            List<System.Action<JsonObject>> list = null;
            if (handlers_.TryGetValue(route, out list))
            {
                if (list.Contains(handler))
                    list.Remove(handler);

                if (list.Count == 0)
                {
                    RemoveOnEvent(route, events_[route]);
                    handlers_.Remove(route);
                    events_.Remove(route);
                }
            }
        }

        /// <summary>
        ///   事件调度
        /// </summary>
        void Dispatch()
        {
            var itr = events_.GetEnumerator();
            while (itr.MoveNext())
            {
                AddOnEvent(itr.Current.Key, itr.Current.Value);
            }
            itr.Dispose();
        }

        /// <summary>
        ///   关闭事件调度
        /// </summary>
        void CloseDispatch()
        {
            var itr = events_.GetEnumerator();
            while (itr.MoveNext())
            {
                RemoveOnEvent(itr.Current.Key, itr.Current.Value);
            }
            itr.Dispose();
        }
        #endregion
    }
}