using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerModelSelector : MonoBehaviour
{
    private GameObject ghostModel;
    public List<GameObject> playerModels;
    
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        ghostModel = GameObject.FindGameObjectWithTag("GhostModel");
        ActivateGhost();
    }

    void Update()
    {
        if(animator != null)
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }    

    public void ActivateGhost()
    {
        playerModels.ForEach(p => p.SetActive(false));
        ghostModel.SetActive(true);
        animator = ghostModel.GetComponent<Animator>();
    }

    public void Reincarnate(string name)
    {
        print(name);
        print(playerModels.Count);
        var body = playerModels.FirstOrDefault(p => name.Contains(p.name));
        if(body != null)
        {
            playerModels.ForEach(p => p.SetActive(false));
            ghostModel.SetActive(false);
            body.SetActive(true);
            animator = body.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag != "Waypoint") return;

        var script = other.GetComponent<Waypoint>();
        if(script.body != null && script.body.activeSelf)
        {
            Reincarnate(script.body.name);
        }
    }
}
