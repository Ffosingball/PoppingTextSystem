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
    public GameObject appearAt=null;
    public bool moveWithGameObject=false;
    public float appearTime=1f;
    public float timeToStay=1f;
    public float disappearTime=1f;
    public List<PoppingTextEffects> appearEffects;
    public List<PoppingTextEffects> stayEffects;
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