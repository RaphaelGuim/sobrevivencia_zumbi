using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour {
	private Rigidbody rb;
	private GameObject jogador;
	public float Velocidade = 5;
	private Rigidbody rbJogador;
	private Animator animator;
	private ControlaJogador controlaJogador;
	

	// Use this for initialization
	void Start () {

		jogador = GameObject.FindWithTag("Player");
		int tipoZumbi = Random.Range(1, 28);
		transform.GetChild(tipoZumbi).gameObject.SetActive(true);
		rb = GetComponent<Rigidbody>();
		rbJogador = jogador.GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		controlaJogador = rbJogador.GetComponent<ControlaJogador>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(jogador.transform.position);
	}
	void FixedUpdate()
	{
		float distancia = Vector3.Distance(rb.position, rbJogador.position);
		Vector3 direcao = (rbJogador.position - rb.position).normalized;		 
		

		 
		 
		if (distancia > 2.5){
			
			rb.MovePosition(rb.position + direcao * Time.deltaTime * Velocidade);
			animator.SetBool("Atacando", false);

		}
		else
		{
			animator.SetBool("Atacando", true);
		}


		
	}

	void AtacaJogador()
	{

		controlaJogador.Dano(Random.Range(20,30));
		


	}
}
