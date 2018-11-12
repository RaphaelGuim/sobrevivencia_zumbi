using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {

	public GameObject Bala;
	public GameObject Cano;
	public AudioClip somTiro;
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			ControlaAudio.instancia.PlayOneShot(somTiro);
			Instantiate(Bala, Cano.transform.position, Cano.transform.rotation);
		}
	}
}
