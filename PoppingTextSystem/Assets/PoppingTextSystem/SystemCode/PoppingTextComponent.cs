using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PoppingTextComponent : MonoBehaviour
{
    [SerializeField] private PopTextConfiguration popTextConfiguration=null;
    [SerializeField] private TextEffectsConfiguration textEffectsConfiguration;
    private event Action<float> appearTransformations;
    private event Action<float> disappearTransformations;
    private event Action<float> stayTransformations;
    private float timePassed=0f;
    private TMP_Text poppingText; 
    private RectTransform rectTransform; 
    private Vector2 originalSize;
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
        if(this.popTextConfiguration==null)
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
        gameObject.SetActive(false);
        poppingText = GetComponent<TMP_Text>();
        if(poppingText==null)
            Debug.Log("Popping text should have TMP text component!");

        rectTransform = GetComponent<RectTransform>();
        if(rectTransform==null)
            Debug.Log("Popping text should have RectTransform component!");
    }



    private void Update()
    {
        if(popTextConfiguration!=null)
        {
            timePassed+=Time.deltaTime;

            if(timePassed<popTextConfiguration.appearTime)
            {
                if(poppingTextStage==PoppingTextStage.Preparation)
                {
                    originalColor = poppingText.color;
                    originalSize = rectTransform.sizeDelta;
                    originalPosition = rectTransform.position;
                    poppingTextStage=PoppingTextStage.Appearing;
                }

                appearTransformations?.Invoke(timePassed/popTextConfiguration.appearTime);
            }
            else if(timePassed<popTextConfiguration.appearTime+popTextConfiguration.timeToStay)
            {
                if(poppingTextStage==PoppingTextStage.Appearing)
                {
                    originalColor = poppingText.color;
                    originalSize = rectTransform.sizeDelta;
                    originalPosition = rectTransform.position;
                    poppingTextStage=PoppingTextStage.Staying;
                }

                stayTransformations?.Invoke((timePassed-popTextConfiguration.appearTime)/popTextConfiguration.timeToStay);
            }
            else
            {
                if(poppingTextStage==PoppingTextStage.Staying)
                {
                    originalColor = poppingText.color;
                    originalSize = rectTransform.sizeDelta;
                    originalPosition = rectTransform.position;
                    poppingTextStage=PoppingTextStage.Disappearing;
                }

                disappearTransformations?.Invoke((timePassed-popTextConfiguration.appearTime-popTextConfiguration.timeToStay)/popTextConfiguration.disappearTime);
            }
        }
    }



    private void StartPoppingText()
    {
        gameObject.SetActive(true);

        foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.appearEffects)
        {
            addEffect(appearTransformations, poppingTextEffects);
        }

        foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.stayEffects)
        {
            addEffect(stayTransformations, poppingTextEffects);
        }

        foreach(PoppingTextEffects poppingTextEffects in popTextConfiguration.disappearEffects)
        {
            addEffect(disappearTransformations, poppingTextEffects);
        }
    }



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
        rectTransform.sizeDelta = originalSize * Mathf.Lerp(1, textEffectsConfiguration.changeSizeBy, Mathf.Clamp01(percentage));
    }



    private void ReduceEffect(float percentage)
    {
        rectTransform.sizeDelta = originalSize * Mathf.Lerp(1, 1/textEffectsConfiguration.changeSizeBy, Mathf.Clamp01(percentage));
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
