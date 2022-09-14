using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 挂接在 场景1 液化气装卸站台上
/// </summary>
public class Add : MonoBehaviour
{
    //查询框，对应第一模块Canvas下的RawImageHightLightSelection
    public GameObject RawImageHightLightSelection;
    public GameObject go;//给所有的子元素的物体添加一个脚本 该物体是所有元素的父元素
    public GameObject Pipeline;//给管线也添加可以查询的脚本，Pipeline是管线
    private int PipelineCount;//管线子元素个数
    private int count;     //计算所有子元素的个数
    private readonly int sum = 0;
    public GameObject obj;
    public GameObject celiangui;
    //场景漫游模块定点查看的五个角度
    public GameObject dingdian;
    public GameObject dingdian1;
    public GameObject dingdian2;
    public GameObject dingdian3;
    public GameObject dingdian4;
    public GameObject dingdian5;
    public GameObject camera;
    public float time = 7.0f;
    public GameObject CruiseCamera;
    public GameObject pause;
    //场景漫游模块的一级button
    public GameObject RawImage;
    //场景漫游模块的定点查看二级button
    public GameObject RawImage_dingdian;
    //场景漫游模块中的巡游物体
    public GameObject CruiseObject;
    //场景漫游模块中的设备查询二级button
    public GameObject RawImage_cx;
    //场景漫游模块的测量提示工具
    public GameObject CeliangHelp;
    //用来判断测量帮助界面是否展示
    private bool isCeliangHelpOpen = false;

    /// <summary>
    /// 搜索结果下拉框
    /// </summary>
    public GameObject RawImageHightLightSelectionScrollView;

    //巡游模块
    public GameObject RawImage_parade;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine("start1");
        /* time += Time.deltaTime;
         count = go.transform.childCount;
         if(time-5>=0)
         {
             for (int i = 0; i < count; i++)
             {
                 {
                     //给所有的子物体添加上interavtive脚本
                     go.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                 }
             }
             //给所有的子元素添加objplay脚本
         }*/
    }
    /// <summary>
    /// 场景1 的Canvas/RawImage/Button_celiang 即“测量工具”按钮关联此函数
    /// 作用是进行测量
    /// </summary>
    public void celiang()
    {
        //StartCoroutine("start1");
        //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(571f, -380f, 0), 0.5f);
        RawImage.SetActive(false);
        //celiangui.transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
        celiangui.SetActive(true);
        //GameObject.Find("Canvas/help").transform.DOLocalMove(new Vector3(0, 263f, 0), 0.5f);
        CeliangHelp.SetActive(true);
    }
    /// <summary>
    /// 测量提示
    /// </summary>
    public void MeasureHelp()
    {
        if (isCeliangHelpOpen == false)
        {
            CeliangHelp.SetActive(true);
            isCeliangHelpOpen = true;
        }
        else
        {
            CeliangHelp.SetActive(false);
            isCeliangHelpOpen = false;
        }
    }
    /// <summary>
    /// 测量退出
    /// </summary>
    public void celiangtuichu()
    {
        RawImage.SetActive(true);
        celiangui.SetActive(false);
        CeliangHelp.SetActive(false);
        //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
        //celiangui.transform.DOLocalMove(new Vector3(571f, -380f, 0), 0.5f);
        //GameObject.Find("Canvas/help").transform.DOLocalMove(new Vector3(0, 481, 0), 0.5f);
        GameObject.Find("Main Camera").GetComponent<Volume>().xiaochu();
        GameObject.Find("Main Camera").GetComponent<Drawline_test>().xiaochu();
        GameObject.Find("Main Camera").GetComponent<Area>().xiaochu();
        GameObject.Find("Main Camera").GetComponent<Volume>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<Area>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<Drawline_test>().enabled = false;
        //GameObject.Find("Canvas/help").transform.DOLocalMove(new Vector3(0, 483f, 0), 0.5f);
    }
    /// <summary>
    /// 设备查询相关按钮显示
    /// </summary>
    public void EquipmentQuery()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(571f, -380f, 0), 0.5f);
        //GameObject.Find("Canvas/RawImage_cx").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
        RawImage.SetActive(false);
        RawImage_cx.SetActive(true);
    }
    /// <summary>
    /// 在点击第一模块的设备查询的根据名称按钮后，调用此方法，显示查询框
    /// </summary>
    public void ShowSelectionInterFace()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        RawImageHightLightSelection.SetActive(true);
        //每调用一次就清空一次输入框
        RawImageHightLightSelection.GetComponentInChildren<InputField>().text = "";
        //清空上次搜索结果
        RawImageHightLightSelectionScrollView.SetActive(false);
        CloseHightLight();
        CloseCubeScript();
    }
    /// <summary>
    /// 关闭所有模型上的cube脚本
    /// </summary>
    public void CloseCubeScript()
    {
        //获取所有子物体的个数
        for (int i = 0; i < count; i++)
        {
            //如果存在cube脚本，那么就禁用
            if (go.transform.GetChild(i).gameObject.GetComponent<Cube>() != null)
            {
                go.transform.GetChild(i).gameObject.GetComponent<Cube>().enabled = false;
            }
            //Pipeline.transform.GetChild(i).gameObject.AddComponent<Cube>();
        }
        for (int i = 0; i < PipelineCount; i++)
        {
            //如果存在cube脚本，那么就禁用
            if (Pipeline.transform.GetChild(i).gameObject.GetComponent<Cube>() != null)
            {
                Pipeline.transform.GetChild(i).gameObject.GetComponent<Cube>().enabled = false;
            }
        }
    }
    /// <summary>
    /// 关闭所有模型高亮
    /// </summary>
    public void CloseHightLight()
    {
        if (GameObject.Find("Main Camera").GetComponent<HighlightingEffect>() != null)
        {
            GameObject.Find("Main Camera").GetComponent<HighlightingEffect>().enabled = false;
        }
        if (GameObject.Find("Main Camera").GetComponent<MouseHighlight1>() != null)
        {
            GameObject.Find("Main Camera").GetComponent<MouseHighlight1>().enabled = false;
        }
    }
    /// <summary>
    /// 场景漫游模块
    /// “手动选择”按钮
    /// 查询模型信息功能
    /// </summary>
    public void xianshixinxi()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        StartCoroutine("start1");
        GameObject.Find("Main Camera").GetComponent<HighlightingEffect>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<MouseHighlight1>().enabled = true;
        //获取所有子物体的个数
        for (int i = 0; i < count; i++)
        {
            go.transform.GetChild(i).gameObject.AddComponent<Cube>();
            //Pipeline.transform.GetChild(i).gameObject.AddComponent<Cube>();
        }
        for (int i = 0; i < PipelineCount; i++)
        {
            Pipeline.transform.GetChild(i).gameObject.AddComponent<Cube>();
        }
        /*  for (int i = 0; i < count; i++)
          {
              //给脚本中的变量赋值
              go.transform.GetChild(i).gameObject.GetComponent<Peter_ObjPlay>().m_InteractiveObj =
                  go.transform.GetChild(i).gameObject.GetComponent<Peter_InteractiveObj>();
          }*/
        /*for (int i = 0; i < count; i++)
        {
            //使脚本的enable设置为false，因为如果在start中就直接enable，会导致接下来的物体赋不上
            go.transform.GetChild(i).gameObject.GetComponent<Cube>().enabled = false;
        }*/
    }
    public void dingdianchaxun()
    {
        //SceneManager.LoadScene("1");
        //System.Threading.Thread.Sleep(2000);
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        camera.transform.position = new Vector3(40, 12, -111);
        //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(571f, -380f, 0), 0.5f);
        //GameObject.Find("Canvas/RawImage_dingdian").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
        //RawImage.SetActive(false);
        RawImage_dingdian.SetActive(true);
        dingdian.SetActive(true);
        dingdian_chongxuan();
    }

    /// <summary>
    /// 路线巡游
    /// </summary>
    public void parade() {
        //SceneManager.LoadScene("1");
        //System.Threading.Thread.Sleep(2000);
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        camera.transform.position = new Vector3(40, 12, -111);
        //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(571f, -380f, 0), 0.5f);
        //GameObject.Find("Canvas/RawImage_dingdian").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
        //RawImage.SetActive(false);
        RawImage_parade.SetActive(true);
        //dingdian.SetActive(true);
        //dingdian_chongxuan();
    }

    public void dingdian_chongxuan()
    {
        camera.SetActive(true);
        camera.transform.position = new Vector3(40, 12, -111);
        dingdian1.SetActive(true);
        dingdian2.SetActive(true);
        dingdian3.SetActive(true);
        dingdian4.SetActive(true);
        dingdian5.SetActive(true);
        CruiseCamera.SetActive(false);
        pause.SetActive(false);
        //GameObject.Find("Canvas/RawImage_dingdian").transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 195);
        CruiseObject.transform.position = new Vector3(27.2f, 2, -90.7f);
        CruiseObject.transform.DOKill();
        GameObject.Find("dingdian/pos1/Camera").SetActive(false);
        GameObject.Find("dingdian/pos2/Camera").SetActive(false);
        GameObject.Find("dingdian/pos3/Camera").SetActive(false);
        GameObject.Find("dingdian/pos4/Camera").SetActive(false);
        GameObject.Find("dingdian/pos5/Camera").SetActive(false);
    }
    /// <summary>
    /// 退出定位查看功能
    /// </summary>
    public void dingdian_exit()
    {
        //GameObject.Find("Canvas/RawImage_dingdian").transform.DOLocalMove(new Vector3(571f, -380f, 0), 0.5f);
        //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
        //GameObject.Find("dingdian/pos1/Camera").SetActive(false);
        //GameObject.Find("dingdian/pos2/Camera").SetActive(false);
        //GameObject.Find("dingdian/pos3/Camera").SetActive(false);
        //GameObject.Find("dingdian/pos4/Camera").SetActive(false);
        //GameObject.Find("dingdian/pos5/Camera").SetActive(false);
        //GameObject.Find("CruiseObject").transform.DOKill();
        //GameObject.Find("CruiseObject").transform.position = new Vector3(27.2f, 2, -90.7f);
        RawImage_dingdian.SetActive(false);
        RawImage_parade.SetActive(false);
        //RawImage.SetActive(true);
        dingdian.SetActive(false);
        CruiseObject.SetActive(false);
        pause.SetActive(false);
        dingdian1.SetActive(false);
        dingdian2.SetActive(false);
        dingdian3.SetActive(false);
        dingdian4.SetActive(false);
        dingdian5.SetActive(false);
        CruiseCamera.SetActive(false);
        camera.SetActive(true);
        dingdian.SetActive(false);
        GameObject.Find("Main Camera").transform.position = new Vector3(40, 12, -111);
    }
    /// <summary>
    /// 退出设备查询按钮绑定的功能
    /// </summary>
    public void guanbixinxi()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        //Debug.Log("测试关闭功能");
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (GameObject.Find("Main Camera").GetComponent<MouseHighlight>().gameCheck != null)
            {
                obj = GameObject.Find("Main Camera").GetComponent<MouseHighlight>().gameCheck;
                GameObject.Find("Main Camera").GetComponent<HighlightingEffect>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<MouseHighlight>().enabled = false;
                //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
                //GameObject.Find("Canvas/RawImage_cx").transform.DOLocalMove(new Vector3(710f, -380f, 0), 0.5f);
                //RawImage.SetActive(true);
                RawImage_cx.SetActive(false);
                Destroy(obj.GetComponent<SpectrumController>());
                Destroy(obj.GetComponent<HighlightableObject>());
                obj = null;
                //GameObject.Find("Canvas/RawImage_cx/biaoti").GetComponent<Text>().text = "";
            }
            else
            {
                GameObject.Find("Main Camera").GetComponent<HighlightingEffect>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<MouseHighlight>().enabled = false;
                //RawImage.SetActive(true);
                RawImage_cx.SetActive(false);
                //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
                //GameObject.Find("Canvas/RawImage_cx").transform.DOLocalMove(new Vector3(710f, -380f, 0), 0.5f);
                //GameObject.Find("Canvas/RawImage_cx/biaoti").GetComponent<Text>().text = "";
            }
        }
        for (int j = 0; j < count; j++)
        {
            Destroy(go.transform.GetChild(j).gameObject.GetComponent<Cube>());
        }
        /*  for (int i = 0; i < count; i++)
          {
              //给脚本中的变量赋值
              go.transform.GetChild(i).gameObject.GetComponent<Peter_ObjPlay>().m_InteractiveObj =
                  go.transform.GetChild(i).gameObject.GetComponent<Peter_InteractiveObj>();
          }*/
        /*  for (int j = 0; j < count; j++)
          {
              //使脚本的enable设置为false，因为如果在start中就直接enable，会导致接下来的物体赋不上
              go.transform.GetChild(j).gameObject.GetComponent<Cube>().enabled = false;
          }*/
    }
    public void mesh_des()
    {
        count = go.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            //给所有的子物体添加上interavtive脚本
            Destroy(go.transform.GetChild(i).gameObject.GetComponent<MeshCollider>());
            //给所有的子元素添加objplay脚本
        }
    }
    public IEnumerator start1()
    {
        //Debug.Log("123");
        count = go.transform.childCount;
        PipelineCount = Pipeline.transform.childCount;
        while (time >= 0)
        {
            time--;//总时间 单位 秒，倒计时
            if (time <= 5 && time > 4)
            {
                //Debug.Log("5");
                for (int i = 0; i < 100; i++)
                {
                    {
                        //为了第三模块，暂时注释了253 254   268  283  298
                        //给所有的子物体添加上interavtive脚本
                        go.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                        //Pipeline.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                    }
                }
            }
            if (time <= 4 && time > 3)
            {
                //Debug.Log("4");
                for (int i = 100; i < 200; i++)
                {
                    {
                        //给所有的子物体添加上interavtive脚本
                        go.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                        //Pipeline.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                    }
                }
            }
            if (time <= 3 && time > 2)
            {
                //Debug.Log("3");
                for (int i = 200; i < 300; i++)
                {
                    {
                        //给所有的子物体添加上interavtive脚本
                        go.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                        //Pipeline.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                    }
                }
            }
            if (time <= 2 && time > 1)
            {
                //Debug.Log("2");
                for (int i = 300; i < count; i++)
                {
                    {
                        //给所有的子物体添加上interavtive脚本
                        go.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                        //Pipeline.transform.GetChild(i).gameObject.AddComponent<MeshCollider>();
                    }
                }
            }
            if (time <= 0)
            {
                yield break;
            }
            else if (time > 0)
            {
                yield return new WaitForSeconds(1f);// 每次 自减1，等待 1 秒
            }
        }
    }
    // Update is called once per frame
    private void Update()
    {
    }
}