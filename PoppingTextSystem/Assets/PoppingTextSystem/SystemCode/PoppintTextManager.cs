using UnityEngine;
using TMPro;


public class PoppingTextManager : MonoBehaviour
{
    [SerializeField] private TextEffectsConfiguration textEffectsConfiguration;
    [SerializeField] private GameObject poppingTextUsual, poppingText2, poppingText3;

    
    //If you want different types of appearing text, in this manager you can add 
    //method with specific configuration for this purpose
    public void createPreconfigPopText1(string message, GameObject appearAt)
    {
        PopTextConfiguration popTextConfiguration = new PopTextConfiguration(appearAt);

        popTextConfiguration.appearEffects.Add(PoppingTextEffects.Enlarge);
        popTextConfiguration.appearEffects.Add(PoppingTextEffects.Appear);
        popTextConfiguration.appearEffects.Add(PoppingTextEffects.MoveUp);

        popTextConfiguration.stayEffects.Add(PoppingTextEffects.MoveUp);

        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.Reduce);
        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.Fade);
        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.MoveUp);

        popTextConfiguration.appearTime = 0.5f;
        popTextConfiguration.disappearTime = 0.5f;
        popTextConfiguration.timeToStay = 0.01f;

        GameObject newPoppingText = Instantiate(poppingTextUsual, appearAt.transform.position, Quaternion.identity);
        newPoppingText.GetComponent<PoppingTextComponent>().setTextEffectsConfiguration(textEffectsConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().setPopTextConfiguration(popTextConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().StartPoppingText();
        
        newPoppingText.GetComponent<TMP_Text>().text = message;
    }


    public void createPreconfigPopText2(string message, GameObject appearAt)
    {
        PopTextConfiguration popTextConfiguration = new PopTextConfiguration(appearAt);

        popTextConfiguration.appearEffects.Add(PoppingTextEffects.SecondaryReduce);
        popTextConfiguration.appearEffects.Add(PoppingTextEffects.Appear);
        popTextConfiguration.appearEffects.Add(PoppingTextEffects.MoveDown);

        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.SecondaryEnlarge);
        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.Fade);
        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.MoveUp);

        popTextConfiguration.appearTime = 0.4f;
        popTextConfiguration.disappearTime = 0.4f;
        popTextConfiguration.timeToStay = 2f;

        GameObject newPoppingText = Instantiate(poppingText2, appearAt.transform.position, Quaternion.identity);
        newPoppingText.GetComponent<PoppingTextComponent>().setTextEffectsConfiguration(textEffectsConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().setPopTextConfiguration(popTextConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().StartPoppingText();
        
        newPoppingText.GetComponent<TMP_Text>().text = message;
    }


    public void createPreconfigPopText3(string message, GameObject appearAt)
    {
        PopTextConfiguration popTextConfiguration = new PopTextConfiguration(appearAt);

        popTextConfiguration.appearEffects.Add(PoppingTextEffects.BlinkColor);
        popTextConfiguration.appearEffects.Add(PoppingTextEffects.MoveDown);

        popTextConfiguration.stayEffects.Add(PoppingTextEffects.BlinkColor);
        popTextConfiguration.stayEffects.Add(PoppingTextEffects.MoveUp);

        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.BlinkColor);
        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.Reduce);
        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.Fade);
        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.MoveUp);

        popTextConfiguration.appearTime = 1.4f;
        popTextConfiguration.disappearTime = 1.4f;
        popTextConfiguration.timeToStay = 1.4f;
        popTextConfiguration.blinkToColor = Color.aquamarine;

        GameObject newPoppingText = Instantiate(poppingText3, appearAt.transform.position, Quaternion.identity);
        newPoppingText.transform.SetParent(appearAt.transform);

        newPoppingText.GetComponent<PoppingTextComponent>().setTextEffectsConfiguration(textEffectsConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().setPopTextConfiguration(popTextConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().StartPoppingText();
        
        newPoppingText.GetComponent<TMP_Text>().text = message;
    }


    //Or you can create popTextConfiguration somewhere else and pass it here
    //Provided PopTextConfiguration has to be fully completed 
    //same applies for the popText it has to be valid (it should have rectTransform, tm_text and poppintTextComponent components)
    public void createCustomPoppingTextWithConfig(string message, PopTextConfiguration popTextConfiguration, GameObject popText)
    {
        Vector3 position = popTextConfiguration.appearAt.transform.position;
        position.z+=textEffectsConfiguration.changeZby;
        GameObject newPoppingText = Instantiate(popText, position, Quaternion.identity);

        if(popTextConfiguration.moveWithGameObject)
        {
            newPoppingText.transform.SetParent(popTextConfiguration.appearAt.transform);
        }

        newPoppingText.GetComponent<PoppingTextComponent>().setTextEffectsConfiguration(textEffectsConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().setPopTextConfiguration(popTextConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().StartPoppingText();
        
        newPoppingText.GetComponent<TMP_Text>().text = message;
    }
}