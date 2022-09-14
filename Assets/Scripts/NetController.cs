/***************************************************************
文件名：NetController.cs    作者：罗克诚    版本：Version 1.0   
版权：©2013-2017 上海形拓科技有限公司 版权所有
功能描述：
创建时间：
***************************************************************/
using System;
using UnityEngine;
using UnityEngine.UI;
using SimtopNet;
using LitJson;
using System.IO;
using System.Text;
using System.Collections;
public class NetController : MonoBehaviour
{
    public Text oilCanInfo;
    public Text carLoadingInfo;
    public Text securityInfo;
    public Text valveInfo;
    public Text firevalveInfo;
    public static string oil;
    public bool connect;//判断是否连接网络
    #region 必要变量和代码  
    private Subscriber subscriber;
    private Action o;
    private void Awake()
    {
        //subscriber = new Subscriber();
        //subscriber.receive += ReceiveMsg;
    }
    void Start()
    {
        //subscriber.SubscribeData("RealTimeSecurity");
    }
    void Update()
    {
        if (ActionConcurrentQueue.TryDequeue(out o))
        {
            o();
        }
    }
    void OnApplicationQuit()
    {
        subscriber.Cleanup();
    }
    #endregion

    public void SubcribeRealtimeData(string topic)
    {
        subscriber.SubscribeData(topic);
        //connect = true;
    }
    public void UnsubcribeRealtimeData(string topic)
    {
        subscriber.UnsubcribeData(topic);
    }
    public void UnsubcribeRealtimeDataAll()
    {
        subscriber.UnsubscribeDataAll();
    }
    public void ReceiveMsg(string msg)
    {
        oilCanInfo.text = msg.Substring(0, msg.Length - 1);
        if (msg.IndexOf("YGMC") != -1 || msg.IndexOf("OilCan") != -1)
        {
            oilCanInfo.text = msg.Substring(0, msg.Length - 1);
        }
        if (msg.IndexOf("TankName") != -1)
        {
            Debug.Log("zhuangche=" + msg);
            carLoadingInfo.text = msg.Substring(0, msg.Length - 1);
        }
        if (msg.IndexOf("DeviceClass") != -1 || msg.IndexOf("AlarmStatus") != -1)
        {
            Debug.Log("anfang=" + msg);
            securityInfo.text = msg.Substring(0, msg.Length - 1);
        }
        if (msg.IndexOf("ValveID") != -1)
        {
            Debug.Log("famen=" + msg);
            valveInfo.text = msg.Substring(0, msg.Length - 1);
        }
        if (msg.IndexOf("FMBH") != -1)
        {
            Debug.Log("xiaofang=" + msg);
            firevalveInfo.text = msg.Substring(0, msg.Length - 1);
        }
    }
}

