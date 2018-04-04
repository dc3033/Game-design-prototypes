using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {

    public Sprite lasY;
    public Sprite lasB;
    public Sprite lasR;

    private SpriteRenderer sr;

    //0 = neutral, 1 = blue, 2 = red
    public int status = 0;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (status == 0)
        {
            sr.sprite = lasY;
        }
        else if (status == 1)
        {
            sr.sprite = lasB;
        }
        else if (status == 2)
        {
            sr.sprite = lasR;
        }
	}
}
