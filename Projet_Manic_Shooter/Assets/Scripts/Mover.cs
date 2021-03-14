using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float f_speed=1.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.up*f_speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
