using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidFire : MonoBehaviour {

    public GameObject fire_prefab;
    public Transform set_position;

    // Use this for initialization
    void Start () {
        InvokeRepeating("CreateFire", 0f,0.3f);

    }
	
    void CreateFire()
    {
        Instantiate(fire_prefab, set_position.position, Quaternion.identity);
    }
	// Update is called once per frame
	void Update () {
 
 
    }
}
