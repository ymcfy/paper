using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class tishixinxi : MonoBehaviour
{
    public Image image;
    void Start()
    {
        //image.transform.position= Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void dakai()
    {
        image.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
    }
    public void guanbi()
    {
        image.transform.DOLocalMove(new Vector3(0, 900, 0), 0.5f);
    }
}
