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

    [SerializeField]
    protected float populationGrowthPerSecond = 0;
    public float PopulationGrowthPerSecond
    {
        get
        {
            return populationGrowthPerSecond;
        }
    }

    [SerializeField]
    protected int energyConsumptionPerPopulation = 1;
    public int EnergyConsumptionPerPopulation
    {
        get
        {
            return energyConsumptionPerPopulation;
        }
    }

    [SerializeField]
    protected float populationGrowthMul = 1;
    public float PopulationGrowthMul
    {
        get
        {
            return populationGrowthMul;
        }
    }

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
    protected int crtTechLevel = 0;
    public static int CrtTechLevel
    {
        get
        {
            return instance.crtTechLevel;
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
        
        GameModule.UseTechPoints(thisTech.TechCost);
        thisTech.isInvented = true;

        TechPanel.DisableInventTech(_techID);

        instance.crtTechLevel = _techID;
    }



    public static bool IsTechInvented(int _techID)
    {
        return instance.techInfos[_techID].isInvented;
    }

    //public static bool IsTechCanInvent(int _techID)
    //{
    //    return !instance.techInfos[_techID].isInvented;
    //}

    protected override void set()
    {
        //base.set();
        establishZacSingleton(this);

        GameObject.Find("TechPanel").GetComponent<TechPanel>().ReportSet();
    }

}
