using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region StaticComponent
    public static bool IsStartGeme, IsGameFlow,IsWinGame,IsLoseGame;
    #endregion

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private Text _levelText;
    private void Awake()
    {
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Level")<=0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        _levelText.text = "Level " + PlayerPrefs.GetInt("Level");
        if (!IsStartGeme)
        {
            _menuUI.SetActive(true);
            IsGameFlow = true;
        }
        else
        {
            _inGameUI.SetActive(true);
            IsGameFlow = true;
        }
    }

    private void Update()
    {
        if (!_inGameUI.activeSelf && IsStartGeme)
        {
            _menuUI.SetActive(false);
            _inGameUI.SetActive(true);
        }
        if (!_wimIU.activeSelf&& IsWinGame)
        {
            IsGameFlow = false;
            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
            PlayerPrefs.SetInt("Scenes", PlayerPrefs.GetInt("Scenes")+1);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

        }
        if (!_lostUI.activeSelf && IsLoseGame)
        {
            IsGameFlow = false;
            _inGameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
}
