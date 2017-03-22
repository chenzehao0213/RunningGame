/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:        
***************************************************************/
using UnityEngine;
using System.IO;
using System;
using System.Reflection;

namespace ModelData
{
    public class Common
    {
        /// <summary>
        ///  数据文件全局路径
        /// </summary>
        public static readonly string DATA_FILE_PATH = Application.dataPath + "/Resources/";

        /// <summary>
        /// Dat文件所在的局部文件夹名称
        /// </summary>
        public const string DAT_FOLDER_NAME = "Data/";

        /// <summary>
        ///   excel 局部路径
        /// </summary>
        public const string EXCEL_FILE_PATH = "Excel/";

        /// <summary>
        /// Dat文件后缀名
        /// </summary>
        public const string DAT_EXTENSION_NAME = ".bytes";

        /// <summary>
        /// Dat文件后缀名
        /// </summary>
        public const string EXCEL_EXTENSION_NAME = ".txt";

        /// <summary>
        /// 从数据资源全路径中读取二进制流
        /// </summary>
        public static string LoadPathText(string path, System.Text.Encoding encoding)
        {
            string result = null;
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path, encoding);
                try
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                finally
                {
                    sr.Close();
                }
            }
            return result;
        }

        /// <summary>
        ///   获得全局目录
        /// </summary>
        public static string GetGlobalPath(string local_file)
        {
            return Application.dataPath + "/Resources/" + local_file;
        }

        /// <summary>
        ///   读取字节数据
        /// </summary>
        public static byte[] LoadBytes(string address)
        {
            Debug.LogError("TODO,LoadBytes,采用资源加载！");

            return null;
        }

        /// <summary>
        /// 从数据资源全路径中读取二进制流
        /// </summary>
        public static byte[] LoadPathBytes(string path)
        {
            byte[] result = null;
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                result = new byte[fs.Length];
                fs.Read(result, 0, (int)fs.Length);
                fs.Close();
                return result;
            }

            return null;
        }

        /// <summary>
        ///   反射方法回调
        /// </summary>
        public static object Invoke<T>(T[] param, string methodName, System.Type type)
        {
            object obj = Activator.CreateInstance(type);

            MethodInfo method = type.GetMethod(methodName);
            for (int i = 0; i < param.Length; i++)
            {
                method.Invoke(obj, new object[] { param[i] });
            }
            return obj;
        }

        /// <summary>
        ///   回调
        /// </summary>
        public static void InvokeCallback(ref System.Action action)
        {
            if (action != null) 
            {
                System.Action temp = action;
                action = null;
                temp();
            }
        }

        #region 设置与测试二进制位值的相关辅助函数
        private const uint UINT_FLAG = 1;
        public static uint Bit32(byte bit)
        {

            return (uint)UINT_FLAG << bit;
        }
        public static uint Bit32All()
        {
            return uint.MaxValue;
        }
        public static void SetBit32(ref uint mask, byte bit, bool set = true)
        {
            if (set)
                mask |= Bit32(bit);
            else
                mask &= ~Bit32(bit);
        }

        public static bool TestBit32(uint mask, byte bit)
        {
            return (mask & Bit32(bit)) > 0;
        }

        private const uint ULONG_FLAG = 1;
        public static uint Bit64(byte bit)
        {
            return (uint)ULONG_FLAG << bit;
        }

        public static ulong Bit64All()
        {
            return ulong.MaxValue;
        }

        public static void SetBit64(ref ulong mask, byte bit, bool set = true)
        {
            if (set)
                mask |= Bit64(bit);
            else
                mask &= ~Bit64(bit);
        }

        public static bool TestBit64(ulong mask, byte bit)
        {
            return (mask & Bit64(bit)) > 0;
        }
        #endregion
    }

}

