/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace ModelData
{
    public class ExcelData
    {

        #region Serialize
        public const char SERIALIZE_SEPARATOR = ';';

        public static void Serialize(out string[] list, string data)
        {
            string[] str = data.Split(';');
            list = str;
        }

        public static void SerializeObj(out uint[] list, string data)
        {
            string[] str = data.Split(',');
            list = new uint[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                if (!string.IsNullOrEmpty(str[i]))
                {
                    list[i] = System.Convert.ToUInt32(str[i].Trim());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Serialize(List<string> list, string data)
        {
            if (list == null)
                list = new List<string>();

            if (!string.IsNullOrEmpty(data))
            {
                string[] arr = data.Split(SERIALIZE_SEPARATOR);
                for (int arrI = 0; arrI < arr.Length; ++arrI)
                {
                    string id = arr[arrI];
                    if (!string.IsNullOrEmpty(id))
                    {
                        list.Add(System.Convert.ToString(arr[arrI]));
                    }
                    else
                    {
                        list.Add(" ");
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Serialize(List<uint> list, string data)
        {
            if (list == null)
                list = new List<uint>();

            if (!string.IsNullOrEmpty(data))
            {
                string[] arr = data.Split(SERIALIZE_SEPARATOR);
                for (int arrI = 0; arrI < arr.Length; ++arrI)
                {
                    string id = arr[arrI];
                    if (!string.IsNullOrEmpty(id))
                    {
                        list.Add(System.Convert.ToUInt32(arr[arrI]));
                    }
                    else
                    {
                        list.Add(0);
                    }
                }
            }

            return true;
        }

        public static bool Serialize(ref KeyValuePair<int, float> output, string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                string[] strs = ToTupleString2(data);
                if (strs != null)
                {
                    output = new KeyValuePair<int, float>(System.Convert.ToInt32(strs[0])
                                , System.Convert.ToSingle(strs[1]));
                }
            }
            else
            {
                output = new KeyValuePair<int, float>();
            }
            return true;
        }

        public static bool Serialize(ref KeyValuePair<int, bool> output, string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                string[] strs = ToTupleString2(data);
                if (strs != null)
                {
                    output = new KeyValuePair<int, bool>(System.Convert.ToInt32(strs[0])
                                , System.Convert.ToBoolean(System.Convert.ToInt32(strs[1])));
                }
            }
            else
            {
                output = new KeyValuePair<int, bool>();
            }

            return true;
        }
        #endregion

        #region ToMask
        //将文本转成合适的掩码
        public static uint ToMaskInt32(string data, uint def = 0)
        {
            uint result = 0;
            if (!string.IsNullOrEmpty(data) && data != "0")
            {
                string[] arr = data.Split(';');
                if (arr != null)
                {
                    for (int i = 0; i < arr.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(arr[i]))
                            Common.SetBit32(ref result, System.Convert.ToByte(arr[i]));
                    }
                }
            }
            else
            {
                result = def;
            }

            return result;
        }

        //将文本转成合适的掩码
        public static ulong ToMaskInt64(string data, ulong def = 0)
        {
            ulong result = def;
            if (!string.IsNullOrEmpty(data))
            {
                string[] arr = data.Split(';');
                if (arr != null)
                {
                    for (int i = 0; i < arr.Length; ++i)
                    {
                        if (!string.IsNullOrEmpty(arr[i]))
                            Common.SetBit64(ref result, System.Convert.ToByte(arr[i]));
                    }
                }
            }

            return result;
        }
        #endregion

        #region ToTuple
        //将文本转成元组
        public static int[] ToTupleInt2(string data)
        {
            int[] arr = new int[2];
            if (!string.IsNullOrEmpty(data))
            {
                string[] str = data.ToString().Split(',');
                if (str.Length >= 2)
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        arr[i] = int.Parse(str[i]);
                    }
                }
            }

            return arr;
        }
        //将文本转成元组
        public static int[] ToTupleInt3(string data)
        {
            int[] arr = new int[3];
            if (!string.IsNullOrEmpty(data))
            {
                string[] str = data.ToString().Split(',');
                if (str.Length == 3)
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        arr[i] = int.Parse(str[i]);
                    }
                }
            }
            return arr;
        }
        //将文本转成元组
        public static int[] ToTupleInt4(string data)
        {
            int[] arr = new int[4];
            if (!string.IsNullOrEmpty(data))
            {
                string[] str = data.ToString().Split(',');
                if (str.Length == 4)
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        arr[i] = int.Parse(str[i]);
                    }
                }
            }
            return arr;
        }

        //将文本转成元组
        public static int[] ToTupleInt5(string data)
        {
            int[] arr = new int[5];
            if (!string.IsNullOrEmpty(data))
            {
                string[] str = data.ToString().Split(',');
                if (str.Length == 5)
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        arr[i] = int.Parse(str[i]);
                    }
                }
            }
            return arr;
        }

        //将文本转成元组
        public static int[] ToTupleInt6(string data)
        {
            int[] arr = new int[6];
            if (!string.IsNullOrEmpty(data))
            {
                string[] str = data.ToString().Split(',');
                if (str.Length == 6)
                {
                    for (int i = 0; i < 6; ++i)
                    {
                        arr[i] = int.Parse(str[i]);
                    }
                }
            }
            return arr;
        }

        //将文本转成元组
        public static string[] ToTupleString2(string data)
        {
            string[] str = data.ToString().Split(',');
            if (str.Length == 2)
                return str;
            return null;
        }
        //将文本转成元组
        public static string[] ToTupleString3(string data)
        {
            string[] str = data.ToString().Split(',');
            if (str.Length == 3)
                return str;
            return null;
        }
        //将文本转成元组
        public static string[] ToTupleString4(string data)
        {
            string[] str = data.ToString().Split(',');
            if (str.Length == 4)
                return str;
            return null;
        }

        //将文本转成元组
        public static float[] ToTupleFloat2(string data)
        {
            float[] arr = new float[2];
            if (!string.IsNullOrEmpty(data))
            {
                string[] str = data.ToString().Split(',');
                if (str.Length >= 2)
                {
                    for (int i = 0; i < 2; ++i)
                    {
                        arr[i] = float.Parse(str[i]);
                    }
                }
            }

            return arr;
        }
        #endregion

        #region ToArray
        //将文本转成二维 int 数组//
        public static int[][] ToArrayInt2(string data)
        {
            string[] str1 = data.ToString().Split(new char[] { ';' });
            int[][] arr = new int[str1.Length - 1][];
            for (int i = 0; i < arr.Length; i++)
            {
                string[] str2 = str1[i].ToString().Split(new char[] { ',' });
                arr[i] = new int[str2.Length];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = int.Parse(str2[j]);
                }
            }
            return arr;
        }
        //将文本转成1维 int 数组//
        static public int[] ToArrayInt1(string data)
        {
            string[] str1 = data.ToString().Split(new char[] { ';' });
            int[] arr = new int[str1.Length - 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = int.Parse(str1[i]);
            }
            return arr;
        }

        //将文本转成1维 uint 数组//
        static public uint[] ToArrayUInt1(string data)
        {
            string[] str1 = data.ToString().Split(new char[] { ';' });
            uint[] arr = new uint[str1.Length - 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = uint.Parse(str1[i]);
            }
            return arr;
        }

        /// <summary>
        /// 将文本转成二维 float 数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static float[][] ToArrayFloat2(string data)
        {
            string[] str1 = data.ToString().Split(new char[] { ';' });
            float[][] arr = new float[str1.Length - 1][];
            for (int i = 0; i < arr.Length; i++)
            {
                string[] str2 = str1[i].ToString().Split(new char[] { ',' });
                arr[i] = new float[str2.Length];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = float.Parse(str2[j]);
                }
            }
            return arr;
        }

        /// <summary>
        /// 将文本转成1维 float 数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public float[] ToArrayFloat1(string data)
        {
            string[] str1 = data.ToString().Split(new char[] { ';' });
            float[] arr = new float[str1.Length - 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = float.Parse(str1[i]);
            }
            return arr;
        }

        /// <summary>
        /// 将文本转成1维 string 数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public string[] ToArrayString1(string data)
        {
            string[] str1 = data.ToString().Split(new char[] { ';' });
            string[] arr = new string[str1.Length - 1];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = str1[i];
            }
            return arr;
        }
        #endregion

        /// <summary>
        ///   行索引标识
        /// </summary>
        private enum emExcelLineIndex
        {
            Identifier = 0,     // 标识符
            Type,               // 类型
            Description,        // 描述
            Default,            // 默认值
            Content,            // 内容
        }

        /// <summary>
        ///   默认值
        /// </summary>
        private static Dictionary<string, string> DefaultValueDic = new Dictionary<string, string>()
        {
            {"int", "0"},
            {"uint", "0"},
            {"long", "0"},
            {"ulong", "0"},
            {"float", "0"},
            {"string", ""},
        };


        /// <summary>
        /// 解释读到TXT内容   
        /// </summary>
        /// <param name="address"></param>
        /// <param name="strLine"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        static public void ExplainString(string address, string strLine, out Dictionary<string, List<string>> content)
        {
            //文本全部内容
            content = new Dictionary<string, List<string>>();

            //文本类型
            Dictionary<string, string> type = new Dictionary<string, string>();
            //文本第初始值
            Dictionary<string, string> initNum = new Dictionary<string, string>();

            //读每一行
            string[] lineArray = strLine.Split(new char[] { '\n' });
            //行的个数//
            int rows = lineArray.Length - 1;
            //列的个数//
            int Columns = lineArray[0].Split(new char[] { '\t' }).Length;

            //列名//
            string[] ColumnName = new string[Columns];
            for (int i = 0; i < rows; i++)
            {
                //当前行的全部列//
                string[] Array = lineArray[i].Split(new char[] { '\t' });

                //检查是否为空行
                bool empty_line = true;
                for (int array_i = 0; array_i < Array.Length; ++array_i)
                {
                    if (!string.IsNullOrEmpty(Array[array_i].Trim()))
                    {
                        empty_line = false;
                    }
                }
                if (empty_line)
                    break;

                for (int j = 0; j < Columns; j++)
                {
                    try
                    {

                        string nvalue = Array[j].Trim();

                        //标题//
                        if (i == (int)emExcelLineIndex.Identifier)
                        {
                            ColumnName[j] = nvalue;
                            //返回出去的Key值//
                            content[ColumnName[j]] = new List<string>();
                        }
                        //类型//
                        else if (i == (int)emExcelLineIndex.Type)
                        {
                            nvalue = nvalue.Replace("\"", "");
                            type[ColumnName[j]] = nvalue;
                        }
                        //说明//
                        else if (i == (int)emExcelLineIndex.Description)
                        {
                        }
                        //初始值//
                        else if (i == (int)emExcelLineIndex.Default)
                        {
                            initNum[ColumnName[j]] = nvalue;
                        }
                        //值//
                        else if (i >= (int)emExcelLineIndex.Content)
                        {
                            string key = ColumnName[j];
                            int index = i - (int)emExcelLineIndex.Content;
                            content[key].Add("");

                            nvalue = nvalue.Replace("\"\"'\"\"", ",");
                            nvalue = nvalue.Replace("\"\"n\"\"", "\n");

                            if (nvalue.IndexOf("\"") == 0 && nvalue.IndexOf("\"pic") != 0)
                                nvalue = nvalue.Remove(0, 1);

                            if (nvalue.Length > 0)
                            {
                                if (nvalue.LastIndexOf("\"") == nvalue.Length - 1)
                                {
                                    //Debug.Log (nvalue);
                                    nvalue = nvalue.Remove(nvalue.Length - 1, 1);
                                }
                            }

                            content[key][index] = nvalue;

                            if (string.IsNullOrEmpty(content[key][index]))
                            {
                                content[key][index] = initNum[ColumnName[j]];

                                if (string.IsNullOrEmpty(content[key][index]))
                                {
                                    //载入默认值
                                    var itr = DefaultValueDic.GetEnumerator();
                                    while (itr.MoveNext())
                                    {
                                        if (type[key] == itr.Current.Key)
                                        {
                                            content[key][index] = itr.Current.Value;
                                        }
                                    }
                                    itr.Dispose();
                                }
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(e.Message + address + "读取出错在\n第 " + i + " 行第 " + j + " 列之前./n");
                        return;
                    }
                }
            }
        }

        #region 利用反射从txt中读取数据

        private static readonly BindingFlags binding_flags_ = (BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        ///   利用反射读取数据
        /// </summary>
        public static bool ImportData<Model>(ref Model data, Dictionary<string, List<string>> content, System.Type element)
        {
            int i = 0;
            try
            {
                Dictionary<string, string> objs = new Dictionary<string, string>();

                int row = 0;
                var itr = content.Values.GetEnumerator();
                if (itr.MoveNext())
                {
                    row = itr.Current.Count;
                }
                object[] datas = new object[row];

                for (i = 0; i < row; i++)                    //i行
                {
                    //得到列的key
                    var str = content.Keys.GetEnumerator();     //第几列
                    while (str.MoveNext())
                    {
                        string key = str.Current;
                        string value = content[key][i];
                        objs.Add(key, value);
                    }

                    object item = ParseObjectByDictionary(objs, element);
                    objs.Clear();

                    if (item != null)
                    {
                        datas[i] = item;
                    }
                }

                System.Type type = data.GetType();

                FieldInfo[] field = type.GetFields(binding_flags_);

                for (int j = 0; j < field.Length; j++)
                {
                    if (field[j].FieldType.IsGenericType)
                    {
                        object obj = Common.Invoke<object>(datas, "Add", field[j].FieldType);
                        field[j].SetValue(data, obj);
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("ExcelSerializer.Load<" + typeof(Model).Name + ">(), catch exception!\n"
            + "----Index:" + i + " -----" + ex.Message);
            }
            return false;
        }

        /// <summary>
        ///   
        /// </summary>
        public static object ParseObjectByDictionary(Dictionary<string, string> obj, System.Type type)
        {
            object model = null;
            try
            {
                model = Activator.CreateInstance(type);

                var itr = obj.Keys.GetEnumerator();
                while (itr.MoveNext())
                {
                    string key = itr.Current;
                    string value = obj[key];
                    ParseObjectByContainer(ref model, key, value);
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message + "对象：" + model == null ? null : model);
            }
            return model;
        }

        static string str = "_";

        /// <summary>
        ///   
        /// </summary>
        private static void ParseObjectByContainer(ref object element, string key, string value)
        {
            Type type = element.GetType();
            FieldInfo property = type.GetField(str + key, binding_flags_);
            if (property == null)
            {
                Debug.LogError("对象为" + element.GetType() + "的字段名与表中的标识不一致!" + key);
                return;
            }
            Type fieldType = property.FieldType;

            try
            {
                if (fieldType.IsEnum)  //枚举
                {
                    object obj = Enum.Parse(fieldType, value);
                    property.SetValue(element, obj);
                }
                else if (fieldType.IsClass)
                {
                    //解析泛型
                    if (fieldType.IsGenericType)     //如果是泛型
                    {
                        Type generic_type = fieldType.GetGenericArguments()[0];
                        List<object> types = new List<object>();

                        //泛型参数为对象
                        if (generic_type.IsClass)
                        {
                            string[] objs = null;                 //对象数组                  
                            ExcelData.Serialize(out objs, value);                  //对象拆分    
                                                                                   //属性
                            PropertyInfo[] sub_field = generic_type.GetProperties();

                            for (int i = 0; i < objs.Length; i++)
                            {
                                uint[] item = null;
                                ExcelData.SerializeObj(out item, objs[i]);         //对象属性拆分

                                //单个对象                          
                                object obj = Activator.CreateInstance(generic_type);
                                types.Add(obj);

                                for (int j = 0; j < item.Length; j++)
                                {
                                    string sub_key = sub_field[j].Name;
                                    string sub_value = item[j].ToString();

                                    ParseObjectByContainer(ref obj, sub_key, sub_value);
                                }
                            }

                            object addobj = Common.Invoke<object>(types.ToArray(), "Add", fieldType);
                            property.SetValue(element, addobj);
                        }
                        //泛型参数为值类型
                        else
                        {
                            List<uint> value_ = new List<uint>();
                            ExcelData.Serialize(value_, value);

                            object obj = Common.Invoke<uint>(value_.ToArray(), "Add", fieldType);
                            property.SetValue(element, obj);
                        }
                    }
                    else    //普通对象
                    {
                        //判断是否是String
                        if (fieldType.ToString() != "System.String")
                        {
                            uint[] item = null;
                            ExcelData.SerializeObj(out item, value);         //对象属性拆分
                            PropertyInfo[] sub_field = fieldType.GetProperties();                                           //单个对象                          
                            object obj = Activator.CreateInstance(fieldType);

                            for (int j = 0; j < item.Length; j++)
                            {
                                string sub_key = sub_field[j].Name;
                                string sub_value = item[j].ToString();

                                ParseObjectByContainer(ref obj, sub_key, sub_value);
                            }
                            property.SetValue(element, obj);
                        }
                        else
                        {
                            object obj = Convert.ChangeType(value, fieldType);
                            property.SetValue(element, obj);
                        }
                    }
                }
                else
                {
                    object obj = Convert.ChangeType(value, fieldType);
                    property.SetValue(element, obj);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message + type.Name + fieldType);
            }
        }
        #endregion

    }
}