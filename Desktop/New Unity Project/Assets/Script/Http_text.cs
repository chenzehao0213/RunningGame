using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Http_text : MonoBehaviour {
	public string username = "zdcgh";
	public string password = "hello";
	public static string postUrl = "119.23.145.192:3010/login";

	private void login(string username,string password)
	{
		Dictionary<string,string> dict = new Dictionary<string,string>();
        dict.Add("1",username);
        dict.Add("100001",password);
        StartCoroutine(PostMessages(postUrl,dict));
	}

	 private IEnumerator PostMessages(string url, Dictionary<string, string> post)
	 {
		 WWWForm form = new WWWForm();
        foreach (KeyValuePair<string,string> post_arg in post)
        {
            form.AddField(post_arg.Key , post_arg.Value);
        }
        WWW www = new WWW(url,form);
        yield return www;
		if(www.error!=null)
		{
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log(www.text);
		}
	 }

	void OnClick()
	{
		Debug.Log("hello");
		login(username,password);
		Debug.Log("world");
	}
}
