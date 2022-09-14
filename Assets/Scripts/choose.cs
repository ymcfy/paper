using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂接在 场景4 Canvas/RawImage_dropdownlist/Dropdown
/// </summary>
public class choose : MonoBehaviour {
    public GameObject dp;
    public GameObject raw;
    public GameObject zhantai;
    public GameObject qiche;
    public GameObject camera;
    public GameObject maincamera;
    public GameObject chufaqi;
    public GameObject peixun;
    public GameObject qiguan;
    public GameObject yeguan;
    public GameObject yeguan1;
    public GameObject qiche_camera;
    public GameObject camera1;
    public Vector3 pos;

    public GameObject button1;
    public GameObject button2;
    public GameObject button2_1;
    public GameObject button2_2;
    public GameObject button2_3;
    public GameObject button3;
    public GameObject button4;
    /// <summary>
    /// 用来存储流程所有button
    /// </summary>
    public List<GameObject> buttons = new List<GameObject>();

    //培训流程中的各个button
    public GameObject methodButton1;
    public GameObject methodButton2;
    public GameObject methodButton3;
    public GameObject methodButton4;
    public GameObject methodButton5;
    public GameObject methodButton6;
    public GameObject methodButton7;
    public GameObject methodButton8;
    public GameObject methodButton9;

    //液化气检验报告单
    public GameObject image;

    //汽车视角
    public GameObject shijiao1;

    //存放读取温度数据的脚本所挂接物体
    public GameObject linechart;

    //培训流程界面
    public GameObject trainningInterface;

    //发油按钮界面
    public GameObject fayouInterface;

    public GameObject door1;
    public GameObject door2;

    public Text text;
    // Use this for initialization
    void Start() {
        buttons.Add(button1);
        buttons.Add(button2);
        buttons.Add(button2_1);
        buttons.Add(button2_2);
        buttons.Add(button2_3);
        buttons.Add(button3);
        buttons.Add(button4);
        raw.SetActive(false);
        dp.GetComponent<Dropdown>();
        peixun.SetActive(false);
        pos = camera1.transform.position;
        qiguan.GetComponent<cube1>().enabled = false;
        yeguan.GetComponent<cube1>().enabled = false;
        yeguan1.GetComponent<cube1>().enabled = false;
        qiguan.GetComponent<cube2>().enabled = false;
        yeguan.GetComponent<cube2>().enabled = false;
        yeguan1.GetComponent<cube2>().enabled = false;
        // zhantai.GetComponent<Add>().enabled = true;
        //将流程所有button存入相应list中
        
    }
	// Update is called once per frame
	void Update () {
	}
    public void skip()
    {
        if (dp.GetComponentInChildren<Dropdown>().options[dp.GetComponentInChildren<Dropdown>().value].text == "灾害模拟")
        {
            zaihaimoni();
        }
        if (dp.GetComponentInChildren<Dropdown>().options[dp.GetComponentInChildren<Dropdown>().value].text == "课程培训")
        {
            gongyiliucheng();
        }
        if (dp.GetComponentInChildren<Dropdown>().options[dp.GetComponentInChildren<Dropdown>().value].text == "工艺流程")
        {
            gongyipeixun();
        }

    }
    public void zaihaimoni()
    {
        raw.SetActive(true);
        //zhantai.GetComponent<Add>().enabled = true;
        dp.SetActive(false);
        


    }
    public void gongyiliucheng()
    {
        image.SetActive(false);
        qiche.SetActive(true);
        camera.SetActive(true);
        maincamera.SetActive(false);
        dp.SetActive(false);
        chufaqi.SetActive(true);
        //qiguan.GetComponent<cube1>().enabled = true;
        //yeguan.GetComponent<cube1>().enabled = true;
        //yeguan1.GetComponent<cube1>().enabled = true;

    }
    public void moni_exit()
    {
        //zhantai.GetComponent<Add>().mesh_des();
        dp.SetActive(true);
        //zhantai.GetComponent<Add>().enabled = false;
        raw.SetActive(false);
       
    }
    public void gongyiliucheng_exit()
    {
        qiche.transform.position=new Vector3(27.5f,0, -35.4f);
        qiche.SetActive(false);
        camera.SetActive(false);
        maincamera.SetActive(true);
        //dp.SetActive(true);
        chufaqi.SetActive(false);
        qiguan.GetComponent<cube1>().enabled = false;
        yeguan.GetComponent<cube1>().enabled = false;
        yeguan1.GetComponent<cube1>().enabled = false;
        qiguan.GetComponent<cube2>().enabled = false;
        yeguan.GetComponent<cube2>().enabled = false;
        yeguan1.GetComponent<cube2>().enabled = false;
        qiche_camera.SetActive(false);
        camera1.transform.position = pos;
        trainningInterface.SetActive(false);
        //button1.SetActive(false);
        //button2.SetActive(false);
        //button3.SetActive(false);
        //button4.SetActive(false);
        //button2_1.SetActive(false);
        //button2_2.SetActive(false);
        //button2_3.SetActive(false);
    }

    /// <summary>
    /// 将除了此button以外的所有button都禁用
    /// </summary>
    /// <param name="button"></param>
    public void enableButton(GameObject button) {
        foreach (GameObject buttonItem in buttons)
        {
            buttonItem.GetComponent<Button>().enabled = false;
        }
        //button.GetComponent<Button>().Select();
    }

    /// <summary>
    /// 场景4  Canvas/RawImage_dingdian/GongYiLiuCheng 使用此函数
    /// 作用是开始进行工艺流程
    /// </summary>
    public void gongyipeixun()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        methodButton1.SetActive(true);
        methodButton2.SetActive(false);
        methodButton3.SetActive(false);
        methodButton4.SetActive(false);
        methodButton5.SetActive(false);
        methodButton6.SetActive(false);
        methodButton7.SetActive(false);
        methodButton8.SetActive(false);
        methodButton9.SetActive(false);
        //将进度按钮中除了1.移动罐车以外的都禁止
        //enableButton(button1);
        //button1.GetComponent<Button>().enabled = true;

        button1.GetComponent<Button>().Select();
        text.text = "1. 将罐车移动到站台";
        qiche.transform.position = new Vector3(27.5f,0,-35.4f);
        image.SetActive(false);
        shijiao1.SetActive(false);
        linechart.SetActive(false);
        trainningInterface.SetActive(true);
        door1.SetActive(true);
        door2.SetActive(true);
        fayouInterface.SetActive(false);

        //button1.SetActive(true);
        //button2.SetActive(true);
        //button3.SetActive(true);
        //button4.SetActive(true);
        //button2_1.SetActive(true);
        //button2_3.SetActive(true);
        //button2_2.SetActive(true);
        qiche.SetActive(true);
        camera.SetActive(true);
        maincamera.SetActive(false);
        dp.SetActive(false);
        peixun.SetActive(true);


        //qiguan.GetComponent<cube2>().enabled = true;
        //yeguan.GetComponent<cube2>().enabled = true;
        //yeguan1.GetComponent<cube2>().enabled = true;
    }
}
