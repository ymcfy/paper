using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 用于模型搜索时仅限搜索到的物体高亮
/// </summary>
public class SearchMouseHightLight : MonoBehaviour {
    //搜索物体名字
    string SelectionText = HightLightSelectionMonitor.endValue;

    // Use this for initialization
    void Start () {
		
	}
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
                    if (GameObject.Find("液化气装卸站台" + SelectionText)==hitObj)
                    {
                        //Debug.Log(hitObj.name);
                        //设置此物体高亮
                        SetObjectHighlight(hitObj);
                        //此处应该做处理：如果没有物体被选中，则“锁定选中”与“解锁选中”按钮都不可用
                        //将此物体传给model，即鼠标点击哪一个就应该控制哪一个的锁定与否
                        GameObject.Find("GameObject").GetComponent<model>().cube = hitObj;
                    }


                }
            }
        }
    }
    /// <summary>
    /// 添加高亮组件
    /// </summary>
    /// <param name="obj"></param>
    public void SetObjectHighlight(GameObject obj)
    {
        if (obj.GetComponent<SpectrumController>() == null)
        {
            obj.AddComponent<SpectrumController>();
        }
      
    }

}
