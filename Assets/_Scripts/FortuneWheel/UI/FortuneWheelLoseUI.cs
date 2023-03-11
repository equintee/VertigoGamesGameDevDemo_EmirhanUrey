using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheelLoseUI : MonoBehaviour, IRewardUI
{
    public Button restartButton;
    public Button reviveButton;

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
            restartButton = buttons[0];
            reviveButton = buttons[1];
        }

        fortuneWheelGameManager = FindObjectOfType<FortuneWheelGameManager>();
        if (fortuneWheelGameManager == null)
            Debug.LogError($"Fortune Wheel Manager is not set on {gameObject.name}");
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(fortuneWheelGameManager.RestartGame);
        reviveButton.onClick.AddListener(fortuneWheelGameManager.Revive);
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
