using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLogic : MonoBehaviour
{
    private float _xPosition;
    private Vector3 _spawnPosition;
    [SerializeField]
    private float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        RandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
    }

    void RandomSpawn()
    {
        _xPosition = Random.Range(-8.5f, 8.5f);
        _spawnPosition = new Vector3(_xPosition, 6, 0);
        transform.position = _spawnPosition;
    }

    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }
}
