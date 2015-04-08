
﻿using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class checker : MonoBehaviour {
	public  GameObject triangle;
	public  GameObject square;
	public  GameObject circle;
	public  GameObject diamond;
	public  GameObject pentagon;
	public  GameObject lshape;
	public  int timeRespawn;

	public float xRotation = 90.0f;
	public float xRotation1 = 0.0f;
	public float RotationSpeed = 2.0f;

	private int respawnBucle = 0;
	private static int maxFigures = 4;
	private int numFigures = 0;
	private Vector3 initialScale;
	private int timeGrow;
	private bool llegend=false;// activate the super difficulty level
	private MeshRenderer marcador3D_mesh;
	bool running2;// coroutine marcador3D

	// more difficulty
	private GameObject difficultyText;
	public GameObject difficultyPrefab;
	private int correctChecket;
	private bool changeDificulty=true;
	bool running; // coroutine difficultyText
	
	//lives controller
	private GameObject livesObject;
	private HearthController scriptLives;
	// score controller
	private GameObject scoreObject;
	private ScoreCounter scriptScore;

	//marcador
	private GameObject marcador3D;
	private marcador3D marcador3D_Script;

	//llegend
	private GameObject difficultyLlegend;
	public GameObject difficultyLlegend_prefab;


	//Arduino
	// creem el serial port 
	SerialPort codeObject = new SerialPort("COM4",115200);
	private char figure;

	// Use this for initialization
	void Start () {
		figure='ñ';
		respawnBucle = 0;
		this.timeGrow = 0;
		initialScale = this.transform.localScale;
		// inicialize livesObject
		livesObject = GameObject.Find ("HearthController");
		scriptLives = livesObject.GetComponent<HearthController> ();
		// inicialize scoreObject
		scoreObject = GameObject.Find ("UIText");
		scriptScore = scoreObject.GetComponent<ScoreCounter>();
		correctChecket = 0;
		// initialize marcador3D
		marcador3D = GameObject.Find ("marcador3D");
		marcador3D_Script = marcador3D.GetComponent<marcador3D>(); 
		marcador3D_Script.enabled = false;
		marcador3D_mesh = marcador3D.GetComponent<MeshRenderer>(); 
		
		// Obrim el port del Arduino
		codeObject.Open();
		// Fixem una tasa de refresc per comprobar la entrada
		codeObject.ReadTimeout = 1;

		//Instantiate (cylinder, cylinder.transform.position, Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {
		checkFigure ();
		//print(figure);
		respawnBucle++;

		if (respawnBucle == timeRespawn) {
			respawnFigure ();
			respawnBucle = 0;
		}
		Transform child;
		for (int i = 0; i <this.transform.childCount; i++){
			child = this.transform.GetChild (i);

	

			//VALIDACIO OK ,si esta al checker
			if(Vector3.SqrMagnitude (child.position) < 0.05 ){
				marcador3D_Script.enabled = true; // scale marcador3D
				this.timeGrow += 1;

				if (string.Equals(System.Convert.ToString(child.name[0]),System.Convert.ToString(figure)) && !llegend || confirmFigure(child.name[0]) && llegend){ // si la encertat

					Destroy(child.gameObject);
					respawnFigure ();
					respawnBucle = 0;
					print (figure);
					scriptScore.changeScore("add",figure);
					figure='ñ';
					correctChecket++;// one more checket correctly 
					//wait X seconds and deactivate it
					StartCoroutine(feedbackOK());

				}else if( figure != 'ñ'){ 
					scriptLives.substractLive ();
					Destroy(child.gameObject);
					figure='ñ';
					marcador3D_Script.enabled = false; // no scale
					StartCoroutine(feedbackKO());

				}
			}else{
				if (figure != 'ñ'){// si no esta dins el marcador i ha checkejat cualsevol figura
					print ("Hauria de restar punts:   "+ System.Convert.ToChar(child.name[0]));
					scriptScore.changeScore("substract",System.Convert.ToChar(child.name[0]));
					figure='ñ';
				}
			}

			//we have to grow the level ?
			moreDificult();


			//VALIDACIO KO (se li passa el temps)
			if(Vector3.SqrMagnitude (child.position) < 0.05 && this.respawnBucle == this.timeRespawn-1){
				//perdo vida
				scriptLives.substractLive ();
				//elimino figura
				Destroy(child.gameObject);
				figure='ñ';
				marcador3D_Script.enabled = false;// no scale
				StartCoroutine(feedbackKO());
				//isOnChecker = true;
			}
		}




	}

	void respawnFigure (){

		//Agafem un random i assignem el tipus de figura
		int rndFig = Random.Range (1, maxFigures + 1);
		this.numFigures++;
		switch (rndFig) {
		case 1: 
			GameObject instance1 = Instantiate (this.triangle, this.triangle.transform.position, Quaternion.identity) as GameObject;
			instance1.transform.SetParent(this.gameObject.transform);
			break;
		case 2: 
			GameObject instance2 = Instantiate (this.square, this.square.transform.position, Quaternion.identity) as GameObject;
			instance2.transform.SetParent(this.gameObject.transform);
			break;
		case 3: 
			GameObject instance3 = Instantiate (this.circle, -this.circle.transform.position, Quaternion.identity) as GameObject;
			instance3.transform.SetParent(this.gameObject.transform);
			//instance3.transform.Rotate(new Vector3(90,0,0));
			break;
		case 4: 
			GameObject instance4 = Instantiate (this.diamond, this.diamond.transform.position, Quaternion.identity) as GameObject;
			instance4.transform.SetParent(this.gameObject.transform);
			break;
		case 5: 
			GameObject instance5 = Instantiate (this.pentagon, this.pentagon.transform.position, Quaternion.identity) as GameObject;
			instance5.transform.SetParent(this.gameObject.transform);
			break;
		case 6: 
			GameObject instance6 = Instantiate (this.lshape, this.lshape.transform.position, Quaternion.identity) as GameObject;
			instance6.transform.SetParent(this.gameObject.transform);
			break;
		}

	}

	// Comprobar figura
	private void checkFigure()
	{

		// controlem si el port esta obert 
		if (codeObject.IsOpen) //OK
		{
			
			try 
			{
				string aux = codeObject.ReadLine();
				this.figure = System.Convert.ToChar(aux);
				// cridem la funcio que comproba si es correcte
				//print (figure);



			}catch(System.Exception)
			{

			}
		}


	}


	// FeedBack OK, correct figure
	IEnumerator feedbackOK()
	{
		running2 = true;

		marcador3D_mesh.material.color=Color.green;
		while (running2)
		{

			yield return new WaitForSeconds(1f);
			marcador3D_mesh.material.color=Color.grey;

		}
	}

	// FeedBack KO, incorrect figure
	IEnumerator feedbackKO()
	{
		running2 = true;
		marcador3D_mesh.material.color=Color.red;
		while (running2)
		{
			yield return new WaitForSeconds(1f);
			marcador3D_mesh.material.color=Color.grey;
		}

	}

	IEnumerator removeText()
	{
		running = true;
		
		while (running)
		{
			Debug.Log("TestCoroutine()");
			yield return new WaitForSeconds(7);
			difficultyText.gameObject.SetActive(false);
		}
	}


	// Confirm that the figure that the user insert is on the correct llegend
	private bool confirmFigure(char figureScreen)
	{

		// figure is the figure of the user
		// figureScreen is the figure that appears on the screen
		// now figureScreen is correct with figure
				// square=q					triangle=t
				// triangle=t				square=q
				// circle=c					rombe=r
				// rombe/Diamond=r			circle=c
		bool ok=false;

		switch (figureScreen) 
		{
			case 'q':	
				if(figure == 't')ok=true;
			break;

			case 't':	
				if(figure == 'q')ok=true;
			break;

			case 'c':	
				if(figure == 'r')ok=true;
			break;

			case 'r':	
				if(figure == 'c')ok=true;
			break;


		}

		return ok;
	}

	// up the dificulty of the respawn
	public void moreDificult()
	{
		if (correctChecket == 10 && changeDificulty == true) 
		{
			timeRespawn = timeRespawn - 100;
			changeDificulty = false;
			difficultyText = Instantiate(difficultyPrefab,new Vector3(-10.02f,-8.81f,2.36f), Quaternion.identity) as GameObject;
			//wait X seconds and deactivate it
			StartCoroutine(removeText());
		}else if(correctChecket == 15 &&  llegend == false)
		{
			//difficultyText = Instantiate(difficultyPrefab,new Vector3(-10.02f,-8.81f,2.36f), Quaternion.identity) as GameObject;

			llegend=true;
			// text
			difficultyText.transform.localPosition = new Vector3(-20.91f,-8.81f,2.36f);
			difficultyText.GetComponent<TextMesh>().text="Super Difficulty, check the legend";
			difficultyText.gameObject.SetActive(true);
			StartCoroutine(removeText());
			// llegend
			difficultyLlegend = Instantiate(difficultyLlegend_prefab,new Vector3(25.8f,-0.2f,9.7f),Quaternion.identity) as GameObject;
			difficultyLlegend.gameObject.transform.eulerAngles = new Vector3(90f,200f,0f);
		}
	}





}
﻿
