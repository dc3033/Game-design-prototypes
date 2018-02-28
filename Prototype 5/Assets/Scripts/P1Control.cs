using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class P1Control : MonoBehaviour {

    public float speed;
    public int score = 0;
    public float chargetime = 4;
    private float cooldown = 0;
    public float chargepower = 5;

    public bool triplespeed = false;
    public bool triplemass = false;
    public bool triplecharge = false;
    private bool charging = false;

    private Rigidbody rb;
    private Rigidbody p2rb;

    public GameObject opponent;
    private P2Control p2script;

    public Text p1text;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        p2rb = opponent.GetComponent<Rigidbody>();
        p2script = opponent.GetComponent<P2Control>();
        p1text.text = "Player 1\nScore: " + score + "\n\nActive buffs: ";
	}
	
	// Update is called once per frame
	void Update ()
    {
        cooldown += Time.deltaTime;
        int diff = Mathf.FloorToInt(chargetime - cooldown);
        string chargetext;
        if (chargetime - cooldown <= 0) chargetext = "\n\nCharge ready!\nPress SPACE to use";
        else chargetext = "\n\nCharge ready\nin " + diff + " seconds";
        string stats = "Player 1\nScore: " + score + "\n\nActive buffs: ";
        if (triplespeed) stats += "\nTriple speed";
        if (triplemass) stats += "\nTriple mass";
        if (triplecharge) stats += "\nHalved cooldown";
        stats += chargetext;
        p1text.text = stats;
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && chargetime - cooldown <= 0)
        {
            charging = true;
            cooldown = 0;
        }

        rb.AddForce(moveX * speed, -9.81f, moveY * speed);

        if (charging)
        {
            rb.velocity += new Vector3(moveX * chargepower, 0, moveY * chargepower);
            charging = false;
        }

        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Speedpit"))
        {
            if (triplespeed) p2script.score += 1;
            else if (p2script.triplespeed)
            {
                p2script.score += 2;
                p2script.triplespeed = false;
                p2script.speed /= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.red;
            }
            else
            {
                p2script.score += 2;
                triplespeed = true;
                speed *= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.magenta;
            }
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (other.gameObject.CompareTag("Masspit"))
        {
            if (triplemass) p2script.score += 1;
            else if (p2script.triplemass)
            {
                p2script.score += 2;
                p2script.triplemass = false;
                p2rb.mass /= 3;
                p2script.speed /= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.red;
            }
            else
            {
                p2script.score += 2;
                triplemass = true;
                rb.mass *= 3;
                speed *= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.magenta;
            }
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (other.gameObject.CompareTag("Chargepit"))
        {
            if (triplecharge) p2script.score += 1;
            else if (p2script.triplecharge)
            {
                p2script.score += 2;
                p2script.triplecharge = false;
                p2script.chargetime *= 2;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.red;
            }
            else
            {
                p2script.score += 2;
                triplecharge = true;
                chargetime /= 2;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.magenta;
            }
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
