using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoModule : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TechModule.InventTech(1);
        }
	}
}
