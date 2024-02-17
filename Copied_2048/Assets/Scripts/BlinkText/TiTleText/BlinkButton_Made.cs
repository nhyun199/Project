using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BlinkButton_Made : MonoBehaviour
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
            flashingText.text = "Made by hyun 2023";            
            yield return new WaitForSeconds(.7f);
            flashingText.text = "";            
            yield return new WaitForSeconds(.7f);
        }
    }
}