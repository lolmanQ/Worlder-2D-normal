using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
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
	}

	// Update is called once per frame
	void Update()
	{
		timer.Update(Time.deltaTime);
		currentTimeText.SetText(timer.Time + "");
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

	}

	public void NextLevel()
	{

	}

	public void Death()
	{
		deathCount++;
	}
}
