[System.Serializable] //pour voir dans l'inspecteur (gadget oh oh)
public class CibleLoi
{
    public bool IsSpecial;
    public string Law;
    public string Parti1;
    public int Points1;
    public string Parti2;
    public int Points2;
    public string Parti3;
    public int Points3;

    public CibleLoi(bool isSpecial, string law, string parti1, int points1, string parti2, int points2, string parti3, int points3)
    {
        IsSpecial = isSpecial;
        Law = law;
        Parti1 = parti1;
        Points1 = points1;
        Parti2 = parti2;
        Points2 = points2;
        Parti3 = parti3;
        Points3 = points3;
    }
}
