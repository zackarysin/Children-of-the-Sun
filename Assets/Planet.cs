using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Zac.ZacGOSingleton<Planet> {

    protected PlanetTerrain planetTerrain;

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
        planetTerrain = GetComponentInChildren<PlanetTerrain>();
        planetTerrain.SetStartingStates();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
