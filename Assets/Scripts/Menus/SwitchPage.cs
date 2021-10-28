using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SwitchPage : MonoBehaviour
{
    public TMP_Text storyText;
    public TMP_Text htpText;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        if (storyText.gameObject.activeSelf)
        {
            storyText.gameObject.SetActive(false);
            htpText.gameObject.SetActive(true);
        }
        else if (htpText.gameObject.activeSelf)
        {
            storyText.gameObject.SetActive(true);
            htpText.gameObject.SetActive(false);
        }
    }
}
