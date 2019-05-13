using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderY : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D()
    {
        print("Y");
        transform.parent.SendMessage("CollideY");
    }
}
