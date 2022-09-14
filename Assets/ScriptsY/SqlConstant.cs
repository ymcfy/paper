using UnityEngine;

/// <summary>
/// 用来保存SQL的常量
/// </summary>
public class SqlConstant : MonoBehaviour
{
    //因为此处是static，因此要先在start外面初始化，因为start是非static函数
    public static string sqlAddress = "Data source=127.0.0.1;Initial Catalog=SqlTest;User ID=web;Password=web";

    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
