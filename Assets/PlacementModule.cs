using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementModule : Zac.ZacGOSingleton<PlacementModule> {

    protected override void set()
    {
        //base.set();
        establishZacSingleton(this);
    }

    protected void randomPlacement(Agent _toBePlaceAgent)
    {
        float rayShootOriginDist = PlanetTerrain.MaxTerrainHeight;

        Vector3 pos = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        pos = pos.normalized;

        RaycastHit hit;
        
        Ray ray = new Ray(pos * rayShootOriginDist, -pos);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;

            float hitHeight = (hitPoint - Planet.Instance.transform.position).magnitude;

            //Debug.Log(hitHeight);

            // prevent underwater agent
            if(hitHeight <= PlanetTerrain.SeaHeight + 0.05f)
            {
                Destroy(_toBePlaceAgent.gameObject);
                return;
            }

            Agent newTree = _toBePlaceAgent;

            newTree.transform.position = hitPoint;
            newTree.transform.up = (newTree.transform.position - Planet.Instance.transform.position).normalized;

            // Do something with the object that was hit by the raycast.
        }


    }

    public static void Generate(int agent_id, int num = 1)
    {
        for(int i=0; i<num; i++)
        {
            Agent generatedAgent = AgentModule.GenerateAgent(agent_id);
            instance.randomPlacement(generatedAgent);
        }
       
    }



}
