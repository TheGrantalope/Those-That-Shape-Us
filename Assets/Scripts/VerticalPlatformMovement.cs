using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatformMovement : MonoBehaviour
{
    // Private Variables
    
    [SerializeField] private float speed;
    [SerializeField] private float upperBoundary;
    [SerializeField] private float lowerBoundary;
    private bool change = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // update vertical smoothly between boundaries,
        // such that when it hits one boundary it changes direction

        if (transform.position.y >= upperBoundary || transform.position.y <= lowerBoundary)
        {
            change = Switch(change);
        }

        if (change == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        } else
        {
            transform.Translate(Vector3.up * Time.deltaTime * -speed);
        }

    }

    bool Switch(bool change)
    {
        if (change == true)
        {
            change = false;
        } else
        {
            change = true;
        }

        return change;
    }

}
