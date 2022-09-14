using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kuosan0 : MonoBehaviour
{
    public GameObject kuosan1;
    float timer = 5f;
    public InputField inputx;
    public InputField inputy;
    public InputField inputz;
    public InputField inputtime;
    private string x;
    private string y;
    private string z;
    private string time;
    static float time1;
    private float V;
    public Text text;
    danger_area1 Du;
    void Start()
    {

        Du = GameObject.Find("Main Camera").GetComponent<danger_area1>();

    }



    // Update is called once per frame
    void Update()
    {


    }
    public void kuosan11()
    {
        x = Convert.ToString(inputx.text);
        y = Convert.ToString(inputy.text);
        z = Convert.ToString(inputz.text);
        time = Convert.ToString(inputtime.text);

        //FileUpLoadController.SheBeiMingCheng = x;
        //FileUpLoadController.GuDingZiChanBianHao = y;
        //FileUpLoadController.XingHao = z;
        //FileUpLoadController.GuiGe = time;
        //new FileUpLoadController().XiaoFangBengModel();
    }
}
