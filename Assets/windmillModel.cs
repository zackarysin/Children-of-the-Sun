using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmillModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Transform centre = transform.Find("centre");

        LeanTween.rotateAround(centre.gameObject, centre.forward, 360, 6.0f).setRepeat(-1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
