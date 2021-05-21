using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        Prepare,
        Play,
        Win,
        Lose,
    };
    [SerializeField] private GameStates CurrentGameState;
    [SerializeField] private GameObject[] LevelParts;
    [SerializeField] private int LevelTotalPartCount;
    [SerializeField] private int LevelCurrentPartIndex;
    [SerializeField] private Transform[] CameraAngles;

    private Transform mainCamera;
    private int RealLevelIndex, FakeLevelIndex;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        LevelTotalPartCount = LevelParts.Length;
        CurrentGameState = GameStates.Prepare;

        //observers
        ObserverManager.BallHitObstacle.AddListener(StopBall);
        ObserverManager.BallHitGoal.AddListener(GameWin);
        ObserverManager.KickBall.AddListener(KickBall);
    }

    private void KickBall()
    {
        if (CurrentGameState == GameStates.Prepare)
        {
            CurrentGameState = GameStates.Play;
        }
    }

    private void StopBall()
    {
        StartCoroutine(GameLose(3));
        CurrentGameState = GameStates.Lose;
    }

    private IEnumerator ActiveNextPart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LevelParts[LevelCurrentPartIndex].SetActive(true);
        mainCamera.DOMove(CameraAngles[LevelCurrentPartIndex].position, 1).SetEase(Ease.OutCubic);
        StartCoroutine(DeActivePreviousPart(1));
    }

    private IEnumerator DeActivePreviousPart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(LevelParts[LevelCurrentPartIndex - 1]);
    }

    private void GameWin()
    {
        if (CurrentGameState != GameStates.Lose)
        {
            LevelCurrentPartIndex++;
            if (LevelCurrentPartIndex == LevelTotalPartCount && CurrentGameState != GameStates.Lose)
            {
                CurrentGameState = GameStates.Win;
                StartCoroutine(OpenNextLevelDelay(1.5f));
            }
            else if (LevelCurrentPartIndex < LevelTotalPartCount && CurrentGameState != GameStates.Lose)
            {
                StartCoroutine(ActiveNextPart(1.5f));
            }
            this.GetComponent<UIManager>().SetLevelProgress(LevelCurrentPartIndex);
        }
    }

    private IEnumerator GameLose(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator OpenNextLevelDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        FakeLevelIndex = PlayerPrefs.GetInt("CurrentFakeLevelIndex");
        FakeLevelIndex++;
        PlayerPrefs.SetInt("CurrentFakeLevelIndex", FakeLevelIndex);

        if (FakeLevelIndex % 10 == 0)
        {
            RealLevelIndex = 10;
            PlayerPrefs.SetInt("CurrentFakeLevelIndex", FakeLevelIndex);
        }
        else
            RealLevelIndex = FakeLevelIndex % 10;

        SceneManager.LoadScene("Level" + RealLevelIndex, LoadSceneMode.Single);
    }
    public int ReturnPartCount()
    {
        return LevelParts.Length;
    }
}
