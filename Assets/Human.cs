﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : Agent {

    protected Image speechBubble;
    protected Image speechBubbleContent;
    protected Canvas myCanvas;

    protected override void set()
    {
        base.set();
        myCanvas = GetComponentInChildren<Canvas>();
        speechBubble = myCanvas.transform.Find("speechBubble").GetComponent<Image>();
        speechBubbleContent = speechBubble.transform.Find("speechBubbleContent").GetComponent<Image>();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected void speechBubbleFaceCamera()
    {
        float forwardDot = Vector3.Dot(speechBubble.transform.forward, GameModule.MainCamera.transform.forward);
        float rightDot = Vector3.Dot(speechBubble.transform.right, GameModule.MainCamera.transform.forward);
        //speechBubble.transform.LookAt(GameModule.MainCamera.transform, camera.transform.up);

        speechBubble.transform.LookAt(speechBubble.transform.position + forwardDot * speechBubble.transform.forward + rightDot * speechBubble.transform.right, transform.up);

    }

    protected override void atLookChange()
    {
        //base.atLookChange();
        speechBubbleFaceCamera();
    }

    // Update is called once per frame
    void Update () {

        speechBubbleFaceCamera();
        //speechBubble.transform.forward = forwardDot * speechBubble.transform.forward + rightDot * speechBubble.transform.right;
    }
}
