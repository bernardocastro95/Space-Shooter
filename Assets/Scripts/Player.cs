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
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Tracker();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shooter();
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
        
    }
    public void Damage()
    {
        _lives--;

        if(_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
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
        StartCoroutine(ShieldPowerupCoroutine());
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
    IEnumerator ShieldPowerupCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldEnabled = false;
    }
}
