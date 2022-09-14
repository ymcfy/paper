
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 控制生成模型的移动
/// </summary>
public class Rotate : MonoBehaviour
{
    public GameObject cube;  //要拖拽的物体
    private float 位置x, 位置y, 位置z;
    private float 角度x, 角度y, 角度z;
    private float InitialPositionX;
    private float InitialPositiony;
    private float InitialPositionZ;
    private float InitialAngelX;
    private float InitialAngelY;
    private float InitialAngelZ;
    private float InitialScaleX;
    private float InitialScaleY;
    private float InitialScaleZ;
    private float 移动速度, 视角改变速度;
    public Vector3 A;
    private Vector3 mouse;    //鼠标
    private Vector3 screeenV;  //存储cube的屏幕坐标
    private Vector3 world;    //记录鼠标坐标转成的世界坐标
    public bool QIEHUAN;
    public Text text;
    public Text text1;
    public int flag;//活动形式，如前后、左右、旋转等，目前已弃用
    public int flag1;//是否被锁定，1是未被锁定，2是被锁定
                     // Use this for initialization
    //判断当前是拾起物体还是放下物体
    public int flag2 = 0;

    private void Start()
    {

        cube = gameObject;
        InitialPositionX = cube.transform.position.x;
        InitialPositiony = cube.transform.position.y;
        InitialPositionZ = cube.transform.position.z;
        InitialAngelX = cube.transform.eulerAngles.x;
        InitialAngelY = cube.transform.eulerAngles.y;
        InitialAngelZ = cube.transform.eulerAngles.z;
        InitialScaleX = cube.transform.localScale.x;
        InitialScaleY = cube.transform.localScale.y;
        InitialScaleZ = cube.transform.localScale.z;
        //GameObject.Find("GameObject").GetComponent<model>().cube = cube;
        GameObject.Find("GameObject").GetComponent<model>().InitialAngelX = InitialAngelX;
        GameObject.Find("GameObject").GetComponent<model>().InitialAngelY = InitialAngelY;
        GameObject.Find("GameObject").GetComponent<model>().InitialAngelZ = InitialAngelZ;
        GameObject.Find("GameObject").GetComponent<model>().InitialPositiony = InitialPositiony;
        GameObject.Find("GameObject").GetComponent<model>().InitialPositionX = InitialPositionX;
        GameObject.Find("GameObject").GetComponent<model>().InitialPositionZ = InitialPositionZ;
        GameObject.Find("GameObject").GetComponent<model>().InitialScaleX = InitialScaleX;
        GameObject.Find("GameObject").GetComponent<model>().InitialScaleY = InitialScaleY;
        GameObject.Find("GameObject").GetComponent<model>().InitialScaleZ = InitialScaleZ;
        //text = GameObject.Find("zhuangtaixianshi").GetComponent<Text>();
        //text1 = GameObject.Find("物体状态").GetComponent<Text>();
        移动速度 = 0.3f;
        视角改变速度 = 45;
        A = GetComponent<Transform>().position;
        flag = 0;
        flag1 = 2;
        //text.text = "";
    }



    // Update is called once per frame
    private void Update()
    {
        flag = 1;
        flag1 = GameObject.Find("GameObject").GetComponent<model>().flag1;
        位置x = cube.transform.position.x;
        位置y = cube.transform.position.y;
        位置z = cube.transform.position.z;
        角度x = cube.transform.eulerAngles.x;
        角度y = cube.transform.eulerAngles.y;
        角度z = cube.transform.eulerAngles.z;
        if (flag == 2)
        {
            text.text = "前后模式";
        }
        if (flag == 1)
        {
            //text.text = "上下模式";
        }
        if (flag == 3)
        {
            text.text = "旋转模式";
        }

        //判断鼠标左键是否按在了当前物体
        //如果鼠标左键按住当前物体，物体跟着鼠标走
        //如果鼠标左键松开，物体固定位置

        screeenV = Camera.main.WorldToScreenPoint(cube.transform.position);
        //当鼠标第一次单击时记录下cube在场景中的坐标，并把世界坐标转成屏幕坐标
        //if (!EventSystem.current.IsPointerOverGameObject() && (cube.GetComponent<SpectrumController>() != null))
        //此处把&& (cube.GetComponent<SpectrumController>() != null)注释掉，使得无论如何都能获取到鼠标坐标
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            mouse = Input.mousePosition;  //当鼠标移动时记录下鼠标的坐标
            mouse.z = screeenV.z;  //因为鼠标的z坐标为0，所以需要一个z坐标
                                   //把鼠标的屏幕坐标转换成世界坐标
            world = Camera.main.ScreenToWorldPoint(mouse);
        }

        /**
        //当鼠标移动时，cube也发生移动，为了让cube的y轴不发生移动，设y轴为原来的y轴
        if (Input.GetMouseButton(0) && flag == 2 && !EventSystem.current.IsPointerOverGameObject() && (cube.GetComponent<SpectrumController>() != null))
        {
            cube.transform.position = new Vector3(world.x, cube.transform.position.y, world.z);
            //print(cube.transform.position);
        }
        //让物体跟着鼠标移动
        if (Input.GetMouseButton(0) && flag == 1 && !EventSystem.current.IsPointerOverGameObject())
        {
            cube.transform.position = new Vector3(world.x, world.y, cube.transform.position.z);
            Debug.Log(world.x + "  " + world.y + "  " + cube.transform.position.z);
            //print(cube.transform.position);
        }
        if (Input.GetMouseButton(0) && flag == 3 && !EventSystem.current.IsPointerOverGameObject())
        {
            cube.gameObject.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") *
                  Time.deltaTime * 300, -Input.GetAxis("Mouse X") * Time.deltaTime * 300, 0));
        }
        //if (cube.GetComponent<SpectrumController>() != null && flag1 == 2)
        */
        //如果左键按下，并且flag2是1，那么物体跟着鼠标走
        if (Input.GetMouseButton(0)  && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(flag2+"==================");
            
            if (flag2 == 1)
            {
                cube.transform.position = new Vector3(world.x, world.y, world.z);
                //Debug.Log(world.x + "  " + world.y + "  " + world.z);
            }
            
            //print(cube.transform.position);
        }
        //如果左键松开，那么不高亮并且不能再移动物体
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("左键抬起");
            flag2 = 0;
            if (cube.GetComponent<HighlightableObject>() != null)
            {
                cube.GetComponent<HighlightableObject>().enabled = false;
            }
            
            
        }

        //锁定
        //if (flag1 == 2)
        //{
        //    cube.GetComponent<Rotate>().enabled = false;
        //    //cube.GetComponent<SpectrumController>().enabled = false;
        //    //text1.text = "锁定";

        //}
        //if (cube.GetComponent<SpectrumController>() != null && flag1 == 1)
        //if ( flag1 == 1)
        //{
        //    //cube.GetComponent<Rotate>().enabled=true;
        //    //text1.text = "激活";
        //}

    }
}