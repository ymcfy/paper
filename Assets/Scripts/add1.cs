using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add1 : MonoBehaviour
{
    public GameObject go;
    private int count;
    // Use this for initialization
    void Start()
    {
        count = go.transform.childCount;
        for (int i = 0; i < count; i++)
        {

            //给所有的子物体添加上interavtive脚本
            go.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
            go.transform.GetChild(i).gameObject.AddComponent<cube1>();
            //给所有的子元素添加objplay脚本


        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
