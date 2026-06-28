using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance { get; private set; }

    [Header("Ending Scene Names")]
    [SerializeField] private string badEndingScene = "BadEnding";
    [SerializeField] private string neutralEndingScene = "NeutralEnding";
    [SerializeField] private string goodEndingScene = "GoodEnding";

    public bool MainTaskChainComplete { get; private set; }
    public int TasksCompleted { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeExpired += DecideEndingWhenTimeRunsOut;
        }
    }

    public void RegisterTaskCompleted(bool isMainTask)
    {
        TasksCompleted++;

        if (isMainTask)
        {
            MainTaskChainComplete = true;
            LoadGoodEnding();
        }
    }

    private void DecideEndingWhenTimeRunsOut()
    {
        if (MainTaskChainComplete)
        {
            LoadGoodEnding();
        }
        else if (TasksCompleted >= 2)
        {
            SceneManager.LoadScene(neutralEndingScene);
        }
        else
        {
            SceneManager.LoadScene(badEndingScene);
        }
    }

    private void LoadGoodEnding()
    {
        SceneManager.LoadScene(goodEndingScene);
    }
}