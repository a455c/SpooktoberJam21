using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;

    public void LoadScene(int levelindex, string trigger)
    {
        StartCoroutine(LoadLevel());

        IEnumerator LoadLevel()
        {
            animator.SetTrigger(trigger);

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(levelindex);
        }
    }
}
