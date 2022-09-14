using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于高亮搜索监听，将监听到的搜索数据传入HightLightSelection类进行搜索
/// </summary>
public class HightLightSelectionMonitor : MonoBehaviour {

    
    public static  string endValue;

    

    // Use this for initialization
    void Start () {
        
        GetComponent<InputField>().onEndEdit.AddListener(EndValue);//文本输入结束时会调用
    }

    private void EndValue(string value)
    {
        endValue = value;//捕捉数据，方便后续操作
        Debug.Log("当前输入框内容为："+value);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
