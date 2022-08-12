using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    //Custom variable for speed in the Inspector
    [SerializeField]
    private float _speed = 4.5f;
    //Custom variable for spawning bullet prefab
    [SerializeField]
    private GameObject _bulletPrefab;
    //True-False variable for checking if the user can fire another bullet
    private bool _bulletCanFire = true;

    IEnumerator BulletReloadTimer()
    {
        yield return new WaitForSeconds(.5f);
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

        //if button is pressed and released AND check if the user can fire another bullet
        //spawn bullet prefab with Player position and default quaternian rotation
        if (Input.GetButtonDown("Fire1") && _bulletCanFire)
        {
            Instantiate(_bulletPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
            _bulletCanFire = false;
            StartCoroutine(BulletReloadTimer());
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
}
