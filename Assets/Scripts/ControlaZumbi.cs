using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour {
	private Rigidbody rb;
	private GameObject jogador;	 
	 
	 
	private ControlaJogador controlaJogador;
	private MovimentoPersonagem movimentoPersonagem;
	private AnimacaoPersonagem animacaoPersonagem;
	public Status status;


	// Use this for initialization
	void Start () {

		jogador = GameObject.FindWithTag("Player");
		
		rb = GetComponent<Rigidbody>();
		 
		 
		controlaJogador = jogador.GetComponent<ControlaJogador>();
		movimentoPersonagem = GetComponent<MovimentoPersonagem>();
		animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
		AleatorizarZumbi();
		status = GetComponent<Status>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(jogador.transform.position);
	}
	void FixedUpdate()
	{
		float distancia = Vector3.Distance(rb.position, jogador.transform.position);
		Vector3 direcao = (jogador.transform.position - rb.position).normalized;		 
		

		 
		 
		if (distancia > 2.5){

			movimentoPersonagem.Movimentar(direcao, status.Velocidade);
			animacaoPersonagem.Atacar(false);

		}
		else
		{
			animacaoPersonagem.Atacar(true);
		}


		
	}

	void AtacaJogador()
	{

		controlaJogador.Dano(Random.Range(20,30));		


	}

	void AleatorizarZumbi()
	{
		int tipoZumbi = Random.Range(1, 28);
		transform.GetChild(tipoZumbi).gameObject.SetActive(true);
	}
}
