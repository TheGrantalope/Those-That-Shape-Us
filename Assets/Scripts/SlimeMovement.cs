using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed;
    public Transform[] patrol;
    public float waitTime;
    int currentPoint;
    bool wait_once_flag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != patrol[currentPoint].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrol[currentPoint].position, speed * Time.deltaTime);
        }
        else
        {
            if (wait_once_flag == false)
            {
                wait_once_flag = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPoint + 1 < patrol.Length)
        {
            currentPoint++;
        }
        else
        {
            currentPoint = 0;
        }
        wait_once_flag = false;
    }
}
