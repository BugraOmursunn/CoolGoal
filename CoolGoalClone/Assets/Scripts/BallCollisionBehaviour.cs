using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.NiceVibrations;
public class BallCollisionBehaviour : MonoBehaviour
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
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                break;
            case "Goal":
                ObserverManager.BallHitGoal?.Invoke();
                _winFx.SetActive(true);
                _trailFx.SetActive(false);
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                Destroy(other.collider);
                break;
            case "Breakable":
                ObserverManager.BreakableHitSoundEffect?.Invoke();
                other.transform.GetComponent<BreakableObject>().ObjectHit();
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                Destroy(other.collider);
                break;
        }
    }
    private void OpenTrail()
    {
        _trailFx.SetActive(true);
    }
}
