using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changevalue : MonoBehaviour
{
    public Text text;
    public Dropdown dropdown;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnValueChanged()
    {
        text.text = dropdown.options[dropdown.value].text;
        Debug.Log(dropdown.options[dropdown.value].text);
    }
}
