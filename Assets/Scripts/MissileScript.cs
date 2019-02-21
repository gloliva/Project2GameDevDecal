using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour

{
    Rigidbody2D rb;
    public float speed;
    public float maxHealth;
    float currHealth;
    public GameObject explosionObj;
    float damage;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject obj = coll.gameObject;
        // Explodes upon contacting player
        if (obj.CompareTag("Ranged Enemy"))
        {
            coll.gameObject.GetComponent<RangedEnemyScript>().TakeDamage(damage);
            Instantiate(explosionObj, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
            currHealth -= 2;
            if (currHealth <= 0)
            {
                Destroy(gameObject);
            }
        } else if (obj.CompareTag("Fireball"))
        {
            Instantiate(explosionObj, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
            Destroy(obj);
            currHealth -= 1;
            if (currHealth <= 0)
            {
                Destroy(gameObject);
            }
        } else if (obj.CompareTag("Melee Enemy"))
        {
            coll.gameObject.GetComponent<MeleeEnemyScript>().TakeDamage(damage);
            Instantiate(explosionObj, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
            currHealth -= 2;
            if (currHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public void SetDamage(float value)
    {
        this.damage = value;
    }
}
