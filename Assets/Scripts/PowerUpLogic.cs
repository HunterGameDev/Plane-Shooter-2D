using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    public int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
    }

    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }
}
