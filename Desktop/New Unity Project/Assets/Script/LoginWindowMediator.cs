using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoginWindowMediator : Mediator
{
		private LoginWindow Button
		{
			get{return (LoginWindow)base.Element;}
		}

		public LoginWindowMediator(LoginWindow Button)
			:base(Button)
		{
			Facade.Instance.ShowWindow<LoginWindow>();
		}
	
		public override void HandleNotification(IMessage message)
		{
			switch (message.Name)
			{
				case MessageConst.REGISTER : 
					Button.Register();
					break;
				case MessageConst.ENTRY : 
					Button.Entry();
					break;
                
			}
		}
	
}