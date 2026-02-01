using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneScript : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("GO");
        if (collision.gameObject.tag.ToString() == "Player")
        {
            Debug.Log("LOAD");
            SceneManager.LoadScene(sceneName);
        }
    }
}
