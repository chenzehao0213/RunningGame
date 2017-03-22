/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note: 
***************************************************************/
using UnityEngine;
using System.Collections;
using ModelData;

public class Config : Singleton<Config>
{
    /// <summary>
    ///   序列化方式
    /// </summary>
    public emSerializeMode Mode = emSerializeMode.Excel;

    /// <summary>
    ///   数据文件路径
    /// </summary>
    public string DataFilePath;

    public void SetSerializer<Model>(out Serializer<Model> serializer)
        where Model : class
    {
        serializer = null;
        switch (Mode)
        {
            case emSerializeMode.Excel:
                serializer = new ExcelSerializer<Model>();
                break;
            case emSerializeMode.Proto:
                serializer = new ProtoSerializer<Model>();
                break;
        }
    }

    public string GetDataFilePath(string file_name)
    {
        string path = null;
 
        switch (Mode)
        {
            case emSerializeMode.Excel:
                path = Common.DATA_FILE_PATH + Common.EXCEL_FILE_PATH + file_name +
                    Common.EXCEL_EXTENSION_NAME;
                break;
            case emSerializeMode.Proto:
                path = Common.DATA_FILE_PATH + Common.DAT_FOLDER_NAME +
                    file_name + Common.DAT_EXTENSION_NAME;
                break;
        }
        return path;
    }

}