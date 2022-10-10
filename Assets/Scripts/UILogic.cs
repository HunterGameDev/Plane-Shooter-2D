using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _finalScoreText;
    private int _numberScore = 0;
    private string _currentScore;

    [SerializeField]
    private GameObject _gameOverScreen;

    [SerializeField]
    private TMP_Text _ammoText;
    private string _currentAmmo;

    private int _lives;
    private int _ammo;
    private bool _hasSpeedBoostItem = false;

    [SerializeField]
    private Sprite _healthZero;
    [SerializeField]
    private Sprite _healthOne;
    [SerializeField]
    private Sprite _healthTwo;
    [SerializeField]
    private Sprite _healthThree;
    [SerializeField]
    private Sprite _healthFour;
    [SerializeField]
    private Sprite _healthFive;
    [SerializeField]
    private Sprite _healthSix;

    [SerializeField]
    private Sprite _tripleInactive;
    [SerializeField]
    private Sprite _tripleActive;

    [SerializeField]
    private Sprite _boostInactive;
    [SerializeField]
    private Sprite _boostActive;

    private PlayerLogic _player;
    private SpriteRenderer _healthSpriteRenderer;
    private SpriteRenderer _tripleSpriteRenderer;
    private SpriteRenderer _boostSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverScreen.SetActive(false);

        _healthSpriteRenderer = GameObject.Find("Health 0").GetComponent<SpriteRenderer>();
        if (_healthSpriteRenderer == null)
        {
            Debug.LogError("The Health Sprite Renderer is NULL.");
        }
        _tripleSpriteRenderer = GameObject.Find("Triple Shot Inactive").GetComponent<SpriteRenderer>();
        if (_tripleSpriteRenderer == null)
        {
            Debug.LogError("The Triple Shot Inactive Sprite Renderer is NULL.");
        }
        _boostSpriteRenderer = GameObject.Find("Boost Inactive").GetComponent<SpriteRenderer>();
        if (_boostSpriteRenderer == null)
        {
            Debug.LogError("The Boost Inactive Sprite Renderer is NULL.");
        }
        _player = GameObject.Find("Player").GetComponent<PlayerLogic>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _currentScore = _numberScore.ToString();
        _scoreText.text = _currentScore;
        _finalScoreText.text = _currentScore;

        _currentAmmo = _ammo.ToString();
        _ammoText.text = "x" + _currentAmmo;

        _lives = _player._lives;
        _ammo = _player._ammo;
        _hasSpeedBoostItem = _player._hasSpeedBoostItem;

        ChangeHealthSprite();
        ChangeTripleSprite();
        ChangeBoostSprite();
    }

    public void IncreaseScore()
    {
        _numberScore++;
    }

    private void ChangeHealthSprite()
    {
        if (_lives <= 0)
        {
            _healthSpriteRenderer.sprite = _healthZero;
            _gameOverScreen.SetActive(true);
        }
        else if (_lives == 1)
        {
            _healthSpriteRenderer.sprite = _healthOne;
        }
        else if (_lives == 2)
        {
            _healthSpriteRenderer.sprite = _healthTwo;
        }
        else if (_lives == 3)
        {
            _healthSpriteRenderer.sprite = _healthThree;
        }
        else if (_lives == 4)
        {
            _healthSpriteRenderer.sprite = _healthFour;
        }
        else if (_lives == 5)
        {
            _healthSpriteRenderer.sprite = _healthFive;
        }
        else if (_lives == 6)
        {
            _healthSpriteRenderer.sprite = _healthSix;
        }
    }

    private void ChangeTripleSprite()
    {
        if (_ammo <= 0)
        {
            _tripleSpriteRenderer.sprite = _tripleInactive;
        }
        else
        {
            _tripleSpriteRenderer.sprite = _tripleActive;
        }
    }

    private void ChangeBoostSprite()
    {
        if (_hasSpeedBoostItem == false)
        {
            _boostSpriteRenderer.sprite = _boostInactive;
        }
        else
        {
            _boostSpriteRenderer.sprite = _boostActive;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
