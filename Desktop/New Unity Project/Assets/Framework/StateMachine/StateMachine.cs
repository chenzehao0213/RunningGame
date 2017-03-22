/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2015/12/22 14:56:15
* Note:        状态机
***************************************************************/
using UnityEngine;
using System.Collections;
using System;

public class StateMachine<OwnerType>
{
    /// <summary>
    /// 
    /// </summary>
    private State current_;

    /// <summary>
    /// 当前ID  
    /// </summary>
    private int current_id_;

    /// <summary>
    /// 是否在修改
    /// </summary>
    private bool is_in_update_;

    /// <summary>
    /// 下一个参数
    /// </summary>
    private object[] next_params_;

    /// <summary>
    /// 下一个id
    /// </summary>
    private int next_id_;

    /// <summary>
    /// 拥有者
    /// </summary>
    private OwnerType owner_;

    /// <summary>
    /// 状态types
    /// </summary>
    private System.Type[] state_types_;

    /// <summary>
    /// 
    /// </summary>
    static StateMachine()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public StateMachine(OwnerType owner, int numStates)
    {
        this.current_id_ = -1;
        this.next_id_ = -1;
        this.owner_ = owner;
        this.state_types_ = new System.Type[numStates];
    }

    /// <summary>
    /// 通知
    /// </summary>
    public void Notify(int messageID, params object[] messageParams)
    {
        this.current_.Notify(messageID, messageParams);
    }

    /// <summary>
    /// 注册状态
    /// </summary>
    public void RegisterState<StateType>(int ID) where StateType : State
    {
        this.state_types_[ID] = typeof(StateType);
    }

    /// <summary>
    /// 注册状态
    /// </summary>
    public void RegisterState(int ID, System.Type type)
    {
        if (!type.IsSubclassOf(typeof(State)))
        {
        }
        this.state_types_[ID] = type;
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    public void SetState(int newStateID)
    {
        this.SetState(newStateID, null);
    }

    /// <summary>
    /// 设置状态
    /// </summary>
    public void SetState(int newStateID, params object[] paramList)
    {
        if (this.is_in_update_)
        {
            this.next_id_ = newStateID;
            this.next_params_ = paramList;
        }
        else
        {
            int currentID = this.current_id_;
            this.SetStateInternal(newStateID);
            if (paramList != null)
            {
                this.current_.Begin(currentID, paramList);
            }
            else
            {
                this.current_.Begin(currentID);
            }
        }
    }

    /// <summary>
    /// 设置内部状态
    /// </summary>
    private void SetStateInternal(int newStateID)
    {
        //在切换状态的时候如果目前 状态不为空则先结束 当前状态
        if (this.current_ != null)
        {
            this.current_.End(newStateID);
        }
        if (newStateID >= 0)
        {
            this.current_ = (State)Activator.CreateInstance(this.state_types_[newStateID]);
            this.current_.StateMachine = (StateMachine<OwnerType>)this;
        }
        else
        {
            this.current_ = null;
        }
        this.current_id_ = newStateID;
    }

    /// <summary>
    /// 停止状态
    /// </summary>
    public void Stop()
    {
        this.SetStateInternal(-1);
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        this.is_in_update_ = true;
        this.current_.Update();
        this.is_in_update_ = false;
        if (this.next_id_ != -1)
        {
            int currentID = this.current_id_;
            this.SetStateInternal(this.next_id_);
            if (this.next_params_ != null)
            {
                this.current_.Begin(currentID, this.next_params_);
                this.next_params_ = null;
            }
            else
            {
                this.current_.Begin(currentID);
            }
            this.next_id_ = -1;
        }
    }

    /// <summary>
    /// 当前状态
    /// </summary>
    public State CurrentState
    {
        get
        {
            return this.current_;
        }
    }

    /// <summary>
    /// 当前ID
    /// </summary>
    public int CurrentStateID
    {
        get
        {
            return this.current_id_;
        }
    }

    /// <summary>
    /// 拥有者
    /// </summary>
    public OwnerType Owner
    {
        get
        {
            return this.owner_;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class State
    {
        /// <summary>
        /// 开始
        /// </summary>
        public virtual void Begin(int previous)
        {
        }

        /// <summary>
        /// 开始
        /// </summary>
        public virtual void Begin(int previous, params object[] list)
        {
        }

        /// <summary>
        /// 结束
        /// </summary>
        public virtual void End(int next)
        {
        }

        /// <summary>
        /// 通知
        /// </summary>
        public virtual void Notify(int messageID, params object[] messageParams)
        {
        }

        /// <summary>
        /// 状态写入
        /// </summary>
        public void SetState(int newStateID)
        {
            this.StateMachine.SetState(newStateID);
        }

        /// <summary>
        /// 状态写入
        /// </summary>
        public void SetState(int newStateID, params object[] paramList)
        {
            this.StateMachine.SetState(newStateID, paramList);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get
            {
                return this.StateMachine.current_id_;
            }
        }

        /// <summary>
        /// 状态持有者
        /// </summary>
        public OwnerType Owner
        {
            get
            {
                return this.StateMachine.owner_;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public StateMachine<OwnerType> StateMachine { get; internal set; }
    }
}
