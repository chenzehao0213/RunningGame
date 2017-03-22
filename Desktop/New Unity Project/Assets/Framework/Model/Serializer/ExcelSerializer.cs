/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:         Excel 序列化
***************************************************************/
using System.Collections.Generic;
using System;

namespace ModelData
{
    public class ExcelSerializer<Model> : Serializer<Model>
            where Model : class
    {
        /// <summary>
        /// 序列化到资源文件中（采用资源管理器加载）
        /// </summary>
        public override bool Serialize(Model data, string local_file)
        {
            return false;
        }
        public override bool Deserialize(ref Model data, string local_file)
        {
            return false;
        }

        /// <summary>
        ///    序列化到文件
        ///    从txt中读取文件
        /// </summary>
        public override bool SerializeToFile(Model data, string full_path)
        {
            return false;
        }
        public override bool DeserializeFromFile(ref Model data, string full_path)
        {
            return DeserializeFromFile(ref data, null, full_path);
        }

        public override bool DeserializeFromFile(ref Model data, Type element, string full_path)
        {
            Dictionary<string, List<string>> content;
            string strLine = Common.LoadPathText(full_path, System.Text.Encoding.UTF8);
            ExcelData.ExplainString(full_path, strLine, out content);
            return ExcelData.ImportData<Model>(ref data, content, element);
        }

        /// <summary>
        /// 序列化至数据流中
        /// </summary>
        public override bool SerializeToBytes(Model data, byte[] bytes)
        {
            return false;
        }

        public override bool DeserializeFromBytes(ref Model data, byte[] bytes)
        {
            return false;
        }


    }
}

