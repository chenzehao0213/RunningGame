/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:         序列化器
***************************************************************/
using UnityEngine;
using System.Collections;
using System;

public abstract class Serializer<Model>
    where Model : class
{
    /// <summary>
    ///   序列化到资源文件中（采用资源管理器加载）
    /// </summary>
    public abstract bool Serialize(Model data, string local_file);

    /// <summary>
    ///   从文件中反序例化数据（采用资源管理器加载）
    /// </summary>
    public abstract bool Deserialize(ref Model data, string local_file);

    /// <summary>
    ///   序列化到文件
    /// </summary>
    public abstract bool SerializeToFile(Model data, string full_path);

    /// <summary>
    /// 从文件中反序例化数据
    /// </summary>
    public abstract bool DeserializeFromFile(ref Model data, string full_path);

    public abstract bool DeserializeFromFile(ref Model data, Type element, string full_path);

    /// <summary>
    /// 序列化至数据流中
    /// </summary>
    public abstract bool SerializeToBytes(Model data, byte[] bytes);

    /// <summary>
    /// 从数据流中反序例化数据
    /// </summary>
    public abstract bool DeserializeFromBytes(ref Model data, byte[] bytes);
}
