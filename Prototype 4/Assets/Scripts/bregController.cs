using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bregController : MonoBehaviour {

    public int speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("BulletBoss"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Spawn"))
        {
            Destroy(gameObject);
        }
    }
}
