using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject WholeObject;
    [SerializeField] private GameObject FractureObject;

    public void ObjectHit()
    {
        WholeObject.SetActive(false);
        FractureObject.SetActive(true);

        for (int i = 0; i < FractureObject.transform.childCount; i++)
            FractureObject.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(500, 1000), ForceMode.Acceleration);

    }
}
