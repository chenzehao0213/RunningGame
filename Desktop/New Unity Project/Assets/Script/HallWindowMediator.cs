using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallWindowMediator : Mediator
{
		private HallWindow button_event
		{
			get{return (HallWindow)base.Element;}
		}

		public HallWindowMediator(HallWindow button_event)
			:base(button_event)
		{
            
		}
	
		public override void HandleNotification(IMessage message)
		{
			switch (message.Name)
			{
				case MessageConst.REGISTER : 
					
					break;
				case MessageConst.ENTRY : 
					
					break;
			}
		}

		
}