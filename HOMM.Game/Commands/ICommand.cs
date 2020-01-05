namespace HOMM.Game.Commands
{
    public interface ICommand
    {
        bool Execute();
        
        string GetMessage();
    }
}