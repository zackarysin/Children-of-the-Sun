using UnityEngine;
using System.Collections;

public class Cloud : Zac.ZacGObj
{

    #region Descriptions

    protected CloudSystem parentCloudSystem;

    #endregion
    #region Links

    [SerializeField]
    protected GameObject cloudModelGO;

    [SerializeField]
    protected ParticleSystem particleSys;
    public ParticleSystem ParticleSys
    {
        get
        {
            return particleSys;
        }
    }


    #endregion
    #region States

    protected float movementSpeed = 5.0f;
    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }
        set
        {
            movementSpeed = value;
        }
    }

    #endregion


    // Use this for initialization
    protected override void Start()
    {
        //particleSys.size
    }



    public override void ChildUpdate()
    {
        if (parentCloudSystem != null)
        {
            transform.Rotate(parentCloudSystem.transform.up, movementSpeed * Time.deltaTime);
        }

        base.ChildUpdate();
    }

    #region Configurations

    //public void MovementSpeed

    public void SetCloudSystem(CloudSystem _cloudSystem)
    {
        parentCloudSystem = _cloudSystem;
    }

    public void SetCloudPos(Vector3 _pos)
    {
        cloudModelGO.transform.localPosition = _pos;

        cloudModelGO.transform.rotation = Quaternion.FromToRotation(cloudModelGO.transform.up, (cloudModelGO.transform.position - parentCloudSystem.transform.position).normalized);
    }


    #endregion

}
