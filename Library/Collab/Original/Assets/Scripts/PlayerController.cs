using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;

    float xAxis;
    float yAxis;

    #region health_variables
    public float maxHealth;
    float currHealth;
    public SimpleHealthBar healthBar;
    #endregion

    #region projectile_variables
    public GameObject projectile;
    public float fireSpeed;
    public float reloadTime;
    public float damage;
    float reloadTimer = 0;
    public bool specialAvailable;
    #endregion

    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();

        currHealth = maxHealth;

        healthBar.UpdateBar(currHealth, maxHealth);

        specialAvailable = false;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveFunction();
        reloadTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.K) && specialAvailable)
        {
            ShootSpecialAttack();
        }
    }

    void moveFunction()
    {
        Vector2 movementVector = new Vector2(xAxis, yAxis);
        movementVector = movementVector * 4;
        playerRigidbody.velocity = movementVector;
    }

    void ShootProjectile()
    {
        if (reloadTimer > reloadTime)
        {
            reloadTimer = 0;
            Vector2 FireDirection = new Vector2(1, 0);
            FireDirection = FireDirection.normalized * fireSpeed;

            // Create Missile
            GameObject missile = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            missile.GetComponent<Rigidbody2D>().velocity = FireDirection;
            missile.GetComponent<MissileScript>().SetDamage(damage);
            FindObjectOfType<AudioManager>().Play("Missile");

            Destroy(missile, 5);
        }
    }

    void ShootSpecialAttack()
    {
        if (reloadTimer > reloadTime)
        {
            reloadTimer = 0;
            Vector2 FireDirection = new Vector2(1, 0);
            FireDirection = FireDirection.normalized * fireSpeed;

            // Create Missiles
            GameObject missile1 = (GameObject)Instantiate(projectile, transform.position + (Vector3.up * 2.0f), transform.rotation);
            missile1.GetComponent<Rigidbody2D>().velocity = FireDirection;
            missile1.GetComponent<MissileScript>().SetDamage(damage);

            GameObject missile2 = (GameObject)Instantiate(projectile, transform.position + (Vector3.up * 1.0f), transform.rotation);
            missile2.GetComponent<Rigidbody2D>().velocity = FireDirection;
            missile2.GetComponent<MissileScript>().SetDamage(damage);

            GameObject missile3 = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            missile3.GetComponent<Rigidbody2D>().velocity = FireDirection;
            missile3.GetComponent<MissileScript>().SetDamage(damage);

            GameObject missile4 = (GameObject)Instantiate(projectile, transform.position + (Vector3.down * 1.0f), transform.rotation);
            missile4.GetComponent<Rigidbody2D>().velocity = FireDirection;
            missile4.GetComponent<MissileScript>().SetDamage(damage);

            GameObject missile5 = (GameObject)Instantiate(projectile, transform.position + (Vector3.down * 2.0f), transform.rotation);
            missile5.GetComponent<Rigidbody2D>().velocity = FireDirection;
            missile5.GetComponent<MissileScript>().SetDamage(damage);

            FindObjectOfType<AudioManager>().Play("Missile");

            Destroy(missile1, 5);
            Destroy(missile2, 5);
            Destroy(missile3, 5);
            Destroy(missile4, 5);
            Destroy(missile5, 5);

            specialAvailable = false;
        }
    }

    public void TakeDamage(float value)
    {
        currHealth -= value;
        healthBar.UpdateBar(currHealth, maxHealth);

        if (currHealth <= 0)
        {
            if (FindObjectOfType<Score>().m_Score > FindObjectOfType<Highscore>().m_Score)
            {
                FindObjectOfType<Highscore>().SetScore(FindObjectOfType<Score>().m_Score);
                Data.highscore = FindObjectOfType<Highscore>().m_Score;
            }
            Die();
            Debug.Log(FindObjectOfType<Button>().gameObject.name);
            
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}

