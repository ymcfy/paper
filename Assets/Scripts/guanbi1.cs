using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guanbi1 : MonoBehaviour
{

    private GameObject UI;
    void Awake()
    {
        UI = this.gameObject;
    }
    void Start()
    {
        foreach (Transform ss in UI.transform)//找到下面的所有子物体，将他们设置为隐藏状态；
        {
            ss.gameObject.SetActive(false);
        }
    }
}
