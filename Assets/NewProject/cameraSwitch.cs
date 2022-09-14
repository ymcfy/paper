using DG.Tweening;
using RE.Flowing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 灭火移动相关
/// </summary>
public class cameraSwitch : MonoBehaviour
{
    /// <summary>
    /// MainCamera
    /// </summary>
    public GameObject MainCamera;
    public GameObject image;
    //public Text text;
    public GameObject P3;
    public GameObject Person;//人物
    public GameObject tank;
    public GameObject tank1;
    public GameObject caidan;
    public GameObject caidan2;
    public GameObject water;
    /// <summary>
    /// 前往火灾现场按钮
    /// </summary>
    public GameObject yidong_button;
    /// <summary>
    /// 开始灭火按钮
    /// </summary>
    public GameObject miehuo_button;
    private readonly Vector3[] m_Path = new Vector3[4];//路线
    private Transform transform1;
    private bool v;
    public GameObject SwitchPerspective;
    /// <summary>
    /// 人物Camera
    /// </summary>
    public GameObject PersonCamera;
    public GameObject SecondaryCamera;
    public GameObject FlameCamera;
    public GameObject miehuoqi_camera;
    public GameObject miehuoqi;
    public bool flag = false;
    /// <summary>
    /// 各种火焰类型按钮
    /// </summary>
    //存储火焰按钮的集合，后续添加新按钮，只需添加到这个集合里即可
    private List<GameObject> Fires = new List<GameObject>();
    /// <summary>
    /// 阀门火
    /// </summary>
    public GameObject ValveFire;
    /// <summary>
    /// 流动火
    /// </summary>
    public GameObject FlowingFire;
    /// <summary>
    /// 各种灭火器类型按钮
    /// </summary>
    //存储灭火器按钮的集合，后续添加新按钮，只需添加到这个集合里即可
    private List<GameObject> Extiguishers = new List<GameObject>();
    /// <summary>
    /// 泡沫模式灭火器按钮
    /// </summary>
    public GameObject FoamMode;
    /// <summary>
    /// 冷却水模式灭火器按钮
    /// </summary>
    public GameObject CoolingWaterMode;
    /// <summary>
    /// 混合模式灭火器按钮
    /// </summary>
    public GameObject HybridMode;
    /// <summary>
    /// 灭火器类型按钮
    /// </summary>
    public GameObject ExtiguisherType;
    /// <summary>
    /// 火灾类型按钮
    /// </summary>
    public GameObject FireType;
    /// <summary>
    /// 结束灭火训练按钮
    /// </summary>
    public GameObject ExitExtiguish;
    //false 第三视角  true  第一视角
    private bool visualAngel = true;
    // Start is called before the first frame update
    private void Start()
    {
        transform1 = GameObject.Find("GameObject").GetComponent<Transform>();
        transform1.rotation = Quaternion.Euler(0, 0, 0);
        water.SetActive(false);
        tank.SetActive(false);
        v = false;
        MainCamera = GameObject.Find("Main Camera");
        //P3 = GameObject.Find("renwu/Camera");
        //P = GameObject.Find("renwu");
        MainCamera.SetActive(true);
        //暂时注释 4月12
        //P3.SetActive(false);
        image.SetActive(false);
        //暂时注释 4月12
        //P.SetActive(false);
        caidan2.SetActive(false);
        m_Path[0] = new Vector3(30, 0, -117);
        m_Path[1] = new Vector3(30, 0, -90.51f);
        m_Path[2] = new Vector3(32.63f, 0, -76.51f);
        m_Path[3] = new Vector3(34.5f, 0.68f, -72.7f);
        SwitchPerspective.SetActive(false);
        //向灭火器按钮集合添加按钮
        Extiguishers.Add(FoamMode);
        Extiguishers.Add(CoolingWaterMode);
        Extiguishers.Add(HybridMode);
        //设置集合内所有按钮不可用
        unableExtiguishers();
        //向火焰按钮集合添加按钮
        Fires.Add(ValveFire);
        Fires.Add(FlowingFire);
        //设置火焰集合内所有按钮不可用
        unableFires();
        //前往火灾现场按钮设置不可用
        yidong_button.GetComponent<Button>().enabled = false;
        //开始灭火按钮设置不可用
        miehuo_button.GetComponent<Button>().enabled = false;
        //灭火器类型按钮设置不可用
        ExtiguisherType.GetComponent<Button>().enabled = false;
        //结束灭火训练按钮设置不可用
        ExitExtiguish.GetComponent<Button>().enabled = false;
    }
    /// <summary>
    /// 设置所有灭火器按钮不可用
    /// </summary>
    public void unableExtiguishers() {
        foreach (GameObject item in Extiguishers)
        {
            item.GetComponent<Button>().enabled = false;
        }
    }
    /// <summary>
    /// 设置所有灭火器按钮可用
    /// </summary>
    public void enableExtiguishers()
    {
        foreach (GameObject item in Extiguishers)
        {
            item.GetComponent<Button>().enabled = true;
        }
    }
    /// <summary>
    /// 设置所有火焰按钮不可用
    /// </summary>
    public void unableFires()
    {
        foreach (GameObject item in Fires)
        {
            item.GetComponent<Button>().enabled = false;
        }
    }
    /// <summary>
    /// 设置所有火焰按钮可用
    /// </summary>
    public void enableFires()
    {
        foreach (GameObject item in Fires)
        {
            item.GetComponent<Button>().enabled = true;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        //暂时注释 4月12
        //if (Person.transform.position == new Vector3(34.5f, 0.68f, -72.7f))
        //{
        //    Person.transform.MORotateQuaternion(Quaternion.Euler(0, -87f, 0), 3f);
        //    //灭火按钮可用
        //    miehuo_button.GetComponent<Button>().enabled = true;
        //}
        //else
        //{
        //    Person.transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //if (visualAngel==false)
        //{
        //    GameObject.Find("renwu/Camera").SetActive(true);
        //    GameObject.Find("renwu/CameraSecondary").SetActive(false);
        //}
        //else
        //{
        //    GameObject.Find("renwu/Camera").SetActive(false);
        //    GameObject.Find("renwu/CameraSecondary").SetActive(true);
        //}
    }
    public void FlameSelection()
    {
        FlameCamera.SetActive(true);
        miehuoqi_camera.SetActive(false);
        P3.SetActive(false);//人物对应的Camera关闭
        //主Camera关闭
        MainCamera.SetActive(false);
        enableFires();
        unableExtiguishers();
        //灭火器类型可用
        ExtiguisherType.GetComponent<Button>().enabled = true;
    }
    /// <summary>
    /// 灭火器类型选择
    /// </summary>
    public void FireExtinguisherSelection()
    {
        //关闭Main Camera
        MainCamera.SetActive(false);
        miehuoqi.SetActive(true);
        miehuoqi_camera.SetActive(true);
        FlameCamera.SetActive(false);
        //人物角度灭火器Camera关闭
        P3.SetActive(false);
        enableExtiguishers();
        unableFires();
        //前往火灾现场按钮可用
        yidong_button.GetComponent<Button>().enabled = true;
    }
    public void PerspectiveSwitch()
    {
        if (visualAngel == true)
        {
            visualAngel = false;
            PersonCamera.SetActive(false);
            SecondaryCamera.SetActive(true);
            //GameObject.Find("Canvas4/火灾提示框/switch/Text").transform.GetComponent<Text>().text
            //    = "切换视角";
            //Debug.Log(visualAngel);
        }
        else
        {
            visualAngel = true;
            SecondaryCamera.SetActive(false);
            PersonCamera.SetActive(true);
            //GameObject.Find("Canvas4/火灾提示框/switch/Text").transform.GetComponent<Text>().text = "切换视角";
            //Debug.Log(visualAngel);
        }
    }
    public void huozaiyidong()
    {
        Person.transform.DOPath(m_Path, 10f, PathType.Linear, PathMode.Full3D);
        Debug.Log(Person.transform.position);//输出人物当前位置
        //text.text = "正在移动中";
        StartCoroutine(startCountDown());
    }
    public void miehuo() //开启副摄像头 关闭主摄像头
    {
        PerspectiveSwitch();
        water.SetActive(true);
        flag = true;
        Person.GetComponent<Animator>().enabled = true;
        miehuo_button.GetComponent<Button>().enabled = false;
        ExitExtiguish.GetComponent<Button>().enabled = true;
    }
    public void FlameSelection5001()
    {
        tank.SetActive(false);
        tank1.SetActive(true);
    }
    public void FlameSelection5002()
    {
        tank.SetActive(true);
        tank1.SetActive(false);
    }
    public void huozaixunlian()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        miehuoqi.SetActive(false);//灭火器
        FlameCamera.SetActive(false);//火焰Camera
        miehuoqi_camera.SetActive(false);
        //展示灭火训练系列按钮
        image.SetActive(true);
        //课程培训菜单消失
        caidan.SetActive(false);
        //caidan.SetActive(false);//课程培训模块的二级菜单，灭火训练、泄露模拟等
        //caidan2.SetActive(true);//课程培训模块的三级菜单，火焰选择灭火器选择等
        Person.transform.position = new Vector3(40, 0.68f, -117f);
        Person.SetActive(true);
        //P.transform.rotation = transform1.rotation;
        //caidan.SetActive(false);
        //caidan2.SetActive(false);
        MainCamera.SetActive(false);//Main Camera
        //image.SetActive(true);
        yidong_button.SetActive(true);//移动的button
        //text = image.transform.Find("Text").GetComponent<Text>();
        P3.SetActive(true);//人物里对应的camera
        v = true;
        //tank.SetActive(true);//火焰  第一种
        //P.SetActive(true);//人物
        //SwitchPerspective.SetActive(true);
        SecondaryCamera.SetActive(false);//第三视角的Camera
        FireType.GetComponent<Button>().enabled = true;
    }
    public void huozai_start()
    {
        Person.SetActive(true);
        Person.GetComponent<Animator>().enabled = false;
        caidan2.SetActive(false);
        image.SetActive(true);
        FlameCamera.SetActive(false);
        miehuoqi_camera.SetActive(false);
        miehuoqi.SetActive(false);
        P3.SetActive(true);//人物对应的Camera开启
        huozaiyidong();
        //所有按钮都应该禁用
        FireType.GetComponent<Button>().enabled = false;
        ExtiguisherType.GetComponent<Button>().enabled = false;
        unableFires();
        unableExtiguishers();
        yidong_button.GetComponent<Button>().enabled = false;
    }
    public void exit_huozai() //关副摄像头 开主摄像头
    {
        MainCamera.SetActive(true);
        P3.SetActive(false);
        //caidan.SetActive(true);
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
        Person.transform.rotation = new Quaternion(0,0,0,0);
        flag = false;
    }
    public IEnumerator startCountDown()
    {
        float timer = 9.0f;
        while (timer >= 0)
        {
            Debug.Log(timer);
            yield return new WaitForSeconds(1);
            Debug.Log(Person.transform.position);//输出人物当前位置
            timer--;
        }
        if (timer <= 0)
        {
            //text.text = "";
            //miehuo_button.SetActive(true);
            //miehuo_button.GetComponent<Button>().enabled = true;
            Person.transform.position = new Vector3(34.5f, 0.68f, -72.7f);
            Person.transform.MORotateQuaternion(Quaternion.Euler(0, -87f, 0), 3f);
            //灭火按钮可用
            miehuo_button.GetComponent<Button>().enabled = true;
            //SwitchPerspective.SetActive(true);
        }
    }
}