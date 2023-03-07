using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FortuneWheelManager : MonoBehaviour
{
    public FortuneWheelConfiguration[] fortuneWheelConfigurations;

    #region Editor Validation
    private void OnValidate()
    {
        CheckFortuneWheelConfigurationSettings();
    }

    private void CheckFortuneWheelConfigurationSettings()
    {
        //To check if configurations are not set.
        if (fortuneWheelConfigurations.Length == 0)
        {
            Debug.LogError("Fortune Wheel Apperance list is empty");
            return;
        }

        //To check if configurations has null field.
        bool isThereNullField = false;
        for(int i = 0; i < fortuneWheelConfigurations.Length; i++)
        {
            if (fortuneWheelConfigurations[i].fortuneWheelSettings == null)
            {
                Debug.LogError($"Fortune Wheel Settings are not assigned at Element {i} in FortuneWheelManager script.");
                isThereNullField = true;
            }

            if(fortuneWheelConfigurations[i].apperanceFrequency <= 0)
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
public struct FortuneWheelConfiguration
{
    public FortuneWheelSettings fortuneWheelSettings;
    public int apperanceFrequency;
}
