using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayPlayerCash : MonoBehaviour
{
    private TextMeshProUGUI playerCashText;
    private void OnValidate()
    {
        playerCashText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        GameManager.Instance.OnPlayerCashUpdate.AddListener(UpdatePlayerCashText);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerCashUpdate.RemoveListener(UpdatePlayerCashText);
    }

    private void UpdatePlayerCashText(int value)
    {
        playerCashText.text = value.ToString();
    }
}
