using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {

    public GameObject ysb;
    public GameObject bsb;
    private Score ys;
    private Score bs;

    private bool iswin = false;
    private Text wintext;

	// Use this for initialization
	void Start () {
        ys = ysb.GetComponent<Score>();
        bs = bsb.GetComponent<Score>();
        wintext = GetComponent<Text>();
        wintext.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (!iswin)
        {
            if (ys.score >= 5 && bs.score < 50)
            {
                wintext.text = "                    Yellow Wins!\nPress Esc to return to the main menu";
                iswin = true;
            }
            else if (bs.score >= 5 && ys.score < 50)
            {
                wintext.text = "                    Blue Wins!\nPress Esc to return to the main menu";
                iswin = true;
            }
            else if (ys.score >= 50 && bs.score >= 50)
            {
                wintext.text = "                    It's a draw!\nPress Esc to return to the main menu";
                iswin = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu");
            }
        }
	}
}
