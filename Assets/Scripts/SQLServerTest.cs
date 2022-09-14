using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Data.SqlClient;
public class SQLServerTest : MonoBehaviour
{
    SqlConnection con = null;
    SqlDataAdapter sda = null;
    private string str;
    // Use this for initialization
    void Start()
    {
        string s = @"server=222.195.148.180;database=yangpu;uid=web;pwd=web";    //注意，这里必须使用SQL Server和Windows验证模式，否则会报错
        //con = new SqlConnection(s);
        //con.Open();
        //string sql = "select * from shebei";
        //sda = new SqlDataAdapter(sql, con);
        //DataSet ds = new DataSet();
        //sda.Fill(ds, "shebei");
        //表名为shebei
        SqlConnection con = new SqlConnection(s);
        SqlCommand cmd = new SqlCommand("select * from shebei", con);   //创建SqlCommand对象，并指定其使用con连接数据库  
        SqlDataAdapter sda = new SqlDataAdapter();                      //创建SqlDataAdapter对象  
        sda.SelectCommand = cmd;                                        //指定Command  
        DataSet ds = new DataSet();                                     //创建DataSet对象,实例化数据集，并写入查询到的数据  
        sda.Fill(ds);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {

            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {
                str += ds.Tables[0].Rows[i][j].ToString().Trim() + "    ";
                if (j == ds.Tables[0].Columns.Count - 1)
                {
                    print(str);
                    str = "";
                }
            }
        }
        //print(ds.Tables[0].Rows[0][0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
