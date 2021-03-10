using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Subtitles : MonoBehaviour
{
    public Transform onScreenDest;
    public Transform offScreenDest;

    public string text;
    TMP_Text tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = text;
    }

    public void PopUp()
    {
        LeanTween.value(0f, 1f, 2f).setOnUpdate((float val) =>
        {
            gameObject.GetComponent<TMP_Text>().alpha = val;
        });
        gameObject.transform.position = offScreenDest.transform.position;
        LeanTween.move(gameObject, onScreenDest.position, 1f).setEaseOutBack();
    }

    public void GoAway()
    {
        if (!LeanTween.isTweening(gameObject)) LeanTween.move(gameObject, offScreenDest.position, 1f).setEaseInBack();
    }
}
