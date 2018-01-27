using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AgentInfo
{
    [SerializeField]
    protected Agent agent;
    public Agent Agent
    {
        get
        {
            return agent;
        }
    }

    [SerializeField]
    protected GameObject[] models;
    public GameObject[] Models
    {
        get
        {
            return models;
        }
    }

    [SerializeField]
    protected string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
}

public class AgentModule : Zac.ZacGOSingleton<AgentModule> {

    public readonly static int Tree_ID = 0;

    [SerializeField]
    protected AgentInfo[] agentInfos;
    public static AgentInfo[] AgentInfos
    {
        get
        {
            return instance.agentInfos;
        }
    }

    protected override void set()
    {
        establishZacSingleton(this);
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {



    }

    public static Agent GenerateAgent(int _agent_id)
    {
        AgentInfo agentInfo = AgentInfos[_agent_id];

        Agent newAgent = Instantiate<Agent>(agentInfo.Agent);

        int randomModelIdx = Random.Range(0, agentInfo.Models.Length);
        GameObject model = Instantiate<GameObject>(agentInfo.Models[randomModelIdx]);

        model.transform.SetParent(newAgent.transform, false);
        model.transform.localPosition = Vector3.zero;
        newAgent.SetStartingStates(model);

        return newAgent;

    }
}
