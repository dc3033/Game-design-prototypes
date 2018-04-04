using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemControl : MonoBehaviour {

    public GameObject blueScore;
    public GameObject redScore;
    private BlueScoreControl bs;
    

    public GameObject holder;
    public bool isHeld = false;

	// Use this for initialization
	void Start ()
    {
        blueScore = GameObject.Find("BlueScore");
        bs = blueScore.GetComponent<BlueScoreControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isHeld == true)
        {
            transform.position = holder.transform.position;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BFlag") && holder.CompareTag("Blue"))
        {
            bs.score += 1;
            if (holder == GameObject.Find("Blue1"))
            {
                Blue1Control b1c = holder.GetComponent<Blue1Control>();
                b1c.hasGem = false;
            }
            else if (holder == GameObject.Find("Blue2"))
            {
                Blue2Control b2c = holder.GetComponent<Blue2Control>();
                b2c.hasGem = false;
            }
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("RFlag") && holder.CompareTag("Red"))
        {
            bs.score += 1;
            if (holder == GameObject.Find("Red1"))
            {
                Red1Control r1c = holder.GetComponent<Red1Control>();
                r1c.hasGem = false;
            }
            else if (holder == GameObject.Find("Red2"))
            {
                Red2Control r2c = holder.GetComponent<Red2Control>();
                r2c.hasGem = false;
            }
            gameObject.SetActive(false);
        }
    }
}
