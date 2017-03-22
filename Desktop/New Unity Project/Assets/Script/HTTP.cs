using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HTTP : MonoBehaviour
{
    //登录错误返回的错误信息
    public string TextC = "";
    public static string postUrl = "119.23.145.192:3010/login";

    #region 
    ///<summary>
    ///post 用户的登录信息
    ///</summary>
    private void login(string username,string password)
    {
        Dictionary<string,string> dict = new Dictionary<string,string>();
        dict.Add("1",username);
        dict.Add("100001",password);
        StartCoroutine(PostMessages(postUrl,dict));
    }

    private IEnumerator PostMessages(string url, Dictionary<string, string> post)
    {
        private string mContent;
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string,string> post_arg in post)
        {
            form.AddField(post_arg.Key , post_arg.value);
        }
        WWW www = new WWW(url,form);
        yield return www;
        //登录失败
        if(!string.IsNullOrEmpty（www.error）)
        {
            private int ERR_TYPE;//解析错误的信息 ,从error解析出来
            switch(ERR_TYPE)
            {
                case -1 : 
                    TextC = "登录失败，请重新登录";
                    break;
                case -2 : 
                    TextC = "用户不存在";
                    break;
                case -3:
                    TextC = "用户密码错误，请重新输入";
                    break;
                case -4;
                    TextC = "用户已被冻结";
                    break;
                case -5;
                    TextC = "登录的用户在线";
                    break;
             }
        }

        //登录成功
        else
        {
            //拉取用户的资料
            //将token返回给客户端
            mContent = www.text;
        }
    }
    #endregion


    #region Get 获取系统信息，游戏版本等
    ///<summary>
    ///get 系统信息，游戏版本等
    ///</summary>
    private void sysInfo(string token)
    {
        StartCoroutine(GetSysInfo(token));
    }
    private IEnumerator GetSysInfo(string token)
    {
        private string mContent;
        WWW www = new WWW(postUrl,token);
        yield return www;
        if (www.error != null)
        {
            //Get请求失败
             
        }
        else
        {
            //Get请求成功
            mContent = www.text;
            //解析数据
        }
    }
    #endregion


    #region Get 拉取游戏公告
    private void notice(string token ,enum type ,int gid ,int rid)
    {
        StartCoroutine(GetNotice(token));
    }
    private IEnumerator GetNotice(string token)
    {
        WWW www = new WWW(postUrl);
        yield return www;
        if (www.error != null)
        {
            //Get请求失败
             
        }
        else
        {
            //Get请求成功
            
        }
    }
    #endregion

    #region Get 拉取游戏列表
    private void game(string token)
    {
        StartCoroutine(GetGame(token));
    }
    private IEnumerator GetGame(string token)
    {
        WWW www = new WWW(postUrl);
        yield return www;
        if (www.error != null)
        {
            //Get请求失败
             
        }
        else
        {
            //Get请求成功
            
        }
    }
    #endregion
}



    #region post注册请求 PostRegisterMessages(string playername,string password);
    ///<summary>
    ///post注册请求
    ///</summary>
    private void PostRegisterMessages(string playername,string password)
    {
        Dictionary<string,string> dict = new Dictionary<string,string>();
        dict.Add(Parameters[0],playername);
        dict.Add(Parameters[1],password);
        StartCoroutine(PostRegister(HostName+URLPath,dict));
    }
    private IEnumerator PostRegister(string url, Dictionary<string, string> post)
    {
        string error_message = "";
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string,string> post_arg in post)
        {
            form.AddField(post_arg.Key,post.value);
        }
        WWW www = new WWW(url,form);
        yield return www;
        if(www.error != null)
        {
            //注册成功
            Debug.Log("Register Success");
            Facade.Instance.ShowWindow<LoginWindow>();
        }
        else
        {
            //注册失败
            switch (www.error)
            {
                //注意不同屏幕下的Rect坐标
                case 404 : 
                    error_message = "用户名已经存在，请重新输入";
                    GUI.Label(new Rect(0,0,0,0),error_message);
                    break;
                case 404 : 
                    error_message = "密码不可以跟用户名一样";
                    GUI.Label(new Rect(0,0,0,0),error_message);
                    break;
            }
            
        }
    }
    #endregion

    