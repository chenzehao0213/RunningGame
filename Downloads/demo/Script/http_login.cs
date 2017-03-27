using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;

public class http_login : sysInfo {
	public UILabel IUsername;
	public UILabel IPassword;
	public string prom_user = "";		//用户名错误时的提示信息
	public string prom_psw = "";		//密码输入错误时的提示信息
	public static string userInfo_url = "119.23.145.192:3010/login";
	public static string sysInfo_url = "119.23.145.192:3010/sysInfo";

	///<summary>
	///定义用来存放用户信息的对象
	///protobuf未能成功转化，会报错
	///</summary>
	SysInfoModel sysInfo = new SysInfoModel();
	UserInfoModel userInfo = new UserInfoModel();
	
	void Awake()
	{
		GameObject button = GameObject.Find("UI Root/LoginWindow/Button_Entry");
		UIEventListener.Get(button).onClick = OnEntryClick;
	}

	void OnEntryClick(GameObject go)
	{
		login(IUsername.text,IPassword.text);
	}

	void onGUI()
	{
		//登录中错误信息的提示框
		//具体的坐标还未定为
		//在不同的屏幕中定位的坐标是否不同
		GUI.Label(new Rect(111,156,266,674),prom_user);
		GUI.Label(new Rect(0,0,0,0),prom_psw);
	}
	
	
	private void login(string Username,string Password)
	{
		StartCoroutine(PostMessages(Username,Password));
	}
	
	private IEnumerator PostMessages(string UserName,string PassWord)
	{
		Debug.Log(PassWord);
		WWWForm form = new WWWForm();
		form.AddField("username",UserName);
		form.AddField("password",PassWord);
		WWW www = new WWW(userInfo_url,form);
		yield return www;
		if(www.error != null)
		{
			//错误信息提示，暂时不知道错误信息的格式
			//超时连接，没有网络等情况
			Debug.Log("登录失败，请检查网络是否正常");
			return false;
		}
		
		//登录失败
		//从服务器返回的信息提取返回码
		string str = www.text;
		string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
		if(result != "200")
		{
			Debug.Log(www.text);
			switch(result)
			{
			case "500" : 
				Debug.Log("登录失败");
				break;
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
			Debug.Log("登录成功");
			///<summary>
			///将用户信息保存在 UserInfoModel userInfo中
			///使用simpleJson解析服务器返回的json
			///</summary>
			parseJson(www.text);
		}
	}


	#region 解析json,将数据存放在userInfo对象中
	private GameObject parseJson(string jsonData)
	{
		string json = JSON.Parse(jsonData);
		if(json["code"] == "200")
		{
			userInfo.id = json["data"]["user"]["id"];
			userInfo.uid = json["data"]["user"]["uid"];
			userInfo.gaccount = json["data"]["user"]["gaccount"];
			userInfo.score = json["data"]["user"]["score"];
			userInfo.score_free = json["data"]["user"]["score_free"];
			userInfo.score_bank = json["data"]["user"]["score_bank"];
			userInfo.is_on = json["data"]["user"]["is_on"];
			userInfo.token = json["data"]["token"];
			return userInfo;
		}
	}
	#endregion
	



}
