using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletsFly : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
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

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bug" || collision.gameObject.tag == "Monster")
        {
            //Debug.Log(gameObject.tag + " hit: " + collision.gameObject.tag);
            if (collision.gameObject.tag == "Bug" && StartBugRoom.BugCount > 0)
            {
                StartBugRoom.BugCount--;
                //Debug.Log(StartBugRoom.BugCount + " bugs left.");
            }
            else if (collision.gameObject.tag == "Monster" && StartBugRoom.MonsterCount > 0)
            {
                StartBugRoom.MonsterCount--;
                //Debug.Log(StartBugRoom.MonsterCount + " monsters left.");
            }
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (StartBugRoom.BugCount <= 0 && StartBugRoom.MonsterCount <= 0)
        // if (GameObject.FindWithTag("Bug") == null && GameObject.FindWithTag("Monster") == null)
        {
            Cursor.visible = true;
            // AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("ShootBugs");
            // while (asyncUnload != null && !asyncUnload.isDone)
            // {
            //     yield return null;
            // }
            Debug.Log("We're done. Time to exit.");
            Application.Quit();
        }  
        return null;      
    }
}
