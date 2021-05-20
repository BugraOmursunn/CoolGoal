using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        Prepare,
        Play,
        Win,
        Lose,
    };
    public static GameStates CurrentGameState;
    [SerializeField] private Vector3[] waypointPositions;
    public Transform _ball;
    private Tween _ballPath;
    private void Start()
    {
        CurrentGameState = GameStates.Prepare;

        //observers
        ObserverManager.BallHitObstacle.AddListener(StopBall);
        ObserverManager.BallHitGoal.AddListener(GameWin);
        ObserverManager.KickBall.AddListener(KickBall);
    }
    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameStates.Prepare:
                break;
            case GameStates.Play:
                break;
            case GameStates.Win:
                break;
            case GameStates.Lose:
                break;
        }
    }
    private void KickBall()
    {
        if (CurrentGameState == GameStates.Prepare)
        {
            waypointPositions = PathManager.GetWaypoints();
            _ballPath = _ball.DOPath(waypointPositions, 0.6f, PathType.CatmullRom, PathMode.Full3D, 10, null);
            _ballPath.ForceInit();
            CurrentGameState = GameStates.Play;
        }
    }
    private void StopBall()
    {
        CurrentGameState = GameStates.Lose;
        _ballPath.Pause();
        ActiveRB();
    }
    private void ActiveRB()
    {
        Rigidbody _ballRB = _ball.GetComponent<Rigidbody>();
        _ballRB.AddForce(Vector3.forward * Random.Range(500, 1000), ForceMode.Acceleration);
        _ballRB.useGravity = true;
    }
    private void GameWin()
    {
        CurrentGameState = GameStates.Win;
        ActiveRB();
    }
}
