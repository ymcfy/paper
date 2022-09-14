using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// text的font定义方法，此处bug出了三四天，多亏苏畅师兄不厌其烦地帮忙指导，解决了这个问题。
/// 原因在于Zhanshi()的ZhanShiTupian()的foreach循环处，程序处于操作文件过程，故无法修改font。
/// 其实理解得还不是很深，不过记住了解决办法，以免以后再遇到此种问题
/// </summary>
public class test1 : MonoBehaviour
{

    //Canvas下一个Text
    public GameObject ts;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //找到此Text
        ts = GameObject.Find("Canvas3/Text");

        //将本脚本挂接的Text的font设置为ts的font
        this.GetComponent<Text>().font = ts.GetComponent<Text>().font;

        this.GetComponent<Text>().alignment = (TextAnchor)LineAlignment.Local;

        this.GetComponent<Text>().color = ts.GetComponent<Text>().color;

        this.GetComponent<Text>().transform.localScale = ts.GetComponent<Text>().transform.localScale;
    }
}
