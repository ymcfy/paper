using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chufacaidan : MonoBehaviour
{
    public GameObject caidan;
    public Text text;
    public GameObject te;
    private bool panduan = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T) && panduan == true)
        {
            caidan.SetActive(true);
            te.SetActive(false);
            panduan = false;
            GameObject.Find("操作台触发").SetActive(false);
            GameObject.Find("青岛炼化-汽油装卸罐车/renwu (1)").GetComponent<CharacterControllercontroller>().enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {


        text.text = "请按T唤醒菜单";
        panduan = true;



    }
    private void OnCollisionExit(Collision collision)
    {
        text.text = "请靠近";

    }

}
