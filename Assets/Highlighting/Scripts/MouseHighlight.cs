using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 第一模块MainCamera
/// 鼠标选中时高亮
/// </summary>
public class MouseHighlight : MonoBehaviour
{

    public GameObject gameCheck;

    //使用generateObjects变量的方法，在update函数里无法识别，目前暂不清楚原因
    //private List<GameObject> generateObjects;

    //private void Start()
    //{
    //    generateObjects = laqu1.generateObjects;
    //}

    private void Update()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//鼠标的屏幕坐标转化为一条射线
                RaycastHit hit;

                //距离为5
                //if(Physics.Raycast(ray, out hit, 5)) {
                //    var hitObj = hit.collider.gameObject;
                //    Debug.Log(hitObj);
                //}
                //无距离限制
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObj = hit.collider.gameObject;
                    //Debug.Log("在点击");
                    //foreach (GameObject item in generateObjects)
                    //{
                    //    Debug.Log(item.name+ "---MouseHighlight");
                    //}
                    //如果获取到的目标是生成物体list里的，那么就高亮  
                    if (laqu1.generateObjects.Contains(hitObj))
                    {
                        Debug.Log(hitObj.name);
                        //设置此物体高亮  效果应该是拖拽时一直高亮，鼠标松开就不高亮
                        SetObjectHighlight(hitObj);
                        //设置可以移动的脚本
                        hitObj.GetComponent<Rotate>().enabled = true;
                        //设置可以移动
                        hitObj.GetComponent<Rotate>().flag2 = 1;
                        //此处应该做处理：如果没有物体被选中，则“锁定选中”与“解锁选中”按钮都不可用
                        //将此物体传给model，即鼠标点击哪一个就应该控制哪一个的锁定与否
                        //GameObject.Find("GameObject").GetComponent<model>().cube = hitObj;
                    }


                }
            }
        }
    }

    /// <summary>
    /// 设置物体高亮
    /// </summary>
    /// <param name="obj"></param>
    public void SetObjectHighlight(GameObject obj)
    {
        if (gameCheck == null)
        {
            //如果当前gameCheck没有内容，那么添加
            AddComponent(obj);
        }
        else if (gameCheck == obj)
        {
            //RemoveComponent(obj);
            //由于鼠标松开时，取消了高亮，这里需要重新加上
            AddComponent(obj);
        }
        else
        {
            RemoveComponent(gameCheck);
            AddComponent(obj);
        }
    }

    /// <summary>
    /// 移出组件
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveComponent(GameObject obj)
    {
        if (obj.GetComponent<SpectrumController>() != null)
        {
            Destroy(obj.GetComponent<SpectrumController>());
        }

        if (obj.GetComponent<HighlightableObject>() != null)
        {
            Destroy(obj.GetComponent<HighlightableObject>());
        }

        gameCheck = null;
    }

    /// <summary>
    /// 添加高亮组件
    /// </summary>
    /// <param name="obj"></param>
    public void AddComponent(GameObject obj)
    {
        //如果高亮的两个组件，没有，那就加上
        if (obj.GetComponent<SpectrumController>() == null)
        {
            obj.AddComponent<SpectrumController>();
            obj.AddComponent<HighlightableObject>();
            //obj.GetComponent<HighlightableObject>().enabled = true;
        }
        //设置高亮的两个组件可以起作用
        obj.GetComponent<SpectrumController>().enabled = true;
        obj.GetComponent<HighlightableObject>().enabled = true;
        gameCheck = obj;
    }

    /// <summary>
    /// 删除组件，并且从生成组件list里删除,绑定的是场景管理模块中的模型的删除按钮
    /// </summary>
    public void delete()
    {
        //并且从生成组件list里删除
        laqu1.generateObjects.Remove(gameCheck);
        //删除组件
        if (gameCheck != null)
        {
            Destroy(gameCheck);
        }
    }

}
