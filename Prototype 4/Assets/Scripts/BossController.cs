using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossController : MonoBehaviour {

    public GameObject p1;
    public GameObject p2;

    public GameObject bullet;
    public float firerate;

    public Text winText;
    public Text loseText;

    private int health = 120;

    private float timer = 0.0f;
    private float seconds;
    private float nextFire;

	// Use this for initialization
	void Start () {
        winText.text = "";
        loseText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (!p1.activeSelf && !p2.activeSelf)
        {
            loseText.text = "Game Over";
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            winText.text = "Stage Clear";
        }
        timer += Time.deltaTime;
        seconds = timer % 60;
        Vector3 position = new Vector3(0, 0.5f, 0);
        if (health > 60 && health <= 90 || health > 60 && seconds >= 30 && seconds < 60)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + firerate;
                var bullet1 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet2 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet3 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet4 = Instantiate(bullet, position, transform.rotation) as GameObject;
                bullet2.transform.Rotate(0, 90, 0);
                bullet3.transform.Rotate(0, 180, 0);
                bullet4.transform.Rotate(0, 270, 0);
            }
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        }
        else if (health > 30 && health <= 60 || health > 30 && seconds >= 60 && seconds < 90)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + firerate;
                var bullet1 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet2 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet3 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet4 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet5 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet6 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet7 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet8 = Instantiate(bullet, position, transform.rotation) as GameObject;
                bullet2.transform.Rotate(0, 45, 0);
                bullet3.transform.Rotate(0, 90, 0);
                bullet4.transform.Rotate(0, 135, 0);
                bullet5.transform.Rotate(0, 180, 0);
                bullet6.transform.Rotate(0, 225, 0);
                bullet7.transform.Rotate(0, 270, 0);
                bullet8.transform.Rotate(0, 315, 0);
            }
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        }
        else if (health < 30 || seconds >= 90)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + firerate - 0.4f;
                var bullet1 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet2 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet3 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet4 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet5 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet6 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet7 = Instantiate(bullet, position, transform.rotation) as GameObject;
                var bullet8 = Instantiate(bullet, position, transform.rotation) as GameObject;
                bullet2.transform.Rotate(0, 45, 0);
                bullet3.transform.Rotate(0, 90, 0);
                bullet4.transform.Rotate(0, 135, 0);
                bullet5.transform.Rotate(0, 180, 0);
                bullet6.transform.Rotate(0, 225, 0);
                bullet7.transform.Rotate(0, 270, 0);
                bullet8.transform.Rotate(0, 315, 0);
            }
            transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BulletSpc") || other.gameObject.CompareTag("BulletBonus")) {
            health -= 1;
            Destroy(other.gameObject);
        }
    }
}
