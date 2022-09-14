using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SouSuoJianTing : MonoBehaviour
{

    private string valueText;
    private static string endValue;

    // Use this for initialization
    void Start()
    {
        GetComponent<InputField>().onValueChanged.AddListener(ChangedValue);//用户输入文本时就会调用
        GetComponent<InputField>().onEndEdit.AddListener(EndValue);//文本输入结束时会调用
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ChangedValue(string value)
    {
        valueText = value;//将用户输入的值赋值给内部的空字符串，我们可以将其来进行后续的操作
        new ZhanShi().SouSuoTupian(valueText);

    }
    private void EndValue(string value)
    {
        endValue = value;//捕捉数据，方便后续操作

    }

    public void SouSuoTuPian()
    {
        new ZhanShi().SouSuoTupian(endValue);
    }
}
