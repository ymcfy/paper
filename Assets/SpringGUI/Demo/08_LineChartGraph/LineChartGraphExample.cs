
/*=========================================
* Author: springDong
* Description: SpringGUI.LineChartGraph example.
==========================================*/

using System.Collections.Generic;
using SpringGUI;
using UnityEngine;

public class LineChartGraphExample : MonoBehaviour
{
    public class TestData
    {
        public float xValue { get; set; }
        public float yValue { get; set; }

        public TestData( float x ,float y )
        {
            xValue = x;
            yValue = y;
        }
    }

    public LineChart LineChart = null;

    public void Awake()
    {
        var data1 = new List<TestData>()
            {
                new TestData(0.0f,0.7f),
                new TestData(0.09f,0.1f),
                new TestData(0.18f,0.5f),
                new TestData(0.27f,0.6f),
                new TestData(0.36f,0.7f),
                new TestData(0.45f,0.2f),
                new TestData(0.54f,0.72f),
                new TestData(0.63f,0.24f),
                new TestData(0.72f,0.52f),
                new TestData(0.81f,0.1f),
                new TestData(0.9f,0.3f),
                new TestData(1f,1.0f),
            };
        //var data2 = new List<TestData>()
        //    {
        //        new TestData(0.0f,0.7f),
        //        new TestData(0.1f,0.1f),
        //        new TestData(0.2f,0.5f),
        //        new TestData(0.3f,0.6f),
        //        new TestData(0.4f,0.7f),
        //        new TestData(0.5f,0.2f),
        //        new TestData(0.6f,0.72f),
        //        new TestData(0.7f,0.24f),
        //        new TestData(0.8f,0.52f),
        //        new TestData(0.9f,0.1f),
        //        new TestData(1f,0.0f),
        //    };
        LineChart.Inject<TestData>(data1);
      //  LineChart.Inject<TestData>(data2);
        LineChart.ShowUnit();
    }

    public void OnGUI()
    {
        
    }
}