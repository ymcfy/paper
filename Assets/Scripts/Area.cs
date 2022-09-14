using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class Area : MonoBehaviour
{
    public EventSystem eventsystem;
    private Vector3[] vertices;
    //用于记录绘制三角形所需要的顶点ID顺序  
    private int[] triangles;
    public LineRenderer lineRenderer;
    public GameObject Emptyobject;
    private Vector3 position;
    private Vector3[] dis = new Vector3[1000];
    private double distance = 0;
    private string distance_1;
    private float dis_1 = 0;
    private int index = 0;
    private int LengthOfLineRenderer = 0;
    private bool isFollow = true;
    private float mx;
    private float my;
    private Vector3 startPoint;
    private Mesh mesh;
    private MeshRenderer Meshrenderer;
    private int isClockwise;
    private int isPolygonType;
    public DOTweenAnimation warn;
    RaycastHit hitt = new RaycastHit();


    private int a = 0;
    public Object O_prefab_qiu;
    public GameObject point;

    // Use this for initialization
    void Start()
    {
        GameObject Emptyobject = GameObject.Find("area");
        lineRenderer = Emptyobject.GetComponent<LineRenderer>();
        Meshrenderer = Emptyobject.GetComponent<MeshRenderer>();
        mesh = Emptyobject.GetComponent<MeshFilter>().mesh;
        LengthOfLineRenderer = 0;
        lineRenderer.enabled = false;
        Meshrenderer.enabled = false;
        mesh.Clear();
        GameObject.Find("Panel_area/Area").GetComponent<Text>().enabled = true;
        O_prefab_qiu = GameObject.Find("celiang/Sphere");

    }
    public void D_Area()
    {
        if (lineRenderer.enabled == true)
        {
            distance = 0;//重置清零
            dis_1 = 0;
            dis.Initialize();
            GameObject.Find("Panel_area/Area").GetComponent<Text>().text = "";
            LengthOfLineRenderer = 0;
            lineRenderer.SetVertexCount(LengthOfLineRenderer);
            lineRenderer.enabled = false;
            Meshrenderer.enabled = false;
            mesh.Clear();
        }
        else if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
            Meshrenderer.enabled = true;
            // isFollow = true;
        }
    }
    double det(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return (p1.x - p0.x) * (p2.z - p0.z) - (p1.z - p0.z) * (p2.x - p0.x);
    }
    double fabs(double m)
    {
        if (m <= 0.0)
        {
            m = -m;

        }
        else if (m > 0.0)
        {
            m = m;
        }
        return m;
    }
    double ploygon_area(int n, Vector3[] p)
    {
        double s = 0.0f;

        for (int i = 1; i < n - 1; i++)
            s += det(p[0], p[i], p[i + 1]);
        return 0.5 * fabs(s);
    }
    int ClockDirection(Vector3[] points, int Count)
    {
        int i, j, k;
        int m;
        int count = 0;
        double z;
        int yTrans = 1;
        if (points == null || Count < 3)
        {
            m = 0;
            return m;
        }
        int n = Count;
        for (i = 0; i < n; i++)
        {
            j = (i + 1) % n;
            k = (i + 2) % n;
            z = (points[j].x - points[i].x) * (points[k].z * yTrans - points[j].z * yTrans);
            z -= (points[j].z * yTrans - points[i].z * yTrans) * (points[k].x - points[j].x);
            if (z < 0)
            {
                count--;
            }
            else if (z > 0)
            {
                count++;
            }
        }
        if (count > 0)
        {
            m = 0;
            return m;//逆时针
        }
        else
        {
            m = 1;
            return m;//顺时针
        }

    }
    int PolygonType(Vector3[] points, int count)
    {
        int i, j, k;
        int flag = 0;
        int m;
        double z;

        if (points == null || count < 3)
        {
            m = 0;
            return m;
        }
        if (count == 3)
        {
            m = 1;
            return m;
        }
        int n = count;
        int yTrans = 1;
        for (i = 0; i < n; i++)
        {
            j = (i + 1) % n;
            k = (i + 2) % n;
            z = (points[j].x - points[i].x) * (points[k].z * yTrans - points[j].z * yTrans);
            z -= (points[j].z * yTrans - points[i].z * yTrans) * (points[k].x - points[j].x);
            if (z < 0)
            {
                flag |= 1;
            }
            else if (z > 0)
            {
                flag |= 2;
            }
            if (flag == 3)
            {
                m = 0;
                return m;  /// 凹多边形 
            }
        }
        if (flag != 0)
        {
            m = 1;
            return m;  /// 凸多边形 
        }
        else
        {
            m = 0;
            return m;
        }
    }
    public void xiaochu()
    {
        var NAME = GameObject.Find("Sphere" + (a - 1));
        a -= 1;
        Destroy(NAME);
        distance = 0;//重置清零
        dis_1 = 0;
        dis.Initialize();
        LengthOfLineRenderer = 0;
        lineRenderer.SetVertexCount(LengthOfLineRenderer);
        GameObject.Find("Panel_area/Area").GetComponent<Text>().text = "";

    }
    void Update()
    {
        distance_1 = distance.ToString("F3");
        GameObject.Find("Panel_area/Area").GetComponent<Text>().text = "测量面积：" + distance_1 + " 平方米";
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //Debug.Log("123456上");
        if (lineRenderer.enabled == true)
        {
            if (Input.GetMouseButtonDown(2))
            {
                GetComponent<Btn_control>().mianji_warning_button();
                lineRenderer.enabled = true;
                Meshrenderer.enabled = true;
            }
            //if (Input.GetMouseButtonDown(0))//左键画线
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())

                    Debug.Log("当前触摸在UI上");
                else if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log(hit.point);
                    position = hit.point;//碰撞点坐标
                    position.y = hit.point.y + 0.1f;
                    point = GameObject.Instantiate(O_prefab_qiu, hit.point, transform.rotation) as GameObject;
                    //point.gameObject.AddComponent<clickmove> ();
                    point.name = "Sphere" + a;
                    a += 1;

                    //画点
                    LengthOfLineRenderer++;//端点点数加1				
                    lineRenderer.SetVertexCount(LengthOfLineRenderer);//设置端点数
                    lineRenderer.SetPosition(LengthOfLineRenderer - 1, position);
                    dis[LengthOfLineRenderer - 1] = position;

                    //如果划出了三个点   就要生成第四个点，其位置与第一个点相同
                    //如果构成了图形就计算面积
                    if (LengthOfLineRenderer >= 3)
                    {
                        LengthOfLineRenderer++;
                        lineRenderer.SetVertexCount(LengthOfLineRenderer);
                        dis[LengthOfLineRenderer - 1] = dis[0];
                        lineRenderer.SetPosition(LengthOfLineRenderer - 1, dis[LengthOfLineRenderer - 1]);
                        LengthOfLineRenderer -= 1;
                        isPolygonType = PolygonType(dis, LengthOfLineRenderer);
                        if (isPolygonType == 1)
                        {
                            distance = ploygon_area(LengthOfLineRenderer, dis);
                            isClockwise = ClockDirection(dis, LengthOfLineRenderer);
                            triangles = new int[3 * (LengthOfLineRenderer - 2)];
                            //根据顶点数来创建记录顶点坐标  
                            vertices = new Vector3[LengthOfLineRenderer];
                            //将链表中的顶点坐标赋值给vertices  
                            if (isClockwise == 1)
                            {
                                for (int i = 0; i < LengthOfLineRenderer; i++)
                                {
                                    vertices[i] = dis[LengthOfLineRenderer - 1 - i];
                                }
                            }
                            else
                            {
                                for (int i = 0; i < LengthOfLineRenderer; i++)
                                {
                                    vertices[i] = dis[i];
                                }
                            }

                            //三角形个数  
                            int triangles_count = LengthOfLineRenderer - 2;
                            //根据三角形的个数，来计算绘制三角形的顶点顺序（索引）  
                            for (int i = 0; i < triangles_count; i++)
                            {
                                triangles[3 * i] = 0;
                                triangles[3 * i + 1] = i + 2;
                                triangles[3 * i + 2] = i + 1;
                            }
                            //设置顶点坐标  
                            mesh.vertices = vertices;
                            //设置顶点索引  
                            mesh.triangles = triangles;
                            //LengthOfLineRenderer = LengthOfLineRenderer - 1;
                        }
                        else
                        {
                            distance = 0;//重置清零
                            dis_1 = 0;
                            dis.Initialize();
                            LengthOfLineRenderer = 0;
                            lineRenderer.SetVertexCount(LengthOfLineRenderer);
                            lineRenderer.enabled = false;
                            Meshrenderer.enabled = false;
                            mesh.Clear();
                            GameObject.Find("Panel_area/Panel").transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
                        }
                    }
                }
                Debug.Log("当前没有触摸在UI上");
            }

        }
    }

}



