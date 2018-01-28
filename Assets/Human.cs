using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : Agent {

    protected static int humanPopulation = 0;
    public static int HumanPopulation
    {
        get
        {
            return humanPopulation;
        }
    }

    [SerializeField]
    protected float thinkStepTime = 1.5f;
    [SerializeField]
    protected float thinkStepTimeSalt = 0.5f;

    protected Image speechBubble;
    protected Image speechBubbleContent;
    protected Canvas myCanvas;
    protected AudioSource audioSourceForBubble;

    protected override void set()
    {
        base.set();
        myCanvas = GetComponentInChildren<Canvas>();
        speechBubble = myCanvas.transform.Find("speechBubble").GetComponent<Image>();
        speechBubbleContent = speechBubble.transform.Find("speechBubbleContent").GetComponent<Image>();
        audioSourceForBubble = GetComponent<AudioSource>();
        humanPopulation += 1;
    }

    protected float GetThisThinkStepTime()
    {
        return Random.Range(-thinkStepTimeSalt, thinkStepTimeSalt) + thinkStepTime;
    }

    protected IEnumerator ThinkLooping()
    {
        while (true) {

            int firstStepIdx = 0;
            int secondStepIdx = 1;
            int thirdStepIdx = 2;

            if (TechModule.CrtTechLevel == 1)
            {
                secondStepIdx = 3;
            }
            else if(TechModule.CrtTechLevel == 2)
            {
                if(Random.Range(0.0f, 1.0f) <= 0.5f)
                {
                    firstStepIdx = 0;
                    secondStepIdx = 3;
                }
                else
                {
                    firstStepIdx = 6;
                    secondStepIdx = 3;
                }
                
            }
            else if (TechModule.CrtTechLevel == 3)
            {
                float value = Random.Range(0.0f, 1.0f);

                if (value <= 0.3f)
                {
                    firstStepIdx = 0;
                    secondStepIdx = 4;
                }
                else if (value <= 0.6f)
                {
                    firstStepIdx = 6;
                    secondStepIdx = 3;
                }
                else
                {
                    firstStepIdx = 4;
                    secondStepIdx = 5;
                }
            }
            else if (TechModule.CrtTechLevel == 4)
            {
                float value = Random.Range(0.0f, 1.0f);

                if (value <= 0.2f)
                {
                    firstStepIdx = 0;
                    secondStepIdx = 4;
                }
                else if (value <= 0.4f)
                {
                    firstStepIdx = 6;
                    secondStepIdx = 3;
                }
                else if (value <= 0.6f)
                {
                    firstStepIdx = 4;
                    secondStepIdx = 5;
                }
                else
                {
                    firstStepIdx = 7;
                    secondStepIdx = 8;
                }
            }

            LTDescr ltdescr = null;
            speechBubble.transform.localScale = Vector3.zero;
            ltdescr = LeanTween.scale(speechBubble.gameObject, Vector3.one, 0.2f);
            ltdescr.setOnComplete(() =>
            {
                audioSourceForBubble.Play();
            });
            ltdescr.setEaseOutBack();
            speechBubbleContent.sprite = SpeechModule.SpeechBubblesInfos[firstStepIdx].Sprite;
            yield return new WaitForSeconds(GetThisThinkStepTime());
            speechBubble.transform.localScale = Vector3.zero;
            ltdescr = LeanTween.scale(speechBubble.gameObject, Vector3.one, 0.2f);
            ltdescr.setOnComplete(() =>
            {
                audioSourceForBubble.Play();
            });
            ltdescr.setEaseOutBack();
            speechBubbleContent.sprite = SpeechModule.SpeechBubblesInfos[secondStepIdx].Sprite;
            yield return new WaitForSeconds(GetThisThinkStepTime());
            speechBubble.transform.localScale = Vector3.zero;
            ltdescr = LeanTween.scale(speechBubble.gameObject, Vector3.one, 0.2f);
            ltdescr.setOnComplete(() =>
            {
                audioSourceForBubble.Play();
            });
            ltdescr.setEaseOutBack();
            speechBubbleContent.sprite = SpeechModule.SpeechBubblesInfos[thirdStepIdx].Sprite;
            yield return new WaitForSeconds(GetThisThinkStepTime());
            GameModule.AddTechPoints(1);
        }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ThinkLooping());
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
