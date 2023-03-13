using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class FortuneWheelRewardScrollerController : MonoBehaviour, IRewardUI
{
    [SerializeField] private RectTransform _collectedCards;
    [SerializeField] private Button restartButton;
    private void OnValidate()
    {
        if (TryGetComponent(out restartButton))
            Debug.LogError($"Restart button is not assigned on {gameObject.name}");

        _collectedCards = GetComponent<ScrollRect>().content;
        restartButton = GetComponentInChildren<Button>();
    }

    public void InitalizeButtons()
    {
        restartButton.onClick.AddListener(FindObjectOfType<FortuneWheelGameManager>().RestartGame);
    }
    public void AddCardToScroller(RectTransform card)
    {
        card.SetParent(_collectedCards, false);
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
