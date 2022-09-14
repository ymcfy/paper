using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fluentTex2 : MonoBehaviour {

    public Material m1;
    public float moveSpeed = 5f;
    public float dir;
    public float time = 0;
    public Material m;

    void Start()
    {
        //m = this.GetComponent<Renderer>().material;
        this.GetComponent<Renderer>().material = m1;
        m1.SetTextureScale("_MainTex", new Vector2(0, 5));
        m1.SetTextureOffset("_MainTex", new Vector2(0, 0));
    }


    void Update()

    {
        this.GetComponent<Renderer>().material = m1;
        if (m1)
        {
            time += Time.deltaTime;
            dir = time * moveSpeed;
            m1.SetTextureOffset("_MainTex", new Vector2(0, -dir));
            
        }
    }
}
