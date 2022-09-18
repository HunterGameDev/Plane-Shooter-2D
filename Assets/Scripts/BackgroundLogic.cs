using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLogic : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    private SpawnManagerLogic _spawnManager;

    private bool _canSpawn = true;

    IEnumerator BackgroundSpawnTimer()
    {
        yield return new WaitForSeconds(15f);
        _canSpawn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerLogic>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();

        if (transform.position.y <= -26.9f && _canSpawn)
        {
            _spawnManager.BottomScrollReached();
            _canSpawn = false;
            StartCoroutine(BackgroundSpawnTimer());
        }
        else if (transform.position.y <= -38f)
        {
            Destroy(this.gameObject);
        }
    }

    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
