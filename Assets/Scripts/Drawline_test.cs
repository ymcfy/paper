using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Drawline_test : MonoBehaviour
{
    public LineRenderer linerender;
    public string distance_1;
    public Vector3 hit_potion;
    public Object O_prefab_qiu;
    public int LengthOfLineRenderer = 0;
    public int i = 0;
    public float distance = 0;
    private Vector3[] dis = new Vector3[1000];
    public float dis_1 = 0;
    public GameObject point;


    void Start()
    {
        linerender = GameObject.Find("celiang/line").GetComponent<LineRenderer>();
        O_prefab_qiu = GameObject.Find("celiang/Sphere");

    }
    public void Distance()
    {
        if (linerender.enabled == true)
        {
            print("true");
            distance = 0;//重置清零
            dis_1 = 0;
            LengthOfLineRenderer = 0;
            linerender.SetVertexCount(LengthOfLineRenderer);
            linerender.enabled = false;
            GameObject.Find("Panel_long/Distance").GetComponent<Text>().text = "";

        }
        else if (linerender.enabled == false)
        {
            print("false");
            linerender.enabled = true;
        }
    }
    void Update()
    {

        distance_1 = distance.ToString("F3");
        GameObject.Find("Panel_long/Distance").GetComponent<Text>().text = "测量距离为：" + distance_1 + " 米";
        //左键画点
        if (Input.GetMouseButtonDown(0) && linerender.enabled == true)
        {
            print("111");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (EventSystem.current.IsPointerOverGameObject())
                Debug.Log("当前触摸在UI上");
            else if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                //if (hit.transform.gameObject.name == "polySurface5087" || hit.transform.gameObject.name == "polySurface4779")
                //{
                hit_potion = hit.point;//碰撞点坐标
                hit_potion.y = hit.point.y + 0.1f;
                point = GameObject.Instantiate(O_prefab_qiu, hit.point, transform.rotation) as GameObject;
                point.name = "Sphere" + i;
                i += 1;
                LengthOfLineRenderer++;
                linerender.SetVertexCount(LengthOfLineRenderer);//设置端点数
                linerender.SetPosition(LengthOfLineRenderer - 1, hit_potion);
                dis[LengthOfLineRenderer - 1] = point.transform.position;
                if (LengthOfLineRenderer >= 2)
                {
                    distance += Vector3.Distance(dis[LengthOfLineRenderer - 2], dis[LengthOfLineRenderer - 1]);
                    //计算长度

                }
                else
                {
                    distance = 0;
                    dis_1 = 0;
                }

            }
        }
        //右键撤销
        if (Input.GetMouseButtonDown(2))
        {
            if (LengthOfLineRenderer >= 2)
            {
                dis_1 = Vector3.Distance(dis[LengthOfLineRenderer - 2], dis[LengthOfLineRenderer - 1]);
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                xiaochu();
                if (LengthOfLineRenderer >= 1)
                {
                    LengthOfLineRenderer--;
                    linerender.SetVertexCount(LengthOfLineRenderer);
                    if (LengthOfLineRenderer >= 2)
                    {
                        distance = distance - dis_1;
                    }
                    else
                    {
                        distance = 0;
                        dis_1 = 0;
                    }
                }
            }
        }
    }

    public void xiaochu()
    {
        var NAME = GameObject.Find("Sphere" + i);
        i -= 1;
        Destroy(NAME);

        distance = 0;//重置清零
        dis_1 = 0;
        LengthOfLineRenderer = 0;
        linerender.SetVertexCount(LengthOfLineRenderer);
        GameObject.Find("Panel_long/Distance").GetComponent<Text>().text = "";
    }

}
