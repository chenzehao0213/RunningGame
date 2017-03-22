using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LoginWindow : UIWindow
{
	private UILabel playername;
	private UILabel password;

	public override UICommon.emWindowType WindowType
    {
        get
        {
            return UICommon.emWindowType.Normal;
        }
    }

    public override UICommon.emWindowHidePlan HidePlan
    {
        get
        {
            return UICommon.emWindowHidePlan.Delete;
        }
    }	

	void Awake()
	{
		UIEventListen.Get((GameObject)this["Button_Entry"]).onclick += Entry;
		UIEventListen.Get((GameObject)this["Button_Register"]).onclick += Register;
		playername = (UILabel)this["Input_username/Value"];
		password = (UILabel)this["Input_password/Value"];
	}
	 
	void Entry()
	{
		if (ValidateAccount(username.text,password.text))		//先验证账号密码
		{
			Facade.Instance.ShowWindow<HallWindow>();
		}
		else
		{
			ValidateAccountFail(errormessage);
		}
	}

	void Register()
	{
		Facade.Instance.ShowWindow<RegisterWindow>();
		Facade.Instance.PushBack(LoginWindow);
	}
	
	///<summary>
	///发送账号密码到服务端请求验证
	///</summary>
	bool ValidateAccount(string username,string password)
	{
		//调用服务器验证账户接口；
		if(Http()){return true;}
		else {ValidateAccountFail();}
	} 
	
	///<summary>
	///密码错误后，页面作出响应
	///</summary>
	void ValidateAccountFail(string errormessage)
	{
		
	}

	void OnGUI()
	{
		GUI.Label(new)
	}


}