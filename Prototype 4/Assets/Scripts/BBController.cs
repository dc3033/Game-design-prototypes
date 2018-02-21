using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBController : MonoBehaviour {

    public int speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Spawn"))
        {
            Destroy(gameObject);
        }
    }
}
