using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fortune Wheel Zone Configuration", menuName = "Fortune Wheel/Fortune Wheel Zone Configuration", order = 1)]
public class FortuneWheelZoneConfiguration : ScriptableObject
{
    public GameObject fortuneWheelPrefab;
    public FortuneWheelReward[] zoneRewards;
    public GameObject rewardCardPrefab;

    [Range(0, 100)] public int bombZoneCount;


    #region Editor Validation
    private void OnValidate()
    {
        if (fortuneWheelPrefab == null)
            Debug.LogError($"Fortune Wheel Prefab is not assigned on {name}.");
        else
        {
            FortuneWheelManager fortuneWheelSettings = fortuneWheelPrefab.GetComponent<FortuneWheelManager>();
            bombZoneCount = Mathf.Clamp(bombZoneCount, 0, fortuneWheelSettings.fortuneWheelSlotTransforms.Length);
        }

        //TODO: Error shown if bombZone is greater than 0 and sprite is not assigned.
    }
    #endregion
}
