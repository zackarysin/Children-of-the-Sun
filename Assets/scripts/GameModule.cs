using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModule : Zac.ZacGOSingleton<GameModule>
{
    #region States

    [SerializeField]
    protected int naturalEnergyValue = 200000;
    [SerializeField]
    protected int capturedEnergyValue = 0;
    [SerializeField]
    protected int brainPowerValue = 0;

    protected int techPointValue = 0;

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

        CaptureEnergy(2);
        ChangeBrainPowerValue(2);
        UpdateEnergyValues();
        TechPanel.ChangeTechPointValue(0);

        //TechPanel.ChangeTechPointValue(techPointValue);

        //UICanvas.ChangeBrainPowerValue(instance.c);
    }
	
	// Update is called once per frame
	void Update () {

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

    public static void CaptureEnergy(int _value)
    {
        instance.naturalEnergyValue -= _value;
        instance.capturedEnergyValue += _value;

        UICanvas.ChangeNaturalEnergyValue(instance.naturalEnergyValue);
        UICanvas.ChangeCapturedEnergyValue(instance.capturedEnergyValue);
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
    }

}
