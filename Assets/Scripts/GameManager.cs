using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	//Ska igentligen använda sig av en metod
	/// <summary>
	/// public static GameManager main;
	/// 
	/// ska vara
	/// 
	/// [SerializeField]
	/// private static GameManager main;
	/// public static GameManager Main { get => main; set => main = value; };
	/// </summary>
	public static GameManager main;
	float finalTime = 0;
	Timer timer = new Timer();
	bool runActive = false;

	int deathCount = 0;

	[SerializeField]
	private GameObject endUI;
	[SerializeField]
	private TMPro.TextMeshProUGUI endTimeText;

	[SerializeField]
	private GameObject inGameUI;
	[SerializeField]
	private TMPro.TextMeshProUGUI currentTimeText;

	// Start is called before the first frame update
	void Start()
	{
		main = this;
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update()
	{
		timer.Update(Time.deltaTime);
		currentTimeText.SetText(Mathf.Round(timer.Time*10)/10 + "");
		if (Input.anyKeyDown && !runActive)
		{
			StartRun();
			runActive = true;
		}
	}

	void StartRun()
	{
		timer.Start();
	}

	public void EnteredGoal()
	{
		if (runActive)
		{
			FinishRun();
		}
	}

	void FinishRun()
	{
		finalTime = timer.End();
		Time.timeScale = 0;

		inGameUI.SetActive(false);

		endUI.SetActive(true);
		endTimeText.SetText(finalTime + "");
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void NextLevel()
	{
		Debug.Log(SceneManager.GetActiveScene().buildIndex);
		Debug.Log(SceneManager.sceneCountInBuildSettings);
		if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else
		{
			SceneManager.LoadScene(0);
		}
	}

	public void GoToMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void Death()
	{
		deathCount++;
	}
}
