using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;
using System.Text;

public class xieru : MonoBehaviour
{


    public GameObject go;//给所有的子元素的物体添加一个脚本 该物体是所有元素的父元素
    private int count;     //计算所有子元素的个数
    private int sum = 0;

    // Use this for initialization
    void Start()
    {
        count = go.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            File.AppendAllText("E:\\123.txt", go.transform.GetChild(i).name + '\n', Encoding.Default);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
