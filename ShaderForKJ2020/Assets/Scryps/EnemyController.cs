using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Vector3 newEnemyPosition;
    private bool isMoving;
    private readonly LayerMask layermask = 8;
    [SerializeField] private Transform rayCastOrigin;
    List<Vector3> directionOptions = new List<Vector3>();
    private bool isNewPositionFound;

    private Coroutine moveCoroutine;

    private void OnDisable()
    {
        if(moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    }

    private void Update()
    {
//        DrawLines();
        if (OurGameManager.actualState == OurGameController.GameState.enemyMove && !isNewPositionFound)
        {
            isNewPositionFound = true;
            ChoseRandomDirection();

            if (directionOptions.Count > 0)
            {
                var direction = directionOptions[Random.Range(0, directionOptions.Count)];
                newEnemyPosition = transform.position + direction;
                
                if(!isMoving)
                    moveCoroutine = StartCoroutine(EnemyMove(0.35f));
            }
            else if (directionOptions.Count == 0)
            {
                OurGameManager.actualState = OurGameController.GameState.sceneMove;
                OurGameController.enemyMadeMove = true;
                isNewPositionFound = false;
            }
        }
    }


    private IEnumerator EnemyMove(float lerpTime)
    {
        Vector3 startingPosition = transform.position;
        isMoving = true;
        var timmer = 0f;
        float lerpRatio;
        while (timmer < lerpTime)
        {
            lerpRatio = timmer / lerpTime;
            gameObject.transform.position = Vector3.Lerp(startingPosition, newEnemyPosition, lerpRatio);
            yield return new WaitForEndOfFrame();
            timmer = timmer + Time.deltaTime;
        }
        newEnemyPosition.x = (float) Math.Round(newEnemyPosition.x, 1);
        newEnemyPosition.z = (float) Math.Round(newEnemyPosition.z, 1);
        transform.position = newEnemyPosition;
        isMoving = false;
        OurGameManager.actualState = OurGameController.GameState.sceneMove;
        OurGameController.enemyMadeMove = true;
        isNewPositionFound = false;
    }

    void ChoseRandomDirection()
    {
        directionOptions.Clear();
        CheckIsPathAvailable(Vector3.right);
        CheckIsPathAvailable(-Vector3.right);
        CheckIsPathAvailable(Vector3.forward);
        CheckIsPathAvailable(-Vector3.forward);
    }

    private void CheckIsPathAvailable(Vector3 direction)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(rayCastOrigin.position, direction,1, 1 << layermask);
        if (hits.Length >= 5)
        {
            directionOptions.Add(direction);
        }     
        
    }


#if UNITY_EDITOR
    private void DrawLines()
    {
        var position = rayCastOrigin.position;
        var right = transform.right;
        Debug.DrawRay(position,right,Color.red,1f);
        Debug.DrawRay(position,-right ,Color.red,1f);
        var forward = transform.forward;
        Debug.DrawRay(position,forward,Color.red,1f);
        Debug.DrawRay(position,-forward,Color.red,1f);
    }
#endif
}
