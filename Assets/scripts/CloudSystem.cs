using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSystem : Zac.ZacGObj
{

    #region Descriptions

    protected Planet parentPlanet;
    [SerializeField]
    protected float cloudHeight = 40.0f;

    [SerializeField]
    protected Cloud pfb_cloud;

    [SerializeField]
    protected int cloudNum = 20;

    [SerializeField]
    protected float defaultCloudMovementSpeed = 5.0f;

    #endregion
    #region Links

    protected List<Cloud> clouds = new List<Cloud>();

    //[SerializeField]
    //protected Material cloudAlphaBlendMat;
    //[SerializeField]
    //protected Material cloudAddMat;

    #endregion
    #region Phases

    public static string reuse_MatPlanetWorldPosStr = "_PlanetWorldPos";
    public static string reuse_MatTintColor = "_TintColor";
    public static string reuse_MatOpacityMul = "_OpacityMul";

    protected void Update()
    {


        if (!GameModule.IsGamePaused)
            for (int i = 0; i < clouds.Count; i++)
            {
                clouds[i].MovementSpeed = defaultCloudMovementSpeed;
                clouds[i].ChildUpdate();
            }

    }

    #endregion
    #region Configurations

    public void SetStartingStates(Planet _planet)
    {
        parentPlanet = _planet;

        int genCloudNum = cloudNum;

        float highestCloudLatitude = 0.8f;

        for (int i = 0; i < genCloudNum; i++)
        {
            float y = Random.Range(-highestCloudLatitude, highestCloudLatitude);
            float x = Random.Range(-1.0f, 1.0f);
            float z = Random.Range(-1.0f, 1.0f);

            if (x == 0 && y == 0 && z == 0)
            {
                x = 1.0f;
            }

            Vector3 newCloudPos = new Vector3(x, y, z);

            Cloud newPlanetCloud = Instantiate<Cloud>(pfb_cloud);
            newPlanetCloud.transform.SetParent(transform, false);

            newPlanetCloud.MovementSpeed = defaultCloudMovementSpeed;
            newPlanetCloud.SetCloudSystem(this);
            newPlanetCloud.SetCloudPos(newCloudPos.normalized * cloudHeight);

            clouds.Add(newPlanetCloud);

        }

    }

    public void SetSize(float _size)
    {
        cloudHeight = _size;
    }

    #endregion
    #region Services


    #endregion


}
