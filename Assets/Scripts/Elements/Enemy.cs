using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    private Player _player;
    private Animator _enemyAnimator;
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioSource _explosionSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player NULL");
        }
        _enemyAnimator = GetComponent<Animator>();
        if(_enemyAnimator == null)
        {
            Debug.LogError("Animator NULL");
        }
        _explosionSource = GetComponent<AudioSource>();
        if (_explosionSource == null)
        {
            Debug.LogError("Audio Source NULL");
        }
        else
        {
            _explosionSource.clip = _explosionClip;
        }
    }
    void Update()
    {
        EnemyMovement();
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            Enemy e = gameObject.GetComponent<Enemy>();
            Vector3 laserPos = new Vector3(e.transform.position.x, 0, e.transform.position.z);
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, laserPos, Quaternion.identity);
            LaserBehavior[] lasers = enemyLaser.GetComponentsInChildren<LaserBehavior>();
            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].enemyShooter();
            }
 
        }

       
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
           if(_player != null)
            {
                _player.AddScore(10);
            }
            _enemyAnimator.SetTrigger("EnemyDeath");
            _speed = 0;
            _explosionSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 3.0f);
            

        }
        else if(other.tag == "Player")
        {    
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
               
            }
            _enemyAnimator.SetTrigger("EnemyDeath");
            _speed = 0;
            _explosionSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 3.0f);
        }
    }
}
