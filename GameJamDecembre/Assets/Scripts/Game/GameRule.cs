[System.Serializable] //pour voir dans l'inspecteur (gadget oh oh)
public class GameRule
{
    public string Law;
    public int IdLaws;

    // Constructeur pour initialiser facilement les donn�es
    public GameRule(string law, int id)
    {
        Law = law;
        IdLaws = id;
    }
}
