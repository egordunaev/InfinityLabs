using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Door class desribes functionality for generic doors
/// </summary>
public class Door : MonoBehaviour
{
    Animator animator;
    bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Status: " + isOpen);
    }
    void OnTriggerEnter(Collider col)
    {
        if(!isOpen)
        {
            if (col.gameObject.tag == "PlayerModel" || col.gameObject.tag == "Enemy")
            {
                isOpen = true;
                DoorControl("Open");
            }
            
        }
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log("Left: " + col.gameObject.tag);
        if (isOpen)
        {
            isOpen = false;
            DoorControl("Close");
        }
    }
    void DoorControl(string direction)
    {
        animator.SetTrigger(direction);
    }

}
