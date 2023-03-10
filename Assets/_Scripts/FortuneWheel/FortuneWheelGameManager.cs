using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FortuneWheelGameManager : MonoBehaviour
{
    [Header("Base Game Settings")]
    public GameObject baseGameFortuneWheelPrefab;
    public FortuneWheelZoneConfiguration baseGameFortuneWheelConfiguration;
    public FortuneWheelReward bomb;
    public GameObject bombCardPrefab;

    [Header("Bonus Game Settings")]
    public SpecialZoneSettings[] specialZoneSettings;

    [Header("UI & Buttons")]
    public GameObject winUI;
    public Button collectButton;
    public Button contunieButton;
    public GameObject loseUI;
    public Button restartButton;
    public Button reviveButton;

    private RectTransform _rectTransform;
    private int _currentZone = 1;
    private FortuneWheelManager _currentFortuneWheelManager;
    private FortuneWheelZoneConfiguration _currentFortuneWheelZoneConfiguration;
    private CardController _currentCardShown;

    #region Editor Validation
     private void OnValidate()
     {
        

     }


    #endregion
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>(); //TODO: Cache in editor.

        collectButton.onClick.AddListener(ShowRewards);
        contunieButton.onClick.AddListener(NextZone);
        restartButton.onClick.AddListener(RestartGame);
        reviveButton.onClick.AddListener(Revive);

        InitalizeFortuneWheel();
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
            if (_currentZone % specialZoneSettings[i].apperanceFrequency == 0)
                return specialZoneSettings[i].specialZoneConfiguration;
        }

        return baseGameFortuneWheelConfiguration;
    }

    private void GiveReward(FortuneWheelReward reward)
    {
        if(reward.itemData.id == bomb.itemData.id)
        {
            _currentCardShown = Instantiate(bombCardPrefab, _rectTransform.position, Quaternion.identity, transform).GetComponent<CardController>();
            loseUI.SetActive(true);
        }
        else
        {
            _currentCardShown = Instantiate(_currentFortuneWheelZoneConfiguration.rewardCardPrefab, _rectTransform.position, Quaternion.identity, transform).GetComponent<CardController>();
            _currentCardShown.InitializeCard(reward.itemData, reward.quantity);
            winUI.SetActive(true);
        }
    }

    public void ShowRewards()
    {
        HideButtons();
    }

    public void NextZone()
    {
        Destroy(_currentCardShown.gameObject);
        Destroy(_currentFortuneWheelManager.gameObject);
        HideButtons();
        _currentZone++;
        InitalizeFortuneWheel();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Revive()
    {
        //Decrement gold(10x)
        NextZone();
    }

    public void HideButtons()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }
}


[System.Serializable]
public struct SpecialZoneSettings
{
    public FortuneWheelZoneConfiguration specialZoneConfiguration;
    public int apperanceFrequency;
}
