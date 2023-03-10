using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using System.Linq;
using TMPro;

//[RequireComponent(typeof(AspectRatioFitter))]
public class FortuneWheelManager : MonoBehaviour
{

    [Header("Fortune Wheel Settings")]
    [Tooltip("Gameobject of fortune wheels base texture.")]
    [SerializeField] private GameObject _fortuneWheelBase;
    [Tooltip("Gameobject of spin button on fortune wheel.")]
    [SerializeField] private GameObject _fortuneWheelSpinButton;

    [Tooltip("Slots of fortune wheel.")]
    public Transform[] fortuneWheelSlotTransforms;

    [Header("Card Prefab")]
    [Tooltip("Reward card made for fortune wheel.")]
    public GameObject cardPrefab;


    [HideInInspector] public UnityEvent<FortuneWheelReward> endOfSpinEvent;

    private FortuneWheelReward[] _slotRewards;
    private int _slotCount => fortuneWheelSlotTransforms.Length;

    #region Editor Validation
    private void OnValidate()
    {
        _fortuneWheelSpinButton = GetComponentInChildren<Button>().gameObject;
    }

    #endregion

    private async void Awake()
    {

        _slotRewards = new FortuneWheelReward[_slotCount];

        //TODO: Make separate method or scriptable object for animation.
        _fortuneWheelBase.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        _fortuneWheelSpinButton.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        _fortuneWheelBase.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack).AsyncWaitForCompletion();
        _fortuneWheelSpinButton.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBack).AsyncWaitForCompletion();

        //TODO: Change ease in first rotation animation.
        await _fortuneWheelBase.transform.DORotate(new Vector3(0, 0, 30f), 0.8f).SetEase(Ease.InBack).AsyncWaitForCompletion();
        await _fortuneWheelBase.transform.DORotate(Vector3.zero, 1.5f).SetEase(Ease.OutElastic).AsyncWaitForCompletion();

        //To ensure wheel gets intracted when initial animations complete.
        _fortuneWheelSpinButton.GetComponent<Button>().onClick.AddListener(SpinWheel);

    }

    public async void SpinWheel()
    {
        _fortuneWheelSpinButton.GetComponent<Button>().interactable = false;

        int pickedAngle = Random.Range(0, 360) + 5 * 360;
        await _fortuneWheelBase.transform.DORotate(new Vector3(0, 0, pickedAngle), 5f, RotateMode.FastBeyond360).AsyncWaitForCompletion();

        int closestSlotIndex = GetClosestSlotIndex(pickedAngle);
        float closestSlotAngle = fortuneWheelSlotTransforms[closestSlotIndex].localRotation.eulerAngles.z;

        await _fortuneWheelBase.transform.DORotate(new Vector3(0, 0, closestSlotAngle), 0.5f).AsyncWaitForCompletion();

        endOfSpinEvent.Invoke(_slotRewards[closestSlotIndex]);
    }

    public int GetClosestSlotIndex(int pickedAngle)
    {
        int closestSlotIndex = 0;
        Transform closestSlotTransform = fortuneWheelSlotTransforms[closestSlotIndex];
        float closestAngle = Mathf.Abs(Mathf.DeltaAngle(pickedAngle, closestSlotTransform.localRotation.eulerAngles.z));


        for(int i = 0; i < _slotCount; i++)
        {
            Transform slotToCheck = fortuneWheelSlotTransforms[i];
            float angleDiffrence = Mathf.Abs(Mathf.DeltaAngle(pickedAngle, slotToCheck.localRotation.eulerAngles.z));
            if (angleDiffrence < closestAngle)
            {
                closestSlotIndex = i;
                closestAngle = angleDiffrence;
            }
        }

        return closestSlotIndex;

    }

    //TODO: Generate random items on wheel, rewards struct and rewarding event on fortune wheel manager.

    public void SetItemsOnWheel(FortuneWheelZoneConfiguration fortuneWheelZoneConfiguration, FortuneWheelReward bomb)
    {
        int rewardCount = _slotCount - fortuneWheelZoneConfiguration.bombZoneCount;
        List<int> shuffledSlotIndexes = Enumerable.Range(0, _slotCount).OrderBy(x => Random.Range(0f, 1f)).ToList();
        int shuffledSlotIndex = 0;

        while (shuffledSlotIndex < rewardCount)
        {
            FortuneWheelReward randomReward = fortuneWheelZoneConfiguration.zoneRewards[Random.Range(0, fortuneWheelZoneConfiguration.zoneRewards.Length)];
            SetItemOnWheel(shuffledSlotIndexes[shuffledSlotIndex], randomReward);
            shuffledSlotIndex++;
        }

        while (shuffledSlotIndex < _slotCount)
        {
            SetItemOnWheel(shuffledSlotIndexes[shuffledSlotIndex], bomb);
            shuffledSlotIndex++;
        }
    }

    private void SetItemOnWheel(int index, FortuneWheelReward randomReward)
    {
        int quantity = Random.Range(randomReward.minimumAmount, randomReward.maximumAmount + 1);
        randomReward = Instantiate(randomReward);
        randomReward.quantity = quantity;
        _slotRewards[index] = randomReward;

        Transform fortuneWheelSlot = fortuneWheelSlotTransforms[index];
        Transform imageTransform = fortuneWheelSlot.GetChild(0);
        Transform textTransform = fortuneWheelSlot.GetChild(1);

        imageTransform.GetComponent<Image>().sprite = randomReward.itemData.itemSprite;
        imageTransform.GetComponent<AspectRatioFitter>().aspectRatio = randomReward.itemData.itemTextureAspectRatio;

        textTransform.GetComponent<TextMeshProUGUI>().text = quantity.ToString();

    }
}
