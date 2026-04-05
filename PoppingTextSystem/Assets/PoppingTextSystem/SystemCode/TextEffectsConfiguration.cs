using UnityEngine;

[CreateAssetMenu(fileName = "TextEffectsConfiguration", menuName = "Scriptable Objects/TextEffectsConfiguration")]
public class TextEffectsConfiguration : ScriptableObject
{
    [Tooltip("How much text should change its size by enlarge or reduce effect. Cannot be less than 1!")]
    public float changeSizeBy = 10f;
    [Tooltip("How much text should change its size by secondary enlarge effect. Cannot be less than 1!")]
    public float secondarySizeChangeBy = 2f;
    [Tooltip("How fat text will be move by any move effect!")]
    public float moveBySpeed = 1.5f;
    [Tooltip("Blink time of the blinking effect")]
    public float blinkPeriod = 0.5f;
    [Tooltip("If your game is 2d then set this value either + or - 0.01f, if 3d set it to 0")]
    public float changeZby = -0.001f;
}
