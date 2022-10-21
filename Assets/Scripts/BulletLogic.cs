using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField]
    private float _speed = 12f;
    [SerializeField]
    private GameObject _tripleShotPower;
    [SerializeField]
    private GameObject _speedBoostPower;
    [SerializeField]
    private GameObject _armorUpPower;
    [SerializeField]
    private GameObject _explosionObject;
    private AudioSource _explosionAudioSource;

    private int _percentChance = 0;
    [SerializeField]
    private int _expectedValue = 60;

    [SerializeField]
    private int _maxPowerUps;

    private int _randomPower;

    private UILogic _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("UI Manager").GetComponent<UILogic>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        _explosionAudioSource = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        if (_explosionAudioSource == null)
        {
            Debug.LogError("The Explosion Audio is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.name == "Bullet(Clone)")
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y >= 4.8f)
            {
                Destroy(gameObject);
            }
        }
        else if (this.gameObject.transform.name == "TripleShotEmpty(Clone)")
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y >= 4.8f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (other.transform.name == "EnemyBullets(Clone)")
            {
                return;
            }
            else
            {
                _uiManager.IncreaseScore();
                Instantiate(_explosionObject, other.transform.position, Quaternion.identity);
                GetNewPercentChance();
                _explosionAudioSource.Play();
                if (_percentChance >= _expectedValue)
                {
                    GetNewRandomPower();
                    switch (_randomPower)
                    {
                        case 0:
                            Instantiate(_tripleShotPower, other.transform.position, Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(_speedBoostPower, other.transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(_armorUpPower, other.transform.position, Quaternion.identity);
                            break;
                        default:
                            Debug.Log("_randomPower does not match numerical value.");
                            break;
                    }
                }
                Destroy(other.gameObject);
                if (this.gameObject.transform.name == "Bullet(Clone)")
                {
                    Destroy(this.gameObject);
                }
                else if (this.gameObject.transform.name == "TripleShotEmpty(Clone)")
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void GetNewPercentChance()
    {
        _percentChance = Random.Range(1, 100);
    }

    void GetNewRandomPower()
    {
        _randomPower = Random.Range(0, (_maxPowerUps--));
    }
}
