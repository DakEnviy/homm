using System;
using System.Collections.Generic;
using System.Linq;
using HOMM.Events;
using HOMM.Objects;
using HOMM.Utils;

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
        public static IList<BattleUnitsStack> SortStacks(IList<BattleUnitsStack> stacks) =>
            stacks
                .OrderBy(stack => stack, InitiativeComparer.GetInstance())
                .ToList();

        private readonly BattleArmy _attacker;
        private readonly BattleArmy _target;

        private BattleState _state;
        private int _round;
        private BattleArmy _winner;

        private IList<BattleUnitsStack> _currentStacks;

        public Battle(Army attacker, Army target)
        {
            _attacker = new BattleArmy(attacker, this);
            _target = new BattleArmy(target, this);

            _state = BattleState.None;
            _round = 0;
            _winner = null;

            _currentStacks = null;
        }

        public void Start()
        {
            _state = BattleState.InGame;
            _round = 0;
            _winner = null;
            
            NextRound();
        }

        public void Stop()
        {
            _state = BattleState.Ended;
        }

        public bool TryToStop()
        {
            if (_attacker.GetAliveStacks().Count == 0)
            {
                _winner = _target;
                Stop();

                return true;
            }

            if (_target.GetAliveStacks().Count == 0)
            {
                _winner = _attacker;
                Stop();

                return true;
            }
            
            return false;
        }

        public void NextRound()
        {
            var aliveStacks = GetAliveStacks();

            foreach (var stack in aliveStacks)
            {
                stack.SetDefended(false);
                stack.SetWaiting(false);
                stack.SetAnswered(false);
            }

            _currentStacks = SortStacks(aliveStacks);

            foreach (var stack in _currentStacks)
            {
                stack.Round();
            }

            ++_round;
        }

        public void NextTurn()
        {
            if (TryToStop()) return;
            
            _currentStacks.RemoveAt(0);
            _currentStacks = _currentStacks.Where(stack => stack.IsAlive()).ToList();

            if (_currentStacks.Count == 0)
            {
                NextRound();

                return;
            }

            _currentStacks = SortStacks(_currentStacks);
        }

        public IList<BattleUnitsStack> GetStacks() =>
            _attacker.GetStacks().Concat(_target.GetStacks()).ToList();

        public IList<BattleUnitsStack> GetAliveStacks() =>
            _attacker.GetAliveStacks().Concat(_target.GetAliveStacks()).ToList();

        public IList<BattleUnitsStack> GetDeadStacks() =>
            _attacker.GetDeadStacks().Concat(_target.GetDeadStacks()).ToList();

        public void Attack(BattleUnitsStack target)
        {
            var source = GetCurrentStack();

            if (source.GetArmy() == target.GetArmy())
            {
                throw new InvalidOperationException("You cant attack your own units");
            }

            var hitPoints = DamageUtils.CalcDamageHitPoints(source, target);
            
            // Fire before attack event
            var beforeAttackEventArgs = new BeforeAttackEventArgs(source, target, hitPoints);
            EventBus.OnBeforeAttack(this, beforeAttackEventArgs);
            if (beforeAttackEventArgs.Cancel) return;

            hitPoints = beforeAttackEventArgs.HitPoints;

            target.Damage(hitPoints);

            if (target.IsAlive() && !target.IsAnswered())
            {
                var answerHitPoints = DamageUtils.CalcDamageHitPoints(target, source);
            
                // Fire before answer event
                var beforeAnswerEventArgs = new BeforeAnswerEventArgs(source, target, answerHitPoints);
                EventBus.OnBeforeAnswer(this, beforeAnswerEventArgs);
                
                if (!beforeAnswerEventArgs.Cancel)
                {
                    answerHitPoints = beforeAnswerEventArgs.HitPoints;
                                    
                    source.Damage(answerHitPoints);
                    target.SetAnswered(beforeAnswerEventArgs.IsAnswered);
                    
                    // Fire after answer event
                    EventBus.OnAfterAnswer(this, new AfterAnswerEventArgs(source, target, hitPoints, beforeAnswerEventArgs.IsAnswered));
                }
            }

            // Fire after attack event
            EventBus.OnAfterAttack(this, new AfterAttackEventArgs(source, target, hitPoints));
                
            NextTurn();
        }
        
        public void UseSkill(Skill skill)
        {
            if (skill.GetTargetType() != SkillTargetType.Battle)
            {
                throw new Exception($"Skill have target type: '${skill.GetSourceType()}', but you try to use skill to battle");
            }
            
            switch (skill.GetSourceType())
            {
                case SkillSourceType.Army:
                    skill.Use(GetCurrentArmy(), this);
                    break;
                case SkillSourceType.Stack:
                    skill.Use(GetCurrentStack(), this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void UseSkill(Skill skill, BattleArmy target)
        {
            if (skill.GetTargetType() != SkillTargetType.Army)
            {
                throw new Exception($"Skill have target type: '${skill.GetSourceType()}', but you try to use skill to army");
            }
            
            switch (skill.GetSourceType())
            {
                case SkillSourceType.Army:
                    skill.Use(GetCurrentArmy(), target);
                    break;
                case SkillSourceType.Stack:
                    skill.Use(GetCurrentStack(), target);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void UseSkill(Skill skill, IList<BattleUnitsStack> target)
        {
            if (skill.GetTargetType() != SkillTargetType.Stacks)
            {
                throw new Exception($"Skill have target type: '${skill.GetSourceType()}', but you try to use skill to stacks");
            }
            
            switch (skill.GetSourceType())
            {
                case SkillSourceType.Army:
                    skill.Use(GetCurrentArmy(), target);
                    break;
                case SkillSourceType.Stack:
                    skill.Use(GetCurrentStack(), target);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void UseSkill(Skill skill, BattleUnitsStack target)
        {
            if (skill.GetTargetType() != SkillTargetType.Stack)
            {
                throw new Exception($"Skill have target type: '${skill.GetSourceType()}', but you try to use skill to stack");
            }
            
            switch (skill.GetSourceType())
            {
                case SkillSourceType.Army:
                    skill.Use(GetCurrentArmy(), target);
                    break;
                case SkillSourceType.Stack:
                    skill.Use(GetCurrentStack(), target);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Wait()
        {
            var stack = GetCurrentStack();

            stack.SetWaiting(true);
            _currentStacks.Add(stack);

            NextTurn();
        }

        public void Defend()
        {
            var stack = GetCurrentStack();

            stack.SetDefended(true);
            // TODO: to modificator
            stack.SetDefence((int) (stack.GetDefence() * 1.3));

            NextTurn();
        }

        public void Surrender()
        {
            _winner = GetCurrentArmy() == _attacker ? _target : _attacker;

            Stop();
        }

        public BattleArmy GetAttacker() => _attacker;

        public BattleArmy GetTarget() => _target;

        public BattleState GetBattleState() => _state;

        public int GetRound() => _round;

        public BattleArmy GetWinner() => _winner;

        public IList<BattleUnitsStack> GetCurrentStacks() => _currentStacks;

        public BattleUnitsStack GetCurrentStack() => _currentStacks?[0];

        public BattleArmy GetCurrentArmy() => GetCurrentStack()?.GetArmy();
    }
}