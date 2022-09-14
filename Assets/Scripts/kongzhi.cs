using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class kongzhi : MonoBehaviour
{

    public GameObject moxing;
    public GameObject guanxian1;
    public Dropdown dropdown;
    public GameObject xianshi;
    public GameObject queding;
    public GameObject shanchu;
    public GameObject quxiao;


    void Start()
    {
        queding.SetActive(true);
        xianshi.SetActive(false);
        shanchu.SetActive(true);
        quxiao.SetActive(false);
        //foreach (Transform ss in moxing.transform)//找到下面的所有子物体，将他们设置为隐藏状态；
        //{
        //    ss.gameObject.SetActive(true);
        //}
        //foreach (Transform ss in guanxian1.transform)//找到下面的所有子物体，将他们设置为隐藏状态；
        //{
        //    ss.gameObject.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void quanbu()
    {
        queding.SetActive(true);
        xianshi.SetActive(false);
        shanchu.SetActive(true);
        quxiao.SetActive(false);
        guanxian1.GetComponent<laqu2>().enabled = false;
        moxing.GetComponent<laqu1>().enabled = true;
    }
    public void guanxian()
    {
        queding.SetActive(false);
        xianshi.SetActive(true);
        shanchu.SetActive(false);
        quxiao.SetActive(true);
        guanxian1.GetComponent<laqu2>().enabled = true;
        moxing.GetComponent<laqu1>().enabled = false;
    }
}


