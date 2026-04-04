using UnityEngine;

[CreateAssetMenu(fileName = "TextEffectsConfiguration", menuName = "Scriptable Objects/TextEffectsConfiguration")]
public class TextEffectsConfiguration : ScriptableObject
{
    [Tooltip("How much text should change its size by enlarge or reduce effect. Cannot be less than 1!")]
    public float changeSizeBy = 10f;
    [Tooltip("How fat text will be move by any move effect!")]
    public float moveBy = 10f;
    [Tooltip("Blink time of the blinking effect")]
    public float blinkPeriod = 0.5f;
    [Tooltip("How much to change transparency during blinking effect")]
    public float blinkRange = 50f;
    [Tooltip("If your game is 2d then set this value either + or - 0.01f, if 3d set it to 0")]
    public float changeZby = -0.001f;
}
