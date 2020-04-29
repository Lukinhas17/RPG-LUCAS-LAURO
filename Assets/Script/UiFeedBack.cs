using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFeedBack : MonoBehaviour
{

    public Text ScoreText;

    private string mensagem;

    public string Mensagens
    {
        get
        {
            return mensagem;
        }

        set
        {
            mensagem = value;

            UpdatePointsText();
        }
    }
    void UpdatePointsText()
    {
        ScoreText.text = mensagem;
    }
}
