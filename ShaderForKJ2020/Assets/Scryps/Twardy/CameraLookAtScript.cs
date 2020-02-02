using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtScript : MonoBehaviour
{

    public Transform objectToLookAt;

    void Start()
    {
        transform.LookAt(objectToLookAt);
    }

}
