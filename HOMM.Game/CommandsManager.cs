using System.Collections.Generic;
using System.Linq;
using HOMM.BattleObjects;
using HOMM.Game.Commands;

namespace HOMM.Game
{
    public static class CommandsManager
    {
        private static readonly IList<ICommand> History = new List<ICommand>();
        private static TargetableCommand _selectedCommand;

        public static void SelectCommand(TargetableCommand command)
        {
            _selectedCommand = command;
        }

        public static void SelectStack(BattleUnitsStack stack)
        {
            if (_selectedCommand == null) return;
            
            _selectedCommand.SetTarget(stack);
            Execute(_selectedCommand);
            _selectedCommand = null;
        }

        public static void Execute(ICommand command)
        {
            if (command.Execute()) Push(command);
        }
        
        public static void Push(ICommand command)
        {
            History.Add(command);
        }

        public static ICommand Pop()
        {
            var command = History.Last();
            History.RemoveAt(History.Count - 1);
            return command;
        }
        
        public static IList<ICommand> GetHistory() => History;
    }
}