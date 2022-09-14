using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 场景4 kongzhi2
/// </summary>
public class peixun : MonoBehaviour
{
    public GameObject qiche;
    public Text text;
    public GameObject button;
    public GameObject shijiao1;
    public GameObject door1;
    public GameObject door2;
    public GameObject caozuotai;
    //液化气检验单图片
    public GameObject image;

    //存放读取温度数据的脚本所挂接物体
    public GameObject linechart;

    //培训流程界面
    public GameObject trainningInterface;

    //第一个液管
    public GameObject fluidPipe1;
    //第二个液管
    public GameObject fluidPipe2;
    //气管
    public GameObject gasPipe;

    //进度条中的各个button
    public GameObject button1;
    public GameObject button2;
    public GameObject button2_1;
    public GameObject button2_2;
    public GameObject button2_3;
    public GameObject button3;
    public GameObject button4;

    //培训流程中的各个button
    public GameObject methodButton1;
    public GameObject methodButton2;
    public GameObject methodButton3;
    public GameObject methodButton4;
    public GameObject methodButton5;
    public GameObject methodButton6;
    public GameObject methodButton7;
    public GameObject methodButton8;
    public GameObject methodButton9;

    //汽车移动Camera
    public GameObject cameraCar;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    /// <summary>
    /// 场景4  Canvas/peixun/next “移动”按钮
    /// 将罐车移动到站台
    /// </summary>
	public void move()
    {
        Debug.Log(button1.name);
        methodButton2.SetActive(true);
        methodButton1.SetActive(false);
        methodButton3.SetActive(false);
        methodButton4.SetActive(false);
        methodButton5.SetActive(false);
        methodButton6.SetActive(false);
        methodButton7.SetActive(false);
        methodButton8.SetActive(false);
        methodButton9.SetActive(false);
        //button2.GetComponent<Button>().Select();
        cameraCar.SetActive(true);
        shijiao1.SetActive(false);
        button1.GetComponent<Button>().Select();
        //3f参数为移动持续时间
        qiche.transform.DOMove(new Vector3(27.2f, 0, -66), 3f);
        text.text = "2. 发油准备";
        image.SetActive(false);
        linechart.SetActive(false);
    }
    /// <summary>
    /// 场景4  Canvas/peixun/next 开始发油准备按钮
    /// 开始发油准备
    /// </summary>
	public void prepare()
    {
        methodButton3.SetActive(true);
        methodButton2.SetActive(false);
        methodButton1.SetActive(false);
        methodButton4.SetActive(false);
        methodButton5.SetActive(false);
        methodButton6.SetActive(false);
        methodButton7.SetActive(false);
        methodButton8.SetActive(false);
        methodButton9.SetActive(false);
        button2.GetComponent<Button>().Select();
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        //GameObject.Find("Camera").SetActive(false);
        text.text = "检查液化石油气检验单";
        image.SetActive(false);
        linechart.SetActive(false);
    }

    /// <summary>
    /// 场景4  Canvas/peixun/next(7)
    /// 结束检查罐车和接收储罐的液位、压力和温度完成
    /// 开始查看管线接口
    /// </summary>
    public void chaguan()
    {
        methodButton7.SetActive(true);
        methodButton2.SetActive(false);
        methodButton3.SetActive(false);
        methodButton4.SetActive(false);
        methodButton5.SetActive(false);
        methodButton6.SetActive(false);
        methodButton1.SetActive(false);
        methodButton8.SetActive(false);
        methodButton9.SetActive(false);
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        button2.GetComponent<Button>().Select();
        button2_3.GetComponent<Button>().Select();
        fluidPipe1.GetComponent<cube2>().enabled = true;
        fluidPipe2.GetComponent<cube2>().enabled = true;
        gasPipe.GetComponent<cube2>().enabled = true;
        trainningInterface.transform.position = new Vector3(1552, 661, 0);
        linechart.SetActive(false);
        door1.SetActive(false);
        door2.SetActive(false);
        shijiao1.GetComponent<HighlightingEffect>().enabled = true;
        shijiao1.GetComponent<mouse1>().enabled = true;
        text.text = "将鼠标放在接口查看需要接的管";
        image.SetActive(false);
    }
    public void final()
    {
        methodButton8.SetActive(true);
        methodButton2.SetActive(false);
        methodButton3.SetActive(false);
        methodButton4.SetActive(false);
        methodButton5.SetActive(false);
        methodButton6.SetActive(false);
        methodButton7.SetActive(false);
        methodButton1.SetActive(false);
        methodButton9.SetActive(false);
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        button3.GetComponent<Button>().Select();
        fluidPipe1.GetComponent<cube2>().enabled = false;
        fluidPipe2.GetComponent<cube2>().enabled = false;
        gasPipe.GetComponent<cube2>().enabled = false;
        text.text = "3. 接完之后先开放散阀，后开放空阀。 然后打开紧急切断阀，之后开启球阀。";
        //text.fontStyle = FontStyle.Bold;
        //text.color = Color.red;
        shijiao1.GetComponent<HighlightingEffect>().enabled = false;
        shijiao1.GetComponent<mouse1>().enabled = false;
        image.SetActive(false);
        linechart.SetActive(false);
    }
    public void over()
    {
        methodButton9.SetActive(true);
        methodButton2.SetActive(false);
        methodButton3.SetActive(false);
        methodButton4.SetActive(false);
        methodButton5.SetActive(false);
        methodButton6.SetActive(false);
        methodButton7.SetActive(false);
        methodButton8.SetActive(false);
        methodButton1.SetActive(false);
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        button4.GetComponent<Button>().Select();
        text.text = "4. 准备工作完毕,进行发油";
        text.fontStyle = FontStyle.Normal;
        text.color = Color.white;
        image.SetActive(false);
        linechart.SetActive(false);
    }
    public void fayou()
    {
        //GameObject.Find("Canvas4/peixun").SetActive(false);
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        caozuotai.SetActive(true);
        button.SetActive(true);
        door1.SetActive(true);
        door2.SetActive(true);
        image.SetActive(false);
        linechart.SetActive(false);
    }

    /// <summary>
    ///开始检查检验单：检验单出现、检查完毕按钮出现
    /// </summary>
    public void CheckLPGInspectionList()
    {
        methodButton4.SetActive(true);
        methodButton2.SetActive(false);
        methodButton3.SetActive(false);
        methodButton1.SetActive(false);
        methodButton5.SetActive(false);
        methodButton6.SetActive(false);
        methodButton7.SetActive(false);
        methodButton8.SetActive(false);
        methodButton9.SetActive(false);
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        button2.GetComponent<Button>().Select();
        button2_1.GetComponent<Button>().Select();
        image.SetActive(true);
        linechart.SetActive(false);
    }

    /// <summary>
    /// 检验单检查完毕，检验单消失，text要修改
    /// </summary>
    public void CheckLPGInspectionListFinished()
    {
        methodButton5.SetActive(true);
        methodButton2.SetActive(false);
        methodButton3.SetActive(false);
        methodButton4.SetActive(false);
        methodButton1.SetActive(false);
        methodButton6.SetActive(false);
        methodButton7.SetActive(false);
        methodButton8.SetActive(false);
        methodButton9.SetActive(false);
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        button2.GetComponent<Button>().Select();
        button2_2.GetComponent<Button>().Select();
        image.SetActive(false);
        text.text = "检查罐车和接收储罐的液位，压力和温度";
        linechart.SetActive(false);
    }

    /// <summary>
    /// 场景4  Canvas/peixun/next(6)
    /// 检查罐车和接收储罐的液位、压力和温度
    /// </summary>
    public void CheckTemperature()
    {
        image.SetActive(false);
        methodButton6.SetActive(true);
        methodButton2.SetActive(false);
        methodButton3.SetActive(false);
        methodButton4.SetActive(false);
        methodButton5.SetActive(false);
        methodButton1.SetActive(false);
        methodButton7.SetActive(false);
        methodButton8.SetActive(false);
        methodButton9.SetActive(false);
        cameraCar.SetActive(false);
        shijiao1.SetActive(true);
        button2.GetComponent<Button>().Select();
        button2_2.GetComponent<Button>().Select();
        linechart.SetActive(true);
    }


}
