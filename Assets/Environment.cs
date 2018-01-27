using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : Zac.ZacGOSingleton<Environment> {

    public static Environment Instance
    {
        get
        {
            return instance;
        }       
    }

    protected override void set()
    {
        //base.set();
        establishZacSingleton(this);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
