namespace LabMooGame.Data;

public static class DataContextConfigCreator
{
    public static DataContextConfig CreateConfig()
    {
        return new DataContextConfig("moohighscores", "mastermindhighscores");
    }
}
