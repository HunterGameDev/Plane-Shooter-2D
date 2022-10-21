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
    private bool _noBoostActive = true;
    private bool _isTripleShotActive = false;
    [SerializeField]
    public int _lives = 3;
    [SerializeField]
    public int _ammo = 0;

    private CapsuleCollider2D _playerCollider;
    private bool _isInvincible = false;

    [SerializeField]
    private Sprite _armorThree;
    [SerializeField]
    private Sprite _armorFour;
    [SerializeField]
    private Sprite _armorFive;
    [SerializeField]
    private Sprite _armorSix;

    [SerializeField]
    private GameObject _speedVFX;
    public bool _hasSpeedBoostItem = false;
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

    [SerializeField]
    private AudioClip _bulletAudio;
    private AudioSource _playerAudioSource;
    private AudioSource _explosionAudioSource;
    private AudioSource _powerUpAudioSource;
    private AudioSource _tripleAudioSource;
    private AudioSource _boostAudioSource;

    IEnumerator BulletReloadTimer()
    {
        yield return new WaitForSeconds(.15f);
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
        if (_spriteRenderer == null)
        {
            Debug.LogError("The Player Sprite Renderer is NULL.");
        }

        _playerCollider = this.gameObject.GetComponent<CapsuleCollider2D>();
        if (_playerCollider == null)
        {
            Debug.LogError("The Player 2D Collider is NULL.");
        }

        _playerAudioSource = this.gameObject.GetComponent<AudioSource>();
        if (_playerAudioSource == null)
        {
            Debug.LogError("The Player Audio Source is NULL.");
        }
        else
        {
            _playerAudioSource.clip = _bulletAudio;
        }
        _explosionAudioSource = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        if (_explosionAudioSource == null)
        {
            Debug.LogError("The Explosion Audio is NULL.");
        }
        _powerUpAudioSource = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
        if (_powerUpAudioSource == null)
        {
            Debug.LogError("The PowerUp Audio is NULL.");
        }
        _tripleAudioSource = GameObject.Find("TripleSound").GetComponent<AudioSource>();
        if (_tripleAudioSource == null)
        {
            Debug.LogError("The TripleShot Audio is NULL.");
        }
        _boostAudioSource = GameObject.Find("BoostSound").GetComponent<AudioSource>();
        if (_boostAudioSource == null)
        {
            Debug.LogError("The SpeedBoost Audio is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovement();
        ChangeArmorSprite();

        if (Input.GetButton("Fire1") && _bulletCanFire && _noBoostActive)
        {
            ControlBulletFire();
        }
        if (Input.GetButtonDown("Fire2") && _isTripleShotActive && _noBoostActive)
        {
            ControlTripleFire();
        }
        else if (Input.GetButtonDown("Jump") && _hasSpeedBoostItem && _noBoostActive)
        {
            ControlSpeedBoost();
        }
        if (_isInvincible == false)
        {
            _spriteRenderer.enabled = true;
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
        _playerAudioSource.Play();
        StartCoroutine(BulletReloadTimer());
    }

    void ControlTripleFire()
    {
        if (_ammo > 0)
        {
            Instantiate(_triplePrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            _tripleAudioSource.Play();
            _ammo--;
        }
    }

    void ControlSpeedBoost()
    {
        _noBoostActive = false;
        GameObject newSpeedVFX = Instantiate(_speedVFX, this.gameObject.transform.position, Quaternion.identity);
        newSpeedVFX.transform.parent = this.gameObject.transform;
        _playerCollider.enabled = false;
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
        _noBoostActive = true;
        _playerCollider.enabled = true;
        _childVFX = GameObject.Find("SpeedBoostVFX(Clone)");
        Destroy(_childVFX);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Enemy")
        {
            Instantiate(_explosionObject, other.transform.position, Quaternion.identity);
            _explosionAudioSource.Play();
            Damage();
            Destroy(other.gameObject);
        }

      else if (other.tag == "PowerUp")
        {
            _powerupScript = other.GetComponent<PowerUpLogic>();
            _newID = _powerupScript.powerupID;
            _powerUpAudioSource.Play();
            
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
        InvincibleEffects();

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionObject, transform.position + new Vector3(0, 0f, 0), Quaternion.identity);
            _explosionAudioSource.Play();
            Destroy(this.gameObject);
        }
    }

    void GiveTripleAmmo()
    {
        _ammo = 5;

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
    }

    void ManageLifeArmor()
    {
        _lives++;
        
        if (_lives > 6)
        {
            _lives = 6;
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
        else if (_lives == 6)
        {
            _spriteRenderer.sprite = _armorSix;
        }
    }

    void InvincibleEffects()
    {
        _playerCollider.enabled = false;
        _isInvincible = true;
        StartCoroutine(InvincibleCooldown());

        if (_isInvincible == true)
        {
            StartCoroutine(InvincibleFlicker());
        }
    }

    IEnumerator InvincibleCooldown()
    {
        yield return new WaitForSeconds(2f);
        _playerCollider.enabled = true;
        _isInvincible = false;
        _spriteRenderer.enabled = true;
    }

    IEnumerator InvincibleFlicker()
    {
        while (_isInvincible == true)
        {
            _spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.25f);
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
