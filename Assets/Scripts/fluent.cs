using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class fluent : MonoBehaviour
{
    public GameObject[] gx;
    public GameObject[] c;
    public GameObject camera1;
    public Text text;
    public bool p;
    public float timer;
    public float time1;
    public GameObject caidan;
    public GameObject button;
    public Material m1;
    public Material m2;
    public Material m3;
    public Vector3 pos;
    // Use this for initialization
    void Start()
    {
        p = false;
        gx[0].GetComponent<FluentTex1>().enabled = false;
        gx[1].GetComponent<fluentTex2>().enabled = false;
        gx[2].GetComponent<fluentTex2>().enabled = false;
        c[0].SetActive(false);
        c[1].SetActive(false);
        c[2].SetActive(false);
        //pos = c[2].transform.position;
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        timer = Time.time;
        time1 = Time.time - timer;
        p = true;
        camera1.SetActive(false);
        gx[0].GetComponent<FluentTex1>().enabled = true;
        // m1 = gx[0].GetComponent<fluentTex2>().m;
        c[0].SetActive(true);
        //camera1.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {

        if (p == true)
        {
            if (Time.time - timer > 6 && Time.time - timer < 11)
            {


                c[0].SetActive(false);
                gx[1].GetComponent<fluentTex2>().enabled = true;
                //m2= gx[1].GetComponent<fluentTex2>().m;
                c[1].SetActive(true);


            }
            if (Time.time - timer > 11)
            {
                c[1].SetActive(false);
                gx[2].GetComponent<fluentTex2>().enabled = true;
                // m3 = gx[2].GetComponent<fluentTex2>().m;
                c[2].SetActive(true);
                //button.SetActive(true);
                c[2].transform.DOLocalMoveX(18.5f, 9f);

            }
            if (Time.time - timer > 22)
            {
                //c[2].transform.position=pos;
                text.text = "";
                gx[0].GetComponent<FluentTex1>().m.SetTextureScale("_MainTex", new Vector2(0, 0.2f));
                gx[0].GetComponent<FluentTex1>().m.SetTextureOffset("_MainTex", new Vector2(0, 0));
                gx[0].GetComponent<FluentTex1>().time = 0;
                gx[0].GetComponent<FluentTex1>().dir = 0;
                gx[1].GetComponent<fluentTex2>().m1.SetTextureScale("_MainTex", new Vector2(0, 5));
                gx[1].GetComponent<fluentTex2>().m1.SetTextureOffset("_MainTex", new Vector2(0, 0));
                gx[1].GetComponent<fluentTex2>().time = 0;
                gx[1].GetComponent<fluentTex2>().dir = 0;
                gx[2].GetComponent<fluentTex2>().m1.SetTextureScale("_MainTex", new Vector2(0, 5));
                gx[2].GetComponent<fluentTex2>().m1.SetTextureOffset("_MainTex", new Vector2(0, 0));
                gx[2].GetComponent<fluentTex2>().time = 0;
                gx[2].GetComponent<fluentTex2>().dir = 0;
                gx[0].GetComponent<FluentTex1>().enabled = false;
                gx[1].GetComponent<fluentTex2>().enabled = false;
                gx[2].GetComponent<fluentTex2>().enabled = false;
                gx[0].GetComponent<Renderer>().material = m1;
                gx[1].GetComponent<Renderer>().material = m2;
                gx[2].GetComponent<Renderer>().material = m3;
                c[0].SetActive(false);
                c[1].SetActive(false);
                c[2].SetActive(false);
                p = false;
                camera1.SetActive(true);
                //button.SetActive(true);

            }
        }
    }
    public void gystart()
    {
        GameObject.Find("青岛炼化-汽油装卸罐车/视角1").SetActive(false);
        StartCoroutine(Timer());
        caidan.SetActive(false);
        camera1.SetActive(true);
        //GameObject.Find("液化气装卸站台").GetComponent<Add>().mesh_des();
        GameObject.Find("青岛炼化-汽油装卸罐车").GetComponent<Carcontrol>().enabled = true;
        //GameObject.Find("Camera").SetActive(false);
    }
}
