using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cage : MonoBehaviour
{
    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCage()
    {
        anim.Play();
    }
}
