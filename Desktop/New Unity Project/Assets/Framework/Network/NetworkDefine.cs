/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System.Collections;

namespace Pomelo
{

    /// <summary>
    ///   网络事件代码
    /// </summary>
    public enum emNetworkCode
    {
        SUCCEEDED = 200,              // 成功  
        FAILED = 500,              // 失败

        //Auth
        ACCOUNT_NONEXIST = 10001,            // 账号不存在
        PASSWORD_ERROR = 10002,            // 密码错误
        //Player
        PLAYER_NONEXIST = 3001,            //角色信息不存在
    }


    /// <summary>
    ///   消息类别
    /// </summary>
    public enum emNetworkMessageType
    {
        ON_NORMAL,              // 普通消息
        ON_CONNECT,             // 连接通知
        ON_DISCONNECT,          // 断开连接通知
        ON_TIMEOUT,             // 超时连接通知
        ON_ERROR,               // 错误通知
    }
}

