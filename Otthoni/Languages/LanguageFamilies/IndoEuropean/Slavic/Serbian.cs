class Serbian : Slavic
{
    private readonly string _cyrillic;
    public Serbian(int proficiency) : Slavic("Croatian", new LatinAlphabet("Croatian", "abcèæ"), proficiency)
    {
        this._cyrillic="abvg";
    }
}