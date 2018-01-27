using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TechInfo
{
    [SerializeField]
    protected string techName;
    [SerializeField]
    protected int techCost = 1;
    public int TechCost
    {
        get
        {
            return techCost;
        }
    }
    [SerializeField]
    protected int energyCaptured = 1;
    public int EnergyCaptured
    {
        get
        {
            return energyCaptured;
        }
    }
    [HideInInspector]
    public bool isInvented = false;
}

public class TechModule : Zac.ZacGOSingleton<TechModule> {

    [SerializeField]
    protected TechInfo[] techInfos;
    public static TechInfo[] TechInfos
    {
        get
        {
            return instance.techInfos;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void InventTech(int _techID)
    {
        TechInfo thisTech = instance.techInfos[_techID];
        GameModule.CaptureEnergy(thisTech.EnergyCaptured);
        GameModule.UseTechPoints(thisTech.TechCost);
        thisTech.isInvented = true;

        if(_techID == 0)
        {
            PlacementModule.Generate(2, 4, true);
            PlacementModule.Generate(1, 4, true);
        }

    }

    public static bool IsTechInvented(int _techID)
    {
        return instance.techInfos[_techID].isInvented;
    }

    protected override void set()
    {
        //base.set();
        establishZacSingleton(this);
    }

}
