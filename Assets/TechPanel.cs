using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechPanel : Zac.ZacGOSingleton<TechPanel> {

    protected Transform techList;
    protected Vector3 techListPos;
    protected Text techPointText;
    protected Button techBtn;
    protected GridLayoutGroup techGridLayout;

    protected CotSButton[] inventTechButtons;

    protected bool isTechListShown
    {
        get
        {
            return techList.gameObject.activeSelf;
        }
        set
        {
            techList.gameObject.SetActive(value);
        }
    }

    protected override void set()
    {

        establishZacSingleton(this);

        techList = transform.Find("TechList");
        techBtn = transform.Find("TechBtn").GetComponent<Button>();
        techListPos = techList.localPosition;
        techPointText = techBtn.transform.Find("techPointText").GetComponent<Text>();
        techGridLayout = techList.GetComponentInChildren<GridLayoutGroup>();

        techList.gameObject.SetActive(false);

        inventTechButtons = new CotSButton[TechModule.TechInfos.Length];

        for(int i=0; i< inventTechButtons.Length; i++)
        {
            inventTechButtons[i] = techGridLayout.transform.Find(i.ToString()).GetComponent<CotSButton>();
            inventTechButtons[i].ReportSet();
            int idxCache = i;
            inventTechButtons[i].Button.onClick.AddListener(() =>
            {
                handleOnInventTechButtonClicked(idxCache);
            });

            inventTechButtons[i].Button.GetComponentInChildren<Text>().text = TechModule.TechInfos[i].TechCost.ToString();
        }

    }

    protected void handleOnInventTechButtonClicked(int _techIdx)
    {
        if(TechModule.TechInfos[_techIdx].TechCost <= GameModule.TechPointValue)
        {
            TechModule.InventTech(_techIdx);
        }

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void handleOnTechButtonClicked()
    {
        isTechListShown = !isTechListShown;

        float from = isTechListShown? 0: 1;
        float to = isTechListShown? 1: 0;

        LTDescr descr = LeanTween.value(gameObject, (_value) =>
        {
            
            Image img = techList.GetComponent<Image>();

            Color oriColor = img.color;
            oriColor.a = _value;
            img.color = oriColor;

            techList.localPosition = Vector3.Lerp(techListPos - Vector3.up * 20, techListPos, _value);

        }, from, to, 0.25f);
        descr.setEaseOutCubic();

    }

    public static void ChangeTechPointValue(int _idx)
    {
        instance.techPointText.text = _idx.ToString();
    }

    public static void UpdateTechList()
    {
        if (TechModule.IsTechInvented(0) && !TechModule.IsTechInvented(1))
        {
            if (TechModule.TechInfos[1].TechCost <= GameModule.TechPointValue)
            {
                instance.inventTechButtons[1].ChangeSprite(1);
            }
        }
        else if (TechModule.IsTechInvented(1) && !TechModule.IsTechInvented(2))
        {
            if (TechModule.TechInfos[2].TechCost <= GameModule.TechPointValue)
            {
                instance.inventTechButtons[2].ChangeSprite(1);
            }
        }
        else if (TechModule.IsTechInvented(2) && !TechModule.IsTechInvented(3))
        {
            if (TechModule.TechInfos[3].TechCost <= GameModule.TechPointValue)
            {
                instance.inventTechButtons[3].ChangeSprite(1);
            }
        }
        else if (TechModule.IsTechInvented(3) && !TechModule.IsTechInvented(4))
        {
            if (TechModule.TechInfos[4].TechCost <= GameModule.TechPointValue)
            {
                instance.inventTechButtons[4].ChangeSprite(1);
            }
        }

    }

    public static void DisableInventTech(int _techIdx)
    {
        instance.inventTechButtons[_techIdx].ChangeSprite(2);
        instance.inventTechButtons[_techIdx].Button.enabled = false;
    }

}
