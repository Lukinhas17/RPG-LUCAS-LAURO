using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFeedBack : MonoBehaviour
{

    public Text Mensagem;

    private string mensagem;

    public string Mensagens//MENSAGEM FAZ UM GET SET PARA TODA HR ELE RECEBER O VALOR QUE ONTEXT RETORNAR
    {
        get
        {
            return mensagem;//RETORNA MENSAGEM
        }

        set
        {
            mensagem = value;//MENSAGEM IGUAL AO VALOR
            OnText();//CHAMA A FUNÇÃO QUE DEIXA A MENSAGEM.TEXT IGUAL A MENSAGEM NA CLASSE QUE ESTÁ SENDO MODIFICADA
        }
    }
    void OnText()
    {
        Mensagem.text = mensagem;
    }
}
