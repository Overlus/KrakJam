using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using Debug = UnityEngine.Debug;

public class EnemyController : MonoBehaviour
{
    private Vector3 childrenPosition;
    private Vector3 newPlayerPosition;
    private bool isLerping;
    private bool enemyMoved;
    private LayerMask layermask = 8;
    private void Update()
    {
        DrawLines();
        childrenPosition = transform.GetChild(0).position;
        CheckIsPatchAvailable(ChoseRandomDirection());
    }

    private void OnEnable()
    {
        enemyMoved = false;
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

    private void CheckIsPatchAvailable(Vector3 direction)
    {
        RaycastHit hit;
        if (!Physics.Raycast(childrenPosition, direction, out hit, 1,layermask)&&!isLerping)
        {
            newPlayerPosition = transform.position + direction;
            StartCoroutine(MovePlayer(0.35f));
            return;
        }
        return;
    }

    private IEnumerator MovePlayer(float lerpTime)
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
        enabled = false;
        enemyMoved = true;
        isLerping = false;
    }
    
#if UNITY_EDITOR
    private void DrawLines()
    {
        var position = childrenPosition;
        var right = transform.right;
        Debug.DrawRay(position,right,Color.red,1f);
        Debug.DrawRay(position,-right ,Color.red,1f);
        var forward = transform.forward;
        Debug.DrawRay(position,forward,Color.red,1f);
        Debug.DrawRay(position,-forward,Color.red,1f);
    }
#endif
}
