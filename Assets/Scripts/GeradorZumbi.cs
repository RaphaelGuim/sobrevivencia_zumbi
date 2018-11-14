using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbi : MonoBehaviour {

	public GameObject modeloZumbi;
	float contador = 0;
	public float tempoGerar = 2;
	public LayerMask LayerZumbi;
	  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		contador += Time.deltaTime;

		if (contador >= tempoGerar)
		{
			contador = 0;
			GerarNovoZumbi();	
			 
		}
	}

	private Vector3 GerarPosicaoAleatoria()
	{
		Vector3 posicao = Random.insideUnitSphere * (Random.value * 2 + 3);
		posicao += transform.position;
		posicao.y = transform.position.y;
		return posicao;
	}

	private void GerarNovoZumbi()
	{
		Vector3 posicaoGeracao = GerarPosicaoAleatoria();
		Collider[] colisores = Physics.OverlapSphere(posicaoGeracao,1, LayerZumbi);

		if (colisores.Length > 0)
		{
			GerarNovoZumbi();
		}
		else
		{
			Instantiate(modeloZumbi, posicaoGeracao, transform.rotation);
		}
		
	}

}
