using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartPick : MonoBehaviour
{
    public Text scoreText;
    public int partCount = 0;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent = other.transform;
            gameObject.transform.localPosition = new Vector3(0, 1.3f, 0);
            partCount = ScoreManager.Instance.Scores++;
        }else if (other.CompareTag("Portal"))
        {

            Map.SpawnItemToReppairPortal(Map.size, Map.repparingItem, Map.gridOfRooms);
            
            Destroy(gameObject);
        }
    }
}
