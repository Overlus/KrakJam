using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Vector3 childrenPosition;
    private Vector3 newPlayerPosition;
    private bool isLerping;
    private readonly LayerMask layermask = 8;
    private bool[] availablePaths;

    [SerializeField] private Transform player;
    
    private void Update()
    {
        DrawLines();
        childrenPosition = transform.GetChild(0).position;
        if (OurGameManager.actualState == OurGameController.GameState.enemyMove)
        {
            ChoseRandomDirection();
        }
    }

    
    void ChoseRandomDirection()
    {
        CheckIsPathAvailable(Vector3.right,0);
        CheckIsPathAvailable(-Vector3.right,1);
        CheckIsPathAvailable(Vector3.forward,2);
        CheckIsPathAvailable(-Vector3.forward,3);
    }

    private void CheckIsPathAvailable(Vector3 direction, int boolnr)
    {
        childrenPosition.y =- 0.20f;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(childrenPosition, direction,1);
        if (hits.Length >= 5 && !isLerping)
        {
            availablePaths[boolnr] = true;
        }

        availablePaths[boolnr] = false;

        ChoseAvailablePath();
    }

    private void ChoseAvailablePath()
    {
        float[] temp = new float[4];

        for (int i = 0; i < 4; i++)
        {
            temp[i] = 9000;
        }
        
        if (availablePaths[0])
        {
            Vector3 tempVect = transform.position + Vector3.right;
            temp[0] = Vector3.Distance(tempVect, player.position);
        }
        if (availablePaths[1])
        {
            Vector3 tempVect = transform.position - Vector3.right;
            temp[1] = Vector3.Distance(tempVect, player.position);
        }
        if (availablePaths[2])
        {
            Vector3 tempVect = transform.position + Vector3.forward;
            temp[2] = Vector3.Distance(tempVect, player.position);
        }
        if (availablePaths[3])
        {
            Vector3 tempVect =  transform.position - Vector3.forward;
            temp[3] = Vector3.Distance(tempVect, player.position);
        }

        float temporaryFloat = 8000;
        int index = -1;

        for (int i = 0; i < 4; i++)
        {
            if (temp[i] < temporaryFloat)
            {
                temporaryFloat = temp[i];
                index = i;
            }
        }


        if (index != -1 && temp[index] != 9000)
        {
            Vector3 direction;
            switch (index)
            {
                case 0 :
                    direction = Vector3.right;
                    break;
                case 1 :
                    direction = -Vector3.right;
                    break;
                case 2 :
                    direction = Vector3.forward;
                    break;
                case 3 :
                    direction = -Vector3.forward;
                    break;
                default: direction = Vector3.zero;
                    break;
            }
            newPlayerPosition = transform.position + direction;
            StartCoroutine(EnemyMove(0.35f));
        }
    }

    private IEnumerator EnemyMove(float lerpTime)
    {
        
        Vector3 startingPosition = transform.position;
        isLerping = true;
        var timmer = 0f;
        float lerpRatio;
        while (timmer < lerpTime)
        {
            lerpRatio = timmer / lerpTime;
            gameObject.transform.position = Vector3.Lerp(startingPosition, newPlayerPosition, lerpRatio);
            yield return new WaitForEndOfFrame();
            timmer = timmer + Time.deltaTime;
        }
        newPlayerPosition.x = (float)Math.Round(newPlayerPosition.x,1);
        newPlayerPosition.z = (float)Math.Round(newPlayerPosition.z,1);
        transform.position = newPlayerPosition;
        isLerping = false;

    }
    
#if UNITY_EDITOR
    private void DrawLines()
    {
        var position = childrenPosition;
        position.y =- 0.20f;
        var right = transform.right;
        Debug.DrawRay(position,right,Color.red,1f);
        Debug.DrawRay(position,-right ,Color.red,1f);
        var forward = transform.forward;
        Debug.DrawRay(position,forward,Color.red,1f);
        Debug.DrawRay(position,-forward,Color.red,1f);
    }
#endif
}
