using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Zac.ZacGOSingleton<Water> {

    [SerializeField]
    protected AnimationCurve[] seaWaveCurves;
    [SerializeField]
    protected float waveMultiplier = 1.0f;
    [SerializeField]
    protected float waveFreqMul = 1.0f;

    protected float timePassed = 0.0f;

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
        timePassed += Time.deltaTime;

        float x = seaLevelHeight * 2 + seaWaveCurves[0].Evaluate(timePassed * waveFreqMul + 0.0f) * waveMultiplier;
        float y = seaLevelHeight * 2 + seaWaveCurves[1].Evaluate(timePassed * waveFreqMul + 0.5f) * waveMultiplier;
        float z = seaLevelHeight * 2 + seaWaveCurves[2].Evaluate(timePassed * waveFreqMul + 0.2f) * waveMultiplier;

        transform.localScale = new Vector3(x, y, z);

        
    }
}
