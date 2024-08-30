namespace LabMooGame.Data
{
    public class DataContextConfig
    {
        public readonly string _filePathMooGame = "moohighscores";

        public readonly string _filePathMasterMind = "mastermindhighscores";
        public DataContextConfig(string filePathMooGame, string filePathMasterMind)
        {
            ArgumentNullException.ThrowIfNull(filePathMooGame);
            ArgumentNullException.ThrowIfNull(filePathMasterMind);
            _filePathMooGame = filePathMooGame;
            _filePathMasterMind = filePathMasterMind;
        }
    }
}
