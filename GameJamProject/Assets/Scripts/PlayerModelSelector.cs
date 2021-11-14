using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerModelSelector : MonoBehaviour
{
    private List<GameObject> playerModels;
    
    // Start is called before the first frame update
    void Start()
    {
        playerModels = GameObject.FindGameObjectsWithTag("PlayerModel").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
