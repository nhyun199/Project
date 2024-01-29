using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BlinkTitle_0 : MonoBehaviour
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
            flashingText.text = "0";
            yield return new WaitForSeconds(0.7f);
            flashingText.text = "";
            yield return new WaitForSeconds(0.7f);
        }
    }
}
