using UnityEngine;

public class SpawnShelfScript : MonoBehaviour
{
    public GameObject shelfPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToString() == "Player")
        {
            Instantiate(shelfPrefab);
        }
    }
}
