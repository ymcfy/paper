using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;

public class sqltest : MonoBehaviour
{

    //数据库连接的定义
    private SqlConnection sqlCon;
    //数据库连接地址
    private string sqlAddress = SqlConstant.sqlAddress;
    //适配器
    SqlDataAdapter sda = null;

    // Use this for initialization
    void Start()
    {
        //传建一个数据库连接事件
        sqlCon = new SqlConnection(sqlAddress);
    }

    // Update is called once per frame
    void Update()
    {
        //按下空格键，执行对应操作
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space down");
            try
            {
                //打开连接
                sqlCon.Open();
                //连接成功
                Debug.Log("Yes");
                //数据库操作语句
                string sql = "select * from youkuchangqu.momo";
                //数据库操作
                sda = new SqlDataAdapter(sql, sqlAddress);
                //结果集
                DataSet ds = new DataSet();
                //将查询的结果放入结果集
                sda.Fill(ds, "youkuchangqu.momo");
                //打印结果
                print(ds.Tables[0].Rows[0][0]);
            }

            //如果出现异常，抛出
            catch (System.Exception)
            {
                Debug.Log("No");
                throw;
            }

        }
        //空格键抬起，数据库连接关闭
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("space up");
            sqlCon.Close();
        }
    }
}