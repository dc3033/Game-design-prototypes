using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagControl : MonoBehaviour {

    public Sprite FlagN;
    public Sprite FlagB;
    public Sprite FlagY;

    public GameObject bluescorebar;
    public GameObject yellowscorebar;
    private Score bluescore;
    private Score yellowscore;

    private int state;
    private float captime;
    private float scoretime;

    private SpriteRenderer sr;

    public AudioSource change;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        bluescore = bluescorebar.GetComponent<Score>();
        yellowscore = yellowscorebar.GetComponent<Score>();
        state = 0;
        captime = 0;
        scoretime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (scoretime >= 3)
        {
            if (state == 1)
            {
                bluescore.score += 1;
                scoretime = 0;
            }
            else if (state == 2)
            {
                yellowscore.score += 1;
                scoretime = 0;
            }
        }
        scoretime += Time.deltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("blue"))
        {
            Debug.Log("blue");
            if (captime >= 0.0)
            {
                if (state == 0)
                {
                    sr.sprite = FlagB;
                    state = 1;
                    scoretime = 0;
                    change.Play();
                }
                else if (state == 2)
                {
                    sr.sprite = FlagN;
                    state = 0;
                    captime = 0;
                    change.Play();
                }
            }
            else
            {
                captime += Time.deltaTime;
            }
        }
        else if (collision.gameObject.CompareTag("yellow"))
        {
            Debug.Log("yellow");
            if (captime >= 0.0)
            {
                if (state == 0)
                {
                    sr.sprite = FlagY;
                    state = 2;
                    scoretime = 0;
                    change.Play();
                }
                else if (state == 1)
                {
                    sr.sprite = FlagN;
                    state = 0;
                    captime = 0;
                    change.Play();
                }
            }
            else
            {
                captime += Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("other");
            captime = 0;
        }
    }
}
