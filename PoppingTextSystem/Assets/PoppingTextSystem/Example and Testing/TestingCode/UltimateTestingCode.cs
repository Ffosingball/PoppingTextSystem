using UnityEngine;
using TMPro;

public class UltimateTestingCode : MonoBehaviour
{
    [SerializeField] private GameObject object1, object2, object3, object4; 
    [SerializeField] private GameObject text1, text2, text3; 
    [SerializeField] private float speed=4f, period=2f, positionToChange=8f;
    [SerializeField] private PoppingTextManager poppingTextManager;

    private PopTextConfiguration popTextConfiguration;
    private float timePassed=0f, direction=1f;
    private TMP_Text appearEffectsText, stayEffectsText, disappearEffectsText;
    private TMP_Text timeAppearStageText, timeStayStageText, timeDisappearStageText;
    private GameObject textSelected;
    private PoppingTextEffects effectSelected;
    private PoppingTextStage stageSelected;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popTextConfiguration = new PopTextConfiguration(object1);
        appearEffectsText = transform.Find("AppearListPanel").Find("effectsList").GetComponent<TMP_Text>();
        stayEffectsText = transform.Find("StayListPanel").Find("effectsList").GetComponent<TMP_Text>();
        disappearEffectsText = transform.Find("DisappearListPanel").Find("effectsList").GetComponent<TMP_Text>();
        textSelected = text1;
        popTextConfiguration.moveWithGameObject = false;
        effectSelected = PoppingTextEffects.Fade;
        stageSelected = PoppingTextStage.Appearing;
        popTextConfiguration.blinkToColor = Color.yellow;
        timeAppearStageText = transform.Find("TimeAppearStage").GetComponent<TMP_Text>();
        timeStayStageText = transform.Find("TimeStayStage").GetComponent<TMP_Text>();
        timeDisappearStageText = transform.Find("TimeDisappearStage").GetComponent<TMP_Text>();
    }



    // Update is called once per frame
    void Update()
    {
        timePassed+=Time.deltaTime;
        object2.transform.Translate(speed*Time.deltaTime*new Vector2(1,0)*direction);
        object3.transform.Translate(speed*Time.deltaTime*new Vector2(1,-1)*direction);

        if(timePassed>=period)
        {
            direction*=-1f;
            timePassed-=period;
            Vector3 pos = object4.transform.position;
            pos.x= direction<0 ? pos.x+positionToChange : pos.x-positionToChange;
            object4.transform.position = pos;
        }

        string listOfEffects = "";

        if(popTextConfiguration.appearEffects.Count==0)
            listOfEffects="None";
        else
        {
            foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.appearEffects)
            {
                listOfEffects=listOfEffects+effectsEnumToText(poppingTextEffects)+"\n";
            }
        }
        appearEffectsText.text = listOfEffects;

        listOfEffects = "";
        if(popTextConfiguration.stayEffects.Count==0)
            listOfEffects="None";
        else
        {
            foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.stayEffects)
            {
                listOfEffects=listOfEffects+effectsEnumToText(poppingTextEffects)+"\n";
            }
        }
        stayEffectsText.text = listOfEffects;

        listOfEffects = "";
        if(popTextConfiguration.disappearEffects.Count==0)
            listOfEffects="None";
        else
        {
            foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.disappearEffects)
            {
                listOfEffects=listOfEffects+effectsEnumToText(poppingTextEffects)+"\n";
            }
        }
        disappearEffectsText.text = listOfEffects;

        timeAppearStageText.text = popTextConfiguration.appearTime+" sec";
        timeStayStageText.text = popTextConfiguration.timeToStay+" sec";
        timeDisappearStageText.text = popTextConfiguration.disappearTime+" sec";
    }


    private string effectsEnumToText(PoppingTextEffects poppingTextEffects)
    {
        switch(poppingTextEffects)
        {
            case PoppingTextEffects.Fade:
                return "Fade";
            case PoppingTextEffects.Enlarge:
                return "Enlarge";
            case PoppingTextEffects.Reduce:
                return "Reduce";
            case PoppingTextEffects.Appear:
                return "Appear";
            case PoppingTextEffects.MoveUp:
                return "Move Up";
            case PoppingTextEffects.MoveDown:
                return "Move Down";
            case PoppingTextEffects.MoveLeft:
                return "Move Left";
            case PoppingTextEffects.MoveRight:
                return "Move Right";
            case PoppingTextEffects.Blink:
                return "Blink";
            case PoppingTextEffects.BlinkColor:
                return "Blink Color";
            case PoppingTextEffects.SecondaryEnlarge:
                return "Secondary enlarge";
            case PoppingTextEffects.SecondaryReduce:
                return "Secondary reduce";
        }

        return "Unidentified";
    }



    public void textSelection(int valueSelected)
    {
        switch(valueSelected)
        {
            case 0:
                textSelected = text1;
                break;
            case 1:
                textSelected = text2;
                break;
            case 2:
                textSelected = text3;
                break;
        }
    }



    public void objectSelection(int valueSelected)
    {
        switch(valueSelected)
        {
            case 0:
                popTextConfiguration.appearAt = object1;
                break;
            case 1:
                popTextConfiguration.appearAt = object2;
                break;
            case 2:
                popTextConfiguration.appearAt = object3;
                break;
            case 3:
                popTextConfiguration.appearAt = object4;
                break;
        }
    }



    public void effectSelection(int valueSelected)
    {
        switch(valueSelected)
        {
            case 0:
                effectSelected = PoppingTextEffects.Fade;
                break;
            case 1:
                effectSelected = PoppingTextEffects.Enlarge;
                break;
            case 2:
                effectSelected = PoppingTextEffects.Reduce;
                break;
            case 3:
                effectSelected = PoppingTextEffects.Appear;
                break;
            case 4:
                effectSelected = PoppingTextEffects.MoveUp;
                break;
            case 5:
                effectSelected = PoppingTextEffects.MoveDown;
                break;
            case 6:
                effectSelected = PoppingTextEffects.MoveLeft;
                break;
            case 7:
                effectSelected = PoppingTextEffects.MoveRight;
                break;
            case 8:
                effectSelected = PoppingTextEffects.Blink;
                break;
            case 9:
                effectSelected = PoppingTextEffects.BlinkColor;
                break;
            case 10:
                effectSelected = PoppingTextEffects.SecondaryEnlarge;
                break;
            case 11:
                effectSelected = PoppingTextEffects.SecondaryReduce;
                break;
        }
    }



    public void stageSelection(int valueSelected)
    {
        switch(valueSelected)
        {
            case 0:
                stageSelected = PoppingTextStage.Appearing;
                break;
            case 1:
                stageSelected = PoppingTextStage.Staying;
                break;
            case 2:
                stageSelected = PoppingTextStage.Disappearing;
                break;
        }
    }



    public void changeMoveWithobject()
    {
        popTextConfiguration.moveWithGameObject=!popTextConfiguration.moveWithGameObject;
    }



    public void addEffect()
    {
        switch(stageSelected)
        {
            case PoppingTextStage.Appearing:
                if(!popTextConfiguration.appearEffects.Contains(effectSelected))
                    popTextConfiguration.appearEffects.Add(effectSelected);
                break;
            case PoppingTextStage.Staying:
                if(!popTextConfiguration.stayEffects.Contains(effectSelected))
                    popTextConfiguration.stayEffects.Add(effectSelected);
                break;
            case PoppingTextStage.Disappearing:
                if(!popTextConfiguration.disappearEffects.Contains(effectSelected))
                    popTextConfiguration.disappearEffects.Add(effectSelected);
                break;
        }
    }



    public void increaseTime()
    {
        switch(stageSelected)
        {
            case PoppingTextStage.Appearing:
                popTextConfiguration.appearTime+=0.2f;
                break;
            case PoppingTextStage.Staying:
                popTextConfiguration.timeToStay+=0.2f;
                break;
            case PoppingTextStage.Disappearing:
                popTextConfiguration.disappearTime+=0.2f;
                break;
        }
    }



    public void decreaseTime()
    {
        switch(stageSelected)
        {
            case PoppingTextStage.Appearing:
                if(popTextConfiguration.appearTime>0.2f)
                    popTextConfiguration.appearTime-=0.2f;
                break;
            case PoppingTextStage.Staying:
                if(popTextConfiguration.timeToStay>0.2f)
                    popTextConfiguration.timeToStay-=0.2f;
                break;
            case PoppingTextStage.Disappearing:
                if(popTextConfiguration.disappearTime>0.2f)
                    popTextConfiguration.disappearTime-=0.2f;
                break;
        }
    }



    public void removeEffect()
    {
        switch(stageSelected)
        {
            case PoppingTextStage.Appearing:
                if(popTextConfiguration.appearEffects.Contains(effectSelected))
                    popTextConfiguration.appearEffects.Remove(effectSelected);
                break;
            case PoppingTextStage.Staying:
                if(popTextConfiguration.stayEffects.Contains(effectSelected))
                    popTextConfiguration.stayEffects.Remove(effectSelected);
                break;
            case PoppingTextStage.Disappearing:
                if(popTextConfiguration.disappearEffects.Contains(effectSelected))
                    popTextConfiguration.disappearEffects.Remove(effectSelected);
                break;
        }
    }



    public void createPoppingText()
    {
        poppingTextManager.createCustomPoppingTextWithConfig("Example message",popTextConfiguration,textSelected);
    }
}
