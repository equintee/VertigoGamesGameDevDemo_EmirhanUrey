using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Fortune Wheel Item", menuName = "Fortune Wheel/Fortune Wheel Item")]
public class FortuneWheelReward : ScriptableObject
{
    public ItemData itemData;
    [Range(0, 100)] public int minimumAmount;
    [Range(0, 100)] public int maximumAmount;
    [HideInInspector] public int quantity;
    [HideInInspector] public UnityEvent rewardEvent;

    private void OnValidate()
    {
        if (itemData == null)
            Debug.LogError($"{name} item data is not assigned.");

        minimumAmount = Mathf.Min(minimumAmount, maximumAmount);
        maximumAmount = Mathf.Max(minimumAmount, maximumAmount);
    }
}
