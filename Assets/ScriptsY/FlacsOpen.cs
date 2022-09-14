using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlacsOpen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OpenFlacs() {
        Application.OpenURL("C:\\Program Files (x86)\\GexCon\\FLACS_v9.0\\bin\\runmanager.exe");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
