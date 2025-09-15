using UnityEngine;

public class Banana : MonoBehaviour
{
    public bool isTouched = false;
    float timer = 0.0f;
    public float waitTime = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched)
        {
            GetComponent<AudioSource>().Play();
            isTouched = false; 
            timer += Time.deltaTime;
            if (timer >= waitTime) 
            {
                timer = 0.0f;
                isTouched = false;
            }
        }           
    }
}
