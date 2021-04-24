using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioSource _explosionSource;
    void Start()
    {
        _explosionSource = GetComponent<AudioSource>();
        if(_explosionSource == null)
        {
            Debug.LogError("Audio Source NULL");
        }
        else
        {
            _explosionSource.clip = _explosionClip;
        }
         _explosionSource.Play();
        Destroy(this.gameObject, 3.0f);
       
    }

}
