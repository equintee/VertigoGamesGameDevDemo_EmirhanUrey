using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayPlayerCash : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerCashText;
    [SerializeField] private FortuneWheelGameManager fortuneWheelGameManager;
    private void OnValidate()
    {
        playerCashText = GetComponent<TextMeshProUGUI>();
        fortuneWheelGameManager = FindObjectOfType<FortuneWheelGameManager>();
    }
    private void OnEnable()
    {
        fortuneWheelGameManager.OnPlayerCashUpdate.AddListener(UpdatePlayerCashText);
    }

    private void OnDisable()
    {
        fortuneWheelGameManager.OnPlayerCashUpdate.RemoveListener(UpdatePlayerCashText);
    }


    private void UpdatePlayerCashText(int value)
    {
        playerCashText.text = value.ToString();
    }
}
