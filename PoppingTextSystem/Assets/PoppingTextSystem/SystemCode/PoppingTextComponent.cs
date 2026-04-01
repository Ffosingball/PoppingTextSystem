using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PoppingTextComponent : MonoBehaviour
{
    private PopTextConfiguration popTextConfiguration=null;
    private event Action<float> appearTransformations;
    private event Action<float> disappearTransformations;
    private event Action<float> stayTransformations;
    private float timePassed=0f;
    private TMP_Text poppingText; 

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



    private void Start()
    {
        gameObject.SetActive(false);
        poppingText = GetComponent<TMP_Text>();
        if(poppingText==null)
            Debug.Log("Popping text should have TMP text component!");
    }



    private void Update()
    {
        if(popTextConfiguration!=null)
        {
            timePassed+=Time.deltaTime;

            if(timePassed<popTextConfiguration.appearTime)
                appearTransformations?.Invoke(timePassed/popTextConfiguration.appearTime);
            else if(timePassed<popTextConfiguration.appearTime+popTextConfiguration.timeToStay)
                stayTransformations?.Invoke((timePassed-popTextConfiguration.appearTime)/popTextConfiguration.timeToStay);
            else
                disappearTransformations?.Invoke((timePassed-popTextConfiguration.appearTime-popTextConfiguration.timeToStay)/popTextConfiguration.disappearTime);
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
            case PoppingTextEffects.Blink:
                transform+=BlinkEffect;
                break;
        }
    }



    private void AppearTransparencyEffect(float percentage)
    {
        Color color = poppingText.color;
        color.a = Mathf.Lerp(0, 255, Mathf.Clamp01(percentage));
        poppingText.color = color;
    }



    private void FadeEffect(float percentage)
    {
        Color color = poppingText.color;
        color.a = Mathf.Lerp(255, 0, Mathf.Clamp01(percentage));
        poppingText.color = color;
    }



    private void EnlargeEffect(float percentage)
    {
        //yield return new WaitForSeconds(0.1f);
    }



    private void ReduceEffect(float percentage)
    {
        //yield return new WaitForSeconds(0.1f);
    }



    private void MoveUpEffect(float percentage)
    {
        //yield return new WaitForSeconds(0.1f);
    }



    private void MoveDownEffect(float percentage)
    {
        //yield return new WaitForSeconds(0.1f);
    }



    private void BlinkEffect(float percentage)
    {
        //yield return new WaitForSeconds(0.1f);
    }
}
