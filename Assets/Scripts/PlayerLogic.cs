using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    //Custom variable for speed in the Inspector
    [SerializeField]
    private float _speed = 8f;
    //Custom variable for spawning bullet prefab
    [SerializeField]
    private GameObject _bulletPrefab;
    //True-False variable for checking if the user can fire another bullet
    private bool _bulletCanFire = true;

    //Coroutine for cooldown on pressing "Fire1"
    IEnumerator BulletReloadTimer()
    {
        yield return new WaitForSeconds(.125f);
        _bulletCanFire = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //start Player Character at center
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();

        //if button is pressed AND check if the user can fire another bullet
        if (Input.GetButtonDown("Fire1") && _bulletCanFire)
        {
            ControlBulletFire();
        }

    }

    //Method contains all logic for moving the Player Character
    void ControlMovement()
    {
        //Player Movement controll via Input Manager
        float _verticalMovement = Input.GetAxis("Vertical");
        float _horizontalMovement = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.up * _verticalMovement * _speed * Time.deltaTime);
        transform.Translate(Vector3.right * _horizontalMovement * _speed * Time.deltaTime);

        //player boundaries y axis
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.4f)
        {
            transform.position = new Vector3(transform.position.x, -4.4f, 0);
        }

        //player wrap-around x axis
        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }

    //Method contains logic for spawning and destroying bullets
    void ControlBulletFire()
    {
        Instantiate(_bulletPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
        _bulletCanFire = false;
        StartCoroutine(BulletReloadTimer());
    }

}
