using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red2Control : MonoBehaviour {

    public GameObject bomb;
    public GameObject mybomb;

    public float speed;
    private float climbspeed;
    private float moveY;
    public float interactCharge;

    Vector3 spawnpoint = new Vector3(-2, 6.6f, 0);

    Rigidbody2D rb;

    public bool hasGem = false;
    public bool hasBomb = true;
    public bool interacting = false;
    public bool atCannon = false;
    public bool atSwitch = false;
    public bool atSupply = false;
    public bool atGem = false;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        mybomb = Instantiate(bomb, transform.position, Quaternion.identity);
        BombControl bc = mybomb.GetComponent<BombControl>();
        bc.holder = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact3"))
        {
            interacting = true;
            interactCharge += 0.01f;
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetButtonUp("Interact3"))
        {
            interacting = false;
            interactCharge = 0;
        }
        if (interactCharge >= 0.5f && !hasGem && hasBomb && !atGem && !atSupply && !atSwitch && !atCannon)
        {
            hasBomb = false;
            BombControl bc = mybomb.GetComponent<BombControl>();
            bc.primed = true;
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal3");
        moveY = Input.GetAxis("Vertical3");

        if (interacting == false)
        {
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blue") || collision.gameObject.CompareTag("Red"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Blast"))
        {
            transform.position = spawnpoint;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //climbing ladders and ropes
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

        //interaction with gem
        if (collision.gameObject.CompareTag("Gem"))
        {
            atGem = true;
            if (interactCharge >= 0.5f)
            {
                Debug.Log("gem");
                if (hasGem == false)
                {
                    GemControl gemScript = collision.gameObject.GetComponent<GemControl>();
                    if (gemScript.isHeld == false)
                    {
                        hasGem = true;
                        gemScript.isHeld = true;
                        gemScript.holder = gameObject;
                    }
                }
                else
                {
                    hasGem = false;
                    GemControl gemScript = collision.gameObject.GetComponent<GemControl>();
                    gemScript.isHeld = false;
                }
                interactCharge = 0;
            }
        }
        //interaction with cannon
        else if (collision.gameObject.CompareTag("Cannon"))
        {
            atCannon = true;
            if (interactCharge >= 0.5f)
            {
                Debug.Log("cannon");
                CannonControl cc = collision.gameObject.GetComponent<CannonControl>();
                cc.status = 2;
                interactCharge = 0;
            }
        }
        //interaction with switch
        if (collision.gameObject.CompareTag("Switch"))
        {
            atSwitch = true;
            if (interactCharge >= 0.5f)
            {
                Debug.Log("switch");
                SwitchControl sc = collision.gameObject.GetComponent<SwitchControl>();
                sc.switchflip = 1;
                interactCharge = 0;
            }

        }
        //interaction with supply
        if (collision.gameObject.CompareTag("Supply"))
        {
            atSupply = true;
            if (interactCharge >= 0.5f)
            {
                if (!hasBomb)
                {
                    Debug.Log("supply");
                    hasBomb = true;
                    mybomb.SetActive(true);
                }
                interactCharge = 0;
            }
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

        if (collision.gameObject.CompareTag("Gem")) atGem = false;
        if (collision.gameObject.CompareTag("Cannon")) atCannon = false;
        if (collision.gameObject.CompareTag("Switch")) atSwitch = false;
        if (collision.gameObject.CompareTag("Supply")) atSupply = false;

    }
}
