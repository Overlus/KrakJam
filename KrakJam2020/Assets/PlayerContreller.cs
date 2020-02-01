using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerContreller : MonoBehaviour
{
    private Vector3 newPlayerPosition;
    private bool isLerping;
    private Vector3 childrenPosition;
    private LayerMask _layerMask = 8;
    private void Update()
    {
        PlayerInput();
        DrawLines();
        childrenPosition = transform.GetChild(0).position;
       
        Debug.Log(newPlayerPosition.x);
    }

    private void PlayerInput()
    {
        if (!Input.anyKeyDown)
            return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            CheckIsPatchAvailable(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CheckIsPatchAvailable(-Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckIsPatchAvailable(-Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CheckIsPatchAvailable(Vector3.right);
        }
    }
    private void CheckIsPatchAvailable(Vector3 direction)
    {
        childrenPosition.y =- 0.20f;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(childrenPosition, direction,1);
        if (hits.Length >= 5 && !isLerping)
        {
            newPlayerPosition = transform.position + direction;

            StartCoroutine(MovePlayer(0.35f));
                Debug.Log("Dupa");
        }
    }
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
        newPlayerPosition.x = (float)Math.Round(newPlayerPosition.x,1);
        newPlayerPosition.z = (float)Math.Round(newPlayerPosition.z,1);
        transform.position = newPlayerPosition;
        isLerping = false;
    }
}
