using System;
using System.Collections.Generic;
using System.Linq;
using HOMM.Objects;

namespace HOMM.BattleObjects
{
    public class BattleUnitsStack
    {
        private readonly UnitsStack _baseStack;
        private readonly Unit _baseUnit;
        private readonly BattleArmy _army;
        
        private readonly IList<BattleUnitsStackMod> _mods;

        private int _amount;
        private int _topHitPoints;
        private int _attack;
        private int _defence;
        private (int, int) _damage;
        private float _initiative;

        private bool _isDefended;
        private bool _isWaiting;
        private bool _isAnswered;

        public BattleUnitsStack(UnitsStack stack, BattleArmy army)
        {
            _baseStack = stack;
            _baseUnit = stack.GetUnit();
            _army = army;

            RestoreFromBaseStack();

            _isDefended = false;
            _isWaiting = false;
            _isAnswered = false;
            
            _mods = new List<BattleUnitsStackMod>();

            foreach (var property in _baseUnit.GetProperties())
            {
                var mod = property.GetMod(this);
                if (mod != null) AddMod(mod);
            }
        }

        public void RestoreFromBaseStack()
        {
            SetDefaultHitPoints();
            SetDefaultParams();
        }

        public void SetDefaultHitPoints()
        {
            _amount = _baseStack.GetAmount();
            _topHitPoints = _baseUnit.GetHitPoints();
        }

        public void SetDefaultParams()
        {
            _attack = _baseUnit.GetAttack();
            _defence = _baseUnit.GetDefence();
            _damage = _baseUnit.GetDamage();
            _initiative = _baseUnit.GetInitiative();
        }

        public void Round()
        {
            foreach (var mod in _mods)
            {
                mod.Round();
            }

            foreach (var mod in _mods.Where(mod => mod.GetRoundsLeft() == 0))
            {
                RemoveMod(mod);
            }
            
            UpdateParamsByMods();
        }

        public void AddMod(BattleUnitsStackMod mod)
        {
            mod.Attach();
            _mods.Add(mod);
        }

        public void RemoveMod(BattleUnitsStackMod mod)
        {
            mod.Detach();
            _mods.Remove(mod);
        }

        public bool ContainsMod(BattleUnitsStackMod mod)
        {
            return _mods.Contains(mod);
        }

        public void UpdateParamsByMods()
        {
            SetDefaultParams();

            foreach (var mod in _mods)
            {
                mod.UpdateParams();
            }
        }

        public void Heal(ushort hitPoints)
        {
            _topHitPoints = Math.Min(_topHitPoints + hitPoints, _baseUnit.GetHitPoints());
        }

        public void Resurrect(ushort hitPoints)
        {
            SetHitPoints(GetHitPoints() + hitPoints);
        }

        public void Damage(ushort hitPoints)
        {
            SetHitPoints(GetHitPoints() - hitPoints);
        }
        
        public bool IsAlive() => _amount > 0;
        
        public bool IsDead() => _amount == 0;

        public int GetHitPoints()
        {
            return _amount == 0 ? 0 : (_amount - 1) * _baseUnit.GetHitPoints() + _topHitPoints;
        }

        public void SetHitPoints(int hitPoints)
        {
            if (hitPoints <= 0)
            {
                _amount = 0;
                _topHitPoints = 0;

                SetDefaultParams();

                return;
            }

            var maxHitPoints = _baseUnit.GetHitPoints();

            _amount = hitPoints / maxHitPoints + 1;
            _topHitPoints = hitPoints % maxHitPoints;

            if (_amount <= _baseStack.GetAmount()) return;

            SetDefaultHitPoints();
        }

        public UnitsStack GetBaseStack() => _baseStack;

        public Unit GetBaseUnit() => _baseUnit;

        public BattleArmy GetArmy() => _army;

        public int GetAmount() => _amount;

        public void SetAmount(int amount)
        {
            _amount = amount;
        }

        public int GetTopHitPoints() => _topHitPoints;

        public void SetTopHitPoints(int topHitPoints)
        {
            _topHitPoints = topHitPoints;
        }

        public int GetAttack() => _attack;

        public void SetAttack(int attack)
        {
            _attack = attack;
        }

        public int GetDefence() => _defence;

        public void SetDefence(int defence)
        {
            _defence = defence;
        }

        public (int, int) GetDamage() => _damage;

        public void SetDamage((int, int) damage)
        {
            _damage = damage;
        }

        public float GetInitiative() => _initiative;

        public void SetInitiative(float initiative)
        {
            _initiative = initiative;
        }

        public bool IsDefended() => _isDefended;

        public void SetDefended(bool isDefended)
        {
            _isDefended = isDefended;
        }

        public bool IsWaiting() => _isWaiting;

        public void SetWaiting(bool isWaiting)
        {
            _isWaiting = isWaiting;
        }
        
        public bool IsAnswered() => _isAnswered;

        public void SetAnswered(bool isAnswered)
        {
            _isAnswered = isAnswered;
        }

        public IList<BattleUnitsStackMod> GetMods() => _mods;
    }
}