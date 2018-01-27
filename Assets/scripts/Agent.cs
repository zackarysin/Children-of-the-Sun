using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    #region Description

    [SerializeField]
    protected bool isCouldMove = false;
    [SerializeField]
    protected float moveSpeed = 1.0f;

    protected Rigidbody rb;
    public Rigidbody Rb
    {
        get
        {
            return rb;
        }
    }

    #endregion

    protected GameObject model;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetStartingStates(GameObject _model)
    {
        model = _model;
        rb = GetComponent<Rigidbody>();
    }
}
