using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private void Start()
    {
        restartButton.onClick.AddListener(fortuneWheelGameManager.RestartGame);
        reviveButton.onClick.AddListener(fortuneWheelGameManager.Revive);

        restartButton.onClick.AddListener(HideUI);
        reviveButton.onClick.AddListener(HideUI);
    }
    public void HideUI()
    {
        reviveButton.GetComponentInChildren<TextMeshProUGUI>().text = "REVIVE 10X";
        gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        if(GameManager.Instance.PlayerCash <= 0)
        {
            reviveButton.GetComponentInChildren<TextMeshProUGUI>().text = "GET 100X";
        }
        gameObject.SetActive(true);
    }


}
