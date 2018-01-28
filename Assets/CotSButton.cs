using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CotSButton : Zac.ZacGObj {

    [SerializeField]
    protected Sprite[] changeSprites;

    protected Button button;
    public Button Button
    {
        get
        {
            return button;
        }
    }

    public void ChangeSprite(int _idx)
    {
        ((Image)button.targetGraphic).sprite = changeSprites[_idx];
    }

    protected override void set()
    {
        base.set();
        button = GetComponent<Button>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
