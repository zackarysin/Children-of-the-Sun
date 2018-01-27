using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : Zac.ZacGOSingleton<Planet> {

    protected PlanetTerrain planetTerrain;
    protected Water water;
    protected CloudSystem cloudSystem;

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
        water = GetComponentInChildren<Water>();
        cloudSystem = GetComponentInChildren<CloudSystem>();

        planetTerrain.ReportSet();
        water.ReportSet();
        planetTerrain.SetStartingStates();
        cloudSystem.SetStartingStates(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
