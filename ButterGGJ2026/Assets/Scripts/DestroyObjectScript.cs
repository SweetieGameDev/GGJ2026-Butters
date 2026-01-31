using System.Collections;
using UnityEngine;

public class DestroyObjectScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.ToString() == "6")
        {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(this.gameObject);
    }
}
