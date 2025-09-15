using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 0f;

    public float minSpeed = 0.5f;
    public float maxSpeed = 3.0f;

    public float acceleration = 0.05f;

    List<int> scores;

    double playerHealth;

    bool gameStart = false;

    bool goLeft = true;
    bool goRight = true;

    bool footstepSoundPlaying = false;

    public AudioSource myCDPlayer;

    public AudioClip dingCD;

    char upKey = 'W';
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = minSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject spr = transform.GetChild(0).gameObject;
        Vector3 currentPos = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Space))
        {
            gameStart = true;
        }

        if (gameStart)
        {
            speed += acceleration * Time.deltaTime;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            if (mousePos.x > (currentPos.x + 3.0f) && goRight)
            {
                currentPos.x += speed * Time.deltaTime;
                spr.GetComponent<Animator>().SetBool("IsWalking", true);
                spr.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (mousePos.x < (currentPos.x - 3.0f) && goLeft)
            {
                currentPos.x -= speed * Time.deltaTime;
                spr.GetComponent<Animator>().SetBool("IsWalking", true);
                spr.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (transform.position != currentPos)
            {
                if (!footstepSoundPlaying)
                {
                    spr.GetComponent<AudioSource>().Play();
                    footstepSoundPlaying = true;
                }
                if (speed < maxSpeed)
                {
                    speed += acceleration;
                }
            }
            else
            {
                speed = minSpeed;
                spr.GetComponent<Animator>().SetBool("IsWalking", false);
                spr.GetComponent<AudioSource>().Stop();
                footstepSoundPlaying = false;
            }
            transform.position = currentPos;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            goLeft = false;
        }

        if (collision.CompareTag("Right"))
        {
            goRight = false;
        }

        if (collision.CompareTag("Flower"))
        {
            if (collision.gameObject.GetComponent<Flower>().didDing == false)
                collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Flower>().didDing = true;
        }
        if (collision.CompareTag("people"))
        {
            if (collision.gameObject.GetComponent<People>() != null)
            {
                if (collision.gameObject.GetComponent<People>().isPlayed == false)
                {
                    collision.gameObject.GetComponent<AudioSource>().Play();
                    collision.gameObject.GetComponent<People>().isPlayed = true;
                }
            }
            else if (collision.gameObject.GetComponent<People_Walk>() != null)
            {
                if (collision.gameObject.GetComponent<People_Walk>().isPlayed == false)
                {
                    collision.gameObject.GetComponent<AudioSource>().Play();
                    collision.gameObject.GetComponent<People_Walk>().isPlayed = true;
                }
            }
        }
        if (collision.CompareTag("Banana") && collision.gameObject.GetComponent<Banana>().isTouched == false)
        {
            collision.gameObject.GetComponent<Banana>().isTouched = true;
            collision.gameObject.GetComponent<AudioSource>().Play();
            speed = 2.0f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            goLeft = true;
        }
        if (collision.CompareTag("Right"))
        {
            goRight = true;
        }
    }
}

