/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:        unity缓冲池
***************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MonoBufferPool<T> 
    where T: MonoBehaviour
{
    /// <summary>
    ///   父亲
    /// </summary>
    public GameObject Parent;

    /// <summary>
    ///   工作状态
    /// </summary>
    private List<T> works_;

    /// <summary>
    ///   休息状态
    /// </summary>
    private List<T> idles_;

    /// <summary>
    ///   最大数量
    /// </summary>
    private int max_num_;

    /// <summary>
    ///   
    /// </summary>
    public MonoBufferPool(GameObject parent,int max_num)
    {
        this.Parent = parent;
        this.max_num_ = max_num;
        this.idles_ = new List<T>();
        this.works_ = new List<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    ~MonoBufferPool()
    {
        Dispose(false);
    }

    /// <summary>
    ///   活跃
    /// </summary>
    public T Active()
    {
        if (idles_.Count <= 0)
             Collect();
 
        T temp = null;

        if (idles_.Count > 0)
        {
            temp = idles_[idles_.Count - 1];
            idles_.Remove(temp);
        }
        else
            temp = Create();

        if (temp != null)
            works_.Add(temp);
        if (temp != null || temp.gameObject != null)
            temp.gameObject.SetActive(true);

        return temp;
    }

    /// <summary>
    ///   释放
    /// </summary>
    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        works_.Remove(obj);
        idles_.Add(obj);
    }

    /// <summary>
    ///  休息
    /// </summary>
    public void Idle()
    {
        List<T> go = new List<T>();
        for (int i = 0; i < works_.Count; i++) 
        {
            go.Add(works_[i]);
        }
        for (int i = 0; i < go.Count; i++)
        {
            works_.Remove(go[i]);
            idles_.Add(go[i]);
            go[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    ///   回收
    /// </summary>
    public void Collect()
    {
        List<T> go = new List<T>();
        for (int i = 0; i < works_.Count; i++)
        {
            if (!works_[i].gameObject.activeSelf)
                go.Add(works_[i]);
        }

        for (int i = 0; i < go.Count; i++) 
        {
            works_.Remove(go[i]);
            idles_.Add(go[i]);
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < works_.Count; i++) 
        {
            Delete(works_[i]);
        }
        works_.Clear();
        for (int i = 0; i < idles_.Count; i++) 
        {
            Delete(idles_[i]);
        }
        idles_.Clear();
    }


    /// <summary>
    ///   创建
    /// </summary>
    private T Create()
    {
        if (works_.Count + idles_.Count >= max_num_)
            return null;

        GameObject go = new GameObject();
        if (Parent != null) 
        {
            Transform t = go.transform;
            t.parent = Parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = Parent.layer;
        }
        go.name = typeof(T).ToString();
        return go.AddComponent<T>();
    }
    
    /// <summary>
    ///   
    /// </summary>
    private void Delete(T obj)
    {
        if (obj != null) 
        {
            if (Application.isPlaying)
            {
                GameObject go = obj.gameObject;
                go.transform.parent = null;
                UnityEngine.Object.Destroy(obj.gameObject);
            }
            else UnityEngine.Object.DestroyImmediate(obj.gameObject);
        }
    }

    /// <summary>
    ///   
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Clear();
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}
