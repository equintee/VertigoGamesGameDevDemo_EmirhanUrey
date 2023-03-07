using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FortuneWheelManager : MonoBehaviour
{
    [Header("Base Game Settings")]
    public GameObject baseGameFortuneWheelPrefab;
    public FortuneWheelItem[] baseGameFortuneWheelItems;

    [Header("Bonus Game Settings")]
    public BonusFortuneWheelConfiguration[] bonusFortuneWheelConfigurations;

    #region Editor Validation
    private void OnValidate()
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
        
    }

    #endregion
}


[System.Serializable]
public struct BonusFortuneWheelConfiguration
{
    public GameObject bonusFortuneWheelPrefab;
    public FortuneWheelItem[] bonusFortuneWheelItems;
    public int apperanceFrequency;
}
