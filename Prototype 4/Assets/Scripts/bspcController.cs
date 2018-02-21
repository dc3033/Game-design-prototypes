using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bspcController : MonoBehaviour {

    public float speed;
    public GameObject bonus;

    private float timealive = 0;
    private float secondsalive;
    private int health = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timealive += Time.deltaTime;
        secondsalive = timealive % 60;
        transform.position += transform.forward * Time.deltaTime * speed;
        if (health <= 0) { Destroy(gameObject); }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletSpc"))
        {
            Vector3 position = (transform.position + other.transform.position) / 2;
            var bonusOne = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            var bonusTwo = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            var bonusThree = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            var bonusFour = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            var bonusFive = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            var bonusSix = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            var bonusSeven = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            var bonusEight = Instantiate(bonus, position, other.transform.rotation) as GameObject;
            bonusTwo.transform.Rotate(0, 45, 0);
            bonusThree.transform.Rotate(0, 90, 0);
            bonusFour.transform.Rotate(0, 135, 0);
            bonusFive.transform.Rotate(0, 180, 0);
            bonusSix.transform.Rotate(0, 225, 0);
            bonusSeven.transform.Rotate(0, 270, 0);
            bonusEight.transform.Rotate(0, 315, 0);
            Destroy(other.gameObject);
            Destroy(gameObject);           
        }
        if (other.gameObject.CompareTag("A Player") && secondsalive > 0.5)
        {
            Vector3 position = (transform.position + other.transform.position) / 2;
            var bonusOne = Instantiate(bonus, position, transform.rotation) as GameObject;
            var bonusTwo = Instantiate(bonus, position, transform.rotation) as GameObject;
            var bonusThree = Instantiate(bonus, position, transform.rotation) as GameObject;
            bonusTwo.transform.Rotate(0, -90, 0);
            bonusThree.transform.Rotate(0, 90, 0);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("BulletBoss"))
        {
            Destroy(other.gameObject);
            health -= 1;
        }
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Spawn"))
        {
            Destroy(gameObject);
        }
    }
}
