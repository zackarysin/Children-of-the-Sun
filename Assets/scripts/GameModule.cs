using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModule : Zac.ZacGOSingleton<GameModule>
{
    #region States

    [SerializeField]
    protected int ori_naturalEnergyValue = 200000;
    protected int naturalEnergyValue;
    [SerializeField]
    protected int ori_capturedEnergyValue = 0;
    protected int capturedEnergyValue = 0;
    [SerializeField]
    protected int brainPowerValue = 0;

    protected int techPointValue = 0;
    public static int TechPointValue
    {
        get
        {
            return instance.techPointValue;
        }
    }

    protected bool isGamePaused = false;
    public static bool IsGamePaused
    {
        get
        {
            return instance.isGamePaused;
        }
    }

    #endregion


    [SerializeField]
    protected GameMainCamera mainCamera;
    public static GameMainCamera MainCamera
    {
        get
        {
            return instance.mainCamera;
        }
    }

    protected override void set()
    {
        establishZacSingleton(this);
        GameObject.Find("Planet").GetComponent<Planet>().ReportSet();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        PlacementModule.Generate(0, 100);
        PlacementModule.Generate(1, 2, true);
        //PlacementModule.Generate(2, 1, true);

        // No tech stage
        TechModule.InventTech(0);
        //CaptureEnergy(2);
        ChangeEnergyConsompution(2);
        ChangeBrainPowerValue(2);
        UpdateEnergyValues();
        TechPanel.ChangeTechPointValue(0);



        //TechPanel.ChangeTechPointValue(techPointValue);

        //UICanvas.ChangeBrainPowerValue(instance.c);
    }

    protected float timePassed = 0;

	// Update is called once per frame
	void Update () {

        timePassed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.I))
        {
            AddTechPoints(Human.HumanPopulation);
        }

        //Debug.Log(Random.Range(0.0f, 1.0f) + " " + TechModule.TechInfos[TechModule.CrtTechLevel].PopulationGrowthPerSecond * Time.deltaTime);

        if (Random.Range(0.0f, 1.0f) < TechModule.TechInfos[TechModule.CrtTechLevel].PopulationGrowthPerSecond * Time.deltaTime)
        {
            //Debug.Log(Random.Range(0.0f, 1.0f));
            //Debug.Log(TechModule.TechInfos[TechModule.CrtTechLevel].PopulationGrowthPerSecond * Time.deltaTime);

            TechInfo crtTechInfo = TechModule.TechInfos[TechModule.CrtTechLevel];
            int unitToGenerate = (int) ((float)Human.HumanPopulation * TechModule.TechInfos[TechModule.CrtTechLevel].PopulationGrowthMul);

            if (TechModule.CrtTechLevel == 4)
            {
                PlacementModule.Generate(5, 1, true);
            }
            else if (TechModule.CrtTechLevel == 3)
            {
                PlacementModule.Generate(4, 1, true);
            }
            else if (TechModule.CrtTechLevel == 2)
            {
                PlacementModule.Generate(3, 1, true);
            }
            else if (TechModule.CrtTechLevel == 1)
            {
                PlacementModule.Generate(2, 1, true);
            }

            PlacementModule.Generate(1, unitToGenerate+1, true);

            ChangeEnergyConsompution(Human.HumanPopulation * crtTechInfo.EnergyConsumptionPerPopulation);
            ChangeBrainPowerValue(Human.HumanPopulation);
            UpdateEnergyValues();

        }



        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    Ray ray = GameModule.MainCamera.Camera.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Vector3 hitPoint = hit.point;

        //        Agent newTree = Instantiate<Agent>(AgentModule.AgentInfos[AgentModule.Tree_ID].Agent);

        //        newTree.transform.position = hitPoint;
        //        newTree.transform.up = (newTree.transform.position - Planet.Instance.transform.position).normalized;

        //        // Do something with the object that was hit by the raycast.
        //    }
        //}

    }

    public static void UpdateEnergyValues()
    {
        UICanvas.ChangeNaturalEnergyValue(instance.naturalEnergyValue);
        UICanvas.ChangeCapturedEnergyValue(instance.capturedEnergyValue);
        UICanvas.ChangeBrainPowerValue(instance.brainPowerValue);
    }

    //public static void CaptureEnergy(int _value)
    //{
    //    instance.naturalEnergyValue -= _value;
    //    instance.capturedEnergyValue += _value;

    //    UICanvas.ChangeNaturalEnergyValue(instance.naturalEnergyValue);
    //    UICanvas.ChangeCapturedEnergyValue(instance.capturedEnergyValue);
    //}

    public static void ChangeEnergyConsompution(int _value)
    {
        instance.naturalEnergyValue = instance.ori_naturalEnergyValue - _value;
        instance.capturedEnergyValue = instance.ori_capturedEnergyValue + _value;
    }

    public static void ChangeBrainPowerValue(int _value)
    {
        instance.brainPowerValue = _value;
       

    }

    public static void UseTechPoints(int _value)
    {
        instance.techPointValue -= _value;
        TechPanel.ChangeTechPointValue(instance.techPointValue);
    }

    public static void AddTechPoints(int _value)
    {
        instance.techPointValue += _value;
        TechPanel.ChangeTechPointValue(instance.techPointValue);

        TechPanel.UpdateTechList();
    }

}
