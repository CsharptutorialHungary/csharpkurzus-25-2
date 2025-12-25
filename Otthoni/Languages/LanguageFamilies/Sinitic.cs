class Sinitic: LanguageSkill
{
    private readonly Tone _tone;
    public Sinitic(string name, WritingSystem writingSystem, Tone tone, int proficiency): LanguageSkill(namespace, writingSystem, proficiency)
    {
        this._tone=tone;
    }
}