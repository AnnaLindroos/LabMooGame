namespace LabMooGame.Interfaces;

public interface IIO
{
    string Read();
    void Write(string message);
    void WriteLine(string message);
}
