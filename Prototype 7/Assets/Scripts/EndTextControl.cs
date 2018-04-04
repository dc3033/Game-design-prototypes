using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTextControl : MonoBehaviour {

    public GameObject bscore;
    private BlueScoreControl bsc;
    public GameObject rscore;
    private RedScoreControl rsc;

    public GameObject timer;
    private TimerControl tc;

    private Text egt;

	// Use this for initialization
	void Start () {
        egt = gameObject.GetComponent<Text>();
        egt.text = "";
        bsc = bscore.GetComponent<BlueScoreControl>();
        rsc = rscore.GetComponent<RedScoreControl>();
        tc = timer.GetComponent<TimerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if (bsc.score > 2)
        {
            egt.text = "Blue wins!";
        }
        else if (rsc.score > 2)
        {
            egt.text = "Red wins!";
        }
        else if (tc.timeUp == true)
        {
            if (bsc.score > rsc.score)
            {
                egt.text = "Blue wins!";
            }
            else if (rsc.score > bsc.score)
            {
                egt.text = "Red wins!";
            }
            else if (rsc.score == bsc.score)
            {
                egt.text = "It's a draw!";
            }
        }
	}
}
