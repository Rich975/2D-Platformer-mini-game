using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coins;
    public TextMeshProUGUI levelCompleteText;
    public GameObject levelCompleteGO;

    public int score;

    public static UIManager Instance;
    [SerializeField] private Animator anim;

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