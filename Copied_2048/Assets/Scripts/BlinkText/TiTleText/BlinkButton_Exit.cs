using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BlinkButton_Exit : MonoBehaviour
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
            flashingText.text = "게 임 종 료";            
            yield return new WaitForSeconds(.7f);
            flashingText.text = "";            
            yield return new WaitForSeconds(.7f);
        }
    }
}