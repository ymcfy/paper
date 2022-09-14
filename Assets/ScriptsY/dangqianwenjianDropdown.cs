using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 显示当前文件的下拉菜单
/// </summary>
public class dangqianwenjianDropdown : MonoBehaviour
{
    //下拉菜单信息
    Dropdown dpn;
    //照片信息展示滚动框
    public GameObject zhaopian;
    //视频信息展示滚动框
    public GameObject shipin;
    //文件信息展示滚动框
    public GameObject wenjian;
    //当前文件下拉菜单
    public GameObject dangqianwenjian;
    public GameObject RawImage;
    public GameObject exit;
    public GameObject PicExit;
    public GameObject VideoExit;
    public GameObject FileExit;
    //右下角坐标
    public Vector3 BottomRightCorner;
    //右下角外面坐标
    public Vector3 OutSideBottomRightCorner;
    //正中间坐标
    public Vector3 Center;
    /// <summary>
    /// 表示图片展示框有没有展示过
    /// </summary>
    private bool isShowed;
    /// <summary>

    /// <summary>
    /// 设置下拉菜单内容
    /// </summary>
    void Start()
    {
        //初始化isShowed 和 isDeleted
        isShowed = false;

        //右下角坐标
        BottomRightCorner = new Vector3(1865, 50, 600);
        //正中间坐标
        Center = new Vector3(954, 640, -100f);
        //右下角外面坐标
        OutSideBottomRightCorner = new Vector3(2310, 50, 600);
        ////图片选项
        //Dropdown.OptionData data1 = new Dropdown.OptionData();
        //data1.text = "图片";
        ////视频选项
        //Dropdown.OptionData data2 = new Dropdown.OptionData();
        //data2.text = "视频";
        ////文件选项
        //Dropdown.OptionData data3 = new Dropdown.OptionData();
        //data3.text = "文件";
        //////退出选项
        ////Dropdown.OptionData data4 = new Dropdown.OptionData();
        ////data4.text = "退出";
        ////获取下拉框的下拉框属性
        //dpn = transform.GetComponent<Dropdown>();
        ////将上述五个选项信息放入下拉框
        //dpn.options.Add(data1);
        //dpn.options.Add(data2);
        //dpn.options.Add(data3);
        ////dpn.options.Add(data4);
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void Picture() {
        //dangqianwenjian.transform.position = new Vector3(2464, 139, 600);
        //dangqianwenjian.transform.position = OutSideBottomRightCorner;
        //照片滚动框展示
        zhaopian.transform.position = new Vector3(960, 640, 0);
        //zhaopian.transform.position = Center;
        //exit.transform.position = new Vector3(2810, 50, 600);
        //exit.transform.position = OutSideBottomRightCorner;
        //PicExit.transform.position = new Vector3(1810, 50, 600);
        //PicExit.transform.position = BottomRightCorner;
        //调用展示图片方法
        //zhaopian.SetActive(true);
        //如果已经展示过一次，那么就不用再数据库搜索一次了
        if (isShowed == true )
        {

        }
        else
        {
            new ZhanShi().ZhanShiTupian();
            isShowed = true;
        }

    }
    public void exitPicture() {
        zhaopian.transform.position = new Vector3(3288, 1133, 0);
        //zhaopian.transform.position =OutSideBottomRightCorner;
        //dangqianwenjian.transform.position = new Vector3(956, 640, 600);
        //dangqianwenjian.transform.position = Center;
        //PicExit.transform.position = new Vector3(2080, 50, 600);
        //PicExit.transform.position = OutSideBottomRightCorner;
        //exit.transform.position = new Vector3(1810, 50, 600);
        //exit.transform.position = BottomRightCorner;
        //zhaopian.SetActive(false);
    }
    public void Video() {
        //dangqianwenjian.transform.position = new Vector3(2464, 139, 600);
        //dangqianwenjian.transform.position = OutSideBottomRightCorner;
        shipin.transform.position = new Vector3(960, 640, 0);
        //shipin.transform.position = Center;
        //exit.transform.position = new Vector3(2080, 50, 600);
        //exit.transform.position = OutSideBottomRightCorner;
        //VideoExit.transform.position = new Vector3(1810, 50, 600);
        //VideoExit.transform.position = BottomRightCorner;
        //shipin.SetActive(true);
        new ZhanShi().ZhanShiShiPin();
    }
    public void exitVideo()
    {
        shipin.transform.position = new Vector3(3288, 1133, 0);
        //shipin.transform.position = OutSideBottomRightCorner;
        //dangqianwenjian.transform.position = Center;
        //VideoExit.transform.position = new Vector3(2080, 50, 600);
        //VideoExit.transform.position = OutSideBottomRightCorner;
        //exit.transform.position = new Vector3(1810, 50, 600);
        //exit.transform.position = BottomRightCorner;
        //shipin.SetActive(false);
    }
    public void exitVideoDisplay() {
        GameObject.Find("Canvas3/Plane").transform.position = OutSideBottomRightCorner;
        shipin.transform.position = Center;
        VideoExit.transform.position = BottomRightCorner;
        GameObject.Find("Canvas3/退出/视频播放退出").transform.position = OutSideBottomRightCorner;
        new ZhanShi().ZhanShiShiPin();
        GameObject.Find("Canvas3/Plane").GetComponent<UnityEngine.Video.VideoPlayer>().url = null;
    }
    public void File() {
        //dangqianwenjian.transform.position = new Vector3(2464, 139, 600);
        //dangqianwenjian.transform.position = OutSideBottomRightCorner;
        wenjian.transform.position = new Vector3(960, 640, 0);
        //wenjian.transform.position =Center;
        //exit.transform.position = new Vector3(2080, 50, 600);
        //exit.transform.position = OutSideBottomRightCorner;
        //FileExit.transform.position = new Vector3(1810, 50, 600);
        //FileExit.transform.position = BottomRightCorner;
        //wenjian.SetActive(true);
        new ZhanShi().ZhanShiWenDang();
    }
    public void exitFile()
    {
        wenjian.transform.position = new Vector3(3288, 1133, 0);
        //wenjian.transform.position = OutSideBottomRightCorner;
        //dangqianwenjian.transform.position = new Vector3(956, 640, 600);
        //dangqianwenjian.transform.position = Center;
        //FileExit.transform.position = new Vector3(2080, 50, 600);
        //FileExit.transform.position = OutSideBottomRightCorner;
        //exit.transform.position = new Vector3(1810, 50, 600);
        //exit.transform.position = BottomRightCorner;
        //wenjian.SetActive(false);
    }
    /// <summary>
    /// 判断选项，根据每个选项做相应操作
    /// </summary>
    public void panduan()
    {
        //如果选择图片
        if (dpn.captionText.text == "图片")
        {
            //当前文件下拉菜单移走
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            //照片滚动框展示
            zhaopian.transform.position = new Vector3(529, 370, 600);
            //调用展示图片方法
            new ZhanShi().ZhanShiTupian();
        }
        else if (dpn.captionText.text == "文件")
        {
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            wenjian.transform.position = new Vector3(529, 370, 600);
            new ZhanShi().ZhanShiWenDang();
        }
        else if (dpn.captionText.text == "视频")
        {
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            shipin.transform.position = new Vector3(529, 370, 600);
            new ZhanShi().ZhanShiShiPin();
        }
        //如果选择退出
        else if (dpn.captionText.text == "退出")
        {
            //当前文件移走
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            //下拉框内容设为空
            dpn.captionText.text = "";
            RawImage.SetActive(true);
        }
    }
}
