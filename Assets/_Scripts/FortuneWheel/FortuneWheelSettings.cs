using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;

//[RequireComponent(typeof(AspectRatioFitter))]
public class FortuneWheelSettings : MonoBehaviour
{

    [Header("Fortune Wheel Settings")]
    [Tooltip("Assign base of fortune wheel.")]
    public GameObject fortuneWheelBase;
    public GameObject fortuneWheelSpinButton;

    [Tooltip("Assign slot transforms of fortune wheel.")]
    public Transform[] fortuneWheelSlotTransforms;

    [Header("Card Prefab")]
    [Tooltip("Assign card prefab made for fortune wheel.")]
    public GameObject cardPrefab;

    [Tooltip("Assign spin events.")]
    public UnityEvent spinEvents;

    private AspectRatioFitter _aspectRatioFitter;

    #region Editor Validation
    private void OnValidate()
    {
        //SetFortuneWheelAspectRatio();
    }

    private void SetFortuneWheelAspectRatio()
    {
        _aspectRatioFitter = GetComponent<AspectRatioFitter>();
        _aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        Texture2D fortuneWheelTexture = GetComponent<Image>().sprite.texture;
        _aspectRatioFitter.aspectRatio = fortuneWheelTexture.width / (float)fortuneWheelTexture.height;
    }
    #endregion

    private async void OnEnable()
    {

        //TODO: Make separate method or scriptable object for animation.
        fortuneWheelBase.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        fortuneWheelSpinButton.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        List<Task> tweeners = new List<Task>();

        fortuneWheelBase.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack).AsyncWaitForCompletion();
        fortuneWheelSpinButton.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack).AsyncWaitForCompletion();

        //TODO: Change ease in first rotation animation.
        await fortuneWheelBase.transform.DORotate(new Vector3(0, 0, 30f), 0.8f).SetEase(Ease.InBack).AsyncWaitForCompletion();
        await fortuneWheelBase.transform.DORotate(Vector3.zero, 1.5f).SetEase(Ease.OutElastic).AsyncWaitForCompletion();

    }

    public void SpinWheel()
    {

    }
    
    //TODO: Generate random items on wheel, rewards struct and rewarding event on fortune wheel manager.
}
