using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public CutSceneManagerScript cutSceneScript;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    IEnumerator TypeLine(string line, float textSpeed)
    {
        textComponent.text = string.Empty;

        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void ClearText()
    {
        textComponent.text = string.Empty;
    }

    public void StopText()
    {
        StopAllCoroutines();
        textComponent.text = text;
    }
}
