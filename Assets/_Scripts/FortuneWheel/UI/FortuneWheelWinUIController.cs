using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheelWinUIController : MonoBehaviour, IRewardUI
{
    public Button collectButton;
    public Button continueButton;

    [SerializeField] private FortuneWheelGameManager fortuneWheelGameManager;
    private void OnValidate()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);
        if (buttons.Length != 2)
        {
            Debug.LogWarning($"Buttons are not properly set on {gameObject.name}");
        }
        else
        {
            collectButton = buttons[0];
            continueButton = buttons[1];
        }

        fortuneWheelGameManager = FindObjectOfType<FortuneWheelGameManager>();
        if (fortuneWheelGameManager == null)
            Debug.LogError($"Fortune Wheel Manager is not set on {gameObject.name}");
    }

    private void Start()
    {
        collectButton.onClick.AddListener(fortuneWheelGameManager.ShowRewards);
        continueButton.onClick.AddListener(fortuneWheelGameManager.NextZone);

        collectButton.onClick.AddListener(HideUI);
        continueButton.onClick.AddListener(HideUI);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

}
