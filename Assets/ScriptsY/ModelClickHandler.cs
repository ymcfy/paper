using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ModelClickHandler : MonoBehaviour {

    //是否有一次单击
    private bool hasOneClick = false;
    Button btn;
    //计时器
    private float timer = 0;
    //默认双击时间间隔
    public float doubleClickInterval = 1.5f;
    private GameObject pre;//要生成的物体
    //存储所有生成的模型
    public static List<GameObject> generateObjects;
    //public GameObject maincamera;

    // Use this for initialization
    void Start () {
        //获得button属性
        btn = this.GetComponent<Button>();
        //添加点击事件所挂接的函数
        btn.onClick.AddListener(OnClick);
    }
	
	// Update is called once per frame
	void Update () {
        if (hasOneClick == true)
        {
            timer += Time.deltaTime;
            if (timer > doubleClickInterval)
            {
                hasOneClick = false;
                timer = 0;
            }
        }
    }

    public void OneClick()
    {
        foreach (Transform E in GameObject.Find("Canvas (1)/ModelBase/Scroll View/Viewport/Content").transform.GetComponentsInChildren<Transform>())
        {
            if (E.name.Contains("Button"))
            {
                E.gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.white;
            }
        }
        //foreach (Transform E in GameObject.Find("Canvas3/视频/Viewport/Content").transform.GetComponentsInChildren<Transform>())
        //{
        //    if (E.name.Contains("Button"))
        //    {
        //        E.gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.white;
        //    }
        //}
        //foreach (Transform E in GameObject.Find("Canvas3/文件/Viewport/Content").transform.GetComponentsInChildren<Transform>())
        //{
        //    if (E.name.Contains("Button"))
        //    {
        //        E.gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.white;
        //    }
        //}
        //读取当前挂接物体的所有子物体
        //foreach (Transform T in this.GetComponentsInChildren<Transform>())
        //{
        //    //如果子物体的名字为Text1，那么就可以用超链接打开，因为Text1里面保存着文件路径
        //    if (T.name == "Text1")
        //    {
        //        //打开文件
        //        HouZhuiMing = T.GetComponent<Text>().text;
        //    }
        //    if (T.name == "Text")
        //    {
        //        //打开文件
        //        fileName = T.GetComponent<Text>().text;
        //    }
        //    if (T.name == "Text2")
        //    {
        //        //打开文件
        //        fileNameWithoutHouZhuiMing = T.GetComponent<Text>().text;
        //    }
        //}
        btn.GetComponent<Image>().color = Color.green;
    }

    /// <summary>
    /// 点击事件所在函数 点击即可打开文件
    /// 注：如果需要传参，可以使用delegate
    /// </summary>
    public void OnClick()
    {
        if (hasOneClick == false)
        {
            hasOneClick = true;
            OneClick();
        }
        else
        {
            if (timer < doubleClickInterval)
            {
                hasOneClick = false;
                DoubleClick();
                timer = 0;
            }
            else
            {
                timer = 0;
                OneClick();
            }
        }
    }

    public void DoubleClick()
    {
        string fullPath = null;
        //读取当前挂接物体的所有子物体
        foreach (Transform T in this.GetComponentsInChildren<Transform>())
        {
            //如果子物体的名字为Text1，那么就可以用超链接打开，因为Text1里面保存着文件路径
            if (T.name == "Text1")
            {
                //打开文件
               // HouZhuiMing = T.GetComponent<Text>().text;
            }
            if (T.name == "Text")
            {
                //打开文件
               // fileName = T.GetComponent<Text>().text;
            }
            if (T.name == "Text2")
            {
                //打开文件
                fullPath = T.GetComponent<Text>().text;
            }
        }
            generateModel(fullPath);
    }
    /// <summary>
    /// 双击生成模型
    /// </summary>
    public void generateModel(string fullPath) {
        //maincamera.GetComponent<MouseHighlight>().enabled = true;
        //maincamera.transform.position = new Vector3(40, 6.09f, -84.39f);//调节摄像机位置
        DirectoryInfo direction = new DirectoryInfo(fullPath);
        //获取文件名
        //Debug.Log(fullPath);
        //先根据斜杠截取，然后根据.截取
        string[] paths = fullPath.Split('/');
        //如果路径里包含"fbx"或者"FBX"
        if (fullPath.Contains("fbx")||fullPath.Contains("FBX")||fullPath.Contains("Fbx"))
        {
            //GameObject image = Resources.Load("folder/model/" + DeviceId + ".FBX") as GameObject;
            //pre = Instantiate(image, new Vector3(38.9f, 1.5f, -72), Quaternion.identity);
            ////if (pre != null)
            ////{
            ////    GameObject.Find("Canvas/Text").GetComponent<Text>().text = "不是null";
            ////}
            ////else
            ////{
            ////    GameObject.Find("Canvas/Text").GetComponent<Text>().text = "null";
            ////}
            ////pre.transform.parent = cube.transform;
            //pre.transform.position = new Vector3(38.9f, 1.5f, -72);
            //Debug.Log(pre.name);
            //pre.AddComponent<Rotate>();
            //pre.AddComponent<MeshCollider>();
            //pre.SetActive(true);
            //generateObjects.Add(pre);
        }
        //如果路径中包含prefab
        if (fullPath.Contains("prefab"))
        {
            string fullName = paths[paths.Length - 1];
            string[] names = fullName.Split('.');
            string name = names[0];
            //Debug.Log(names[0]);
            ////下面是prefab格式的
            GameObject prefabGenerate = (GameObject)Resources.Load("ModelBase/ModelPrefab/" + name);
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
        }

        ////Debug.Log(Application.dataPath + fullPath);
        //FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        ////Debug.Log(files.Length + "model文件夹数量");
        ////string isExist;
        //for (int i = 0; i < files.Length; i++)
        //{
        //    //Debug.Log("正在查找" + fullName);
        //    if (files[i].Name.Equals(fullNameFbx) || files[i].Name.Equals(fullNamePrefab)
        //        || files[i].Name.Equals(fullNameFbxLarge) || files[i].Name.Equals(fullNamePrefabLarge))
        //    {
        //        //files[i] = new GameObject(files[i].Name) as gameObject;
        //        //Debug.Log(fullName + "存在");
        //        //pre1 = gameObject.transform.Find(x).gameObject; //将找到的这个物体赋值给pre1
        //        //Debug.Log(files[i].FullName);
        //        //pre1 = GameObject.Find(fullName);

        //        if (files[i].Name.Equals(fullNameFbx) || files[i].Name.Equals(fullNameFbxLarge))
        //        {
        //            //下面是fbx格式的
        //            //GameObject.Find("Canvas/Text").GetComponent<Text>().text = "ok";
        //            //GameObject cube = Instantiate(cubePresent);
        //            ////GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //            //cube.transform.position = new Vector3(38.9f, 1.5f, -72);
        //            ////cube.GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,0.5f);
        //            //cube.GetComponent<BoxCollider>().size = new Vector3(10,10,10);
        //            GameObject image = Resources.Load("folder/model/" + DeviceId + ".FBX") as GameObject;
        //            pre = Instantiate(image, new Vector3(38.9f, 1.5f, -72), Quaternion.identity);
        //            //if (pre != null)
        //            //{
        //            //    GameObject.Find("Canvas/Text").GetComponent<Text>().text = "不是null";
        //            //}
        //            //else
        //            //{
        //            //    GameObject.Find("Canvas/Text").GetComponent<Text>().text = "null";
        //            //}
        //            //pre.transform.parent = cube.transform;
        //            pre.transform.position = new Vector3(38.9f, 1.5f, -72);
        //            Debug.Log(pre.name);
        //            pre.AddComponent<Rotate>();
        //            pre.AddComponent<MeshCollider>();
        //            pre.SetActive(true);
        //            generateObjects.Add(pre);
        //            //pre.AddComponent<BoxCollider>();
        //            break;
        //        }
        //        else
        //        {
        //            //下面是prefab格式的
        //            GameObject prefabGenerate = (GameObject)Resources.Load("folder/model/" + DeviceId);
        //            pre = Instantiate(prefabGenerate);
        //            //Debug.Log(pre.name);
        //            pre.transform.position = new Vector3(38.9f, 1.5f, -72);
        //            pre.SetActive(true);
        //            pre.AddComponent<MeshCollider>();
        //            pre.AddComponent<SpectrumController>();//这个脚本的作用是高亮
        //            pre.GetComponent<SpectrumController>().enabled = false;//使这个脚本暂时不启用
        //            //pre.AddComponent<MouseHighlight>();//这个脚本的作用是点击高亮
        //            pre.AddComponent<Rotate>();//这个脚本的作用是移动模型
        //            generateObjects.Add(pre);
        //            //foreach (GameObject item in generateObjects)
        //            //{
        //            //    Debug.Log(item.name + "---laqu1");
        //            //}
        //        }
        //    }
        //}
    }
}
