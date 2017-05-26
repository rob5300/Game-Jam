using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public GameObject MainContainer;
	public Canvas DifficultyCanvas;

	public void SettingsMenu()
	{
        DifficultyCanvas.gameObject.SetActive(true);
		MainContainer.SetActive(false);
	}

	public void MainMenu()
	{
		DifficultyCanvas.gameObject.SetActive(false);
		MainContainer.SetActive(true);
	}

	public void SelectDifficulty(string difficulty)
	{
		if (difficulty == "Easy")
		{
			Game.DifficultyMultiplier = PlayerDifficultyMultiplier.Easy;
		}
		else if (difficulty == "Medium")
		{
			Game.DifficultyMultiplier = PlayerDifficultyMultiplier.Medium;
		}
		else if (difficulty == "Hard")
		{
			Game.DifficultyMultiplier = PlayerDifficultyMultiplier.Hard;
		}
	}
}
