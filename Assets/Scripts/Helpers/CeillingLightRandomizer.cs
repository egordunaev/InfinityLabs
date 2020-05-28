using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for randomizing state of Ceilling light, just doing it for fun i guess
/// </summary>
public class CeillingLightRandomizer : MonoBehaviour
{
    public bool isFlickering;
    [Range(0f,100f)]
    public float flickerChance;
    public bool randomIntensity;
    [Range(0f, 600f)]
    public float maxIntensity;
    private Light lightSource;
    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
        if (randomIntensity)
        {
            lightSource.intensity = Random.Range(5f, maxIntensity);
        }
        else
        {
            lightSource.intensity = maxIntensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering)
        {
            if (Random.Range(0f,100f) < flickerChance)
            {
                lightSource.enabled = false;
            }
            else
            {
                lightSource.enabled = true;
            }
        }
    }
}
