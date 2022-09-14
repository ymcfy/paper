using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class laqu2 : MonoBehaviour
{
    public Dropdown dropdown;
    public Text text;
    public GameObject pre1;
    public GameObject maincamera;
    private Vector3 weizhi;


    public void OnValueChanged()
    {
        text.text = dropdown.options[dropdown.value].text;
        Debug.Log("11");
    }

    public void shengcheng()
    {
        string x;
        maincamera.transform.position = new Vector3(41, 2, -87);
        x = dropdown.options[dropdown.value].text;
        Debug.Log(x);
        foreach (var t in GetComponentsInChildren<Transform>(true))
        {
            Debug.Log(t.name);
            if (t.name == x)
            {
                Debug.Log(t.name);
                // pre1 = t.gameObject;
            }

        }
        /* if (pre1 != null)
         {
             pre1.SetActive(true);
             pre1.AddComponent<SpectrumController>();
             pre1.AddComponent<MeshCollider>();
         }*/
    }
    public void del()
    {
        string x;
        x = dropdown.options[dropdown.value].text;
        pre1 = gameObject.transform.Find(x).gameObject;
        if (pre1 != null)
        {
            pre1.SetActive(false);
            Destroy(pre1.AddComponent<SpectrumController>());
            Destroy(pre1.AddComponent<MeshCollider>());
        }
    }
    void Start()
    {
        weizhi = maincamera.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }
}