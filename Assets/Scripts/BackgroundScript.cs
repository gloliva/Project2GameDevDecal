using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-2, 0);
        if (transform.position.x <= -32)
        {
            transform.position = new Vector3(29.3f * 2f + transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
