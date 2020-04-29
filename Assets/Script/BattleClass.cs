using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleClass : MonoBehaviour
{
    int enemyAtual = 0;
    public ClasseBase golem = new Golem(200,200, 35, 40);
    public ClasseBase goblin = new Goblin(100,100, 30, 20);
    public ClasseBase dragao = new Dragao(500,500, 50, 30);
    public ClasseBase necromancer = new Necromancer(90,90, 40, 10);
    public ClasseBase ogro = new Ogro(200,200, 44, 26);
    public static ClasseBase enemy { get; set; }
    private ClasseBase[] enemys = new ClasseBase[5];

    public GameObject ogroP;
    public GameObject dragaoP;
    public GameObject goblinP;
    public GameObject necromancerP;
    public GameObject golemP;

    private GameObject enemyPrefeb;


    public GameObject PainelDeDerrota,Passarturno;
    public GameObject PainelDeVitoria;


    public Button especialB, especialF;
    private int acurice, opcao, dano, valorPlayer, poderE;
    private bool enemyA, playerA, turnoPlayer,efeitosobreI;
    private int i;
    private void Awake()
    {
        goblin.prefab = goblinP;
        golem.prefab = golemP;
        ogro.prefab = ogroP;
        necromancer.prefab = necromancerP;
        dragao.prefab = dragaoP;

        enemys[0] = goblin;
        enemys[1] = golem;
        enemys[2] = dragao;
        enemys[3] = ogro;
        enemys[4] = necromancer;

        enemy = enemys[0];

        enemyPrefeb = Instantiate(enemy.prefab) as GameObject;

        var ClassePrefab = Instantiate(PlayerScript.singleton.classe.prefab) as GameObject;
        Debug.Log("Seu nome é : " + PlayerScript.nomePlayer);

        Debug.Log("OS STATUS INICIAIS DE CADA JOGADOR É DE: ");
        Debug.Log("PLAYER VIDA: " + PlayerScript.singleton.classe.vida);
        Debug.Log("PLAYER FORÇA: " + PlayerScript.singleton.classe.forca);
        Debug.Log("PLAYER DEFESA: " + PlayerScript.singleton.classe.defesa);
        Debug.Log("INIMIGO VIDA: " + enemy.vida);
        Debug.Log("INIMIGO FORÇA: " + enemy.forca);
        Debug.Log("INIMIGO DEFESA: " + enemy.defesa);
        turnoPlayer = true;
        OnDestroyButton(especialB);
        OnDestroyButton(especialF);
        Passarturno.SetActive(false);
        poderE = 0;
    }

    private void Update()
    {
        Especial();
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
        else if (acurice > 5 && acurice == 9)
        {
            Debug.Log(valor);
        }
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
                Debug.Log("OPÇÃO FOI ATACAR");
                playerA = true;//VARIAVEL BOOL PARA DEFINIR SE O PLAYER ESTÁ ATACANDO OU N 
                AcureceValueModif(PlayerScript.singleton.classe.forca);//PEGA OS VALORES DO PLAYER, TANTO DEFESA QUANTO ATAQUE 
               //NÃO MUDAR ESSA PARTE
                if (valorPlayer > enemy.forca && enemyA == true)//FAZ UMA VERIFICACAO SE O ATAQUE DO PLAYER É MAIOR OU IGUAL A FORÇA DO INIMIGO E SE O INMIGO ESTÁ ATACANDO SE ISSO TUDO ESTIVER CORRETO 
                {
                    //ATAQUE CRITICO DO PLAYER SOBRE O INIMGO
                    dano = valorPlayer + 5;//O PLAYER VAI CRITAR DANDO UM VALOR ADICIONAL AO ATAQUE
                    enemy.vida = enemy.vida - dano;//SUBTRAI O A VIDA PELO DANO
                    dano = 0;//RESETA O VALOR DO DANO
                    Debug.Log("PLAYER ACERTOU UM DANO CRITICO O INIMIGO FICOU COM " + enemy.vida);

                }
                else if (valorPlayer < enemy.forca && enemyA == true)//VERIFICA SE O ATAQUE DO INIMIGO É MAIOR QUE O DO PLAYER E SE O INIMIGO ESTÁ ATACANDO SE TUDO ISSO FOR VERDADE
                {
                    //CRITICO DO INIMIGO
                    dano = enemy.forca + 5;//ACRESCENTA UM VALOR AO DANO DO INIMIGO
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;//SUBTRAI A VIDA DO PLAYER PELO DANO
                    dano = 0;
                    Debug.Log("O INIMIGO ACERTO UM CONTRA ATAQUE CRITICO A VIDA DO PLAYER FICOU " + PlayerScript.singleton.classe.vida);
                }
                
                if (valorPlayer > enemy.defesa && enemyA == false)// VERIFICA SE O ATAQUE DO PLAYER É MAIOR DO QUE A DEFESA DO INIMGO E SE O INIMGO NÃO ESTA ATACANDO, SE ISSO FOR VERDADE
                {
                    // DANO MENOR 
                    dano = valorPlayer - enemy.defesa;// SUBTRAI O DANO DO PLAYER PELA DEFESA DO INIMIGO
                    enemy.vida = enemy.vida - dano;//SUBTRAI A VIDA DO INMIGO PELA VIDA DO PLAYER
                    dano = 0;
                    Debug.Log("O PLAYER ATACOU POREM O INIMIGO ESTAVA EM MODO DE DEFESA O ATAQUE PASSOU, A VIDA DO INIMIGO É DE " + enemy.vida);
                }
                else if (valorPlayer <= enemy.defesa && enemyA != true)// VERIFICA SE A FORÇA DO PLAYER É MENOR QUE A DEFESA DO INIMIGO E VERIFICA SE O INIMIGO NÃO ESTÁ ATACANDO, SE ISSO FOR VERDADE 
                {
                    //INIMIGO DEFENDEU
                    dano = 0;
                    Debug.Log("VOCE DEU MISS");
                }
                //NÃO MUDAR ESSA PARTE
                playerA = true;
                turnoPlayer = false;
                Passarturno.SetActive(true);//CANVAS
            }
            if (opcao == 2)
            {
                AcureceValueModif(AcureceValue(PlayerScript.singleton.classe.defesa));
                playerA = false;
                turnoPlayer = false;
                Passarturno.SetActive(true);
            }
            if (opcao == 3)
            {
                if (UiScript.playerClass == 1) 
                {
                    efeitosobreI = PlayerScript.singleton.classe.EspecialB(enemy);
                    Debug.Log("HABILIDADE SAIU COM EXITO");
                }

                if (UiScript.playerClass == 3) 
                {
                    efeitosobreI = PlayerScript.singleton.classe.EspecialB(enemy);
                    Debug.Log("HABILIDADE SAIU COM EXITO");
                }

                if (UiScript.playerClass == 2)
                {
                    efeitosobreI = PlayerScript.singleton.classe.EspecialB(enemy);
                    Debug.Log("HABILIDADE SAIU COM EXITO");
                }

                poderE -= 3;
                playerA = true;
                turnoPlayer = false;
                Passarturno.SetActive(true);
            }
            if (opcao == 4)
            {
                PlayerScript.singleton.classe.EspecialF(enemy);
                poderE -= 6;
                
                if (UiScript.playerClass == 1)
                {
                    efeitosobreI = false;
                }

                if (UiScript.playerClass == 3)
                {
                    efeitosobreI = false;
                }

                if (UiScript.playerClass == 2)
                {
                    efeitosobreI = false;
                    i = 3;
                }


                playerA = true;
                turnoPlayer = false;
                Passarturno.SetActive(true);
            }

            poderE += 1;
            turnoPlayer = false;
            Passarturno.SetActive(true);
            Debug.Log("TURNO DO PLAYER FINALIZADO");
            Death();
        }
        else 
        {
            Passarturno.SetActive(true);
            if (opcao == 5)
            {
                TurnoInimigo();
            }
        }
    }
    public void TurnoInimigo()
    {
        Debug.Log("TURNO DO INIMIGO");
        if (efeitosobreI == true) //CASO O PLAYER ATIVE O ESPECIAL O INIMIGO VAI VERIFICAR SE A O MISS ACERTOU OU N
        {
            if (UiScript.playerClass == 1)
            {
                Debug.Log("MISS");
                efeitosobreI = false;//SE SIM ELE VOLTA AO NORMAL 
            }

            if (UiScript.playerClass == 2)
            {
                Debug.Log("NESSA RODADA O INIMIGO NÃO ATACOU POR ESTAR CONGELADO");
                efeitosobreI = false;//SE SIM ELE VOLTA AO NORMAL 
            }

            Passarturno.SetActive(false);
            turnoPlayer = true;

        }
        else 
        {
            RandomAtaq();
            if (enemyA == true)
            {
                Debug.Log("INIMIGO ATACOU");
                if (enemy.forca > valorPlayer && playerA == true)
                {
                    dano = enemy.forca + 5;
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                    dano = 0;
                    Debug.Log("INIMIGO ACERTOU UM ATAQUE CRITICO A VIDA DO PLAYER FOI PARA " + PlayerScript.singleton.classe.vida);
                }
                else if (enemy.forca < valorPlayer && playerA == true)
                {
                    dano = valorPlayer + 5;
                    enemy.vida = enemy.vida - dano;
                    dano = 0;
                    Debug.Log("INIMIGO TOMOU UM CONTRA ATAQUE CRITICO A VIDA DO INIMIGO FOI PARA " + enemy.vida);

                }

                if (enemy.forca > valorPlayer && playerA == false)
                {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULTADO GOLEM TOMA DANO
                    Debug.Log(valorPlayer);
                    dano =  enemy.forca - valorPlayer;
                    PlayerScript.singleton.classe.vida = PlayerScript.singleton.classe.vida - dano;
                    dano = 0;
                    Debug.Log("INIMIGO ACERTOU ATAQUE PODEREM O PLAYER ESTAVA EM MODO DE DEFESA, MESMO ASSIM A DEFESA DO PLAYER N FOI SUFICIENTE " + PlayerScript.singleton.classe.vida);
                }
                else if (enemy.forca <= valorPlayer && playerA != true)
                {//PLAYER TA ATACANDO E O GOLEM DEFENDENDO RESULDADO GOLEM N TOMA DONO
                    dano = 0;
                    Debug.Log("MISS " + PlayerScript.singleton.classe.vida);
                }
                Death();
                Passarturno.SetActive(false);
                turnoPlayer = true;
            }
            else
            {
                Death();
                Debug.Log("INIMIGO ENTROU EM MODO DE DEFESA");
                //PODEMOS ADICIONAR UM ROLL AQUI TB PARA QUE O INIMIGO TENHA CHANCE DE TOMAR DANO QUANDO TIVER EM MODO DE DEFESA
                enemyA = false;
                Passarturno.SetActive(false);
                turnoPlayer = true;
            }

            if (UiScript.playerClass == 2)
            {
                if (i>0)
                {
                    i -= 1;
                    enemy.vida -= 20;
                    Debug.Log("INIMIGO TOMOU TORNS " + enemy.vida);
                }
            }

            if (UiScript.playerClass == 3) 
            {
                if (PlayerScript.singleton.classe.forca >= 10) 
                {
                    PlayerScript.singleton.classe.vidaMax -= 50;
                    PlayerScript.singleton.classe.vida -= 50;
                    PlayerScript.singleton.classe.forca -= 30;
                    PlayerScript.singleton.classe.defesa -= 30;
                }
                Debug.Log("vida " + PlayerScript.singleton.classe.vida + "forca " + PlayerScript.singleton.classe.forca + "defesa " + PlayerScript.singleton.classe.defesa);
            }

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
    public void OnCreateButton(Button especial)
    {
        especial.enabled = true;
        especial.image.color = Color.white;
    }   
    public void Especial() 
    {
        if (poderE < 3) 
        {
            OnDestroyButton(especialB);
            OnDestroyButton(especialF);
        }

        if(poderE > 3 &&  poderE < 6) 
        {
            OnCreateButton(especialB);
            OnDestroyButton(especialF);
        }
       
        if (poderE == 6)
        {
            OnCreateButton(especialF);
        }

        if (poderE > 6) 
        {
            poderE = 6;
        }
    }
    public void Duelo(int op)
    {     
        TurnoPlayer(op);  
    }
    public void Death()
    {
        if(enemy.vida <= 0)
        {
            Debug.Log("VOCE VENCEU BLUAHAHAHAH");
            GameObject.Destroy(enemyPrefeb);      
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
            enemyPrefeb = Instantiate(enemy.prefab);
            Debug.Log("UM NOVO INIMIGO SURGIU, SUA VIDA É " + enemy.vida);           
        }

    }



}
