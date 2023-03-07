using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FortuneWheel/Fortune Wheel Item", order = 1)]
public class FortuneWheelItem : ScriptableObject
{
    public Texture2D itemSprite;
    [HideInInspector] public float itemSpriteAspectRatio;

    private void OnValidate()
    {
        if (itemSprite == null)
        {
            Debug.LogError($"{this.name} sprite not assigned.");
            return;
        }
            
        itemSpriteAspectRatio = (float)itemSprite.width / (float)itemSprite.height;
    }
}
