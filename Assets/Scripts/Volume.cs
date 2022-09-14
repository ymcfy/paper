using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class Volume : MonoBehaviour
{
    private Vector3[] vertices;
    //用于记录绘制三角形所需要的顶点ID顺序  
    private int[] triangles;
    private int[] triangles_f;
    public Material mat;
    public LineRenderer lineRenderer;
    //public GameObject Emptyobject;
    private Vector3 position;
    private Vector3[] dis = new Vector3[1000];
    private Vector3[] dis_b = new Vector3[1000];
    private Vector3[] dis_m = new Vector3[1000];
    Vector4[] screenPos = new Vector4[1000];
    private double distance = 0;
    private string distance_1;
    private float dis_1 = 0;
    private int index = 0;
    private int LengthOfLineRenderer = 0;
    private bool isFollow;
    private float mx;
    private float my;
    private Vector3 startPoint;
    private Mesh mesh;
    private MeshRenderer Meshrenderer;
    private int isClockwise;
    private int isPolygonType;
    int count_v;
    int count_p;
    private float height = 1;

    RaycastHit hitt = new RaycastHit();
    private int ab = 0;
    private Object O_prefab_qiu;
    private GameObject point;
    // Use this for initialization
    void Start()
    {
        EventSystem eventSystem;
        lineRenderer = GameObject.Find("volume").GetComponent<LineRenderer>();
        Meshrenderer = GameObject.Find("volume").GetComponent<MeshRenderer>();
        mesh = GameObject.Find("volume").GetComponent<MeshFilter>().mesh;
        LengthOfLineRenderer = 0;
        isFollow = true;
        lineRenderer.enabled = false;
        Meshrenderer.enabled = false;
        mesh.Clear();
        GameObject.Find("Panel_volume/Volume").GetComponent<Text>().enabled = true;
        O_prefab_qiu = GameObject.Find("celiang/Sphere");

    }
    public void D_Volume()//按钮控制
    {
        if (lineRenderer.enabled == true)
        {
            distance = 0;//重置清零
            dis_1 = 0;
            dis.Initialize();
            LengthOfLineRenderer = 0;
            lineRenderer.SetVertexCount(LengthOfLineRenderer);
            lineRenderer.enabled = false;
            Meshrenderer.enabled = false;
            mesh.Clear();
            GameObject.Find("Panel_volume/Volume").GetComponent<Text>().text = "";


        }
        else if (lineRenderer.enabled == false)
        {
            lineRenderer.enabled = true;
            Meshrenderer.enabled = true;
            //isFollow = true;
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
    double ploygon_area(int n, Vector3[] p)//底面积计算
    {
        double s = 0.0f;
        int i = 1;
        for (; i < n - 1; i++)
            s += det(p[0], p[i], p[i + 1]);
        return 0.5 * fabs(s);
    }
    int ClockDirection(Vector3[] points, int Count)//顺时针和逆时针判断
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
    int PolygonType(Vector3[] points, int count)//多边形性质判断
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
        var NAME = GameObject.Find("Sphere" + (ab - 1));

        ab -= 1;
        Destroy(NAME);
        distance = 0;//重置清零
        dis_1 = 0;
        dis.Initialize();
        LengthOfLineRenderer = 0;
        GameObject.Find("Panel_volume/Volume").GetComponent<Text>().text = "";
        lineRenderer.SetVertexCount(LengthOfLineRenderer);

    }
    // Update is called once per frame
    void Update()
    {

        distance_1 = distance.ToString("F3");
        GameObject.Find("Panel_volume/Volume").GetComponent<Text>().text = "测量体积：" + distance_1 + " 立方米";
        if (height < 0)
        {
            height = 0;
        }
        if (distance < 0)
        {
            distance = 0;
        }
        //print (distance);
        //传值给shader
        mat.SetVectorArray("Value", screenPos);
        mat.SetInt("PointNum", LengthOfLineRenderer);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (lineRenderer.enabled == true)
        {
            //右键重新画线
            if (Input.GetMouseButtonDown(2))
            {
                GetComponent<Btn_control>().tiji_warning_button();
                lineRenderer.enabled = true;
                Meshrenderer.enabled = true;
            }
            //左键画线
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    Debug.Log("当前触摸在UI上");
                else if (Physics.Raycast(ray, out hit))
                {
                    position = hit.point;//碰撞点坐标
                    position.y = hit.point.y + 0.1f;
                    point = GameObject.Instantiate(O_prefab_qiu, hit.point, transform.rotation) as GameObject;

                    point.name = "Sphere" + ab;
                    ab += 1;
                    LengthOfLineRenderer++;//端点点数加1
                    lineRenderer.SetVertexCount(LengthOfLineRenderer);//设置端点数
                    lineRenderer.SetPosition(LengthOfLineRenderer - 1, position);
                    dis_b[LengthOfLineRenderer - 1] = position;
                    if (LengthOfLineRenderer >= 3)
                    {
                        LengthOfLineRenderer++;
                        lineRenderer.SetVertexCount(LengthOfLineRenderer);
                        dis_b[LengthOfLineRenderer - 1] = dis_b[0];
                        lineRenderer.SetPosition(LengthOfLineRenderer - 1, dis_b[LengthOfLineRenderer - 1]);
                        LengthOfLineRenderer = LengthOfLineRenderer - 1;
                        isPolygonType = PolygonType(dis_b, LengthOfLineRenderer);
                        if (isPolygonType == 1)
                        {
                            isClockwise = ClockDirection(dis_b, LengthOfLineRenderer);
                            triangles = new int[3 * (LengthOfLineRenderer - 2)];
                            //根据顶点数来创建记录顶点坐标  
                            vertices = new Vector3[LengthOfLineRenderer];
                            if (isClockwise == 1)
                            {
                                for (int i = 0; i < LengthOfLineRenderer; i++)
                                {
                                    vertices[i] = dis_b[LengthOfLineRenderer - 1 - i];
                                    Vector3 v3 = Camera.main.WorldToScreenPoint(dis_b[LengthOfLineRenderer - 1 - i]);
                                    screenPos[i] = new Vector4(v3.x, Screen.height - v3.y, v3.z, 0);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < LengthOfLineRenderer; i++)
                                {
                                    vertices[i] = dis_b[i];
                                    Vector3 v3 = Camera.main.WorldToScreenPoint(dis_b[i]);
                                    screenPos[i] = new Vector4(v3.x, Screen.height - v3.y, v3.z, 0);
                                }
                            }
                            //三角形个数  
                            int triangles_count = LengthOfLineRenderer - 2;
                            for (int i = 0; i < triangles_count; i++)
                            {
                                triangles[3 * i] = 0;
                                triangles[3 * i + 1] = i + 2;
                                triangles[3 * i + 2] = i + 1;
                            }
                            mesh.vertices = vertices;
                            //设置顶点索引  
                            mesh.triangles = triangles;
                        }
                        else
                        {
                            distance = 0;//重置清零
                            dis_1 = 0;
                            dis_b.Initialize();
                            LengthOfLineRenderer = 0;
                            lineRenderer.SetVertexCount(LengthOfLineRenderer);
                            lineRenderer.enabled = false;
                            Meshrenderer.enabled = false;
                            //GameObject.Find ("Canvas/Drawvolume/Volume_n").GetComponent<Text> ().text = "";
                            mesh.Clear();
                            GameObject.Find("Panel_volume/Panel").transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
                        }
                    }

                    Debug.Log("当前没有触摸在UI上");
                }
            }
            else if (Input.GetMouseButtonDown(1) && isFollow)
            {
                isFollow = false;
            }
            else if (LengthOfLineRenderer >= 3)
            {
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    if (height >= 0)
                    {
                        height = Input.GetAxis("Mouse ScrollWheel") * 1f + height;
                    }
                    else
                    {
                        height = 0;
                    }
                    lineRenderer.SetVertexCount(0);
                    isClockwise = ClockDirection(dis_b, LengthOfLineRenderer);
                    distance = (ploygon_area(LengthOfLineRenderer, dis_b)) * height;
                    count_v = 0;
                    if (isClockwise == 0)
                    {//逆时针直接绘制
                        for (int i = 0; i < LengthOfLineRenderer - 1; i++)
                        {
                            dis[count_v] = dis_b[i + 1];
                            dis[count_v + 1] = new Vector3(dis_b[i + 1].x, dis_b[i + 1].y + height, dis_b[i + 1].z);
                            dis[count_v + 2] = new Vector3(dis_b[i].x, dis_b[i].y + height, dis_b[i].z);
                            dis[count_v + 3] = dis_b[i];
                            dis[count_v + 4] = dis_b[i + 1];
                            count_v = count_v + 5;
                        }
                        dis[count_v] = dis_b[LengthOfLineRenderer - 1];
                        dis[count_v + 1] = new Vector3(dis_b[LengthOfLineRenderer - 1].x, dis_b[LengthOfLineRenderer - 1].y + height, dis_b[LengthOfLineRenderer - 1].z);
                        dis[count_v + 2] = new Vector3(dis_b[0].x, dis_b[0].y + height, dis_b[0].z);
                        dis[count_v + 3] = dis_b[0];
                        dis[count_v + 4] = dis_b[LengthOfLineRenderer - 1];
                        count_v = count_v + 5;
                        lineRenderer.SetVertexCount(count_v);
                    }
                    else
                    {//图形为顺时针的情况
                        for (int i = 0; i < LengthOfLineRenderer - 1; i++)
                        {
                            dis[count_v] = dis_b[LengthOfLineRenderer - 1 - i];
                            dis[count_v + 1] = new Vector3(dis_b[LengthOfLineRenderer - 1 - i].x, dis_b[LengthOfLineRenderer - 1 - i].y + height, dis_b[LengthOfLineRenderer - 1 - i].z);
                            dis[count_v + 2] = new Vector3(dis_b[LengthOfLineRenderer - 2 - i].x, dis_b[LengthOfLineRenderer - 2 - i].y + height, dis_b[LengthOfLineRenderer - 2 - i].z);
                            dis[count_v + 3] = dis_b[LengthOfLineRenderer - 2 - i];
                            dis[count_v + 4] = dis_b[LengthOfLineRenderer - 1 - i];
                            count_v = count_v + 5;
                        }
                        dis[count_v] = dis_b[0];
                        dis[count_v + 1] = new Vector3(dis_b[0].x, dis_b[0].y + height, dis_b[0].z);
                        dis[count_v + 2] = new Vector3(dis_b[LengthOfLineRenderer - 1].x, dis_b[LengthOfLineRenderer - 1].y + height, dis_b[LengthOfLineRenderer - 1].z);
                        dis[count_v + 3] = dis_b[LengthOfLineRenderer - 1];
                        dis[count_v + 4] = dis_b[0];
                        count_v = count_v + 5;
                        lineRenderer.SetVertexCount(count_v);
                    }
                    //绘制框架线条
                    for (int i = 0; i < count_v; i++)
                    {
                        lineRenderer.SetPosition(i, dis[i]);
                    }

                    //绘制面片
                    count_p = 0;
                    for (int i = 0; i < LengthOfLineRenderer - 1; i++)
                    {
                        dis_m[count_p] = dis_b[i];
                        dis_m[count_p + 1] = new Vector3(dis_b[i].x, dis_b[i].y + height, dis_b[i].z);
                        dis_m[count_p + 2] = new Vector3(dis_b[i + 1].x, dis_b[i + 1].y + height, dis_b[i + 1].z);
                        dis_m[count_p + 3] = dis_b[i];
                        dis_m[count_p + 4] = dis_b[i + 1];
                        dis_m[count_p + 5] = new Vector3(dis_b[i + 1].x, dis_b[i + 1].y + height, dis_b[i + 1].z);
                        count_p = count_p + 6;
                    }
                    //最后一个侧面
                    dis_m[count_p] = dis_b[LengthOfLineRenderer - 1];
                    dis_m[count_p + 1] = new Vector3(dis_b[LengthOfLineRenderer - 1].x, dis_b[LengthOfLineRenderer - 1].y + height, dis_b[LengthOfLineRenderer - 1].z);
                    dis_m[count_p + 2] = new Vector3(dis_b[0].x, dis_b[0].y + height, dis_b[0].z);
                    dis_m[count_p + 3] = dis_b[LengthOfLineRenderer - 1];
                    dis_m[count_p + 4] = dis_b[0];
                    dis_m[count_p + 5] = new Vector3(dis_b[0].x, dis_b[0].y + height, dis_b[0].z);
                    count_p = count_p + 6;
                    //底面绘制
                    for (int i = 0; i < LengthOfLineRenderer - 1; i++)
                    {
                        dis_m[count_p] = dis_b[0];
                        dis_m[count_p + 1] = dis_b[i + 2];
                        dis_m[count_p + 2] = dis_b[i + 1];
                        count_p = count_p + 3;
                    }
                    //顶面绘制
                    for (int i = 0; i < LengthOfLineRenderer - 1; i++)
                    {
                        dis_m[count_p] = new Vector3(dis_b[0].x, dis_b[0].y + height, dis_b[0].z);
                        dis_m[count_p + 1] = new Vector3(dis_b[i + 2].x, dis_b[i + 2].y + height, dis_b[i + 2].z);
                        dis_m[count_p + 2] = new Vector3(dis_b[i + 1].x, dis_b[i + 1].y + height, dis_b[i + 1].z);
                        count_p = count_p + 3;
                    }
                    //顶点赋值
                    triangles = new int[3 * (count_p - 2)];
                    vertices = new Vector3[count_p];
                    //将链表中的顶点坐标赋值给vertices  
                    for (int i = 0; i < count_p; i++)
                    {
                        vertices[i] = dis_m[i];
                    }

                    //三角形索引								
                    int triangles_count = count_p;
                    for (int i = 0; i < triangles_count; i++)
                    {
                        triangles[i] = i;
                    }

                    //设置顶点坐标  
                    mesh.vertices = vertices;
                    //设置顶点索引  
                    mesh.triangles = triangles;
                }
            }
        }

    }
}
//	void OnRenderImage (RenderTexture src, RenderTexture dest)
//	{
//		Graphics.Blit (src, dest, mat);
//	}

