using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pow : MonoBehaviour
{
    protected Enemy[] Enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "PlayerHead")
            Active();
    }

    void Active()
    {
        Enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy e in Enemies)
        {
            e.StunController();
        }
    }
}
