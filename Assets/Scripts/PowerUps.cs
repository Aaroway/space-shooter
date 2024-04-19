using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    
    private float _speed = 3f;
    [SerializeField]
    private int _powerUpID;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if (transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerUpActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        Debug.Log("Default Values");
                        break;
                }
                

                Destroy(this.gameObject);
            }
        }
    }
}

