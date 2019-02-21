using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyScript : MonoBehaviour
{
    #region Unity_Functions
    public float movespeed;
    private Rigidbody2D rb;
    private float endX;
    private float endY;
    private bool endReached;
    public float fireSpeed;
    public float reloadTime;
    public float damage;
    public GameObject projectile;
    private bool collided;
    public float maxHealth;
    private float currHealth;
    private Animator anim;

    float reloadTimer = 0;

    void Start()
    {
        //Spawn Enemy with a random y position, at x == 10, and a movespeed
        endReached = false;
        collided = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(10f, Random.Range(-5f, 5f));
        endX = Random.Range(0f, 8f);
        endY = Random.Range(-4f, 4f);
        //Set velocity vector of Enemy based off of start and end locations
        Vector2 movementVector = new Vector2(endX - transform.position.x, endY - transform.position.y);
        rb.velocity = movementVector.normalized * movespeed;
        //rb.velocity = new Vector2(Random.Range(-10, 10), Random.Range(13, 18));
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= endX && !endReached)
        {
            endReached = true;
            rb.velocity = new Vector2(0f, 2f);
        }
        if (endReached)
        {
            if (collided)
            {
                rb.velocity = new Vector2(0f, -rb.velocity.y);
                collided = false;
            }
            //else if (transform.position.y >= endY + 2f)
            //{
            //    rb.velocity = new Vector2(0f, -1f);
            //}
            //else if (transform.position.y <= endY - 2f)
            //{
            //    rb.velocity = new Vector2(0f, 1f);
            //}
        }
        
        if (endReached && reloadTimer > reloadTime)
        {
            StartCoroutine(FireballRoutine());
        } else
        {
            anim.SetBool("Throwing", false);
            reloadTimer += Time.deltaTime;
        }
    }

    IEnumerator FireballRoutine()
    {
        reloadTimer = 0;
        Vector2 FireDirection = new Vector2(-1, 0);
        FireDirection = FireDirection.normalized * fireSpeed;

        // Create Fireball
        anim.SetBool("Throwing", true);
        GameObject fireball = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
        fireball.GetComponent<Rigidbody2D>().velocity = FireDirection;
        fireball.GetComponent<Fireball>().SetDamage(damage);

        Destroy(fireball, 5);

        yield return new WaitForSeconds(0.1f);
    }

    public void TakeDamage(float value)
    {
        currHealth -= value;
        if (currHealth <= 0)
        {
            FindObjectOfType<Score>().AddScore(10);
            Die();
        } else
        {
            FindObjectOfType<AudioManager>().Play("Orc2");
        }
    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().Play("Explosion2");
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        collided = true;
    }
    #endregion
}
