using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class ConfettiFx : MonoBehaviour
{
    [SerializeField] private GameObject[] _confettis;
    void Start()
    {
        ObserverManager.BallHitGoal.AddListener(OpenConfetti);
    }

    private void OpenConfetti()
    {
        foreach (GameObject item in _confettis)
        {
            item.SetActive(true);
        }
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }
}
