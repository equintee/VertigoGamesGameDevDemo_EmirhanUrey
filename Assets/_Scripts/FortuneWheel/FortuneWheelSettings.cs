using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

[RequireComponent(typeof(AspectRatioFitter))]
public class FortuneWheelSettings : MonoBehaviour
{
    [Header("Fortune Wheel Settings")]
    [Tooltip("Assign slot transforms of fortune wheel.")]
    public Transform[] fortuneWheelSlotTransforms;

    [Header("Card Prefab")]
    [Tooltip("Assign card prefab made for fortune wheel.")]
    public GameObject cardPrefab;

    [Tooltip("Assign special end of spin events for fortune wheel.")]
    public UnityEvent specialEndOfSpinEvents;

    private AspectRatioFitter _aspectRatioFitter;

    private void Awake()
    {
        
    }
    #region Editor Validation
    private void OnValidate()
    {
        SetFortuneWheelAspectRatio();
    }

    private void SetFortuneWheelAspectRatio()
    {
        _aspectRatioFitter = GetComponent<AspectRatioFitter>();
        _aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        Texture2D fortuneWheelTexture = GetComponent<Image>().sprite.texture;
        _aspectRatioFitter.aspectRatio = fortuneWheelTexture.width / (float)fortuneWheelTexture.height;
    }
    #endregion
}
