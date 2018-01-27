using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : Zac.ZacGOSingleton<UICanvas> {

    [SerializeField]
    protected Text naturalEnergyText;
    [SerializeField]
    protected Text capturedEngergyText;
    [SerializeField]
    protected Text brainPowerText;

    protected override void set()
    {
        //base.set();
        establishZacSingleton(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ChangeNaturalEnergyValue(int _value)
    {


        instance.naturalEnergyText.text = _value.ToString();
    }

    public static void ChangeCapturedEnergyValue(int _value)
    {


        instance.capturedEngergyText.text = _value.ToString();
    }

    public static void ChangeBrainPowerValue(int _value)
    {


        instance.brainPowerText.text = _value.ToString();
    }
}
