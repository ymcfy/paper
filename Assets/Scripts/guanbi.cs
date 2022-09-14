using UnityEngine;
using System.Collections;


public class guanbi : MonoBehaviour
{

    private GameObject UI;
    void Awake()
    {
        UI = this.gameObject;
    }
    void Start()
    {
    }
    public void quanbu()
    {
        foreach (Transform ss in UI.transform)//找到下面的所有子物体，将他们设置为隐藏状态；
        {
            ss.gameObject.SetActive(false);
        }
    }
    public void guanxian()
    {
        foreach (Transform ss in UI.transform)//找到下面的所有子物体，将他们设置为隐藏状态；
        {
            ss.gameObject.SetActive(true);
        }
    }

}

