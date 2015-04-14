using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
	public ParticleSystem muzzleFlash;
	public GameObject impactPrefab;
	public AudioSource gunSound;
	public AudioSource enemyDeath;
	public GUIText killCounter;
	public GUIText startText;
	public GUIText winText;

	GameObject[] impacts;
	int currentImpact = 0;
	int maxImpacts = 5;
	int enemiesKilled = 0;

	//bool winCondition = false;
	bool shooting = false;

	private void enemyKilled()
	{
		enemiesKilled++;
		killCounter.text = "Enemies killed: " + enemiesKilled.ToString() + " / 50";

		if (enemiesKilled == 4)
		{
			startText.text = "";
		}

		if (enemiesKilled == 50)
		{
			winText.text = "Congratulations, you killed all the enemies!";
		}
	}

	// Use this for initialization
	void Start ()
	{
		killCounter.text = "Enemies killed: " + enemiesKilled.ToString() + " / 50";
		startText.text = "Kill all enemies!";
		impacts = new GameObject[maxImpacts];

		for (int i = 0; i < maxImpacts; i++)
		{
			impacts[i] = (GameObject)Instantiate(impactPrefab);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1"))
		{
			muzzleFlash.Play();
			gunSound.Play();
			shooting = true;
		}
	}

	void FixedUpdate()
	{
		if (shooting)
		{
			shooting = false;

			RaycastHit hit;

			if (Physics.Raycast(transform.position, transform.forward, out hit, 500f))
			{
				if (hit.transform.tag == "Enemy")
				{
					enemyDeath.Play();
					//hit.transform.gameObject.SetActive(false);
					Destroy (hit.transform.gameObject);
					enemyKilled();
				}

				impacts[currentImpact].transform.position = hit.point;
				impacts[currentImpact].GetComponent<ParticleSystem>().Play();

				if (++currentImpact >= maxImpacts)
				{
					currentImpact = 0;
				}
			}
		}
	}
}