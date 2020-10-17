namespace redd096
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("redd096/MonoBehaviours/UI Manager")]
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu = default;
        [SerializeField] GameObject endMenu = default;
        [SerializeField] Text scoreText = default;
        [SerializeField] string scoreString = "Score: ";

        void Awake()
        {
            //remove end menu on awake
            EndMenu(false);
        }

        public void PauseMenu(bool active)
        {
            pauseMenu.SetActive(active);
        }

        public void EndMenu(bool active)
        {
            //be sure to not have pause menu when active end menu
            if (active)
                PauseMenu(false);

            endMenu.SetActive(active);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = scoreString + score.ToString();
        }
    }
}