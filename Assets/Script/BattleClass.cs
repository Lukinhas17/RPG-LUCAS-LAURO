using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleClass : MonoBehaviour
{
    int enemyAtual = 0;
    public ClasseBase golem = new Golem(50, 34, 26);
    public ClasseBase goblin = new Goblin(50, 30, 26);
    public ClasseBase dragao = new Dragao(200, 34, 26);
    public ClasseBase necromancer = new Necromancer(100, 30, 26);
    public ClasseBase ogro = new Ogro(200, 34, 26);
    public ClasseBase enemy { get; set; }
    public Button especialB;
    public GameObject PainelDeDerrota;
    public GameObject PainelDeVitoria;
    private ClasseBase[] enemys = new ClasseBase[5];
    private int rodada, acurice, opcao, dano, valorPlayer;
    bool enemyA, playerA, turnoPlayer, debuf;

    private void Awake()
    {
        enemys[0] = goblin;
        enemys[1] = golem;
        enemys[2] = dragao;
        enemys[3] = ogro;
        enemys[4] = necromancer;
    }
    void Start()
    {
        enemy = enemys[0];
        OnDebug();
        var ClassePrefab = Instantiate(PlayerScript.singleton.classe.prefab) as GameObject;
        Debug.Log("Seu nome é : " + PlayerScript.nomePlayer);
        rodada = 3;
        turnoPlayer = true;
        OnDestroyButton(especialB);      
    }
    public int AcureceValue(int valor)//ACURECEVALUE RECEBE POR PARAMETRO UM VALOR ESSE VALOR É OU O ATAQUE OU A DEFESA DO PLAYER
    {
        acurice = Random.Range(0, 10);
        if (acurice == 0)
        {
            valor = 0;
        }
        else if (acurice > 1 && acurice <= 5)
        {
            valor = valor - valor / 2;
        }
        else if (acurice > 5 && acurice == 9) { }
        else
        {
            valor = valor + valor / 2;
        }

        return valor;
    }
    public void AcureceValueModif(int valor)
    {
        valorPlayer = valor;
    }
    public void TurnoPlayer(int op)//TURNO DO PLAYER
    {      
        opcao = op;
        //VERIFICA SE É O TURNO DO PLAYER, ESTANDO TRUE É O TURNO DO PLAYER
        if (turnoPlayer == true)
        {
            Debug.Log("TURNO DO PLAYER");

            // SE A OPCAO DO PLAYER FOR 1 ELE ATACA SE N ELE DEFENDE - PARAMETRO DEFINIDO NO INSPECTOR
            if (opcao == 1)
            {
                playerA = true;//VARIAVEL BOOL PARA DEFINIR SE O PLAYER ESTÁ ATACANDO OU N 
                AcureceValueModif(PlayerScript.singleton.classe.forca);//PEGA OS VALORES DO PLAYER, TANTO DEFESA QUANTO ATAQUE 
                if (valorPlayer >= enemy.forca && enemyA == true)//FAZ UMA VERIFICACAO SE O ATAQUE DO PLAYER É MAIOR OU IGUAL A FORÇA DO INIMIGO E SE O INMIGO ESTÁ ATACANDO SE ISSO TUDO ESTIVER CORRETO 
                {
                    //ATAQUE CRITICO DO PLAYER SOBRE O INIMGO
                    dano = valorPlayer + 5;//O PLAYER VAI CRITAR DANDO UM VALOR ADICIONAL AO ATAQUE
                    enemy.vida = enemy.vida - dano;//SUBTRAI O A VIDA PELO DANO
                    dano = 0;//RESETA O VALOR DO DANO
                    OnDebug();
                }
                else if (valorPlayer <= enemy.forca && enemyA == true)//VERIFICA SE O ATAQUE DO INIMIGO É MAIOR QUE O DO PLAYER E SE O INIMIGO ESTÁ ATACANDO SE TUDO ISSO FOR VERDADE
                {
                    //CRITICO DO INIMIGO
                    dano = enemy.forca + 5;//ACRESCENTA UM VALOR AO DANO DO INIMIGO
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;//SUBTRAI A VIDA DO PLAYER PELO DANO
                    dano = 0;
                    OnDebug();
                }

                if (valorPlayer >= enemy.defesa && enemyA != true)// VERIFICA SE O ATAQUE DO PLAYER É MAIOR DO QUE A DEFESA DO INIMGO E SE O INIMGO NÃO ESTA ATACANDO, SE ISSO FOR VERDADE
                {
                    // DANO MENOR 
                    dano = valorPlayer - enemy.defesa;// SUBTRAI O DANO DO PLAYER PELA DEFESA DO INIMIGO
                    enemy.vida = enemy.vida - dano;//SUBTRAI A VIDA DO INMIGO PELA VIDA DO PLAYER
                    dano = 0;
                    OnDebug();
                }
                else if (valorPlayer <= enemy.defesa && enemyA != true)// VERIFICA SE A FORÇA DO PLAYER É MENOR QUE A DEFESA DO INIMIGO E VERIFICA SE O INIMIGO NÃO ESTÁ ATACANDO, SE ISSO FOR VERDADE 
                {
                    //INIMIGO DEFENDEU
                    dano = 0;
                    OnDebug();

                }

            }
            if (opcao == 2)
            {
                AcureceValueModif(AcureceValue(PlayerScript.singleton.classe.defesa));
                playerA = false;
            }
            else
            {
                EspecialN();
            }
            turnoPlayer = false;
            Debug.Log("TURNO DO PLAYER FINALIZADO");
            Death();
        }
        else
        {
            Death();
            TurnoInimigo();
            turnoPlayer = true;
            Debug.Log("TURNO DO PLAYER FINALIZADO");
            //N É TRUE ENTÃO É O TURNO DO INIMIGO
        }
    }
    public void TurnoInimigo()
    {
        Debug.Log("TURNO DO INIMIGO");
        if (opcao == 1)
        {
            EspecialV();
        }
        Debug.Log("RODADA NUMERO " + rodada);

        RandomAtaq();
        if (enemyA == true)
        {
            Debug.Log("INIMIGO ATACANDO");

            if (enemy.forca >= valorPlayer && playerA == true)
            {
                dano = enemy.forca + 5;
                PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                dano = 0;
                OnDebug();
            }
            else if (enemy.forca <= valorPlayer && playerA == true)
            {
                dano = valorPlayer + 5;
                enemy.vida = enemy.vida - dano;
                dano = 0;
                OnDebug();
            }

            if (enemy.forca >= valorPlayer && enemyA != true)
            {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULTADO GOLEM TOMA DANO
                dano = valorPlayer - enemy.defesa;
                enemy.vida = enemy.vida - dano;
                dano = 0;
                OnDebug();
            }
            else if (enemy.forca <= valorPlayer && enemyA != true)
            {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULDADO GOLEM N TOMA DONO
                dano = 0;
                OnDebug();
            }
            Death();
        }
        else
        {
            Death();
            Debug.Log("INIMIGO DEFENDENDO");
            enemyA = false;
        }
    }
    public void RandomAtaq()
    {
        int randomAtaq = Random.Range(0, 10);

        if (randomAtaq <= 5)
        {
            enemyA = true;
        }
        else
        {
            enemyA = false;
        }
    }
    public void OnDestroyButton(Button especial)
    {
        especial.enabled = false;
        especial.image.color = Color.red;

    }
    public void EspecialN()
    {
        if (debuf != true)
        {
            if (rodada == 0)
            {
                PlayerScript.singleton.classe.EspecialP(enemy);
                debuf = true;
                rodada = 3;
                OnDestroyButton(especialB);
            }
        }
    }
    public void EspecialV()
    {
        if (rodada > 0)
        {
            rodada -= 1;
        }

        if (rodada <= 0)
        {
            especialB.enabled = true;
            especialB.image.color = Color.white;
            debuf = false;
        }
        else
        {
            OnDestroyButton(especialB);
        }

        if (debuf == true)
        {
            dano = 20;
            enemy.vida -= dano;
            dano = 0;
            Debug.Log("VIDA DO INIMIGO DEPOIS DO DEBUF " + enemy.vida);
        }
    }
    public void OnDebug()
    {
        Debug.Log("VIDA DO PLAYER  " + PlayerScript.singleton.classe.vida);
        Debug.Log("VIDA DO INIMIGO " + enemy.vida);
    }

    public void Duelo(int op)
    {     
        Debug.Log("botao");
        TurnoPlayer(op);  
    }

    public void Death()
    {
        if(enemy.vida <= 0)
        {
            Debug.Log("VOCE VENDEU BLUAHAHAHAH");
            TrocarDeInimigo();
            //PainelDeVitoria.SetActive(true);
        }

        if (PlayerScript.singleton.classe.vida <= 0)
        {
            Debug.Log("VOCE MORREU");
            PainelDeDerrota.SetActive(true);
        }

        return;
    }

    public void TrocarDeInimigo()
    {
        if (enemyAtual < enemys.Length)
        {
            enemyAtual++;
            enemy = enemys[enemyAtual];
            Debug.LogError("UM NOVO INIMIGO SURGIU, SUA VIDA É " + enemy.vida);           
        }

    }

}
