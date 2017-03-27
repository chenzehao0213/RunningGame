using UnityEngine;
using System.Collections;

///<summary>
///限制公告的字数 字数 = 长度／滚动速度
///</summary>

public class http_notice : MonoBehaviour {
	public UILabel billlabel;									//滚动栏的文字
	public string notice_url = "119.23.145.192:3010/notice";	//请求链接
	List<string> noticeStack_title = new List<string>();		//公告标题集合	
	List<string> noticeStack_msg = new List<string>();			//公告信息集合
	public const int SPEED;										//固定的滚动速度
	public uint boundary;										//滚动字幕的边界
	public string token;
	
	///Get函数的可选参数
	public enum type{type = lobby,};
	public uint gid;
	public uint rid;

	///<summary>
	///notice对象用来存放信息
	///</summary>
	NoticeModel notice = new NoticeModel();

	void Update () 
	{
		getNotice(token);
		while (noticeStack_msg[0] == null || noticeStack_title[0]==null){}
		//滚动信息
		rollNotice(noticeStack_title,noticeStack_msg);
		//删除信息
		removeNotice(noticeStack_title,noticeStack_msg);
	}

	///<summary>
	///获取公告信息
	///</summary>
	private void getNotice(string token)
	{
		StartCoroutine(Get(token));
	}

	private IEnumerator Get(string token , enum type=lobby , uint gid=null , uint rid=null)
	{
		//需要是%编码 
		string getUrl = notice_url + "?" +"token="	+ tokne + "&type=" + type + "&gid=" + gid + "&rid=" + rid;
		WWW www = new WWW(getUrl);
		yield return www;
		if (www.error != null)
		{
			//错误信息处理
			Debug.Log("公告栏获取失败");
			return false;
		}

		//从服务器返回的信息提取返回码
		string str = www.text;
		string result = System.Text.RegularExpressions.Regex.Replace(str, @"[^0-9]+", "");
		if (result != "200")
		{
			switch (result)
			{
				case "500":
					Debug.Log("FAIL");
				case "2001":
					Debug.Log("FA_NO_CONNECTOR_SERVER");
				case "3001":
					Debug.Log("FA_ENTER_ERROR");
				case "3002":
					Debug.Log("FA_CONNECTED");
			}
			return false;
		}
		else
		{
			//解析数据
			ParseJson(www.text);
			//将获取到的信息保存在集合中，供滚动信息调用
			addNotice(notice.title,notice.msg);
		}
	}


	#region 解析json,添加信息，展示信息，移除信息

	///<summary>
	///解析json并把数据存放在notice对象中
	///</summary>
	private GameObject ParseJson(string jsonData)
	{
		if(notice["code"] == 200)
		{
			string json = Json.Parse(jsonData);
			notice.id = json["data"]["notice"]["id"];
			notice.title = json["data"]["notice"]["title"];
			notice.msg = json["data"]["notice"]["msg"];
			notice.type = json["data"]["notice"]["type"];
			notice.gid = json["data"]["notice"]["gid"];
			notice.rid = json["data"]["notice"]["rid"];
			notice.is_show = json["data"]["notice"]["is_show"];
			return notice;
		}
		else
		{
			Debug.Log("解析json错误");
			return false;
		}
	}

	///<summary>
	///将获取到的消息保存起来
	///</summary>
	public void addNotice(string title,string message)
	{
		if(title!="" && message!="")
		{
			noticeStack_title.Add(title);
			noticeStack_msg.Add(message);
		}
	}

	///<summary>
	///滚动信息的函数
	///</summary>
	public void rollNotice(List<string> title,List<string> msg)
	{
		billlabel.text = title[0] + ": " + msg[0];
		//这里的lefttop并不是正确的写法
		if(billlabel.text.leftpos>boundary)
		{
			billlabel.text.leftpos -= Time.deltaTime * SPEED;
		}

	}

	///<summary>
	///移除已经显示过的信息
	///</summary>
	public void removeNotice(List<string> title,List<string> msg)
	{
		title.remove(0);
		msg.remove(0;)
	}

	#endregion

}

