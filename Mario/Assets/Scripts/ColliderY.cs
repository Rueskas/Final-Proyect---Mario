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
        if (trans.position.x != transform.parent.position.x ||
                trans.position.y != transform.parent.position.y)
            trans.position = new Vector2(0, 0);
    }

    void OnCollisionEnter2D()
    {
        transform.parent.SendMessage("CollideY");
    }
}
