using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qidong : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 此函数挂接在场景漫游模块下的定点查看按钮里，作用是对每一个定点进行视角转变
    /// </summary>
    public void chazhao()
    {
        //Debug.Log("111");
        //打开对应定点下面的摄像头
        this.transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Main Camera").SetActive(false);
        GameObject.Find("Canvas (1)/pos1").SetActive(false);
        GameObject.Find("Canvas (1)/pos2").SetActive(false);
        GameObject.Find("Canvas (1)/pos3").SetActive(false);
        GameObject.Find("Canvas (1)/pos4").SetActive(false);
        GameObject.Find("Canvas (1)/pos5").SetActive(false);
    }
}
