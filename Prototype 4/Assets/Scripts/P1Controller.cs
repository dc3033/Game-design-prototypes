using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class P1Controller : MonoBehaviour {

    public float speed;
    public Text hptext;

    public int health = 3;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        hptext.text = "Player 1 Health: " + health.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        hptext.text = "Player 1 Health: " + health.ToString();
        if (health <= 0) { gameObject.SetActive(false); }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0.0f, moveY);

        rb.velocity = (movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("BulletBoss") || other.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            Destroy(other.gameObject);
        }
    }
}
