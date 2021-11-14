using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBugRoom : MonoBehaviour
{
    public GameObject Bug;
    public GameObject Monster;
    public GameObject player;

    // Start is called before the first frame update
    public void Start()
    {
        for(int x=0; x<5; x++) {
            GameObject bugObj = Instantiate(Bug, new Vector3(Random.Range(-3.0f, 3.0f), 6, Random.Range(-1.0f, 1.0f)), Quaternion.identity);
            bugObj.transform.LookAt(player.transform);
        }        
        for(int x=0; x<3; x++) {
            GameObject monsterObj = Instantiate(Monster, new Vector3(Random.Range(-4.0f, 4.0f), 0, Random.Range(-1.0f, 1.0f)), Quaternion.identity);
            monsterObj.transform.LookAt(player.transform);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}