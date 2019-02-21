using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    Rigidbody2D rb;
    public float movespeed;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(10f, Random.Range(-4.5f, 4.5f));
        //Set velocity vector of Enemy based off of start and end locations
        Vector2 movementVector = new Vector2(transform.position.x, transform.position.y);
        rb.velocity = movementVector.normalized * movespeed;
    }

    public void Update()
    {
        MoveAcrossScreen();
    }

    private void MoveAcrossScreen()
    {
        // Move left across the screen
        Vector2 direction = Vector2.left;

        rb.velocity = direction.normalized * movespeed;

        // Kill enemy when moved offscreen
        if (rb.transform.position.x < -11f)
        {
            Debug.Log("Enemy moved offscreen");
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) 
        {
            coll.GetComponent<PlayerController>().specialAvailable = true;
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
