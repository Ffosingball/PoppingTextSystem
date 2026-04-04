using System.Collections.Generic;
using UnityEngine;


//Add more types of effects here
//IMPORTANT! Some effects might be incompatible with each other!
public enum PoppingTextEffects
{
    Fade,
    Enlarge,
    Reduce,
    Appear,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    Blink,
    BlinkColor,
}


public enum PoppingTextStage
{
    Preparation,
    Appearing,
    Staying,
    Disappearing
}


//Configuration of the poppingText
public class PopTextConfiguration
{
    //Gameobject at which popping text should appear
    public GameObject appearAt=null;
    //Indicates whether the popping text will move with the gameobject at which it appeared
    //or it will be independent from it
    public bool moveWithGameObject=false;
    //How long text will appear
    public float appearTime=1f;
    //How long text will stay
    public float timeToStay=1f;
    //How long text will disappear
    public float disappearTime=1f;
    //All effects which will be applied to the text at appearing stage
    public List<PoppingTextEffects> appearEffects;
    //All effects which will be applied to the text at staying stage
    public List<PoppingTextEffects> stayEffects;
    //All effects which will be applied to the text at disappearing stage
    public List<PoppingTextEffects> disappearEffects;
    //Set only if you will use blinkColorEffect
    public Color blinkToColor;

    public PopTextConfiguration(GameObject appearAt)
    {
        this.appearAt = appearAt;
        appearEffects = new List<PoppingTextEffects>();
        stayEffects = new List<PoppingTextEffects>();
        disappearEffects = new List<PoppingTextEffects>();
    }
}