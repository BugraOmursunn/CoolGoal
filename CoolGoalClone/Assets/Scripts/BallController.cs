using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BallController : MonoBehaviour
{
    [SerializeField] private Vector3[] waypointPositions;
    [SerializeField] private Transform _ball;
    private Tween _ballPath;
    private Rigidbody _ballRB;

    private void Start()
    {
        _ballRB = _ball.GetComponent<Rigidbody>();

        //observers
        ObserverManager.BallHitObstacle.AddListener(StopBall);
        ObserverManager.KickBall.AddListener(KickBall);
    }
    private void KickBall()
    {
        StartCoroutine(BallMoveDelay(1));
    }
    private IEnumerator BallMoveDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        waypointPositions = PathManager.GetWaypoints();
        _ballPath = _ball.DOPath(waypointPositions, 0.6f, PathType.CatmullRom, PathMode.Full3D, 10, null);
        _ballPath.ForceInit();
    }
    private void StopBall()
    {
        _ballPath.Pause();
        ActiveRB();
    }
    private void ActiveRB()
    {
        _ballRB.AddForce(Vector3.forward * Random.Range(100, 300), ForceMode.Acceleration);
        _ballRB.useGravity = true;
    }
}
