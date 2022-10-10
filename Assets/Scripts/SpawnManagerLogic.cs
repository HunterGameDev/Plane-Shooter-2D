using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject _basicEnemyType;
    [SerializeField]
    private GameObject _basicMineType;
    [SerializeField]
    private GameObject _containerTypeEnemy;
    [SerializeField]
    private GameObject _containerTypeMine;
    [SerializeField]
    private GameObject _firstBackground;
    [SerializeField]
    private GameObject _secondBackground;
    [SerializeField]
    private GameObject _cloudLayer;

    private bool _stopSpawning = false;
    private bool _spawnNewBackground = false;
    private bool _spawnNewClouds = false;

    IEnumerator BasicEnemySpawnTimer()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.3f, 8.3f), 6, 0);
            GameObject newEnemy = Instantiate(_basicEnemyType, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _containerTypeEnemy.transform;
            yield return new WaitForSeconds(Random.Range(2f, 4f));
        }
    }

    IEnumerator BasicMineSpawnTimer()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 6, 0);
            GameObject newMine = Instantiate(_basicMineType, posToSpawn, Quaternion.identity);
            newMine.transform.parent = _containerTypeMine.transform;
            yield return new WaitForSeconds(Random.Range(5f, 8f));
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        Instantiate(_firstBackground, transform.position + new Vector3(0, 24.7f, 0), Quaternion.identity);

        Instantiate(_cloudLayer, transform.position + new Vector3(16f, 33f, 0), Quaternion.identity);

        StartCoroutine(BasicEnemySpawnTimer());

        StartCoroutine(BasicMineSpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBackground();
        SpawnClouds();
    }

    private void SpawnBackground()
    {
        if (_spawnNewBackground == true)
        {
            Instantiate(_secondBackground, transform.position + new Vector3(0, 37.85f, 0), Quaternion.identity);
            _spawnNewBackground = false;
        }
    }

    private void SpawnClouds()
    {
        if (_spawnNewClouds == true)
        {
            Instantiate(_cloudLayer, transform.position + new Vector3(20f, 47f, 0), Quaternion.identity);
            _spawnNewClouds = false;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void BottomScrollReached()
    {
        _spawnNewBackground = true;
    }

    public void CloudScrollReached()
    {
        _spawnNewClouds = true;
    }
}
