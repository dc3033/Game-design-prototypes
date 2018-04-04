using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : MonoBehaviour {

    public GameObject blast;
    public GameObject holder;
    public bool primed = false;

    public float fuze = 0;

	// Use this for initialization
	void Start () {
        primed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (primed == false)
        {
            transform.position = holder.transform.position;
        }
        else if (fuze < 1f)
        {
            fuze += 0.01f;
        }
        else if (fuze >= 1f)
        {
            primed = false;
            fuze = 0;
            Instantiate(blast, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
	}
}
