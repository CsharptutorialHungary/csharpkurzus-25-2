class Speaker
{
    public readonly string name;
    public ArrayList<LanguageSkill> languages;
    public Speaker(string name)
    {
        this.name = name;
        languages = new ArrayList();
    }
    public Speaker(string name, LanguageSkill native)
    {
        this.name=name;
        languages = new ArrayList();
        languages.Add(native);
    }
    public void addLanguage(LanguageSkill l)
    {
        for (int i = 0; i < languages.size(); i++)
        {
            if languages[i].name == l.name{
                languages[i].proficiency=l.proficiency;
                languages.sort(new SkillComparer());
                return;
            }
        }
        languages.Add(l);
        languages.sort(new SkillComparer());
    }
    public LanguageSkill native()
    {
        if (languages.size()==0)
        {
            throw new Exception(name+" is a feral child; they don't speak any human language.")
        }
        return languages.get(0);
    }
    public void lingual()
    {
        if (languages.size() == 0)
        {
            Console.WriteLine(name + " is a feral child; they don't speak any human language.");
            return;
        }
        string lingual;
        switch (languages.size())
        {
            case 1:
                lingual = "monolingual";
                break;
            case 2:
                lingual = "bilingual";
                break;
            case 3:
                lingual = "trilingual";
                break;
            default:
                lingual = "a polyglot";
        }
        Console.WriteLine(name + " can speak " + languages.size() + " language(s) - they are " + lingual+".");
    }
    public void speak(int index)
    {
        if (index<0)
        {
            throw new Exception();
        }
        if (index>=languages.size())
        {
            Console.WriteLine(name + " doesn't know that many languages.");
            return;
        }
        languages[index].speak();
    }
}