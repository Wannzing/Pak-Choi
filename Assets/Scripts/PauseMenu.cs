using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    public GameObject setting;

    private void Awake()
    {
        isPaused = false;
    }

    private void Update()
    {
        if (isPaused)
        {
            setting.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            setting.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Unpause()
    {
        isPaused = false;
    }
}
