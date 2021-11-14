using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerModelSelector : MonoBehaviour
{   
    private GameObject ghostModel;
    public List<GameObject> playerModels;
    
    public Animator animator;

    private bool reincarnated;

    private bool canReincarnate;

    private bool canLeaveBody;

    private string bodyName;

    private Waypoint script;

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

        if(Input.GetKey(KeyCode.R))
        {
            Reincarnate();
        }

        if(Input.GetKey(KeyCode.L))
        {
            LeaveBody();            
        }
    }    

    public void ActivateGhost()
    {
        playerModels.ForEach(p => p.SetActive(false));
        ghostModel.SetActive(true);
        animator = ghostModel.GetComponent<Animator>();
        reincarnated = false;
    }

    public void LeaveBody()
    {
        if(!reincarnated) return;
        ActivateGhost();        
    }

    public void Reincarnate()
    {
        if(reincarnated) return;

        var body = playerModels.FirstOrDefault(p => bodyName.Contains(p.name));
        if(body != null)
        {
            playerModels.ForEach(p => p.SetActive(false));
            ghostModel.SetActive(false);
            body.SetActive(true);
            script.body.SetActive(false);
            animator = body.GetComponent<Animator>();
            reincarnated = true;
            canReincarnate = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Waypoint") 
        {
            if(reincarnated) return;

            script = other.GetComponent<Waypoint>();
            if(script.body != null && script.body.activeSelf)
            {
                bodyName = script.body.name;
                script.smoke.SetActive(true);
                Labrynth.instance.PanelReincarnate.SetActive(true);
                canReincarnate = true;
            }
        }

        if(other.tag == "Savepoint")
        {
            canLeaveBody = true;
            Labrynth.instance.PanelLeave.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Waypoint") 
        {
            script.smoke.SetActive(false);
            Labrynth.instance.PanelReincarnate.SetActive(false);
            canReincarnate = false;
            script = null;
            bodyName = string.Empty;
        }

        if(other.tag == "Savepoint")
        {
            canLeaveBody = false;
            Labrynth.instance.PanelLeave.SetActive(false);
        }
    }
}
