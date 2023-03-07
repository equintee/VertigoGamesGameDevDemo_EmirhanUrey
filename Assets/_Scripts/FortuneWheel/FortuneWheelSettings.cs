using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(AspectRatioFitter))]
public class FortuneWheelSettings : MonoBehaviour
{
    private AspectRatioFitter _aspectRatioFitter;

    [Header("Fortune Wheel Settings")]
    public Transform[] fortuneWheelSlotTransforms;

    [Header("Card Prefab")]
    public GameObject cardPrefab;

    public UnityEvent specialEndOfSpinEvents;

    #region Editor Validation
    private void OnValidate()
    {
        _aspectRatioFitter = GetComponent<AspectRatioFitter>();
        _aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        Texture2D fortuneWheelTexture = GetComponent<Image>().sprite.texture;
        _aspectRatioFitter.aspectRatio = fortuneWheelTexture.width / (float)fortuneWheelTexture.height;
    }
    #endregion
}
