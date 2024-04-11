using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;
    
    void Start()
    {
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            Player player = other.transform.GetComponent<Player>();
            Destroy(this.gameObject);
            other.transform.GetComponent<Player>().Damage();
        }

        if (other.tag == "PlayerLaser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
