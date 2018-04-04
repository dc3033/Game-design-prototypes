using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour {

    public GameObject laser;
    private LaserControl lc;

    //0 = neutral, 1 = blue, 2 = red
    public int status = 0;

	// Use this for initialization
	void Start () {
        lc = laser.GetComponent<LaserControl>();
	}
	
	// Update is called once per frame
	void Update () {
        status = lc.status;
	}
}
