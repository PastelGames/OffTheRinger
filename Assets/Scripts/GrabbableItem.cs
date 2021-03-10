using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableItem : MonoBehaviour
{
    public bool held;
    public string id;

    public static float speed = 3f;


    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the item is being held, move towards the object.
        if (held) rb.velocity = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position) * speed;
    }

    private void OnMouseDown()
    {
        held = true;
    }

    private void OnMouseUp()
    {
        held = false;
    }
}
