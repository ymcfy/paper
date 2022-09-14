using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickController : MonoBehaviour
{

    //是否有一次单击
    private bool hasOneClick = false;

    //默认双击时间间隔
    public float doubleClickInterval = 0.5f;

    //计时器
    private float timer = 0;

    // Use this for initialization
    void Start()
    {

    }

    public void judge()
    {
        if (hasOneClick)
        {
            timer += Time.deltaTime;
            Debug.Log("90");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //如果完成第一次点击，开始计时


        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (!hasOneClick)
        //    {
        //        Debug.Log("97");
        //        //添加单击事件
        //        OneClick();
        //        //标记完成一次点击
        //        hasOneClick = true;
        //    }
        //    //当前是第二次点击
        //    else
        //    {
        //        //判断时间间隔是否超时
        //        if (timer < doubleClickInterval)
        //        {
        //            Debug.Log("109");
        //            //添加双击的事件
        //            DoubleClick();

        //            //收尾操作：重置标志位,计时器归零
        //            hasOneClick = false;
        //            timer = 0;
        //        }
        //        //操作超时，判断它不为一次双击
        //        //虽然第二次点击超时，但是我们应该把它作为第二次双击操作的第一次单击，而不是要重新再来一次双击
        //        else
        //        {
        //            Debug.Log("121");
        //            //计时器归零
        //            timer = 0;
        //            //再次进行单击应该实现的功能
        //            OneClick();
        //        }
        //    }
        //}
    }
}
