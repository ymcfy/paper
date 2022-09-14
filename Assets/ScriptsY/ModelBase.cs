using System.Data;
using System.Data.SqlClient;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ModelBase : MonoBehaviour
{

    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    private static readonly string sqlAddress = SqlConstant.sqlAddress;
    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);
    //模型库界面content
    public GameObject modelBaseContent;
    //模型库
    public GameObject modelBase;
    //模型库的scrollview
    public GameObject modelScrollView;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    private static byte[] getImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];//给imgByte空间
        files.Read(imgByte, 0, imgByte.Length);
        //将files中的信息读进imgByte中,public override int Read(byte[] array, int offset, int count);
        files.Close();//关闭文件流
        return imgByte;
    }

    /// <summary>
    /// 展示所有模型
    /// </summary>
    public void showAllModel()
    {
        //展示右侧模型库UI
        modelBase.SetActive(true);
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        //连接数据库
        //获取图片路径与prefab路径
        //将图片以button形式展示在modelBase上
        //双击可以在固定位置生成

        //开启数据库
        sqlCon.Open();
        //模糊查询      
        string sql = "select * from Model ";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //根据文件数量设置图片滚动框高度
        modelBaseContent.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, sqlDataTable.Rows.Count * 130);
        ////定义一个i，用来命名button，i递增
        int i = 1;
        

        
        //有隐患，可能会产生一批没有被销毁的对象
        modelScrollView.GetComponent<ScrollRect>().content.DetachChildren();
        
        foreach (DataRow row in sqlDataTable.Rows)
        {
            Texture2D tx = new Texture2D(100, 100);
            string imgePath = Application.dataPath + "//Resources//ModelBase//ModelImage//" + row["modelImage"].ToString();
            tx.LoadImage(getImageByte(imgePath));//tx接收byte数据转化为data
                                                 //images.Add(tx);
                                                 //新建button
            GameObject button = new GameObject("Button" + i, typeof(Button), typeof(RectTransform), typeof(Image));
            Sprite sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector2.zero);
            button.GetComponent<Image>().sprite = sprite;
            //新建text 存储文件名
            GameObject text = new GameObject("Text", typeof(Text));
            text.transform.position = new Vector3(0, -126, 0);
            //新建text1 存储文件路径
            GameObject text1 = new GameObject("Text1", typeof(Text));
            GameObject text2 = new GameObject("Text2", typeof(Text));
            //设置button的父物体
            button.transform.SetParent(modelBaseContent.transform);
            //设置text的父物体
            text.transform.SetParent(GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content/Button" + i).transform);
            //设置text1的父物体
            text1.transform.SetParent(GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content/Button" + i).transform);
            text2.transform.SetParent(GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content/Button" + i).transform);
            //text存储文件名
            text.GetComponent<Text>().text = row["modelName"].ToString();
            //text1存储文件路径
            text1.GetComponent<Text>().text = row["modelImage"].ToString();
            text2.GetComponent<Text>().text = Application.dataPath + "//ModelBase//" + row["modelPath"].ToString();
            //改变text的字体大小
            text.GetComponent<Text>().fontSize = 0;
            //改变text的字体颜色
            text.GetComponent<Text>().color = Color.red;
            //text2.GetComponent<Text>().color = Color.red;
            //给text添加点击事件，使得点击可以打开文件
            GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content/Button" + i).AddComponent<ModelClickHandler>();
            //GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i).GetComponent<Button>().onClick.AddListener(clickHandler.OnClick);
            //给text添加test1脚本用来修改font
            GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content/Button" + i + "/Text").AddComponent<test1>();
            //GameObject.Find("Canvas3/照片/Viewport/Content/Text" + i).AddComponent<test1>();
            //GameObject.Find("Canvas3/照片/Viewport/Content/Text" + i).transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30);
            //button命名变量自增
            i++;
        }
        sqlCon.Close();

    }

    public void searchModel(string modelName)
    {
        //开启数据库
        sqlCon.Open();
        //模糊查询
        string sql = "select * from Model where modelName like '%" + modelName + "%'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //根据文件数量设置图片滚动框高度
        GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content").transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, sqlDataTable.Rows.Count * 130);
        for (int j = 0; j < GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content").transform.childCount; j++)
        {
            GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content").transform.GetChild(j).gameObject.SetActive(false);
        }
        foreach (DataRow row in sqlDataTable.Rows)
        {
            for (int j = 0; j < GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content").transform.childCount; j++)
            {
                if (GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content").transform.GetChild(j).transform.
                    GetChild(0).GetComponent<Text>().text == row["modelName"].ToString() )
                {
                    GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content").transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        sqlCon.Close();
    }
}
