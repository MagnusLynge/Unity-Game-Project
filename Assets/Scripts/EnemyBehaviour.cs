using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
	public AudioSource EnemyDeath;
	
	// Update is called once per frame
	void Update ()
	{
		if (this.isActiveAndEnabled == false)
		{
			EnemyDeath.Play();
		}

	}
}