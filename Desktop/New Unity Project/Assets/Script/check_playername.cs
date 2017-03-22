using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class check_playername : MonoBehaviour 
{
	public UIInput playernameLabel;
	public UIInput passwordLabel;
	public UIInput comfirmPasswordLabel;
	public UIInput real_nameLabel;
	public UIInput verification_codeLabel;

	public UILabel error_playername;
	public UILabel error_password;
	public UILabel error_comfirmpassword;
	public UILabel error_realname;
	public UILabel error_code;
	void OnClick()
	{
		//用户名
		Verification("^[0-9A-Za-z]+$",playernameLabel.value,"账号格式错误",error_playername);

		//密码  \w报错。
		// Verification("^[a-zA-Z]\w{5,17}+$",passwordLabel.value,"密码格式错误",error_password);
		
		//真实姓名
		Verification("^[\u4e00-\u9fa5]+$",real_nameLabel.value,"姓名格式错误",error_realname);
		//密码是否一致
		ComfirmPassword(passwordLabel.value,comfirmPasswordLabel.value,error_comfirmpassword);

	}

	///<summary>
	///验证输入信息是否规范，参数依次是：正则表达，输入框内容，报错内容，报错内容提示地方
	///</summary>
	void Verification(string pattern,string objectLabel,string error,UILabel errorr)
	{
		Regex rgx = new Regex(@pattern);
		if(!rgx.IsMatch(objectLabel))
		{
			errorr.text = error;
		}
		else
		{
			errorr.text = "";
		}
	}

	///<summary>
	///验证两次密码是否一致
	///</summary>
	void ComfirmPassword(string res_password,string comf_password,UILabel errorr)
	{	
		if(comf_password == "")
		{
			errorr.text = "请输入密码";
		}
		else if(res_password != comf_password)
			 {
				errorr.text = "密码不一致";
			 }
			 else
			 {
		 	 	errorr.text = "";
			 }
	} 

	

}
