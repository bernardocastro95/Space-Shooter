using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private enum _powerupIds
    {
        tripleShot = 0,
        speed = 1,
        shield = 2
    }
    [SerializeField]
    private int _powerupId;
    [SerializeField]
    private AudioClip _powerupClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5f)
        {
            
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_powerupClip, Camera.main.transform.position);
            if(player != null)
            {
                switch (_powerupId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldEnabled();
                        break;
                    default:
                        break;
                }
               
            }
            Destroy(this.gameObject);
        }
    }
    
}
