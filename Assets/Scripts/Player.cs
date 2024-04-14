using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player sprite added
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject playerLaser;
    [SerializeField]
    private GameObject tripleShot;
    [SerializeField]
    public GameObject speedBoost;
    private float _canfire = -1f;
    private float _fireRate = .3f;

    private int _lives = 3;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTrippleShotActive = false;
    private bool _isSpeedPowerUpActive = false;
    private bool _isShieldsActive = false;


    void Start()
    {
        
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }
    }

    
    void Update()
    {
        CalculateMovement();
        FireLaser();

        

    }
    void CalculateMovement()
    {
        //input controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (_isSpeedPowerUpActive == false)
        {
            _speed = 7f;
        }
        else if (_isSpeedPowerUpActive)
        {
            _speed = 14f;
        }
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //y boundary
        if (transform.position.y >= 2)
        {
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //x boundary
        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            if (_isTrippleShotActive == false)
            {
                _canfire = Time.time + _fireRate;
                Instantiate(playerLaser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
            else if (_isTrippleShotActive == true)
            {
                _canfire = Time.time + _fireRate;
                Instantiate(tripleShot, transform.position, Quaternion.identity);
            }
        }
    }
    public void TripleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }
    IEnumerator TripleShotPowerDown()
    {
        while (_isTrippleShotActive == true)
        {
            yield return new WaitForSeconds(5f);
            _isTrippleShotActive = false;
        }
    }

    public void SpeedPowerUpActive()
    {
        _isSpeedPowerUpActive = true;
        StartCoroutine(SpeedPowerDown());
    }
    IEnumerator SpeedPowerDown()
    {
         while (_isSpeedPowerUpActive == true)
        {
            yield return new WaitForSeconds(5f);
            _isSpeedPowerUpActive = false;
        }
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
    }

    public void Damage()
    {
        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            return;
        }

        _lives --;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
