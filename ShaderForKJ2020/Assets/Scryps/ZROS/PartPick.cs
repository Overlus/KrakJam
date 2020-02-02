using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            Map.SpawnItemToReppairPortal(Map.size, Map.repparingItem, Map.gridOfRooms);
            
            Destroy(gameObject);
        }
    }
}
