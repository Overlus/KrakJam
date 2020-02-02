using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyAnimationScript : MonoBehaviour
{
    [SerializeField] GameObject parent;
    [SerializeField] Rigidbody[] childrenToLunchOut;


    void Start()
    {
        childrenToLunchOut = GetComponentsInChildren<Rigidbody>();
        Camera.main.transform.position = transform.position + (Vector3.up * 2 - Vector3.forward * 3);
        Camera.main.transform.LookAt(transform);

        parent.GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(WaitABit());


    }


    IEnumerator WaitABit()
    {

        yield return new WaitForSeconds(0.7f);

        foreach (Rigidbody rig in childrenToLunchOut)
        {
            rig.isKinematic = false;
            rig.AddExplosionForce(200, transform.position, 1.5f);
        }


    }

    // Update is called once per frame

}
