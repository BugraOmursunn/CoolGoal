using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimController : MonoBehaviour
{
    private Animator characterAnimator;
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        ObserverManager.KickBall.AddListener(KickBallState);
    }

    private void KickBallState()
    {
        characterAnimator.SetTrigger("IsKick");
        this.transform.DOMoveZ(this.transform.position.z + 3.7f, 1);

    }
}
