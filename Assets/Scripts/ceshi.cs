using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ceshi : MonoBehaviour
{
    public GameObject obj;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("111");
        GameObject ParentObject = GameObject.Find("123");
        GameObject ChildObject = ParentObject.transform.Find("Water").gameObject;
        ChildObject.SetActive(true);

    }

}
