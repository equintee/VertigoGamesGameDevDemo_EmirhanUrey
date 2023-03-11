using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton Field
    public static GameManager Instance;
    #endregion
    #region Player Prefs Keys
    private const string PLAYERPREFS_PLAYERCASH = "PlayerCash";
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
            TryUpdatePlayerCash(value);
        }
    }
    #endregion

    #region Events
    public UnityEvent<int> OnPlayerCashUpdate;
    #endregion

    #region Private Variables
    private int _playerCash;
    #endregion

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey(PLAYERPREFS_PLAYERCASH))
            PlayerPrefs.SetInt(PLAYERPREFS_PLAYERCASH, 100);

        
    }
    private void Start()
    {
        PlayerCash = PlayerPrefs.GetInt(PLAYERPREFS_PLAYERCASH);
    }
    private bool TryUpdatePlayerCash(int value)
    {
        _playerCash = value;
        PlayerPrefs.SetInt("PlayerCash", _playerCash);
        OnPlayerCashUpdate?.Invoke(_playerCash);
        return true;
    }
}
