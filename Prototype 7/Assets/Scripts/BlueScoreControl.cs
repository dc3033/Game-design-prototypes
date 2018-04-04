using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueScoreControl : MonoBehaviour {

    public GameObject gameEndText;
    private EndTextControl etc;

    public int score = 0;

    private Text bsc;

	// Use this for initialization
	void Start () {
        bsc = GetComponent<Text>();
        bsc.text = "Score: 0";
        etc = gameEndText.GetComponent<EndTextControl>();
	}
	
	// Update is called once per frame
	void Update () {
        string scoretxt = "Score: " + score;
        bsc.text = scoretxt;
	}
}
