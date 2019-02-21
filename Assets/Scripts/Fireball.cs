using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public GameObject explosionObj;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, 90));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        // Explodes upon contacting player
        if (isPlayer(coll.gameObject))
        {
            coll.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Instantiate(explosionObj, coll.gameObject.transform.position, coll.gameObject.transform.rotation);
            FindObjectOfType<AudioManager>().Play("Explosion1");
            Destroy(gameObject);
        }

    }

    public void SetDamage(float value)
    {
        this.damage = value;
    }

    //Returns whether or not the Game Object is the Player Character
    bool isPlayer(GameObject obj)
    {
        return obj.CompareTag("Player");
    }
}
