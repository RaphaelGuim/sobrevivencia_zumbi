using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao : MonoBehaviour {

	public int Balas = 20;
	 

	private void Start()
	{
		 
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == Tags.Jogador)
		{
			other.GetComponent<ControlaArma>().AdicionaBalas(20);
			 
			Destroy(gameObject);

		}
	}
}
