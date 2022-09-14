using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chazhao : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void chongxinxuanze()
    {
        GameObject.Find("dingdian/pos1/Button").SetActive(false);
        GameObject.Find("dingdian/pos2/Button").SetActive(false);
        GameObject.Find("dingdian/pos3/Button").SetActive(false);
        GameObject.Find("dingdian/pos4/Button").SetActive(false);
        GameObject.Find("dingdian/pos5/Button").SetActive(false);
    }
}
