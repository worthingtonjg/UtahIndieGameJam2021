using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;
using TMPro;

public class RingsController : MonoBehaviour
{
    public GameObject StartRing;
    public TMP_Text TimeRemaining;

    private Vector3 currentPosition;
    private GameObject currentRing;
    
    public Flowchart flowchart;
    public GameObject Ship;
    public TMP_Text collectedText;

    public float FlySpeed = 25f;
	public float TurnSpeed = 40f;
	public int TotalRings = 50;
	public int TotalSeconds = 60;
	public int ChangeChanceMin = 30;
	public int ChangeChanceMax = 70;
	public int ChangeChanceStartMin = 20;
	public int ChangeChanceStartMax = 30;
	public int MaxChangeChanceIncrease = 5;
	public float MinAngle = 20f;
	public float MaxAngle = 35f;
	public float SpaceBetween = 30f;
    
    private  bool started;
    // Start is called before the first frame update

    private float timeRemaining = 60;
    private ship shipScript;


    void Start()
    {
        shipScript = Ship.GetComponent<ship>();
        currentRing = StartRing;

        int changeChance = Random.Range (ChangeChanceMin, ChangeChanceMax);
		int changeValue = Random.Range(ChangeChanceStartMin, ChangeChanceStartMax);
		float angle = Random.Range (MinAngle, MaxAngle);

		float xAngle = Random.Range (-angle, angle); 
		float zAngle = Random.Range (-angle, angle); 

		for (int i = 0; i <= TotalRings * 2; i++) {
			var nextRing = (GameObject)Instantiate (StartRing, currentRing.transform.position, currentRing.transform.rotation);		

			if(changeValue >= changeChance)
			{
				angle = Random.Range (MinAngle, MaxAngle);
				xAngle = Random.Range (-angle, angle); 
				zAngle = Random.Range (-angle, angle); 
			}

			if(changeValue < ChangeChanceMax - MaxChangeChanceIncrease)
				changeValue += Random.Range(1, MaxChangeChanceIncrease);

			nextRing.transform.Rotate(xAngle,0,zAngle);
			nextRing.transform.Translate(0f, SpaceBetween, 0f);

			currentRing = nextRing;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(!started) return;

        timeRemaining -= Time.deltaTime;

        if(timeRemaining <= 0)
        {
            timeRemaining = 0;
            started = false;
            collectedText.text = $"Rings collected: {shipScript.ringsCount}";
            flowchart.ExecuteBlock("End Game");
        }

        TimeRemaining.text = Mathf.Round(timeRemaining).ToString();

        if(!started) return;

        Ship.transform.Rotate ( -1 * ((Input.GetAxis ("Vertical") + Input.GetAxis("Vertical")) * TurnSpeed * Time.deltaTime), 0, 0);

		Ship.transform.Rotate (0, 0, -1 * ((Input.GetAxis ("Horizontal") + Input.GetAxis("Horizontal"))* TurnSpeed * Time.deltaTime));

		Ship.transform.Translate(0f, FlySpeed * Time.deltaTime, 0f);
    }

    public void StartGame() 
    {
        started = true;
    }
}
