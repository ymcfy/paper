using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XCharts;

/// <summary>
/// 检查罐车和接收储罐的液位、压力和温度
/// 展示相应数据图表
/// </summary>
public class CheckTemperature : MonoBehaviour {

    private CoordinateChart chart;
    private float updateTime;
    private DateTime timeNow;
    public int maxCacheDataNumber = 100;//文本数据集最大数据量
    public TextAsset TxtFile;    //建立TextAsset
    private string Mytxt;          //用来存放文本内容
    private string[] text;//sting类型温度值
    public float[] temperature;//float类型温度值
    private int count;

    void Awake()
    {
        chart = gameObject.GetComponentInChildren<CoordinateChart>();
        chart.RemoveData();
        var serie = chart.AddSerie(SerieType.Line);
        serie.symbol.show = false;
        serie.maxCache = maxCacheDataNumber;
        chart.xAxises[0].maxCache = maxCacheDataNumber;
        timeNow = DateTime.Now;
        timeNow = timeNow.AddSeconds(-maxCacheDataNumber);

    }

    private void Start()
    {
        count = 0;
        LoadTex();//加载文本
    }

    void Update()
    {

        //设置读取时间间隔
        updateTime += Time.deltaTime;
        if (updateTime >= 1)
        {
            if (count < maxCacheDataNumber && count < temperature.Length)
            {
                AddTemperature();//数据读取个数小于等于最大数据量时添加温度数据
            }
            else if (count >= maxCacheDataNumber && count < temperature.Length)
            {
                AddStaticTemperature();//数据读取个数大于最大数据量时添加温度数据
            }else{
                
            }

        }


    }


    void LoadTex()//加载文本文件
    {
        Mytxt = TxtFile.text;
        Debug.Log("Mytxt is " + Mytxt);                //输出验证

        text = Mytxt.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        temperature = text.Select(x => Convert.ToSingle(x)).ToArray();
        for (int i = 0; i < text.Length; i++)
        {
            Debug.Log("temperature is " + temperature[i]);
        }
    }
    void AddTemperature()
    {
        updateTime = 0;
        string category = DateTime.Now.ToString("hh:mm:ss");
        chart.AddXAxisData(category);
        chart.AddData(0, temperature[count]);
        chart.RefreshChart();
        count++;
    }//动态添加温度
    void AddStaticTemperature()
    {
        updateTime = 0;
        string category = DateTime.Now.ToString("hh:mm:ss");
        chart.AddXAxisData(category);
        chart.AddData(0, temperature[maxCacheDataNumber - 1]);
        chart.RefreshChart();
    }//添加温度
}
