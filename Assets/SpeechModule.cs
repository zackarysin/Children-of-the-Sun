using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeechBubbleInfo
{
    [SerializeField]
    protected string speechName;
    [SerializeField]
    protected Sprite speechSprite;
    public Sprite Sprite
    {
        get
        {
            return speechSprite;
        }
    }
       
}

public class SpeechModule : Zac.ZacGOSingleton<SpeechModule> {

    [SerializeField]
    protected SpeechBubbleInfo[] speechBubbleInfos;
    public static SpeechBubbleInfo[] SpeechBubblesInfos
    {
        get
        {
            return instance.speechBubbleInfos;
        }
    }

    protected override void set()
    {
        //base.set();
        establishZacSingleton(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
