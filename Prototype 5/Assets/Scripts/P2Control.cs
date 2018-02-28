using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class P2Control : MonoBehaviour {

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
    private Rigidbody p1rb;

    public GameObject opponent;
    private P1Control p1script;

    public Text p2text;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        p1rb = opponent.GetComponent<Rigidbody>();
        p1script = opponent.GetComponent<P1Control>();
        p2text.text = "Player 2\nScore: " + score + "\n\nActive buffs: ";
    }
	
	// Update is called once per frame
	void Update ()
    {
        cooldown += Time.deltaTime;
        int diff = Mathf.FloorToInt(chargetime - cooldown);
        string chargetext;
        if (chargetime - cooldown <= 0) chargetext = "\n\nCharge ready!\nPress [0] to use";
        else chargetext = "\n\nCharge ready\nin " + diff + " seconds";
        string stats = "Player 2\nScore: " + score + "\n\nActive buffs: ";
        if (triplespeed) stats += "\nTriple speed";
        if (triplemass) stats += "\nTriple mass";
        if (triplecharge) stats += "\nHalved cooldown";
        stats += chargetext;
        p2text.text = stats;
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal2");
        float moveY = Input.GetAxis("Vertical2");

        if (Input.GetKeyDown(KeyCode.Keypad0) && chargetime - cooldown <= 0)
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
            if (triplespeed) p1script.score += 1;
            else if (p1script.triplespeed)
            {
                p1script.score += 2;
                p1script.triplespeed = false;
                p1script.speed /= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.red;
            }
            else
            {
                p1script.score += 2;
                triplespeed = true;
                speed *= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.yellow;
            }
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (other.gameObject.CompareTag("Masspit"))
        {
            if (triplemass) p1script.score += 1;
            else if (p1script.triplemass)
            {
                p1script.score += 2;
                p1script.triplemass = false;
                p1rb.mass /= 3;
                p1script.speed /= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.red;
            }
            else
            {
                p1script.score += 2;
                triplemass = true;
                rb.mass *= 3;
                speed *= 3;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.yellow;
            }
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (other.gameObject.CompareTag("Chargepit"))
        {
            if (triplecharge) p1script.score += 1;
            else if (p1script.triplecharge)
            {
                p1script.score += 2;
                p1script.triplecharge = false;
                p1script.chargetime *= 2;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.red;
            }
            else
            {
                p1script.score += 2;
                triplecharge = true;
                chargetime /= 2;
                Renderer objcolor = other.gameObject.GetComponent<Renderer>();
                objcolor.sharedMaterial.color = Color.yellow;
            }
            transform.position = new Vector3(0, 10, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
