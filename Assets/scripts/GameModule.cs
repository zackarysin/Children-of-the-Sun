using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModule : Zac.ZacGOSingleton<GameModule>
{
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
}
