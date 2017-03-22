using UnityEngine;
using System.Collections;

public class UIWindow : MonoBehaviour
{
    /// <summary>
    ///   
    /// </summary>
    public Vector3 Position
    {
        get
        {
            if (HidePlan == UICommon.emWindowHidePlan.OutSide)
            {
                if (Visible)
                {
                    return new Vector3(transform.localPosition.x
                    , transform.localPosition.y
                    , transform.localPosition.z);
                }
                else
                {
                    return new Vector3(transform.localPosition.x + UICommon.Constant.WINDOW_OUTSIDE_OFFSET
                    , transform.localPosition.y
                    , transform.localPosition.z);
                }
            }

            return transform.localPosition;
        }
        set
        {
            if (HidePlan == UICommon.emWindowHidePlan.OutSide)
            {
                if (Visible)
                {
                    transform.localPosition = value;
                }
                else
                {
                    transform.localPosition = new Vector3(value.x + UICommon.Constant.WINDOW_OUTSIDE_OFFSET
                                                        , value.y
                                                        , value.z);
                }
            }
            else
            {
                transform.localPosition = value;
            }
        }
    }

    /// <summary>
    /// 显示
    /// </summary>
    public bool Visible { get; private set; }

    /// <summary>
    ///   
    /// </summary>
    public virtual UICommon.emWindowType WindowType
    {
        get { return UICommon.emWindowType.Normal; }
    }

    /// <summary>
    ///   
    /// </summary>
    public virtual UICommon.emWindowHidePlan HidePlan
    {
        get { return UICommon.emWindowHidePlan.Delete; }

    }

    /// <summary>
    /// 显示
    /// </summary>
    public void Show()
    {
        if (Visible)
            return;

        Visible = true;

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (HidePlan == UICommon.emWindowHidePlan.OutSide)
        {
            transform.localPosition = new Vector2(transform.localPosition.x - UICommon.Constant.WINDOW_OUTSIDE_OFFSET,
                                                  transform.localPosition.y);
        }

        BringForward();

        OnShow();
    }

    public void Hide()
    {
        if (!Visible)
            return;

        Visible = false;

        if (HidePlan == UICommon.emWindowHidePlan.Hide)
        {
            gameObject.SetActive(false);
        }
        else if (HidePlan == UICommon.emWindowHidePlan.Delete)
        {
            Facade.Instance.DeleteWindow(this, false);
        }
        else if (HidePlan == UICommon.emWindowHidePlan.OutSide)
        {
            transform.localPosition = new Vector2(transform.localPosition.x + UICommon.Constant.WINDOW_OUTSIDE_OFFSET,
                                                  transform.localPosition.y);
        }

        OnHide();
    }

    public void BringForward()
    {
        Facade.Instance.BringForward(this);
    }

    public void PushBack()
    {
        Facade.Instance.PushBack(this);
    }

    /// <summary>
    /// 显示
    /// </summary>
    protected virtual void OnShow() { }

    /// <summary>
    /// 隐藏
    /// </summary>
    protected virtual void OnHide() { }

    /// <summary>
    /// 获得子控件
    /// </summary>
    public UIControl this[string name]
    {
        get
        {
            try
            {
                Transform child = transform.FindChild(name);
                if (child != null)
                    return new UIControl(child.gameObject);
            }
            catch (System.Exception)
            {
                Debug.Log("Cann't find GuiControl, Name is " + name);
            }

#if UNITY_EDITOR
            return null;
#else
            return CGUIControl.Invalid;
#endif
        }
    }

    #region  NGUI

    /// <summary>
    /// Depth
    /// </summary>
    public int Depth
    {
        get
        {
            UIPanel p = (UIPanel)this;
            if (p != null)
                return p.depth;
            return 0;
        }
        set
        {
            UIPanel p = (UIPanel)this;
            if (p != null)
                p.depth = value;
        }
    }

    /// <summary>
    ///   UIRoot
    /// </summary>
    public UIRoot GetUIRoot()
    {
        return NGUITools.FindInParents<UIRoot>(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIWidget(UIWindow w)
    {
        return _GetComponent<UIWidget>(w);
    }

    /// <summary>
    /// 
    /// </summary>
    public static explicit operator UIPanel(UIWindow w)
    {
        return _GetComponent<UIPanel>(w);
    }

    /// <summary>
    /// 获得指定Component
    /// </summary>
    private static T _GetComponent<T>(UIWindow w) where T : Component
    {
        return w != null ? w.GetComponent<T>() : null;
    }
    #endregion

}
