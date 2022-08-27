using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsLogic : MonoBehaviour
{
    //Custom variable for down speed
    [SerializeField]
    private float _speedDown = 1f;
    //Custom variable for left speed
    [SerializeField]
    private float _speedLeft = 0.25f;
    
    // Start is called before the first frame update
    void Start()
    {
        //starting location
        transform.position = new Vector3(16, 33, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //call the method
        ControlMovement();
    }

    //Method containing Logic for movement at relative speed and time.deltatime
    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speedDown * Time.deltaTime);
        transform.Translate(Vector3.left * _speedLeft * Time.deltaTime);
    }

}
