using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image settingsImg;
    [SerializeField] private Sprite[] settingsSprites;
    private bool IsSettingsOpen;
    [SerializeField] private GameObject HapticBtn, SoundBtn, RestartBtn;
    [SerializeField] private GameObject HandImg;

    [SerializeField] private Text CurrentLevelIndex, NextLevelIndex;
    [SerializeField] private GameObject[] LevelProgressBodySprites, LevelProgressCheckSprites;
    [SerializeField] private int LevelTotalPartCount;
    void Start()
    {
        ObserverManager.DragStarted.AddListener(HideUI);
        CurrentLevelIndex.text = PlayerPrefs.GetInt("CurrentFakeLevelIndex").ToString();
        NextLevelIndex.text = (PlayerPrefs.GetInt("CurrentFakeLevelIndex") + 1).ToString();

        LevelTotalPartCount = this.GetComponent<GameManager>().ReturnPartCount();

        for (int i = 0; i < LevelTotalPartCount; i++)
        {
            LevelProgressBodySprites[i].SetActive(true);
        }
    }
    public void ChangeSettings()
    {
        if (IsSettingsOpen == false)
        {
            settingsImg.sprite = settingsSprites[1];
            IsSettingsOpen = true;
        }
        else
        {
            settingsImg.sprite = settingsSprites[0];
            IsSettingsOpen = false;
        }
    }
    private void HideUI()
    {
        HandImg.SetActive(false);
    }
    public void SetLevelProgress(int index)
    {
        for (int i = 0; i < index; i++)
        {
            LevelProgressCheckSprites[i].SetActive(true);
        }
    }


}
