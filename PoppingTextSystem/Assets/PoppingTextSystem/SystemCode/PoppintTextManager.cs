using UnityEngine;
using TMPro;


public class PoppingTextManager : MonoBehaviour
{
    [SerializeField] private TextEffectsConfiguration textEffectsConfiguration;
    [SerializeField] private GameObject poppingTextUsual;
    [SerializeField] private GameObject poppingTextSpecial1;
    [SerializeField] private GameObject poppingTextSpecial2;

    
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
    public void createPoppingTextWithConfig(string message, PopTextConfiguration popTextConfiguration)
    {
        GameObject newPoppingText = Instantiate(poppingTextUsual, popTextConfiguration.appearAt.transform.position, Quaternion.identity);

        if(popTextConfiguration.moveWithGameObject)
        {
            newPoppingText.transform.SetParent(popTextConfiguration.appearAt.transform);
            newPoppingText.GetComponent<RectTransform>().position = new Vector3(0,0,0);
        }

        newPoppingText.GetComponent<PoppingTextComponent>().setPopTextConfiguration(popTextConfiguration);
        newPoppingText.GetComponent<PoppingTextComponent>().setTextEffectsConfiguration(textEffectsConfiguration);
        newPoppingText.GetComponent<TMP_Text>().text = message;
    }
}