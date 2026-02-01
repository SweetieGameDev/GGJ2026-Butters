using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaskMeta : MonoBehaviour
{
    public int MasksCollected;

    private Animator maskAnimator;

    private AudioSource maskBreak;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MasksCollected = 0;

        maskAnimator = GetComponent<Animator>();

        maskBreak = GetComponent<AudioSource>();

        maskAnimator.SetBool("isCollected", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MasksCollected += 1;

        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        maskAnimator.SetBool("isCollected", true);

        //Play mask breaking sound effect
        maskBreak.Play();

        // Wait for one frame to ensure that the animation has started
        yield return null;

        // Get the length of the current animation, which will be "isDeath"
        float animationLength = maskAnimator.GetCurrentAnimatorStateInfo(0).length;

        // Wait for the duration of the enemy death animation
        yield return new WaitForSeconds(animationLength);

        //Animation is done destory self
        StopCoroutine(WaitForAnimation());
        Destroy(gameObject);
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
