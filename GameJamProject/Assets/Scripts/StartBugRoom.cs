using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBugRoom : MonoBehaviour
{
    public GameObject Bug;
    public GameObject Monster;
    public GameObject Rifle;
    public static int BugCount = 10;
    public static int MonsterCount = 7;

    // Start is called before the first frame update
    public void Start()
    {
        Cursor.visible = false;
        for(int x=0; x<BugCount; x++) {
            GameObject bugObj = Instantiate(Bug, new Vector3(Random.Range(-15.0f, 15.0f), 4.0f, Random.Range(5.0f, 15.0f)), Quaternion.identity);
            bugObj.transform.LookAt(Rifle.transform);
        }        
        for(int x=0; x<MonsterCount; x++) {
            GameObject monsterObj = Instantiate(Monster, new Vector3(Random.Range(-17.0f, 17.0f), 0, Random.Range(5.0f, 15.0f)), Quaternion.identity);
            monsterObj.transform.LookAt(Rifle.transform);
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
}
