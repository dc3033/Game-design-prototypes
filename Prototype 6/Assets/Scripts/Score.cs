using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    public float score;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (score < 50)
        {
            gameObject.transform.localScale = new Vector3(score / 10, 0.5f, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(5, 0.5f, 1);
        }

	}
}
