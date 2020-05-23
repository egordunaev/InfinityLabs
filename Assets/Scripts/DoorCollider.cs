using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    public bool isContactingCollider;
    public Collider contactingCollider;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Door collider connecting status:" + isContactingCollider.ToString());
    }
    void onTriggerEnter(Collider other)
    {
        isContactingCollider = true;
    }
    void OnTriggerStay(Collider other)
    {
        contactingCollider = other;
        isContactingCollider = true;
    }
    void OnTriggerExit(Collider other)
    {
        isContactingCollider = false;
    }
}
