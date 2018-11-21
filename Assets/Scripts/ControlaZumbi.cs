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
	private float contadorVagar = 0;
	private float tempoPosicao = 4;
	private float porcentagemGerarKitMedico = 0.1f;
	public GameObject kitMedico;
	private ControlaInterface controlaInterface;
	public GeradorZumbi gerador;
	public GameObject ParticulaSangue;
	

	// Use this for initialization
	void Start() {

		jogador = GameObject.FindWithTag(Tags.Jogador);

		rb = GetComponent<Rigidbody>();


		controlaJogador = jogador.GetComponent<ControlaJogador>();
		movimentoPersonagem = GetComponent<MovimentoPersonagem>();
		animacaoPersonagem = GetComponent<AnimacaoPersonagem>();
		AleatorizarZumbi();
		status = GetComponent<Status>();

		tempoPosicao = Random.value * 6 + 4;

		controlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;


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
		else if (distancia > 2.5) {
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
		if (contadorVagar <= 0)
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
	private void AtacaJogador()
	{
		 
		direcao = Vector3.zero;
		controlaJogador.TomarDano(Random.Range(20, 30));


	}

	void AleatorizarZumbi()
	{
		int tipoZumbi = Random.Range(1, transform.childCount);
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
	public void Sangrar(Vector3 position, Quaternion rotation)
	{
		Instantiate(ParticulaSangue,  position,  rotation);

		 
	}

	public void Morrer()
	{
		Destroy(gameObject,2);
		ControlaAudio.instancia.PlayOneShot(morteZumbi);
		this.enabled = false;
		VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
		controlaInterface.AtualizarQuantidadeZumbisMortos();
		gerador.DiminueZumbisVivos();
		animacaoPersonagem.Morrer();
		movimentoPersonagem.Morrer();


	}

	 

	private void Mover()
	{
		animacaoPersonagem.Mover(direcao.magnitude);
		movimentoPersonagem.Movimentar(direcao, status.Velocidade);
		movimentoPersonagem.Rotacionar(direcao.normalized);
		
	}

	private void VerificarGeracaoKitMedico(float porcentagem )
	{
		
		if (Random.value <= porcentagem)			
		{
			Instantiate(kitMedico, transform.position, Quaternion.identity);
		}
	}
}
