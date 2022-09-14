using UnityEngine;
/// <summary>
/// 控制生成的模型
/// </summary>
public class model : MonoBehaviour
{
    public Sprite highSprite;
    public Sprite normalSprite;
    public GameObject cube;//控制哪一个模型，由MouseHightLight脚本的40行赋值
    public int flag;//活动形式，如前后、左右、旋转等，目前已弃用
    public int flag1;//是否被锁定，1是未被锁定，2是被锁定
    public float InitialPositionX;
    public float InitialPositiony;
    public float InitialPositionZ;
    public float InitialAngelX;
    public float InitialAngelY;
    public float InitialAngelZ;
    public float InitialScaleX;
    public float InitialScaleY;
    public float InitialScaleZ;
    // Use this for initialization
    private void Start()
    {
        flag1 = 2;//默认初始值是锁定，同时注意在unity中赋初始值，在start()函数里才管用
    }
    // Update is called once per frame
    private void Update()
    {
    }
    public void Magnify()
    {
        cube.transform.localScale += new Vector3(InitialScaleX * 3, InitialScaleY * 3, InitialScaleZ * 3);
    }
    public void resetObject()
    {
        cube.transform.position = new Vector3(InitialPositionX, InitialPositiony, InitialPositionZ);
        //cube.transform.rotation = Quaternion.identity;
        cube.transform.rotation = Quaternion.Euler(InitialAngelX, InitialAngelY, InitialAngelZ);
        cube.transform.localScale = new Vector3(InitialScaleX, InitialScaleY, InitialScaleZ);
    }
    public void shangxia()
    {
        flag = 1;
    }
    public void qianhou()
    {
        flag = 2;
    }
    public void xuanzhuan()
    {
        flag = 3;
    }
    /// <summary>
    /// 锁定生成的模型，被场景管理模块的“锁定选中”按钮绑定
    /// </summary>
    public void suoding()
    {
        flag1 = 2;//切换为锁定模式
        //GameObject.Find("Canvas/RawImage_moshi/Button_lock").GetComponent<Image>().sprite = highSprite;
        //GameObject.Find("Canvas/RawImage_moshi/Button_unlock").GetComponent<Image>().sprite = normalSprite;
    }
    /// <summary>
    /// 解锁绑定，被场景管理模块的“解锁选中”按钮绑定
    /// </summary>
    public void jiesuo()
    {
        flag1 = 1;//切换为未被绑定模式
        cube.GetComponent<Rotate>().enabled = true;//开启rotate脚本
        //GameObject.Find("Canvas/RawImage_moshi/Button_unlock").GetComponent<Image>().sprite = highSprite;
        //GameObject.Find("Canvas/RawImage_moshi/Button_lock").GetComponent<Image>().sprite = normalSprite;
    }
}
