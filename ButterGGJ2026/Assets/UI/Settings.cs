using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    public GameObject mediabackground;

    private void Start()
    {

    }

    private void inputcheck()
    {
        
    }

    public void playbutton()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void leveltranstion1()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
    }

    public void leveltranstion2()
    {
        SceneManager.LoadScene("Level3", LoadSceneMode.Additive);
    }
}
