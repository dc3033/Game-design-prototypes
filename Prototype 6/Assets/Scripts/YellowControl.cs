using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowControl : MonoBehaviour {

    public float speed = 5;
    bool facingRight = true;

    Rigidbody2D rb;

    Animator anim;

    bool powering = false;
    bool charging = false;
    bool chargeleft = false;
    bool grounded = false;
    public bool stunned = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpforce = 700;
    public float chargepower = 20;
    private float chargetime = 0;
    public float stuntime = 0;

    public GameObject blue;
    private BlueControl bc;

    public AudioSource walk;
    public AudioSource jump;
    public AudioSource powerup;
    public AudioSource crash;
    public AudioSource hit;
    public AudioSource charge;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = blue.GetComponent<BlueControl>();
    }

    void Update()
    {
        if (grounded && !charging && !stunned && Input.GetKeyDown(KeyCode.Keypad8))
        {
            anim.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpforce));
            jump.Play();
            walk.Stop();
        }
        if (!charging && !stunned && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            powering = true;
            anim.SetBool("powering", true);
            powerup.Play();
            walk.Stop();
        }
        if (powering)
        {
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                anim.SetBool("charging", true);
                anim.SetBool("powering", false);
                powering = false;
                charging = true;
                chargeleft = true;
                chargetime = 0;
                charge.Play();
                powerup.Stop();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                anim.SetBool("charging", true);
                anim.SetBool("powering", false);
                powering = false;
                charging = true;
                chargeleft = false;
                chargetime = 0;
                charge.Play();
                powerup.Stop();
            }
        }
        if (grounded && !charging && !stunned && (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Keypad6)))
        {
            walk.Play();
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vspeed", rb.velocity.y);

        float moveX = Input.GetAxis("Horizontal2");

        anim.SetFloat("speed", Mathf.Abs(moveX));

        if (stunned)
        {
            if (stuntime < 0.5)
            {
                anim.SetBool("stunned", true);
                stuntime += Time.deltaTime;
            }
            else
            {
                stunned = false;
                anim.SetBool("stunned", false);
            }
        }

        else if (powering)
        {
            rb.velocity = new Vector2(0, 0.6f);
        }

        else if (charging)
        {
            Camera.main.GetComponent<CameraControl>().Shake(0.05f, 2, 8);
            chargetime += Time.deltaTime;
            if (chargeleft)
            {
                rb.velocity = new Vector2(-chargepower, 0.6f);
            }
            else
            {
                rb.velocity = new Vector2(chargepower, 0.6f);
            }
            if (chargetime > 1)
            {
                charging = false;
                anim.SetBool("charging", false);
                stunned = true;
                stuntime = 0;
                if (facingRight)
                {
                    rb.AddForce(new Vector2(-1200, 500));
                }
                else
                {
                    rb.AddForce(new Vector2(1200, 500));
                }
                Camera.main.GetComponent<CameraControl>().Shake(0.3f, 10, 8);
                crash.Play();
                charge.Stop();
            }
        }

        else
        {
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        }

        if (moveX > 0 && !facingRight) Flip();
        else if (moveX < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("blue") && charging)
        {
            bc.stunned = true;
            bc.stuntime = -1f;
            hit.Play();
            bc.walk.Stop();
        }
    }

}
