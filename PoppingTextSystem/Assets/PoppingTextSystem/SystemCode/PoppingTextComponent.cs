using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PoppingTextComponent : MonoBehaviour
{
    //Configurations of the pop text and effects
    [SerializeField] private PopTextConfiguration popTextConfiguration=null;
    [SerializeField] private TextEffectsConfiguration textEffectsConfiguration;
    //They will store references to all effects which should be applied at each stage
    private event Action<float> appearTransformations;
    private event Action<float> disappearTransformations;
    private event Action<float> stayTransformations;
    //Time passed since the creation of this pop text
    private float timePassed=0f;
    //References to its components
    private TMP_Text poppingText; 
    private RectTransform rectTransform; 
    //Storing its original properties
    private float originalFont;
    private Color originalColor;
    private Vector3 originalPosition;
    private PoppingTextStage poppingTextStage = PoppingTextStage.Preparation;

    //Getters and setters
    public PopTextConfiguration getPopTextConfiguration()
    {
        return popTextConfiguration;
    }

    public void setPopTextConfiguration(PopTextConfiguration popTextConfiguration)
    {
        //Check that this is a first text configuration and its stage is preparation then 
        //start popping text
        if(this.popTextConfiguration==null && poppingTextStage==PoppingTextStage.Preparation)
        {
            this.popTextConfiguration = popTextConfiguration;
            StartPoppingText();
        }
    }

    public void setTextEffectsConfiguration(TextEffectsConfiguration textEffectsConfiguration)
    {
        this.textEffectsConfiguration = textEffectsConfiguration;
    }



    private void Start()
    {
        //Get components
        gameObject.SetActive(false);
        poppingText = GetComponent<TMP_Text>();
        if(poppingText==null)
            Debug.Log("Popping text should have TMP text component!");

        rectTransform = GetComponent<RectTransform>();
        if(rectTransform==null)
            Debug.Log("Popping text should have RectTransform component!");

        //Check that this is a first text configuration and its stage is preparation then 
        //start popping text
        if(popTextConfiguration!=null && poppingTextStage==PoppingTextStage.Preparation)
            StartPoppingText();
    }



    private void Update()
    {
        //If its configuration is set then apply effects
        if(popTextConfiguration!=null)
        {
            timePassed+=Time.deltaTime;

            //Check which effects to apply by looking how much time has passed
            if(timePassed<popTextConfiguration.appearTime)
            {
                appearTransformations?.Invoke(timePassed/popTextConfiguration.appearTime);
            }
            else if(timePassed<popTextConfiguration.appearTime+popTextConfiguration.timeToStay)
            {
                //Set new original properties when text transitions to new stage
                if(poppingTextStage==PoppingTextStage.Appearing)
                {
                    originalColor = poppingText.color;
                    originalFont = poppingText.fontSize;
                    originalPosition = rectTransform.position;
                    poppingTextStage=PoppingTextStage.Staying;
                }

                stayTransformations?.Invoke((timePassed-popTextConfiguration.appearTime)/popTextConfiguration.timeToStay);
            }
            else if(timePassed<popTextConfiguration.appearTime+popTextConfiguration.timeToStay+popTextConfiguration.disappearTime)
            {
                //Set new original properties when text transitions to new stage
                if(poppingTextStage==PoppingTextStage.Staying)
                {
                    originalColor = poppingText.color;
                    originalFont = poppingText.fontSize;
                    originalPosition = rectTransform.position;
                    poppingTextStage=PoppingTextStage.Disappearing;
                }

                disappearTransformations?.Invoke((timePassed-popTextConfiguration.appearTime-popTextConfiguration.timeToStay)/popTextConfiguration.disappearTime);
            }
            else//When all time is passed then destroy itself
                Destroy(gameObject);
        }
    }



    //This method initializes popping text effects, properties and shows it
    private void StartPoppingText()
    {
        gameObject.SetActive(true);

        foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.appearEffects)
        {
            addEffect(appearTransformations, poppingTextEffects);
            changeText(poppingTextEffects);
        }

        foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.stayEffects)
        {
            addEffect(stayTransformations, poppingTextEffects);
        }

        foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.disappearEffects)
        {
            addEffect(disappearTransformations, poppingTextEffects);
        }

        originalColor = poppingText.color;
        originalFont = poppingText.fontSize;
        originalPosition = rectTransform.position;
        poppingTextStage=PoppingTextStage.Appearing;
    }



    //This method adds correct effect to the event action by its enum type
    private void addEffect(Action<float> transform, PoppingTextEffects poppingTextEffects)
    {
        switch(poppingTextEffects)
        {
            case PoppingTextEffects.Fade:
                transform+=FadeEffect;
                break;
            case PoppingTextEffects.Enlarge:
                transform+=EnlargeEffect;
                break;
            case PoppingTextEffects.Reduce:
                transform+=ReduceEffect;
                break;
            case PoppingTextEffects.Appear:
                transform+=AppearTransparencyEffect;
                break;
            case PoppingTextEffects.MoveUp:
                transform+=MoveUpEffect;
                break;
            case PoppingTextEffects.MoveDown:
                transform+=MoveDownEffect;
                break;
            case PoppingTextEffects.MoveLeft:
                transform+=MoveLeftEffect;
                break;
            case PoppingTextEffects.MoveRight:
                transform+=MoveRightEffect;
                break;
            case PoppingTextEffects.Blink:
                transform+=BlinkEffect;
                break;
            case PoppingTextEffects.BlinkColor:
                transform+=BlinkColorEffect;
                break;
        }
    }



    //This method changes text properties, so appearing effects will make its appear nice
    private void changeText(PoppingTextEffects poppingTextEffects)
    {
        Vector3 position = rectTransform.position;
        switch(poppingTextEffects)
        {
            case PoppingTextEffects.Enlarge:
                poppingText.fontSize*=textEffectsConfiguration.changeSizeBy;
                break;
            case PoppingTextEffects.Reduce:
                poppingText.fontSize/=textEffectsConfiguration.changeSizeBy;
                break;
            case PoppingTextEffects.Appear:
                Color color = poppingText.color;
                color.a = 0;
                poppingText.color = color;
                break;
            case PoppingTextEffects.MoveUp:
                position.y -= textEffectsConfiguration.moveBy;
                rectTransform.position = position;
                break;
            case PoppingTextEffects.MoveDown:
                position.y += textEffectsConfiguration.moveBy;
                rectTransform.position = position;
                break;
            case PoppingTextEffects.MoveLeft:
                position.x += textEffectsConfiguration.moveBy;
                rectTransform.position = position;
                break;
            case PoppingTextEffects.MoveRight:
                position.y -= textEffectsConfiguration.moveBy;
                rectTransform.position = position;
                break;
        }
    }


    //below are all effects methods
    private void AppearTransparencyEffect(float percentage)
    {
        Color color = poppingText.color;
        color.a = Mathf.Lerp(originalColor.a, 255, Mathf.Clamp01(percentage));
        poppingText.color = color;
    }



    private void FadeEffect(float percentage)
    {
        Color color = poppingText.color;
        color.a = Mathf.Lerp(originalColor.a, 0, Mathf.Clamp01(percentage));
        poppingText.color = color;
    }



    private void EnlargeEffect(float percentage)
    {
        poppingText.fontSize = originalFont * Mathf.Lerp(1, textEffectsConfiguration.changeSizeBy, Mathf.Clamp01(percentage));
    }



    private void ReduceEffect(float percentage)
    {
        poppingText.fontSize = originalFont * Mathf.Lerp(1, 1/textEffectsConfiguration.changeSizeBy, Mathf.Clamp01(percentage));
    }



    private void MoveUpEffect(float percentage)
    {
        Vector3 currentPosition = originalPosition;
        currentPosition.y += Mathf.Lerp(0, textEffectsConfiguration.moveBy, Mathf.Clamp01(percentage));
        rectTransform.position = currentPosition;
    }



    private void MoveDownEffect(float percentage)
    {
        Vector3 currentPosition = originalPosition;
        currentPosition.y -= Mathf.Lerp(0, textEffectsConfiguration.moveBy, Mathf.Clamp01(percentage));
        rectTransform.position = currentPosition;
    }


    private void MoveLeftEffect(float percentage)
    {
        Vector3 currentPosition = originalPosition;
        currentPosition.x -= Mathf.Lerp(0, textEffectsConfiguration.moveBy, Mathf.Clamp01(percentage));
        rectTransform.position = currentPosition;
    }



    private void MoveRightEffect(float percentage)
    {
        Vector3 currentPosition = originalPosition;
        currentPosition.x += Mathf.Lerp(0, textEffectsConfiguration.moveBy, Mathf.Clamp01(percentage));
        rectTransform.position = currentPosition;
    }


    //Even increase brightness, odd decrease brightness
    private void BlinkEffect(float percentage)
    {
        float timePassedInStage=0f;
        switch(poppingTextStage)
        {
            case PoppingTextStage.Appearing:
                timePassedInStage = timePassed;
                break;
            case PoppingTextStage.Staying:
                timePassedInStage = timePassed-popTextConfiguration.appearTime;
                break;
            case PoppingTextStage.Disappearing:
                timePassedInStage = timePassed-popTextConfiguration.appearTime-popTextConfiguration.timeToStay;
                break;
        }

        int increaseOrDecrease = (int)(timePassedInStage/textEffectsConfiguration.blinkPeriod);
        Color color = poppingText.color;

        if(increaseOrDecrease%2==0)
            color.a = Mathf.Lerp(Mathf.Max(originalColor.a-textEffectsConfiguration.blinkRange,0), Mathf.Min(originalColor.a+textEffectsConfiguration.blinkRange,255), Mathf.Clamp01(percentage));
        else
            color.a = Mathf.Lerp(Mathf.Min(originalColor.a+textEffectsConfiguration.blinkRange,255), Mathf.Max(originalColor.a-textEffectsConfiguration.blinkRange,0), Mathf.Clamp01(percentage));

        poppingText.color = color;
    }



    private void BlinkColorEffect(float percentage)
    {
        float timePassedInStage=0f;
        switch(poppingTextStage)
        {
            case PoppingTextStage.Appearing:
                timePassedInStage = timePassed;
                break;
            case PoppingTextStage.Staying:
                timePassedInStage = timePassed-popTextConfiguration.appearTime;
                break;
            case PoppingTextStage.Disappearing:
                timePassedInStage = timePassed-popTextConfiguration.appearTime-popTextConfiguration.timeToStay;
                break;
        }

        int increaseOrDecrease = (int)(timePassedInStage/textEffectsConfiguration.blinkPeriod);
        Color color = poppingText.color;

        if(increaseOrDecrease%2==0)
        {
            color.r = Mathf.Lerp(originalColor.r, popTextConfiguration.blinkToColor.r, Mathf.Clamp01(percentage));
            color.g = Mathf.Lerp(originalColor.g, popTextConfiguration.blinkToColor.g, Mathf.Clamp01(percentage));
            color.b = Mathf.Lerp(originalColor.b, popTextConfiguration.blinkToColor.b, Mathf.Clamp01(percentage));
        }
        else
        {
            color.r = Mathf.Lerp(popTextConfiguration.blinkToColor.r, originalColor.r, Mathf.Clamp01(percentage));
            color.g = Mathf.Lerp(popTextConfiguration.blinkToColor.g, originalColor.g, Mathf.Clamp01(percentage));
            color.b = Mathf.Lerp(popTextConfiguration.blinkToColor.b, originalColor.b, Mathf.Clamp01(percentage));
        }

        poppingText.color = color;
    }
}
