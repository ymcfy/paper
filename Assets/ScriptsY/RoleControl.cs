using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/// <summary>
/// 控制角色跳转与场景跳转的脚本，挂接在Main Camera上
/// </summary>
public class RoleControl : MonoBehaviour
{
    /// <summary>
    /// 场景漫游一级菜单
    /// </summary>
    public GameObject ChangJingManYou;
    /// <summary>
    /// 场景漫游的定点查看二级子菜单
    /// </summary>
    public GameObject RawImage_dingdian;
    //场景漫游模块定点查看的五个角度
    public GameObject dingdian;
    public GameObject dingdian1;
    public GameObject dingdian2;
    public GameObject dingdian3;
    public GameObject dingdian4;
    public GameObject dingdian5;
    /// <summary>
    /// 主摄像头
    /// </summary>
    public GameObject camera;
    /// <summary>
    /// 巡游镜头
    /// </summary>
    public GameObject CruiseCamera;
    //场景漫游模块中的巡游物体
    public GameObject CruiseObject;

    //场景漫游模块中的设备查询二级button
    public GameObject RawImage_cx;
    public GameObject go;//给所有的子元素的物体添加一个脚本 该物体是所有元素的父元素
    public GameObject Pipeline;//给管线也添加可以查询的脚本，Pipeline是管线
    private int count;     //计算所有子元素的个数
    public float time = 7.0f;
    private int PipelineCount;//管线子元素个数
    /// <summary>
    /// 手动输入查询模型UI
    /// </summary>
    public GameObject HighLightSelection;
    /// <summary>
    /// 模型详细信息面板
    /// </summary>
    public GameObject ModelDetail;

    public GameObject Ground;
    public GameObject Road;
    public GameObject Road1;
    /// <summary>
    /// 道路上的箭头，这个在地图透明时也应该取消,代号EAQDYHQJZ04
    /// </summary>
    public GameObject RoadArrow;
    /// <summary>
    /// 道路旁的细长竿子，这个在地图透明时也应该取消，代号EOQDYHQXP05
    /// </summary>
    public GameObject SlenderRod;
    /// <summary>
    /// 道路旁的矮粗石墩，这个在地图透明时也应该取消，代号EOQDYHQXP23
    /// </summary>
    public GameObject LowRoughStonePier;

    public GameObject celiangui;
    //场景漫游模块的测量提示工具
    public GameObject CeliangHelp;


    public GameObject image;
    //public Text text;
    public GameObject P3;
    public GameObject Person;//人物
    public GameObject tank;
    public GameObject tank1;

    public GameObject water;


    private bool v;
    public GameObject SwitchPerspective;
    public bool flag = false;


    //public GameObject KeChengPeiXun;
    public GameObject ShuJuGuanLi;
    public GameObject ChangJingGuanLi;
    public GameObject XiTongTuiChu;
    public GameObject XiTongDuDang;


    public GameObject btnChangjingmanyouReturn;
    /// <summary>
    /// 场景管理子菜单
    /// </summary>
    public GameObject SceneManage;
    /// <summary>
    /// 模型搜索选择框
    /// </summary>
    public GameObject ModelSearchSelectionBox;
    /// <summary>
    /// 数据管理模块的Canvas
    /// </summary>
    public GameObject Canvas3;
    /// <summary>
    /// 课程培训模块的Canvas
    /// </summary>
    public GameObject Canvas4;
    /// <summary>
    /// 关闭场景漫游模块的模型查询功能
    /// </summary>
    public GameObject obj;

    /// <summary>
    /// 第四个点的camera
    /// </summary>
    public GameObject pos4Camera;
    /// <summary>
    /// 管线的camera  PPQDYHQGX08
    /// </summary>
    public GameObject pipelineCamera;
    /// <summary>
    /// 第三个点的camera
    /// </summary>
    public GameObject pos3Camera;
    /// <summary>
    /// 第一个点的camera
    /// </summary>
    public GameObject pos1Camera;
    /// <summary>
    /// 火焰的camera
    /// </summary>
    public GameObject flameCamera;
    /// <summary>
    /// camera  位于场景一级菜单下
    /// </summary>
    public GameObject camera1;
    /// <summary>
    /// 灭火器camera
    /// </summary>
    public GameObject extigushierCamera;
    /// <summary>
    /// 第五个点的camera
    /// </summary>
    public GameObject pos5Camera;
    /// <summary>
    /// 汽车的camera
    /// </summary>
    public GameObject cameraOfCar;
    /// <summary>
    /// 第三视角camera
    /// </summary>
    public GameObject cameraSecondary;
    /// <summary>
    /// 汽车的视角1camera
    /// </summary>
    public GameObject camera1OfCar;
    /// <summary>
    /// camera(1)  位于场景一级菜单下
    /// </summary>
    public GameObject camera2;
    /// <summary>
    /// 第二个点的camera
    /// </summary>
    public GameObject pos2Camera;
    /// <summary>
    /// 管线的camera  PPQDYHQGX140
    /// </summary>
    public GameObject pipelineCamera1;

    /// <summary>
    /// 泄漏训练的UI
    /// </summary>
    public GameObject LeakTraining;
    /// <summary>
    /// 挂载choose脚本的物体，用来退出工艺流程
    /// </summary>
    public GameObject dropdown;

    /// <summary>
    /// kongzhi2 这个挂载脚本的物体
    /// </summary>
    public GameObject kongzhi2;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("start1");
        string path = Application.dataPath + "/role.txt";
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
        StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
        if (null == sr) return;
        string str = sr.ReadToEnd();
        Debug.Log(str);
        if (str.Equals("user\r\n"))
        {
            user();
        }
        sr.Close();
    }
    /// <summary>
    /// 如果是用户角色，则有以下操作
    /// </summary>
    public void user()
    {
        //Debug.Log("111");
        //dingdianchakan.SetActive(false);
        ShuJuGuanLi.SetActive(false);
        ChangJingGuanLi.SetActive(false);
    }
    /// <summary>
    /// 模型跳转的前期工作
    /// </summary>
    public void SkipBefore()
    {
        #region 摄像头部分
        pos4Camera.SetActive(false);
        pipelineCamera.SetActive(false);
        pos3Camera.SetActive(false);
        pos1Camera.SetActive(false);
        flameCamera.SetActive(false);
        camera1.SetActive(false);
        extigushierCamera.SetActive(false);
        pos5Camera.SetActive(false);
        cameraOfCar.SetActive(false);
        cameraSecondary.SetActive(false);
        camera1OfCar.SetActive(false);
        camera2.SetActive(false);
        pos2Camera.SetActive(false);
        pipelineCamera1.SetActive(false);
        #endregion
        #region 一级菜单部分
        //关闭场景漫游一级菜单
        ChangJingManYou.SetActive(false);
        //关闭课程培训一级菜单
        Canvas4.SetActive(false);
        //关闭数据管理一级菜单
        Canvas3.SetActive(false);
        //关闭场景管理一级菜单
        //关闭模型搜索选择框
        ModelSearchSelectionBox.SetActive(false);
        SceneManage.SetActive(false);
        #endregion
        #region 场景漫游部分
        #region 场景漫游定点查看功能部分
        //关闭场景漫游定点查看菜单
        RawImage_dingdian.SetActive(false);
        dingdian.SetActive(false);
        CruiseObject.SetActive(false);
        dingdian1.SetActive(false);
        dingdian2.SetActive(false);
        dingdian3.SetActive(false);
        dingdian4.SetActive(false);
        dingdian5.SetActive(false);
        CruiseCamera.SetActive(false);
        camera.SetActive(true);
        GameObject.Find("Main Camera").transform.position = new Vector3(40, 12, -111);
        #endregion
        #region 场景漫游模型查看功能部分
        //关闭场景漫游模型查看菜单
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (GameObject.Find("Main Camera").GetComponent<MouseHighlight>().gameCheck != null)
            {
                obj = GameObject.Find("Main Camera").GetComponent<MouseHighlight>().gameCheck;
                GameObject.Find("Main Camera").GetComponent<HighlightingEffect>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<MouseHighlight>().enabled = false;
                //GameObject.Find("Canvas/RawImage").transform.DOLocalMove(new Vector3(458f, -380f, 0), 0.5f);
                //GameObject.Find("Canvas/RawImage_cx").transform.DOLocalMove(new Vector3(710f, -380f, 0), 0.5f);

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
        //关闭手动搜索UI
        HighLightSelection.SetActive(false);
        //关闭详细信息面板
        ModelDetail.SetActive(false);
        #endregion
        #region 场景漫游地图透明功能部分
        //关闭场景漫游地图透明效果
        Ground.SetActive(true);
        Road.SetActive(true);
        Road1.SetActive(true);
        RoadArrow.SetActive(true);
        SlenderRod.SetActive(true);
        LowRoughStonePier.SetActive(true);
        #endregion
        #region 场景漫游测量功能部分
        //关闭场景漫游测量功能效果
        celiangui.SetActive(false);
        CeliangHelp.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<Volume>().xiaochu();
        GameObject.Find("Main Camera").GetComponent<Drawline_test>().xiaochu();
        GameObject.Find("Main Camera").GetComponent<Area>().xiaochu();
        GameObject.Find("Main Camera").GetComponent<Volume>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<Area>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<Drawline_test>().enabled = false;
        #endregion
        #endregion
        #region 课程培训部分
        #region 灭火训练部分
        P3.SetActive(false);
        water.SetActive(false);
        v = false;
        tank.SetActive(false);
        tank1.SetActive(false);
        //text = image.transform.Find("Text").GetComponent<Text>();
        //text.text = "";
        image.SetActive(false);
        Person.SetActive(false);
        SwitchPerspective.SetActive(false);
        //将人物角度复原
        Person.transform.rotation = new Quaternion(0, 0, 0, 0);
        flag = false;
        #endregion
        #region 泄漏训练部分
        #region 泄漏训练整体UI部分
        LeakTraining.SetActive(false);
        #endregion
        #region FLACS模拟部分
        kongzhi2.GetComponent<ParticleTest>().stopSimulation();
        kongzhi2.GetComponent<ParticleTest>().closeUI();
        #endregion
        #region 数值模拟部分
        camera.GetComponent<danger_area>().exit();
        LeakTraining.SetActive(false);
        #endregion
        #region 工艺流程部分
        dropdown.GetComponent<choose>().gongyiliucheng_exit();
        Canvas4.SetActive(false);
        #endregion
        #endregion
        #endregion
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
    /// <summary>
    /// 泄漏训练的返回按钮
    /// </summary>
    public void exitLeakTraining() {
        LeakTraining.SetActive(false);
        Canvas4.SetActive(true);
    }
    public void ChangJingManYouSkip()
    {
        //跳转前期操作
        SkipBefore();
        //SceneManager.LoadScene("1");
        ChangJingManYou.SetActive(true);
    }
    public void ChangJingGuanLiSkip()
    {
        //btnChangjingmanyouReturn.SetActive(true);
        //ChangJingManYou.SetActive(false);
        //KeChengPeiXun.SetActive(false);
        //ShuJuGuanLi.SetActive(false);
        //ChangJingGuanLi.SetActive(false);
        //XiTongTuiChu.SetActive(false);
        //XiTongDuDang.SetActive(false);
        //Canvas2.SetActive(true);
        //先跳到首页，即把所有操作清零    实验发现不合适
        //SceneManager.LoadScene("1");
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        //跳转前期操作
        SkipBefore();
        //打开场景管理子菜单
        SceneManage.SetActive(true);
        //打开模型搜索选择框
        ModelSearchSelectionBox.SetActive(true);
        //打开HighlightingEffect脚本，使得生成的模型可以点击高亮
        camera.GetComponent<HighlightingEffect>().enabled = true;
    }
    public void KeChengPeiXunSkip()
    {
        //跳转前期操作
        SkipBefore();
        //课程培训模块的canvas可见
        //SceneManager.LoadScene("1");
        Canvas4.SetActive(true);
        //SceneManager.LoadScene("4");
    }
    /// <summary>
    /// 数据管理模块跳转
    /// </summary>
    public void ShuJuGuanLiSkip()
    {
        //跳转前期操作
        SkipBefore();
        //数据管理模块的canvas可见
        //SceneManager.LoadScene("1");
        Canvas3.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
