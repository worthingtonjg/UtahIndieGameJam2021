using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using TMPro;
using Fungus;

public class Labrynth : MonoBehaviour
{
    public static Labrynth instance;
    public GameObject LevelLayout;
    public GameObject PlayerPrefab;
    public List<GameObject> TilePrefabs;
    public List<GameObject> bodyPrefabs;
    public GameObject EnemyPrefab;
    public NavMeshSurface surface;
    public Animation LightPulse;

    public GameObject PanelReincarnate;
    public GameObject PanelLeave;
    public GameObject PanelDamage;
    public GameObject PanelLab;
    public TMP_Text BodiesRemaining;
    public TMP_Text BodyCount;
    public Flowchart flowChart;

    private int bodyCount;
    private int bodiesRemaining;
    private List<GameObject> waypoints;
    private int mapSize = 5;
    private int centerTile = 2;
    private float startX = 19.5f;
    private float startY = -19.5f;
    private float tileSize = 10f;
    private int enemySpawnPoint;
    private int playerSpawnPoint;

    private PlayerModelSelector playerScript;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        GenerateLevel();
        SpawnPlayer();
        SpawnEnemy();
        SpawnBodies();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update() 
    {
        if(PanelLab.activeSelf && Input.GetKey(KeyCode.E))
        {
            if(bodyCount >= 10)
            {
                flowChart.ExecuteBlock("Win Game");
            }
            else
            {
                flowChart.ExecuteBlock("Lose Game");
            }
        }    
    }

    private void GenerateLevel()
    {
        float x = startX;
        float y = startY;

        for (int row = 0; row < mapSize; row++)
        {
            for (int col = 0; col < mapSize; col++)
            {
                if(row != centerTile || col != centerTile) 
                {
                    var prefab = TilePrefabs[Random.Range(0, TilePrefabs.Count)];
                    var tile = GameObject.Instantiate(prefab, new Vector3(x, 0, y), Quaternion.identity);
                }
                x -= tileSize;
            }
            x = startX;
            y += tileSize;
        }

        surface.BuildNavMesh();

        // Hide the cube used as colliders
        var collidersToHide = GameObject.FindGameObjectsWithTag("Collider").ToList();
        foreach(var colliderToHide in collidersToHide)
        {
            colliderToHide.SetActive(false);
        }

        // Show the layout
        LevelLayout.SetActive(true);

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();            
        if(waypoints.Count == 0) Debug.LogError("No Waypoints detected");
    }

    private void SpawnPlayer()
    {
        playerSpawnPoint = Random.Range(0, waypoints.Count);
        var waypoint = waypoints[playerSpawnPoint];
        
        var player = GameObject.Instantiate(PlayerPrefab, waypoint.transform.position, Quaternion.identity);
        playerScript = player.GetComponent<PlayerModelSelector>();
        waypoints.Remove(waypoint);
    }

    private void SpawnEnemy()
    {
        enemySpawnPoint = Random.Range(0, waypoints.Count);
        var waypoint = waypoints[enemySpawnPoint];
        
        GameObject.Instantiate(EnemyPrefab, waypoint.transform.position, Quaternion.identity);
        waypoints.Remove(waypoint);
    }

    private void SpawnBodies()
    {
        if(bodyPrefabs.Count == 0) return;

        foreach(var waypoint in waypoints)
        {
            var prefab = bodyPrefabs[Random.Range(0, bodyPrefabs.Count)];
            var body = GameObject.Instantiate(prefab, new Vector3(waypoint.transform.position.x, 0, waypoint.transform.position.z), Quaternion.identity);
            
            var waypointScript = waypoint.GetComponent<Waypoint>();
            waypointScript.body = body;

            var anim = body.GetComponent<Animator>();
            anim.SetTrigger("die");
            ++bodiesRemaining;
        }

        BodiesRemaining.text = bodiesRemaining.ToString();
    }

    public void TeleportBody() 
    {
        LightPulse.Play();
        ++bodyCount;
        DecrementBodies();
        BodyCount.text = bodyCount.ToString();
    }

    public bool BodyStolen()
    {
        bool hadBody = playerScript.ActivateGhost();
        DecrementBodies();
        return hadBody;
    }

    public void DecrementBodies()
    {
        --bodiesRemaining;
        BodiesRemaining.text = bodiesRemaining.ToString();

        if(bodiesRemaining <= 0)
        {
            PanelLab.SetActive(true);
        }
    }
}
