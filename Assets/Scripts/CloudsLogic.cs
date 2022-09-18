using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsLogic : MonoBehaviour
{
    [SerializeField]
    private float _speedDown = 1f;
    [SerializeField]
    private float _speedLeft = 0.25f;
    private SpawnManagerLogic _spawnManager;

    private bool _canSpawn = true;

    IEnumerator CloudSpawnTimer()
    {
        yield return new WaitForSeconds(20f);
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

        if (transform.position.y <= -38.68f && _canSpawn)
        {
            _spawnManager.CloudScrollReached();
            _canSpawn = false;
            StartCoroutine(CloudSpawnTimer());
        }
        else if (transform.position.y <= -48f)
        {
            Destroy(this.gameObject);
        }
    }

    void ControlMovement()
    {
        transform.Translate(Vector3.down * _speedDown * Time.deltaTime);
        transform.Translate(Vector3.left * _speedLeft * Time.deltaTime);
    }

}
