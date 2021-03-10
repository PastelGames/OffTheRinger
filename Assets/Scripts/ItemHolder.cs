using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public float size = .5f;
    
    public GrabbableItem currentItem;
    Rigidbody2D currentItemRB;

    Vector3 origScale;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If there is nothing there, keep checking for a new item.
        if (currentItem == null)
        {
            GrabItem();
        }
        else
        {
            //If there is something there, move it to the center.
            currentItem.transform.position = Vector2.MoveTowards(currentItem.transform.position, transform.position, Time.deltaTime * 5f);

            //If the item is picked back up, remove it.
            if (currentItem.held && !LeanTween.isTweening(currentItem.gameObject))
            {
                currentItemRB.bodyType = RigidbodyType2D.Dynamic;
                if (!LeanTween.isTweening(currentItem.gameObject))
                {
                    LeanTween.scale(currentItem.gameObject, origScale, 1f).setEaseInOutBack();
                }
                currentItem = null;
                currentItemRB = null;
            }
        }
    }

    void GrabItem()
    {
        Collider2D hitCollider = Physics2D.OverlapBox(gameObject.transform.position, transform.localScale / 2, 0);
        //If you manage to grab something, store it there.
        if (hitCollider != null && hitCollider.GetComponent<GrabbableItem>() && !hitCollider.GetComponent<GrabbableItem>().held && !LeanTween.isTweening(hitCollider.gameObject))
        {
            currentItem = hitCollider.GetComponent<GrabbableItem>();
            currentItemRB = currentItem.GetComponent<Rigidbody2D>();
            currentItemRB.bodyType = RigidbodyType2D.Static;
            origScale = currentItem.transform.localScale;
            if (!LeanTween.isTweening(currentItem.gameObject))
            {
                LeanTween.scale(currentItem.gameObject, currentItem.transform.localScale / 2, 1f).setEaseInOutBack();
            }
            LeanTween.rotate(currentItem.gameObject, Vector3.zero, 1f).setEaseInOutBack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
