using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAround : MonoBehaviour
{
    public float BugSpeed = 1.0f;
    private GameObject Rifle;
    private int counter = 0;
    private Vector3 direction = Vector3.back;

    // Start is called before the first frame update
    void Start()
    {
        Rifle = GameObject.Find("Rifle");
        if (Rifle == null)
        {
            Rifle = GameObject.Find("Sci-Fi Rifle_fbx");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 100)
        {
            counter = 0;
            ChangeDirections(false);
        }
        counter++;
        // if (transform.position.y > 10.0f)
        // {
        //     transform.Translate(Vector3.down * BugSpeed * Time.deltaTime, Space.Self);
        // } else
        // {
        //     transform.Translate(direction * BugSpeed * Time.deltaTime, Space.Self);
        // }
    }

    private void ChangeDirections(bool hitWall)
    {
        if (hitWall && Rifle != null)
        {
            direction = Rifle.transform.position;
        }
        else
        {
            int rand = Random.Range(0,4);
            switch (rand)
            {
                case 0:
                    // transform.Rotate(90.0f, 0.0f, 0.0f);
                    if(direction == Vector3.forward)
                    {
                        direction = Vector3.right;
                    } else if (direction == Vector3.right)
                    {
                        direction = Vector3.back;
                    } else if (direction == Vector3.back)
                    {
                        direction = Vector3.left;
                    } else
                    {
                        direction = Vector3.forward;
                    }

                break;
                case 1:
                    // transform.Rotate(180.0f, 0.0f, 0.0f);
                    if(direction == Vector3.forward)
                    {
                        direction = Vector3.back;
                    } else if (direction == Vector3.right)
                    {
                        direction = Vector3.left;
                    } else if (direction == Vector3.back)
                    {
                        direction = Vector3.forward;
                    } else
                    {
                        direction = Vector3.right;
                    }
                break;
                case 2:
                    // transform.Rotate(270.0f, 0.0f, 0.0f);
                    if(direction == Vector3.forward)
                    {
                        direction = Vector3.left;
                    } else if (direction == Vector3.right)
                    {
                        direction = Vector3.forward;
                    } else if (direction == Vector3.back)
                    {
                        direction = Vector3.right;
                    } else
                    {
                        direction = Vector3.back;
                    }
                break;
                case 3:
                    // transform.Rotate(0.0f, 0.0f, 0.0f);
                break;
            }        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arena")
        {
            Debug.Log(gameObject.tag + " hit " + collision.gameObject.tag);
            ChangeDirections(true);
        }
    }

}
