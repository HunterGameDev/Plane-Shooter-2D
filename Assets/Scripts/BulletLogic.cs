using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    //Bullet speed variable
    [SerializeField]
    private float _speed = 12f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moves the bullet at a rate based on seconds
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //destroys the bullet if it reaches a certain distance
        if (transform.position.y >= 7f)
        {
            Destroy(gameObject);
        }

    }
}
