using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chufa : MonoBehaviour
{
    public Text text;
    public GameObject te;
    public GameObject kz;
    public GameObject chufaqi;
    public GameObject caozuotai;
    private bool panduan = false;
    gongyiliucheng gy;
    // Use this for initialization
    void Start()
    {
        gy = kz.GetComponent<gongyiliucheng>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G) && panduan == true)
        {
            text.text = "";
            panduan = false;
            te.SetActive(false);
            GameObject.Find("kongzhi2").GetComponent<test>().test_1();
            GameObject.Find("Camera").SetActive(false);
            GameObject.Find("青岛炼化-汽油装卸罐车").GetComponent<Carcontrol>().enabled = false;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        te.SetActive(true);

        text.text = "请按G开始装油";
        panduan = true;


    }
    private void OnCollisionExit(Collision collision)
    {
        text.text = "请移动到指定位置";
        panduan = false;
    }
    public void kaishizhuangyou()
    {
        //text.text = "请前往控制台操作";
        chufaqi.SetActive(false);
        caozuotai.SetActive(true);
        //renwu.SetActive(true);
        //te.SetActive(true);
        //GameObject.Find("液化气装卸站台").GetComponent<Add>().enabled = true;
        //GameObject.Find("青岛炼化-汽油装卸罐车").GetComponent<Carcontrol>().enabled = false;
    }
}
