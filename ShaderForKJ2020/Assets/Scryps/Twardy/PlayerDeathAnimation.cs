using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathAnimation : MonoBehaviour
{

    [SerializeField] Rigidbody[] childrenToLunchOutAvay;

    [SerializeField] GameObject rodzic;

    void Start()
    {
        rodzic.GetComponent<MeshRenderer>().enabled = false;

        childrenToLunchOutAvay = GetComponentsInChildren<Rigidbody>();

        Vector3.Lerp(Camera.main.transform.position, transform.position + Vector3.up * 2 - Vector3.forward * 3, 1f);

        foreach(Rigidbody rig in childrenToLunchOutAvay)
        {
            rig.AddForce(rig.transform.position - transform.position * 1000);
        }

    }

    private void Update()
    {
        Camera.main.transform.LookAt(transform);
    }
}
