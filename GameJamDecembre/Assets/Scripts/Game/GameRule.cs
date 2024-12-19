[System.Serializable] //pour voir dans l'inspecteur (gadget oh oh)
public class GamesRules
{
    public string PartiConcerne1;
    public int PointsRemporte1;
    public string Parti2;
    public int Points2;

    public GamesRules(string parti1, int points1, string parti2, int points2)
    {
        PartiConcerne1 = parti1;
        PointsRemporte1 = points1;
        Parti2 = parti2;
        Points2 = points2;
    }
}
