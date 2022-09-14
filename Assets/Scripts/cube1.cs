using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cube1 : MonoBehaviour
{
    public string lname;
    // Use this for initialization
    void Start()
    {
        lname = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameObject.Find("kongzhi2").GetComponent<test>().lname = lname;
        }

    }
    //void OnMouseEnter()
    //{
    //    Debug.Log("ok");
    //}

}
