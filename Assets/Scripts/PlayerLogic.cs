using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private GameObject _triplePrefab;
    [SerializeField]
    private GameObject _explosionObject;
    private bool _bulletCanFire = true;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _ammo = 0;

    [SerializeField]
    private Sprite _armorThree;
    [SerializeField]
    private Sprite _armorFour;
    [SerializeField]
    private Sprite _armorFive;

    [SerializeField]
    private GameObject _speedVFX;
    private bool _hasSpeedBoostItem = false;
    private GameObject _childVFX;
    [SerializeField]
    private float _speedMultiplier = 2f;
    private float fixedDeltaTime;
    private void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    
    private SpawnManagerLogic _spawnManager;
    private SpriteRenderer _spriteRenderer;

    private PowerUpLogic _powerupScript;
    private int _newID;

    IEnumerator BulletReloadTimer()
    {
        yield return new WaitForSeconds(.1f);
        _bulletCanFire = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerLogic>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
        ChangeArmorSprite();

        if (Input.GetButtonDown("Fire1") && _bulletCanFire)
        {
            ControlBulletFire();
        }
        if (Input.GetButtonDown("Fire2") && _bulletCanFire && _isTripleShotActive)
        {
            ControlTripleFire();
        }
        else if (Input.GetButtonDown("Fire2") && _hasSpeedBoostItem)
        {
            ControlSpeedBoost();
        }

    }

    void ControlMovement()
    {
        float _verticalMovement = Input.GetAxis("Vertical");
        float _horizontalMovement = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.up * _verticalMovement * _speed * Time.deltaTime);
        transform.Translate(Vector3.right * _horizontalMovement * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.9f)
        {
            transform.position = new Vector3(transform.position.x, -4.9f, 0);
        }

        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }

    void ControlBulletFire()
    {
        Instantiate(_bulletPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
        _bulletCanFire = false;
        StartCoroutine(BulletReloadTimer());
    }

    void ControlTripleFire()
    {
        if (_ammo > 0)
        {
            Instantiate(_triplePrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            _ammo--;
            _bulletCanFire = false;
            StartCoroutine(BulletReloadTimer());
        }
    }

    void ControlSpeedBoost()
    {
        GameObject newSpeedVFX = Instantiate(_speedVFX, this.gameObject.transform.position, Quaternion.identity);
        newSpeedVFX.transform.parent = this.gameObject.transform;
        Time.timeScale = _speedMultiplier;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        _hasSpeedBoostItem = false;
        StartCoroutine(SpeedBoostCooldown());
    }

    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(10f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = this.fixedDeltaTime;
        _childVFX = GameObject.Find("SpeedBoostVFX(Clone)");
        Destroy(_childVFX);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Enemy")
        {
            Instantiate(_explosionObject, other.transform.position, Quaternion.identity);
            Damage();
            Destroy(other.gameObject);
        }

      else if (other.tag == "PowerUp")
        {
            _powerupScript = other.GetComponent<PowerUpLogic>();
            _newID = _powerupScript.powerupID;
            
            switch(_newID)
            {
                case 0:
                    GiveTripleAmmo();
                    break;
                case 1:
                    GiveSpeedBoostItem();
                    break;
                case 2:
                    ManageLifeArmor();
                    break;
                default:
                    Debug.Log("PowerUpID does not match predetermined values.");
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionObject, transform.position + new Vector3(0, 0f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void GiveTripleAmmo()
    {
        _ammo = 5;
        _hasSpeedBoostItem = false;

        if (_ammo < 1)
        {
            _isTripleShotActive = false;
        }
        else if (_ammo > 5)
        {
            _ammo = 5;
        }
        else if (_ammo > 0)
        {
            _isTripleShotActive = true;
        }
    }

    void GiveSpeedBoostItem()
    {
        _hasSpeedBoostItem = true;
        _ammo = 0;
        _isTripleShotActive = false;
    }

    void ManageLifeArmor()
    {
        _lives++;
        
        if (_lives > 5)
        {
            _lives = 5;
        }
    }

    void ChangeArmorSprite()
    {
        if (_lives <= 3)
        {
            _spriteRenderer.sprite = _armorThree;
        }
        else if (_lives == 4)
        {
            _spriteRenderer.sprite = _armorFour;
        }
        else if (_lives == 5)
        {
            _spriteRenderer.sprite = _armorFive;
        }
    }
}
