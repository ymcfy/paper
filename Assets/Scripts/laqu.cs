using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public class laqu : MonoBehaviour
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

    //下拉表单 
    public GameObject dropdown;
    public GameObject dropdown1;

    public GameObject LaoBeng1h;


    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    //127.0.0.1  127.0.0.1
    private static string sqlAddress = SqlConstant.sqlAddress;

    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);


    void Start()
    {


        //开启数据库
        sqlCon.Open();
        //数据库执行语句
        string sql1 = "select SheBeiMingCheng from SheBeiTest ";
        string sql2 = "select SheBeiMingCheng from SheBeiTest1 ";
        string sql3 = "select SheBeiMingCheng from SheBeiTest2 ";
        string sql4 = "select SheBeiMingCheng from SheBeiTest3 ";

        //检索保存数据
        SqlDataAdapter sqlAdapter1 = new SqlDataAdapter(sql1, sqlCon);
        SqlDataAdapter sqlAdapter2 = new SqlDataAdapter(sql2, sqlCon);
        SqlDataAdapter sqlAdapter3 = new SqlDataAdapter(sql3, sqlCon);
        SqlDataAdapter sqlAdapter4 = new SqlDataAdapter(sql4, sqlCon);

        //数据存储表
        DataTable sqlDataTable1 = new DataTable();
        DataTable sqlDataTable2 = new DataTable();
        DataTable sqlDataTable3 = new DataTable();
        DataTable sqlDataTable4 = new DataTable();

        //执行查询
        sqlAdapter1.Fill(sqlDataTable1);
        sqlAdapter2.Fill(sqlDataTable2);
        sqlAdapter3.Fill(sqlDataTable3);
        sqlAdapter4.Fill(sqlDataTable4);


        Dropdown.OptionData Ddt1 = new Dropdown.OptionData();
        Ddt1.text = "请选择消防设备";
        Dd1.Add(Ddt1);

        Dropdown.OptionData Ddt2 = new Dropdown.OptionData();
        Ddt2.text = "请选择管线";
        Dd2.Add(Ddt2);

        Dropdown.OptionData Ddt3 = new Dropdown.OptionData();
        Ddt3.text = "请选择汽车";
        Dd3.Add(Ddt3);

        Dropdown.OptionData Ddt4 = new Dropdown.OptionData();
        Ddt4.text = "请选择阀门";
        Dd4.Add(Ddt4);


        //获取每一行数据
        foreach (DataRow row in sqlDataTable1.Rows)
        {
            Dropdown.OptionData Dt1 = new Dropdown.OptionData();
            Dt1.text = row[0].ToString();
            Dd1.Add(Dt1);
        }

        //获取每一行数据
        foreach (DataRow row in sqlDataTable2.Rows)
        {
            Dropdown.OptionData Dt2 = new Dropdown.OptionData();
            Dt2.text = row[0].ToString();
            Dd2.Add(Dt2);
        }

        //获取每一行数据
        foreach (DataRow row in sqlDataTable3.Rows)
        {
            Dropdown.OptionData Dt3 = new Dropdown.OptionData();
            Dt3.text = row[0].ToString();
            Dd3.Add(Dt3);
        }

        //获取每一行数据
        foreach (DataRow row in sqlDataTable4.Rows)
        {
            Dropdown.OptionData Dt4 = new Dropdown.OptionData();
            Dt4.text = row[0].ToString();
            Dd4.Add(Dt4);
        }

        sqlCon.Close();

    }

    void Update()
    {
        s1 = dropdown.GetComponent<Dropdown>().captionText.text;
        s2 = dropdown1.GetComponent<Dropdown>().captionText.text;



        //if (s1 == "消防设备")
        //{
        //    Dropdown.OptionData Ddt1 = new Dropdown.OptionData();
        //    Ddt1.text = "请选择消防设备";
        //    Dd1.Add(Ddt1);
        //    dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd1);

        //}

        //if (s1 == "管线")
        //{
        //    Dropdown.OptionData Ddt2 = new Dropdown.OptionData();
        //    Ddt2.text = "请选择管线";
        //    Dd2.Add(Ddt2);
        //    dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd2);
        //}
        //if (s1 == "汽车")
        //{
        //    Dropdown.OptionData Ddt3 = new Dropdown.OptionData();
        //    Ddt3.text = "请选择汽车";
        //    Dd3.Add(Ddt3);
        //    dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd3);
        //}
        //if (s1 == "阀门")
        //{
        //    Dropdown.OptionData Ddt4 = new Dropdown.OptionData();
        //    Ddt4.text = "请选择阀门";
        //    Dd4.Add(Ddt4);
        //    dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd4);
        //}


    }





    public void xuanze() //选择模型类型
    {
        if (dropdown.GetComponent<Dropdown>().captionText.text == "消防设备")
        {
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd1);
            GameObject.Find("GameObject").GetComponent<kongzhi>().quanbu();

        }
        if (dropdown.GetComponent<Dropdown>().captionText.text == "管线")
        {
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd2);
            GameObject.Find("GameObject").GetComponent<kongzhi>().guanxian();

        }
        if (dropdown.GetComponent<Dropdown>().captionText.text == "汽车")
        {
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd3);
            GameObject.Find("GameObject").GetComponent<kongzhi>().quanbu();
        }
        if (dropdown.GetComponent<Dropdown>().captionText.text == "阀门")
        {
            dropdown1.GetComponent<Dropdown>().options = new List<Dropdown.OptionData>(Dd4);
            GameObject.Find("GameObject").GetComponent<kongzhi>().quanbu();
        }
    }
}