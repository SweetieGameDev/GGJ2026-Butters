using UnityEngine;

public class SpawnShelfScript : MonoBehaviour
{
    public GameObject shelfPrefab;
    private bool shelfSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.ToString() == "Player" && !shelfSpawned)
        {
            shelfSpawned = true;
            Instantiate(shelfPrefab);
            Destroy(this.gameObject);
        }
    }
}
