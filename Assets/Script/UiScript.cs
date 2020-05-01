using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public InputField nomeInput;//recebe o nome digitado pelo player
    private bool escolher = false;//escolha da class do player
    public ClassManager classeManager;//REFERENCIA PARA CLASSE MANAGER
    public static int playerClass;//CLASSE ESCOLHIDA

    public void BottonClass(int op)
    {
       
        if (op == 1)
        {
            classeManager.CriarClasse(classeManager.ninja);
            playerClass = op;
        }
        else if (op == 2)
        {
            classeManager.CriarClasse(classeManager.mago);
            playerClass = op;
        }
        else
        {
            classeManager.CriarClasse(classeManager.berserker);
            playerClass = op;
        }

        escolher = true;
    }//RECEBE A CLASSE ESCOLHIDA 
    public void BottonContinuar(string scene)
    {
        if (nomeInput.textComponent.text.Length != 0 && escolher == true)//SE O NOME FOR DIGITADO E A ESCOLHA DE CLASS FOR IGUAL A TRUE 
        {
            PlayerScript.singleton.nomePlayer = nomeInput.textComponent.text;//ARMAZENA O NOME DO PLAYER 
            SceneScript.singleton.LoadScene(scene);//PASSA DE CENA
        }
        else
        {
            return;
        }
    }//PASSA PARA AS CENAS 


}