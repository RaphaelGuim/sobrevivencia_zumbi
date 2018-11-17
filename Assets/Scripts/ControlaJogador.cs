using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{

	Vector3 direcao;

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

	void Start()
	{

		status = GetComponent<Status>();


		Time.timeScale = 1;

		movimentarPersonagem = GetComponent<MovimentoPersonagem>();
		animacaoPersonagem = GetComponent<AnimacaoPersonagem>();



	}
	// Update is called once per frame


	void Update()
	{

		Mover();


	}

	private void FixedUpdate()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;

		movimentarPersonagem.Movimentar(direcao, status.Velocidade);

		movimentarPersonagem.RotacaoJogador();


	}


	public void Mover()
	{
		float eixoX = Input.GetAxis(Tags.Horizontal);
		float eixoY = Input.GetAxis(Tags.Vertical);
		direcao = new Vector3(eixoX, 0, eixoY);

		animacaoPersonagem.Mover(direcao.magnitude);
	}

	public void TomarDano(int dano)
	{
		ControlaAudio.instancia.PlayOneShot(somDano);

		status.Vida -= dano;
		controlaInterface.AtualizaVida();

		if (status.Vida <= 0)
		{

			Morrer();
		}
	}

	public void Morrer()
	{

		controlaInterface.GameOver();
	}

	public void CurarVida(int quantidade)
	{

		status.Vida += quantidade;
		controlaInterface.AtualizaVida();

		if (status.Vida > status.VidaInicial)
		{
			status.Vida = status.VidaInicial;
		}

	}
}
