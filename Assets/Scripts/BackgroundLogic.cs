using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLogic : MonoBehaviour
{
    //Custom variable for background movement speed
    [SerializeField]
    private float _speed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        //transform new Vector3 for start location
        transform.position = new Vector3(0, 24.7f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //Start method
        ControlMovement();

    }

    //Method containing logic for moving background by the speed variable and time.deltatime
    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }








}
