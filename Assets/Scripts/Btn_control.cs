using DG.Tweening;
using ExcelData.DirectReading;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂接在 场景1 MainCamera
/// </summary>
public class Btn_control : MonoBehaviour
{

    public bool ischangdu = false;
    public LineRenderer linerenderer;
    public bool ismianji = false;
    public bool istiji = false;
    public string xname;
    public GameObject xinxiban;
    public GameObject mingzi;

    private void Start()
    {
        xinxiban.SetActive(false);
    }

    private void Update()
    {
    }
    public void kaiqixiangxixinxi() //详细信息显示
    {
        xinxiban.SetActive(true);//详细信息组件开启显示
        GetComponent<test123>().enabled = true; //启动数据库脚本
    }
    /// <summary>
    /// 停止显示详细信息
    /// </summary>
    public void guanbixiangxixinxi()
    {
        xinxiban.SetActive(false);
        mingzi.GetComponent<Text>().text = "";
        GetComponent<test123>().enabled = false;
        GetComponent<test123>().lname = "";
        //暂时注释  4月12
        //GameObject.Find("Canvas/RawImage_cx/biaoti").GetComponent<Text>().text = "";
        
    }

    /// <summary>
    /// 场景1 的Canvas/RawImage_cl/tiji 引用此函数
    /// 作用是开始进行体积测量
    /// </summary>
    public void tiji()
    {
        //在测量体积时禁用鼠标滚轮控制视野的功能
        GameObject.Find("Main Camera").GetComponent<CameraControl>().enabled = false;

        istiji = !istiji;
        StartCoroutine(IE_tiji());
        if (istiji)
        {
            GameObject.Find("Panel_area/Area").GetComponent<Text>().text = "";
            GameObject.Find("Panel_long/Distance").GetComponent<Text>().text = "";
            if (ischangdu)
            {
                changdu();
            }
            if (ismianji)
            {
                mianji();
            }
            GameObject.Find("Panel_volume").transform.DOLocalMove(new Vector3(0, -345, 0), 0.5f);

            GetComponent<Volume>().enabled = true;
        }
        if (!istiji)
        {
            GetComponent<Volume>().enabled = false;
            GameObject.Find("Panel_volume").transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            for (int cishu = 30; cishu >= 0; cishu--)
            {
                GetComponent<Volume>().xiaochu();
            }
        }
    }
    public void mianji()
    {
        ismianji = !ismianji;
        StartCoroutine(IE_mianji());
        if (ismianji)
        {
            GameObject.Find("Panel_long/Distance").GetComponent<Text>().text = "";
            GameObject.Find("Panel_volume/Volume").GetComponent<Text>().text = "";
            if (ischangdu)
            {
                changdu();
            }
            if (istiji)
            {
                tiji();
            }
            GameObject.Find("Panel_area").transform.DOLocalMove(new Vector3(0, -345, 0), 0.5f);
            GetComponent<Area>().enabled = true;
        }
        if (!ismianji)
        {
            GameObject.Find("Panel_area").transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            for (int cishu = 30; cishu >= 0; cishu--)
            {
                GetComponent<Area>().xiaochu();
            }
            GetComponent<Area>().enabled = false;

        }
    }
    public void changdu()
    {
        ischangdu = !ischangdu;
        StartCoroutine(IE_changdu());
        if (ischangdu)
        {
            GameObject.Find("Panel_area/Area").GetComponent<Text>().text = "";
            GameObject.Find("Panel_volume/Volume").GetComponent<Text>().text = "";
            if (ismianji)
            {

                mianji();
            }
            if (istiji)
            {
                tiji();
            }
            //改变按钮颜色
            GetComponent<Drawline_test>().enabled = true;
            GameObject.Find("Panel_long").transform.DOLocalMove(new Vector3(0, -345, 0), 0.5f);
        }
        if (!ischangdu)
        {
            for (int cishu = 30; cishu >= 0; cishu--)
            {

                GetComponent<Drawline_test>().xiaochu();
            }
            GetComponent<Drawline_test>().enabled = false;
            GameObject.Find("Panel_long").transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
        }
    }
    public void mianji_warning_button()
    {
        GetComponent<Area>().D_Area();
        GameObject.Find("Panel_area/Panel").transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
        for (int cishu = 30; cishu >= 0; cishu--)
        {
            GetComponent<Area>().xiaochu();
        }
    }
    public void tiji_warning_button()
    {
        GetComponent<Volume>().D_Volume();
        //GameObject.Find("Panel_volume/Panel").transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
        for (int cishu = 30; cishu >= 0; cishu--)
        {
            GetComponent<Volume>().xiaochu();
        }
    }
    private IEnumerator IE_changdu()
    {
        yield return new WaitForSeconds(0.11f);
        GetComponent<Drawline_test>().Distance();
        yield return null;
    }
    private IEnumerator IE_mianji()
    {
        yield return new WaitForSeconds(0.11f);
        GetComponent<Area>().D_Area();
        yield return null;
    }
    private IEnumerator IE_tiji()
    {
        yield return new WaitForSeconds(0.11f);
        GetComponent<Volume>().D_Volume();
        yield return null;
    }
    public void xiaochu_mokuai()
    {
        if (ischangdu)
        {
            for (int cishu = 30; cishu >= 0; cishu--)
            {
                GetComponent<Drawline_test>().xiaochu();
            }
        }
        if (ismianji)
        {
            for (int cishu = 30; cishu >= 0; cishu--)
            {
                GetComponent<Area>().xiaochu();
            }
        }
        if (istiji)
        {
            for (int cishu = 30; cishu >= 0; cishu--)
            {
                GetComponent<Volume>().xiaochu();
            }
        }
    }
}
