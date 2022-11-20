using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.Networking;


public class TextHandler : MonoBehaviour
{
    public TextMeshProUGUI input_text;
    public TMP_InputField input_field;


    public void inputHandler()
    {
        ResultData result = APIHandler.getResult(input_field.text);
        if (result.prediction == "Not Spam")
        {
            input_text.text = input_field.text;
        }
        else
        {
            input_text.text = result.prediction;
        }
    }
}
