using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {
	private Status status;
	public Slider SliderVidaJogador;
	public GameObject PainelGameOver;
	public Text TextoSobrevivencia;
	public Text TextoSobrevivenciaMaximo;
	private float tempoMaximo = 0;
	private int quantidadeZumbisMortos = 0;
	private int zumbisMortosMaximo = 0;
	public Text TextoZumbisMortos;
	public Text TextoZumbisAtual;
	public Text TextoZumbisMaximo;
	public Text TextoChefe;
	public Text TextoBalas;
	 

	// Use this for initialization
	void Start () {

		tempoMaximo = PlayerPrefs.GetFloat("tempoMaximo");
		zumbisMortosMaximo = PlayerPrefs.GetInt(Tags.ZumbisMortosMaximo);
		status = GameObject.FindWithTag(Tags.Jogador).GetComponent<Status>();
		 
	}

	public void AtualizarQuantidadeZumbisMortos()
	{
		quantidadeZumbisMortos++;
		TextoZumbisMortos.text = " x " + quantidadeZumbisMortos;
	}
	public void AtualizarQuantidadeBalas(int balas)
	{
		TextoBalas.text = "Balas: " + balas;
	}
	 
	public void AtualizaVida()
	{
		SliderVidaJogador.value = (float)status.Vida / status.VidaInicial;
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		int minutos = (int)(Time.timeSinceLevelLoad / 60);
		int segundos =  (int)Time.timeSinceLevelLoad % 60;
		TextoSobrevivencia.text = string.Format("{0} min {1} seg", minutos, segundos);
		TextoZumbisAtual.text = quantidadeZumbisMortos.ToString();
		AjustarMaximo(minutos, segundos);
		AjustarMaximoZumbis();
		
		
		PainelGameOver.SetActive(true);
	}
	public void Reiniciar()
	{
		SceneManager.LoadScene("game");
		
	}

	void AjustarMaximoZumbis ()
	{
		if (quantidadeZumbisMortos >= zumbisMortosMaximo)
		{
			PlayerPrefs.SetInt(Tags.ZumbisMortosMaximo, quantidadeZumbisMortos);
			zumbisMortosMaximo = quantidadeZumbisMortos;
		}

		TextoZumbisMaximo.text = string.Format("{0}", zumbisMortosMaximo);


	}

	void AjustarMaximo(int minutos, int segundos)
	{
			


		if (Time.timeSinceLevelLoad > tempoMaximo)
		{
			tempoMaximo = Time.timeSinceLevelLoad;			 
			
			PlayerPrefs.SetFloat("tempoMaximo", tempoMaximo);
			TextoSobrevivenciaMaximo.text = string.Format("{0} min {1} seg", minutos, segundos);
		}
		else{
			 minutos = (int)( tempoMaximo / 60);
			 segundos = (int)(tempoMaximo % 60);
			TextoSobrevivenciaMaximo.text = string.Format("{0} min {1} seg", minutos, segundos);
		}

		

	}

	public void MostraAvisoChefe()
	{
		StartCoroutine(MostraAvisoChefe(2, TextoChefe));
	}

	IEnumerator MostraAvisoChefe(float tempoSumico, Text textoParaSumir)
	{
		textoParaSumir.gameObject.SetActive(true);
		Color corTexto = textoParaSumir.color;
		corTexto.a = 1;
		textoParaSumir.color = corTexto;

		float contator = 0;
		yield return new WaitForSeconds(tempoSumico);

		while (textoParaSumir.color.a > 0)
		{
			contator += Time.deltaTime / tempoSumico;
			corTexto.a = Mathf.Lerp(1,0,contator);
			textoParaSumir.color = corTexto;
			if (textoParaSumir.color.a < 0)
			{
				textoParaSumir.gameObject.SetActive(false);
			}
			yield return null;
		}
		
	}
}
