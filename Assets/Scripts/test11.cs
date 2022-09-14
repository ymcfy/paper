using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 灭火相关
/// </summary>
public class test11 : MonoBehaviour
{
    public Text text;
    public GameObject exit;
    private int timeCount = 0;
    /// <summary>
    /// 灭火器水流
    /// </summary>
    public GameObject water;
    void Start()
    {
    }
    public float timer =3f;
    void Update()
    {
        if(GameObject.Find("kongzhi2").GetComponent<cameraSwitch>().flag==true)
        {
            count();
        }
    }
    void count()
    {
        Debug.Log("开始");
        Debug.Log(timer);
        timer -= Time.deltaTime;
        text.text = "正在灭火中";
        if (timer <= 0)
        {
            text.text = "您已成功灭火!";
            exit.SetActive(true);
            this.gameObject.SetActive(false);
            //关闭水流
            water.SetActive(false);
            GameObject.Find("renwu").GetComponent<Animator>().enabled = false;
             timer = 3f;
        }
    }
}
