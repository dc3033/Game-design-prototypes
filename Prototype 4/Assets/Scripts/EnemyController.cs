using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameObject p1;
    private GameObject p2;

    public float speed;

	// Use this for initialization
	void Start () {
        p1 = GameObject.Find("Player 1");
        p2 = GameObject.Find("Player 2");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        float p1dist = Vector3.Distance(p1.transform.position, transform.position);
        float p2dist = Vector3.Distance(p2.transform.position, transform.position);
        if (p1dist < p2dist)
        {
            transform.position = Vector3.MoveTowards(transform.position, p1.transform.position, speed);
        }
        else if (p2dist < p1dist)
        {
            transform.position = Vector3.MoveTowards(transform.position, p2.transform.position, speed);
        }
	}
}
