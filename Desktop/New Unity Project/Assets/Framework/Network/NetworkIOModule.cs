/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

namespace Pomelo
{
    public abstract class NetworkIOModule
    {
        /// <summary>
        ///   
        /// </summary>
        protected ClientNetwork client_network_;

        /// <summary>
        ///   处理函数集
        /// </summary>
        protected Dictionary<string, System.Action<JsonObject>> handlers_ = new Dictionary<string, System.Action<JsonObject>>();

        /// <summary>
        ///   
        /// </summary>
        public NetworkIOModule(ClientNetwork client_network)
        {
            client_network_ = client_network;
        }

        /// <summary>
        ///   
        /// </summary>
        ~NetworkIOModule()
        {
            client_network_ = null;
        }

        /// <summary>
        ///   
        /// </summary>
        public void Start()
        {
            Dispatch();
        }

        /// <summary>
        ///   
        /// </summary>
        public void Close()
        {
            CloseDispatch();
        }

        /// <summary>
        ///   
        /// </summary>
        public void Dispatch()
        {
            if (client_network_ != null && handlers_ != null)
            {
                var itr = handlers_.GetEnumerator();
                while (itr.MoveNext())
                {
                    client_network_.RegisterHandler(itr.Current.Key, itr.Current.Value);
                }
                itr.Dispose();
            }
        }

        /// <summary>
        ///   
        /// </summary>
        public void CloseDispatch()
        {
            if (client_network_ != null && handlers_ != null)
            {
                var itr = handlers_.GetEnumerator();
                while (itr.MoveNext())
                {
                    client_network_.UnregisterHandler(itr.Current.Key, itr.Current.Value);
                }
                itr.Dispose();
            }
        }
    }
}


