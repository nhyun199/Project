using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BlinkGameOver : MonoBehaviour
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
            flashingText.text = "GAME OVER";            
            yield return new WaitForSeconds(.5f);
            flashingText.text = "";            
            yield return new WaitForSeconds(.5f);
        }
    }
}