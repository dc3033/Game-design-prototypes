using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastControl : MonoBehaviour {

    public float lifetime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lifetime += 0.01f;
        if (lifetime > 0.5f) Destroy(gameObject);
	}

}
