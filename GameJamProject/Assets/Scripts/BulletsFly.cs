using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsFly : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-Vector3.up * BulletSpeed * Time.deltaTime, Space.Self);        

        if (transform.position.z > 10.0f) {
            // Destroy object when it gets too far away
            Destroy(gameObject);   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bug" || collision.gameObject.tag == "Monster")
        {
            Debug.Log(gameObject.tag + " hit: " + collision.gameObject.tag);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
