using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "FortuneWheel/FortuneWheelSettings", order = 1)]
public class FortuneWheelSettings : ScriptableObject
{
    /*[Header("Textures")]
    public Texture2D wheelSprite;
    public Texture2D wheelStopperSprite;
    public Texture2D spinButtonSprite;*/

    [Header("Fortune Wheel Settings")]
    public GameObject fortuneWheelPrefab;
    public Transform[] fortuneWheelSlotTransforms;

    [Header("Card Prefab")]
    public GameObject cardPrefab;

    [Header("Items")]
    public FortuneWheelItem[] fortuneWheelItems;

    public UnityEvent specialSpinEvents;

    private void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(this);
        name = Path.GetFileNameWithoutExtension(assetPath);
    }

}
