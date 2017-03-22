using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallWindow : UIWindow
{
    private PlayerInfModel info_;   //用户信息
	private UILabel ID;
	private UILabel username;
    private UILabel money;
    private UIGrid grid_;
	

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
            return UICommon.emWindowHidePlan.Hide;
        }
    }

 	void Awake()
	{
        ///<summary>
        ///为按钮增加监听事件
        ///</summary>
		UIEventListen.Get((GameObject)this["ActivityCenter"]).onclick += ActivityCenter;
		UIEventListen.Get((GameObject)this["BankCenter"]).onclick += BankCenter;
        UIEventListen.Get((GameObject)this["AvatarFrame"]).onclick += AvatarFrame;
        UIEventListen.Get((GameObject)this["Service"]).onclick += Service;
        UIEventListen.Get((GameObject)this["Chart"]).onclick += Chart;
        UIEventListen.Get((GameObject)this["Scroll_GameList/Game_List/Game_Type1"]).onclick += Game_Type1Window;


        // grid_ = (UIGrid)this["Scroll View"];
        ID = (UILabel)this["AvatarFrame/User_ID"];
        username = (UILabel)this["AvatarFrame/Username"];
        money = (UILabel)this["Chart/Money"];
    }

    public void ShowPlayerInfo(PlayerInfModel Info_)
    {
        //
        this.Info = Info_;
        username.text = HandleMoney(Info.money);
        ID.text = "ID"+Info.ID;
        money.text = Info.money;
        
    }

    void UpdateInfo()
    {
        ShowPlayerInfo(PlayerInfModel Info_);
    }

    ///<summary>
    ///将数字转化为千分数
    ///</summary>
    public string HandleMoney(string money)
    {
        public string result;
        public int cs = 0;
        for(int i=0;i<money.length-1;i++)
        {
            cs++;
            result = money.charAt(i) + result;
            if (!(cs%3 && i!=0)
            {
                result = "," +result;
            }
        }
        return result;
    }

    #region 点击事件，展示功能窗口 Facade.Instance.ShowWindow<>();

	void ActivityCenter()
	{
        Facade.Instance.ShowWindow<ActivityWindow>();
	}
	
	void BankCenter()
	{
		Facade.Instance.ShowWindow<BankWindow>();
	}   

    void AvatarFrame()
    {
        Facade.Instance.ShowWindow<AvatarWindow>();
    }

    void Service()
    {
        Facade.Instance.ShowWindow<ServiceWindow>();
    }

    void Chart()
    {
        Facade.Instance.ShowWindow<ChartWindow>();
    }
    #endregion

    void Game_Type1Window()
    {
        Facade.Instance.ShowWindow<Game_Type1Window>();
    }
    

}

// //希望的返回
// message PlayerMessages
// {
//     string playername;    
//     uint player_ID;
//     uint money;     //只有一种游戏币
// }

