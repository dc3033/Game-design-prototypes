using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour {

    private Text timeText;
    public float timeLimit = 90;
    private float shownTime = 90;

    public bool timeUp = false;

	// Use this for initialization
	void Start () {
        timeText = gameObject.GetComponent<Text>();
        timeText.text = "" + shownTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            shownTime = Mathf.Floor(timeLimit);
        }
        timeText.text = "" + shownTime;
        if (timeLimit <= 0) timeUp = true;
	}
}
