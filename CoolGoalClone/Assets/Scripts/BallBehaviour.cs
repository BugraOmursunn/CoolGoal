using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _hitFx, _trailFx, _winFx;
    public enum HitTypes
    {
        Obstacle,
        Goal,
        Breakable,
    };
    public static HitTypes _hitType;
    private void Start()
    {
        ObserverManager.KickBall.AddListener(OpenTrail);
    }
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Obstacle":
                ObserverManager.BallHitObstacle?.Invoke();
                _hitFx.SetActive(true);
                break;
            case "Goal":
                ObserverManager.BallHitGoal?.Invoke();
                _winFx.SetActive(true);
                _trailFx.SetActive(false);
                break;
            case "Breakable":
                other.transform.GetComponent<BreakableObject>().ObjectHit();
                break;
        }
    }
    private void OpenTrail()
    {
        _trailFx.SetActive(true);
    }
}
