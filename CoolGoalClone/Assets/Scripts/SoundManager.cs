using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip BallKickClip, GoalClip, GoldPickClip, BreakableHitClip;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        ObserverManager.KickBall.AddListener(BallKickSound);
        ObserverManager.BallHitObstacle.AddListener(BallKickSound);
        ObserverManager.BallHitGoal.AddListener(GoalSound);
        ObserverManager.BreakableHitSoundEffect.AddListener(BreakableHitSound);

    }

    private void BallKickSound() => StartCoroutine(BallKickSoundDelay(1));
    private void GoalSound() => _audioSource.PlayOneShot(GoalClip);
    private void GoldPickSound() => _audioSource.PlayOneShot(GoldPickClip);
    private void BreakableHitSound() => _audioSource.PlayOneShot(BreakableHitClip);

    private IEnumerator BallKickSoundDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _audioSource.PlayOneShot(BallKickClip);
    }
}
