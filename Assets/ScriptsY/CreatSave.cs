using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Globalization;
/// <summary>
/// 实现场景保存功能
/// 要保存的内容包括场景内每个物体的名字、位置、角度、大小
/// </summary>
public class CreatSave : MonoBehaviour
{
    //生成新模型
    private GameObject pre;
    /// <summary>
    /// 液化气装卸站台
    /// </summary>
    public GameObject Station;
    /// <summary>
    /// 液化气装卸站台管线
    /// </summary>
    public GameObject Pipe;
    //用于读取每一个物体
    private GameObject[] obj; //开头定义GameObject数组
    /// <summary>
    /// String转化为Float
    /// </summary>
    /// <param name="FloatString"></param>
    /// <returns></returns>
    public float StrToFloat(object FloatString)
    {
        float result;
        if (FloatString != null)
        {
            if (float.TryParse(FloatString.ToString(), out result))
                return result;
            else
            {
                return (float)0.00;
            }
        }
        else
        {
            return (float)0.00;
        }
    }
    /// <summary>
    /// 将场景内所有物体保存到场景对象save内
    /// </summary>
    /// <returns>保存了所有信息的save对象</returns>
    public Save CreateSaveObjects()
    {
        //读取所有物体，此处主要是为了读取用户生成的Clone的物体
        obj = FindObjectsOfType(typeof(GameObject)) as GameObject[]; //关键代码，获取所有gameobject元素给数组obj
        //新建save对象
        Save save = new Save();
        //接下来跟数据读取范围一致，均是站台、管线、Clone物体要记录
        //保存每一个物体的名字、位置、大小、角度保存进save对象
        foreach (Transform child in Station.transform)    //遍历所有gameobject
        {
            //将物体坐标x方向取到最大精度
            //string PositionX = child.gameObject.transform.position.x.ToString("F5");
            //string PositionY = child.gameObject.transform.position.y.ToString("F5");
            //string PositionZ = child.gameObject.transform.position.z.ToString("F5");
            //string RotationW = child.gameObject.transform.rotation.w.ToString("F5");
            //string RotationX = child.gameObject.transform.rotation.x.ToString("F5");
            //string RotationY = child.gameObject.transform.rotation.y.ToString("F5");
            //string RotationZ = child.gameObject.transform.rotation.z.ToString("F5");
            //child.gameObject.transform.position = new Vector3(float.Parse(PositionX, CultureInfo.InvariantCulture.NumberFormat), float.Parse(PositionY, CultureInfo.InvariantCulture.NumberFormat), float.Parse(PositionZ, CultureInfo.InvariantCulture.NumberFormat));
            //child.gameObject.transform.rotation = new Quaternion(float.Parse(RotationX, CultureInfo.InvariantCulture.NumberFormat), float.Parse(RotationY, CultureInfo.InvariantCulture.NumberFormat), float.Parse(RotationZ, CultureInfo.InvariantCulture.NumberFormat), float.Parse(RotationW, CultureInfo.InvariantCulture.NumberFormat));
            save.gameobjectNames.Add(child.gameObject.name);//名字
            //save.gameobjectPositions.Add(child.gameObject.transform.position);//名字
            //save.gameobjectRotations.Add(child.gameObject.transform.rotation);//角度
            //save.gameobjectScales.Add(child.gameObject.transform.localScale);//大小
            save.gameobjectPositionX.Add(child.position.x);
            save.gameobjectPositionY.Add(child.position.y);
            save.gameobjectPositionZ.Add(child.position.z);
            save.gameobjectRotationX.Add(child.rotation.x);
            save.gameobjectRotationY.Add(child.rotation.y);
            save.gameobjectRotationZ.Add(child.rotation.z);
            save.gameobjectRotationW.Add(child.rotation.w);
            save.gameobjectScaleX.Add(child.localScale.x);
            save.gameobjectScaleY.Add(child.localScale.y);
            save.gameobjectScaleZ.Add(child.localScale.z);
        }
        foreach (Transform child in Pipe.transform)    //遍历所有gameobject
        {
            //save.gameobjectNames.Add(child.gameObject.name);//名字
            //save.gameobjectPositions.Add(child.gameObject.transform.position);//名字
            //save.gameobjectRotations.Add(child.gameObject.transform.rotation);//角度
            //save.gameobjectScales.Add(child.gameObject.transform.localScale);//大小
            save.gameobjectNames.Add(child.gameObject.name);//名字
            save.gameobjectPositionX.Add(child.position.x);
            save.gameobjectPositionY.Add(child.position.y);
            save.gameobjectPositionZ.Add(child.position.z);
            save.gameobjectRotationX.Add(child.rotation.x);
            save.gameobjectRotationY.Add(child.rotation.y);
            save.gameobjectRotationZ.Add(child.rotation.z);
            save.gameobjectRotationW.Add(child.rotation.w);
            save.gameobjectScaleX.Add(child.localScale.x);
            save.gameobjectScaleY.Add(child.localScale.y);
            save.gameobjectScaleZ.Add(child.localScale.z);
        }
        foreach (GameObject child in obj)    //遍历所有gameobject
        {
            if (child.name.Contains("Clone"))
            {
                //save.gameobjectNames.Add(child.gameObject.name);//名字
                //save.gameobjectPositions.Add(child.gameObject.transform.position);//名字
                //save.gameobjectRotations.Add(child.gameObject.transform.rotation);//角度
                //save.gameobjectScales.Add(child.gameObject.transform.localScale);//大小
                save.gameobjectNames.Add(child.gameObject.name);//名字
                save.gameobjectPositionX.Add(child.gameObject.transform.position.x);
                save.gameobjectPositionY.Add(child.gameObject.transform.position.y);
                save.gameobjectPositionZ.Add(child.gameObject.transform.position.z);
                save.gameobjectRotationX.Add(child.gameObject.transform.rotation.x);
                save.gameobjectRotationY.Add(child.gameObject.transform.rotation.y);
                save.gameobjectRotationZ.Add(child.gameObject.transform.rotation.z);
                save.gameobjectRotationW.Add(child.gameObject.transform.rotation.w);
                save.gameobjectScaleX.Add(child.gameObject.transform.localScale.x);
                save.gameobjectScaleY.Add(child.gameObject.transform.localScale.y);
                save.gameobjectScaleZ.Add(child.gameObject.transform.localScale.z);
            }
        }
        //返回save对象
        return save;
    }
    /// <summary>
    /// 将场景内的信息保存进xml
    /// </summary>
    public void SaveByXml(string filePath)
    {
        //保存了所有内容的save对象
        Save save = CreateSaveObjects();
        //创建XML文件的存储路径
        //string filePath = Application.dataPath + "/StreamingFile" + "/byXML.xml";
        //创建XML文档
        XmlDocument xmlDoc = new XmlDocument();
        //创建根节点，即最上层节点
        XmlElement root = xmlDoc.CreateElement("save");
        //设置根节点中的值
        root.SetAttribute("name", "saveFile1");
        //创建XmlElement
        XmlElement objectSaved;
        XmlElement objectName;
        XmlElement objectPositionX;
        XmlElement objectPositionY;
        XmlElement objectPositionZ;
        XmlElement objectRotationX;
        XmlElement objectRotationY;
        XmlElement objectRotationZ;
        XmlElement objectRotationW;
        XmlElement objectScalesX;
        XmlElement objectScalesY;
        XmlElement objectScalesZ;
        //遍历save中存储的数据，将数据转换成XML格式
        for (int i = 0; i < save.gameobjectNames.Count; i++)
        {
            objectSaved = xmlDoc.CreateElement("objectSaved");
            objectName = xmlDoc.CreateElement("objectName");
            objectName.InnerText = save.gameobjectNames[i].ToString();
            objectPositionX = xmlDoc.CreateElement("objectPositionX");
            objectPositionX.InnerText = save.gameobjectPositionX[i].ToString();
            objectPositionY = xmlDoc.CreateElement("objectPositionY");
            objectPositionY.InnerText = save.gameobjectPositionY[i].ToString();
            objectPositionZ = xmlDoc.CreateElement("objectPositionZ");
            objectPositionZ.InnerText = save.gameobjectPositionZ[i].ToString();
            objectRotationX = xmlDoc.CreateElement("objectRotationX");
            objectRotationX.InnerText = save.gameobjectRotationX[i].ToString();
            objectRotationY = xmlDoc.CreateElement("objectRotationY");
            objectRotationY.InnerText = save.gameobjectRotationY[i].ToString();
            objectRotationZ = xmlDoc.CreateElement("objectRotationZ");
            objectRotationZ.InnerText = save.gameobjectRotationZ[i].ToString();
            objectRotationW = xmlDoc.CreateElement("objectRotationW");
            objectRotationW.InnerText = save.gameobjectRotationW[i].ToString();
            objectScalesX = xmlDoc.CreateElement("objectScalesX");
            objectScalesX.InnerText = save.gameobjectScaleX[i].ToString();
            objectScalesY = xmlDoc.CreateElement("objectScalesY");
            objectScalesY.InnerText = save.gameobjectScaleY[i].ToString();
            objectScalesZ = xmlDoc.CreateElement("objectScalesZ");
            objectScalesZ.InnerText = save.gameobjectScaleZ[i].ToString();
            //objectRotation = xmlDoc.CreateElement("objectRotation");
            //objectRotation.InnerText = save.gameobjectRotations[i].ToString();
            //objectScales = xmlDoc.CreateElement("objectScales");
            //objectScales.InnerText = save.gameobjectScales[i].ToString();
            //设置节点间的层级关系 root -- objectName -- (objectPosition, objectRotation)
            objectSaved.AppendChild(objectName);
            objectSaved.AppendChild(objectPositionX);
            objectSaved.AppendChild(objectPositionY);
            objectSaved.AppendChild(objectPositionZ);
            objectSaved.AppendChild(objectRotationX);
            objectSaved.AppendChild(objectRotationY);
            objectSaved.AppendChild(objectRotationZ);
            objectSaved.AppendChild(objectRotationW);
            objectSaved.AppendChild(objectScalesX);
            objectSaved.AppendChild(objectScalesY);
            objectSaved.AppendChild(objectScalesZ);
            root.AppendChild(objectSaved);
        }
        xmlDoc.AppendChild(root);
        xmlDoc.Save(filePath);
        if (File.Exists(Application.dataPath + "/StreamingFile" + "/byXML.xml"))
        {
            Debug.Log("保存成功");
        }
    }
    //太棒了！！！！自定义文件夹存档
    public void SaveAs()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        SaveFileDlg pth = new SaveFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath.Replace("/", "\\") + "\\Resources"; //默认路径
        pth.title = "保存存档";
        pth.defExt = "xml";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (SaveFileDialog.GetSaveFileName(pth))
        {
            SaveByXml(pth.file);
        }
    }
    /// <summary>
    /// 字符串转vector3
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
    /// 字符串转Quaternion
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Quaternion QuaternionParse(string name)
    {
        name = name.Replace("(", "").Replace(")", "");
        string[] s = name.Split(',');
        return new Quaternion(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
    }
    /// <summary>
    /// 太棒啦！！！！自定义读取
    /// </summary>
    public void Load()
    {
        //关闭所有二级、三级菜单
        ButtonHoverControl.closeAllTwoMenu();
        ButtonTwoHoverControl.closeAllThreeMenu();
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = "All files (*.*)|*.*";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = "c:";
        //pth.initialDir = Application.dataPath.Replace("/", "\\") + "\\Resources"; //默认路径
        pth.title = "打开项目";
        pth.defExt = "dat";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (OpenFileDialog.GetOpenFileName(pth))
        {
            LoadByXml(pth.file);
        }
    }
    /// <summary>
    /// 加载xml
    /// </summary>
    public void LoadByXml(string filePath)
    {
        //string filePath = Application.dataPath + "/StreamingFile" + "/byXML.xml";
        if (File.Exists(filePath))
        {
            Save save = new Save();
            //加载XML文档
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            //通过节点名称来获取元素，结果为XmlNodeList类型
            XmlNodeList targets = xmlDoc.GetElementsByTagName("objectSaved");
            //遍历所有的target节点，并获得子节点和子节点的InnerText
            if (targets.Count != 0)
            {
                foreach (XmlNode target in targets)
                {
                    XmlNode objectNameXml = target.ChildNodes[0];
                    string objectName = objectNameXml.InnerText;
                    //把得到的值存储到save中
                    save.gameobjectNames.Add(objectName);
                    XmlNode objectPositionX = target.ChildNodes[1];
                    save.gameobjectPositionX.Add(float.Parse(objectPositionX.InnerText));
                    XmlNode objectPositionY = target.ChildNodes[2];
                    save.gameobjectPositionY.Add(float.Parse(objectPositionY.InnerText));
                    XmlNode objectPositionZ = target.ChildNodes[3];
                    save.gameobjectPositionZ.Add(float.Parse(objectPositionZ.InnerText));
                    XmlNode objectRotationX = target.ChildNodes[4];
                    save.gameobjectRotationX.Add(float.Parse(objectRotationX.InnerText));
                    XmlNode objectRotationY = target.ChildNodes[5];
                    save.gameobjectRotationY.Add(float.Parse(objectRotationY.InnerText));
                    XmlNode objectRotationZ = target.ChildNodes[6];
                    save.gameobjectRotationZ.Add(float.Parse(objectRotationZ.InnerText));
                    XmlNode objectRotationW = target.ChildNodes[7];
                    save.gameobjectRotationW.Add(float.Parse(objectRotationW.InnerText));
                    XmlNode objectScalesX = target.ChildNodes[8];
                    save.gameobjectScaleX.Add(float.Parse(objectScalesX.InnerText));
                    XmlNode objectScalesY = target.ChildNodes[9];
                    save.gameobjectScaleY.Add(float.Parse(objectScalesY.InnerText));
                    XmlNode objectScalesZ = target.ChildNodes[10];
                    save.gameobjectScaleZ.Add(float.Parse(objectScalesZ.InnerText));
                    ////Vector3 objectPosition = Parse(objectPositionXml.InnerText);
                    //save.gameobjectPositionX.Add(float.Parse(objectPositionXml.InnerText)); 
                    ////把得到的值存储到save中
                    ////save.gameobjectPositions.Add(objectPosition);
                    //XmlNode objectRotationXml = target.ChildNodes[2];
                    ////Quaternion objectRotation = QuaternionParse(objectRotationXml.InnerText);
                    ////把得到的值存储到save中
                    ////save.gameobjectRotations.Add(objectRotation);
                    //XmlNode objectScalesXml = target.ChildNodes[3];
                    ////Vector3 objectScales = Parse(objectScalesXml.InnerText);
                    ////把得到的值存储到save中
                    ////save.gameobjectScales.Add(objectScales);
                }
            }
            //Debug.Log("成功");
            SetGame(save);
        }
        else
        {
            Debug.Log("存档文件不存在");
        }
    }
    private void SetGame(Save save)
    {
        GameObject system = GameObject.Find("液化气装卸站台");
        List<Transform> lst = new List<Transform>();
        foreach (Transform child in system.transform)
        {
            lst.Add(child);
        }
        foreach (Transform child in lst)    //遍历所有gameobject
        {
            int index = save.gameobjectNames.IndexOf(child.gameObject.name);//会不会出现重名的情况
                                                                            //Debug.Log(child.gameObject.name + "/t" + index);
                                                                            //Vector3 position = save.gameobjectPositions[index];
            float positionX = save.gameobjectPositionX[index];
            Vector3 position = new Vector3(positionX, save.gameobjectPositionY[index], save.gameobjectPositionZ[index]);
            Quaternion quaternion = new Quaternion(save.gameobjectRotationX[index], save.gameobjectRotationY[index], save.gameobjectRotationZ[index], save.gameobjectRotationW[index]);
            Vector3 scale = new Vector3(save.gameobjectScaleX[index], save.gameobjectScaleY[index], save.gameobjectScaleZ[index]);
            child.gameObject.transform.position = position;
            child.gameObject.transform.rotation = quaternion;
            child.gameObject.transform.localScale = scale;
        }
        GameObject pipe = GameObject.Find("液化气装卸站台管线");
        List<Transform> lst1 = new List<Transform>();
        foreach (Transform child in pipe.transform)
        {
            lst1.Add(child);
        }
        foreach (Transform child in lst1)    //遍历所有gameobject
        {
            int index = save.gameobjectNames.IndexOf(child.gameObject.name);//会不会出现重名的情况
            //Vector3 position = save.gameobjectPositions[index];
            //Quaternion quaternion = save.gameobjectRotations[index];
            //Vector3 scale = save.gameobjectScales[index];
            //child.gameObject.transform.position = position;
            //child.gameObject.transform.rotation = quaternion;
            //child.gameObject.transform.localScale = scale;
            float positionX = save.gameobjectPositionX[index];
            Vector3 position = new Vector3(positionX, save.gameobjectPositionY[index], save.gameobjectPositionZ[index]);
            Quaternion quaternion = new Quaternion(save.gameobjectRotationX[index], save.gameobjectRotationY[index], save.gameobjectRotationZ[index], save.gameobjectRotationW[index]);
            Vector3 scale = new Vector3(save.gameobjectScaleX[index], save.gameobjectScaleY[index], save.gameobjectScaleZ[index]);
            child.gameObject.transform.position = position;
            child.gameObject.transform.rotation = quaternion;
            child.gameObject.transform.localScale = scale;
        }
        foreach (string name in save.gameobjectNames)
        {
            if (name.Contains("Clone"))
            {  //AFQDYHQXF01(Clone)
                //Debug.Log(name);
                string[] nameString = name.Split('(');
                int index = save.gameobjectNames.IndexOf(name);//会不会出现重名的情况
                //Vector3 position = save.gameobjectPositions[index];
                //Quaternion quaternion = save.gameobjectRotations[index];
                //Vector3 scale = save.gameobjectScales[index];
                //child.gameObject.transform.position = position;
                //child.gameObject.transform.rotation = quaternion;
                //child.gameObject.transform.localScale = scale;
                //shengcheng(nameString[0], position, quaternion, scale);
                float positionX = save.gameobjectPositionX[index];
                Vector3 position = new Vector3(positionX, save.gameobjectPositionY[index], save.gameobjectPositionZ[index]);
                Quaternion quaternion = new Quaternion(save.gameobjectRotationX[index], save.gameobjectRotationY[index], save.gameobjectRotationZ[index], save.gameobjectRotationW[index]);
                Vector3 scale = new Vector3(save.gameobjectScaleX[index], save.gameobjectScaleY[index], save.gameobjectScaleZ[index]);
                //child.gameObject.transform.position = position;
                //child.gameObject.transform.rotation = quaternion;
                //child.gameObject.transform.localScale = scale;
                shengcheng(nameString[0], position, quaternion, scale);
            }
        }
    }
    public void shengcheng(string name, Vector3 position, Quaternion rotation, Vector3 scale) //生成模型
    {
        ////判断文件夹类型
        //if (type == "消防设备")
        //{
        //    Folder = "XiaoFangSheBei";
        //}
        //else if (type == "管线")
        //{
        //    Folder = "GuanXian";
        //}
        //else if (type == "汽车")
        //{
        //    Folder = "QiChe";
        //}
        //else if (type == "阀门")
        //{
        //    Folder = "FaMen";
        //}
        string fullPath = "/Resources/folder/model/";
        string fullNameFbx = name + ".fbx";
        string fullNameFbxLarge = name + ".FBX";
        string fullNamePrefab = name + ".prefab";
        string fullNamePrefabLarge = name + ".PREFAB";
        //GameObject.Find("Canvas/Text").GetComponent<Text>().text = "2222";
        DirectoryInfo direction = new DirectoryInfo(Application.dataPath + fullPath);
        Debug.Log(Application.dataPath + fullPath);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        Debug.Log(files.Length + "model文件夹数量");
        //string isExist;
        for (int i = 0; i < files.Length; i++)
        {
            //Debug.Log("正在查找" + fullName);
            if (files[i].Name.Equals(fullNameFbx) || files[i].Name.Equals(fullNamePrefab)
                || files[i].Name.Equals(fullNameFbxLarge) || files[i].Name.Equals(fullNamePrefabLarge))
            {
                //files[i] = new GameObject(files[i].Name) as gameObject;
                //Debug.Log(fullName + "存在");
                //pre1 = gameObject.transform.Find(x).gameObject; //将找到的这个物体赋值给pre1
                //Debug.Log(files[i].FullName);
                //pre1 = GameObject.Find(fullName);
                Debug.Log("成");
                if (files[i].Name.Equals(fullNameFbx) || files[i].Name.Equals(fullNameFbxLarge))
                {
                    //GameObject.Find("Canvas/Text").GetComponent<Text>().text = "ok";
                    ;
                    //GameObject cube = Instantiate(cubePresent);
                    ////GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cube.transform.position = new Vector3(38.9f, 1.5f, -72);
                    ////cube.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,0.5f);
                    //cube.GetComponent<BoxCollider>().size = new Vector3(10,10,10);
                    GameObject image = Resources.Load("folder/model/" + name + ".fbx") as GameObject;
                    pre = Instantiate(image, position, rotation);
                    pre.transform.localScale = scale;
                    //if (pre != null)
                    //{
                    //    GameObject.Find("Canvas/Text").GetComponent<Text>().text = "不是null";
                    //}
                    //else
                    //{
                    //    GameObject.Find("Canvas/Text").GetComponent<Text>().text = "null";
                    //}
                    //pre.transform.parent = cube.transform;
                    pre.transform.position = new Vector3(38.9f, 1.5f, -72);
                    pre.AddComponent<Rotate>();
                    pre.AddComponent<MeshCollider>();
                    //pre.AddComponent<BoxCollider>();
                    break;
                }
                else
                {
                    GameObject prefabGenerate = (GameObject)Resources.Load("folder/model/" + name);
                    pre = Instantiate(prefabGenerate);
                    pre.transform.position = position;
                    pre.transform.rotation = rotation;
                    pre.transform.localScale = scale;
                    pre.SetActive(true);
                    pre.AddComponent<Rotate>();
                    pre.AddComponent<MeshCollider>();
                }
            }
        }
    }
}
