using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 鼠标选中时高亮
/// </summary>
public class mouse : MonoBehaviour
{
    public Camera camera;
    public GameObject gameCheck;

    void Update()
    {

    }

    /// <summary>
    /// 设置物体高亮
    /// </summary>
    /// <param name="obj"></param>
    public void SetObjectHighlight(GameObject obj)
    {
        if (gameCheck == null)
        {
            AddComponent(obj);
        }
        else if (gameCheck == obj)
        {
            RemoveComponent(obj);
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
        if (obj.GetComponent<SpectrumController>() == null)
        {
            obj.AddComponent<SpectrumController>();
        }
        gameCheck = obj;
    }
    public void delete()
    {
        if (gameCheck != null)
        {
            Destroy(gameCheck);
        }
    }

}

