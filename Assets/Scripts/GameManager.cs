using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public enum GAME_STATE { menu, play, gameOver };
    private GAME_STATE gameState = GAME_STATE.menu;

    [SerializeField] private PlayerManager player;
    [SerializeField] private BuildSpikesManager buildSpikesManager;
    [SerializeField] private GameObject menu;
    [SerializeField] private Text bestScore;
    [SerializeField] private Text gamesPlayed;
    private int bestScoreNb;
    private int gamesPlayedNb;
    [HideInInspector] public int level;

    private void Start()
    {
        bestScoreNb = PlayerPrefs.GetInt("bestScore", 0);
        bestScore.text = "Best Score : " + bestScoreNb.ToString();
        gamesPlayedNb = PlayerPrefs.GetInt("gamesPlayed", 0);
        gamesPlayed.text = "Games Played : " + gamesPlayedNb.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (gameState == GAME_STATE.menu) {
                gameState = GAME_STATE.play;
                level = 0;
                menu.SetActive(false);
                player.StartMoving();
            } else if (gameState == GAME_STATE.gameOver) {
                gameState = GAME_STATE.menu;
                menu.SetActive(true);
                buildSpikesManager.Init();
                player.Init();
            }
        }
    }

    public void GameOver()
    {
        gameState = GAME_STATE.gameOver;
        gamesPlayedNb++;
        PlayerPrefs.SetInt("gamesPlayed", gamesPlayedNb);
        gamesPlayed.text = "Games Played : " + gamesPlayedNb.ToString();
        if (level > bestScoreNb) {
            bestScoreNb = level;
            PlayerPrefs.SetInt("bestScore", bestScoreNb);
        }
    }
}
