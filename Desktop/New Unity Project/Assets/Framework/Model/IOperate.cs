/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System.Collections;

namespace ModelData
{
    public interface IOperate<Model>
          where Model : class
    {
        /// <summary>
        /// 序列化到资源文件中（采用默认资源加载）
        /// </summary>
        bool Serialize(string local_file);

        /// <summary>
        /// 从文件中反序例化数据（采用默认资源加载）
        /// </summary>
        bool Deserialize(string local_file);

        /// <summary>
        /// 序列化到文件
        /// </summary>
        bool SerializeToFile(string full_path);

        /// <summary>
        /// 从文件中反序例化数据
        /// </summary>
        bool DeserializeFromFile(string full_path);

        /// <summary>
        /// 序列化至数据流中
        /// </summary>
        bool SerializeToBytes(byte[] bytes);

        /// <summary>
        /// 从数据流中反序例化数据
        /// </summary>
        bool DeserializeFromBytes(byte[] bytes);

        /// <summary>
        /// 序列化到资源文件中（采用默认资源加载）
        /// </summary>
        bool Serialize(Serializer<Model> serializer, string local_file);

        /// <summary>
        /// 从文件中反序例化数据（采用默认资源加载）
        /// </summary>
        bool Deserialize(Serializer<Model> serializer, string local_file);

        /// <summary>
        /// 序列化到文件
        /// </summary>
        bool SerializeToFile(Serializer<Model> serializer, string full_path);

        /// <summary>
        /// 从文件中反序例化数据
        /// </summary>
        bool DeserializeFromFile(Serializer<Model> serializer, string full_path);

        /// <summary>
        /// 序列化至数据流中
        /// </summary>
        bool SerializeToBytes(Serializer<Model> serializer, byte[] bytes);

        /// <summary>
        /// 从数据流中反序例化数据
        /// </summary>
        bool DeserializeFromBytes(Serializer<Model> serializer, byte[] bytes);

    }

}

