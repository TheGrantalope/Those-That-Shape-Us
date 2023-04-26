using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatformMovement : MonoBehaviour
{
    // Private Variables
    [SerializeField] private float speed;
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;
    private bool change = true;
    private bool touchPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // update horizontal smoothly between boundaries,
        // such that when it hits one boundary it changes direction

        if (transform.position.x >= rightBoundary || transform.position.x <= leftBoundary)
        {
            change = Switch(change);
        }

        if (change == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * -speed);
        }
        
    }

    bool Switch(bool change)
    {
        if (change == true)
        {
            change = false;
        }
        else
        {
            change = true;
        }

        return change;
    }

   /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchPlayer = true;
        }
    }*/
}
