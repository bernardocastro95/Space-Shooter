using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _enemyAnimator;

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
    }
    void Update()
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
            Destroy(this.gameObject, 3.0f);
        }
    }
}
