namespace redd096
{
    using UnityEngine;
    using UnityEngine.UI;

    [AddComponentMenu("redd096/MonoBehaviours/UI Manager")]
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu = default;
        [SerializeField] Text scoreText = default;
        [SerializeField] string scoreString = "Score: ";

        public void PauseMenu(bool active)
        {
            pauseMenu.SetActive(active);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = scoreString + score.ToString();
        }
    }
}