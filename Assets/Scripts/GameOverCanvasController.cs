using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverCanvasController : MonoBehaviour {
	public Text TextObject;
	public Button BtnRetry;

	public void DisplayResult()
	{
		TextObject.enabled = true;
		BtnRetry.enabled = true;
		StringBuilder sb = new StringBuilder();
		sb.Append("GAME OVER \r\n");
		sb.Append("FNAL SCORE: ");
		if (Game.DifficultyMultiplier == PlayerDifficultyMultiplier.Easy)
		{
			sb.Append(Movement.Player.transform.position.y);
		}
		else if (Game.DifficultyMultiplier == PlayerDifficultyMultiplier.Medium)
		{
			sb.Append(Movement.Player.transform.position.y * 1.5f);
		}
		else if (Game.DifficultyMultiplier == PlayerDifficultyMultiplier.Hard)
		{
			sb.Append(Movement.Player.transform.position.y * 2f);
		}

		TextObject.text = sb.ToString();
		//TextObject.text
	}

	public void Reset()
	{
		SceneManager.LoadScene("Main");
	}
}
