using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionWithPlayer : MonoBehaviour
{
    public AudioSource hit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OurGameManager.actualState = OurGameController.GameState.end;
            Debug.Log(OurGameManager.actualState );
            hit.Play();
        }
           
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OurGameManager.actualState = OurGameController.GameState.end;
            Debug.Log(OurGameManager.actualState );
        }
           
    }
}
