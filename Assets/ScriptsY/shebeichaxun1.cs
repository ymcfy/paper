using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Data.SqlClient;
using System.Data;
using System.IO;

/// <summary>
/// 挂接在第3模块GameObject下
/// </summary>
public class shebeichaxun1 : MonoBehaviour
{
    //初始化数据 后面用于文本的显示
    //下拉选项数据集合可以理解成在总的列表框
    List<Dropdown.OptionData> Dd1 = new List<Dropdown.OptionData>();
    List<Dropdown.OptionData> Dd2 = new List<Dropdown.OptionData>();
    List<Dropdown.OptionData> Dd3 = new List<Dropdown.OptionData>();
    List<Dropdown.OptionData> Dd4 = new List<Dropdown.OptionData>();
    //定义变量替换显示的文本名称
    public string s1;
    public string s2;
    //private string DeviceId;
    //下拉表单 
    public GameObject dropdown;
    public GameObject dropdown1;
    public GameObject LaoBeng1h;
    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    private static string sqlAddress = SqlConstant.sqlAddress;
    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);

    //右下角坐标
    public Vector3 BottomRightCorner;

    //右下角外面坐标
    public Vector3 OutSideBottomRightCorner;

    //正中间坐标
    public Vector3 Center;
    void Start()
    {
        //右下角坐标
        BottomRightCorner = new Vector3(1865, 50, 600);

        //正中间坐标
        Center = new Vector3(954, 640, -100f);

        //右下角外面坐标
        OutSideBottomRightCorner = new Vector3(2310, 50, 600);
    }
    void Update()
    {
        s1 = dropdown.GetComponent<Dropdown>().captionText.text;
        s2 = dropdown1.GetComponent<Dropdown>().captionText.text;
    }

    /// <summary>
    /// 第3模块Canvas/RawImage_dropdownlist/Dropdown 选择消防设备类型
    /// 作用是选择消防设备类型
    /// </summary>
    public void xuanze() //选择模型类型
    {
        if (dropdown.GetComponent<Dropdown>().captionText.text == "消防设备")
        {
            Debug.Log("选择模型");
            Dd1.Clear();
            sqlCon.Open();
            string sql1 = "select DeviceId,EquipmentName from Equipment where EquipmentType = '"+ dropdown.GetComponent<Dropdown>().captionText.text + "'";
            SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sql1, sqlCon);
            DataTable sqlDataTable1 = new DataTable();
            sqlAdapter1.Fill(sqlDataTable1);
            Dropdown.OptionData Ddt1 = new Dropdown.OptionData();
            Ddt1.text = "请选择消防设备";
            Dd1.Add(Ddt1);
            foreach (DataRow row in sqlDataTable1.Rows)
            {
                Dropdown.OptionData Dt1 = new Dropdown.OptionData();
                Dt1.text = row[0].ToString()+" "+row[1].ToString();
                
                Dd1.Add(Dt1);
            }
            sqlCon.Close();
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd1);
        }
        if (dropdown.GetComponent<Dropdown>().captionText.text == "管线")
        {
            Dd2.Clear();
            sqlCon.Open();
            string sql1 = "select DeviceId,EquipmentName from Equipment where EquipmentType = '" + dropdown.GetComponent<Dropdown>().captionText.text + "'";
            SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sql1, sqlCon);
            DataTable sqlDataTable1 = new DataTable();
            sqlAdapter1.Fill(sqlDataTable1);
            Dropdown.OptionData Ddt1 = new Dropdown.OptionData();
            Ddt1.text = "请选择管线";
            Dd2.Add(Ddt1);
            foreach (DataRow row in sqlDataTable1.Rows)
            {
                Dropdown.OptionData Dt1 = new Dropdown.OptionData();
                Dt1.text = row[0].ToString() + " " + row[1].ToString();

                Dd2.Add(Dt1);
            }
            sqlCon.Close();
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd2);
        }
        if (dropdown.GetComponent<Dropdown>().captionText.text == "汽车")
        {
            Dd3.Clear();
            sqlCon.Open();
            string sql1 = "select DeviceId,EquipmentName from Equipment where EquipmentType = '" + dropdown.GetComponent<Dropdown>().captionText.text + "'";
            SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sql1, sqlCon);
            DataTable sqlDataTable1 = new DataTable();
            sqlAdapter1.Fill(sqlDataTable1);
            Dropdown.OptionData Ddt1 = new Dropdown.OptionData();
            Ddt1.text = "请选择汽车";
            Dd3.Add(Ddt1);
            foreach (DataRow row in sqlDataTable1.Rows)
            {
                Dropdown.OptionData Dt1 = new Dropdown.OptionData();
                Dt1.text = row[0].ToString() + " " + row[1].ToString();

                Dd3.Add(Dt1);
            }
            sqlCon.Close();
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd3);
        }
        if (dropdown.GetComponent<Dropdown>().captionText.text == "阀门")
        {
            Dd4.Clear();
            sqlCon.Open();
            string sql1 = "select DeviceId,EquipmentName from Equipment where EquipmentType = '" + dropdown.GetComponent<Dropdown>().captionText.text + "'";
            SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sql1, sqlCon);
            DataTable sqlDataTable1 = new DataTable();
            sqlAdapter1.Fill(sqlDataTable1);
            Dropdown.OptionData Ddt1 = new Dropdown.OptionData();
            Ddt1.text = "请选择阀门";
            Dd4.Add(Ddt1);
            foreach (DataRow row in sqlDataTable1.Rows)
            {
                Dropdown.OptionData Dt1 = new Dropdown.OptionData();
                Dt1.text = row[0].ToString() + " " + row[1].ToString();

                Dd4.Add(Dt1);
            }
            sqlCon.Close();
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd4);
        }
    }
    
    public void shanchu()
    {
        sqlCon.Open();
        //文件夹类型
        string Folder = "";
        ////判断文件夹类型
        //if (s1 == "消防设备")
        //{
        //    Folder = "XiaoFangSheBei";
        //}
        //else if (s1 == "管线")
        //{
        //    Folder = "GuanXian";
        //}
        //else if (s1 == "汽车")
        //{
        //    Folder = "QiChe";
        //}
        //else if (s1 == "阀门")
        //{
        //    Folder = "FaMen";
        //}
        string path = Application.dataPath + "//Resources//folder//model//";
        string Imgpath = Application.dataPath + "//Resources//folder//modelImg//";
        string[] s2String = s2.Split(' ');
        //if (s1 == "消防设备")
        //{
            string searchSql = "select * from [SqlTest].[dbo].[Equipment] where DeviceId = '" + s2String[0] + "'";
            //检索保存数据
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(searchSql, sqlCon);
            //数据存储表
            DataTable sqlDataTable = new DataTable();
            //执行查询
            sqlAdapter.Fill(sqlDataTable);
            string modelFullName;
            string modelImgFullName;
            DataRow row = sqlDataTable.Rows[0];
            modelFullName = row["StorePath"].ToString();
            modelImgFullName = row["ImgPath"].ToString();
            path += modelFullName;
            Imgpath += modelImgFullName;
            string MyDelete = "delete from [SqlTest].[dbo].[Equipment] where DeviceId = '" + s2String[0] + "'";
            SqlCommand sqlCommand = new SqlCommand(MyDelete, sqlCon);
            sqlCommand.ExecuteNonQuery();
            File.Delete(path);
            File.Delete(Imgpath);
        //}
        //if (s1 == "管线")
        //{
        //    string searchSql = "select * from [SqlTest].[dbo].[ShebeiTest1] where SheBeiMingCheng = '" + s2 + "'";
        //    //检索保存数据
        //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(searchSql, sqlCon);
        //    //数据存储表
        //    DataTable sqlDataTable = new DataTable();
        //    //执行查询
        //    sqlAdapter.Fill(sqlDataTable);
        //    string modelFullName;
        //    string modelImgFullName;
        //    DataRow row = sqlDataTable.Rows[0];
        //    modelFullName = row["StorePath"].ToString();
        //    modelImgFullName = row["ImgPath"].ToString();
        //    path += modelFullName;
        //    Imgpath += modelImgFullName;
        //    string MyDelete = "delete from [SqlTest].[dbo].[ShebeiTest1] where SheBeiMingCheng = '" + s2 + "'";
        //    SqlCommand sqlCommand = new SqlCommand(MyDelete, sqlCon);
        //    sqlCommand.ExecuteNonQuery();
        //    File.Delete(path);
        //    File.Delete(Imgpath);
        //}
        //if (s1 == "汽车")
        //{
        //    string searchSql = "select * from [SqlTest].[dbo].[ShebeiTest2] where SheBeiMingCheng = '" + s2 + "'";
        //    //检索保存数据
        //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(searchSql, sqlCon);
        //    //数据存储表
        //    DataTable sqlDataTable = new DataTable();
        //    //执行查询
        //    sqlAdapter.Fill(sqlDataTable);
        //    string modelFullName;
        //    string modelImgFullName;
        //    DataRow row = sqlDataTable.Rows[0];
        //    modelFullName = row["StorePath"].ToString();
        //    modelImgFullName = row["ImgPath"].ToString();
        //    path += modelFullName;
        //    Imgpath += modelImgFullName;
        //    string MyDelete = "delete from [SqlTest].[dbo].[ShebeiTest2] where SheBeiMingCheng = '" + s2 + "'";
        //    SqlCommand sqlCommand = new SqlCommand(MyDelete, sqlCon);
        //    sqlCommand.ExecuteNonQuery();
        //    File.Delete(path);
        //    File.Delete(Imgpath);
        //}
        //if (s1 == "阀门")
        //{
        //    string searchSql = "select * from [SqlTest].[dbo].[ShebeiTest3] where SheBeiMingCheng = '" + s2 + "'";
        //    //检索保存数据
        //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(searchSql, sqlCon);
        //    //数据存储表
        //    DataTable sqlDataTable = new DataTable();
        //    //执行查询
        //    sqlAdapter.Fill(sqlDataTable);
        //    string modelFullName;
        //    string modelImgFullName;
        //    DataRow row = sqlDataTable.Rows[0];
        //    modelFullName = row["StorePath"].ToString();
        //    modelImgFullName = row["ImgPath"].ToString();
        //    path += modelFullName;
        //    Imgpath += modelImgFullName;
        //    string MyDelete = "delete from [SqlTest].[dbo].[ShebeiTest3] where SheBeiMingCheng = '" + s2 + "'";
        //    SqlCommand sqlCommand = new SqlCommand(MyDelete, sqlCon);
        //    sqlCommand.ExecuteNonQuery();
        //    File.Delete(path);
        //    File.Delete(Imgpath);
        //}
        LaoBeng1h.SetActive(false);
        GameObject.Find("Canvas3/删除成功").transform.position = new Vector3(529, 430, 600);
        sqlCon.Close();
        xuanze();
    }
    public void chaxun()
    {
        //LaoBeng1h.SetActive(true);
        LaoBeng1h.transform.position = new Vector3(492, 590, -0);
        string[] s2String =  s2.Split(' ');
        new ZhanShi().ZhanShiModelByDeviceId(s2String[0]);
    }
}