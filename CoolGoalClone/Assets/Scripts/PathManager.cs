using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PathManager : MonoBehaviour
{
    public GameObject[] waypointObjects;
    public static Vector3[] waypointPositions;
    public Transform[] dots;
    private Tween _pathTween;
    private GameObject _pathDummy;
    private bool IsDotsShowing;
    void Start()
    {
        ObserverManager.KickBall.AddListener(HideDots);
        waypointPositions = new Vector3[waypointObjects.Length];
        _pathDummy = new GameObject();
    }
    public void DrawPath()
    {
        for (int i = 0; i < waypointObjects.Length; i++)
            waypointPositions[i] = waypointObjects[i].transform.position;

        _pathDummy.transform.position = waypointPositions[0];

        _pathTween = _pathDummy.transform.DOPath(waypointPositions, 1, PathType.CatmullRom, PathMode.Full3D, 10, null);
        _pathTween.ForceInit();

        for (int i = 0; i < dots.Length; i++)
        {
            float u = (((float)100 / dots.Length) * i) / 100;
            Vector3 point = _pathTween.PathGetPoint(u);
            dots[i].transform.position = point;
        }
        ShowDots();
    }
    private void ShowDots()
    {
        if (IsDotsShowing == false)
        {
            foreach (Transform item in dots)
                item.gameObject.SetActive(true);

            waypointObjects[2].SetActive(true);
            IsDotsShowing = true;
        }
    }
    private void HideDots()
    {
        if (IsDotsShowing == true)
        {
            foreach (Transform item in dots)
                item.gameObject.SetActive(false);

            waypointObjects[2].SetActive(false);
            IsDotsShowing = false;
        }
    }
    public static Vector3[] GetWaypoints()
    {
        return waypointPositions;
    }

}
