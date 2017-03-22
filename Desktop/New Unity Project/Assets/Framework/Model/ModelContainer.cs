/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using System;
using System.Collections.Generic;

namespace ModelData
{
    public abstract class ModelContainerSingleton<Key, Element, Model, Type> : Singleton<Type>
                                                                              , IOperate<Model>
         where Type : Singleton<Type>, new()
         where Model : class, new()
    {
        /// <summary>
        ///   序化化器
        /// </summary>
        public Serializer<Model> Serializer;

        /// <summary>
        /// 数据集
        /// </summary>
        public Dictionary<Key, Element> Datas;

        /// <summary>
        ///   
        /// </summary>
        public ModelContainerSingleton()
        {
            Datas = new Dictionary<Key, Element>();
        }

        /// <summary>
        /// 增加
        /// </summary>
        public bool Add(Key key, Element value)
        {
            return ModelContainerOperate.Add(Datas, key, value);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Remove(Key key)
        {
            return ModelContainerOperate.Remove(Datas, key);
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Modify(Key key, Element value)
        {
            return ModelContainerOperate.Modify(Datas, key, value);
        }

        /// <summary>
        /// 查找函数
        /// </summary>
        public Element Find(Key key)
        {
            return ModelContainerOperate.Find(Datas, key);
        }

        /// <summary>
        /// 序列化到资源文件中（采用默认资源加载）
        /// </summary>
        public bool Serialize(string local_file)
        {
            return ModelContainerOperate.Serialize(Export, Serializer, local_file);
        }
        public bool Deserialize(string local_file)
        {
            Datas.Clear();
            return ModelContainerOperate.Deserialize(Import, Serializer, local_file);
        }


        /// <summary>
        /// 序列化到文件
        /// </summary>
        public bool SerializeToFile(string full_path)
        {
            return ModelContainerOperate.SerializeToFile(Export, Serializer, full_path);
        }
        public bool DeserializeFromFile(string full_path)
        {
            Datas.Clear();
            return ModelContainerOperate.DeserializeFromFile(Import, Serializer, full_path);
        }
        public bool DeserializeFromFile(string full_path, System.Type type)
        {
            Datas.Clear();
            return ModelContainerOperate.DeserializeFromFile(Import, Serializer, full_path, type);
        }


        /// <summary>
        /// 序列化至数据流中
        /// </summary>
        public bool SerializeToBytes(byte[] bytes)
        {
            return ModelContainerOperate.SerializeToBytes(Export, Serializer, bytes);
        }
        public bool DeserializeFromBytes(byte[] bytes)
        {
            Datas.Clear();
            return ModelContainerOperate.DeserializeFromBytes(Import, Serializer, bytes);
        }

        #region TODO

        /// <summary>
        /// 序列化到资源文件中（采用默认资源加载）
        /// </summary>
        public bool Serialize(Serializer<Model> serializer, string local_file)
        {
            return ModelContainerOperate.Serialize(Export, serializer, local_file);
        }

        /// <summary>
        /// 从文件中反序例化数据（采用默认资源加载）
        /// </summary>
        public bool Deserialize(Serializer<Model> serializer, string local_file)
        {
            Datas.Clear();
            return ModelContainerOperate.Deserialize(Import, serializer, local_file);
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        public bool SerializeToFile(Serializer<Model> serializer, string full_path)
        {
            return ModelContainerOperate.SerializeToFile(Export, serializer, full_path);
        }

        /// <summary>
        /// 从文件中反序例化数据
        /// </summary>
        public bool DeserializeFromFile(Serializer<Model> serializer, string full_path)
        {
            Datas.Clear();
            return ModelContainerOperate.DeserializeFromFile(Import, serializer, full_path);
        }

        /// <summary>
        /// 序列化至数据流中
        /// </summary>
        public bool SerializeToBytes(Serializer<Model> serializer, byte[] bytes)
        {
            return ModelContainerOperate.SerializeToBytes(Export, serializer, bytes);
        }

        /// <summary>
        /// 从数据流中反序例化数据
        /// </summary>
        public bool DeserializeFromBytes(Serializer<Model> serializer, byte[] bytes)
        {
            Datas.Clear();
            return ModelContainerOperate.DeserializeFromBytes(Import, serializer, bytes);
        }

        #endregion

        /// <summary>
        ///   导入数据
        /// </summary>
        protected abstract void Import(Model model);

        /// <summary>
        ///   导出数据
        /// </summary>
        protected abstract Model Export();
    }

    public static class ModelContainerOperate
    {
        /// <summary>
        /// 增加
        /// </summary>
        public static bool Add<Key, Element>(Dictionary<Key, Element> source, Key key, Element value)
        {
            if (source == null)
                return false;
            if (source.ContainsKey(key))
                return false;

            source.Add(key, value);

            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public static bool Remove<Key, Element>(Dictionary<Key, Element> source, Key key)
        {
            if (source == null)
                return false;

            source.Remove(key);

            return true;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public static bool Modify<Key, Element>(Dictionary<Key, Element> source, Key key, Element value)
        {
            if (source == null)
                return false;
            if (!source.ContainsKey(key))
                return false;

            source[key] = value;

            return true;
        }

        /// <summary>
        /// 查找函数
        /// </summary>
        public static Element Find<Key, Element>(Dictionary<Key, Element> source, Key key)
        {
            if (source == null)
                return default(Element);
            if (!source.ContainsKey(key))
                return default(Element);

            return source[key];
        }

        /// <summary>
        /// 序列化到资源文件中（采用默认资源加载
        /// </summary>
        public static bool Serialize<Model>(System.Func<Model> export
                                                   , Serializer<Model> serializer
                                                   , string local_file)
            where Model : class, new()
        {
            if (export == null)
                return false;
            if (serializer == null)
                return false;

            Model model = export();
            return serializer.Serialize(model, local_file);
        }

        /// <summary>
        /// 从文件中反序例化数据（采用默认资源加载）
        /// </summary>
        public static bool Deserialize<Model>(System.Action<Model> import
                                                , Serializer<Model> serializer
                                                , string local_file)
            where Model : class, new()
        {
            if (import == null)
                return false;
            if (serializer == null)
                return false;

            Model model = new Model();
            if (serializer.Deserialize(ref model, local_file))
            {
                import(model);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 序列化到文件
        /// </summary>
        public static bool SerializeToFile<Model>(System.Func<Model> export
                                                   , Serializer<Model> serializer
                                                   , string full_path)
            where Model : class, new()
        {
            if (export == null)
                return false;
            if (serializer == null)
                return false;

            Model model = export();
            return serializer.SerializeToFile(model, full_path);
        }

        public static bool DeserializeFromFile<Model>(System.Action<Model> import
                                                , Serializer<Model> serializer
                                                , string full_path)
            where Model : class, new()
        {
            if (import == null)
                return false;
            if (serializer == null)
                return false;

            Model model = new Model();
            if (serializer.DeserializeFromFile(ref model, full_path))
            {
                import(model);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从文件中反序例化数据
        /// </summary>
        public static bool DeserializeFromFile<Model>(System.Action<Model> import
                                                , Serializer<Model> serializer
                                                , string full_path
                                                , Type element)
            where Model : class, new()
        {
            if (import == null)
                return false;
            if (serializer == null)
                return false;

            Model model = new Model();
            if (serializer.DeserializeFromFile(ref model, element, full_path))
            {
                import(model);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 序列化至数据流中
        /// </summary>
        public static bool SerializeToBytes<Model>(System.Func<Model> export
                                                   , Serializer<Model> serializer
                                                   , byte[] bytes)
            where Model : class, new()
        {
            if (export == null)
                return false;
            if (serializer == null)
                return false;

            Model model = export();
            return serializer.SerializeToBytes(model, bytes);
        }

        /// <summary>
        /// 从数据流中反序例化数据
        /// </summary>
        public static bool DeserializeFromBytes<Model>(System.Action<Model> import
                                                , Serializer<Model> serializer
                                                , byte[] bytes)
            where Model : class, new()
        {
            if (import == null)
                return false;
            if (serializer == null)
                return false;

            Model model = new Model();
            if (serializer.DeserializeFromBytes(ref model, bytes))
            {
                import(model);
                return true;
            }
            return false;
        }
    }

}


