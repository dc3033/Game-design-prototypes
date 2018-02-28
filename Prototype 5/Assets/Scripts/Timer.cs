using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public GameObject p1;
    public GameObject p2;

    private P1Control p1script;
    private P2Control p2script;

    private Rigidbody p1rb;
    private Rigidbody p2rb;

    private bool end = false;

    private float timer = 60;
    UnityEngine.UI.Text toptext;

	// Use this for initialization
	void Start () {
        p1script = p1.GetComponent<P1Control>();
        p2script = p2.GetComponent<P2Control>();
        p1rb = p1.GetComponent<Rigidbody>();
        p2rb = p2.GetComponent<Rigidbody>();
        toptext = gameObject.GetComponent<UnityEngine.UI.Text>();
        toptext.text = "" + 60;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        int roundtimer = Mathf.RoundToInt(timer);
        if (timer > 0) toptext.text = "" + roundtimer;
        else
        {
            RectTransform tpos = gameObject.GetComponent<RectTransform>();
            tpos.anchoredPosition = new Vector3(20, 0, 0);
            toptext.text = " TIME OVER";
            if (p1script.score > p2script.score) toptext.text += "\n\n\n\n\n\n\n\n Player 1 wins!";
            else if (p2script.score > p1script.score) toptext.text += "\n\n\n\n\n\n\n\n Player 2 wins!";
            else toptext.text += "\n\n\n\n\n\n\n\n It's a draw!";
            p1rb.velocity = new Vector3(0, 0, 0);
            p2rb.velocity = new Vector3(0, 0, 0);
        }
	}
}
