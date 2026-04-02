using UnityEngine;

[CreateAssetMenu(fileName = "TextEffectsConfiguration", menuName = "Scriptable Objects/TextEffectsConfiguration")]
public class TextEffectsConfiguration : ScriptableObject
{
    public float changeSizeBy = 10f;
    public float moveBy = 10f;
    public float blinkPeriod = 0.5f;
    public float blinkRange = 50f;
}
