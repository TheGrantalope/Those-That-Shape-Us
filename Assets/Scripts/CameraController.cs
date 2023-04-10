using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Private Variables
    private Vector3 offset;

    // Public Variables
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(-1, -player.transform.position.y, -10);
        transform.position = player.transform.position + offset;
    }

}
