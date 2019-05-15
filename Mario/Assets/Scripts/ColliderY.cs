using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderY : MonoBehaviour
{
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        /*Sometimes when te fireball go from one side of the screen to the other,
         * the collider position can be different of the sprite position (FIXED)
         */
        if(trans.position.x != transform.parent.position.x ||
           trans.position.y != transform.parent.position.y)
            trans.position = new Vector2(
                transform.parent.position.x,
                    transform.parent.position.y);
    }

    void OnCollisionEnter2D()
    {
        transform.parent.SendMessage("CollideY");
    }
}
