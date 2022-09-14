using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 控制一级菜单下二级菜单的出现与消失
/// 鼠标悬浮经过就产生，悬浮离开就在timer时间后消失
/// </summary>
public class ButtonHoverControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 所有的当前模块下二级菜单的按钮
    /// </summary>
    public GameObject SecondButtons;
    /// <summary>
    /// 定时时间
    /// </summary>
    public float timer = 0.5f; 
    /// <summary>
    /// 是否需要开始计时关闭当前二级菜单，true为需要，false为不需要
    /// </summary>
    public static bool flagSecond = false;
    /// <summary>
    /// 当前鼠标是否在当前菜单上，如果在，那么如果鼠标移开上级菜单，进入当前菜单
    /// 当前菜单不可消失
    /// </summary>
    public bool flagOne = false;
    /// <summary>
    /// 判断是否进入了二级菜单
    /// </summary>
    public static bool flagTwoEnter = false;

    //把所有二级菜单放到一个list里
    //声明一个list
   static List<GameObject> listTwo = new List<GameObject>();
    //场景漫游模块
    public GameObject SceneRoam;
    //设备管理模块
    public GameObject EquipmentManagement;
    //标准操作模块
    public GameObject StandardOperation;
    //事故与应急模块
    public GameObject AccidentAndEmergency;
    //模型库管理管理模块
    public GameObject ModelBaseManagement;
    //场景编辑模块
    public GameObject SceneEditing;
    //系统设置模块
    public GameObject SystemSetup;

    private void Start()
    {
        listTwo.Add(SceneRoam);
        listTwo.Add(EquipmentManagement);
        listTwo.Add(StandardOperation);
        listTwo.Add(AccidentAndEmergency);
        listTwo.Add(ModelBaseManagement);
        listTwo.Add(SceneEditing);
        listTwo.Add(SystemSetup);
    }

    /// <summary>
    /// 关闭所有二级菜单
    /// </summary>
    public static void closeAllTwoMenu() {
        for (int i = 0; i < listTwo.Count; i++)
        {
            if (listTwo[i] != null)
            {
                listTwo[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        //if (flagOne == true)
        //{
        //    this.gameObject.SetActive(true);
        //}
        //随时判断当前菜单的二级菜单有没有进入
        //if (flagTwoEnter == false)
        //{

        //}
        //Debug.Log(flagSecond+"111");
        //if (flagTwoEnter == true)
        //{
        //    Debug.Log(flagTwoEnter + "222");
        //}
        //if (flagSecond == false)
        //{
        //    Debug.Log(flagSecond + "111");
        //}

        ////当flagSecond为true时，倒计时timer时间来隐藏二级菜单
        //if (flagSecond == true )
        //{
        //    //时间倒退
        //    timer -= Time.deltaTime;
        //    //当timer为0时
        //    if (timer <= 0)
        //    {
        //        //隐藏二级菜单
        //        SecondButtons.SetActive(false);
        //        //重置timer,以便下次打开二级菜单以及隐藏二级菜单
        //        timer = 0.5f;
        //        //重置flagSecond，使得二级菜单处于显示状态
        //        flagSecond = false;
        //    }
        //}
    }
    /// <summary>
    /// 当鼠标悬浮进入时要做的操作
    /// 关闭所有二级菜单，打开当前二级菜单
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        //二级菜单显示
        SecondButtons.SetActive(true);
        //flagOne = true;
    }
    /// <summary>
    /// 当鼠标悬浮退出时要做的操作
    /// 两种可能，进入二级菜单或者其他位置
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //开始倒计时关闭二级菜单
        //flagSecond = true;
        //if (flagTwoEnter == false)
        //{
        //    //没进入二级菜单，那么开始倒计时关闭二级菜单
        //    flagSecond = true;
            
        //}
        //else
        //{
        //    //进入了二级菜单，暂时啥都不做
        //}
    }
}
