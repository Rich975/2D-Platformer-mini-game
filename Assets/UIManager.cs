using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public TextMeshProUGUI levelCompleteText;
    public GameObject levelCompleteGO;

    public GameObject menuButtons;
    public GameObject gameInfo;

    public Button newGameButton;

    public int score;

    public static UIManager Instance;
    [SerializeField] private Animator anim;

    private bool isPaused = false;

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        score = 0;
        coins.text = 0.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    public void ShowMenuButtons()
    {
        menuButtons.SetActive(true);
    }

    public void HideMenuButtons()
    {
        menuButtons.SetActive(false);
    }

    private void PauseGame()
    {
        ShowMenuButtons();
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        HideMenuButtons();
        HideGameInfo();
        Time.timeScale = 1;
    }

    public void ShowGameInfo()
    {
        gameInfo.SetActive(true);
        HideMenuButtons();
    }

    public void HideGameInfo()
    {
        gameInfo.SetActive(false);
    }

    public void AddScore(int score)
    {
        this.score += score;
        coins.text = this.score.ToString();
        //change the scoretext on the UI
    }

    public void LevelComplete()
    {
        levelCompleteGO.SetActive(true);
        anim.SetTrigger("levelComplete");
    }
}