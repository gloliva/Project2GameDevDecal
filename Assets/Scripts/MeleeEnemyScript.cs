using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyScript : MonoBehaviour

{

    #region game_components
    private Rigidbody2D rb;
    private Animator anim;
    #endregion

    #region movement_variables
    public float movespeed;
    #endregion

    #region targeting_variables
    public Transform player;
    public bool trackPlayer;
    #endregion

    #region attack_variables
    private bool isAttacking;
    private bool readyToAttack;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float attackspeed;
    float attackTimer;
    [SerializeField]
    private float hitboxTiming;
    [SerializeField]
    private float endAnimationTiming;
    #endregion

    #region
    private float health;
    #endregion

    #region Unity_Functions
    void Start()
    {
        //Spawn Enemy with a random y position, at x == 10, and a movespeed
        trackPlayer = false;
        isAttacking = false;
        attackTimer = 2.0f;
        damage = 1;
        health = 2;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.position = new Vector2(10f, Random.Range(-5f, 5f));
        //Set velocity vector of Enemy based off of start and end locations
        Vector2 movementVector = new Vector2(transform.position.x, transform.position.y);
        rb.velocity = movementVector.normalized * movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            return;
        }

        if (player == null)
        {
            trackPlayer = false;
        }

        if (attackTimer <= 0 && trackPlayer)
        {
            Attack();
        }
        else if (trackPlayer)
        {
            MoveTowardsPlayer();
        } else
        {
            MoveAcrossScreen();
        }

        attackTimer -= Time.deltaTime;
    }
    #endregion

    #region movement_functions
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


    private void MoveTowardsPlayer()
    {
        // Calculate movement vector. Player pos - Enemy pos = Direction of player relative to enemy
        Vector2 direction = (player.position + Vector3.right - (Vector3.up * 0.5f)) - transform.position;

        Debug.Log("direction: " + direction);
        if (Mathf.Abs(direction.x) < 0.1f && Mathf.Abs(direction.y) < 0.1f)
        {
            rb.velocity = Vector2.zero;
        } else
        {
            rb.velocity = direction.normalized * movespeed;
        }
    }
    #endregion

    #region attack_functions
    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Trigger animation
        anim.SetBool("Attacking", true);

        yield return new WaitForSeconds(hitboxTiming);

        Debug.Log("Cast hitbox now");

        // Create hitbox
        RaycastHit2D[] hits = Physics2D.BoxCastAll(rb.position + Vector2.left, Vector2.one, 0f, Vector2.zero, 0);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Tons of damage");
                hit.transform.GetComponent<PlayerController>().TakeDamage(damage);
            }
        }

        // Wait for animation to end
        yield return new WaitForSeconds(endAnimationTiming);

        anim.SetBool("Attacking", false);
        isAttacking = false;
        attackTimer = 2.0f;
    }
    #endregion

    #region health_functions
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            FindObjectOfType<Score>().AddScore(20);
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion
}

