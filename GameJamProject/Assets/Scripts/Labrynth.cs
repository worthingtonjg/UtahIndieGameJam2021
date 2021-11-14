using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Labrynth : MonoBehaviour
{
    public int Enemies;
    public List<GameObject> Tiles;

    public GameObject Enemy;

    public NavMeshSurface surface;

    private List<GameObject> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();
        
        float startX = 14.5f;
        float startY = -15.5f;    

        for(int row = 0; row < 4; row++)
        {
            for(int col = 0; col < 4; col++)
            {
                var prefab = Tiles[Random.Range(0, Tiles.Count)];
                var tile = GameObject.Instantiate(prefab, new Vector3(startX, 0, startY), Quaternion.identity);
                startX-=10;
            }
            startX = 14.5f;
            startY+=10;
        }

        for(int i = 0; i < Enemies; i++)
        {

        }

        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
