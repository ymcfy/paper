using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using Image = UnityEngine.UI.Image;
using Color = UnityEngine.Color;
using Assets.ScriptsY;
/// <summary>
/// 挂接 场景3 GameObject下
/// </summary>
public class ZhanShi : MonoBehaviour
{
    //数据库连接语句 .为项目所在服务器IP SqlTest为数据库名称 web为采用用户名登录方式的用户名 web为密码
    private static string sqlAddress = SqlConstant.sqlAddress;
    //进行连接
    private SqlConnection sqlCon = new SqlConnection(sqlAddress);
    //新建button
    //GameObject button;
    //新建text用来展示文件名
    //GameObject text;
    //新建text1用来存放文件路径
    //GameObject text1;
    //GameObject text2;
    //照片信息展示滚动框
    public ScrollRect zhaopian;
    //视频信息展示滚动框
    public ScrollRect shipin;
    //文件信息展示滚动框
    public ScrollRect wenjian;
    //当前文件下拉菜单
    public GameObject dangqianwenjian;
    private List<Texture2D> images = new List<Texture2D>();
    //public Sprite sprite;
    public GameObject RawImage_dropdownlist;
    public GameObject RawImage;
    public Dropdown modelChoose;
    public GameObject exit;
    public GameObject ModelDisplayBox;
    public ClickHandler clickHandler;
    //右下角坐标
    public Vector3 BottomRightCorner;
    //右下角外面坐标
    public Vector3 OutSideBottomRightCorner;
    //正中间坐标
    public Vector3 Center;
    // Use this for initialization
    void Start()
    {
        //ZhanShiTupian();
        clickHandler = new ClickHandler();
        //右下角坐标
        BottomRightCorner = new Vector3(1865, 50, 600);
        //正中间坐标
        Center = new Vector3(954, 640, -100f);
        //右下角外面坐标
        OutSideBottomRightCorner = new Vector3(2310, 50, 600);
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void exitModelSelection()
    {
        //模型选择移走
        modelChoose.transform.position = new Vector3(1464, 139, 600);
        //下拉框内容设为空
        //dangqianwenjian.captionText
        modelChoose.captionText.text = "";
        RawImage.SetActive(true);
    }
    public void ExitDangqianwenjian()
    {
        //当前文件移走
        //dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
        dangqianwenjian.transform.position = OutSideBottomRightCorner;
        //exit.transform.position = new Vector3(1080, 50, 600);
        exit.transform.position = OutSideBottomRightCorner;
        //下拉框内容设为空
        //dangqianwenjian.captionText
        //dangqianwenjian.captionText.text = "";
        RawImage.SetActive(true);
    }
    public void ZhanShiModel(string modelName)
    {
        //开启数据库
        sqlCon.Open();
        #region 数据库类型扩展内容
        //提高程序的可扩展性，防止不同模型的字段不同，根据模型类型搭建数据库
        //可尽量分类，某些模型不具备的字段可以设空，如果为“”那么就字段名字也不展示，如果null那么字段名字展示
        //可以进行提前判断，然后用总字段数减去ID减去ImgPath减去""字段数
        ////数据库类型
        //string ShuJuKu = "";
        ////判断数据库类型
        //if (type == "消防设备")
        //{
        //    ShuJuKu = "SheBeiTest";
        //}
        //else if (type == "管线")
        //{
        //    ShuJuKu = "SheBeiTest1";
        //}
        //else if (type == "汽车")
        //{
        //    ShuJuKu = "SheBeiTest2";
        //}
        //else if (type == "阀门")
        //{
        //    ShuJuKu = "SheBeiTest3";
        //}
        //数据库执行语句
        string sql = "select * from Equipment where DeviceId='" + modelName + "'";
        #endregion
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //获取每一行数据
        foreach (DataRow row in sqlDataTable.Rows)
        {
            //字段数减2，是减去ID与ImagePath   未来应改为减4，减去Path与SourcePath ，注意除ID外，均要放置在最后几列
            int lableNum = sqlDataTable.Columns.Count - 4;
            //如果lableNum <= 4，只放在第一行table中
            if (lableNum <= 4)
            {
                //对于该行的每一个数据
                for (int i = 1; i < 5; i++)
                {
                    //查询该字段对应的中文名
                    string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    //执行查询与保存
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    //数据存储表
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    //如果无数据，退出循环
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    //该行该列字段数据
                    string str = row[i].ToString();
                    //存储字段中文名的路径
                    string title = "Canvas3/详细信息/table/label" + i + "/title" + i;
                    //存储字段英文名的路径
                    string text = "Canvas3/详细信息/table/label" + i + "/text" + i;
                    //在title处存放字段的名称
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    //在text处存放字段的数据
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
            }
            else if (lableNum >= 5 && lableNum <= 8)
            {
                for (int i = 1; i < 5; i++)
                {
                    string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/详细信息/table/label" + i + "/title" + i;
                    string text = "Canvas3/详细信息/table/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
                for (int i = 5; i < 9; i++)
                {
                    string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/详细信息/table2/label" + i + "/title" + i;
                    string text = "Canvas3/详细信息/table2/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
            }
            else
            {
                for (int i = 1; i < 5; i++)
                {
                    string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    sqlAdapterAttr.Fill(sqlDataTableAttr);
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/详细信息/table/label" + i + "/title" + i;
                    string text = "Canvas3/详细信息/table/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
                for (int i = 5; i < 9; i++)
                {
                    string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    sqlAdapterAttr.Fill(sqlDataTableAttr);
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/详细信息/table2/label" + i + "/title" + i;
                    string text = "Canvas3/详细信息/table2/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
                for (int i = 9; i < 13; i++)
                {
                    string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/详细信息/table3/label" + i + "/title" + i;
                    string text = "Canvas3/详细信息/table3/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
            }
            string imgPath = Application.dataPath + "//Resources//folder//modelImg//" + row["ImgPath"].ToString();
            string rawImgPath = "Canvas3/详细信息/Image_text/RawImage";
            Texture2D tx = new Texture2D(220, 220);
            tx.LoadImage(getImageByte(imgPath));
            GameObject.Find(rawImgPath).GetComponent<RawImage>().texture = tx;
        }
        sqlCon.Close();
    }
    public void ZhanShiModelByDeviceId(string DeviceId)
    {
        //ModelDisplayBox.SetActive(true);
        //开启数据库
        sqlCon.Open();
        #region 数据库类型扩展内容
        //提高程序的可扩展性，防止不同模型的字段不同，根据模型类型搭建数据库
        //可尽量分类，某些模型不具备的字段可以设空，如果为“”那么就字段名字也不展示，如果null那么字段名字展示
        //可以进行提前判断，然后用总字段数减去ID减去ImgPath减去""字段数
        ////数据库类型
        //string ShuJuKu = "";
        ////判断数据库类型
        //if (type == "消防设备")
        //{
        //    ShuJuKu = "SheBeiTest";
        //}
        //else if (type == "管线")
        //{
        //    ShuJuKu = "SheBeiTest1";
        //}
        //else if (type == "汽车")
        //{
        //    ShuJuKu = "SheBeiTest2";
        //}
        //else if (type == "阀门")
        //{
        //    ShuJuKu = "SheBeiTest3";
        //}
        //数据库执行语句
        string sql = "select * from Equipment where DeviceId='" + DeviceId + "'";
        #endregion
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //获取每一行数据
        foreach (DataRow row in sqlDataTable.Rows)
        {
            //字段数减2，是减去ID与ImagePath   未来应改为减4，减去Path与SourcePath ，注意除ID外，均要放置在最后几列
            int lableNum = sqlDataTable.Columns.Count - 4;
            //如果lableNum <= 4，只放在第一行table中
            if (lableNum <= 4)
            {
                //对于该行的每一个数据
                for (int i = 1; i < 5; i++)
                {
                    //查询该字段对应的中文名
                    string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    //执行查询与保存
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    //数据存储表
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    //如果无数据，退出循环
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    //该行该列字段数据
                    string str = row[i].ToString();
                    //存储字段中文名的路径
                    string title = "Canvas3/ModelDetail/table/label" + i + "/title" + i;
                    //存储字段英文名的路径
                    string text = "Canvas3/ModelDetail/table/label" + i + "/text" + i;
                    //在title处存放字段的名称
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    //在text处存放字段的数据
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
            }
            else if (lableNum >= 5 && lableNum <= 8)
            {
                for (int i = 1; i < 5; i++)
                {
                    string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/ModelDetail/table/label" + i + "/title" + i;
                    string text = "Canvas3/ModelDetail/table/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
                for (int i = 5; i < 9; i++)
                {
                    string sqlAttr = "select HanYi from [SqlTest].[dbo].[SheBeiTestAttribute] where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/ModelDetail/table2/label" + i + "/title" + i;
                    string text = "Canvas3/ModelDetail/table2/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
            }
            else
            {
                for (int i = 1; i < 5; i++)
                {
                    string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    sqlAdapterAttr.Fill(sqlDataTableAttr);
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/ModelDetail/table/label" + i + "/title" + i;
                    string text = "Canvas3/ModelDetail/table/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
                for (int i = 5; i < 9; i++)
                {
                    string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    sqlAdapterAttr.Fill(sqlDataTableAttr);
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/ModelDetail/table2/label" + i + "/title" + i;
                    string text = "Canvas3/ModelDetail/table2/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
                for (int i = 9; i < 13; i++)
                {
                    string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
                    SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
                    DataTable sqlDataTableAttr = new DataTable();
                    //执行查询
                    int SearchResult = sqlAdapterAttr.Fill(sqlDataTableAttr);
                    if (SearchResult == 0)
                    {
                        break;
                    }
                    string str = row[i].ToString();
                    string title = "Canvas3/ModelDetail/table3/label" + i + "/title" + i;
                    string text = "Canvas3/ModelDetail/table3/label" + i + "/text" + i;
                    GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
                    GameObject.Find(text).GetComponent<Text>().text = str;
                }
            }
            string imgPath = Application.dataPath + "//Resources//folder//modelImg//" + row["ImgPath"].ToString();
            string rawImgPath = "Canvas3/ModelDetail/Image_text/RawImage";
            Texture2D tx = new Texture2D(220, 220);
            tx.LoadImage(getImageByte(imgPath));
            GameObject.Find(rawImgPath).GetComponent<RawImage>().texture = tx;
        }
        sqlCon.Close();
    }
    /// <summary>
    /// 在展示卡片里展示模型信息
    /// </summary>
    /// <param name="modelName"></param>
    //public void ZhanShiModel(string modelName, string type)
    //{
    //    //开启数据库
    //    sqlCon.Open();
    //    #region 数据库类型扩展内容
    //    //提高程序的可扩展性，防止不同模型的字段不同，根据模型类型搭建数据库
    //    //可尽量分类，某些模型不具备的字段可以设空，如果为“”那么就字段名字也不展示，如果null那么字段名字展示
    //    //可以进行提前判断，然后用总字段数减去ID减去ImgPath减去""字段数
    //    //数据库类型
    //    string ShuJuKu = "";
    //    //判断数据库类型
    //    if (type == "消防设备")
    //    {
    //        ShuJuKu = "SheBeiTest";
    //    }
    //    else if (type == "管线")
    //    {
    //        ShuJuKu = "SheBeiTest1";
    //    }
    //    else if (type == "汽车")
    //    {
    //        ShuJuKu = "SheBeiTest2";
    //    }
    //    else if (type == "阀门")
    //    {
    //        ShuJuKu = "SheBeiTest3";
    //    }
    //    //数据库执行语句
    //    string sql = "select * from " + ShuJuKu + " where SheBeiMingCheng='" + modelName + "'";
    //    #endregion
    //    //检索保存数据
    //    SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
    //    //数据存储表
    //    DataTable sqlDataTable = new DataTable();
    //    //执行查询
    //    sqlAdapter.Fill(sqlDataTable);
    //    //判断字段条数，用来判断如何展示
    //    //获取每一行数据
    //    foreach (DataRow row in sqlDataTable.Rows)
    //    {
    //        //字段数减2，是减去ID与ImagePath   未来应改为减4，减去Path与SourcePath ，注意除ID外，均要放置在最后几列
    //        int lableNum = sqlDataTable.Columns.Count - 4;
    //        //如果lableNum <= 4，只放在第一行table中
    //        if (lableNum <= 4)
    //        {
    //            //对于该行的每一个数据
    //            for (int i = 1; i < 5; i++)
    //            {
    //                //查询该字段对应的中文名
    //                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
    //                //执行查询与保存
    //                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
    //                //数据存储表
    //                DataTable sqlDataTableAttr = new DataTable();
    //                //执行查询
    //                sqlAdapterAttr.Fill(sqlDataTableAttr);
    //                //该行该列字段数据
    //                string str = row[i].ToString();
    //                //存储字段中文名的路径
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table/label" + i + "/title" + i;
    //                //存储字段英文名的路径
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table/label" + i + "/text" + i;
    //                //在title处存放字段的名称
    //                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
    //                //在text处存放字段的数据
    //                GameObject.Find(text).GetComponent<Text>().text = str;
    //            }
    //            for (int i = 5; i < 9; i++)
    //            {
    //                //存储字段中文名的路径
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table2/label" + i + "/title" + i;
    //                //存储字段英文名的路径
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table2/label" + i + "/text" + i;
    //                //在title处存放字段的名称
    //                GameObject.Find(title).GetComponent<Text>().text = "";
    //                //在text处存放字段的数据
    //                GameObject.Find(text).GetComponent<Text>().text = "";
    //            }
    //            for (int i = 9; i < 13; i++)
    //            {
    //                //存储字段中文名的路径
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table3/label" + i + "/title" + i;
    //                //存储字段英文名的路径
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table3/label" + i + "/text" + i;
    //                //在title处存放字段的名称
    //                GameObject.Find(title).GetComponent<Text>().text = "";
    //                //在text处存放字段的数据
    //                GameObject.Find(text).GetComponent<Text>().text = "";
    //            }
    //        }
    //        else if (lableNum >= 5 && lableNum <= 8)
    //        {
    //            for (int i = 1; i < 5; i++)
    //            {
    //                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
    //                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
    //                DataTable sqlDataTableAttr = new DataTable();
    //                sqlAdapterAttr.Fill(sqlDataTableAttr);
    //                string str = row[i].ToString();
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table/label" + i + "/title" + i;
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table/label" + i + "/text" + i;
    //                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
    //                GameObject.Find(text).GetComponent<Text>().text = str;
    //            }
    //            for (int i = 5; i < 9; i++)
    //            {
    //                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
    //                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
    //                DataTable sqlDataTableAttr = new DataTable();
    //                sqlAdapterAttr.Fill(sqlDataTableAttr);
    //                string str = row[i].ToString();
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table2/label" + i + "/title" + i;
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table2/label" + i + "/text" + i;
    //                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
    //                GameObject.Find(text).GetComponent<Text>().text = str;
    //            }
    //            for (int i = 9; i < 13; i++)
    //            {
    //                //存储字段中文名的路径
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table3/label" + i + "/title" + i;
    //                //存储字段英文名的路径
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table3/label" + i + "/text" + i;
    //                //在title处存放字段的名称
    //                GameObject.Find(title).GetComponent<Text>().text = "";
    //                //在text处存放字段的数据
    //                GameObject.Find(text).GetComponent<Text>().text = "";
    //            }
    //        }
    //        else
    //        {
    //            for (int i = 1; i < 5; i++)
    //            {
    //                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
    //                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
    //                DataTable sqlDataTableAttr = new DataTable();
    //                sqlAdapterAttr.Fill(sqlDataTableAttr);
    //                string str = row[i].ToString();
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table/label" + i + "/title" + i;
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table/label" + i + "/text" + i;
    //                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
    //                GameObject.Find(text).GetComponent<Text>().text = str;
    //            }
    //            for (int i = 5; i < 9; i++)
    //            {
    //                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
    //                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
    //                DataTable sqlDataTableAttr = new DataTable();
    //                sqlAdapterAttr.Fill(sqlDataTableAttr);
    //                string str = row[i].ToString();
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table2/label" + i + "/title" + i;
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table2/label" + i + "/text" + i;
    //                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
    //                GameObject.Find(text).GetComponent<Text>().text = str;
    //            }
    //            for (int i = 9; i < 13; i++)
    //            {
    //                string sqlAttr = "select HanYi from SheBeiTestAttribute where ZiDuanMingCheng='" + sqlDataTable.Columns[i] + "'";
    //                SqlDataAdapter sqlAdapterAttr = new SqlDataAdapter(sqlAttr, sqlCon);
    //                DataTable sqlDataTableAttr = new DataTable();
    //                sqlAdapterAttr.Fill(sqlDataTableAttr);
    //                string str = row[i].ToString();
    //                string title = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table3/label" + i + "/title" + i;
    //                string text = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/table3/label" + i + "/text" + i;
    //                GameObject.Find(title).GetComponent<Text>().text = sqlDataTableAttr.Rows[0].ItemArray[0].ToString();
    //                GameObject.Find(text).GetComponent<Text>().text = str;
    //            }
    //        }
    //        string imgPath;
    //        if (type=="消防设备")
    //        {
    //         imgPath = Application.dataPath + "//Resources//folder//modelImg//XiaoFangSheBei/" + row["ImgPath"].ToString();
    //        }
    //        else if (type=="管线")
    //        {
    //            imgPath = Application.dataPath + "//Resources//folder//modelImg//GuanXian/" + row["ImgPath"].ToString();
    //        }
    //        else if (type =="汽车")
    //        {
    //            imgPath = Application.dataPath + "//Resources//folder//modelImg//QiChe/" + row["ImgPath"].ToString();
    //        }
    //        else
    //        {
    //            imgPath = Application.dataPath + "//Resources//folder//modelImg//FaMen/" + row["ImgPath"].ToString();
    //        }
    //        string rawImgPath = "Canvas3/Canvas3_shebeichaxun/Canvas3_shebeichaxun_quan/1号消防泵/Image_text/RawImage";
    //        Texture2D tx = new Texture2D(220, 220);
    //        tx.LoadImage(getImageByte(imgPath));
    //        GameObject.Find(rawImgPath).GetComponent<RawImage>().texture = tx;
    //    }
    //    sqlCon.Close();
    //}
    /// <summary>  
    /// 根据图片路径返回图片的字节流byte[]  
    /// </summary>  
    /// <param name="imagePath">图片路径</param>  
    /// <returns>返回的字节流</returns>  
    private static byte[] getImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];//给imgByte空间
        files.Read(imgByte, 0, imgByte.Length);
        //将files中的信息读进imgByte中,public override int Read(byte[] array, int offset, int count);
        files.Close();//关闭文件流
        return imgByte;
    }
    public string JudgeType(string HouZhuiMing, string fileName)
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
            path = Application.dataPath + "//Resources//folder//img//" + fileName + HouZhuiMing;
        }
        else if (HouZhuiMing == ".mp4" || HouZhuiMing == ".m4v" || HouZhuiMing == ".mov" || HouZhuiMing == ".qt"
            || HouZhuiMing == ".avi" || HouZhuiMing == ".flv" || HouZhuiMing == ".wmv" || HouZhuiMing == ".asf"
            || HouZhuiMing == ".mpeg" || HouZhuiMing == ".mpg" || HouZhuiMing == ".vob" || HouZhuiMing == ".mkv"
            || HouZhuiMing == ".asf" || HouZhuiMing == ".rm" || HouZhuiMing == ".rmvb" || HouZhuiMing == ".vob"
            || HouZhuiMing == ".ts" || HouZhuiMing == ".dat")
        {
            path = Application.dataPath + "//Resources//folder//video//" + fileName + HouZhuiMing;
        }
        else
        {
            path = Application.dataPath + "//Resources//folder//file//" + fileName + HouZhuiMing;
        }
        return path;
    }
    public void ZhanShiTupian()
    {
        //开启数据库
        sqlCon.Open();
        //模糊查询      
        string sql = "select * from img ";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //根据文件数量设置图片滚动框高度
        GameObject.Find("Canvas3/照片/Viewport/Content").transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, sqlDataTable.Rows.Count * 130);
        ////定义一个i，用来命名button，i递增
        int i = 1;
        //foreach (Transform t in GameObject.Find("Canvas3/照片/Viewport/Content" ).transform)
        //{
        //    //GameObject.Destroy(t.gameObject);
        //    Debug.Log(t.name + "  a");
        //}

        //之前的清空列表方法
        //GameObject go = GameObject.Find("Canvas3/照片/Viewport/Content");
        //List<Transform> lst = new List<Transform>();
        //foreach (Transform child in go.transform)
        //{
        //    lst.Add(child);
        //    Debug.Log(child.gameObject.name);
        //}
        //for (int j = 0; j < lst.Count; j++)
        //{
        //    Debug.Log("销毁的物体是：" + lst[j].gameObject);
        //    Destroy(lst[j].gameObject);
        //}

        GameObject go = GameObject.Find("Canvas3/照片");
        //有隐患，可能会产生一批没有被销毁的对象
        go.GetComponent<ScrollRect>().content.DetachChildren();
        ////100000还需要权衡
        //for (i = 1; i < 100000; i++)
        //{
        //    if (GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i) != null)
        //    {
        //        GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i).SetActive(false);
        //        //GameObject.Find("Canvas3/照片/Viewport/Content/Text" + i).SetActive(false);
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}
        foreach (DataRow row in sqlDataTable.Rows)
        {
            Texture2D tx = new Texture2D(100, 100);
            string path = JudgeType(row["type"].ToString(), row["name"].ToString());
            tx.LoadImage(getImageByte(path));//tx接收byte数据转化为data
                                             //images.Add(tx);
                                             //新建button
            GameObject button = new GameObject("Button" + i, typeof(Button), typeof(RectTransform), typeof(Image));
            Sprite sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector2.zero);
            button.GetComponent<Image>().sprite = sprite;
            //新建text 存储文件名
            GameObject text = new GameObject("Text", typeof(Text));
            text.transform.position = new Vector3(0, -102, 0);
            //新建text1 存储文件路径
            GameObject text1 = new GameObject("Text1", typeof(Text));
            GameObject text2 = new GameObject("Text2", typeof(Text));
            //设置button的父物体
            button.transform.SetParent(GameObject.Find("Canvas3/照片/Viewport/Content").transform);
            //设置text的父物体
            text.transform.SetParent(GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i).transform);
            //设置text1的父物体
            text1.transform.SetParent(GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i).transform);
            text2.transform.SetParent(GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i).transform);
            //text存储文件名
            text.GetComponent<Text>().text = row["name"].ToString() + row["type"].ToString();
            //text1存储文件路径
            text1.GetComponent<Text>().text = row["type"].ToString();
            text2.GetComponent<Text>().text = row["name"].ToString();
            //改变text的字体大小
            text.GetComponent<Text>().fontSize = 0;
            //改变text的字体颜色
            text.GetComponent<Text>().color = Color.red;
            //text2.GetComponent<Text>().color = Color.red;
            //给text添加点击事件，使得点击可以打开文件
            GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i).AddComponent<ClickHandler>();
            //GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i).GetComponent<Button>().onClick.AddListener(clickHandler.OnClick);
            //给text添加test1脚本用来修改font
            GameObject.Find("Canvas3/照片/Viewport/Content/Button" + i + "/Text").AddComponent<test1>();
            //GameObject.Find("Canvas3/照片/Viewport/Content/Text" + i).AddComponent<test1>();
            //GameObject.Find("Canvas3/照片/Viewport/Content/Text" + i).transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30);
            //button命名变量自增
            i++;
        }
        sqlCon.Close();
    }
    public void SouSuoTupian(string imgName)
    {
        //开启数据库
        sqlCon.Open();
        //模糊查询
        string sql = "select * from img where name like '%" + imgName + "%'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //根据文件数量设置图片滚动框高度
        GameObject.Find("Canvas3/照片/Viewport/Content").transform.GetComponent<RectTransform>().
            sizeDelta = new Vector2(0, ((sqlDataTable.Rows.Count / 2) + 1) * 150 + 30);
        for (int j = 0; j < GameObject.Find("Canvas3/照片/Viewport/Content").transform.childCount; j++)
        {
            GameObject.Find("Canvas3/照片/Viewport/Content").transform.GetChild(j).gameObject.SetActive(false);
        }
        foreach (DataRow row in sqlDataTable.Rows)
        {
            for (int j = 0; j < GameObject.Find("Canvas3/照片/Viewport/Content").transform.childCount; j++)
            {
                if (GameObject.Find("Canvas3/照片/Viewport/Content").transform.GetChild(j).transform.
                    GetChild(0).GetComponent<Text>().text == row["name"].ToString() + row["type"].ToString())
                {
                    GameObject.Find("Canvas3/照片/Viewport/Content").transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        sqlCon.Close();
    }
    public void ZhanShiShiPin()
    {
        //开启数据库
        sqlCon.Open();
        //模糊查询
        string sql = "select * from video ";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //根据文件数量设置图片滚动框高度
        GameObject.Find("Canvas3/视频/Viewport/Content").transform.GetComponent<RectTransform>().
            sizeDelta = new Vector2(0, ((sqlDataTable.Rows.Count / 2) + 1) * 150 + 30);
        ////定义一个i，用来命名button，i递增
        int i = 1;
        //100000还需要权衡
        for (i = 1; i < 100000; i++)
        {
            if (GameObject.Find("Canvas3/视频/Viewport/Content/Button" + i) != null)
            {
                GameObject.Find("Canvas3/视频/Viewport/Content/Button" + i).SetActive(false);
                //GameObject.Find("Canvas3/视频/Viewport/Content/Text" + i).SetActive(false);
            }
            else
            {
                break;
            }
        }
        foreach (DataRow row in sqlDataTable.Rows)
        {
            Texture2D tx = new Texture2D(100, 100);
            string path = Application.dataPath + "//Resources//folder//SuoLueTu//文件类型-标准图-视频文件.png";
            tx.LoadImage(getImageByte(path));//tx接收byte数据转化为data
                                             //新建button
            GameObject button = new GameObject("Button" + i, typeof(Button), typeof(Image));
            Sprite sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector2.zero);
            button.GetComponent<Image>().sprite = sprite;
            //新建text 存储文件名
            GameObject text = new GameObject("Text", typeof(Text));
            text.transform.position = new Vector3(0, -110, 0);
            //新建text1 存储文件路径
            GameObject text1 = new GameObject("Text1", typeof(Text));
            GameObject text2 = new GameObject("Text2", typeof(Text));
            //设置button的父物体
            button.transform.SetParent(GameObject.Find("Canvas3/视频/Viewport/Content").transform);
            //设置text的父物体
            text.transform.SetParent(GameObject.Find("Canvas3/视频/Viewport/Content/Button" + i).transform);
            //设置text1的父物体
            text1.transform.SetParent(GameObject.Find("Canvas3/视频/Viewport/Content/Button" + i).transform);
            text2.transform.SetParent(GameObject.Find("Canvas3/视频/Viewport/Content/Button" + i).transform);
            //text存储文件名
            text.GetComponent<Text>().text = row["name"].ToString() + row["type"].ToString();
            //text1存储文件路径
            text1.GetComponent<Text>().text = row["type"].ToString();
            text2.GetComponent<Text>().text = row["name"].ToString();
            //改变text的字体大小
            text.GetComponent<Text>().fontSize = 0;
            //改变text的字体颜色
            text.GetComponent<Text>().color = Color.red;
            //text2.GetComponent<Text>().color = Color.red;
            //给text添加点击事件，使得点击可以打开文件
            GameObject.Find("Canvas3/视频/Viewport/Content/Button" + i).AddComponent<ClickHandler>();
            //给text添加test1脚本用来修改font
            GameObject.Find("Canvas3/视频/Viewport/Content/Button" + i + "/Text").AddComponent<test1>();
            //GameObject.Find("Canvas3/视频/Viewport/Content/Text" + i).AddComponent<test1>();
            //GameObject.Find("Canvas3/视频/Viewport/Content/Text" + i).transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30);
            //button命名变量自增
            i++;
        }
        sqlCon.Close();
    }
    public void SouSuoShiPin(string videoName)
    {
        //开启数据库
        sqlCon.Open();
        //模糊查询
        string sql = "select * from video where name like '%" + videoName + "%'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //根据文件数量设置图片滚动框高度
        GameObject.Find("Canvas3/视频/Viewport/Content").transform.GetComponent<RectTransform>().
            sizeDelta = new Vector2(0, ((sqlDataTable.Rows.Count / 2) + 1) * 150 + 30);
        for (int j = 0; j < GameObject.Find("Canvas3/视频/Viewport/Content").transform.childCount; j++)
        {
            GameObject.Find("Canvas3/视频/Viewport/Content").transform.GetChild(j).gameObject.SetActive(false);
        }
        foreach (DataRow row in sqlDataTable.Rows)
        {
            for (int j = 0; j < GameObject.Find("Canvas3/视频/Viewport/Content").transform.childCount; j++)
            {
                if (GameObject.Find("Canvas3/视频/Viewport/Content").transform.GetChild(j).transform.
                    GetChild(0).GetComponent<Text>().text == row["name"].ToString() + row["type"].ToString())
                {
                    GameObject.Find("Canvas3/视频/Viewport/Content").transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        sqlCon.Close();
    }
    public string GetSuoLueTuPath(string type)
    {
        string path = Application.dataPath + "//Resources//folder//SuoLueTu//";
        if (type == ".iso" || type == ".ISO" || type == ".mdf" || type == ".MDF")
        {
            path += "镜像文件-iso.png";
        }
        else if (type == ".rar" || type == ".RAR" || type == ".zip" || type == ".ZIP" || type == ".arj" || type == ".ARJ" || type == ".gz" ||
           type == ".GZ")
        {
            path += "文件类型-标准图-压缩文件.png";
        }
        else if (type == ".html" || type == ".HTML" || type == ".asp" || type == ".ASP" || type == ".htm" || type == ".HTM"
            || type == ".JSP" || type == ".jsp" || type == ".php" || type == ".PHP" || type == ".php3" || type == ".PHP3"
            || type == ".phtml" || type == ".PHTML")
        {
            path += "网页-web_h5.png";
        }
        else if (type == ".exe" || type == ".EXE" || type == ".com" || type == ".COM" || type == ".bat" || type == ".BAT"
            || type == ".cmd" || type == ".CMD")
        {
            path += "可执行文件-exe.png";
        }
        else if (type == ".pdf" || type == ".PDF")
        {
            path += "文件类型-标准图-PDF文档.png";
        }
        else if (type == ".tmp" || type == ".TMP")
        {
            path += "临时文件.png";
        }
        else if (type == ".txt" || type == ".TXT")
        {
            path += "文件类型-标准图-记事本.png";
        }
        else if (type == ".doc" || type == ".DOC" || type == ".docx" || type == ".DOCX" || type == ".rtf" || type == ".RTF"
            || type == ".dot" || type == ".DOT")
        {
            path += "文件类型-标准图-Word文档.png";
        }
        else if (type == ".xls" || type == ".XLS" || type == ".xlsx" || type == ".XLSX" || type == ".xlt" || type == ".XLT")
        {
            path += "文件类型-标准图-工作表.png";
        }
        else if (type == ".ppt" || type == ".PPT" || type == ".pptx" || type == ".PPTX")
        {
            path += "文件类型-标准图-幻灯片.png";
        }
        else if (type == ".wav" || type == ".WAV" || type == ".aif" || type == ".AIF" || type == ".au" || type == ".AU"
            || type == ".mp3" || type == ".MP3" || type == ".ram" || type == ".RAM" || type == ".wma" || type == ".WMA"
            || type == ".mmf" || type == ".MMF" || type == ".amr" || type == ".AMR" || type == ".aac" || type == ".AAC"
            || type == ".flac" || type == ".FLAC")
        {
            path += "文件类型-标准图-声音文件.png";
        }
        else if (type == ".dll" || type == ".DLL")
        {
            path += "dll.png";
        }
        else if (type == ".fbx" || type == ".FBX")
        {
            path += "FBX.png";
        }
        else if (type == ".json" || type == ".JSON")
        {
            path += "json.png";
        }
        else
        {
            path += "文件类型-标准图-未知文件.png";
        }
        return path;
    }
    public void ZhanShiWenDang()
    {
        //开启数据库
        sqlCon.Open();
        //模糊查询
        string sql = "select * from [SqlTest].[dbo].[file] ";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        //根据文件数量设置图片滚动框高度
        GameObject.Find("Canvas3/文件/Viewport/Content").transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, ((sqlDataTable.Rows.Count / 2) + 1) * 150 + 30);
        ////定义一个i，用来命名button，i递增
        int i = 1;
        //100000还需要权衡
        for (i = 1; i < 100000; i++)
        {
            if (GameObject.Find("Canvas3/文件/Viewport/Content/Button" + i) != null)
            {
                GameObject.Find("Canvas3/文件/Viewport/Content/Button" + i).SetActive(false);
                //GameObject.Find("Canvas33/文件/Viewport/Content/Text" + i).SetActive(false);
            }
            else
            {
                break;
            }
        }
        foreach (DataRow row in sqlDataTable.Rows)
        {
            Texture2D tx = new Texture2D(100, 100);
            string path = GetSuoLueTuPath(row["type"].ToString());
            tx.LoadImage(getImageByte(path));//tx接收byte数据转化为data
                                             //新建button
            GameObject button = new GameObject("Button" + i, typeof(Button), typeof(Image));
            Sprite sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), Vector2.zero);
            button.GetComponent<Image>().sprite = sprite;
            //新建text 存储文件名
            GameObject text = new GameObject("Text", typeof(Text));
            text.transform.position = new Vector3(0, -110, 0);
            //新建text1 存储文件路径
            GameObject text1 = new GameObject("Text1", typeof(Text));
            GameObject text2 = new GameObject("Text2", typeof(Text));
            //设置button的父物体
            button.transform.SetParent(GameObject.Find("Canvas3/文件/Viewport/Content").transform);
            //设置text的父物体
            text.transform.SetParent(GameObject.Find("Canvas3/文件/Viewport/Content/Button" + i).transform);
            //设置text1的父物体
            text1.transform.SetParent(GameObject.Find("Canvas3/文件/Viewport/Content/Button" + i).transform);
            text2.transform.SetParent(GameObject.Find("Canvas3/文件/Viewport/Content/Button" + i).transform);
            //text存储文件名
            text.GetComponent<Text>().text = row["name"].ToString() + row["type"].ToString();
            //text1存储文件路径
            text1.GetComponent<Text>().text = row["type"].ToString();
            text2.GetComponent<Text>().text = row["name"].ToString();
            //改变text的字体大小
            text.GetComponent<Text>().fontSize = 0;
            //改变text的字体颜色
            text.GetComponent<Text>().color = Color.red;
            //text2.GetComponent<Text>().color = Color.red;
            //给text添加点击事件，使得点击可以打开文件
            GameObject.Find("Canvas3/文件/Viewport/Content/Button" + i).AddComponent<ClickHandler>();
            //给text添加test1脚本用来修改font
            GameObject.Find("Canvas3/文件/Viewport/Content/Button" + i + "/Text").AddComponent<test1>();
            //GameObject.Find("Canvas3/文件/Viewport/Content/Text" + i).AddComponent<test1>();
            //GameObject.Find("Canvas3/文件/Viewport/Content/Text" + i).transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 30);
            //button命名变量自增
            i++;
        }
        sqlCon.Close();
    }
    public void SouSuoWenDang(string wendangName)
    {
        //开启数据库
        sqlCon.Open();
        //模糊查询
        string sql = "select * from [SqlTest].[dbo].[file] where name like '%" + wendangName + "%'";
        //检索保存数据
        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sql, sqlCon);
        //数据存储表
        DataTable sqlDataTable = new DataTable();
        //执行查询
        sqlAdapter.Fill(sqlDataTable);
        GameObject.Find("Canvas3/文件/Viewport/Content").transform.GetComponent<RectTransform>().
            sizeDelta = new Vector2(0, ((sqlDataTable.Rows.Count / 2) + 1) * 150 + 30);
        for (int j = 0; j < GameObject.Find("Canvas3/文件/Viewport/Content").transform.childCount; j++)
        {
            GameObject.Find("Canvas3/文件/Viewport/Content").transform.GetChild(j).gameObject.SetActive(false);
        }
        foreach (DataRow row in sqlDataTable.Rows)
        {
            for (int j = 0; j < GameObject.Find("Canvas3/文件/Viewport/Content").transform.childCount; j++)
            {
                if (GameObject.Find("Canvas3/文件/Viewport/Content").transform.GetChild(j).transform.
                    GetChild(0).GetComponent<Text>().text == row["name"].ToString() + row["type"].ToString())
                {
                    GameObject.Find("Canvas3/文件/Viewport/Content").transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        sqlCon.Close();
    }
    //展示视频退出按钮
    public void ShiPinTuiChu()
    {
        shipin.transform.position = new Vector3(2883, 999, 600);
        dangqianwenjian.transform.position = new Vector3(529, 484, 600);
    }
    //展示图片退出按钮
    public void TuPianTuiChu()
    {
        zhaopian.transform.position = new Vector3(1668, 1050, 600);
        dangqianwenjian.transform.position = new Vector3(529, 484, 600);
    }
    //展示文件退出按钮
    public void WenJianTuiChu()
    {
        wenjian.transform.position = new Vector3(2292, 1037, 600);
        dangqianwenjian.transform.position = new Vector3(529, 484, 600);
    }
    /// <summary>
    /// 按钮位于 场景3 Canvas3/RawImage/Button_moxingxianshi 模型显示按钮
    /// 作用是开始进行模型显示
    /// </summary>
    public void ModelDisplayButton()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        RawImage_dropdownlist.SetActive(true);
        //FileDisplay.GetComponent<Button>().interactable = false;
        RawImage.SetActive(false);
    }
    public void ModelDisplayButtonExit()
    {
        RawImage_dropdownlist.SetActive(false);
        //FileDisplay.GetComponent<Button>().interactable = true;
        //RawImage.SetActive(true);
    }
}
