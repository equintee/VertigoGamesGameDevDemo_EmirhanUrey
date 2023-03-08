using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fortune Wheel Zone Configuration", menuName = "FortuneWheel/Fortune Wheel Zone Configuration", order = 1)]
public class FortuneWheelZoneConfiguration : ScriptableObject
{
    public GameObject fortuneWheelPrefab;
    public GameObject rewardCardPrefab;
    public FortuneWheelZoneRewardConfiguration[] zoneRewards;


    #region Editor Validation
    private void OnValidate()
    {
        if (fortuneWheelPrefab == null)
            Debug.LogError($"Fortune Wheel Prefab is not assigned on {name}.");

        if (rewardCardPrefab == null)
            Debug.LogError($"Reward Card Prefab is not assigned on {name}.");

        for(int i = 0; i < zoneRewards.Length; i++)
        {
            FortuneWheelZoneRewardConfiguration reward = zoneRewards[i];
            if (reward.itemTexture == null)
                Debug.LogError($"Item texture not assigned on Element {i} on {name}");
            else
                reward.itemTextureAspectRatio = (float)reward.itemTexture.width / reward.itemTexture.height;

            reward.minimumAmount = Mathf.Min(reward.minimumAmount, reward.maximumAmount);
            reward.maximumAmount = Mathf.Max(reward.minimumAmount, reward.maximumAmount);

            zoneRewards[i] = reward;
        }
    }
    #endregion
}

[System.Serializable]
public struct FortuneWheelZoneRewardConfiguration
{
    public Texture2D itemTexture;
    [HideInInspector] public float itemTextureAspectRatio; //Avoiding to calculate aspect ratio in runtime. Gets assigned on OnValidate().
    [Range(0, int.MaxValue)] public int minimumAmount;
    [Range(0, int.MaxValue)] public int maximumAmount;
}
