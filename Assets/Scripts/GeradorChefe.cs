using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeradorChefe : MonoBehaviour {

	private float tempoProximaGeracao;
	public float tempoEntreGeracoes = 60;
	public GameObject chefe;
	private ControlaInterface controlaInterface;
	public Transform[] posicoes;
	private GameObject Jogador;

	private void Start()
	{
		tempoProximaGeracao = tempoEntreGeracoes;
		controlaInterface = GameObject.FindObjectOfType<ControlaInterface>();
		Jogador = GameObject.FindWithTag(Tags.Jogador);


	}

	private void Update()
	{
		 
		if (Time.timeSinceLevelLoad > tempoProximaGeracao)
		{
			
			
			Instantiate(chefe,proximaPosicao(), Quaternion.identity);
			tempoProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
			controlaInterface.MostraAvisoChefe();
			 
		}
	}

	private Vector3 proximaPosicao()
	{
		Vector3 retorno = transform.position;
		float distancia = -1;

		foreach(Transform posicao in posicoes){
			float dist = Vector3.Distance(posicao.position, Jogador.transform.position);

			if (dist > distancia)
			{
				distancia = dist;
				retorno = posicao.position;
			}
		}
		return retorno;
	}
	
}
