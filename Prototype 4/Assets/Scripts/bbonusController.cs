using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bbonusController : MonoBehaviour {

    public float speed;
    private int health = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
        if (health <= 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("BulletBoss"))
        {
            Destroy(other.gameObject);
            health -= 1;
        }
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Spawn"))
        {
            Destroy(gameObject);
        }
    }
}
