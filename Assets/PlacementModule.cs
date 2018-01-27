using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementModule : Zac.ZacGOSingleton<PlacementModule> {

    protected override void set()
    {
        //base.set();
        establishZacSingleton(this);
    }

    protected bool randomPlacementOnce(Agent _toBePlaceAgent)
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
            if (hitHeight <= PlanetTerrain.SeaHeight + 0.05f)
            {
                return false;
            }

            _toBePlaceAgent.transform.SetParent(Environment.Instance.transform);
            _toBePlaceAgent.transform.position = hitPoint;
            _toBePlaceAgent.transform.up = (_toBePlaceAgent.transform.position - Planet.Instance.transform.position).normalized;

            return true;
        }
        else
        {
            return false;
        }
    }

    protected void randomPlacement(Agent _toBePlaceAgent, bool _guranteePlacement = false)
    {
        if (_guranteePlacement)
        {
            while (!randomPlacementOnce(_toBePlaceAgent)) ;
        }
        else
        {
            if (!randomPlacementOnce(_toBePlaceAgent))
            {
                Destroy(_toBePlaceAgent.gameObject);
            }
        }
    }

    public static void Generate(int agent_id, int num = 1, bool _isGuranteePlacement = false)
    {
        for(int i=0; i<num; i++)
        {
            Agent generatedAgent = AgentModule.GenerateAgent(agent_id);
            instance.randomPlacement(generatedAgent, _isGuranteePlacement);
        }
       
    }



}
