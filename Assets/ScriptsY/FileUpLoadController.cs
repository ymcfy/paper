using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// 连接数据库并上传
/// </summary>
public class FileUpLoadController : MonoBehaviour
{
    //下拉菜单信息
    public Dropdown dp;

    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    //private static string sqlAddress = "Data source=127.0.0.1;Initial Catalog=SqlTest;User ID=web;Password=web";
    private static string sqlAddress = SqlConstant.sqlAddress;

    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);

    public static string DeviceId;

    public static string EquipmentName;

    public static string DeviceModel;

    public static string EquipmentType;

    public static string ProductionDate;

    public static string Manufacturer;

    public static string StorageLocation;

    //public static string QiYongShiJian;

    //public static string LiuLiang;

    //public static string ZuiDaGongZuoYaLi;

    //public static string ZhuanSu;

    //public static string GongLv;

    public static string ImgSourcePath;

    public static string ImgType;

    public static string modelType;

    public static string SourcePath;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 进行上传函数选择
    /// </summary>
    /// <param name="name">文件名称</param>
    /// <param name="type">文件后缀名</param>
    /// <param name="sourcePath">文件源路径</param>
    /// <param name="clazz">文件所属img video file类型</param>
    public void Upload(string name, string type, string sourcePath, string clazz)
    {
        if (clazz == "img")
        {
            ImgUpload(name, type, sourcePath);
        }
        else if (clazz == "video")
        {
            VideoUpload(name, type, sourcePath);
        }
        else
        {
            FileUpload(name, type, sourcePath);
        }
    }

    /// <summary>
    /// 图片上传
    /// </summary>
    /// <param name="name">图片名字</param>
    /// <param name="type">图片后缀名</param>
    /// <param name="sourcePath">图片源路径</param>
    public void ImgUpload(string name, string type, string sourcePath)
    {
        //打开数据库连接
        sqlCon.Open();

        string strJudgeSql = "select * from [SqlTest].[dbo].[img] where name = '" + name + "'and type = '" + type + "'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

        //数据存储表
        DataTable sqlDataTable = new DataTable();

        //执行查询
        sqlAdapter.Fill(sqlDataTable);

        if (sqlDataTable.Rows.Count > 0)
        {
            //数据库连接关闭
            sqlCon.Close();
            GameObject.Find("Canvas3/上传重复").transform.position = new Vector3(956, 640, 600);
        }
        else
        {
            //图片目标存储路径
            string path = Application.dataPath + "//Resources//folder//img//" + name + type;


            //进行复制
            System.IO.File.Copy(sourcePath, path);


            //定义数据库添加操作语句 ‘“++”’
            string strSql = "insert into [SqlTest].[dbo].[img] values('" + name + "','" + type + "','" + path + "','" + sourcePath + "')";

            //数据库执行操作
            SqlCommand com = new SqlCommand(strSql, sqlCon);

            //执行数据库添加操作
            com.ExecuteNonQuery();

            //数据库连接关闭
            sqlCon.Close();

            //显示上传成功
            //GameObject.Find("Canvas3/上传成功").transform.position = new Vector3(956, 640, 600);
        }


    }
    public void VideoUpload(string name, string type, string sourcePath)
    {
        sqlCon.Open();
        string strJudgeSql = "select * from [SqlTest].[dbo].[video] where name = '" + name + "'and type = '" + type + "'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

        //数据存储表
        DataTable sqlDataTable = new DataTable();

        //执行查询
        sqlAdapter.Fill(sqlDataTable);

        if (sqlDataTable.Rows.Count > 0)
        {
            //数据库连接关闭
            sqlCon.Close();
            GameObject.Find("Canvas3/上传重复").transform.position = new Vector3(956, 640, 600);
        }
        else
        {
            string path = Application.dataPath + "//Resources//folder//video//" + name + type;
            System.IO.File.Copy(sourcePath, path);
            string strSql = "insert into [SqlTest].[dbo].[video] values('" + name + "','" + type + "','" + path + "','" + sourcePath + "')";
            SqlCommand com = new SqlCommand(strSql, sqlCon);
            com.ExecuteNonQuery();
            sqlCon.Close();
            GameObject.Find("Canvas3/上传成功").transform.position = new Vector3(956, 640, 600);
        }

    }
    public void FileUpload(string name, string type, string sourcePath)
    {
        sqlCon.Open();
        string strJudgeSql = "select * from [SqlTest].[dbo].[file] where name = '" + name + "'and type = '" + type + "'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

        //数据存储表
        DataTable sqlDataTable = new DataTable();

        //执行查询
        sqlAdapter.Fill(sqlDataTable);

        if (sqlDataTable.Rows.Count > 0)
        {
            //数据库连接关闭
            sqlCon.Close();
            GameObject.Find("Canvas3/上传重复").transform.position = new Vector3(956, 640, 600);
        }
        else
        {
            string path = Application.dataPath + "//Resources//folder//file//" + name + type;
            System.IO.File.Copy(sourcePath, path);
            string strSql = "insert into [SqlTest].[dbo].[file] values('" + name + "','" + type + "','" + path + "','" + sourcePath + "')";
            SqlCommand com = new SqlCommand(strSql, sqlCon);
            com.ExecuteNonQuery();
            sqlCon.Close();
            GameObject.Find("Canvas3/上传成功").transform.position = new Vector3(956, 640, 600);
        }

    }

    public void TypeChoose()
    {
        GameObject.Find("Canvas3/模型选择").transform.position = new Vector3(956, 640, 600);
    }

    public void Clear()
    {
        foreach (Transform T in this.GetComponentsInChildren<Transform>())
        {
            if (T.name.Contains("x"))
            {
                T.GetComponentInChildren<Text>().text = "";
            }
        }
    }

    //public void typeUpLoad()
    //{
    //    if (modelType == "消防设备")
    //    {
    //        XiaoFangBengModel();
    //    }
    //    else if (modelType == "汽车")
    //    {
    //        QiCheModel();
    //    }
    //    else if (modelType == "管线")
    //    {
    //        ModelUpLoad();
    //    }
    //    else if (modelType == "阀门")
    //    {
    //        FaMennModel();
    //    }
    //}

    //public void FaMennModel()
    //{

    //    sqlCon.Open();
    //    string strJudgeSql = "select * from [SqlTest].[dbo].[SheBeiTest3] where SheBeiMingCheng = '" + SheBeiMingCheng + "'";
    //    //检索保存数据
    //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

    //    //数据存储表
    //    DataTable sqlDataTable = new DataTable();

    //    //执行查询
    //    sqlAdapter.Fill(sqlDataTable);

    //    if (sqlDataTable.Rows.Count > 0)
    //    {
    //        //数据库连接关闭
    //        sqlCon.Close();
    //        GameObject.Find("Canvas/数据库4").transform.position = new Vector3(-1060, 353, 0);
    //        for (int i = 0; i < GameObject.Find("Canvas/数据库4").transform.childCount; i++)
    //        {
    //            if (GameObject.Find("Canvas/数据库4").transform.GetChild(i).name.Contains("a"))
    //            {
    //                InputField input = GameObject.Find("Canvas/数据库4").transform.GetChild(i).GetComponent<InputField>();
    //                input.text = "";
    //            }
    //        }
    //        GameObject.Find("Canvas/上传重复").transform.position = new Vector3(529, 430, 600);
    //    }
    //    else
    //    {
    //        //模型目标存储路径
    //        string path = Application.dataPath + "//Resources//folder//model/FaMen/" + SheBeiMingCheng +modelType;
    //        string modelPath = SheBeiMingCheng + modelType;

    //        //模型图片存储路径
    //        string imgDestPath = Application.dataPath + "//Resources//folder//modelImg/FaMen/" + SheBeiMingCheng +ImgType;
    //        string imPath = SheBeiMingCheng + ImgType;


    //        //进行复制模型
    //        System.IO.File.Copy(SourcePath, path);

    //        //进行复制模型图片
    //        System.IO.File.Copy(ImgSourcePath, imgDestPath);

    //        //打开数据库连接

    //        //定义数据库添加操作语句 ‘“++”’
    //        string strSql = "insert into SheBeiTest3 values('" + SheBeiMingCheng + "','" + GuDingZiChanBianHao + "','" + XingHao + "','" + GuiGe + "','" + modelPath + "','" + SourcePath + "','" + imPath + "')";

    //        //数据库执行操作
    //        SqlCommand com = new SqlCommand(strSql, sqlCon);

    //        //执行数据库添加操作
    //        com.ExecuteNonQuery();

    //        //数据库连接关闭
    //        sqlCon.Close();

    //        GameObject.Find("Canvas/数据库4").transform.position = new Vector3(-1060, 353, 0);
    //        for (int i = 0; i < GameObject.Find("Canvas/数据库4").transform.childCount; i++)
    //        {
    //            if (GameObject.Find("Canvas/数据库4").transform.GetChild(i).name.Contains("a"))
    //            {
    //                InputField input = GameObject.Find("Canvas/数据库4").transform.GetChild(i).GetComponent<InputField>();
    //                input.text = "";
    //            }
    //        }

    //        //显示上传成功
    //        GameObject.Find("Canvas/上传成功").transform.position = new Vector3(529, 430, 600);
    //    }


    //}

    //public void XiaoFangBengModel()
    //{
    //    sqlCon.Open();
    //    string strJudgeSql = "select * from [SqlTest].[dbo].[SheBeiTest] where SheBeiMingCheng = '" + SheBeiMingCheng + "'";
    //    //检索保存数据
    //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

    //    //数据存储表
    //    DataTable sqlDataTable = new DataTable();

    //    //执行查询
    //    sqlAdapter.Fill(sqlDataTable);

    //    if (sqlDataTable.Rows.Count > 0)
    //    {
    //        //数据库连接关闭
    //        sqlCon.Close();
    //        GameObject.Find("Canvas/数据库1").transform.position = new Vector3(-860, 353, 0);
    //        for (int i = 0; i < GameObject.Find("Canvas/数据库1").transform.childCount; i++)
    //        {
    //            if (GameObject.Find("Canvas/数据库1").transform.GetChild(i).name.Contains("a"))
    //            {
    //                InputField input = GameObject.Find("Canvas/数据库1").transform.GetChild(i).GetComponent<InputField>();
    //                input.text = "";
    //            }
    //        }
    //        GameObject.Find("Canvas/上传重复").transform.position = new Vector3(529, 430, 600);
    //    }
    //    else
    //    {
    //        //模型目标存储路径
    //        string path = Application.dataPath + "//Resources//folder//model/XiaoFangSheBei/" + SheBeiMingCheng +modelType;
    //        string modelPath = SheBeiMingCheng + modelType;

    //        //模型图片存储路径
    //        string imgDestPath = Application.dataPath + "//Resources//folder//modelImg/XiaoFangSheBei/" + SheBeiMingCheng + ImgType;
    //        string imPath = SheBeiMingCheng + ImgType;


    //        //进行复制模型
    //        System.IO.File.Copy(SourcePath, path);

    //        //进行复制模型图片
    //        System.IO.File.Copy(ImgSourcePath, imgDestPath);


    //        //定义数据库添加操作语句 ‘“++”’
    //        string strSql = "insert into SheBeiTest values('" + SheBeiMingCheng + "','" + GuDingZiChanBianHao + "','" + XingHao + "','" + GuiGe + "','" + modelPath + "','" + SourcePath + "','" + imPath + "')";

    //        //数据库执行操作
    //        SqlCommand com = new SqlCommand(strSql, sqlCon);

    //        //执行数据库添加操作
    //        com.ExecuteNonQuery();

    //        //数据库连接关闭
    //        sqlCon.Close();

    //        GameObject.Find("Canvas/数据库1").transform.position = new Vector3(-860, 353, 0);

    //        for (int i = 0; i < GameObject.Find("Canvas/数据库1").transform.childCount; i++)
    //        {
    //            if (GameObject.Find("Canvas/数据库1").transform.GetChild(i).name.Contains("a"))
    //            {
    //                InputField input = GameObject.Find("Canvas/数据库1").transform.GetChild(i).GetComponent<InputField>();
    //                input.text = "";
    //            }
    //        }


    //        //显示上传成功
    //        GameObject.Find("Canvas/上传成功").transform.position = new Vector3(529, 430, 600);
    //    }


    //}

    //public void QiCheModel()
    //{
    //    sqlCon.Open();
    //    string strJudgeSql = "select * from [SqlTest].[dbo].[SheBeiTest2] where SheBeiMingCheng = '" + SheBeiMingCheng + "'";
    //    //检索保存数据
    //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

    //    //数据存储表
    //    DataTable sqlDataTable = new DataTable();

    //    //执行查询
    //    sqlAdapter.Fill(sqlDataTable);

    //    if (sqlDataTable.Rows.Count > 0)
    //    {
    //        //数据库连接关闭
    //        sqlCon.Close();
    //        GameObject.Find("Canvas/数据库3").transform.position = new Vector3(-660, 353, 0);
    //        for (int i = 0; i < GameObject.Find("Canvas/数据库3").transform.childCount; i++)
    //        {
    //            if (GameObject.Find("Canvas/数据库3").transform.GetChild(i).name.Contains("a"))
    //            {
    //                InputField input = GameObject.Find("Canvas/数据库3").transform.GetChild(i).GetComponent<InputField>();
    //                input.text = "";
    //            }
    //        }
    //        GameObject.Find("Canvas/上传重复").transform.position = new Vector3(529, 430, 600);
    //    }
    //    else
    //    {
    //        //模型目标存储路径
    //        string path = Application.dataPath + "//Resources//folder//model/QiChe/" + SheBeiMingCheng +  modelType;
    //        string modelPath = SheBeiMingCheng + modelType;

    //        //模型图片存储路径
    //        string imgDestPath = Application.dataPath + "//Resources//folder//modelImg/QiChe/" + SheBeiMingCheng + ImgType;
    //        string imPath = SheBeiMingCheng + ImgType;


    //        //进行复制模型
    //        System.IO.File.Copy(SourcePath, path);

    //        //进行复制模型图片
    //        System.IO.File.Copy(ImgSourcePath, imgDestPath);

    //        //打开数据库连接
    //        //sqlCon.Open();

    //        //定义数据库添加操作语句 ‘“++”’
    //        string strSql = "insert into SheBeiTest2 values('" + SheBeiMingCheng + "','" + GuDingZiChanBianHao + "','" + XingHao + "','" + GuiGe + "','" + ZhiZaoShang + "','" + ChuChangShiJian + "','" + AnZhuangDiDian + "','" + QiYongShiJian + "','" + LiuLiang + "','" + ZuiDaGongZuoYaLi + "','" + ZhuanSu + "','" + GongLv + "','" + modelPath + "','" + SourcePath + "','" + imPath + "')";

    //        //数据库执行操作
    //        SqlCommand com = new SqlCommand(strSql, sqlCon);

    //        //执行数据库添加操作
    //        com.ExecuteNonQuery();

    //        //数据库连接关闭
    //        sqlCon.Close();

    //        GameObject.Find("Canvas/数据库3").transform.position = new Vector3(-660, 353, 0);
    //        for (int i = 0; i < GameObject.Find("Canvas/数据库3").transform.childCount; i++)
    //        {
    //            if (GameObject.Find("Canvas/数据库3").transform.GetChild(i).name.Contains("a"))
    //            {
    //                InputField input = GameObject.Find("Canvas/数据库3").transform.GetChild(i).GetComponent<InputField>();
    //                input.text = "";
    //            }
    //        }
    //        //显示上传成功
    //        GameObject.Find("Canvas/上传成功").transform.position = new Vector3(529, 430, 600);
    //    }


    //}

    public void ModelUpLoad()
    {
        sqlCon.Open();
        string strJudgeSql = "select * from [SqlTest].[dbo].[Equipment] where DeviceId = '" + DeviceId + "'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

        //数据存储表
        DataTable sqlDataTable = new DataTable();

        //执行查询
        sqlAdapter.Fill(sqlDataTable);

        if (sqlDataTable.Rows.Count > 0)
        {
            //数据库连接关闭
            sqlCon.Close();
            GameObject.Find("Canvas3/数据库2").transform.position = new Vector3(956, 640, 0);
            for (int i = 0; i < GameObject.Find("Canvas3/数据库2").transform.childCount; i++)
            {
                if (GameObject.Find("Canvas3/数据库2").transform.GetChild(i).name.Contains("a"))
                {
                    InputField input = GameObject.Find("Canvas3/数据库2").transform.GetChild(i).GetComponent<InputField>();
                    input.text = "";
                }
            }
            GameObject.Find("Canvas3/上传重复").transform.position = new Vector3(956, 640, 600);
        }
        else
        {
            //模型目标存储路径
            string path = Application.dataPath + "//Resources//folder//model/" + DeviceId + modelType;
            string modelPath = DeviceId + modelType;

            //模型图片存储路径
            string imgDestPath = Application.dataPath + "//Resources//folder//modelImg/" + DeviceId + ImgType;
            string imPath = DeviceId + ImgType;


            //进行复制模型
            System.IO.File.Copy(SourcePath, path, true);

            //进行复制模型图片
            System.IO.File.Copy(ImgSourcePath, imgDestPath, true);

            //打开数据库连接
            //sqlCon.Open();

            //定义数据库添加操作语句 ‘“++”’
            string strSql = "insert into Equipment(DeviceId,EquipmentName,DeviceModel,EquipmentType,ProductionDate,Manufacturer,StorageLocation,StorePath,SourcePath,ImgPath) values('" + DeviceId + "','" + EquipmentName + "','" + DeviceModel + "','" + EquipmentType + "','" + ProductionDate + "','" + Manufacturer + "','" + StorageLocation + "','" + modelPath + "','" + SourcePath + "','" + imPath + "')";

            //数据库执行操作
            SqlCommand com = new SqlCommand(strSql, sqlCon);

            //执行数据库添加操作
            com.ExecuteNonQuery();

            //数据库连接关闭
            sqlCon.Close();

            GameObject.Find("Canvas3/数据库2").transform.position = new Vector3(956, 640, 0);
            for (int i = 0; i < GameObject.Find("Canvas3/数据库2").transform.childCount; i++)
            {
                if (GameObject.Find("Canvas3/数据库2").transform.GetChild(i).name.Contains("a"))
                {
                    InputField input = GameObject.Find("Canvas3/数据库2").transform.GetChild(i).GetComponent<InputField>();
                    input.text = "";
                }
            }
            //显示上传成功
            GameObject.Find("Canvas3/上传成功").transform.position = new Vector3(956, 640, 600);
        }


    }

    //当点击上传成功滚动框的上传成功按钮时
    public void ShangChuanChengGong()
    {
        GameObject.Find("Canvas3/上传成功").transform.position = new Vector3(2323, 627, 600);
    }
    public void ShanChuChengGong()
    {
        GameObject.Find("Canvas3/删除成功").transform.position = new Vector3(2323, 687, 600);
    }

    public void ShangChuanChongFu()
    {
        GameObject.Find("Canvas3/上传重复").transform.position = new Vector3(2323, 627, 600);
    }
}
