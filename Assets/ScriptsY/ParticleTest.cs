﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using UnityEngine;
using UnityEngine.Profiling;

public class ParticleTest : MonoBehaviour
{
    public ParticleSystem system;
    public ParticleSystem system1;
    public GameObject go;
    public List<Vector3> list = new List<Vector3>();
    public List<Vector3> list1 = new List<Vector3>();
    public List<float> listX = new List<float>();
    public List<float> listY = new List<float>();
    public List<float> listZ = new List<float>();
    public List<float> listX1 = new List<float>();
    public List<float> listY1 = new List<float>();
    public List<float> listZ1 = new List<float>();
    public List<double> listMole6 = new List<double>();
    /// <summary>
    /// 插值后的坐标
    /// </summary>
    public List<double> listMoleinterp = new List<double>();
    public List<double> listMole6Copy = new List<double>();
    public Dictionary<Vector3, double> dictionary = new Dictionary<Vector3, double>();
    //声明一个变量，储存当前加载到第几个时间段
    public int times = 0;
    private int a = 0;
    private int b = 1;
    private bool paticalSwitch = true;
    private System.Timers.Timer timer;
    //private int releaseSourcePoint = 0;
    private int onePoint = 0;
    private int twoPoint = 0;
    private int zeroPoint = 0;
    private int threePoint = 0;
    private int fourPoint = 0;
    private int fivePoint = 0;
    private int sixPoint = 0;
    private int sevenPoint = 0;

    private float lastTime;
    private float currentTime;
    private int render = 0;

    /// <summary>
    /// flacs文件路径，当选择确定文件后更新这个变量
    /// </summary>
    private string pathFile = "";

    /// <summary>
    /// 此变量用来判断是否开始选择泄漏发生源  与点击脚本MouseHightLight对应
    /// </summary>
    public static bool isSelectFlacs = false;

    /// <summary>
    /// 此变量是泄漏核心源 与点击脚本MouseHightLight对应
    /// </summary>
    public static Vector3 flacsSourceLocation = new Vector3();

    /// <summary>
    /// FLACS扩散的相应UI，例如开始暂停等等
    /// </summary>
    public GameObject FLACSLeackSpread;
    /// <summary>
    /// 扩散训练UI，包含FLACS模拟和数值模拟等UI
    /// </summary>
    public GameObject LeakTraining;
    private void Start()
    {
        lastTime = Time.time;
        StreamReader sr = new StreamReader("D:\\grid.txt", Encoding.Default);
        string line;
        //判断当前是x y z的哪一个方向
        int judgeXyz = 1;
        while ((line = sr.ReadLine()) != null)
        {
            char[] separator = { ' ', ' ', ' ' };
            //line = sr.ReadLine();
            if (line.Contains(":") && line.Contains("-"))
            {
                string[] data = line.Split(separator);
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Trim().Contains("x-direction"))
                    {
                        //接下来的数据都是x方向的
                        judgeXyz = 1;
                    }
                    if (data[i].Trim().Contains("y-direction"))
                    {
                        //接下来的数据都是y方向的
                        judgeXyz = 2;
                    }
                    if (data[i].Trim().Contains("z-direction"))
                    {
                        //接下来的数据都是z方向的
                        judgeXyz = 3;
                    }
                    switch (judgeXyz)
                    {
                        case 1:
                            if (data[i].Trim() != string.Empty && data[i].Trim().Contains("."))
                            {
                                // 保存当前行的每一列数据
                                listX.Add(float.Parse(data[i].Trim()));
                            }
                            break;
                        case 2:
                            if (data[i].Trim() != string.Empty && data[i].Trim().Contains("."))
                            {
                                // 保存当前行的每一列数据
                                listY.Add(float.Parse(data[i].Trim()));
                            }
                            break;
                        case 3:
                            if (data[i].Trim() != string.Empty && data[i].Trim().Contains("."))
                            {
                                // 保存当前行的每一列数据
                                listZ.Add(float.Parse(data[i].Trim()));
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        //float[] fx = new float[] { 220.1F ,   222 ,   224 ,   226 ,   228 ,
        //230 ,   232 ,   234 ,   236 ,   238 ,
        //240 ,   242 ,   244 ,   246 ,   248 ,
        //250 ,   252 ,   254 ,   256 ,   258 ,
        //260 ,   262 ,   264 ,   266 ,   268 ,
        //270 ,   272 ,   274 ,   276 ,   278 ,
        //280 ,   282 ,   284 ,   286 ,   288 ,
        //290 ,   292 ,   294 ,   296 ,   298 ,
        //300 ,   302 ,   304 ,   306 ,   308 ,
        //310 ,   312 ,   314 ,   316 ,   318 ,
        //320 ,   322 ,   324 ,   326 ,   328 ,
        //330 ,   332 ,   334 ,   336 ,   338 ,
        //340 ,   342 ,   344 ,   346 ,   348 ,
        //350 ,   352 ,   354 ,   356 ,   358 ,
        //360 ,   362 ,   364 ,   366 ,   368 ,
        //370 ,   372 ,   374 ,   376 ,   378 ,
        //380 ,   382 ,   384 ,   386 ,   388 ,
        //390 ,   392 ,   394 ,   396 ,   398 ,
        //400 ,   402 ,   404 ,   406 ,   408 ,
        //410 ,   412 ,   414 ,   416 ,   418
        // };
        //listX1.AddRange(fx);
        //float[] fy = new float[] { 80.1F  ,   82  ,   84  ,   86  ,   88  ,
        //90  ,   92  ,   94  ,   96  ,   98  ,
        //100 ,   102 ,   104 ,   106 ,   108 ,
        //110 ,   112 ,   114 ,   116 ,   118 ,
        //120 ,   122 ,   124 ,   126 ,   128 ,
        //130 ,   132 ,   134 ,   136 ,   138 ,
        //140 ,   142 ,   144 ,   146 ,   148 ,
        //150 ,   152 ,   154 ,   156 ,   158 ,
        //160 ,   162 ,   164 ,   166 ,   168 ,
        //170 ,   172 ,   174 ,   176 ,   178 ,
        //180 ,   182 ,   184 ,   186 ,   188 ,
        //190 ,   192 ,   194 ,   196 ,   198 ,
        //200 ,   202 ,   204 ,   206 ,   208 ,
        //210 ,   212 ,   214 ,   216 ,   218 ,
        //220 ,   222 ,   224 ,   226 ,   228 ,
        //230 ,   232 ,   234 ,   236 ,   238 };
        //listY1.AddRange(fy);



        float[] fx = new float[] {335,336,337,338,339,340,341,342,343,344,345
         };
        listX1.AddRange(fx);
        float[] fy = new float[] { 80.1f, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93 };
        listY1.AddRange(fy);
        float[] fz = new float[] { 0.1F ,  1 ,  2 ,  3 ,  4
         };
        listZ1.AddRange(fz);

        //        float[] fx = new float[] {335,336,337,338,339,340,341,342,343,344,345,
        // 346,347,348,349,350,351,352,353,354,355,356,357,358,359,
        // 360,361,362,363,364,365,366,367,368,369,370,371,372,373,
        // 374,375,376,377,378,379
        //         };
        //        listX1.AddRange(fx);
        //        float[] fy = new float[] { 80.1f, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93
        //,94, 95, 96, 97, 98, 99,100,101,102,103,104,105,106,107,
        // 108,109,110,111,112,113,114};
        //        listY1.AddRange(fy);
        //        float[] fz = new float[] { 0.1F ,  1 ,  2 ,  3 ,  4,
        //        5 ,  6  , 7 ,  8  , 9,
        //        10,  11,  12  ,13, 14
        //         };
        //        listZ1.AddRange(fz);

        //第一种   XZY有待商榷
        //for (int i = 0; i < listX.Count; i++)
        //{
        //    for (int j = 0; j < listZ.Count; j++)
        //    {
        //        for (int k = 0; k < listY.Count; k++)
        //        {
        //            Vector3 vector3 = new Vector3(listX[i], listZ[j], listY[k]);
        //            list.Add(vector3);
        //        }
        //    }
        //}
        //第二种  ZXY  不合适
        //for (int i = 0; i < listZ.Count; i++)
        //{
        //    for (int j = 0; j < listX.Count; j++)
        //    {
        //        for (int k = 0; k < listY.Count; k++)
        //        {
        //            Vector3 vector3 = new Vector3(listZ[i], listX[j], listY[k]);
        //            list.Add(vector3);
        //        }
        //    }
        //}
        //第三种  XYZ  不合适
        //for (int i = 0; i < listX.Count; i++)
        //{
        //    for (int j = 0; j < listY.Count; j++)
        //    {
        //        for (int k = 0; k < listZ.Count; k++)
        //        {
        //            Vector3 vector3 = new Vector3(listX[i], listY[j], listZ[k]);
        //            list.Add(vector3);
        //        }
        //    }
        //}
        //第四种  YXZ 不合适
        //for (int i = 0; i < listX.Count; i++)
        //{
        //    for (int j = 0; j < listY.Count; j++)
        //    {
        //        for (int k = 0; k < listZ.Count; k++)
        //        {
        //            //Vector3 vector3 = new Vector3(listY[j]-88, listX[i]-218, listZ[k]-78f);
        //            //Vector3 vector3 = new Vector3(listY[j] - 180, listX[i] - 258, listZ[k] - 20f);
        //            Vector3 vector3 = new Vector3(listY[j], listX[i], listZ[k]);
        //            list.Add(vector3);
        //        }
        //    }
        //}
        //第五种  YZX   有待商榷
        for (int i = 0; i < listY1.Count; i++)
        {
            for (int j = 0; j < listZ1.Count; j++)
            {
                for (int k = 0; k < listX1.Count; k++)
                {
                    Vector3 vector3 = new Vector3(listY1[i], listZ1[j], listX1[k]);
                    list1.Add(vector3);
                }
            }
        }
        for (int i = 0; i < listY.Count; i++)
        {
            for (int j = 0; j < listZ.Count; j++)
            {
                for (int k = 0; k < listX.Count; k++)
                {
                    Vector3 vector3 = new Vector3(listY[i], listZ[j], listX[k]);
                    list.Add(vector3);
                }
            }
        }
        //第六种 ZYX  不合适
        //for (int i = 0; i < listZ.Count; i++)
        //{
        //    for (int j = 0; j < listY.Count; j++)
        //    {
        //        for (int k = 0; k < listX.Count; k++)
        //        {
        //            Vector3 vector3 = new Vector3(listZ[i], listY[j], listX[k]);
        //            list.Add(vector3);
        //        }
        //    }
        //}
        //interpolation();
        //Debug.Log(searchMin(listX, 396));
        //进行插值，先测试对不对，再改为加载之前进行插值
        //Debug.Log(Parse("(80.977,226.028,0.395)"));
        //loadMoleData();
        //interpolationNFML();
    }
    /// <summary>
    /// 找到有序数组第一个比目标数值大的数
    /// </summary>
    /// <param name="list"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public float searchMax(List<float> listsearch, float target)
    {
        int lf = 0, rt = listsearch.Count - 1;
        while (lf <= rt)
        {
            int mid = lf + (rt - lf) / 2;
            if (listsearch[mid] > target)
            {
                rt = mid - 1;
            }
            else
            {
                lf = mid + 1;
            }
        }
        return listsearch[lf];
    }

    /// <summary>
    /// 找到有序数组第一个比有序数组小的数
    /// </summary>
    /// <param name="list"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public float searchMin(List<float> listsearch, float target)
    {
        int lf = 0, rt = listsearch.Count - 1;
        while (lf <= rt)
        {
            int mid = lf + (rt - lf) / 2;
            if (listsearch[mid] < target)
            {
                lf = mid + 1;
            }
            else
            {
                rt = mid - 1;

            }
        }
        return listsearch[rt];
    }

    /// <summary>
    /// 将字符串转为vector
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Vector3 Parse(string str)
    {
        str = str.Replace("(", " ").Replace(")", " "); //将字符串中"("和")"替换为" "
        string[] s = str.Split(',');
        return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
    }

    /// <summary>
    /// 加载nfmole文件，进行flacs模拟
    /// </summary>
    public void loadNfmlFile()
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = "NFMOLE files (*.NFMOLE)\0*.NFMOLE";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = "c:";
        //pth.initialDir = Application.dataPath.Replace("/", "\\") + "\\Resources"; //默认路径
        pth.title = "打开flacs文件";
        pth.defExt = "dat";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (OpenFileDialog.GetOpenFileName(pth))
        {
            //Debug.Log(pth.file);
            //为了找到扩散源，需要在实际扩散之前，先对中间的一个时间片段进行插值
            //然后获取这个片段内的浓度最高值，进而获得其扩散源
            //目前暂时不考虑坐标数据及坐标数据的均匀化，此处较简单
            //第一步是找到总共多少个时间片段
            //第二步找到中间这个时间片段
            //第三步对这个时间片段进行插值
            //第四步对这个时间片段找到浓度最高值和扩散源，这两个数据可以用两个变量表示
            //第五步清空所有list
            //第六步结合选定的坐标位置，进行坐标转换，然后进行实际模拟，选定坐标位置可以使用变量表示
            pathFile = pth.file;
            loadMoleData(pth.file);

            interpolationNFML();
            render = 1;
        }
    }

    /// <summary>
    /// 选定一个泄漏位置
    /// 点击某个位置，产生图标，并且打印这个位置的坐标，并且把这个坐标更新到全局变量中
    /// </summary>
    public void selectFlacsSimulationLocation()
    {
        //设置isSelectFlacs为true,开始选择位置
        //isSelectFlacs = true;
    }

    public double interpolation(Vector3 Des)
    {
        /**目前有了均匀化网格数据 原始浓度数据
        需要得到插值后的浓度数据
        先对少量数据进行测试
        1.对于确定位置关系的数据进行插值测试
        2.对于不确定位置关系的数据进行插值测试
        3.对于所有数据进行测试
        首先是对确定位置关系的数据进行插值测试
        输入数据是八个原始点及其浓度值、一个待插值点坐标，要求输出待插值点坐标的数据

         */
        //
        /**
        Vector3 A = new Vector3(1,5,5);
        Vector3 B = new Vector3(5, 5, 5);
        Vector3 C = new Vector3(5, 1, 5);
        Vector3 D = new Vector3(1, 1, 5);
        Vector3 E = new Vector3(1, 5, 1);
        Vector3 F = new Vector3(5, 5, 1);
        Vector3 G = new Vector3(5, 1, 1);
        Vector3 H = new Vector3(1, 1, 1);
        */
        //Vector3 Des = new Vector3(2,2,2);

        /**
        接下来需要确定谁是ABCDEFGH，这些必须是固定的才能求abcdMN
        */
        //A比待插值点z值大且比其y值大且比其x值小
        Vector3 A = new Vector3(searchMin(listY, Des.x), searchMax(listZ, Des.y), searchMax(listX, Des.z));
        //B x大 y大 z大
        Vector3 B = new Vector3(searchMax(listY, Des.x), searchMax(listZ, Des.y), searchMax(listX, Des.z));
        //C x大 y小 z大                       Y                        Z                        X
        Vector3 C = new Vector3(searchMax(listY, Des.x), searchMin(listZ, Des.y), searchMax(listX, Des.z));
        //D x小 y小 z大                       Y                        Z                        X
        Vector3 D = new Vector3(searchMin(listY, Des.x), searchMin(listZ, Des.y), searchMax(listX, Des.z));
        //E x小 y大 z小                       Y                        Z                        X
        Vector3 E = new Vector3(searchMin(listY, Des.x), searchMax(listZ, Des.y), searchMin(listX, Des.z));
        //F x大 y大 z小                       Y                        Z                        X
        Vector3 F = new Vector3(searchMax(listY, Des.x), searchMax(listZ, Des.y), searchMin(listX, Des.z));
        //G x大 y小 z小                       Y                        Z                        X
        Vector3 G = new Vector3(searchMax(listY, Des.x), searchMin(listZ, Des.y), searchMin(listX, Des.z));
        //H x小 y小 z小                       Y                        Z                        X
        Vector3 H = new Vector3(searchMin(listY, Des.x), searchMin(listZ, Des.y), searchMin(listX, Des.z));

        //还需要将原始浓度listmole6与原始坐标list绑定
        double concentrationA = dictionary[A];
        double concentrationB = dictionary[B];
        double concentrationC = dictionary[C];
        double concentrationD = dictionary[D];
        double concentrationE = dictionary[E];
        double concentrationF = dictionary[F];
        double concentrationG = dictionary[G];
        double concentrationH = dictionary[H];

        //求abcdMN六个点的坐标
        //以M点为例，是O点xy坐标不变，z坐标与A坐标相等  
        //不过如果这样写，那么之后立方体代码，A点方向应该确定，也就是必须是上方靠左靠后那个，其他点类似
        Vector3 M = new Vector3(Des.x, Des.y, A.z);
        //Debug.Log(M);
        Vector3 N = new Vector3(Des.x, Des.y, E.z);
        //Debug.Log(N);
        //a点，是M点yz不变，x变为与A相似
        Vector3 a = new Vector3(A.x, M.y, M.z);
        //Debug.Log(a);
        //b点，是M点yz不变，x变为与B相似
        Vector3 b = new Vector3(B.x, M.y, M.z);
        //Debug.Log(b);
        //c点，是N点yz不变，x变为与F相似
        Vector3 c = new Vector3(F.x, N.y, N.z);
        //Debug.Log(c);
        //d点，是N点yz不变，x变为与E相似
        Vector3 d = new Vector3(E.x, N.y, N.z);
        //Debug.Log(d);

        //接下来根据坐标比例计算浓度，根据AD Aa 长度比例和AD浓度值计算a浓度值  此处可以单独再写个函数
        //计算a b c d
        double concentrationOfa = concentrationByRatey(A, D, a, concentrationA, concentrationD);
        //Debug.Log(concentrationOfa);
        double concentrationOfb = concentrationByRatey(B, C, b, concentrationB, concentrationC);
        //Debug.Log(concentrationOfb);
        double concentrationOfc = concentrationByRatey(F, G, c, concentrationF, concentrationG);
        //Debug.Log(concentrationOfc);
        double concentrationOfd = concentrationByRatey(E, H, d, concentrationE, concentrationH);
        //Debug.Log(concentrationOfd);
        //计算M N
        double concentrationOfM = concentrationByRatex(a, b, M, concentrationOfa, concentrationOfb);
        //Debug.Log(concentrationOfM);
        double concentrationOfN = concentrationByRatex(d, c, N, concentrationOfd, concentrationOfc);
        //计算目标点
        double concentrationDes = concentrationByRatez(M, N, Des, concentrationOfM, concentrationOfN);
        //Debug.Log(concentrationDes);
        return concentrationDes;
    }

    /// <summary>
    /// 根据长度比例计算浓度,采取在x轴上插值
    /// </summary>
    /// <returns></returns>
    public double concentrationByRatex(Vector3 A, Vector3 B, Vector3 a, double concentrationA, double concentrationB)
    {
        ////首先计算长度
        //double lengthOfAB = (A - B).magnitude;
        //double lengthOfAa = (A - a).magnitude;
        ////然后计算比例
        ////都是double，直接除就行
        ////比例乘以AB浓度差，得到Aa浓度差，即A-a的浓度差
        //double offsetAa = (concentrationA - concentrationB) * (lengthOfAB / lengthOfAa);
        ////得到a的浓度
        //return concentrationA - offsetAa;
        return concentrationA + ((concentrationB - concentrationA) / (B.x - A.x)) * (a.x - A.x);
    }

    /// <summary>
    /// 根据长度比例计算浓度,采取在y轴上插值
    /// </summary>
    /// <returns></returns>
    public double concentrationByRatey(Vector3 A, Vector3 B, Vector3 a, double concentrationA, double concentrationB)
    {
        ////首先计算长度
        //double lengthOfAB = (A - B).magnitude;
        //double lengthOfAa = (A - a).magnitude;
        ////然后计算比例
        ////都是double，直接除就行
        ////比例乘以AB浓度差，得到Aa浓度差，即A-a的浓度差
        //double offsetAa = (concentrationA - concentrationB) * (lengthOfAB / lengthOfAa);
        ////得到a的浓度
        //return concentrationA - offsetAa;
        return concentrationA + ((concentrationB - concentrationA) / (B.y - A.y)) * (a.y - A.y);
    }

    /// <summary>
    /// 根据长度比例计算浓度,采取在z轴上插值
    /// </summary>
    /// <returns></returns>
    public double concentrationByRatez(Vector3 A, Vector3 B, Vector3 a, double concentrationA, double concentrationB)
    {
        ////首先计算长度
        //double lengthOfAB = (A - B).magnitude;
        //double lengthOfAa = (A - a).magnitude;
        ////然后计算比例
        ////都是double，直接除就行
        ////比例乘以AB浓度差，得到Aa浓度差，即A-a的浓度差
        //double offsetAa = (concentrationA - concentrationB) * (lengthOfAB / lengthOfAa);
        ////得到a的浓度
        //return concentrationA - offsetAa;
        return concentrationA + ((concentrationB - concentrationA) / (B.z - A.z)) * (a.z - A.z);
    }

    private DateTime startTime;
    private bool isRender = false;
    private void Update()
    {
        //if (a == b)
        //{
        //    b++;
        //    loadData();
        //}
        if (render == 1)
        {
            currentTime = Time.time;
            if (currentTime - lastTime >= 5)
            {
                Debug.Log("渲染");
                loadData();
                lastTime = currentTime;
            }
        }
    }
    /// <summary>
    /// 跳转到flacsUI
    /// </summary>
    public void FlacsUi()
    {
        //关闭所有二级、三级菜单
        //ButtonHoverControl.closeAllTwoMenu();
        //ButtonTwoHoverControl.closeAllThreeMenu();
        FLACSLeackSpread.SetActive(true);
        //退出扩散训练UI
        LeakTraining.SetActive(false);
    }
    /// <summary>
    /// 停止模拟
    /// </summary>
    public void stopSimulation()
    {
        if (timer != null)
        {
            timer.Close();
        }
        a = 0;
        b = 1;
    }
    /// <summary>
    /// 暂停模拟
    /// 未想好如何实现
    /// </summary>
    public void pauseSimulation()
    {

    }
    /// <summary>
    /// 退出模拟
    /// </summary>
    public void exitSimulation()
    {
        stopSimulation();
        closeUI();
        //LeakTraining.SetActive(true);
    }
    /// <summary>
    /// 退出模拟时关闭UI
    /// </summary>
    public void closeUI()
    {
        FLACSLeackSpread.SetActive(false);
    }
    private void ServerStart(object sender, ElapsedEventArgs e)
    {
        //Debug.Log(DateTime.Now + "============定时器数据接收服务开启============");
    }

    /// <summary>
    /// 目的是把0-1的值转化为0-255
    /// 使用公式：gnew=255*((g-Gmin)/(Gmax-Gmin))
    /// </summary>
    private byte normalize(double g)
    {
        if (g > 0.3)
        {
            g = 0.3;
        }
        if (g < 0.01)
        {
            g = 0;
        }
        //return (byte)(255 * g);
        return (byte)(((90 - 3) * ((g - 0.01) / (0.3 - 0.01))) + 3);
    }



    /// <summary>
    /// 进行插值
    /// </summary>
    public void interpolationNFML()
    {
        DateTime start = DateTime.Now;
        Debug.Log("插值开始" + DateTime.Now.ToString());
        dictionary.Clear();
        listMoleinterp.Clear();
        //先将所有数据存入字典
        for (int i = 0; i < list.Count; i++)
        {
            dictionary.Add(list[i], listMole6[i]);
        }
        //对均匀网格每一个数据插值
        for (int i = 0; i < list1.Count; i++)
        {
            //listMole6[i] =  interpolation(list1[i]);
            listMoleinterp.Add(interpolation(list1[i]));
        }
        Debug.Log("插值完成" + DateTime.Now.ToString());
        DateTime end = DateTime.Now;
        Debug.Log(end - start);
    }

    public Color32 calculateColor(double mole)
    {
        Color32 color = new Color32(0, 0, 0, 0);
        if (mole == 0)
        {
            zeroPoint++;
            //color = new Color32(0, 0, 0, normalize(mole));
            //concentration = 0;
        }
        else if (0 < mole && mole < 0.01)
        {
            twoPoint++;
            color = new Color32(0, 0, 255, normalize(mole));
            //concentration = 3;
        }
        else if (0.01 <= mole && mole < 0.03)
        {
            threePoint++;
            //color = new Color32(0, 125, 125, normalize(mole));
            //color = new Color32(152, 251, 152, normalize(mole));
            color = new Color32(34, 139, 34, normalize(mole));
            //concentration = 7;
        }
        else if (0.03 <= mole && mole < 0.06)
        {
            fourPoint++;
            //color = new Color32(0, 255, 0, normalize(mole));
            //color = new Color32(50, 205, 50, normalize(mole));
            color = new Color32(30, 230, 30, normalize(mole));
            //concentration = 15;
        }
        else if (0.06 <= mole && mole < 0.1)
        {
            fivePoint++;
            // color = new Color32(125, 125, 0, normalize(mole));
            //color = new Color32(34, 139, 34, normalize(mole));
            color = new Color32(0, 255, 0, normalize(mole));
            //concentration = 25;
        }
        else if (0.1 <= mole && mole <= 0.5)
        {
            sixPoint++;
            color = new Color32(255, 0, 0, normalize(mole));
            //concentration = 128;
        }
        else if (mole > 0.5)
        {
            sevenPoint++;
            color = new Color32(255, 125, 255, normalize(mole));
            //concentration = 255;
        }
        return color;
    }

    public void loadData()
    {
        //system.Stop(true);
        //system.Clear(true);
        //在此处进行插值，需要原始坐标、新坐标、原始浓度数据
        Profiler.BeginSample("testForloadData");


        listMole6Copy.Clear();
        listMole6Copy.AddRange(listMoleinterp);
        //Debug.Log("开始渲染" + listMole6Copy.Count);
        Debug.Log("坐标数量" + list1.Count);
        listMole6Copy.Sort();
        //Debug.Log("最小值是：" + listMole6Copy[0]);
        //Debug.Log("第二个值是：" + listMole6Copy[1]);
        //Debug.Log("第三个值是：" + listMole6Copy[2]);
        Debug.Log(listMoleinterp[77] + "!!!!!!!!!!!!!");
        Debug.Log("最大值是：" + listMole6Copy[listMole6Copy.Count - 1]);
        //Debug.Log(listMole6Copy.Contains(9.54032020481622E-41) + "----" + normalize(2.2588931E-42) + "-----" + normalize(0.004));

        ////打印所有坐标数据，判断是不是保存成功
        //GameObject go = GameObject.Instantiate(Resources.Load("cube")) as GameObject;
        //go.transform.position = list1[1];

        //当前颜色
        //Color32 color = new Color32(0,0,0,0);
        Debug.Log(paticalSwitch + "???????????");
        for (int i = 0; i < list1.Count; i++)
        //for (int i = 0; i < 15; i++)
        {

            ////找到泄漏核心坐标，方法：找到所有浓度为1的坐标，找到其大致区域
            //if (listMoleinterp[i] == listMole6Copy[listMole6Copy.Count - 1])
            //{
            //    //Debug.Log("泄漏核心区域" + list1[i]);
            //    GameObject go1 = GameObject.Instantiate(Resources.Load("cube")) as GameObject;
            //    go1.transform.position = new Vector3(list1[i].x - 91, list1[i].y + 0.5f, list1[i].z - 436);
            //}


            //根据浓度计算透明度
            //if (listMole6[i] < 1e)
            //{

            //}
            //记录当前浓度值，后续根据不同浓度修改
            byte concentration = 0;
            //if (listMoleinterp[i] == 0)
            //{
            //    zeroPoint++;
            //    //color = new Color32(0, 0, 0, normalize(listMoleinterp[i]));
            //    concentration = 0;
            //}else if (0 < listMoleinterp[i] && listMoleinterp[i] < 0.01)
            //{
            //    twoPoint++;
            //   // color = new Color32(0, 0, 255, normalize(listMoleinterp[i]));
            //    concentration = 3;
            //}
            //else if (0.01 <= listMoleinterp[i] && listMoleinterp[i] < 0.03)
            //{
            //    threePoint++;
            //    //color = new Color32(0, 125, 125, normalize(listMoleinterp[i]));
            //    concentration = 7;
            //}
            //else if (0.03 <= listMoleinterp[i] && listMoleinterp[i] < 0.06)
            //{
            //    fourPoint++;
            //    color = new Color32(0, 255, 0, normalize(listMoleinterp[i]));
            //    concentration = 15;
            //}
            //else if (0.06 <= listMoleinterp[i] && listMoleinterp[i] < 0.1)
            //{
            //    fivePoint++;
            //   // color = new Color32(125, 125, 0, normalize(listMoleinterp[i]));
            //    concentration = 25;
            //}
            //else if (0.1 <= listMoleinterp[i] && listMoleinterp[i] <= 0.5)
            //{
            //    sixPoint++;
            //    color = new Color32(255, 0, 0, normalize(listMoleinterp[i]));
            //    concentration = 128;
            //}
            //else if (listMoleinterp[i] > 0.5)
            //{
            //    sevenPoint++;
            //    color = new Color32(255, 125, 255, normalize(listMoleinterp[i]));
            //    concentration = 255;
            //}

            //GameObject SmokeEffect = GameObject.Instantiate(Resources.Load("SmokeEffect")) as GameObject;
            //SmokeEffect.transform.position = list1[i];
            //Debug.Log(list[i]);
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {
                position = new Vector3(list1[i].x - 91, list1[i].y + 0.5f, list1[i].z - 436),
                //startSize = (float)(listMoleinterp[i] * Math.Pow(10, 2) * 3),
                //startSize = (float)listMoleinterp[i],
                //startColor = new Color32(255, 255, 255, 255),
                startColor = calculateColor(listMoleinterp[i]),
                //startColor = new Color32(0, 112, 255, normalize(listMoleinterp[i])),
                //startColor = new Color32(255, 255, 255, normalize(listMoleinterp[i])),
                //startColor = new Color32(0, 0, 0, normalize(listMoleinterp[i])),
                //startColor = new Color32(255, 255, 255, concentration),
                startSize = 6.5f,
                //还需要斟酌这个时间
                //startLifetime = 5.1f,
                startLifetime = 6000f,
                angularVelocity = 0f
            };
            emitParams.ResetVelocity();

            if (paticalSwitch == true)
            {
                //paticalSwitch = false;
                system.Emit(emitParams, 1);
                system.Play(); // Continue normal emissions
            }
            else
            {
                //paticalSwitch = true;
                system1.Emit(emitParams, 1);
                system1.Play(); // Continue normal emissions
            }

        }
        //Debug.Log("0---" + zeroPoint + "比例：" + Math.Round(((decimal)zeroPoint / listMoleinterp.Count), 4));
        //Debug.Log("0-0.01---" + twoPoint + "比例：" + Math.Round(((decimal)twoPoint / listMoleinterp.Count), 4));
        //Debug.Log("0.01-0.03---" + threePoint + "比例：" + Math.Round(((decimal)threePoint / listMoleinterp.Count), 4));
        //Debug.Log("0.03-0.06---" + fourPoint + "比例：" + Math.Round(((decimal)fourPoint / listMoleinterp.Count), 4));
        //Debug.Log("0.06-0.1---" + fivePoint + "比例：" + Math.Round(((decimal)fivePoint / listMoleinterp.Count), 4));
        //Debug.Log("0.1-0.5---" + sixPoint + "比例：" + Math.Round(((decimal)sixPoint / listMoleinterp.Count), 4));
        //Debug.Log("0.5-1---" + sevenPoint + "比例：" + Math.Round(((decimal)sevenPoint / listMoleinterp.Count), 4));
        if (paticalSwitch == true)
        {
            paticalSwitch = false;
        }
        else
        {
            paticalSwitch = true;
        }
        //loadMoleData(pathFile);
        //interpolationNFML();
        //Debug.Log(DateTime.Now.TimeOfDay.ToString());
        Profiler.EndSample();
    }
    public void getCenter() { }
    /// <summary>
    /// 删除系统中所有的粒子系统
    /// </summary>
    public void clearPartical()
    {
        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
    /// <summary>
    /// 每读取一次五秒钟数据，声明一个变量a，默认为0,读取一次，a++
    /// 然后在update里有一个变量b默认为1，如果a==b，那么调用一次渲染，同时b++
    /// </summary>
    public void loadMoleData6()
    {
        render = 1;
        //startTime = DateTime.Now;
        //isRender = true;

        //timer = new System.Timers.Timer();  //五秒访问

        //timer.Elapsed += new ElapsedEventHandler(loadMoleData);
        ////timer.Elapsed += new ElapsedEventHandler(test);
        //timer.Start();
        //timer.Interval = 5000;

        //timer.AutoReset = true;
        //timer.Enabled = true;
    }
    public void test(object source, System.Timers.ElapsedEventArgs e)
    {
        //Debug.Log("测试");
    }

    private void OnDestroy()
    {
        if (timer != null)
        {
            timer.Close();
        }

    }

    /// <summary>
    /// 静态加载数据
    /// </summary>
    public void loadMoleDataStatic()
    {
        //Debug.Log("当前时间：" + DateTime.Now.ToString());
        //a3730100  - 副本200秒.NFMOLE  data_39.txt  data39.txt  result.txt
        StreamReader sr = new StreamReader("D:\\a3730100  - 副本200秒.NFMOLE", Encoding.Default);
        string line;
        //Debug.Log("开始读取");
        //清空浓度list
        listMole6.Clear();
        listMole6Copy.Clear();
        //时间段递增
        //times++;
        //声明一个变量，用来达到当前变量
        //int curTimes = 0;
        while ((line = sr.ReadLine()) != null)
        {
            //if (curTimes < times)
            //{
            //    if (line.Contains("N"))
            //    {
            //        curTimes++;
            //    }
            //    else
            //    {
            //    }
            //}
            //else
            //{
            //if (line.Contains("N"))
            //{
            //    curTimes++;
            //}
            //else
            //{
            //需要限定只能在数字里进行
            //line.Length<=15是为了避开空行和时间行
            if (line.Length <= 15
                || line.Contains("G") || line.Contains("d") || line.Contains("r"))
            {
                if (line.Contains("TIME"))
                {
                    //Debug.Log("当前时间" + line);
                }
            }
            else
            {
                string[] data = line.Split(' ');
                //如果遇到NFMOLE那么curTimes++,如果curTimes>times,那么就结束当前方法
                //if (line.Contains("N"))
                //{
                //    curTimes++;
                //}
                //else
                //{
                //代码到此处，说明已经超过所需5秒数据到了下一次5秒数据
                //if (curTimes > times)
                //{
                //    Debug.Log(list.Count);
                //    Debug.Log(listMole6.Count + "listMole6.Count");
                //    Debug.Log("a===" + a);
                //    Debug.Log("b===" + b);
                //    a++;
                //    return;
                //}
                //如果还在当前5秒数据，则加入listMole6
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Trim() != string.Empty && data[i].Trim().Contains("."))
                    //if (data[i].Trim() != string.Empty)
                    {
                        //Debug.Log(data[i].Trim());
                        listMole6.Add(float.Parse(data[i].Trim()));
                    }
                }
                //}
            }
            //}
            //}
        }
        sr.Close();
        interpolationNFML();
        loadData();
    }

    /// <summary>
    /// 动态加载数据  object source, System.Timers.ElapsedEventArgs e
    /// </summary>
    public void loadMoleData(String path)
    {
        //a3730100 .NFMOLE  a3730100  - 副本.NFMOLE
        StreamReader sr = new StreamReader(path, Encoding.Default);
        string line;
        //Debug.Log("开始读取");
        //清空浓度list
        listMole6.Clear();
        //时间段递增
        times++;
        //声明一个变量，用来达到当前变量
        int curTimes = 0;
        while ((line = sr.ReadLine()) != null)
        {
            if (curTimes < times)
            {
                if (line.Contains("N"))
                {
                    curTimes++;
                }
                else
                {
                }
            }
            else
            {
                if (line.Contains("N"))
                {
                    curTimes++;
                }
                else
                {
                    //需要限定只能在数字里进行
                    //line.Length<=15是为了避开空行和时间行
                    if (line.Length <= 15
                        || line.Contains("G") || line.Contains("d") || line.Contains("r"))
                    {
                        if (line.Length <= 15 && !line.Contains("G") && !line.Contains("d") && !line.Contains("r") && !line.Contains("TIME"))
                        {
                            Debug.Log("当前时间" + line);
                        }
                    }
                    else
                    {
                        string[] data = line.Split(' ');
                        //如果遇到NFMOLE那么curTimes++,如果curTimes>times,那么就结束当前方法
                        if (line.Contains("N"))
                        {
                            curTimes++;
                        }
                        else
                        {
                            //代码到此处，说明已经超过所需5秒数据到了下一次5秒数据
                            if (curTimes > times)
                            {
                                //Debug.Log(list.Count);
                                //Debug.Log(listMole6.Count + "listMole6.Count");
                                //Debug.Log("a===" + a);
                                //Debug.Log("b===" + b);
                                Debug.Log("渲染第" + a * 5 + "秒数据");
                                a++;
                                return;
                            }
                            //如果还在当前5秒数据，则加入listMole6
                            for (int i = 0; i < data.Length; i++)
                            {
                                if (data[i].Trim() != string.Empty && data[i].Trim().Contains("."))
                                {
                                    //Debug.Log(data[i].Trim());
                                    listMole6.Add(float.Parse(data[i].Trim()));
                                }
                            }
                        }
                    }
                }
            }
        }
        sr.Close();

    }
}
