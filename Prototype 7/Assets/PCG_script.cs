using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCG_script : MonoBehaviour
{

    public GameObject laserCannon;
    public GameObject laser;
    public GameObject ladder;
    public GameObject supply;
    public GameObject gateSwitch;
    public GameObject floor;

    //spacing between floors is 2
    //ladder is 2 ladder objects stacked on top of each other, ypos for higher ladder is 1 more than lower ladder, ypos for lower ladder is same as other objects
    private float[] fxpos = { -5.5f, -2.5f, 2.5f, 5.5f };
    private float[] oxpos = { -4.5f, -1.5f, 1.5f, 4.5f };
    private float l1fypos = 5.73f;
    private float l1oypos = 4.5f;

    private int emptyCount = 0;
    private int cannonCount = 0;
    private int switchCount = 0;
    private int supplyCount = 0;

    void fillMap()
    {
        //loop through each floor
        for (int i = 0; i < 6; i++)
        {
            //choose position of ladder on the floor
            int lpos = Random.Range(0, 4);
            for (int j = 0; j < 4; j++)
            {
                if (lpos == j)
                {
                    var ladlow = Instantiate(ladder, new Vector3(fxpos[j], l1oypos - 2 * i, 0), Quaternion.identity);
                    var ladhigh = Instantiate(ladder, new Vector3(fxpos[j], l1oypos - 2 * i + 1, 0), Quaternion.identity);
                }
                else
                {
                    var nolad = Instantiate(floor, new Vector3(fxpos[j], l1fypos - 2 * i, 0), Quaternion.identity);
                }
            }

            //add objects to be randomly chosen, 1 = empty, 2 = lasercannon, 3 = switch, 4 = supply
            List<int> choices = new List<int>();
            if (emptyCount < 4) choices.Add(1);
            if (cannonCount < 4) choices.Add(2);
            if (switchCount < 2) choices.Add(3);
            if (supplyCount < 2) choices.Add(4);

            //place first object
            int firstpick = choices[Random.Range(0, choices.Count)];
            int firstpos = Random.Range(0, 2);
            if (firstpick == 1)
            {
                emptyCount += 1;
            }
            else if (firstpick == 2)
            {
                cannonCount += 1;
                choices.Remove(2);
                var canInstance = Instantiate(laserCannon, new Vector3(oxpos[firstpos], l1oypos - 2 * i, 0), Quaternion.identity);
                canInstance.transform.Rotate(0, 0, -90);
                if (Random.Range(0, 2) == 0)
                {
                    var laserleft = Instantiate(laser, new Vector3(oxpos[firstpos] - 8, l1oypos - 2 * i, 0), Quaternion.identity);
                }
                else
                {
                    Vector3 theScale = canInstance.transform.localScale;
                    theScale.y *= -1;
                    canInstance.transform.localScale = theScale;
                    var laserright = Instantiate(laser, new Vector3(oxpos[firstpos] + 8, l1oypos - 2 * i, 0), Quaternion.identity);
                }
            }
            else if (firstpick == 3)
            {
                switchCount += 1;
                choices.Remove(3);
                var switchInstance = Instantiate(gateSwitch, new Vector3(oxpos[firstpos], l1oypos - 2 * i, 0), Quaternion.identity);
            }
            else if (firstpick == 4)
            {
                supplyCount += 1;
                choices.Remove(4);
                var supplyInstance = Instantiate(supply, new Vector3(oxpos[firstpos], l1oypos - 2 * i, 0), Quaternion.identity);
            }

            //place second object
            int secondpick = choices[Random.Range(0, choices.Count)];
            int secondpos = Random.Range(2, 4);
            if (secondpick == 1)
            {
                emptyCount += 1;
            }
            else if (secondpick == 2)
            {
                cannonCount += 1;
                choices.Remove(2);
                var canInstance = Instantiate(laserCannon, new Vector3(oxpos[secondpos], l1oypos - 2 * i, 0), Quaternion.identity);
                canInstance.transform.Rotate(0, 0, -90);
                if (Random.Range(0, 2) == 0)
                {
                    var laserleft = Instantiate(laser, new Vector3(oxpos[secondpos] - 8, l1oypos - 2 * i, 0), Quaternion.identity);
                }
                else
                {
                    Vector3 theScale = canInstance.transform.localScale;
                    theScale.y *= -1;
                    canInstance.transform.localScale = theScale;
                    var laserright = Instantiate(laser, new Vector3(oxpos[secondpos] + 8, l1oypos - 2 * i, 0), Quaternion.identity);
                }
            }
            else if (secondpick == 3)
            {
                switchCount += 1;
                choices.Remove(3);
                var switchInstance = Instantiate(gateSwitch, new Vector3(oxpos[secondpos], l1oypos - 2 * i, 0), Quaternion.identity);
            }
            else if (secondpick == 4)
            {
                supplyCount += 1;
                choices.Remove(4);
                var supplyInstance = Instantiate(supply, new Vector3(oxpos[secondpos], l1oypos - 2 * i, 0), Quaternion.identity);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        fillMap();
    }

}
