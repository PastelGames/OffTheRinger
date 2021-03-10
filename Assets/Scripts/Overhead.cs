using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overhead : MonoBehaviour
{

    public Phone[] phones;
    public float gap = 10;
    bool gameStarted;
    public float gapDecrement = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            StartCoroutine(PhoneGame());
        }
        gameStarted = true;
    }
 
    private IEnumerator PhoneGame()
    {
        yield return new WaitForSeconds(gap);
        phones[Random.Range(0, phones.Length)].Ring();
        gap -= gapDecrement;
        StartCoroutine(PhoneGame());
    }

    //Restart the scene again.
    public void Restart()
    {
        LeanTween.move(Camera.main.gameObject, new Vector2(11 * -1, 0), 1f).setEaseOutBack().setOnComplete(ReloadScene);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
