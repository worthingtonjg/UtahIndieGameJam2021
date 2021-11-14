using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    public GameObject LaserBullet;

    [SerializeField]
    private float AimSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(LaserBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        }

        if(Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Mouse X") < 0)
        {
            transform.Rotate(-Vector3.forward * AimSpeed * Time.deltaTime);
        }

        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Mouse X") > 0)
        {
            transform.Rotate(Vector3.forward * AimSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Mouse Y") > 0) 
        {
            transform.Rotate(Vector3.left * AimSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Mouse Y") < 0)
        {
            transform.Rotate(-Vector3.left * AimSpeed * Time.deltaTime);
        }
    }
}
