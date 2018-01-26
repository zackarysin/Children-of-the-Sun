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
	void Update () {






	}
}
