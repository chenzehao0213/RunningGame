using UnityEngine;
using System.Collections;

public class UIControl
{
    /// <summary>
    /// 无效的控件类
    /// </summary>
    public static UIControl Invalid = new UIControl();

    /// <summary>
    /// 绑定的对象
    /// </summary>
    private GameObject game_object_;

    /// <summary>
    /// 
    /// </summary>
    private UIControl()
    {
        game_object_ = null;
    }

    /// <summary>
    /// 
    /// </summary>
    public UIControl(GameObject obj)
    {
        game_object_ = obj;
    }

    /// <summary>
    /// 父对象
    /// </summary>
    public UIControl Parent
    {
        get
        {
            if (game_object_ != null && game_object_.transform != null && game_object_.transform.parent != null)
            {
                return new UIControl(game_object_.transform.parent.gameObject);
            }

            return null;
        }
    }

    /// <summary>
    /// 获得子对象
    /// </summary>
    public UIControl this[string name]
    {
        get
        {
            try
            {
                if (game_object_ != null)
                    return new UIControl(game_object_.transform.FindChild(name).gameObject);
            }
            catch (System.Exception)
            {
                Debug.Log("Cann't find GuiControl, Name is " + name);
            }

#if UNITY_EDITOR
            return null;
#else
            return Invalid;
#endif
        }
    }

    #region NGUI
    /// <summary>
    ///   获得UI组件
    /// </summary>
    private static T GetUIComponent<T>(UIControl c) where T : MonoBehaviour
    {
        return (c != null && c.game_object_ != null) ? c.game_object_.GetComponent<T>() : null;
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator GameObject(UIControl c)
    {
        return (c != null && c.game_object_ != null) ? c.game_object_ : null;
    }
    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIWidget(UIControl c)
    {
        return GetUIComponent<UIWidget>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIPanel(UIControl c)
    {
        return GetUIComponent<UIPanel>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UISprite(UIControl c)
    {
        return GetUIComponent<UISprite>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UI2DSprite(UIControl c)
    {
        return GetUIComponent<UI2DSprite>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UITexture(UIControl c)
    {
        return GetUIComponent<UITexture>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UILabel(UIControl c)
    {
        return GetUIComponent<UILabel>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIButton(UIControl c)
    {
        return GetUIComponent<UIButton>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIGrid(UIControl c)
    {
        return GetUIComponent<UIGrid>(c);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UISlider(UIControl c)
    {
        return GetUIComponent<UISlider>(c);
    }
    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIToggle(UIControl c)
    {
        return GetUIComponent<UIToggle>(c);
    }
    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIInput(UIControl c)
    {
        return GetUIComponent<UIInput>(c);
    }
    #endregion
}