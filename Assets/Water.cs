using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Zac.ZacGOSingleton<Water> {

    protected float seaLevelHeight;
    public static float SeaLevelHeight
    {
        get
        {
            return instance.seaLevelHeight;
        }
    }

    protected override void set()
    {
        seaLevelHeight = transform.localScale.x/2.0f;
        establishZacSingleton(this);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
