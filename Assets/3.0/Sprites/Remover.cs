using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject Splash;


	void OnTriggerEnter2D(Collider2D col)
	{

		if (col.gameObject.tag == "Player")
		{

			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CrmeraFollow>().enabled = false;

			//防止英雄销毁后再去获取位置出现NullReferenceException
			if (GameObject.Find("HealthBar").activeSelf)
			{
				GameObject.Find("HealthBar").SetActive(false);
			}


			Instantiate(Splash, col.transform.position, transform.rotation);

			Destroy(col.gameObject);


			StartCoroutine("ReloadGame");
		}
		else
		{

			Instantiate(Splash, col.transform.position, transform.rotation);


			Destroy(col.gameObject);
		}
	}

	IEnumerator ReloadGame()
	{

		yield return new WaitForSeconds(2);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}
