using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class FortuneWheelGameManager : MonoBehaviour
{
    #region Serialized Fields
    [Header("Base Game Settings")]
    public GameObject baseGameFortuneWheelPrefab;
    public FortuneWheelZoneConfiguration baseGameFortuneWheelConfiguration;
    public FortuneWheelReward bomb;
    public GameObject bombCardPrefab;
    public int reviveCost;

    [Header("Bonus Game Settings")]
    public SpecialZoneSettings[] specialZoneSettings;

    [Header("UI & Buttons")]
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject rewardCardScroller;
    public Button collectUIRestartButton;
    public TextMeshProUGUI stageText;
    public RectTransform _collectedCards;
    #endregion

    #region Private Variables
    private IRewardUI winUIController;
    private IRewardUI loseUIController;
    private FortuneWheelRewardScrollerController rewardCardScrollerController;
    private int _playerCash = 71;
    private RectTransform _rectTransform;
    private int _currentZone = 1;
    private int CurrentZone { get { return _currentZone; } set { stageText.text = $"STAGE {value}"; _currentZone = value; } }
    private FortuneWheelManager _currentFortuneWheelManager;
    private FortuneWheelZoneConfiguration _currentFortuneWheelZoneConfiguration;
    private CardController _currentCardShown;
    #endregion

    #region Events
    [HideInInspector] public UnityEvent<int> OnPlayerCashUpdate;
    #endregion
    
    #region Public Fields
    public int PlayerCash
    {
        get
        {
            return _playerCash;
        }
        set
        {
            UpdatePlayerCash(value);
        }
    }
    #endregion

    #region Editor Validation

    private void OnValidate()
     {
        if (winUI == null || !winUI.TryGetComponent(out winUIController))
            Debug.LogWarning($"winUI is not set on {gameObject.name}");

        if (loseUI == null || !loseUI.TryGetComponent(out loseUIController))
            Debug.LogWarning($"loseUI is not set {gameObject.name}");

    }


    #endregion
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>(); //TODO: Cache in editor.

        loseUI.TryGetComponent(out loseUIController);
        winUI.TryGetComponent(out winUIController);
        rewardCardScroller.TryGetComponent(out rewardCardScrollerController);
        InitalizeFortuneWheel();

        loseUIController.InitalizeButtons();
        winUIController.InitalizeButtons();
        rewardCardScrollerController.InitalizeButtons();

        PlayerCash = 100;


    }

    private void Start()
    {
        OnPlayerCashUpdate.Invoke(PlayerCash);
    }
    public void InitalizeFortuneWheel()
    {
        _currentFortuneWheelZoneConfiguration = GetCurrentZone();
        _currentFortuneWheelManager = Instantiate(_currentFortuneWheelZoneConfiguration.fortuneWheelPrefab, _rectTransform.position, Quaternion.identity, transform).GetComponent<FortuneWheelManager>();
        _currentFortuneWheelManager.SetItemsOnWheel(_currentFortuneWheelZoneConfiguration, bomb);

        _currentFortuneWheelManager.endOfSpinEvent.AddListener(GiveReward);

    }
    public FortuneWheelZoneConfiguration GetCurrentZone()
    {
        if (specialZoneSettings.Length == 0)
            return baseGameFortuneWheelConfiguration;

        for(int i = specialZoneSettings.Length - 1; i > -1; i--)
        {
            if (CurrentZone % specialZoneSettings[i].apperanceFrequency == 0)
                return specialZoneSettings[i].specialZoneConfiguration;
        }

        return baseGameFortuneWheelConfiguration;
    }

    private void GiveReward(FortuneWheelReward reward)
    {
        if(reward.itemData.id == bomb.itemData.id)
        {
            loseUIController.ShowUI();
            _currentCardShown = Instantiate(bombCardPrefab, _rectTransform.position, Quaternion.identity, transform).GetComponent<CardController>();
            Debug.Log(reward.quantity);
        }
        else
        {
            _currentCardShown = Instantiate(_currentFortuneWheelZoneConfiguration.rewardCardPrefab, _rectTransform.position, Quaternion.identity, transform).GetComponent<CardController>();
            _currentCardShown.InitializeCard(reward.itemData, reward.quantity);
            rewardCardScrollerController.AddCardToScroller(Instantiate(_currentCardShown.gameObject));

            winUIController.ShowUI();
            Debug.Log(reward.quantity);
        }
    }

    public void ShowRewards()
    {
        rewardCardScrollerController.ShowUI();
    }

    public void NextZone()
    {
        Destroy(_currentFortuneWheelManager.gameObject);
        CurrentZone++;
        InitalizeFortuneWheel();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void RevivePlayer()
    {
        if (PlayerCash <= 0)
            PlayerCash = 56;

        PlayerCash -= reviveCost;
        NextZone();
        Destroy(_currentCardShown.gameObject);
    }

    private bool UpdatePlayerCash(int value)
    {
        _playerCash = value;
        OnPlayerCashUpdate?.Invoke(_playerCash);
        return true;
    }
}


[System.Serializable]
public struct SpecialZoneSettings
{
    public FortuneWheelZoneConfiguration specialZoneConfiguration;
    public int apperanceFrequency;
}
