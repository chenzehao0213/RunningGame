/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System;
using System.IO;

namespace ModelData
{
    public class ProtoSerializer<Model> : Serializer<Model>
        where Model : class
    {
        /// <summary>
        /// 序列化到资源文件中（采用资源管理器加载）
        /// </summary>
        public override bool Serialize(Model data, string local_file)
        {
            try
            {
                if (!string.IsNullOrEmpty(local_file))
                {
                    //判断是否有文件夹，没有则创建
                    string full_path = Common.GetGlobalPath(local_file);
                    string path = Path.GetDirectoryName(full_path);
                    if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    //写入数据
                    using (var file = System.IO.File.Create(full_path))
                    {
                        ProtoBuf.Serializer.Serialize(file, data);
                    }

                    Debug.Log("ProtoSerializer.SerializeToFile<" + typeof(Model).Name + ">(" + local_file + "), Succeeded！");
                    return true;
                }
                else
                {
                    Debug.LogWarning("ProtoSerializer.SerializeToFile<" + typeof(Model).Name + ">(" + local_file + "), Can't find file！");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                "ProtoSerializer.SerializeToFile<" + typeof(Model).Name + ">(" + local_file + "), catch exception!\n"
                + e.Message);
            }

            return false;
        }

        /// <summary>
        /// 从文件中反序例化数据（采用资源管理器加载）
        /// </summary>
        public override bool Deserialize(ref Model data, string local_file)
        {
            try
            {
                byte[] bytes = Common.LoadBytes(local_file);
                if (bytes != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                    {
                        data = ProtoBuf.Serializer.Deserialize<Model>(ms);
                    }

                    Debug.Log("ProtoSerializer.DeserializeFromFile<" + typeof(Model).Name + ">(" + local_file + "), Succeeded!");
                    return true;
                }
                else
                {
                    Debug.LogWarning("ProtoSerializer.DeserializeFromFile<" + typeof(Model).Name + ">(" + local_file + "), Can't find file！");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                "ProtoSerializer.DeserializeFromFile<" + typeof(Model).Name + ">(" + local_file + "), catch exception!\n"
              + e.Message);
            }

            return false;
        }

        /// <summary>
        /// Serialize
        /// </summary>
        public override bool SerializeToFile(Model data, string full_path)
        {
            try
            {
                if (!string.IsNullOrEmpty(full_path))
                {
                    //判断是否有文件夹，没有则创建
                    string path = Path.GetDirectoryName(full_path);
                    if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    //写入数据
                    using (var file = System.IO.File.Create(full_path))
                    {
                        ProtoBuf.Serializer.Serialize(file, data);
                    }

                    Debug.Log("ProtoSerializer.SerializeToFile<" + typeof(Model).Name + ">(" + full_path + "), Succeeded！");
                    return true;
                }
                else
                {
                    Debug.LogWarning("ProtoSerializer.SerializeToFile<" + typeof(Model).Name + ">(" + full_path + "), Can't find file！");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                "ProtoSerializer.SerializeToFile<" + typeof(Model).Name + ">(" + full_path + "), catch exception!\n"
                + e.Message);
            }

            return false;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        public override bool DeserializeFromFile(ref Model data, string full_path)
        {
            try
            {
                byte[] bytes = Common.LoadPathBytes(full_path);
                if (bytes != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                    {
                        data = ProtoBuf.Serializer.Deserialize<Model>(ms);
                    }

                    Debug.Log("ProtoSerializer.DeserializeFromFile<" + typeof(Model).Name + ">(" + full_path + "), Succeeded!");
                    return true;
                }
                else
                {
                    Debug.LogWarning("ProtoSerializer.DeserializeFromFile<" + typeof(Model).Name + ">(" + full_path + "), Can't find file！");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                "ProtoSerializer.DeserializeFromFile<" + typeof(Model).Name + ">(" + full_path + "), catch exception!\n"
              + e.Message);
            }

            return false;
        }

        /// <summary>
        /// Serialize
        /// </summary>
        public override bool SerializeToBytes(Model data, byte[] bytes)
        {
            try
            {
                if (bytes != null && bytes.Length > 0)
                {
                    //写入数据
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                    {
                        ProtoBuf.Serializer.Serialize(ms, data);
                    }

                    Debug.Log("ProtoSerializer.SerializeToBytes<" + typeof(Model).Name + ">(), Succeeded！");
                    return true;
                }
                else
                {
                    Debug.LogWarning("ProtoSerializer.SerializeToBytes<" + typeof(Model).Name + ">(), Falid!");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                "ProtoSerializer.SerializeToBytes<" + typeof(Model).Name + ">(), catch exception!\n"
                + e.Message);
            }

            return false;
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        public override bool DeserializeFromBytes(ref Model data, byte[] bytes)
        {
            try
            {
                if (bytes != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
                    {
                        data = ProtoBuf.Serializer.Deserialize<Model>(ms);
                    }

                    Debug.Log("ProtoSerializer.DeserializeFromBytes<" + typeof(Model).Name + ">(), Succeeded！");
                    return true;
                }
                else
                {
                    Debug.LogWarning("ProtoSerializer.DeserializeFromBytes<" + typeof(Model).Name + ">(), Falid!");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                "ProtoSerializer.DeserializeFromBytes<" + typeof(Model).Name + ">(), catch exception!\n"
                + e.Message);
            }

            return false;
        }

        public override bool DeserializeFromFile(ref Model data, Type element, string full_path)
        {
            throw new NotImplementedException();
        }
    }
}