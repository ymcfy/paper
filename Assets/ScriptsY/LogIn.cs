using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{

    //用户名
    public InputField username;
    //用户密码
    public InputField password;
    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    private static readonly string sqlAddress = SqlConstant.sqlAddress;
    public GameObject rawImageUser;
    public GameObject rawImageUserAdmin;
    //用户角色选择
    public Dropdown roleDropDown;

    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);

    /// <summary>
    /// 获取用户选择的角色
    /// </summary>
    /// <returns></returns>
    public string roleSelected()
    {
        //获取用户角色下拉表的信息
        string role = roleDropDown.captionText.text;
        Debug.Log(role);
        if (role == "管理员")
        {
            role = "admin";
        }
        else
        {
            role = "user";
        }
        return role;
    }
    /// <summary>
    /// 跟roleControl脚本结合使用
    /// </summary>
    public void login()
    {
        //获取用户角色
        string role = roleSelected();
        //获取用户名
        string user = Convert.ToString(username.text);
        //获取用户密码
        string pwd = Convert.ToString(password.text);
        Debug.Log(user + "  " + pwd + "  " + role);
        sqlCon.Open();
        string strJudgeSql = "select * from [SqlTest].[dbo].[user_role] where username = '" + user + "' and password = '" + pwd + "' and role = '" + role + "'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

        //数据存储表
        DataTable sqlDataTable = new DataTable();

        //执行查询
        sqlAdapter.Fill(sqlDataTable);

        if (sqlDataTable.Rows.Count > 0)
        {

            if (sqlDataTable.Rows[0][3].ToString() == "user")
            {
                string path = Application.dataPath + "/role.txt";
                //Debug.Log(path);
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine("user");
                sw.Close();
                //rawImageUser.SetActive(true);
                SceneManager.LoadScene("1");

            }
            else
            {
                string path = Application.dataPath + "/role.txt";
                //Debug.Log(path);
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine("admin");
                sw.Close();
                //rawImageUser.SetActive(true);
                SceneManager.LoadScene("1");
            }
            //数据库连接关闭
            sqlCon.Close();

            //GameObject.Find("Canvas/RawImage").SetActive(true);

        }
        else
        {
            Debug.Log("登录失败");
            //数据库连接关闭
            sqlCon.Close();
        }
    }

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }
}
