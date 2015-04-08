using UnityEngine;
using System.Collections;

public class BackgroundMenuMusic : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		// dont destroy the object when the scene change
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

