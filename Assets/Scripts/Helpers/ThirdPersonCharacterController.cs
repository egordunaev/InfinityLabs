using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    public float Speed;
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 playermovement = new Vector3(horizontal, 0f, vertical) * Speed * Time.deltaTime;
        transform.Translate(playermovement, Space.Self);
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
}
