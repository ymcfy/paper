using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 点击显示按钮，然后将当前文件下拉框显示
/// </summary>
public class FileShowController : MonoBehaviour
{
    //当前文件下拉框
    public Transform dangqianwenjian;
    public GameObject RawImage;
    public GameObject exit;
    //右下角坐标
    public Vector3 BottomRightCorner;
    //右下角外面坐标
    public Vector3 OutSideBottomRightCorner;
    //正中间坐标
    public Vector3 Center;
    /// <summary>
    /// Main Camera
    /// </summary>
    public GameObject MainCamera;
    // Use this for initialization
    void Start()
    {
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
    /// <summary>
    /// 显示当前文件下拉框
    /// </summary>
    public void DangqianwenjianShow()
    {
        //关闭滑轮控制模型功能
        MainCamera.GetComponent<CameraControl>().enabled = false;
        //dangqianwenjian.transform.position = new Vector3(956, 640, 600);
        dangqianwenjian.transform.position = Center;
        RawImage.SetActive(false);
        //exit.transform.position = new Vector3(1810, 50, 600);
        exit.transform.position = BottomRightCorner;
    }
}
