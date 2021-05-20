using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SwipeManager : MonoBehaviour
{


    private bool IsDragging;
    private Vector2 _startTouch, _swipeLength;
    private float _swipeLengthX;
    [SerializeField] private Transform midCurvePointPos, lastCurvePointPos;
    private float midCurveNewPos, lastCurveNewPos;
    public PathManager _pathManager;

    private float minMidCurveBorder, maxMidCurveBorder;
    private float minLastCurveBorder, maxLastCurveBorder;
    [Range(0, 2)]
    [SerializeField] private float LastCurveBorder;
    private void Start()
    {
        minMidCurveBorder = midCurvePointPos.position.x - 5;
        maxMidCurveBorder = midCurvePointPos.position.x + 5;

        minLastCurveBorder = lastCurvePointPos.position.x - 2;
        maxLastCurveBorder = lastCurvePointPos.position.x + 2;
    }
    private void Update()
    {
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            IsDragging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
            Reset();
            ObserverManager.KickBall?.Invoke();
        }
        #endregion
    }
    public void OnMouseDrag()
    {
        //calculate the distance
        _swipeLength = Vector2.zero;

        if (IsDragging)
        {
            if (Input.touches.Length > 0)//if there are more than 1 touch at the same time
            {
                _swipeLength = Input.touches[0].position - _startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                _swipeLength = (Vector2)Input.mousePosition - _startTouch;
                _swipeLengthX = Mathf.Clamp(_swipeLength.x, -20, 20);
            }
        }
        SetCurvePos();
    }
    private void SetCurvePos()
    {
        float diff = Mathf.Abs(midCurveNewPos - lastCurveNewPos);

        if (diff < 3)
        {
            midCurveNewPos = Mathf.Clamp(midCurveNewPos - _swipeLengthX / 100, minMidCurveBorder, maxMidCurveBorder);
            midCurvePointPos.position = new Vector3(midCurveNewPos, midCurvePointPos.position.y, midCurvePointPos.position.z);

            lastCurveNewPos = Mathf.Clamp(lastCurveNewPos - _swipeLengthX / 350, minLastCurveBorder, maxLastCurveBorder);
            lastCurvePointPos.position = new Vector3(lastCurveNewPos, lastCurvePointPos.position.y, lastCurvePointPos.position.z);
        }
        else
        {
            midCurveNewPos = Mathf.Clamp(midCurveNewPos - _swipeLengthX / 350, minMidCurveBorder, maxMidCurveBorder);
            midCurvePointPos.position = new Vector3(midCurveNewPos, midCurvePointPos.position.y, midCurvePointPos.position.z);

            lastCurveNewPos = Mathf.Clamp(lastCurveNewPos + _swipeLengthX / 100, minLastCurveBorder, maxLastCurveBorder);
            lastCurvePointPos.position = new Vector3(lastCurveNewPos, lastCurvePointPos.position.y, lastCurvePointPos.position.z);
        }
        _pathManager.DrawPath();
    }

    private void Reset()
    {
        _startTouch = _swipeLength = Vector2.zero;
        IsDragging = false;
    }


}
