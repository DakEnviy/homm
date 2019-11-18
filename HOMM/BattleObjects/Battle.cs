using System;
using System.Collections.Generic;

namespace HOMM.BattleObjects
{
    public enum BattleState
    {
        None,
        InGame,
        Ended
    }
    
    public class Battle
    {
        private readonly IList<BattleArmy> _armies;

        private BattleState _state;
        private int _round;
        private BattleArmy _winner;
        
        private int _currentArmyIndex;
        private BattleUnitsStack _currentStack;

        public Battle(IList<BattleArmy> armies)
        {
            _armies = armies;

            _state = BattleState.None;
            _round = 0;
            _winner = null;
            
            _currentArmyIndex = -1;
            _currentStack = null;
        }
        
        public void Attack(BattleUnitsStack target) {}
        
        public void UseSkill(/* Skill */) {}
        
        public void Wait() {}
        
        public void Defend() {}
        
        public void Surrender() {}

        public IList<BattleArmy> GetArmies() => _armies;

        public BattleState GetBattleState() => _state;
        
        public int GetRound() => _round;
        
        public BattleArmy GetWinner() => _winner;

        public int GetCurrentArmyIndex() => _currentArmyIndex;

        public BattleArmy GetCurrentArmy() => _armies[_currentArmyIndex];
        
        public BattleUnitsStack GetCurrentStack() => _currentStack;
    }
}