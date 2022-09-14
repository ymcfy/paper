using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;

public class EqupmentQueryClickHandler : MonoBehaviour {

    /// <summary>
    /// 当前脚本是挂在每一个模糊搜索到的设备button上
    /// 1.双击当前button
    /// 2.隐藏高亮搜索窗口
    /// 3.获取当前按钮的name
    /// 4.根据当前name查id
    /// 4.根据id高亮相应物体
    /// </summary>
    /// 

    Button btn;
    //是否有一次单击
    private bool hasOneClick = false;
    //计时器
    private float timer = 0;
    //默认双击时间间隔
    public float doubleClickInterval = 1.5f;
    //查询框，对应第一模块Canvas下的RawImageHightLightSelection
    public GameObject RawImageHightLightSelection;

    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    private static readonly string sqlAddress = SqlConstant.sqlAddress;
    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);

    // Use this for initialization
    void Start () {
        //获得button属性
        btn = this.GetComponent<Button>();
        //添加点击事件所挂接的函数
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (hasOneClick == false)
        {
            hasOneClick = true;
            //OneClick();
        }
        else
        {
            if (timer < doubleClickInterval)
            {
                hasOneClick = false;
                //DoubleClick();
                
                //获取text
                //btn.GetComponent<>
                //读取当前挂接物体的所有子物体
                foreach (Transform T in this.GetComponentsInChildren<Transform>())
                {
                     if (T.name == "Text1")
                    {
                        //打开文件
                        string name = T.GetComponent<Text>().text;
                        //Debug.Log(name);
                        //string id =  nameToId(name);
                        //Debug.Log(id);
                        //高亮显示
                        Selection(name);
                    }
                }
                timer = 0;
                GameObject.Find("Canvas/RawImageHightLightSelection").SetActive(false);
            }
            else
            {
                timer = 0;
                //OneClick();
            }
        }
    }

    public string nameToId(string text) {
        sqlCon.Open();
        string strJudgeSql = "select DeviceId from [SqlTest].[dbo].[Equipment] where EquipmentName = '" + text + "' ";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

        //数据存储表
        DataTable sqlDataTable = new DataTable();

        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        string id = "111";
        foreach (DataRow row in sqlDataTable.Rows)
        {
           //text存储文件名
            id =  row["DeviceId"].ToString();
        }
        sqlCon.Close();
        return id;
    }

    /// <summary>
    /// 进行高亮搜索   string SelectionText
    /// </summary> 
    /// <param name="SelectionText"></param>
    public void Selection(string text)
    {
        //此处留一个接口，既可以按照模型唯一标识进行查找，也可以按照名字，然后从数据库进行查找



        //第二步用户选择准确的名字，在设备表里根据名字查询id，并且在系统里高亮
        Debug.Log(text+"111");
        //设置待查询模型初始为不存在状态
        bool isExist = false;
        //搜索模型名
        //string SelectionText = HightLightSelectionMonitor.endValue;
        //Debug.Log("液化气装卸站台" + SelectionText);
        //站台所有子物体
        Transform[] StationGameObjects = GameObject.Find("液化气装卸站台").GetComponentsInChildren<Transform>();
        //遍历站台所有子物体
        foreach (Transform obj in StationGameObjects)
        {
            //把所有子物体的SpectrumController关掉
            //如果SpectrumController脚本已经存在 
            if (obj.GetComponent<SpectrumController>() != null)
            {
                obj.GetComponent<SpectrumController>().enabled = false;
            }
            if (obj.GetComponent<HighlightableObject>() != null)
            {
                obj.GetComponent<HighlightableObject>().enabled = false;
            }
            //如果该子物体与待搜索物体名称相同  此处改为包含  应该显示一个列表
            if (obj.name.Equals(text))
            {
                //如果HighlightingEffect脚本已经存在   
                if (GameObject.Find("Main Camera").GetComponent<HighlightingEffect>() != null)
                {
                    GameObject.Find("Main Camera").GetComponent<HighlightingEffect>().enabled = true;
                }
                else
                {
                    //如果HighlightingEffect脚本尚未存在，那么就添加这个脚本
                    GameObject.Find("Main Camera").AddComponent<HighlightingEffect>();
                }
                //如果Cube脚本已经存在 
                if (GameObject.Find("液化气装卸站台" + text).GetComponent<Cube>() != null)
                {
                    GameObject.Find("液化气装卸站台" + text).GetComponent<Cube>().enabled = true;
                }
                else
                {
                    //如果Cube脚本尚未存在，那么就添加这个脚本
                    GameObject.Find("液化气装卸站台" + text).AddComponent<Cube>();
                }
                //如果Cube脚本已经存在 
                if (GameObject.Find("液化气装卸站台" + text).GetComponent<HighlightableObject>() != null)
                {
                    GameObject.Find("液化气装卸站台" + text).GetComponent<HighlightableObject>().enabled = true;
                }
                else
                {
                    //如果Cube脚本尚未存在，那么就添加这个脚本
                    GameObject.Find("液化气装卸站台" + text).AddComponent<HighlightableObject>();
                }
                //GameObject.Find("Main Camera").GetComponent<SearchMouseHightLight>().enabled = true;
                //如果SpectrumController脚本已经存在 
                if (GameObject.Find("液化气装卸站台" + text).GetComponent<SpectrumController>() != null)
                {
                    GameObject.Find("液化气装卸站台" + text).GetComponent<SpectrumController>().enabled = true;
                }
                else
                {
                    //如果SpectrumController脚本尚未存在，那么就添加这个脚本
                    GameObject.Find("液化气装卸站台" + text).AddComponent<SpectrumController>();
                }
                //获取待查询模型的坐标
                Vector3 position = GameObject.Find("液化气装卸站台" + text).transform.position;
                //根据待查询模型的坐标确定相机的坐标
                GameObject.Find("Main Camera").transform.position = new Vector3(position.x + 7, position.y + 1.6f, position.z);
                //确定相机角度
                GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(0.0f, -90f, 0.0f);
                //如果MeshCollider脚本已经存在
                //if (GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<MeshCollider>() != null)
                //{
                //    GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<MeshCollider>().enabled = true;
                //}
                //else
                //{
                //    //如果MeshCollider脚本尚未存在，那么就添加这个脚本
                //    GameObject.Find("液化气装卸站台" + SelectionText).AddComponent<MeshCollider>();
                //}
                //GameObject.Find("Main Camera").GetComponent<test123>().ZhanShiModel(SelectionText);
                //设置待查询模型为存在状态
                isExist = true;
            }
        }
        //Transform[] PipeLineGameObjects = PipeLine.GetComponentsInChildren<Transform>();
        //foreach (Transform obj in PipeLineGameObjects)
        //{
        //    if (obj.name.Equals(SelectionText))
        //    {
        //        GameObject.Find("液化气装卸站台管线" + SelectionText).AddComponent<SpectrumController>();
        //        Vector3 position = GameObject.Find("液化气装卸站台管线" + SelectionText).transform.position;
        //        maincamera.transform.position = new Vector3(position.x + 7, position.y + 1.6f, position.z);
        //        maincamera.transform.rotation = Quaternion.Euler(0.0f, -90f, 0.0f);
        //        isExist = true;
        //    }
        //}
        //如果待查询模型为不存在状态，显示无此模型
        if (isExist == false)
        {
            //isExistText.text = "无此模型";
        }
        //判断是管线还是站台  ok
        //镜头对准  OK
        //如果镜头不合适，再加一个重置视角
        //或者加一个镜头对准功能
        //点击即可查看详细信息
        //再次搜索时要去掉高亮
        //去掉QWER这种移动
    }

    // Update is called once per frame
    void Update () {
        
    }
}
