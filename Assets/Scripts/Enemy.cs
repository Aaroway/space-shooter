using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
   
    
    private Player _player;
    
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
            Destroy(this.gameObject);
            _player.Damage();
        }

        if (other.tag == "PlayerLaser")
        {
            Destroy(other.gameObject); 
            if (_player != null)
            {
                _player.AddScore(10);
            }
            Destroy(this.gameObject);
            
        }
    }
}
