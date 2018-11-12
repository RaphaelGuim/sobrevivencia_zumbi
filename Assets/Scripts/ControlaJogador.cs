﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour {
		 
	Vector3 direcao;
	
	public GameObject textoGameOver;	 

	Status status;

	public ControlaInterface controlaInterface;
	public AudioClip somDano;
	private MovimentoPersonagem movimentarPersonagem;
	private AnimacaoPersonagem animacaoPersonagem;

	public bool Vivo
	{
		get
		{
			if (status.Vida <= 0)
			{
				return false;
			}
			return true;
		} 
	}

	void Start(){

		status = GetComponent<Status>();


		Time.timeScale = 1;
		 
		movimentarPersonagem = GetComponent<MovimentoPersonagem>();
		animacaoPersonagem = GetComponent<AnimacaoPersonagem>();



	}
	// Update is called once per frame

	
	void Update () {

		Mover();		
		 
		if (!Vivo  && Input.GetButtonDown("Fire1"))
		{
			SceneManager.LoadScene("Game");
			
		}
	}

	private void FixedUpdate()
	{

		movimentarPersonagem.Movimentar(direcao, status.Velocidade);

		movimentarPersonagem.RotacaoJogador();


	}

	public void Dano(int dano)
	{
		ControlaAudio.instancia.PlayOneShot(somDano);

		status.Vida -= dano;
		controlaInterface.AtualizaVida();
		if (status.Vida <= 0)
		{
			Time.timeScale = 0;
			textoGameOver.SetActive(true);
			 
		}
	}
	public void Mover()
	{
		float eixoX = Input.GetAxis("Horizontal");
		float eixoY = Input.GetAxis("Vertical");
		direcao = new Vector3(eixoX, 0, eixoY);

		animacaoPersonagem.Mover(direcao.magnitude);
	}

	



}
