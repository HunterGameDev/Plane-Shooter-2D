using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLogic : MonoBehaviour
{
    //Custom field to indicate which game object gets spawned
    [SerializeField]
    private GameObject _basicEnemyType;
    [SerializeField]
    private GameObject _basicMineType;
    [SerializeField]
    private GameObject _containerTypeEnemy;
    [SerializeField]
    private GameObject _containerTypeMine;

    private bool _stopSpawning = false;

    //Coroutine for delaying the spawn of enemies
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

    //Corroutine for delaying the spawn of mines
    IEnumerator BasicMineSpawnTimer()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 6, 0);
            GameObject newMine = Instantiate(_basicMineType, posToSpawn, Quaternion.identity);
            newMine.transform.parent = _containerTypeMine.transform;
            yield return new WaitForSeconds(Random.Range(5f, 12f));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Method that contains logic to spawn new enemies on a timer
        StartCoroutine(BasicEnemySpawnTimer());
        //Method that contains logic to spawn new mines on a timer
        StartCoroutine(BasicMineSpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
