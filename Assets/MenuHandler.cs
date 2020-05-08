using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject page1;
	[SerializeField]
	private GameObject page2;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void StartPress()
	{
		SceneManager.LoadScene(1);
	}

	public void LevelSelectPress()
	{
		page1.SetActive(false);
		page2.SetActive(true);
	}

	public void BackButtonPress()
	{
		page1.SetActive(true);
		page2.SetActive(false);
	}

	public void LevelPress(int levelNumb)
	{
		SceneManager.LoadScene(levelNumb);
	}
}
