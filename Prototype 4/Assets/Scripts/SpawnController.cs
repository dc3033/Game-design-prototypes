using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public GameObject boss;
    public GameObject enemy;

    public float spawnrate;

    private float nextSpawn;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn && boss.activeSelf)
        {
            nextSpawn = Time.time + spawnrate;
            var spawn = Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
        }
	}
}
