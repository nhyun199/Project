using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BlinkButton_GoTitle : MonoBehaviour
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
            flashingText.text = "타 이 틀 로";            
            yield return new WaitForSeconds(.5f);
            flashingText.text = "";            
            yield return new WaitForSeconds(.5f);
        }
    }
}