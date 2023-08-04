using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalScore;
        void Start()
        {
           LoadScore();
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void QuitGame()
        {
            Application.Quit();
            GameManager.instance.SaveScore();
        }
        public void LoadScore()
        {
            if (PlayerPrefs.HasKey("totalScore"))
            {
                totalScore.text = "" + PlayerPrefs.GetInt("totalScore");
            }
           
        }
    }
}
