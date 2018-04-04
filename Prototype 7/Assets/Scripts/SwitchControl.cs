using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {

    public GameObject gate1;
    public GameObject gate2;

    public Sprite notflippedsp;
    public Sprite flippedsp;

    public bool flipped = false;
    public int switchflip = 0;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        gate1 = GameObject.Find("Gate1");
        gate2 = GameObject.Find("Gate2");
        gate1.SetActive(true);
        gate2.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (flipped)
        {
            sr.sprite = flippedsp;
        }
        else
        {
            sr.sprite = notflippedsp;
        }
        if (gate1.activeSelf && switchflip == 1)
        {
            gate1.SetActive(false);
            gate2.SetActive(false);
            switchflip = -1;
            flipped = !flipped;
        }
        else if (!gate1.activeSelf && switchflip == 1)
        {
            gate1.SetActive(true);
            gate2.SetActive(true);
            switchflip = -2;
            flipped = !flipped;
        }
	}
}
