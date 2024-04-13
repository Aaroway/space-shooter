using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    private float _speed = 8.3f;
    


    
    void Update()
    {
        
        
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        
        if (transform.position.y >= 8f)
        {
            Destroy(this.gameObject);
        }


    }
}
