using UnityEngine;

public class Flower : MonoBehaviour
{
    public bool didDing = false;
    public Canvas CanCanvas;

    float timer = 0.0f;
    public float waitTime = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CanCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (didDing)
        {
            CanCanvas.enabled = true;
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                CanCanvas.enabled = false;
                timer = 0.0f;
                didDing = false;
            }
        }
    }
}
