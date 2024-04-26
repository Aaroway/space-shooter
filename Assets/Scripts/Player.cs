using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float _thrusterSpeedMod = 1.5f;
    private float _speedBoostMod = 2.2f;
    private float _speed = 7f;
    [SerializeField]
    private GameObject _playerLaser;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _speedBoost;
    private float _canfire = -1f;
    private float _fireRate = .3f;

    private int _lives = 3;
    [SerializeField]
    private SpawnManager _spawnManager;

    private bool _isTrippleShotActive = false;
    private bool _isSpeedPowerUpActive = false;
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    private UI_Manager _uiManager;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private AudioSource _audioSource;
    private bool _isThrusterActive = false;
    private int _ammunition = 20;
    private bool _isAmmoDepleted = false;
    private bool _maxAmmo = false;
    private int _fullAmmo = 20;
    


    //speed thruster not working

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();


        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

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

        if (_isThrusterActive)
        {
            transform.Translate(Vector3.right * horizontalInput * (_speed * _thrusterSpeedMod) * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * (_speed * _thrusterSpeedMod) * Time.deltaTime);
        }
        else if (_isThrusterActive == false)
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }


        if (_isSpeedPowerUpActive)
        {
            transform.Translate(Vector3.right * horizontalInput * (_speed * _speedBoostMod) * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * (_speed * _speedBoostMod) * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }
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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire && !_isAmmoDepleted)
        {
            _canfire = Time.time + _fireRate;
            _ammunition--;
            AmmunitionCount(_ammunition);

            if (_ammunition <= 0)
            {
                _isAmmoDepleted = true;
            }


            if (_isTrippleShotActive == false)
            {

                Instantiate(_playerLaser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
                _audioSource.Play();
            }
            else if (_isTrippleShotActive == true)
            {

                Instantiate(_tripleShot, transform.position, Quaternion.identity);
                _audioSource.Play();
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

    public void PlayerThruster()
    {
        _isThrusterActive = true;
    }
    public void PlayerNormalSpeed()
    {
        _isThrusterActive = false;
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
        _shieldVisualizer.gameObject.SetActive(true);
    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.gameObject.SetActive(false);
            return;
        }


        _lives--;

        _uiManager.UpdateLives(_lives);
        _uiManager.UpdateShieldBar(_lives);
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
        }
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    public void AmmunitionCount(int count)
    {
        _ammunition = count;
        if (_maxAmmo == true)
        {
            _ammunition = 20;
        }
        _uiManager.UpdateAmmoCount(_ammunition);
    }

    public void AmmunitionRefilled()
    {
        _ammunition = _fullAmmo;
        _uiManager.UpdateAmmoCount(_ammunition);
    }

    public void LifePowerUp()
    {
        if (_lives < 3)
        {

            _lives++;
            _uiManager.UpdateLives(_lives);
            _uiManager.UpdateShieldBar(_lives);
            return;
        }
    }
}
