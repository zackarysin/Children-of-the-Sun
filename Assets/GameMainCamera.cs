using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainCamera : Zac.ZacGObj {

    protected Camera camera;
    public Camera Camera
    {
        get
        {
            return camera;
        }
    }

    protected override void set()
    {
        camera = GetComponent<Camera>();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
