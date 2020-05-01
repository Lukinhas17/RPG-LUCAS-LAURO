using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattle : MonoBehaviour
{
    //REFERENCIA DO UI IMAGENS E TEXTOS 
    public Image vidaImage;
    public Text vidaText;
    public Text nomeDoPlayer;
    public Text defesaText;
    public Text danoText;
    public Image vidaEnemyImage;
    public Text vidaEnemyText;
    public Text defesaEnemyText;
    public Text danoEnemyText;


    //VARIAVEIS 
    float vida = PlayerScript.singleton.classe.vida;//VIDA É A ATUAL VIDA NO UI
    float vidaCheia = PlayerScript.singleton.classe.vidaMax;//VIDA MAX É A ATUAL VIDA MAX NO UI
    float vidaE;//VIDA DO INIMIGO NO UI
    float vidaCheiaE;//VIDA MAX DO INIMIGO NO UI
    private void Start()
    {
        nomeDoPlayer.text = "Jogador : " + PlayerScript.singleton.nomePlayer;
        AtualizarStatus();//CHAMA A FUNÇÃO QUE MOSTRA OS STATUS
    }

    public void AtualizarStatus()
    {
        vidaE = BattleClass.Enemy.vida;//VIDA DO INIMIGO NO UI RECEBE VIDA DO INIMIGO
        vidaCheiaE = BattleClass.Enemy.vidaMax;//VIDA MAX DO ININIMIGO NO UI RECEBE VIDA MAX DO INIMIGO

        vida = PlayerScript.singleton.classe.vida;//VIDA NO UI RECEBE VIDA DO PLAYER
        vidaCheia = PlayerScript.singleton.classe.vidaMax;//VIDA MAX NO UI RECEBE VIDA MAX DO PLAYER

        danoText.text = "Dano : " + PlayerScript.singleton.classe.forca.ToString();//STRING QUE MOSTRA O DANO DO PLAYER
        defesaText.text = "Defesa : " + PlayerScript.singleton.classe.defesa.ToString();//STRING QUE MOSTRA A DEFESA DO PLAYER

        if (PlayerScript.singleton.classe.vida > PlayerScript.singleton.classe.vidaMax)//VERIFICA SE A VIDA DO PLAYER É MAIOR QUE A VIDA MAXIMA DO PLAYER
        {
            PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vidaMax;//DEIXA A VIDA DO UI IGUAL A VIDA MAXIMA DO UI

        }
        if (PlayerScript.singleton.classe.vida <= 0)//VERIFICA SE A VIDA DO PLAYER É MENOR OU IGUAL A 0
        {
            PlayerScript.singleton.classe.vida = 0;//DEIXA A VIDA IGUAL A 0
        }

        vidaImage.rectTransform.sizeDelta = new Vector2(vida / vidaCheia * 159, 20);//TAMANHO DO CAMPO DA IMAGEM


        //IMPLEMENTANDO TODOS OS VALORES NOS RESPECTIVOS CAMPOS DO UI
        vidaText.text = PlayerScript.singleton.classe.vida.ToString() + "/" + PlayerScript.singleton.classe.vidaMax.ToString();      
        danoEnemyText.text = "Dano : " + BattleClass.Enemy.forca.ToString();
        defesaEnemyText.text = "Defesa : " + BattleClass.Enemy.defesa.ToString();
        vidaEnemyText.text = BattleClass.Enemy.vida.ToString() + "/" + BattleClass.Enemy.vidaMax.ToString();
        vidaEnemyImage.rectTransform.sizeDelta = new Vector2(vidaE / vidaCheiaE * 159, 20);//TAMANHO DO CAMPO DA IMAGEM
        vidaImage.rectTransform.sizeDelta = new Vector2(vida / vidaCheia * 159, 20);//TAMANHO DO CAMPO DA IMAGEM
    }//ATUALIZA O STATUS DO INIMGO E DO PLAYER NO HUD
    public void RecomecarJogo(string cene)//RECOMEÇA O JOGO
    {
        SceneScript.singleton.LoadScene(cene);//CHAMA A PROXIMA CENA 
        PlayerScript.singleton.tentativas = 3; //SETA AS TENTATIVAS PARA 3 QUANDO O PLAYER COMEÇAR DE NOVO O JOGO ELAS VÃO ESTAR PRONTAS PARA SEREM USADAS NOVAMENTE

    }
    public void EscolherBuff(int buff)
    {
        if (buff == 1)//RECUPERA TODA VIDA
        {
            PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vidaMax;
        }
        if (buff == 2)//AUMENTA A DEFESA
        {
                PlayerScript.singleton.classe.defesa += 15;//AUMENTA 15 NA FORÇA
           
            if (PlayerScript.singleton.classe.defesa >= PlayerScript.singleton.classe.defesaMax) //SE A VIDA FOR MAIOR QUE A VIDA MAXIMA NO UI
            {
                
                PlayerScript.singleton.classe.defesa = PlayerScript.singleton.classe.defesaMax;//VIDA RECEBE VIDA MAX
            }
            BattleClass.valorD = PlayerScript.singleton.classe.defesa;
        }
        if (buff == 3)//AUMENTA A FORÇA 
        {
                PlayerScript.singleton.classe.forca += 15;//AUMENTA 15 NA FORÇA
            if (PlayerScript.singleton.classe.forca >= PlayerScript.singleton.classe.forcaMax)//SE A FORÇA FOR MAIOR QUE FORÇA MAXIMA NO UI 
            {
                PlayerScript.singleton.classe.forca = PlayerScript.singleton.classe.forcaMax;//FORCA RECEBE FORCA MAX
            }

            BattleClass.valorF = PlayerScript.singleton.classe.forca;
        }
    }//BUFF APÓS A DERROTA DE UM INIMIGO


}
