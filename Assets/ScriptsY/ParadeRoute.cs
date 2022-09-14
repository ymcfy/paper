using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
* 再加几个点，加长一下时间，让它再慢一点
* 运行的时候，其他点隐掉
* 运行完之后，镜头切回
* 添加一个暂停键，就在巡游之上，在暂停和继续之间切换
* 用代码规范对当前代码进行规范
* */
public class ParadeRoute : MonoBehaviour
{

    /// <summary>
    /// true是暂停  false是继续
    /// </summary>
    public bool pauseOrContinue = true;
    private readonly Vector3[] m_Path = new Vector3[21];//路线
    public GameObject P;//巡游物体
    public GameObject MainCamera;//主摄像机
    public GameObject CruiseCamera;//巡游摄像机
    public GameObject pause;//暂停键
    public GameObject pauseText;//暂停按钮的文本内容
    public GameObject pos1;//定点查看按钮1
    public GameObject pos2;//定点查看按钮2
    public GameObject pos3;//定点查看按钮3
    public GameObject pos4;//定点查看按钮4
    public GameObject pos5;//定点查看按钮5

    // Use this for initialization
    private void Start()
    {
        //初始化巡游经过的各个点
        //此处有一个bug，那就是越往后越慢
        //从前向后第一轮   x方向是从左到右的移动。z方向是从前到后的移动，-90.7至-55
        m_Path[0] = new Vector3(27.2f, 2, -90.7f);//起始点
        m_Path[1] = new Vector3(27.2f, 2, -72.85f);//中间点
        m_Path[2] = new Vector3(27.2f, 2, -55);//结束点
        //从后向前第一轮
        m_Path[3] = new Vector3(33, 2, -55);
        m_Path[4] = new Vector3(33, 2, -72.85f);
        m_Path[5] = new Vector3(33, 2, -90.7f);
        //从前向后第二轮
        m_Path[6] = new Vector3(39.5f, 2, -90.7f);
        m_Path[7] = new Vector3(39.5f, 2, -72.85f);
        m_Path[8] = new Vector3(39.5f, 2, -55);
        //从后向前第二轮
        m_Path[9] = new Vector3(46, 2, -55);
        m_Path[10] = new Vector3(46, 2, -72.85f);
        m_Path[11] = new Vector3(46, 2, -90.7f);
        //从前向后第三轮
        m_Path[12] = new Vector3(52, 2, -90.7f);
        m_Path[13] = new Vector3(52, 2, -72.85f);
        m_Path[14] = new Vector3(52, 2, -55);
        //从后向前第三轮
        m_Path[15] = new Vector3(59, 2, -55);
        m_Path[16] = new Vector3(59, 2, -72.85f);
        m_Path[17] = new Vector3(59, 2, -90.7f);
        //从前向后第四轮
        m_Path[18] = new Vector3(65, 2, -90.7f);
        m_Path[19] = new Vector3(65, 2, -72.85f);
        m_Path[20] = new Vector3(65, 2, -55);
        P = GameObject.Find("CruiseObject");//巡游的物体
        MainCamera = GameObject.Find("Main Camera");//主摄像机
    }

    // Update is called once per frame
    private void Update()
    {
        //如果巡游位置到达了结束点
        if (P.transform.position == new Vector3(65, 2, -55))
        {
            CruiseCamera.SetActive(false);//关闭巡游摄像机
            MainCamera.SetActive(true);//打开主摄像机
            pause.SetActive(false);//暂停按钮隐藏
            //改变按钮框的大小，缩小至无暂停按钮的大小
            GameObject.Find("Canvas/RawImage_dingdian").transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 195);
        }
    }

    /// <summary>
    /// 定点查看的巡游功能
    /// 设定一个空物体，下面挂一个camera，然后进行移动
    /// 开始位置 （27.1，2，-90.7）
    /// </summary>
    public void cruise()
    {
        P.SetActive(true);
        CruiseCamera.SetActive(true);//巡游摄像机打开
        MainCamera.SetActive(false);//主摄像机关闭
        //此处应该用拖拽方法
        //五个定点查看位置点隐藏
        pos1.SetActive(false);
        pos2.SetActive(false);
        pos3.SetActive(false);
        pos4.SetActive(false);
        pos5.SetActive(false);
        //开始移动，参数一是路线，参数二是时间，参数三是路线类型，参数四是Lookat模式
        //参数五是路线研三，这个函数不是很懂，还需要多研究
        P.transform.DOPath(m_Path, 100f, PathType.CatmullRom, PathMode.Full3D, 30, Color.red);
        pause.SetActive(true);//暂停按钮显示
        //多出一个暂停按钮的背景
        //GameObject.Find("Canvas (1)/RawImage_dingdian").transform.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 260);
    }

    /// <summary>
    /// 巡游暂停
    /// </summary>
    public void cruiseStop()
    {
        P.transform.DOKill();
    }

    /// <summary>
    /// 判断是否巡游暂停
    /// </summary>
    public void cruisePauseOrContinue()
    {
        //如果pauseOrContinue是true，表明需要暂停
        if (pauseOrContinue == true)
        {
            cruisePause();//暂停
            pauseOrContinue = false;//修改pauseOrContinue数值
        }
        else
        {
            cruiseContinue();//继续
            pauseOrContinue = true;//修改pauseOrContinue数值
        }
    }

    /// <summary>
    /// 暂停进行
    /// </summary>
    public void cruisePause()
    {
        P.transform.DOPause();//暂停移动
        pauseText.GetComponent<Text>().text = "继续";//修改文本
    }

    /// <summary>
    /// 继续进行
    /// </summary>
    public void cruiseContinue()
    {
        P.transform.DOPlay();//继续进行
        pauseText.GetComponent<Text>().text = "暂停";//修改文本
    }
}
