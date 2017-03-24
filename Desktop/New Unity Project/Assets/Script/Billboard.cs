using UnityEngine;
using System.Collections;

//
//限制公告的字数 字数 = 长度／滚动速度
//
public class GetNotice : MonoBehaviour {
	public UILabel billlabel;
	public string url = "119.23.145.192:3010/notice";
	List<string> noticeStack_title = new List<string>();		//公告标题集合	
	List<string> noticeStack_msg = new List<string>();		//公告信息集合
	public string token;
	
	///<summary>
	///存放可选参数
	///</summary>
	public enum type{};
	public uint gid;
	public uint rid;

	NoticeModel notice = new NoticeModel();

	void Start () 
	{
		getNotice(token);
	}

	void Update () 
	{
		getNotice(token);
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
		string getUrl = url + "?" +"token="	+ tokne + "&type=" + type + "&gid=" + gid + "&rid=" + rid;
		WWW www = new WWW(getUrl);
		yield return www;
		if (www.error != null)
		{
			//错误信息处理
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
			///<summary>
			///拉取公告栏的信息，保存到 NoticeModel notice;
			///</summary>
			List<NoticeModel> _notice =  JsonConvert.DeserializeObject(List<NoticeModel>)(www.text);
			notice.ID = _notice.ID;
			notice.Title = _notice.Title;
			notice.Msg = _notice.Msg;
			notice.Type = _notice.Type;
			notice.GID = _notice.GID;
			notice.RID = _notice.RID;
			notice.IsShow = _notice.IsShow;
			
			//将获取到的信息保存在集合中，供滚动信息调用
			addNotice(notice.Title,notice.Msg);
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
		billlabel.text = title[0] + msg[0];
	}
}
