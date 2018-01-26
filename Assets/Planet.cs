using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Zac.ZacGOSingleton<Planet> {

    public static Planet Instance
    {
        get
        {
            return instance;
        }
    }

    protected override void set()
    {
        establishZacSingleton(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
