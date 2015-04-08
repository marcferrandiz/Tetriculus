using UnityEngine;
using System.Collections;

public class ScoreCounter : MonoBehaviour {

	private TextMesh ScoreText;// Score:0
	private string actScorePoints="Score: 0";
	private GameObject plusScore; // +120
	private int scorePoints=0;

	public GameObject prefabPlusScore;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);

	}
	
	// Update is called once per frame
	void Update () {
		// take mesh
		ScoreText = gameObject.GetComponent<TextMesh>();

		// display it !!
		ScoreText.text = actScorePoints;


		// destroy plusScore "+120" when it's on a determinate position
		if (plusScore != null) {
			if (plusScore.transform.position.y <= 5.25)
				Destroy (plusScore);
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			print ("Space Clicked!");
			Application.LoadLevel("GameOver");
		}
	}


	private void playScoreSound()
	{
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		audio.PlayOneShot (audio.clip);
	}


	public void changeScore(string operation,char figure)
	{
		int points=0;
	


		switch(figure)
		{
			// triangle
			case 't':
				points=120;
				// depending of the operation add or substract
				scorePoints = (string.Equals(operation,"add"))? scorePoints + points : scorePoints - points ;	
			break;
			//square
			case 'q':

				points=130;
				// depending of the operation add or substract
				scorePoints = (operation == "add")? scorePoints + points : scorePoints - points ;	
			break;
			// rombo
			case 'r':
				points=140;
				// depending of the operation add or substract
				scorePoints = (operation == "add")? scorePoints + points : scorePoints - points ;	
			break;
			//cercle
			case 'c':
				points=150;
				// depending of the operation add or substract
				scorePoints = (operation == "add")? scorePoints + points : scorePoints - points ;	
			break;
			default:
			break;
		}

		if (points != 0) {
			// instantiate the effect "+120" points

			char mesMenys = (operation == "substract")? '-' : '+';
			plusScore = Instantiate(prefabPlusScore, new Vector3((this.gameObject.transform.position.x), this.gameObject.transform.position.y, this.gameObject.transform.position.z),Quaternion.identity) as GameObject;
			plusScore.GetComponent<TextMesh>().text = mesMenys+ points.ToString();


			playScoreSound ();
		
		
		}
		// concat total string points
		actScorePoints = "Score: " + scorePoints.ToString ();

	}


}
