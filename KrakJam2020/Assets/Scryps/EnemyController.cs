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
    private void Update()
    {
        DrawLines();
        childrenPosition = transform.GetChild(0).position;
        if (OurGameManager.actualState == OurGameController.GameState.enemyMove)
        {
            CheckIsPathAvailable(ChoseRandomDirection());
        }
    }

    
    Vector3 ChoseRandomDirection()
    {
        var rnd = Random.Range(1, 5); 
        switch (rnd)
        {
            case 1:
                return Vector3.right;
            case 2:
                return -Vector3.right;
            case 3:
                return Vector3.forward;
            case 4:
                return -Vector3.forward;
        }
        return Vector3.zero;
    }

    private void CheckIsPathAvailable(Vector3 direction)
    {
//        childrenPosition.y =- 0.20f;
//        RaycastHit hit;
//        if (!Physics.Raycast(childrenPosition, direction, out hit, 1,layermask)&&!isLerping&&
//            OurGameManager.actualState == OurGameController.GameState.enemyMove)
//        {
//            newPlayerPosition = transform.position + direction;
//            StartCoroutine(EnemyMove(0.35f));
//            return;
//        }
        childrenPosition.y =- 0.20f;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(childrenPosition, direction,1);
        if (hits.Length >= 5 && !isLerping)
        {
            newPlayerPosition = transform.position + direction;
            StartCoroutine(EnemyMove(0.35f));
        }

        return;
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
        OurGameManager.actualState = OurGameController.GameState.sceneMove;
        OurGameController.enemyMadeMove = true;
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
