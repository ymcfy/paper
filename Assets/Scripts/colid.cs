
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// 鼠标选中时高亮
/// </summary>
public class colid : MonoBehaviour
{
    public Text text;
    public GameObject gameCheck;
    public bool shiqu = false;
    private bool panduan = false;
    public GameObject P;
    private void Start()
    {
        P.SetActive(false);
        gameCheck = this.gameObject;
    }
    void Update()
    {
        miehuoqi();
        if (panduan)
        {
            text.text = "请前往火灾现场\n" + "按Q键开启灭火装置";
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            panduan = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {

        text.text = "请按T拾取灭火器";
        shiqu = true;
        AddComponent(gameCheck);
    }
    void OnCollisionExit(Collision collision)
    {
        RemoveComponent(gameCheck);
        shiqu = false;
    }

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

    }
    void miehuoqi()
    {
        if (shiqu == true)
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                P.SetActive(true);
                panduan = true;
                text.text = "请前往火灾现场\n" + "按Q键开启灭火装置";
            }
        }
    }


}
