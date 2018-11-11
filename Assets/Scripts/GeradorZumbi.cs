using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbi : MonoBehaviour {

	public GameObject modeloZumbi;
	float contador = 0;
	public float tempoGerar = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		contador += Time.deltaTime;

		if (contador >= tempoGerar)
		{
			contador = 0;
			Instantiate(modeloZumbi, transform.position, transform.rotation);			
			 
		}
	}
}
