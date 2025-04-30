using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public float health= 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float value)
    {
        health += value;
        if (health <=0)
        {
            Destroy(gameObject);
        }
    }
}
