using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour {
    public GameObject shijiao1;
    public GameObject testlist;
    public Text text;
    public GameObject button_1;
    public GameObject button_2;
    public GameObject button_3;
    public GameObject button_4;
    public GameObject button_5;
    public GameObject button_6;
    public GameObject button_7;
    public GameObject error;
    public GameObject door1;
    public GameObject door2;
    public string lname;
    private bool b1=false;
    private bool b2=false;
    public GameObject button_shangyibu;
    public GameObject button_shangyibu1;
    public GameObject button_shangyibu2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void test_1()
    {
        shijiao1.SetActive(true);
        testlist.SetActive(true);
        button_1.SetActive(false);
        button_2.SetActive(false);
        door1.SetActive(true);
        door2.SetActive(true);
        shijiao1.GetComponent<HighlightingEffect>().enabled = false;
        shijiao1.GetComponent<mouse1>().enabled = false;
        text.text = "请检查液化石油气检验单，检查罐车和接收储罐的液位，压力和温度，检查装卸阀和法兰连接处有无泄露。之后请点击下一步。";
    }
    public void test_2()
    {
        shijiao1.GetComponent<HighlightingEffect>().enabled = true;
        shijiao1.GetComponent<mouse1>().enabled = true;
        text.text = "请选择软管并接入正确的接口";
        button_1.SetActive(true);
        button_2.SetActive(true);
        door1.SetActive(false);
        door2.SetActive(false);
        button_3.SetActive(false);
        button_4.SetActive(false);
        button_5.SetActive(false);
        button_6.SetActive(false);
        button_7.SetActive(false);
    }
    public void qiguan()
    {

        if (lname == "气管")
        {
            text.text = "恭喜你选择正确";
            button_1.SetActive(false);
            b1 = true;
        }
        else
        {
            text.text = "选择错误请重新选择";
        }


    }
    public void yeguan()
    {
       
            if (lname == "液管")
            {
                text.text = "恭喜你选择正确";
                button_2.SetActive(false);
            b2 = true;
            
            }
            else
            {

                text.text = "选择错误请重新选择";
            }
    
    }
    public void test_3()
    {
        if(b1==true && b2==true)
        {
            Debug.Log("true");
            shijiao1.GetComponent<HighlightingEffect>().enabled = false;
            shijiao1.GetComponent<mouse1>().enabled = false;
            text.text = "请选择下一步操作";
            button_3.SetActive(true);
            button_4.SetActive(true);
            b1 = false;
            b2 = false;
            GameObject.Find("Canvas/test/下一步 2").SetActive(false);
        }
        else
        {
            Debug.Log("FALSE");
            error.SetActive(true);
            StartCoroutine(error_fade());
        }
    }
    public void correct()
    {
        text.text = "操作正确";
        button_7.SetActive(true);
        button_3.SetActive(false);
        button_4.SetActive(false);

    }
    public void fault()
    {
        text.text = "操作错误！";
    }
    public void next()
    {
        text.text = "请选择下一步操作";
        button_7.SetActive(false);
        button_5.SetActive(true);
        button_6.SetActive(true);
    }
    public void correct1()
    {
        text.text = "操作正确，接下来请去操作台开始发油操作！";
        button_5.SetActive(false);
        button_6.SetActive(false);
    }
    public void finish()
    {
        GameObject.Find("触发器/触发器").GetComponent<chufa>().kaishizhuangyou();
        //shijiao1.SetActive(false);
        testlist.SetActive(false);

    }
    IEnumerator error_fade()
    {
        Debug.Log("sss");
        yield return new WaitForSeconds(3.0f);
        error.SetActive(false);
    }
}
