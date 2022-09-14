using System.Data;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 高亮查询，用于在模型中查询某一模型，并进行高亮显示
/// 挂接在第一模块的kongzhi上
/// 入口为第一模块的设备查询的根据名称按钮
/// </summary>
public class HightLightSelection : MonoBehaviour
{
    //查询框，对应第一模块Canvas下的RawImageHightLightSelection
    public GameObject RawImageHightLightSelection;
    //用于读取每一个物体
    private readonly GameObject[] obj; //开头定义GameObject数组
    //站台
    public GameObject Station;
    //管线
    public GameObject PipeLine;
    //是否存在提示
    public Text isExistText;
    //主摄像机
    public GameObject maincamera;
    //实例化Add脚本对象
    public Add add = new Add();

    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    private static readonly string sqlAddress = SqlConstant.sqlAddress;
    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);

    /// <summary>
    /// 搜索结果下拉框
    /// </summary>
    public GameObject RawImageHightLightSelectionScrollView;

    /// <summary>
    /// 搜索高亮显示滚动框
    /// </summary>
    public GameObject RawImageHightLightSelectionContent;

    

    // Use this for initialization
    private void Start()
    {
    }
    // Update is called once per frame
    private void Update()
    {
    }
    ///// <summary>
    ///// 在点击第一模块的设备查询的根据名称按钮后，调用此方法，显示查询框
    ///// </summary>
    //public void ShowSelectionInterFace()
    //{
    //    RawImageHightLightSelection.SetActive(true);
    //}

    /// <summary>
    /// 根据模型名字，从数据库查出id来
    /// </summary>
    /// <returns></returns>
    //public string[] nameToId() {

    //}

    //第一步根据名字在设备表里模糊查找，找到相应的名字，保存在list里
    //并且展示在scroll view里
    public void queryName() {
        //初始化无此模型提示Text
        isExistText.text = "";

        //设置待查询模型初始为不存在状态
        bool isExist = false;
        //搜索模型名
        string SelectionText = HightLightSelectionMonitor.endValue;
        Debug.Log("液化气装卸站台" + SelectionText);
        sqlCon.Open();
        string strJudgeSql = "select EquipmentName,DeviceId from [SqlTest].[dbo].[Equipment] where EquipmentName like '%" + SelectionText + "%' ";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(strJudgeSql, sqlCon);

        //数据存储表
        DataTable sqlDataTable = new DataTable();

        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        if (sqlDataTable.Rows.Count > 0)
        {
            //显示搜索结果下拉框
            RawImageHightLightSelectionScrollView.SetActive(true);


            ////定义一个i，用来命名button，i递增
            int i = 1;

        //根据文件数量设置高亮显示滚动框高度
        RawImageHightLightSelectionContent.transform.GetComponent<RectTransform>().
            sizeDelta = new Vector2(0, (sqlDataTable.Rows.Count ) * 60 + 30);
        RawImageHightLightSelectionScrollView.GetComponent<ScrollRect>().content.DetachChildren();
        //for (int j = 0; j < RawImageHightLightSelectionContent.transform.childCount; j++)
        //{
        //    RawImageHightLightSelectionContent.transform.GetChild(j).gameObject.SetActive(false);
        //}
        foreach (DataRow row in sqlDataTable.Rows)
        {
            GameObject button = new GameObject("Button" + i, typeof(Button), typeof(RectTransform), typeof(Image));
            //button.transform.localScale
            //Sprite sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector2.zero);
            //button.GetComponent<Image>().sprite = sprite;
            //设置button的父物体
            button.transform.SetParent(RawImageHightLightSelectionContent.transform);
            //新建text 存储文件名
            GameObject text = new GameObject("Text", typeof(Text));
            text.transform.position = new Vector3(0, -59, 0);
            //设置text的父物体
            text.transform.SetParent(GameObject.Find("Canvas/RawImageHightLightSelection/Scroll View/Viewport/Content/Button" + i).transform);
            //text存储文件名
            text.GetComponent<Text>().text = row["EquipmentName"].ToString();
            //改变text的字体大小
            text.GetComponent<Text>().fontSize = 20;
            text.GetComponent<Text>().color = new Color(68,11,255);
            //改为overflow模式，使得字体可以伸展在一行
            text.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;

            //新建一个text1存储设备id
            GameObject text1 = new GameObject("Text1", typeof(Text));
            text1.transform.position = new Vector3(0, -59, 0);
            //设置text的父物体
            text1.transform.SetParent(GameObject.Find("Canvas/RawImageHightLightSelection/Scroll View/Viewport/Content/Button" + i).transform);
            //text存储文件名
            text1.GetComponent<Text>().text = row["DeviceId"].ToString();
            //改变text的字体大小   应该不能显示
            text1.GetComponent<Text>().fontSize = 20;
            text1.GetComponent<Text>().color = new Color(68, 11, 255);

            //text.transform.localScale.
            //给text添加test1脚本用来修改font
            GameObject.Find("Canvas/RawImageHightLightSelection/Scroll View/Viewport/Content/Button" + i + "/Text").AddComponent<test1>();
            GameObject.Find("Canvas/RawImageHightLightSelection/Scroll View/Viewport/Content/Button" + i).AddComponent<EqupmentQueryClickHandler>();
            i++;
        }
        sqlCon.Close();
        }
        else
        {
            //如果设备不存在
            isExistText.text = "无此设备！";
            //即使不存在也要关闭数据库连接
            sqlCon.Close();
        }
    }

    /// <summary>
    /// 进行高亮搜索   string SelectionText
    /// </summary> 
    /// <param name="SelectionText"></param>
    public void Selection()
    {
        //此处留一个接口，既可以按照模型唯一标识进行查找，也可以按照名字，然后从数据库进行查找
        
        

        //第二步用户选择准确的名字，在设备表里根据名字查询id，并且在系统里高亮
        
        //设置待查询模型初始为不存在状态
        bool isExist = false;
        //搜索模型名
        string SelectionText = HightLightSelectionMonitor.endValue;
        Debug.Log("液化气装卸站台" + SelectionText);
        //站台所有子物体
        Transform[] StationGameObjects = Station.GetComponentsInChildren<Transform>();
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
            if (obj.name.Contains(SelectionText))
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
                if (GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<Cube>() != null)
                {
                    GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<Cube>().enabled = true;
                }
                else
                {
                    //如果Cube脚本尚未存在，那么就添加这个脚本
                    GameObject.Find("液化气装卸站台" + SelectionText).AddComponent<Cube>();
                }
                //如果Cube脚本已经存在 
                if (GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<HighlightableObject>() != null)
                {
                    GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<HighlightableObject>().enabled = true;
                }
                else
                {
                    //如果Cube脚本尚未存在，那么就添加这个脚本
                    GameObject.Find("液化气装卸站台" + SelectionText).AddComponent<HighlightableObject>();
                }
                //GameObject.Find("Main Camera").GetComponent<SearchMouseHightLight>().enabled = true;
                //如果SpectrumController脚本已经存在 
                if (GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<SpectrumController>() != null)
                {
                    GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<SpectrumController>().enabled = true;
                }
                else
                {
                    //如果SpectrumController脚本尚未存在，那么就添加这个脚本
                    GameObject.Find("液化气装卸站台" + SelectionText).AddComponent<SpectrumController>();
                }
                //获取待查询模型的坐标
                Vector3 position = GameObject.Find("液化气装卸站台" + SelectionText).transform.position;
                //根据待查询模型的坐标确定相机的坐标
                maincamera.transform.position = new Vector3(position.x + 7, position.y + 1.6f, position.z);
                //确定相机角度
                maincamera.transform.rotation = Quaternion.Euler(0.0f, -90f, 0.0f);
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
        Transform[] PipeLineGameObjects = PipeLine.GetComponentsInChildren<Transform>();
        foreach (Transform obj in PipeLineGameObjects)
        {
            if (obj.name.Equals(SelectionText))
            {
                GameObject.Find("液化气装卸站台管线" + SelectionText).AddComponent<SpectrumController>();
                Vector3 position = GameObject.Find("液化气装卸站台管线" + SelectionText).transform.position;
                maincamera.transform.position = new Vector3(position.x + 7, position.y + 1.6f, position.z);
                maincamera.transform.rotation = Quaternion.Euler(0.0f, -90f, 0.0f);
                isExist = true;
            }
        }
        //如果待查询模型为不存在状态，显示无此模型
        if (isExist == false)
        {
            isExistText.text = "无此模型";
        }
        //判断是管线还是站台  ok
        //镜头对准  OK
        //如果镜头不合适，再加一个重置视角
        //或者加一个镜头对准功能
        //点击即可查看详细信息
        //再次搜索时要去掉高亮
        //去掉QWER这种移动
    }
    /// <summary>
    /// 挂接在第一模块的手动搜索模型的退出按钮上
    /// 实现退出手动搜索
    /// </summary>
    public void ExitManualSearch()
    {
        isExistText.text = "";
        RawImageHightLightSelection.SetActive(false);
        string SelectionText = HightLightSelectionMonitor.endValue;
        //add.CloseCubeScript();
        //add.CloseHightLight();
        if (GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<Cube>() != null)
        {
            GameObject.Find("液化气装卸站台" + SelectionText).GetComponent<Cube>().enabled = false;
        }
        if (GameObject.Find("Main Camera").GetComponent<HighlightingEffect>() != null)
        {
            GameObject.Find("Main Camera").GetComponent<HighlightingEffect>().enabled = false;
        }
    }
}
