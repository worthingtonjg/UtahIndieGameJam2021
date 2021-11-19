using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StartBugRoom : MonoBehaviour
{
    public GameObject Bug;
    public GameObject Monster;
    public GameObject Rifle;
    public Flowchart BugFlowchart;
    public bool Running = false;
    public int BugCount = 10;
    public int MonsterCount = 7;

    // Start is called before the first frame update
    public void Start()
    {
        Cursor.visible = false;
        for(int x=0; x<BugCount; x++) {
            GameObject bugObj = Instantiate(Bug, new Vector3(Random.Range(-15.0f, 15.0f), 4.0f, Random.Range(-15.0f, 15.0f)), Quaternion.identity);
            //bugObj.transform.LookAt(Rifle.transform);
            bugObj.transform.Rotate(Vector3.up, ChangeDirection(), Space.Self);
        }        
        for(int x=0; x<MonsterCount; x++) {
            GameObject monsterObj = Instantiate(Monster, new Vector3(Random.Range(-14.0f, 14.0f), 0, Random.Range(-15.0f, 15.0f)), Quaternion.identity);
            //monsterObj.transform.LookAt(Rifle.transform);
            monsterObj.transform.Rotate(Vector3.up, ChangeDirection(), Space.Self);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        Running = true;
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
