using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BlinkButton_Score : MonoBehaviour
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
            flashingText.text = "최 고 기 록";            
            yield return new WaitForSeconds(.7f);
            flashingText.text = "";            
            yield return new WaitForSeconds(.7f);
        }
    }
}