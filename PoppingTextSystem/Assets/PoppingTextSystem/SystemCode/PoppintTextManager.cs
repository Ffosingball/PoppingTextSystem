using UnityEngine;
using TMPro;


public class PoppingTextManager : MonoBehaviour
{
    [SerializeField] private TextEffectsConfiguration textEffectsConfiguration;
    [SerializeField] private GameObject poppingTextUsual;

    
    //If you want different types of appearing text, in this manager you can add 
    //method with specific configuration for this purpose
    public void createUsualPoppingText(string message, GameObject appearAt)
    {
        PopTextConfiguration popTextConfiguration = new PopTextConfiguration(appearAt);

        popTextConfiguration.appearEffects.Add(PoppingTextEffects.Enlarge);
        popTextConfiguration.appearEffects.Add(PoppingTextEffects.Appear);

        popTextConfiguration.stayEffects.Add(PoppingTextEffects.MoveUp);

        popTextConfiguration.disappearEffects.Add(PoppingTextEffects.Reduce);
        popTextConfiguration.appearEffects.Add(PoppingTextEffects.Fade);

        GameObject newPoppingText = Instantiate(poppingTextUsual, appearAt.transform.position, Quaternion.identity);
        newPoppingText.GetComponent<PoppingTextComponent>().setPopTextConfiguration(popTextConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().setTextEffectsConfiguration(textEffectsConfiguration);
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
            //newPoppingText.GetComponent<RectTransform>().position = new Vector3(0,0,0);
        }

        newPoppingText.GetComponent<PoppingTextComponent>().setTextEffectsConfiguration(textEffectsConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().setPopTextConfiguration(popTextConfiguration);
        newPoppingText.GetComponent<TMP_Text>().text = message;
    }
}