using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 为文件显示出的按钮添加的点击事件
/// </summary>
public class ClickHandler : MonoBehaviour
{
    Button btn;
    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    private static string sqlAddress = SqlConstant.sqlAddress;
    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);
    private static string HouZhuiMing;
    private static string fileName;
    private static string fileNameWithoutHouZhuiMing;
    private bool doubleClickBool;
    //默认双击时间间隔
    public float doubleClickInterval = 1.5f;
    //是否有一次单击
    private bool hasOneClick = false;
    //计时器
    private float timer = 0;
    public GameObject PlaneVideo;
    //右下角坐标
    public Vector3 BottomRightCorner;
    //右下角外面坐标
    public Vector3 OutSideBottomRightCorner;
    //正中间坐标
    public Vector3 Center;
    /// <summary>
    /// 照片的content
    /// </summary>
    public GameObject PictureContent;
    /// <summary>
    /// 视频的content
    /// </summary>
    public GameObject VideoContent;
    /// <summary>
    /// 文件的content
    /// </summary>
    public GameObject FileContent;
    /// <summary>
    /// 添加点击事件
    /// </summary>
    void Start()
    {
        //获得button属性
        btn = this.GetComponent<Button>();
        //添加点击事件所挂接的函数
        btn.onClick.AddListener(OnClick);
        RectTransform rt = this.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 100);
        //右下角坐标
        BottomRightCorner = new Vector3(1865, 50, 600);
        //正中间坐标
        Center = new Vector3(954, 640, -100f);
        //右下角外面坐标
        OutSideBottomRightCorner = new Vector3(2310, 50, 600);
    }
    void Update()
    {
        if (hasOneClick == true)
        {
            timer += Time.deltaTime;
            if (timer > doubleClickInterval)
            {
                hasOneClick = false;
                timer = 0;
            }
        }
    }
    public void OneClick()
    {
        foreach (Transform E in GameObject.Find("Canvas3/照片/Viewport/Content").transform.GetComponentsInChildren<Transform>())
        {
            if (E.name.Contains("Button"))
            {
                E.gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.white;
            }
        }
        foreach (Transform E in GameObject.Find("Canvas3/视频/Viewport/Content").transform.GetComponentsInChildren<Transform>())
        {
            if (E.name.Contains("Button"))
            {
                E.gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.white;
            }
        }
        foreach (Transform E in GameObject.Find("Canvas3/文件/Viewport/Content").transform.GetComponentsInChildren<Transform>())
        {
            if (E.name.Contains("Button"))
            {
                E.gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.white;
            }
        }
        //读取当前挂接物体的所有子物体
        foreach (Transform T in this.GetComponentsInChildren<Transform>())
        {
            //如果子物体的名字为Text1，那么就可以用超链接打开，因为Text1里面保存着文件路径
            if (T.name == "Text1")
            {
                //打开文件
                HouZhuiMing = T.GetComponent<Text>().text;
            }
            if (T.name == "Text")
            {
                //打开文件
                fileName = T.GetComponent<Text>().text;
            }
            if (T.name == "Text2")
            {
                //打开文件
                fileNameWithoutHouZhuiMing = T.GetComponent<Text>().text;
            }
        }
        btn.GetComponent<Image>().color = Color.green;
    }
    public void DoubleClick()
    {
        ////读取当前挂接物体的所有子物体
        //foreach (Transform T in this.GetComponentsInChildren<Transform>())
        //{
        //    //如果子物体的名字为Text1，那么就可以用超链接打开，因为Text1里面保存着文件路径
        //    if (T.name == "Text1")
        //    {
        //        //打开文件
        //        HouZhuiMing = T.GetComponent<Text>().text;
        //    }
        //    if (T.name == "Text")
        //    {
        //        //打开文件
        //        fileName = T.GetComponent<Text>().text;
        //    }
        //    if (T.name == "Text2")
        //    {
        //        //打开文件
        //        fileNameWithoutHouZhuiMing = T.GetComponent<Text>().text;
        //    }
        //}
        OpenFile();
    }
    public void ShanChuFile()
    {
        string path;
        string clazz = "";
        //HouZhuiMing为文件后缀名，根据后缀名判断文件类型
        if (HouZhuiMing == ".bmp" || HouZhuiMing == ".BMP" || HouZhuiMing == ".jpg" || HouZhuiMing == ".JPG"
            || HouZhuiMing == ".jpeg" || HouZhuiMing == ".JPEG" || HouZhuiMing == ".png" || HouZhuiMing == ".PNG"
            || HouZhuiMing == ".gif" || HouZhuiMing == ".GIF" || HouZhuiMing == ".psd" || HouZhuiMing == ".PSD"
            || HouZhuiMing == ".tiff" || HouZhuiMing == ".TIFF" || HouZhuiMing == ".ai" || HouZhuiMing == ".AI"
            || HouZhuiMing == ".eps" || HouZhuiMing == ".EPS" || HouZhuiMing == ".svg" || HouZhuiMing == ".SVG"
            || HouZhuiMing == ".cr2" || HouZhuiMing == ".CR2" || HouZhuiMing == ".nef" || HouZhuiMing == ".NEF"
            || HouZhuiMing == ".dng" || HouZhuiMing == ".DNG" || HouZhuiMing == ".jiff" || HouZhuiMing == ".JIFF")
        {
            clazz = "img";
            path = Application.dataPath + "//Resources//folder//img//" + fileName;
        }
        else if (HouZhuiMing == ".mp4" || HouZhuiMing == ".m4v" || HouZhuiMing == ".mov" || HouZhuiMing == ".qt"
            || HouZhuiMing == ".avi" || HouZhuiMing == ".flv" || HouZhuiMing == ".wmv" || HouZhuiMing == ".asf"
            || HouZhuiMing == ".mpeg" || HouZhuiMing == ".mpg" || HouZhuiMing == ".vob" || HouZhuiMing == ".mkv"
            || HouZhuiMing == ".asf" || HouZhuiMing == ".rm" || HouZhuiMing == ".rmvb" || HouZhuiMing == ".vob"
            || HouZhuiMing == ".ts" || HouZhuiMing == ".dat")
        {
            clazz = "video";
            path = Application.dataPath + "//Resources//folder//video//" + fileName;
        }
        else
        {
            clazz = "file";
            path = Application.dataPath + "//Resources//folder//file//" + fileName;
        }
        //开启数据库
        sqlCon.Open();
        string MyDelete = "delete from [SqlTest].[dbo].[" + clazz + "] where name = '" + fileNameWithoutHouZhuiMing + "'";
        SqlCommand sqlCommand = new SqlCommand(MyDelete, sqlCon);
        sqlCommand.ExecuteNonQuery();
        sqlCon.Close();
        File.Delete(path);

        if (clazz == "img")
        {
            new ZhanShi().ZhanShiTupian();
        }
        else if (clazz == "file")
        {
            new ZhanShi().ZhanShiWenDang();
        }
        else if (clazz == "video")
        {
            new ZhanShi().ZhanShiShiPin();
        }
    }
    public void OpenFile()
    {
        string path;
        //HouZhuiMing为文件后缀名，根据后缀名判断文件类型
        if (HouZhuiMing == ".bmp" || HouZhuiMing == ".BMP" || HouZhuiMing == ".jpg" || HouZhuiMing == ".JPG"
            || HouZhuiMing == ".jpeg" || HouZhuiMing == ".JPEG" || HouZhuiMing == ".png" || HouZhuiMing == ".PNG"
            || HouZhuiMing == ".gif" || HouZhuiMing == ".GIF" || HouZhuiMing == ".psd" || HouZhuiMing == ".PSD"
            || HouZhuiMing == ".tiff" || HouZhuiMing == ".TIFF" || HouZhuiMing == ".ai" || HouZhuiMing == ".AI"
            || HouZhuiMing == ".eps" || HouZhuiMing == ".EPS" || HouZhuiMing == ".svg" || HouZhuiMing == ".SVG"
            || HouZhuiMing == ".cr2" || HouZhuiMing == ".CR2" || HouZhuiMing == ".nef" || HouZhuiMing == ".NEF"
            || HouZhuiMing == ".dng" || HouZhuiMing == ".DNG" || HouZhuiMing == ".jiff" || HouZhuiMing == ".JIFF")
        {
            path = Application.dataPath + "//Resources//folder//img//" + fileName;
            Application.OpenURL(path);
        }
        else if (HouZhuiMing == ".mp4" || HouZhuiMing == ".m4v" || HouZhuiMing == ".mov" || HouZhuiMing == ".qt"
            || HouZhuiMing == ".avi" || HouZhuiMing == ".flv" || HouZhuiMing == ".wmv" || HouZhuiMing == ".asf"
            || HouZhuiMing == ".mpeg" || HouZhuiMing == ".mpg" || HouZhuiMing == ".vob" || HouZhuiMing == ".mkv"
            || HouZhuiMing == ".asf" || HouZhuiMing == ".rm" || HouZhuiMing == ".rmvb" || HouZhuiMing == ".vob"
            || HouZhuiMing == ".ts" || HouZhuiMing == ".dat")
        {
            path = Application.dataPath + "//Resources//folder//video//" + fileName;
            //39.8f, 12.7f, -100f
            //GameObject.Find("Canvas3/Plane").GetComponent<UnityEngine.Video.VideoPlayer>().GetComponent<Transform>().transform.position = new Vector3(39.8f, 12.7f, -100f);
            //GameObject.Find("Canvas3/Plane").GetComponent<UnityEngine.Video.VideoPlayer>().url = path;
            //GameObject.Find("Canvas3/视频").transform.position = new Vector3(2883, 999, 600);
            //GameObject.Find("Canvas3/退出/视频退出").transform.position = OutSideBottomRightCorner;
            //GameObject.Find("Canvas3/退出/视频播放退出").transform.position = BottomRightCorner;
            Application.OpenURL(path);
        }
        else
        {
            path = Application.dataPath + "//Resources//folder//file//" + fileName;
            Application.OpenURL(path);
        }
        Debug.Log(path);
        //PlaneVideo.GetComponent<UnityEngine.Video.VideoPlayer>().gameObject.SetActive(true);
    }
    /// <summary>
    /// 点击事件所在函数 点击即可打开文件
    /// 注：如果需要传参，可以使用delegate
    /// </summary>
    public void OnClick()
    {
        if (hasOneClick == false)
        {
            hasOneClick = true;
            OneClick();
        }
        else
        {
            if (timer < doubleClickInterval)
            {
                hasOneClick = false;
                DoubleClick();
                timer = 0;
            }
            else
            {
                timer = 0;
                OneClick();
            }
        }
    }
}
