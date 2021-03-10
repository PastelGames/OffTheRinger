using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Phone : MonoBehaviour
{

    public bool moveable;
    public bool holding;
    public GameObject ringingSymbol;
    public string[] lines;
    public Subtitles subtitles;
    public AudioSource pickUp;
    public AudioSource putDown;

    int currentLineIndex = 0;
    string currentLine;
    bool subtitlesActive;

    public static float phoneSpeed = 4;
    public static float mountRadius = 2f;

    Collider2D col;
    Rigidbody2D rb;
    Animator anim;
    Vector2 origin;
    Quaternion originRotation;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
        origin = transform.position;
        originRotation = transform.rotation;
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        currentLine = lines[currentLineIndex];
    }

    // Update is called once per frames
    void Update()
    {
        //If the phone is being held.
        if (holding)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position) * phoneSpeed;
            audioSource.priority = 10;
        }
        //If the phone is close enough to the handle, return it to its original position and rotation.
        else if (Vector2.Distance((Vector2)origin, (Vector2)transform.position) < mountRadius && !audioSource.isPlaying)
        {

            rb.bodyType = RigidbodyType2D.Static;
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, origin, Time.deltaTime * 2f);
            rb.transform.rotation = Quaternion.RotateTowards(rb.transform.rotation, originRotation, 1f);
            if (!anim.GetBool("Mounted")) putDown.Play(); 
            anim.SetBool("Mounted", true);
        }
        else
        {
            audioSource.priority = 128;
        }

       //Uncomment for testing rings.
       //if (Input.GetKeyDown(KeyCode.A)) Ring();
    }

    public void Ring()
    {
        if (!anim.GetBool("Ringing"))
        {
            if (anim.GetBool("Mounted"))
            {
                ringingSymbol.SetActive(true);
                anim.SetBool("Ringing", true);
            }

            currentLineIndex = (currentLineIndex + 1) % lines.Length;
            currentLine = lines[currentLineIndex];
        }
    } 

    private void OnMouseDown()
    {
        //If the phone is moving, you can pick it up.
        if (moveable)
        {
            if (anim.GetBool("Mounted"))
            {
                subtitles.text = currentLine;
                subtitles.PopUp();
                if (subtitlesActive)
                {
                    StopCoroutine(WaitToRemove());
                }
                StartCoroutine(WaitToRemove());
                audioSource.Play();
                pickUp.Play();
            }

            holding = true;
            anim.SetBool("Mounted", false);
            anim.SetBool("Ringing", false);
            ringingSymbol.SetActive(false);
        }
    }

    private IEnumerator WaitToRemove()
    {
        subtitlesActive = true;
        yield return new WaitForSeconds(8f);
        subtitles.GoAway();
        audioSource.Stop();
        subtitlesActive = false;
    }

    private void OnMouseUp()
    {
        holding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Phone" || collision.gameObject.tag == "Cord")
        {
            Physics2D.IgnoreCollision(collision.collider, col);
        }
    }
}
