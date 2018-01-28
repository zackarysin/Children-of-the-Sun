using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoModule : MonoBehaviour {

    [SerializeField]
    protected bool isOff = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isOff) { 
            if (Input.GetKeyDown(KeyCode.A))
            {
                TechModule.InventTech(1);
            }
        }
	}
}
