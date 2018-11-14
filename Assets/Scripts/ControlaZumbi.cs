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

		tempoPosicao = Random.value * 6 + 4;




	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		rb.velocity = Vector3.zero;

		float distancia = Vector3.Distance(rb.position, jogador.transform.position);
		direcao = jogador.transform.position - transform.position;

		if (distancia > 15)
		{
			Vagar();
		}		 
		else if (distancia > 2.5){
			Mover();
			animacaoPersonagem.Atacar(false);
		}
		else
		{
			movimentoPersonagem.Rotacionar(direcao);
			animacaoPersonagem.Atacar(true);
		}


		
	}
	public Vector3 GerarPosicaoAleatoria()
	{
		Vector3 posicao = Random.insideUnitSphere * (Random.value * 8 + 2);
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
			Mover();
		}
		 
		 
		
	}
	void AtacaJogador()
	{
		direcao = Vector3.zero;
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

	private void Mover()
	{
		animacaoPersonagem.Mover(direcao.magnitude);
		movimentoPersonagem.Movimentar(direcao, status.Velocidade);
		movimentoPersonagem.Rotacionar(direcao.normalized);
		
	}
}
