using UnityEngine;
using UnityEngine.EventSystems;//此处是实现拖拽必须要引用的

/// <summary>
/// 实现详细信息的拖拽
/// </summary>
public class DragDetails : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    //偏移量
    private Vector3 offset;

    /// <summary>
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        ////开始拖拽的时候记录偏移量
        //Vector3 v3;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(),
        //    eventData.position, eventData.enterEventCamera, out v3);
        //offset = transform.position - v3;
    }

    /// <summary>
    /// 拖拽时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //讨巧的笨办法，而是把设置当前位置是鼠标所在位置靠左(400.200)的位置，
        //这是因为在transform.position = eventData.position的情况下，
        //当前位置在鼠标所在位置的正右方，或者说鼠标在模型信息框的最左边靠中间的位置
        //没有使用计算偏移量的方法的原因是：
        //是因为RectTransformUtility.ScreenPointToWorldPointInRectangle
        //在模型展示中进行计算时，计算出来的v3是0，因此偏移量也是0
        transform.position = eventData.position-new Vector2(400,200);
    }

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    throw new System.NotImplementedException();
    //}
}
