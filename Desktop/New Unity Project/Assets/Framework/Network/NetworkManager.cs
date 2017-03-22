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
using System;
using System.Reflection;

namespace Pomelo
{
    public class NetworkManager
    {
        /// <summary>
        ///   
        /// </summary>
        private Dictionary<string, NetworkIOModule> modules_;

        /// <summary>
        ///   
        /// </summary>
        public ClientNetwork Client;

        /// <summary>
        ///   
        /// </summary>
        public NetworkManager()
        {
            Debug.Log("TODO这里要传递Host跟Prot");
            this.modules_ = new Dictionary<string, NetworkIOModule>();
            this.Client = new ClientNetwork();
        }

        /// <summary>
        ///   
        /// </summary>
        public void AddNetworkModule(NetworkIOModule module) 
        {
            string name = module.GetType().ToString();
            if (!modules_.ContainsKey(name))
            {
                modules_.Add(name, module);
            }
        }

        /// <summary>
        ///   
        /// </summary>
        public void AddNetworkModule<T>()
            where T : NetworkIOModule
        {
            string name = typeof(T).ToString();
            object module = Activator.CreateInstance(typeof(T), Client);
            if (!modules_.ContainsKey(name))
            {
                modules_.Add(name, (T)module);
            }
        }

        /// <summary>
        ///   发送请求
        /// </summary>
        public void SendRequest<T>(string method)
            where T : NetworkIOModule
        {
            SendRequest<T>(method, null);
        }

        /// <summary>
        ///   
        /// </summary>
        public void SendRequest<T>(string method, object[] obj)
           where T : NetworkIOModule
        {
            T module = null;
            string name = typeof(T).ToString();
            if (modules_.ContainsKey(name))
                module = modules_[name] as T;
            else
                Debug.LogError(typeof(T).ToString() + "未Add!");

            BindingFlags f = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            Type type = module.GetType();
            MethodInfo mi = type.GetMethod(method, f);
            mi.Invoke(module, obj);
        }

        /// <summary>
        ///   
        /// </summary>
        public void StartUp()
        {
            var itr = modules_.Values.GetEnumerator();
            while (itr.MoveNext())
                itr.Current.Start();
            itr.Dispose();
        }

        /// <summary>
        ///   
        /// </summary>
        public void Update()
        {
            if (Client != null)
                Client.Run();
        }

    }
}

