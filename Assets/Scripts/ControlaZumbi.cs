using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaZumbi : MonoBehaviour, IMatavel {
	private Rigidbody rb;
	private GameObject jogador;	 
	 
	 
	private ControlaJogador controlaJogador;
	private MovimentoPersonagem movimentoPersonagem;
	private AnimacaoPersonagem animacaoPersonagem;
	public Status status;
	public AudioClip morteZumbi;

	private Vector3 posicaoAleatoria;
	private Vector3 direcao;
	private float contadorVagar=0;
	private float tempoPosicao = 4;

	// Use this for initialization
	void Start () {

		jogador = GameObject.FindWithTag(Tags.Jogador);
		
		rb = GetComponent<Rigidbody>();
		 
		 
		controlaJogador = jogador.GetComponent<ControlaJogador>();
		movimentoPersonagem = GetComponent<MovimentoPersonagem>();
		animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
		AleatorizarZumbi();
		status = GetComponent<Status>();
	 


	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt(jogador.transform.position);
	}
	void FixedUpdate()
	{
		float distancia = Vector3.Distance(rb.position, jogador.transform.position);
		 

		movimentoPersonagem.Rotacionar(direcao);
		animacaoPersonagem.Mover(direcao.magnitude);


		if (distancia > 15)
		{
			Vagar();
		}		 
		else if (distancia > 2.5){
			direcao = jogador.transform.position - transform.position;
			movimentoPersonagem.Movimentar(direcao.normalized, status.Velocidade);			
			animacaoPersonagem.Atacar(false);

		}
		else
		{
			animacaoPersonagem.Atacar(true);
		}


		
	}
	Vector3 GerarPosicaoAleatoria()
	{
		Vector3 posicao = Random.insideUnitSphere * 10;
		posicao += transform.position;
		posicao.y = transform.position.y;
		return posicao;
	}
	void Vagar()

	{
		contadorVagar -= Time.deltaTime;
		if(contadorVagar <= 0)
		{
			posicaoAleatoria = GerarPosicaoAleatoria();
			contadorVagar = tempoPosicao;
		}
		
		 
		direcao = posicaoAleatoria - transform.position;

		if (direcao.magnitude > 0.1f)
		{
			movimentoPersonagem.Movimentar(direcao , status.Velocidade);
			movimentoPersonagem.Rotacionar(direcao.normalized);
		}
		
	}
	void AtacaJogador()
	{

		controlaJogador.TomarDano(Random.Range(20,30));		


	}

	void AleatorizarZumbi()
	{
		int tipoZumbi = Random.Range(1, 28);
		transform.GetChild(tipoZumbi).gameObject.SetActive(true);
	}

	public void TomarDano(int dano)
	{
		status.Vida -= dano;

		if (status.Vida <= 0)
		{
			Morrer();
		}
	}

	public void Morrer()
	{
		ControlaAudio.instancia.PlayOneShot(morteZumbi);
		Destroy(gameObject);
	}
}
