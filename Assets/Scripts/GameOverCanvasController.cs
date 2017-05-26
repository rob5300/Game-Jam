using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverCanvasController : MonoBehaviour {
	public Text TextObject;
	public Button BtnRetry;
    public static GameOverCanvasController Controller;

    private void Start()
    {
        if (!Controller) Controller = this;
    }

    public void DisplayResult()
	{
		TextObject.gameObject.SetActive(true);
		BtnRetry.gameObject.SetActive(true);
        StringBuilder sb = new StringBuilder();
		sb.Append("GAME OVER \r\n");
		sb.Append("FINAL SCORE: ");
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

	public void ResetScene()
	{
		SceneManager.LoadScene("Main");
	}
}