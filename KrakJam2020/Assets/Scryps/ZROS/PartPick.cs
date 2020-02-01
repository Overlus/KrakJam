using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent = other.transform;
            gameObject.transform.localPosition = new Vector3(0, 1.3f, 0);
        }else if (other.CompareTag("Portal"))
        {
            Destroy(gameObject);
        }
    }
}
