using UnityEngine;
using System.Collections;

public abstract class ClasseBase
{

    //CRIA AS VARIAVEIS DA CLASS BASE
    public int vida;
    public int forca;
    public int defesa;
    public int vidaMax;
    public int forcaMax;
    public int defesaMax;
    public bool desviar;
    //CRIA O GAME OBJECT DA CLASSE BASE
    public GameObject prefab;

    
    public abstract bool EspecialB(ClasseBase inimigo);//CRIA UMA FUNÇÃO ABTRACT DE ESPECIAL COM UM PARAMETRO DE UM ALVO, NO CASO O ALVO É O INIMIGO.
    public virtual bool Roll(ClasseBase inimigo) //FUNÇÃO VIRTUAL FAZ UM ROLL PARA VERIFICAR SE O INIMIGO VAI DAR MIRR NO TURNO SEGUINTE 
    {
        int acurice = Random.Range(0, 10);
        if (acurice >= 0 && acurice <= 7)//SE O ROLL FOR MAIOR OU IGUAL A 0 E MENOR OU IGUAL A 7 ENTÃO  O INIMIGO ERRA O ATAQUE
        {
            desviar = true;// se for true então 
        }
        else if (acurice > 7 && acurice == 10)//SE O VALOR DO ROLL FOR MAIOR QUE 7 E IGUAL A 10 ENTÃO O INIMIGO ATACA NORMALMENTE 
        {
            desviar = false;// se for false então o inimigo ataca   
        }
        return desviar;//RETORNA O VALOR
    }
    abstract public void EspecialF(ClasseBase inimigo);//ESPECIAL FORTE 

}

public class Ninja : ClasseBase
{
    public Ninja(int vidaMax, int defesaMax, int forcaMax, int vida, int forca, int defesa)//COSTRUTOR DO NINJA
    {
        this.vida = vida;//VIDA DO NINJA
        this.forca = forca;//FORÇA DO NINJA
        this.defesa = defesa;//DEFESA DO NINJA
        this.vidaMax = vidaMax;//VIDA MAXIMA DO NINJA
        this.defesaMax = defesaMax;//DEFESA MAXIMA DO NINJA
        this.forcaMax = forcaMax;//FORÇA MAXIMA DO NINJA
    }
    
    public override void EspecialF(ClasseBase inimigo)//FAZ UM OVERRIDE DA FUNÇÃO BASE E ADICIONA OS PROPRIOS METODOS DA CLASSE NINJA
    {   
        if (vida <= 20)//SE A VIDA DO NINJA ESTIVER MENOR OU IGUAL A 20
        {
            vida += 20;//ADICIONA MAIS 2O DE VIDA 
            inimigo.vida -= forca;//DA UM ATAQUE COM A FORÇA NORMAL DO NINJA
            inimigo.vida -= forca + forca / 2;//DA UM ATAQUE COM A FORÇA AUMENTADA 
            inimigo.vida -= forca + forca;//DA UM ATAQUE FO A FORÇA DOBRADA
        }
        else //SE A VIDA DO NINJA NÃO ESTIVER MENOR OU IGUAL A 20 ENTÃO O ATAQUE VAI EXISTIS POREM NÃO RECUPERA VIDA
        {
            inimigo.vida -= forca;
            inimigo.vida -= forca + forca / 2;
            inimigo.vida -= forca + forca;
        }
       
    }
    public override bool EspecialB(ClasseBase inimigo)//ESPECIAL BASICO DO NINJA FAZ UM OVERRIDE DO ESPECIAL BASE DA CLASSE BASE 
    {
        vida += 50;//VIDA AUMENTA 50% DA VIDA DO NINJA
        if (vida > vidaMax)//SE A VIDA MAXIMA DO NINJA FOR MENOR QUE A VIDA 
        {
            vida = vidaMax;//VIDA RECEBE VIDA MAXIMA
        }
        return Roll(inimigo);//CHAMA A FUNÇÃO DE MISS, ELA SERVE PARA DEIXAR O NINJA COM UM PERCENTUAL ALTO DE NÃO TOMAR UM HIT NO PROXIMO ATAQUE 
    }
    public override bool Roll(ClasseBase inimigo)//FAZ UM OVERRIDE DA FUNÇÃO VIRTUAL DA CLASSE BASE 
    {
        return base.Roll(inimigo);//SÓ CHAMA A BASE DA FUNÇÃO COM UM ALVO, QUE É O INIMIGO
    }
}

public class Berserker : ClasseBase
{
    public Berserker(int vidaMax, int defesaMax, int forcaMax, int vida, int forca, int defesa)//CONTRUTOR PARA O BERSERKER
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
        this.defesaMax = defesaMax;
        this.forcaMax = forcaMax;
    }

    public override bool EspecialB(ClasseBase inimigo)//FAZ O OVERRIDE PARA O ESPECIAL BASICO DO BERSERKER
    {
        vida = 300;//ADD 300 DE VIDA
        vidaMax = 300;//ADD 300 DE VIDA MAX
        forca = 80;//ADD 80 DE DANO 
        defesa = 80;//ADD 80 DE DEFESA
        return false;
    }
    public override void EspecialF(ClasseBase inimigo)//FAZ UM OVERRIDE DO ESPECIAL FORTE DA CLASSE BASE 
    {
        int dano;
        dano = forca * 5;//O INIMIGO TOMA UM DANO COM 5 VEZES A FORÇA ATUAL DO PLAYER
        inimigo.vida -= dano;
    }
}

public class Mago : ClasseBase
{
    public Mago(int vidaMax, int defesaMax, int forcaMax, int vida, int forca, int defesa)//CONTRUTOR PARA O MAGO
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
        this.defesaMax = defesaMax;
        this.forcaMax = forcaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)//SÓ RETORNA TRUE E NA OUTRA CLASSE A FUNÇÃO É VERIFICADA
    {
        return true;
    }
    public override void EspecialF(ClasseBase inimigo)//O INIMIGO RECEBE UM DANO DE 2 VEZES A FORÇA ATUAL DO MAGO
    {
        inimigo.vida -= forca*2;
    }

}

/***************CRIA TODAS AS CLASSES DOS INIMIGOS A PARTIR DA CLASSE BASE SEM NENHUM OVERRIDE*************

                                    ||
                                    ||
                                    ||
                                    ||
                                    ||
                                ---------
                                 -     -
                                  -   -
                                   - -
                                    -
*/
public class Golem : ClasseBase
{

    public Golem(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Goblin : ClasseBase
{

    public Goblin(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Dragao : ClasseBase
{

    public Dragao(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Ogro : ClasseBase
{

    public Ogro(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Necromancer : ClasseBase
{

    public Necromancer(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}


