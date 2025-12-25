abstract class LanguageSkill
{
    public readonly string name;
    public readonly WritingSystem writingSystem;
    public int proficiency;
    public LanguageSkill(string name, WritingSystem writingSystem,  int proficiency)
    {
        if (proficiency<0 || proficiency>3)
        {
            throw new Exception();
        }
        this.name = name;
        this.writingSystem = writingSystem;
        this.proficiency = proficiency;
    }
    public abstract void speak() { }
}

public class SkillComparer: Comparer<LanguageSkill>
{
    public override int compare(LanguageSkill s1, LanguageSkill s2)
    {
        return s1.proficiency-s2.proficiency;
    }
}