using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FortuneWheelGameManager : MonoBehaviour
{
    [Header("Base Game Settings")]
    public GameObject baseGameFortuneWheelPrefab;
    public FortuneWheelZoneConfiguration baseGameFortuneWheelConfiguration;

    [Header("Bonus Game Settings")]
    public SpecialZoneSettings[] specialZoneSettings;

    [Header("Bomb Sprite")]
    public FortuneWheelReward bomb;

    private RectTransform _rectTransform;
    private int _currentZone = 1;
    private FortuneWheelManager _currentFortuneWheelSettings;

    #region Editor Validation
    /* private void OnValidate()
     {
         //CheckFortuneWheelConfigurationSettings();
     }

     private void CheckFortuneWheelConfigurationSettings()
     {
         //To check if configurations are not set.
         if (bonusFortuneWheelConfigurations.Length == 0)
         {
             Debug.LogError("Fortune Wheel Apperance list is empty");
             return;
         }

         //To check if configurations has null field.
         bool isThereNullField = false;
         for(int i = 0; i < bonusFortuneWheelConfigurations.Length; i++)
         {
             if (bonusFortuneWheelConfigurations[i].bonusFortuneWheelPrefab == null)
             {
                 Debug.LogError($"Fortune Wheel Prefab are not assigned at Element {i} in FortuneWheelManager script.");
                 isThereNullField = true;
             }

             if(bonusFortuneWheelConfigurations[i].apperanceFrequency <= 0)
             {
                 Debug.LogError($"Apperance frequency has to be greater than 0 at Element {i} in FortuneWheelManager script.");
                 isThereNullField = true;
             }
         }

         if (isThereNullField)
             return;

     }*/

    #endregion
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>(); //TODO: Cache in editor.
        InitalizeFortuneWheel();
    }
    public void InitalizeFortuneWheel()
    {
        FortuneWheelZoneConfiguration fortuneWheelZoneConfiguration = GetCurrentZone();
        FortuneWheelManager fortuneWheelManager = Instantiate(fortuneWheelZoneConfiguration.fortuneWheelPrefab, _rectTransform.position, Quaternion.identity, transform).GetComponent<FortuneWheelManager>();
        fortuneWheelManager.SetItemsOnWheel(fortuneWheelZoneConfiguration, bomb);

        fortuneWheelManager.endOfSpinEvent.AddListener(GiveReward);

    }
    public FortuneWheelZoneConfiguration GetCurrentZone()
    {
        if (specialZoneSettings.Length == 0)
            return baseGameFortuneWheelConfiguration;

        for(int i = specialZoneSettings.Length - 1; i > -1; i--)
        {
            if (_currentZone % specialZoneSettings[i].apperanceFrequency == 0)
                return specialZoneSettings[i].specialZoneConfiguration;
        }

        return baseGameFortuneWheelConfiguration;
    }

    private void GiveReward(FortuneWheelReward reward)
    {
        if(reward.itemData.id == bomb.itemData.id)
        {
            //show lose UI
            Debug.Log("bomb poof");
        }
        else
        {
            Debug.Log($"{reward.quantity} {reward.itemData.name}");
        }
    }
}


[System.Serializable]
public struct SpecialZoneSettings
{
    public FortuneWheelZoneConfiguration specialZoneConfiguration;
    public int apperanceFrequency;
}
