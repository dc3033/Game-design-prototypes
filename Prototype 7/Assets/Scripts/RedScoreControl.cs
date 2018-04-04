using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedScoreControl : MonoBehaviour {

    public GameObject gameEndText;
    private EndTextControl etc;

    public int score = 0;

    private Text rsc;

    // Use this for initialization
    void Start()
    {
        rsc = GetComponent<Text>();
        rsc.text = "Score: 0";
        etc = gameEndText.GetComponent<EndTextControl>();
    }

    // Update is called once per frame
    void Update()
    {
        string scoretxt = "Score: " + score;
        rsc.text = scoretxt;
    }
}
