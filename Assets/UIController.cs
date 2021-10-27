using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PumpkinCounterScript pumpkinCounter;

    public TMP_Text guideCounterText;
    public TMP_Text explodeCounterText;
    public TMP_Text timerCounterText;

    public Slider completedCounterSlider;
    public Image sliderFill;

    public Color colourGreen;
    public Color colourYellow;
    public Color colourRed;

    // Start is called before the first frame update
    void Start()
    {
        completedCounterSlider.maxValue = 5;
    }

    // Update is called once per frame
    void Update()
    {
        guideCounterText.text = System.Convert.ToString(pumpkinCounter.maxGuideAbility);
        explodeCounterText.text = System.Convert.ToString(pumpkinCounter.maxExplodeAbility);
        timerCounterText.text = System.Convert.ToString(pumpkinCounter.currentTime);
        SetSlider();
    }


    void SetSlider()
    {
        if (pumpkinCounter.currentTime > 30)
        {
            timerCounterText.color = colourGreen;
        }
        else if (pumpkinCounter.currentTime > 10)
        {
            timerCounterText.color = colourYellow;
        }
        else if (pumpkinCounter.currentTime > 0)
        {
            timerCounterText.color = colourRed;
        }

        if(pumpkinCounter.currentCompleted > 3)
        {
            sliderFill.color = colourGreen;
        }
        else if (pumpkinCounter.currentCompleted > 2)
        {
            sliderFill.color = colourYellow;
        }
        else if (pumpkinCounter.currentCompleted > 0)
        {
            sliderFill.color = colourRed;
        }


        completedCounterSlider.value = pumpkinCounter.currentCompleted;
    }
}
