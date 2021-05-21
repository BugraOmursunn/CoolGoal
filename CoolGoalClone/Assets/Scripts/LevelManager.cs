using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public int CurrentLevelIndex;
    [SerializeField] private int RealLevelIndex, FakeLevelIndex;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("CurrentFakeLevelIndex") == false)
        {
            PlayerPrefs.SetInt("CurrentFakeLevelIndex", 1);
            SceneManager.LoadScene("Level" + 1, LoadSceneMode.Single);
        }
        else
        {
            FakeLevelIndex = PlayerPrefs.GetInt("CurrentFakeLevelIndex");

            if (FakeLevelIndex % 10 == 0)
            {
                RealLevelIndex = 10;
                PlayerPrefs.SetInt("CurrentFakeLevelIndex", FakeLevelIndex);
            }
            else
                RealLevelIndex = FakeLevelIndex % 10;

            SceneManager.LoadScene("Level" + RealLevelIndex, LoadSceneMode.Single);
        }
    }


}
