using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes flashlight logic. Pretty basic.
/// </summary>
public class FlashLight : MonoBehaviour
{
    private bool lightState;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        lightState = false;
        light = GetComponent<Light>();
        light.enabled = lightState;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (lightState)
            {
                light.enabled = false;
                lightState = false;
            }
            else
            {
                light.enabled = true;
                lightState = true;
            }
        }
    }
}
