using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    public GameObject LaserBullet;
    public Camera camera;
    private StartBugRoom StartBugRoomScript;
    private float AimSpeed = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartBugRoomScript = camera.GetComponent<StartBugRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!StartBugRoomScript.Running) {return;}

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown("space")) {
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
