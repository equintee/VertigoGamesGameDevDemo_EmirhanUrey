using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class CardController : MonoBehaviour
{
    [SerializeField] private Image _itemDisplayImage;
    [SerializeField] private TextMeshProUGUI _itemQuantityText;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;

    private void Awake()
    {
        transform.DOScale(new Vector3(2, 3, 1), 0.5f).SetEase(Ease.OutQuart);
    }
    public void InitializeCard(ItemData itemData, int quantity)
    {
        _itemDisplayImage.sprite = itemData.itemSprite;
        _aspectRatioFitter.aspectRatio = itemData.itemTextureAspectRatio;
        _itemQuantityText.text = $"X{quantity}";
    }
}
