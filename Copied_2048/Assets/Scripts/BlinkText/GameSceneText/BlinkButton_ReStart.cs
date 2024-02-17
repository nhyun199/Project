using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BlinkButton_ReStart : MonoBehaviour
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
            flashingText.text = "다 시 시 작";            
            yield return new WaitForSeconds(.5f);
            flashingText.text = "";            
            yield return new WaitForSeconds(.5f);
        }
    }
}