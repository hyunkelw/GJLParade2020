using UnityEngine;
using redd096;

[AddComponentMenu("redd096/Singletons/Game Manager")]
public class GameManager : Singleton<GameManager>
{
    public UIManager uiManager { get; private set; }
    public Player player { get; private set; }
    public LevelManager levelManager { get; private set; }
    public FallManager fallManager { get; private set; }

    protected override void SetDefaults()
    {
        //get references
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        fallManager = FindObjectOfType<FallManager>();
            
        //if there is a player, lock mouse
        if (player)
        {
            FindObjectOfType<SceneLoader>().ResumeGame();
        }
    }

    void Update()
    {
        //if press escape or start, pause or resume game
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (Time.timeScale <= 0)
                SceneLoader.instance.ResumeGame();
            else
                SceneLoader.instance.PauseGame();
        }
    }
}
