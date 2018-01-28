using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farmModel : Zac.ZacGObj {

    [SerializeField]
    protected Transform[] plantRots;

    [SerializeField]
    protected float rotSpace = 2.0f;
    [SerializeField]
    protected float rotNoiseSpace = 0.5f;

    // Use this for initialization
    protected override void Start () {

        LTDescr ltdescr = LeanTween.value(gameObject, (_value) =>
        {
            for(int i=0; i< plantRots.Length; i++)
            {
                Vector3 rot = plantRots[i].localEulerAngles;
                rot.x = _value * rotSpace + Random.Range(-rotNoiseSpace, rotNoiseSpace);
                rot.z = _value * rotSpace + Random.Range(-rotNoiseSpace, rotNoiseSpace);
                plantRots[i].localEulerAngles = rot;
            }
        },
        -1.0f, 1.0f, 2.0f);
        ltdescr.setLoopPingPong();// = -1;


    }
	
	// Update is called once per frame
	void Update () {
		



	}
}
