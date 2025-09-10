using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 0.5f;

    public float minSpeed = 0.5f;
    public float maxSpeed = 5.0f;

    public float acceleration = 0.1f;
    private GameObject enemyObj;

    string playerName = "Mario";

    List<int> scores;

    double playerHealth;

    bool gameStart = false;

    bool goLeft = true;
    bool goRight = true;

    public AudioSource myCDPlayer;

    public AudioClip dingCD;

    char upKey = 'W';

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // speed = minSpeed;
        myCDPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.Space))
        {
            gameStart = true;
        }

        if (gameStart)
        {
            speed += acceleration * Time.deltaTime;
            Debug.Log(speed);
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            if (mousePos.x > (currentPos.x + 3.0f) && goRight)
            {
                currentPos.x += speed * Time.deltaTime;
            }
            if (mousePos.x < (currentPos.x - 3.0f) && goLeft)
            {
                currentPos.x -= speed * Time.deltaTime;
            }
            if (transform.position != currentPos)
            {
                if (speed < maxSpeed)
                {
                    speed += acceleration;
                }
            }
            else
            {
                speed = minSpeed;
            }
            transform.position = currentPos;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            goLeft = false;
            Debug.Log("Left Collision Detected");
        }

        if (collision.CompareTag("Right"))
        {
            goRight = false;
            Debug.Log("Right Collision Detected");
        }

        if (collision.CompareTag("Flower"))
        {
            if (collision.gameObject.GetComponent<Flower>().didDing == false)
                myCDPlayer.PlayOneShot(dingCD, 1.0f);
            collision.gameObject.GetComponent<Flower>().didDing = true;
            collision.gameObject.GetComponent<Animator>().SetBool("Bloom", true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            goLeft = true;
            Debug.Log("Left Collision Exited");
        }
        if (collision.CompareTag("Right"))
        {
            goRight = true;
            Debug.Log("Right Collision Exited");
        }
    }
}

