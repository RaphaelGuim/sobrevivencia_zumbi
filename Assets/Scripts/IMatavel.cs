 using UnityEngine;

public interface IMatavel  {

	void TomarDano(int dano);
	void Morrer();
	void Sangrar(Vector3 position, Quaternion rotation);
}
