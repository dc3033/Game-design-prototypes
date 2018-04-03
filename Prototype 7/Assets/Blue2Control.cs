using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue2Control : MonoBehaviour {

    public float speed;
    private float climbspeed;
    private float moveY;

    Rigidbody2D rb;

    private bool hasGem = false;
    private bool cantBomb = false;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal2");
        moveY = Input.GetAxis("Vertical2");

        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Ladder")) || (collision.gameObject.CompareTag("Rope") && hasGem == false))
        {
            climbspeed = 3;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, moveY * climbspeed);
        }
        else if (collision.gameObject.CompareTag("Rope") && hasGem)
        {
            climbspeed = 1.5f;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, moveY * climbspeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder") || collision.gameObject.CompareTag("Rope"))
        {
            climbspeed = 0;
            rb.gravityScale = 2;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
