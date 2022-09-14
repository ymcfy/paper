using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class danger_area1 : MonoBehaviour
{
    public GameObject duqi;
    public GameObject duqi1;
    public Camera camera1;
    public GameObject text;
    public GameObject weizhi11;
    public GameObject weizhi12;
    public GameObject weizhi13;
    public GameObject shuzhi1;
    public GameObject shuzhi2;
    public GameObject shuzhi3;
    public GameObject shuzhi4;
    private Vector3 b, c;
    public Text text1;
    public GameObject menu;

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clear()
    {
        foreach (Transform T in this.GetComponentsInChildren<Transform>())
        {
            if (T.name.Contains("x"))
            {
                T.GetComponentInChildren<Text>().text = "";
            }
        }
    }
    public void exit1()
    {
        shuzhi1.transform.position = new Vector3(-860, 353, 0);
        for (int i = 0; i < GameObject.Find("Canvas/数据库1").transform.childCount; i++)
        {
            if (GameObject.Find("Canvas/数据库1").transform.GetChild(i).name.Contains("a"))
            {
                InputField input = GameObject.Find("Canvas/数据库1").transform.GetChild(i).GetComponent<InputField>();
                input.text = "";
            }
        }
    }
    public void exit2()
    {
        shuzhi2.transform.position = new Vector3(-460, 353, 0);
        for (int i = 0; i < GameObject.Find("Canvas/数据库2").transform.childCount; i++)
        {
            if (GameObject.Find("Canvas/数据库2").transform.GetChild(i).name.Contains("a"))
            {
                InputField input = GameObject.Find("Canvas/数据库2").transform.GetChild(i).GetComponent<InputField>();
                input.text = "";
            }
        }
    }
    public void exit3()
    {
        shuzhi3.transform.position = new Vector3(-660, 353, 0);
        for (int i = 0; i < GameObject.Find("Canvas/数据库3").transform.childCount; i++)
        {
            if (GameObject.Find("Canvas/数据库3").transform.GetChild(i).name.Contains("a"))
            {
                InputField input = GameObject.Find("Canvas/数据库3").transform.GetChild(i).GetComponent<InputField>();
                input.text = "";
            }
        }
    }
    public void exit4()
    {
        shuzhi4.transform.position = new Vector3(-1060, 353, 0);
        for (int i = 0; i < GameObject.Find("Canvas/数据库4").transform.childCount; i++)
        {
            if (GameObject.Find("Canvas/数据库4").transform.GetChild(i).name.Contains("a"))
            {
                InputField input = GameObject.Find("Canvas/数据库4").transform.GetChild(i).GetComponent<InputField>();
                input.text = "";
            }
        }
    }
}
