using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Animator _anim;

    private Collider2D _collider;


    private AudioSource _audioSource;
    
    private Player _player;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _player = GameObject.Find("Player").GetComponent<Player>();
        
        _collider = GetComponent<Collider2D>();

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is Null");
        }
    }

    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-11, 11);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player1")
        {
            EnemyDeath();
            _player.Damage();
        }

        if (other.tag == "PlayerLaser")
        {
            Destroy(other.gameObject); 
            if (_player != null)
            {
                _player.AddScore(10);
            }
            EnemyDeath();
        }
    }
    public void EnemyDeath()
    {
        _anim.SetTrigger("OnEnemyDeath");
        
        _enemySpeed = 0;
        _collider.enabled = false;
        _audioSource.Play();
        Destroy(this.gameObject, 1f);
    }
}
