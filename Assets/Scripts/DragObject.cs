using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour
{
    /// <summary>
    /// 将要拖动的物体
    /// </summary>
    private Transform dragGameObject;
    /// <summary>
    /// 获取射线需要碰撞的层
    /// </summary>
    private LayerMask canDrag;
    /// <summary>
    /// 直接从外部定义好层，简单理解
    /// </summary>
    public LayerMask canDrag2;
    /// <summary>
    /// 获得鼠标的位置和cube位置差
    /// </summary>
    private Vector3 offset;
    /// <summary>
    /// 是否点击到cube
    /// </summary>
    private bool isClickCube;
    /// <summary>
    /// 目标对象的屏幕坐标
    /// </summary>
    private Vector3 targetScreenPoint;

    // Use this for initialization
    void Start()
    {
        // LayerMask.GetMask("Cube"); 得到 名字为 Cube 的层的 2 进制
        // LayerMask.LayerToName(9);  得到一个 10 进制表示的层 的名字 这里既第十层
        // LayerMask.NameToLayer("Cube");  得到 名字为 Cube 的层的 10 进制

        //使用位运算，因为 LayerMask （好像）是以2进制存储的  Layer 的层是以0开始
        canDrag = 1 << LayerMask.NameToLayer("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckGameObject())
            {
                offset = dragGameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z));
            }
        }

        if (isClickCube)
        {
            //当前鼠标所在的屏幕坐标
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z);
            //把当前鼠标的屏幕坐标转换成世界坐标
            Vector3 curWorldPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);
            dragGameObject.position = curWorldPoint + offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isClickCube = false;
        }
    }

    /// <summary>
    /// 检查是否点击到cbue
    /// </summary>
    /// <returns></returns>
    bool CheckGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, canDrag))
        {
            isClickCube = true;
            //得到射线碰撞到的物体
            dragGameObject = hitInfo.collider.gameObject.transform;
            targetScreenPoint = Camera.main.WorldToScreenPoint(dragGameObject.position);
            return true;
        }
        return false;
    }

}
