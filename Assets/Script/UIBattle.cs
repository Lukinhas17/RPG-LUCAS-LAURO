using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattle : MonoBehaviour
{
    public Image vidaImage;
    public Text vidaText;
    public Text defesaText;
    public Text danoText;

    public Image vidaEnemyImage;
    public Text vidaEnemyText;
    public Text defesaEnemyText;
    public Text danoEnemyText;



    float vida = PlayerScript.singleton.classe.vida;
    float vidaCheia = PlayerScript.singleton.classe.vidaMax;
    float vidaE;
    float vidaCheiaE;
    float defesaE;

    private void Start()
    {
        AtualizarStatus();
    }

    public void AtualizarStatus()
    {
        vidaE = BattleClass.enemy.vida;
        vidaCheiaE = BattleClass.enemy.vidaMax;
        defesaE = BattleClass.enemy.defesa;



        vida = PlayerScript.singleton.classe.vida;
        vidaCheia = PlayerScript.singleton.classe.vidaMax;

        danoText.text = "dano : " + PlayerScript.singleton.classe.forca.ToString();
        defesaText.text = "defesa : " + PlayerScript.singleton.classe.defesa.ToString();

        if (PlayerScript.singleton.classe.vida > PlayerScript.singleton.classe.vidaMax)
        {
            PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vidaMax;

        }
        if (PlayerScript.singleton.classe.vida <= 0)
        {
            PlayerScript.singleton.classe.vida = 0;
        }

        vidaImage.rectTransform.sizeDelta = new Vector2(vida / vidaCheia * 159, 20);

        vidaText.text = PlayerScript.singleton.classe.vida.ToString() + "/" + PlayerScript.singleton.classe.vidaMax.ToString();      
        danoEnemyText.text = "dano : " + BattleClass.enemy.forca.ToString();
        defesaEnemyText.text = "defesa : " + BattleClass.enemy.defesa.ToString();
        vidaEnemyText.text = BattleClass.enemy.vida.ToString() + "/" + BattleClass.enemy.vidaMax.ToString();
        vidaEnemyImage.rectTransform.sizeDelta = new Vector2(vidaE / vidaCheiaE * 159, 20);
        vidaImage.rectTransform.sizeDelta = new Vector2(vida / vidaCheia * 159, 20);
    }

    public void RecomecarJogo(string cene)
    {
        SceneScript.singleton.LoadScene(cene);
    }

    public void EscolherBuff(int buff)
    {
        if (buff == 1)
        {
            PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vidaMax;
        }
        if (buff == 2)
        {
                PlayerScript.singleton.classe.defesa += 10;
            if (PlayerScript.singleton.classe.defesa >= PlayerScript.singleton.classe.defesaMax) 
            {
                PlayerScript.singleton.classe.defesa = PlayerScript.singleton.classe.defesaMax;
                BattleClass.Mensagem.Mensagens = "SUA DEFESA CHEGOU NA DEFESA MAXIMA";
            }
        }
        if (buff == 3)
        {
                PlayerScript.singleton.classe.forca += 10;
            if (PlayerScript.singleton.classe.forca >= PlayerScript.singleton.classe.forcaMax)
            {
                PlayerScript.singleton.classe.forca = PlayerScript.singleton.classe.forcaMax;
                BattleClass.Mensagem.Mensagens = "SUA DEFESA CHEGOU NA DEFESA MAXIMA";
            }
        }
    }


}
