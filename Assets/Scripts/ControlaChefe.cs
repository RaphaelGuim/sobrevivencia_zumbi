using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel {

	private Transform jogador;
	private NavMeshAgent agente;
	private Status status;
	private AnimacaoPersonagem animacao;
	private MovimentoPersonagem movimento;
	private ControlaJogador controlaJogador;
	public AudioClip morteZumbi;
	public GameObject kitMedico;
	public Slider slider;
	public Image ImageSlider;
	public Color VidaMaxima, VidaMinima;

	private float CalculaPorcentagemAtual()
	{
		return (float)status.Vida / status.VidaInicial;
	}

	void AtualizarSlider()
	{
		float atual = CalculaPorcentagemAtual();
		slider.value = atual;
		ImageSlider.color =Color.Lerp(VidaMinima, VidaMaxima,atual);
	}
	private void Start()
	{

		
		jogador = GameObject.FindWithTag(Tags.Jogador).transform;
		controlaJogador = jogador.GetComponent<ControlaJogador>();
		agente = GetComponent<NavMeshAgent>();
		status = GetComponent<Status>();
		animacao = GetComponent<AnimacaoPersonagem>();
		movimento = GetComponent<MovimentoPersonagem>();
		agente.speed = status.Velocidade;
		ImageSlider.color = Color.Lerp(VidaMinima, VidaMaxima, CalculaPorcentagemAtual());

	}

	private void FixedUpdate()
	{
		
		animacao.Mover(agente.velocity.magnitude);		 
		agente.SetDestination(jogador.position);
		if (agente.hasPath)
		{
			 
			bool perto = agente.remainingDistance < agente.stoppingDistance;

			if (perto)
			{
				animacao.Atacar(true);
				Vector3 direcao = jogador.position - transform.position;
				movimento.Rotacionar(direcao);
			}
			else
			{
				animacao.Atacar(false);
			}
		}
		
	}

	private void AtacaJogador()
	{

		 
		controlaJogador.TomarDano(Random.Range(30, 40));


	}

	public void TomarDano(int dano)
	{
		status.Vida -= dano;
		AtualizarSlider();

		if (status.Vida <= 0)
		{
			Morrer();
		}
	}

	public void Morrer()
	{
		Destroy(gameObject, 2);
		agente.enabled = false;
		this.enabled = false;
		GetComponent<Rigidbody>().isKinematic = false;
		animacao.Morrer();
		movimento.Morrer();
		ControlaAudio.instancia.PlayOneShot(morteZumbi);
		Instantiate(kitMedico, transform.position, Quaternion.identity);
	}
}
