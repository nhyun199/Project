using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlinkTitle_4 : MonoBehaviour
{
    Text flashingText;

    void Start()
    {
        flashingText = GetComponent<Text>();
        StartCoroutine(Blink());
    }
    public IEnumerator Blink()
    {
        while (true)
        {
            flashingText.text = "4";
            yield return new WaitForSeconds(0.7f);
            flashingText.text = "";
            yield return new WaitForSeconds(0.7f);
        }
    }
}
