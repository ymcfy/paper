using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 控制二级菜单下三级菜单的出现与消失
/// 鼠标悬浮经过就产生，悬浮离开就在timer时间后消失
/// </summary>
public class ButtonThreeHoverControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 所有的当前模块下三级菜单的按钮
    /// </summary>
    public GameObject ThirdButtons;
    /// <summary>
    /// 定时时间
    /// </summary>
    public float timer = 0.1f; 
    /// <summary>
    /// 是否需要开始计时关闭当前二级菜单，true为需要，false为不需要
    /// </summary>
    public static bool flagSecond = false;
    /// <summary>
    /// 当前鼠标是否在当前菜单上，如果在，那么如果鼠标移开上级菜单，进入当前菜单
    /// 当前菜单不可消失
    /// </summary>
    //public static bool flagOne = false;
    /// <summary>
    /// 判断是否进入了三级菜单
    /// </summary>
    public static bool flagThreeEnter = false;


    private void Update()
    {
        //if (flagOne == true)
        //{
        //    this.gameObject.SetActive(true);
        //}
        ////当flagSecond为true时，倒计时timer时间来隐藏二级菜单
        //if (flagSecond == true)
        //{
        //    //时间倒退
        //    timer -= Time.deltaTime;
        //    //当timer为0时
        //    if (timer <= 0)
        //    {
        //        //隐藏二级菜单
        //        ThirdButtons.SetActive(false);
        //        //重置timer,以便下次打开二级菜单以及隐藏二级菜单
        //        timer = 0.5f;
        //        //重置flagSecond，使得二级菜单处于显示状态
        //        flagSecond = false;
        //    }
        //}
    }
    /// <summary>
    /// 当鼠标悬浮进入时要做的操作
    /// 告诉上一级菜单，已经进入三级菜单，不能隐藏当前菜单
    /// 显示对应的四级菜单
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //告诉上级菜单，已进入三级菜单
       ButtonTwoHoverControl.flagThreeEnter = true;
        //三级菜单显示
        //ThirdButtons.SetActive(true);
        //flagOne = true;
    }
    /// <summary>
    /// 当鼠标悬浮退出时要做的操作
    /// 有三种可能，
    /// 进入其他二级菜单：flagTwoEnter还是true
    /// 显示二级菜单，当前三级菜单消失，显示新的三级菜单
    /// 进入三级菜单:三级菜单报告进入
    /// 显示二级菜单、三级菜单、四级菜单
    /// 到其他位置：三级菜单未报告进入 并且 flagTwoEnter是false
    /// 所有菜单消失
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //开始倒计时关闭二级菜单
        //flagSecond = true;
    }
}
