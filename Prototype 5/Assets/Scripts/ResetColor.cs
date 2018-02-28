using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Renderer objcolor = gameObject.GetComponent<Renderer>();
        objcolor.sharedMaterial.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
