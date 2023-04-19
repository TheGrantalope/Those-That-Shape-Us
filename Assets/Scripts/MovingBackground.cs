using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField]
    private Transform centerBackground;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
    if (transform.position.y >= centerBackground.position.y + 11.2f)
        centerBackground.position = new Vector2(centerBackground.position.x + 11.2f, transform.position.y);
    
    else if (transform.position.y <= centerBackground.position.y - 11.2f)
        centerBackground.position = new Vector2(centerBackground.position.x - 11.2f, transform.position.y);
     }
}
