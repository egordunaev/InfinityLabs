using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles behavior of various body parts
/// </summary>
public class BodyPart : MonoBehaviour
{
    private Enemy enemy;
    private Collider hitbox;
    [Range(0f,100f)]
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        hitbox = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col != null)
        {
            Debug.Log("Collision detected on " + gameObject + ". Collides with: " + col.gameObject.name+". Tag: "+col.tag);
            enemy.health = enemy.health - damage;
        }
    }
}
