
using UnityEngine;
using System.Collections;

public class Figure : MonoBehaviour {
	
	public string   shape;
	public Vector3  position;
	public int 	    score;
	private float    speed = -8;
	public string   respawnZone;

	private int      initialOffset = 15;
	private Vector3 initialUP;
	private Vector3 initialDOWN;
	private Vector3 initialLEFT;
	private Vector3 initialRIGHT;

	//prova
	public Transform prefab;

	//Varibles Estatiques
	private static int     maxFigures = 4;
	private static string  UP    = "UP";
	private static string  DOWN  = "DOWN";
	private static string  LEFT  = "LEFT";
	private static string  RIGHT = "RIGHT";

	//Variables per a la rotacio
	public float xRotation = 90.0f;
	public float xRotation1 = 0.0f;
	public float RotationSpeed = 2.0f;

	private int timeGrow;
	private int timeLimit;
	private int timeDiminish;


	// Use this for initialization
	void Start () {
		initialization ();
		whereAmI ();
		timeDiminish = 20;
		timeLimit = 20;
		this.timeGrow = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.SqrMagnitude (this.transform.position) < 0.05) {
			this.timeGrow += 1;
			this.transform.position = new Vector3(0,0,0);
			this.transform.Rotate(Vector3.up * Time.deltaTime * 100);

		} else {
			moveToCenter ();
		}


	}

	void initialization (){
		this.initialUP    = new Vector3 (0,+this.initialOffset,0);
		this.initialDOWN  = new Vector3 (0,-this.initialOffset,0);
		this.initialLEFT  = new Vector3 (-this.initialOffset,0,0);
		this.initialRIGHT = new Vector3 (+this.initialOffset,0,0);
	}



	void whereAmI (){
		//Agafem un random per cada posicio cardinal en 2D.
		int rndPos = Random.Range (1, 5);

		switch (rndPos) {
		case 1: 
			this.position = initialUP;
			this.respawnZone = UP;
			break;
		case 2: 
			this.position = initialRIGHT;
			this.respawnZone = RIGHT;
			break;
		case 3: 
			this.position = initialDOWN;
			this.respawnZone = DOWN;
			break;
		case 4: 
			this.position = initialLEFT;
			this.respawnZone = LEFT;
			break;
		}
		//Movem la figura al origen que li pertoca
		this.transform.position = this.position;

	}

	void moveToCenter (){
		Vector3 direction = Vector3.Normalize(this.position);
		//Time.deltatime
		this.transform.Translate (direction * this.speed*0.7f * Time.deltaTime);
	}
}


