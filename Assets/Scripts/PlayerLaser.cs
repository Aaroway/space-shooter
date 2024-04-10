using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    private float _speed = 8.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //instantiates and moves
        
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //bounds
        if (transform.position.y >= 8f)
        {
            Destroy(this.gameObject);
        }


    }
}
