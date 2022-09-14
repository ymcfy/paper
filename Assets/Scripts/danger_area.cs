using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 数值模拟等相关部分的代码，挂载在main camera上
/// </summary>
public class danger_area : MonoBehaviour
{
    public GameObject duqi;
    public GameObject duqi1;
    public Camera camera1;
    public GameObject text;
    public GameObject weizhi11;
    public GameObject weizhi12;
    public GameObject weizhi13;
    public GameObject shuzhi;
    private Vector3 b, c;
    public Text text1;
    public GameObject menu;
    public GameObject zhunbei;

    // Use this for initialization
    void Start()
    {
        camera1.GetComponent<Transform>();
        shuzhi.GetComponent<Transform>();
        weizhi11.SetActive(false);
        weizhi12.SetActive(false);
        weizhi13.SetActive(false);
        text.GetComponent<Transform>();
        b = text.transform.position;
        //c = shuzhi.transform.position;
        zhunbei.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void xielouxuanze()
    {
        menu.SetActive(false);
        zhunbei.SetActive(true);
    }

    public void qidong()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        weizhi11.SetActive(true);
        weizhi12.SetActive(true);
        weizhi13.SetActive(true);
        camera1.transform.position = new Vector3(40, 12, -111);
        camera1.transform.rotation = Quaternion.Euler(0, 0, 0);
        text.transform.position = new Vector3(500, 500, 0);
        //shuzhi.transform.position = new Vector3(100f, 500f, 0f);
        zhunbei.SetActive(false);
    }
    public void weizhi1()
    {
        duqi1 = Instantiate(duqi, new Vector3(29.8f, 0, -70f), Quaternion.identity);
        camera1.transform.position = new Vector3(40, 8, -100);
        shuzhi.SetActive(true);
        //shuzhi.transform.position = new Vector3(875.5f, 583f, 0f);
        guanbi();
    }
    public void weizhi2()
    {
        duqi1 = Instantiate(duqi, new Vector3(42.7f, 0, -70f), Quaternion.identity);
        camera1.transform.position = new Vector3(40, 8, -100);
        shuzhi.SetActive(true);
        //shuzhi.transform.position = new Vector3(875.5f, 583f, 0f);
        guanbi();
    }
    public void weizhi3()
    {
        duqi1 = Instantiate(duqi, new Vector3(54.7f, 0, -70f), Quaternion.identity);
        camera1.transform.position = new Vector3(40, 8, -100);
        shuzhi.SetActive(true);
        //shuzhi.transform.position = new Vector3(875.5f, 583f, 0f);
        guanbi();
    }
    public void guanbi()
    {
        weizhi11.SetActive(false);
        weizhi12.SetActive(false);
        weizhi13.SetActive(false);
        text.transform.position = b;

    }
    public void exit()
    {
        //shuzhi.transform.position = c;
        shuzhi.SetActive(false);
        text.transform.position = b;
        Destroy(duqi1);
        camera1.transform.position = new Vector3(40, 12, -111);
        camera1.transform.rotation = Quaternion.Euler(0, 0, 0);
        text1.text = "";
        //zhunbei.SetActive(true);
        weizhi11.SetActive(false);
        weizhi12.SetActive(false);
        weizhi13.SetActive(false);
    }

}
