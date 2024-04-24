using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftShiftInput : MonoBehaviour
{
    
    [SerializeField]
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _player.PlayerThruster();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _player.PlayerNormalSpeed();
        }
    }
}
