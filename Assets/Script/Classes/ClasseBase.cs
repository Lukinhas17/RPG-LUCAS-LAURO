using UnityEngine;
using System.Collections;

public abstract class ClasseBase
{
    public int vida;
    public int forca;
    public int defesa;
    public int op;
    public bool especial;
    public GameObject prefab;
    abstract public void EspecialP(ClasseBase inimigo);
}

public class Ninja : ClasseBase
{
    public Ninja(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public int Especial()
    {
        vida += 55;
        int forcaE = forca + 40;
        Debug.Log("VIDA RECUPERADA " + vida);
        especial = false;
        return forcaE;
    }
    public override void EspecialP(ClasseBase inimigo)
    {
        int dano;
        dano = Especial();
        inimigo.vida -= dano;
    }
}

public class Berserker : ClasseBase
{
    public Berserker(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }

    public override void EspecialP(ClasseBase inimigo)
    {
        vida += 50;
        forca += 10;
        defesa += 20;
        Debug.Log("ESPECIAL DO BERSERKER");
    }

}

public class Mago : ClasseBase
{
    public Mago(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public override void EspecialP(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }

}


public class Golem : ClasseBase
{

    public Golem(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public override void EspecialP(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Goblin : ClasseBase
{

    public Goblin(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public override void EspecialP(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Dragao : ClasseBase
{

    public Dragao(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public override void EspecialP(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Ogro : ClasseBase
{

    public Ogro(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public override void EspecialP(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}

public class Necromancer : ClasseBase
{

    public Necromancer(int vida, int forca, int defesa)
    {
        this.vida = vida;
        this.forca = forca;
        this.defesa = defesa;
    }
    public override void EspecialP(ClasseBase inimigo)
    {
        throw new System.NotImplementedException();
    }
}


