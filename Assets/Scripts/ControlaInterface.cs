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
	// Use this for initialization
	void Start () {

		tempoMaximo = PlayerPrefs.GetFloat("tempoMaximo");
		status = GameObject.FindWithTag(Tags.Jogador).GetComponent<Status>();
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
		TextoSobrevivencia.text = "Você sobreviveu " + minutos + " minutos " + +segundos + " segundos";
		AjustarMaximo(minutos, segundos);
		
		
		PainelGameOver.SetActive(true);
	}
	public void Reiniciar()
	{
		SceneManager.LoadScene("game");
		
	}

	void AjustarMaximo(int minutos, int segundos)
	{
		if(Time.timeSinceLevelLoad > tempoMaximo)
		{
			tempoMaximo = Time.timeSinceLevelLoad;			 
			
			PlayerPrefs.SetFloat("tempoMaximo", tempoMaximo);
			TextoSobrevivenciaMaximo.text = string.Format("Se melhor tempo é {0} minutos {1} segundos", minutos, segundos);
		}
		else{
			 minutos = (int)( tempoMaximo / 60);
			 segundos = (int)(tempoMaximo % 60);
			TextoSobrevivenciaMaximo.text = string.Format("Se melhor tempo é {0} minutos {1} segundos", minutos, segundos);
		}
		
	}
}
