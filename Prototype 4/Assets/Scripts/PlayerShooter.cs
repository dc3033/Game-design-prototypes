using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {

    public float fireRate;

    public GameObject director;
    public GameObject bullet;
    public GameObject spbullet;

    private int bulletcount = 1;

    private float nextFire;

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            var position = new Vector3(0, 0.5f, 0);
            if (director.activeSelf)
            {
                position = director.transform.position;
            }
            if (bulletcount != 4)
            {
                bulletcount += 1;
                var go = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
                go.transform.LookAt(position);
                Debug.Log(position);
            }
            else
            {
                bulletcount = 1;
                var go = Instantiate(spbullet, transform.position, Quaternion.identity) as GameObject;
                go.transform.LookAt(position);
                Debug.Log(position);
            }
        }
    }
}
