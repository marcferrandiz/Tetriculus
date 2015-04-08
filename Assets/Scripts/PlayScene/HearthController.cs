using UnityEngine;
using System.Collections;

public class HearthController : MonoBehaviour {

	public int maxNumLives; 
	private int numLives;// number of lives 
	private GameObject[] lives;
	private Quaternion rotation = Quaternion.identity;

	public GameObject prefabHeath; // 3D hearth

	// Use this for initialization
	void Start () {
		numLives = maxNumLives;
		// initialize the array
		lives = new GameObject[maxNumLives-1];
		rotation.eulerAngles = new Vector3 (0,90,0);// base rotation
		// instantiate the number of lives that we want
		for (int i=0; i<maxNumLives-1; i++) {
			// instantiate the lives left
			lives[i] = Instantiate(prefabHeath, new Vector3((this.gameObject.transform.position.x-3f) - i*3f, this.gameObject.transform.position.y, this.gameObject.transform.position.z), rotation) as GameObject;
		}

	}


	// Update is called once per frame, draw the adecuate number of lives.
	void Update () {
	
		MeshRenderer mesh;

		// remove all the hearths
		for (int i=0; i<maxNumLives-1; i++) {

			mesh=lives[i].gameObject.GetComponent<MeshRenderer>();
			mesh.enabled=false;
		}

		// make visible the correct hearths
		for (int i=0; i<numLives-1; i++) {
			
			mesh=lives[i].gameObject.GetComponent<MeshRenderer>();
			mesh.enabled=true;
		}

	}


	private void playErrorSound()
	{
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		audio.PlayOneShot (audio.clip);
	}
	

	// Substract live
	public void substractLive()
	{
		numLives--;

		// depending of the number of lives
		if (numLives == 0) {
			print ("GAME OVER");
			Application.LoadLevel ("GameOver");
		} else 
		{
			print ("Numero de vides:" + numLives);
			playErrorSound();
		}
	}




}
