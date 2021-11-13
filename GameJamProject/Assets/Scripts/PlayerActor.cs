using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    public KeyCode interactKey;

    private readonly List<GameObject> triggers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            foreach (var _gameObject in triggers)
            {
                _gameObject.GetComponent<Interactable>()?.Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggers.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        triggers.Remove(other.gameObject);
    }
}
