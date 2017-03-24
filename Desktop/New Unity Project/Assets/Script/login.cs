using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//使用newtonsoft解析json
// using Newtonsoft.Json; 

public class Login : MonoBehaviour {
	public string username = "zdcgh";
	public string password = "hello";
	//登录失败的提示信息
	public string prom_msg = "";
	public static string postUrl = "119.23.145.192:3010/login";
	///<summary>
	///定义用来存放用户信息的对象
	///</summary>
	// SysInfoModel sysInfo = new SysInfoModel();
	// UserInfoModel userInfo = new UserInfoModel();


	void onClick()
	{
		login(username,password);
	}
	void onGUI()
	{
		//登录场景中错误的提示处
		GUI.Label(new Rect(0,0,0,0),prom_msg);
	}


	private void login(string Username,string Password)
	{
        StartCoroutine(PostMessages(Username,Password));
	}

	 private IEnumerator PostMessages(string UserName,string PassWord)
	 {
		WWWForm form = new WWWForm();
        form.AddField("username" , UserName);
		form.AddField("password",PassWord);
        WWW www = new WWW(postUrl,form);
        yield return www;
		if(www.error != null)
		{
			//错误信息提示，暂时不知道错误信息的格式
			//超时连接，没有网络等情况
			return false;
		}

		//登录失败
		//从服务器返回的信息提取返回码
		string str = www.text;
		string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
		if(result != "200")
		{
			switch(result)
            {
                case "501" : 
                    Debug.Log("参数错误");
                    break;
				case "-1" :
					prom_msg = "登录失败，请重新登录";
					break;
                case "-2" :
                    prom_msg = "用户名未找到，请检查用户名是否正确";
                    break;
                case "-3" :
                    prom_msg = "密码输入错误，请重新输入";
                    break;
                case "-4" :
                    prom_msg = "用户被锁";
                    break;
                case "-5" : 
                    prom_msg = "用户已经在在线";
                    break;
            }
            return false;
		}
		//登录成功
		else
		{
			///<summary>
			///将用户信息保存在 UserInfoModel userInfo中
			///使用newtonsoft解析服务器返回的json
			///</summary>
			// List<UserInfoModel> _userInfo = JsonConvert.DeserializeObject(List<UserInfoModel>)(www.text);
			// userInfo.ID = _userInfo.ID;
			// userInfo.UID = _userInfo.UID;
			// userInfo.Gaccount = _userInfo.Gaccount;
			// userInfo.Score = _userInfo.Score;
			// userInfo.ScoreFree = _userInfo.ScoreFree;
			// userInfo.ScoreBank = _userInfo.ScoreBank;
			// userInfo.IsOn = _userInfo.IsOn;
			// userInfo.Token = _userInfo.Token;
			//加载游戏大厅
			// Application.LoadLevel("HallWindow");	


			//检测版本
			// VersionTest(userInfo.Token);
		}
	 }

	 #region 获取系统信息，游戏版本等 GET
//		private void VersionTest(string token)
//		{
//      	 	 StartCoroutine(SysInfo(token));
//		}
//		private IEnumerator SysInfo(string token)
//		{
//			WWW www = new WWW(postUrl);
//			yield return www;
//			if (www.error != null)
//			{
//				//Get请求失败
//			}
//			else
//			{
//				//Get请求成功
//				
//				//解析数据
//			}
//		}
	 #endregion
}
