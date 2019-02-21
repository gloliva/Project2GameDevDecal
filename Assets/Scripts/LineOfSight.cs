using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    MeleeEnemyScript script;

    // Called when something enters the trigger collider
    private void OnTriggerEnter2D(Collider2D coll)
    {
        // Check if coll is player
        if (coll.CompareTag("Player"))
        {
            script = GetComponentInParent<MeleeEnemyScript>();
            script.player = coll.transform;
            script.trackPlayer = true;
            
        }
    }
}
