using UnityEngine;

public class People : MonoBehaviour
{
    public bool isPlayed = false;
    float timer = 0.0f;
    public float waitTime = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayed)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                timer = 0.0f;
                isPlayed = false;
            }
        }
    }
}
