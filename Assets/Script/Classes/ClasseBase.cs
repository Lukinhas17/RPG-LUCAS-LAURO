using UnityEngine;
using System.Collections;

public abstract class ClasseBase
{
    public int vida;
    public int forca;
    public int defesa;
    public int vidaMax;
    public GameObject prefab;
    public abstract bool EspecialB(ClasseBase inimigo);
    public virtual bool Roll(ClasseBase inimigo) 
    {
        int acurice = Random.Range(0, 10);
        if (acurice >= 0 && acurice <= 7)
        {
            Debug.Log("O PROXIMO ATAQUE DO INIMIGO TERÁ UMA GRANDE CHANDE DE DAR MISS");
        }
        else if (acurice >= 8 && acurice == 10) 
        {
            inimigo.forca += inimigo.forca;
            Debug.Log("VALOR DA FORÇA DO INIMIGO COM O MISS ALTO" + inimigo.forca);
        }
        return true;
    }
    abstract public void EspecialF(ClasseBase inimigo);
}

public class Ninja : ClasseBase
{
    public Ninja(int vidaMax,int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public int Especial()
    {
        vida += 55;
        int forcaE = forca + 40;
        Debug.Log("VIDA RECUPERADA " + vida);
        return forcaE;

    }
    public override void EspecialF(ClasseBase inimigo)
    {
        Debug.Log("ESPECIAL 4 LAMINAS MORTAIS");
        int dano;
        dano = Especial();
        inimigo.vida -= dano;
        Debug.Log("INIMIGO TOMOU UM ATAQUE RAPIDO");//COLOCAR UMA COROTINA
        inimigo.vida -= 20;
        Debug.Log("INIMIGO TOMOU UM ATAQUE RAPIDO" + inimigo.vida);
        inimigo.vida -= 20;
        Debug.Log("INIMIGO TOMOU UM ATAQUE RAPIDO" + inimigo.vida);
        inimigo.vida -= 20;
        Debug.Log("INIMIGO TOMOU UM ATAQUE RAPIDO" + inimigo.vida);
       
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        Debug.Log("TENTE ME ACERTAR");
        return Roll(inimigo);
    }
    public override bool Roll(ClasseBase inimigo)
    {
        return base.Roll(inimigo);
    }
}

public class Berserker : ClasseBase
{
    public Berserker(int vidaMax,int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }

    public override bool EspecialB(ClasseBase inimigo)
    {
        Debug.Log("MODO BERSERKER ATIVADO");
        Debug.Log("DEUS ACIMA DE TODOS BESERKER ACIMA DE TUDOO HA HA HAA");
        vida = 300;
<<<<<<< HEAD
        vidaMax = 300;
=======
>>>>>>> master
        forca = 70;
        defesa = 70;
        Debug.LogError("ATRIBUTOS ALTOS");
        return false;
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        int dano;
        Debug.Log("SINTA O PESO DO MARTELOOO");//COLOCAR UMA COROTINA
        dano = forca += forca * 3;
        inimigo.vida -= dano;
        Debug.Log("INIMIGO TOMOU UM ATAQUE DE E FICOU COM A VIDA " + inimigo.vida);//COLOCAR UMA COROTINA
       
    }
}

public class Mago : ClasseBase
{
    public Mago(int vidaMax, int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
        this.vidaMax = vidaMax;
    }
    public override bool EspecialB(ClasseBase inimigo)
    {
        Debug.Log("ORBE DE GELO FIQUE PARADO POR UM TURNO");
        return true;
    }
    public override void EspecialF(ClasseBase inimigo)
    {
        Debug.Log("EFEITO DE TORNS");
        inimigo.vida -= 50;
    }

}


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


