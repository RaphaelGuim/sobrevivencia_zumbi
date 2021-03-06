﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbi : MonoBehaviour {

	public GameObject modeloZumbi;
	float contador = 0;
	float tempoBalas;
	float contadorBalas;
	public float tempoGerar = 2;
	public LayerMask LayerZumbi;	
	private int quantidadeMaximaZumbis = 2;	
	private int quantidadeZumbisVivos = 0;
	private float tempoAumentoDificuldade = 30;
	private float contadorTempoDificuldade = 0;
	private GameObject jogador;
	public GameObject Municao;
	

	// Use this for initialization
	void Start () {

		jogador = GameObject.FindWithTag(Tags.Jogador);
		tempoBalas = 20;

	}
	
	// Update is called once per frame
	void Update () {

		contador += Time.deltaTime;
		contadorTempoDificuldade += Time.deltaTime;

		contadorBalas += Time.deltaTime;

		if(contadorBalas > tempoBalas)
		{
			Vector3 posicao = GerarPosicaoAleatoria();
			if (Vector3.Distance(posicao, jogador.transform.position) > 30)
			{
				GameObject municao = Instantiate(Municao, posicao, transform.rotation);
				contadorBalas = 0;
				Destroy(municao, 60);
			}
			
		}

		if (contador >= tempoGerar)
		{
			contador = 0;
			GerarNovoZumbi();	
			 
		}
		if(contadorTempoDificuldade>= tempoAumentoDificuldade)
		{
			contadorTempoDificuldade = 0;
			quantidadeMaximaZumbis += 3;
		}

		 

	}

	private Vector3 GerarPosicaoAleatoria()
	{
		Vector3 posicao = Random.insideUnitSphere * (Random.value * 5 + 3);
		posicao += transform.position;
		posicao.y = transform.position.y;
		return posicao;
	}

	private void GerarNovoZumbi()
	{
		
		Vector3 posicaoGeracao = GerarPosicaoAleatoria();
		if (Vector3.Distance(posicaoGeracao, jogador.transform.position) > 30)
		{

			Collider[] colisores = Physics.OverlapSphere(posicaoGeracao, 1, LayerZumbi);

			bool existeEspaco = colisores.Length > 0;
			bool quantidadeValida = quantidadeZumbisVivos < quantidadeMaximaZumbis;
			if (quantidadeValida)
			{
				if (existeEspaco)
				{
					GerarNovoZumbi();
				}
				else
				{
					quantidadeZumbisVivos++;
					ControlaZumbi controlaZumbi = Instantiate(modeloZumbi, posicaoGeracao, transform.rotation).GetComponent<ControlaZumbi>();
					controlaZumbi.gerador = this;
				}
			}
		}
		

	}

	public void DiminueZumbisVivos()
	{
		 quantidadeZumbisVivos--;
	}

}
