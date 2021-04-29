using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _multiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotEnabled = false;
    private bool _isSpeedBoostEnabled = false;
    private bool _isShieldEnabled = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _damageLeft, _damageRight;
    [SerializeField]
    private AudioSource _laserSource;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private GameManager _manager;
    public bool isPlayerOne = false, isPlayerTwo = false;

    // Start is called before the first frame update
    void Start()
    {

       
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _laserSource = GetComponent<AudioSource>();
        

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager NULL");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager == null)
        {
            Debug.LogError("UI Manager NULL");
        }
        if (_laserSource == null)
        {
            Debug.LogError("Audio Source NULL");
        }
        else
        {
            _laserSource.clip = _laserClip;
        }
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_manager == null)
        {
            Debug.LogError("Game Manager NULL");
        }

        if (_manager._isCoop == false)
        {
            transform.position = new Vector3(0, 0, 0);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerOne == true)
        {
            Tracker();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                Shooter();
            }
        }
        else if(isPlayerTwo == true)
        {
            TrackerTwo();
            ShooterTwo();
            
        }
        

       
    }

    void Tracker()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
      
        transform.Translate(direction * ( _speed * _multiplier) * Time.deltaTime);

        if (transform.position.y >= 0 || transform.position.y <= 3.8f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        }

        if (transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }
    void TrackerTwo()
    {

        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * (_speed * _multiplier) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad2)){
            transform.Translate(Vector3.down * (_speed * _multiplier) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * (_speed * _multiplier) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * (_speed * _multiplier) * Time.deltaTime);
        }

        if (transform.position.y >= 0 || transform.position.y <= 3.8f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        }

        if (transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }
    void Shooter()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotEnabled == true)
        {
            
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
        _laserSource.Play();
        
    }
    void ShooterTwo()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotEnabled == true)
            {

                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
            }
            _laserSource.Play();
        }
       

    }
    public void Damage()
    {

        if (_isShieldEnabled == true)
        {
            _isShieldEnabled = false;
            _shield.gameObject.SetActive(false);
            return;
        }
        else
        {
            _lives--;
            _uiManager.CurrentLive(_lives);

            if(_lives == 2)
            {
                _damageRight.gameObject.SetActive(true);
            }
            else if(_lives == 1)
            {
                _damageLeft.gameObject.SetActive(true);
            }

            else if (_lives < 1)
            {
                _spawnManager.onPlayerDeath();
                Destroy(this.gameObject);
            }
        
           
        }
        
    }
    public void DamageTwo()
    {

        if (_isShieldEnabled == true)
        {
            _isShieldEnabled = false;
            _shield.gameObject.SetActive(false);
            return;
        }
        else
        {
            _lives--;
            _uiManager.CurrentLive(_lives);

            if (_lives == 2)
            {
                _damageRight.gameObject.SetActive(true);
            }
            else if (_lives == 1)
            {
                _damageLeft.gameObject.SetActive(true);
            }

            else if (_lives < 1)
            {
                _spawnManager.onPlayerDeath();
                Destroy(this.gameObject);
            }


        }

    }
    public void TripleShotActive()
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerupRoutine());
       
    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostEnabled = true;
        _speed *= _multiplier;
        StartCoroutine(SppedPowerupRoutine());
    }
    public void ShieldEnabled()
    {
        _isShieldEnabled = true;
        _shield.gameObject.SetActive(true);
    }
    IEnumerator TripleShotPowerupRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotEnabled = false;
    }
    IEnumerator SppedPowerupRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _multiplier;
        _isSpeedBoostEnabled = false;
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
