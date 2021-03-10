using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Submit : MonoBehaviour
{

    public GameObject itemHolderHolder;
    int winSquare = 2;
    int loseSquare = 3;
    bool submittable;

    public TMP_Text userFeedback;

    Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        submitButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        submittable = true;

        foreach (ItemHolder holder in itemHolderHolder.GetComponentsInChildren<ItemHolder>())
        {
            if (holder.currentItem == null)
            {
                submittable = false;
            }
        }

        if (submittable)
        {
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
        
    }

    public void OnSubmit()
    { 
        if (CheckItems()) Camera.main.GetComponent<Transition>().MoveToSquare(winSquare);
        else Camera.main.GetComponent<Transition>().MoveToSquare(loseSquare);
    }

    private bool CheckItems()
    {
        ItemHolder[] itemHolders = itemHolderHolder.GetComponentsInChildren<ItemHolder>();

        int correct = 0;

        Debug.Log(itemHolders.Length);

        //Check the id's of each item andd verify if they are correct.
        for(int i = 0; i < itemHolders.Length; i++)
        {
            if (itemHolders[i].currentItem.GetComponent<GrabbableItem>().id == (i + 1).ToString()) correct++;
        }

        userFeedback.text = correct + "/5 OBJECTS CORRECT";

        if (correct < 5) return false;
        else return true;
    }
}
