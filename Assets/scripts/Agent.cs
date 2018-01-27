using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    #region Description

    [SerializeField]
    protected bool isCouldMove = false;
    [SerializeField]
    protected float moveSpeed = 1.0f;
    [SerializeField]
    protected float wanderSpace = 1;

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
        if (isCouldMove)
        {
            StartCoroutine(wandering());
        }
	}

    // Update is called once per frame
    void Update()
    {

    }

    protected Vector3 findWanderTarget()
    {
        Vector3 wanderFinalTarget = transform.localPosition
            + transform.forward * Random.Range(-1.0f, 1.0f) 
            + transform.right * Random.Range(-1.0f, 1.0f);
        wanderFinalTarget = wanderFinalTarget.normalized;

        float surfaceHeight;

        if(PlacementModule.FindTerrainSurfece(wanderFinalTarget, out surfaceHeight))
        {
            return wanderFinalTarget * surfaceHeight;
        }
        else
        {
            return Vector3.zero;
        }
        
    }

    protected IEnumerator wandering()
    {
        for (;;)
        {
            yield return StartCoroutine(wanderToTarget());

            //yield return null;
        }
    }

    protected IEnumerator wanderToTarget()
    {
        Vector3 target = findWanderTarget();
        if(target == Vector3.zero)
        {
            yield break;
        }

        //transform.forward = (target - transform.position).normalized;
        transform.LookAt(target, transform.localPosition.normalized);

        for (;;)
        {

            //Debug.Log();
            rb.MovePosition(transform.localPosition + (target - transform.position).normalized * Time.deltaTime * moveSpeed);
            //rb.MovePosition((transform.forward).normalized * Time.deltaTime * moveSpeed);
            yield return null;
            //Planet.ApplyGravityToAgent(this);
            transform.LookAt(target, transform.localPosition.normalized);

            if ((transform.position - target).sqrMagnitude <= 0.2f * 0.2f)
            {
                yield break;
            }

            yield return null;
        }
        
    }


    public void SetStartingStates(GameObject _model)
    {
        model = _model;
        rb = GetComponent<Rigidbody>();
    }
}
