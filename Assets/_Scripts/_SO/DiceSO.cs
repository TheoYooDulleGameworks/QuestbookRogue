using UnityEngine;

[CreateAssetMenu(fileName = "DiceSO", menuName = "Scriptable Objects/Items/DiceSO")]
public class DiceSO : ScriptableObject
{
    [Header("Notes")]
    public string diceName;
    public string diceDescription;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite defaultHoverSprite;

    [Header("DieValues")]
    public Sprite value1;
    public Sprite value2;
    public Sprite value3;
    public Sprite value4;
    public Sprite value5;
    public Sprite value6;
}
