using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Create Item/Item")]
public class ItemData : ScriptableObject
{
    //Database ID of the item.
    public int id;
    public Sprite itemSprite;
    [HideInInspector] public float itemTextureAspectRatio;
    private void OnValidate()
    {
        if (itemSprite != null)
            itemTextureAspectRatio = (float)itemSprite.rect.width / itemSprite.rect.height;
        else
            Debug.LogWarning($"{name} texture not assigned.");

        id = Mathf.Clamp(id, 0, int.MaxValue);
        
    }
}
