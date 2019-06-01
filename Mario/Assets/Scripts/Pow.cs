using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pow : MonoBehaviour
{
    protected GameObject[] Enemies;
    protected int hits;
    // Start is called before the first frame update
    void Start()
    {
        hits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hits == 3)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "PlayerHead")
            Active();
    }

    void Active()
    {
        hits++;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject e in Enemies)
        {
            e.SendMessage("StunController");
        }
    }
}
