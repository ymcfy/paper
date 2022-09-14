using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DropDownHandler : MonoBehaviour
{


    //下拉菜单信息
    Dropdown dpn;

    //照片信息展示滚动框
    public GameObject shujuku1;

    //视频信息展示滚动框
    public GameObject shujuku2;

    //文件信息展示滚动框
    public GameObject shujuku3;

    public GameObject shujuku4;

    //当前文件下拉菜单
    public Dropdown dangqianwenjian;

    /// <summary>
    /// 设置下拉菜单内容
    /// </summary>
    void Start()
    {

        //图片选项
        Dropdown.OptionData data1 = new Dropdown.OptionData();
        data1.text = "消防设备";

        //视频选项
        Dropdown.OptionData data2 = new Dropdown.OptionData();
        data2.text = "管线";

        //文件选项
        Dropdown.OptionData data3 = new Dropdown.OptionData();
        data3.text = "汽车";
        Dropdown.OptionData data4 = new Dropdown.OptionData();
        data4.text = "阀门";

        ////退出选项
        //Dropdown.OptionData data5 = new Dropdown.OptionData();
        //data5.text = "退出";


        //获取下拉框的下拉框属性
        dpn = transform.GetComponent<Dropdown>();
        //将上述五个选项信息放入下拉框
        dpn.options.Add(data1);
        dpn.options.Add(data2);
        dpn.options.Add(data3);
        dpn.options.Add(data4);
        //dpn.options.Add(data5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 判断选项，根据每个选项做相应操作
    /// </summary>
    public void panduan()
    {
        //如果选择图片
        if (dpn.captionText.text == "消防设备")
        {
            //new FileUpLoadController().XiaoFangBengModel();
            //FileUpLoadController.modelType = "消防设备";
            //当前文件下拉菜单移走
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            //照片滚动框展示
            shujuku1.transform.position = new Vector3(529, 400, 600);
        }
        else if (dpn.captionText.text == "管线")
        {
            //FileUpLoadController.modelType = "管线";
            //当前文件下拉菜单移走
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            //照片滚动框展示
            shujuku2.transform.position = new Vector3(529, 340, 600);
        }
        else if (dpn.captionText.text == "汽车")
        {
            //FileUpLoadController.modelType = "汽车";
            //当前文件下拉菜单移走
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            //照片滚动框展示
            shujuku3.transform.position = new Vector3(529, 380, 600);
        }
        else if (dpn.captionText.text == "阀门")
        {
            //FileUpLoadController.modelType = "阀门";
            //当前文件下拉菜单移走
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);
            //照片滚动框展示
            shujuku4.transform.position = new Vector3(529, 400, 600);
        }
        //如果选择退出
        else if (dpn.captionText.text == "退出")
        {
            //当前文件移走
            dangqianwenjian.transform.position = new Vector3(1464, 139, 600);

            //下拉框内容设为空
            dpn.captionText.text = "";
        }
    }

}
