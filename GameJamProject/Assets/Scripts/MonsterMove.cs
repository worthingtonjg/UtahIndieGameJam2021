using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float MonsterSpeed = 1.0f;
    private GameObject Rifle;
    private int counter = 0;
    private int steps = 0;
    private float direction = 0;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Rifle = GameObject.Find("Rifle");
        if (Rifle == null)
        {
            Rifle = GameObject.Find("Sci-Fi Rifle_fbx");
        }
        animator = gameObject.GetComponent<Animator>();
        steps = Random.Range(100, 1000);
        animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > steps)
        {
            counter = 0;
            steps = Random.Range(100, 1000);
            direction = ChangeDirection();
            transform.Rotate(Vector3.up, direction, Space.Self);
        }
        counter++;
        float distance = Vector3.Distance (transform.position, Rifle.transform.position);
        if (distance > 19.0f)
        {
            transform.LookAt(Rifle.transform);            
        } 
        transform.Translate(Vector3.forward * MonsterSpeed * Time.deltaTime, Space.Self);
    }

    private float ChangeDirection()
    {
        float dir = 0;
        switch (Random.Range(0,4))
        {
            case 0:
                dir = 90;
            break;
            case 1:
                dir = 180;
            break;
            case 2:
                dir = -90;
            break;
            case 3:
                dir = 0;
            break;
        } 
        return dir;       
    }    
}
