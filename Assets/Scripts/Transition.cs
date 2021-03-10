using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B)) MoveRight();
    }

    public void MoveToSquare(int squareIndex)
    {
        //Check to make sure that there are no transitions currently happening.
        if (!LeanTween.isTweening(gameObject))
        {
            LeanTween.moveX(gameObject, 11 * squareIndex, 1f).setEaseOutBack();
        }
    }

    public void MoveRight()
    {
        //Check to make sure that there are no transitions currently happening.
        if (!LeanTween.isTweening(gameObject))
        {
            LeanTween.moveX(gameObject, transform.position.x + 11f, 1f).setEaseOutBack();
        }
    }

    public void MoveLeft()
    {
        //Check to make sure that there are no transitions currently happening.
        if (!LeanTween.isTweening(gameObject))
        {
            LeanTween.moveX(gameObject, transform.position.x - 11f, 1f).setEaseOutBack();
        }
    }
}
