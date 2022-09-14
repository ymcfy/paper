using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kuosan3 : MonoBehaviour
{

    public InputField inputx;
    public InputField inputy;
    public InputField inputz;
    public InputField inputtime;
    public InputField inputZhiZaoShang;
    public InputField inputChuChangShiJian;
    public InputField inputAnZhuangDiDian;
    public InputField inputQiYongShiJian;
    public InputField inputLiuLiang;
    public InputField inputZuiDaGongZuoYaLi;
    public InputField inputZhuanSu;
    public InputField inputGongLv;

    private string x;
    private string y;
    private string z;
    private string time;
    private string ZhiZaoShang;
    private string ChuChangShiJian;
    private string AnZhuangDiDian;
    private string QiYongShiJian;
    private string LiuLiang;
    private string ZuiDaGongZuoYaLi;
    private string ZhuanSu;
    private string GongLv;



    public GameObject kuosan1;
    float timer = 5f;

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
        ZhiZaoShang = Convert.ToString(inputZhiZaoShang.text);
        ChuChangShiJian = Convert.ToString(inputChuChangShiJian.text);
        AnZhuangDiDian = Convert.ToString(inputAnZhuangDiDian.text);
        QiYongShiJian = Convert.ToString(inputQiYongShiJian.text);
        LiuLiang = Convert.ToString(inputLiuLiang.text);
        ZuiDaGongZuoYaLi = Convert.ToString(inputZuiDaGongZuoYaLi.text);
        ZhuanSu = Convert.ToString(inputZhuanSu.text);
        GongLv = Convert.ToString(inputGongLv.text);

        //FileUpLoadController.SheBeiMingCheng = x;
        //FileUpLoadController.GuDingZiChanBianHao = y;
        //FileUpLoadController.XingHao = z;
        //FileUpLoadController.GuiGe = time;
        //FileUpLoadController.ZhiZaoShang = ZhiZaoShang;
        //FileUpLoadController.ChuChangShiJian = ChuChangShiJian;
        //FileUpLoadController.AnZhuangDiDian = AnZhuangDiDian;
        //FileUpLoadController.QiYongShiJian = QiYongShiJian;
        //FileUpLoadController.LiuLiang = LiuLiang;
        //FileUpLoadController.ZuiDaGongZuoYaLi = ZuiDaGongZuoYaLi;
        //FileUpLoadController.ZhuanSu = ZhuanSu;
        //FileUpLoadController.GongLv = GongLv;
        //new FileUpLoadController().QiCheModel();
    }
}
