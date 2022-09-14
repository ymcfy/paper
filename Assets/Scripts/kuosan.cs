using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kuosan : MonoBehaviour
{
    public GameObject kuosan1;
    float timer = 5f;
    public InputField inputx;
    public InputField inputy;
    public InputField inputz;
    public InputField inputtime;
    private float x;
    private float y;
    private float z;
    private float time;
    static float time1;
    private float V;
    public Text text;
    danger_area Du;
    private int count;
    public GameObject shuzhi;

    void Start()
    {

        Du = GameObject.Find("Main Camera").GetComponent<danger_area>();

    }

    IEnumerator Timer()
    {

            time1 = Time.time;
            Debug.Log(Time.time - time1);

            while (Time.time - time1 <= time + 0.221f)
            {
                yield return new WaitForSeconds(0.1f);

                kuosan1.transform.localScale += new Vector3(x / 10, y / 10, z / 10);
                count = kuosan1.transform.childCount;
                for (int i = 0; i < count; i++)
                {
                    kuosan1.transform.GetChild(i).localScale += new Vector3(x / 300, y / 300, z / 300);


                }
                text.text = "实时X的扩散:" + kuosan1.transform.localScale.x + "\n" + "实时Y的扩散:" + kuosan1.transform.localScale.y + "\n" + "实时Z的扩散:" + kuosan1.transform.localScale.z + "\n" + "时间:" + (Time.time - time1) + "\n";


            }
            text.text = "危险区域为" + V / 2 + "m³\n" + "核心危险区域为黄色范围。";
            count = kuosan1.transform.childCount;

       

    }

    // Update is called once per frame
    void Update()
    {
        if (Du.duqi1 != null)
        {
            kuosan1 = Du.duqi1;
        }

    }
    public void kuosan11()
    {
        x = Convert.ToSingle(inputx.text);
        y = Convert.ToSingle(inputy.text);
        z = Convert.ToSingle(inputz.text);
        time = Convert.ToSingle(inputtime.text);
        V = 4 / 3 * Mathf.PI * x * y * z;
        StartCoroutine(Timer());
        //shuzhi.transform.position = new Vector3(875.5f, 583f, 0f);
        //shuzhi.SetActive(false);

    }
    public void chongzhi()
    {
        Destroy(kuosan1);
        text.text = "";
        Du.qidong();
    }
}
