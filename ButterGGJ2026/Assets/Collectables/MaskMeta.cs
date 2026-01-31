using UnityEngine;
using UnityEngine.SceneManagement;

public class MaskMeta : MonoBehaviour
{
    public int MasksCollected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MasksCollected = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MasksCollected += 1;
    }

    public void TriggerEnding()
    {
        if (MasksCollected > 2)
        {
            SceneManager.LoadScene("GoodEnd", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("BadEnd", LoadSceneMode.Additive);
        }
    }
}
