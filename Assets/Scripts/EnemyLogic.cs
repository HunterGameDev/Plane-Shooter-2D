using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private float _xPosition;
    private Vector3 _spawnPosition;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _enemyBullets;
    private bool _canFire = false;

    // Start is called before the first frame update
    void Start()
    {
        //random spawn point
        RandomSpawn();
        StartCoroutine(BulletCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
        SpawnBullets();
    }

    void RandomSpawn()
    {
        _xPosition = Random.Range(-8.9f, 8.9f);
        _spawnPosition = new Vector3(_xPosition, 6, 0);
        transform.position = _spawnPosition;
    }

    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            RandomSpawn();
        }
    }

    void SpawnBullets()
    {
        if (_canFire == true)
        {
            Instantiate(_enemyBullets, transform.position, Quaternion.identity);
            _canFire = false;
            StartCoroutine(BulletCooldown());
        }
    }

    IEnumerator BulletCooldown()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 4f));
        _canFire = true;
    }
}
