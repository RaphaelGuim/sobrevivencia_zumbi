using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour {

	private int quantidadeCura = 10;
	private float tempoDestruir = 5f;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == Tags.Jogador)
		{
			other.GetComponent<ControlaJogador>().CurarVida(quantidadeCura);
			Destroy(gameObject);
			
		}
	}

	private void Start()
	{

		Destroy(gameObject, tempoDestruir);
		 
	}
}
