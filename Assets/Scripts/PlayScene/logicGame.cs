using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class logicGame : MonoBehaviour {


	public GameObject objectGame;
	public GameObject lives;

	// creem el serial port 
	SerialPort codeObject = new SerialPort("COM6",115200);

	// Use this for initialization
	void Start () {
	
	
		// Obrim el port del Arduino
		codeObject.Open();
		// Fixem una tasa de refresc per comprobar la entrada
		codeObject.ReadTimeout = 1;
		 
		print ("Ian aquí tienes tu ARDUINO con UNITY");

	}
	
	// Update is called once per frame
	void Update () {
		
		// controlem si el port esta obert 
		if (codeObject.IsOpen) //OK
		{

			try 
			{
				string value = codeObject.ReadLine();
				print ("Estoy en el try :)  \n");
				// cridem la funcio que comproba si es correcte

				print (value);
				//print (codeObject.IsOpen);
				//codeObject.BaseStream.Flush();
				//correctColor(int.Parse(value));

			}catch(System.Exception)
			
			{
				print ("Estoy en el catch :(  \n");
			}
		}


	}

	void correctColor(int code)
	{

		if (code >= 400) 
		{
			print ("Correct Color !!! Borrem el cub");
			Destroy(objectGame);


		}else if(code < 400){

			print ("Wrong Color !!!!! Borrem el cub ");
			//Destroy(objectGame);

		}

	}

}
