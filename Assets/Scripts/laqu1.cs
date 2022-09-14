using System.Collections.Generic;
using System.IO;
//using TriLib;
//using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class laqu1 : MonoBehaviour
{
    public Dropdown dropdown;
    public Dropdown dropdownMain;
    public Text text;
    private GameObject pre;//要生成的物体
    public GameObject pre1;
    public GameObject maincamera;
    private Vector3 weizhi;
    public GameObject cubePresent;

    //存储所有生成的模型
    public static List<GameObject> generateObjects;

    public void shengcheng() //生成模型
    {
        //GameObject.Find("Canvas/Text").GetComponent<Text>().text = "在生成";
        //GameObject prefabGenerate = (GameObject)Resources.Load("灭火箱");
        //GameObject pre = Instantiate(prefabGenerate);
        //pre.transform.position = new Vector3(38.9f, 1.5f, -72);
        //pre.SetActive(true);
        //pre.AddComponent<Rotate>();
        //pre.AddComponent<MeshCollider>();
        maincamera.GetComponent<MouseHighlight>().enabled = true;
        string x;
        maincamera.transform.position = new Vector3(40, 6.09f, -84.39f);//调节摄像机位置
        x = dropdown.options[dropdown.value].text;//获取二级菜单中选中的名字 （即想要生成的模型的名字）
        string[] xString = x.Split(' ');
        string DeviceId = xString[0];

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
        string fullNameFbx = DeviceId + ".fbx";
        string fullNameFbxLarge = DeviceId + ".FBX";
        string fullNamePrefab = DeviceId + ".prefab";
        string fullNamePrefabLarge = DeviceId + ".PREFAB";


        //GameObject.Find("Canvas/Text").GetComponent<Text>().text = "2222";
        DirectoryInfo direction = new DirectoryInfo(Application.dataPath + fullPath);
        //Debug.Log(Application.dataPath + fullPath);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        //Debug.Log(files.Length + "model文件夹数量");
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

                if (files[i].Name.Equals(fullNameFbx) || files[i].Name.Equals(fullNameFbxLarge))
                {
                    //下面是fbx格式的
                    //GameObject.Find("Canvas/Text").GetComponent<Text>().text = "ok";
                    //GameObject cube = Instantiate(cubePresent);
                    ////GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cube.transform.position = new Vector3(38.9f, 1.5f, -72);
                    ////cube.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,0.5f);
                    //cube.GetComponent<BoxCollider>().size = new Vector3(10,10,10);
                    GameObject image = Resources.Load("folder/model/" + DeviceId + ".FBX") as GameObject;
                    pre = Instantiate(image, new Vector3(38.9f, 1.5f, -72), Quaternion.identity);
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
                    Debug.Log(pre.name);
                    pre.AddComponent<Rotate>();
                    pre.AddComponent<MeshCollider>();
                    pre.SetActive(true);
                    generateObjects.Add(pre);
                    //pre.AddComponent<BoxCollider>();
                    break;
                }
                else
                {
                    //下面是prefab格式的
                    GameObject prefabGenerate = (GameObject)Resources.Load("folder/model/" + DeviceId);
                    pre = Instantiate(prefabGenerate);
                    //Debug.Log(pre.name);
                    pre.transform.position = new Vector3(38.9f, 1.5f, -72);
                    pre.SetActive(true);
                    pre.AddComponent<MeshCollider>();
                    pre.AddComponent<SpectrumController>();//这个脚本的作用是高亮
                    pre.GetComponent<SpectrumController>().enabled = false;//使这个脚本暂时不启用
                    //pre.AddComponent<MouseHighlight>();//这个脚本的作用是点击高亮
                    pre.AddComponent<Rotate>();//这个脚本的作用是移动模型
                    generateObjects.Add(pre);
                    //foreach (GameObject item in generateObjects)
                    //{
                    //    Debug.Log(item.name + "---laqu1");
                    //}
                }
            }
        }

    }
    //else
    //{
    //GameObject.Find("Canvas/Text").GetComponent<Text>().text = "没找到";

    //}

    ////克隆pre1 生成新模型
    //if (pre1 != null)
    //{
    //    Debug.Log("pre1存在");
    //    pre = GameObject.Instantiate(pre1, new Vector3(38.9f, 1.5f, -72), pre1.transform.rotation) as GameObject;
    //    pre.SetActive(true);
    //    pre.AddComponent<Rotate>();
    //    pre.AddComponent<MeshCollider>();
    //}



    //private AssetLoaderOptions GetAssetLoaderOptions()
    //{
    //    AssetLoaderOptions assetLoaderOptions = AssetLoaderOptions.CreateInstance();
    //    assetLoaderOptions.DontLoadCameras = false;
    //    assetLoaderOptions.DontLoadLights = false;
    //    assetLoaderOptions.UseCutoutMaterials = true;
    //    assetLoaderOptions.AddAssetUnloader = true;
    //    return assetLoaderOptions;
    //}

    private void Start()
    {
        weizhi = maincamera.transform.position;
        generateObjects = new List<GameObject>();//保存所有此次系统运行生成的物体
    }

    // Update is called once per frame
    private void Update()
    {
        //foreach (GameObject obj in generateObjects)
        //{
        //    if (obj.transform.position==new Vector3(0,0,-72))
        //    {
        //        Debug.Log(obj.name + "跑了");
        //        obj.transform.position = new Vector3(38.9f, 1.5f, -72);
        //    }
        //}
    }
}