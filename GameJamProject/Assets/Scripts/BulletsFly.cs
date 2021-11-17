using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class BulletsFly : MonoBehaviour
{
    private Camera camera;
    private StartBugRoom StartBugRoomScript;
    private float BulletSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.Find("Main Camera");
        camera = obj.GetComponent<Camera>();
        StartBugRoomScript = camera.GetComponent<StartBugRoom>();
        var flowchartArray = GameObject.FindObjectsOfType<Fungus.Flowchart>();
        Debug.Log("Found " + flowchartArray.Length + " flowcharts");
        // flowchart = flowchartArray[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-Vector3.up * BulletSpeed * Time.deltaTime, Space.Self);        

        if (transform.position.z > 30.0f) {
            // Destroy object when it gets too far away
            Destroy(gameObject);   
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Bug" || collision.gameObject.tag == "Monster")
        {
            if (collision.gameObject.tag == "Bug" && StartBugRoomScript.BugCount > 0)
            {
                StartBugRoomScript.BugCount--;
            }
            else if (collision.gameObject.tag == "Monster" && StartBugRoomScript.MonsterCount > 0)
            {
                StartBugRoomScript.MonsterCount--;
            }
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

        if (StartBugRoomScript.BugCount <= 0 && StartBugRoomScript.MonsterCount <= 0)
        {
            Debug.Log("We're done. Time to exit.");
            Cursor.visible = true;
            StartBugRoomScript.Running = false;
            if (StartBugRoomScript.BugFlowchart != null)
            {
                StartBugRoomScript.BugFlowchart.ExecuteBlock("End Bug Scene");
            } 
            else
            {
                Debug.Log("flowchart is null.");
            }
        }
    }
}
