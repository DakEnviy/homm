using System;
using System.Collections.Generic;
using System.Linq;
using HOMM.BattleUnitsStackMods.Turn;
using HOMM.Events;
using HOMM.Events.Attack;
using HOMM.Events.Turn;
using HOMM.Objects;
using HOMM.Utils;
using OneOf;

namespace HOMM.BattleObjects
{
    using SkillTarget = OneOf<Battle, BattleArmy, IList<BattleUnitsStack>, BattleUnitsStack>;

    public enum BattleState
    {
        None,
        InGame,
        Ended
    }

    public class Battle
    {
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

            foreach (var stack in GetStacks())
            {
                stack.DetachMods();
            }
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
                stack.SetDefaultStates();
            }

            _currentStacks = InitiativeUtils.SortStacks(aliveStacks);

            foreach (var stack in _currentStacks)
            {
                stack.Round();
            }

            ++_round;
            
            // Fire after turn event
            EventBus.OnNextTurn(this, new NextTurnEventArgs(this));
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

            _currentStacks = InitiativeUtils.SortStacks(_currentStacks);
            
            // Fire after turn event
            EventBus.OnNextTurn(this, new NextTurnEventArgs(this));
        }

        public IList<BattleUnitsStack> GetStacks() =>
            _attacker.GetStacks().Concat(_target.GetStacks()).ToList();

        public IList<BattleUnitsStack> GetAliveStacks() =>
            _attacker.GetAliveStacks().Concat(_target.GetAliveStacks()).ToList();

        public IList<BattleUnitsStack> GetDeadStacks() =>
            _attacker.GetDeadStacks().Concat(_target.GetDeadStacks()).ToList();

        public bool Attack(BattleUnitsStack target)
        {
            if (_state != BattleState.InGame) return false;
            
            var source = GetCurrentStack();

            if (source.GetArmy() == target.GetArmy()) return false;

            var hitPoints = DamageUtils.CalcDamageHitPoints(source, target);

            // Fire before attack event
            var beforeAttackEventArgs = new BeforeAttackEventArgs(source, target, hitPoints);
            EventBus.OnBeforeAttack(this, beforeAttackEventArgs);
            if (beforeAttackEventArgs.Cancel) return false;

            hitPoints = beforeAttackEventArgs.HitPoints;

            target.Damage(hitPoints);

            // Fire after attack event
            EventBus.OnAfterAttack(this, new AfterAttackEventArgs(source, target, hitPoints));

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
                    EventBus.OnAfterAnswer(this,
                        new AfterAnswerEventArgs(source, target, hitPoints, beforeAnswerEventArgs.IsAnswered));
                }
            }

            NextTurn();
            
            return true;
        }

        public bool UseSkill(Skill skill, SkillTarget target)
        {
            if (_state != BattleState.InGame) return false;
            
            bool result;
            
            switch (skill.GetSourceType())
            {
                case SkillSourceType.Army:
                    result = skill.Use(GetCurrentArmy(), target);
                    break;
                case SkillSourceType.Stack:
                    var stack = GetCurrentStack();

                    if (!stack.GetBaseStack().GetUnit().ContainsSkill(skill))
                    {
                        throw new ArgumentException("Unit doesnt have this skill");
                    }

                    result = skill.Use(stack, target);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skill.GetSourceType), skill.GetSourceType(), "sourceType is broken");
            }

            if (result)
            {
                NextTurn();
            }

            return result;
        }

        public void Wait()
        {
            if (_state != BattleState.InGame) return;
            
            var stack = GetCurrentStack();

            stack.SetWaiting(true);
            _currentStacks.Add(stack);

            NextTurn();
        }

        public void Defend()
        {
            if (_state != BattleState.InGame) return;
            
            var stack = GetCurrentStack();

            stack.AddMod(new BattleUnitsStackModDefend(stack), true);

            NextTurn();
        }

        public void Surrender()
        {
            if (_state != BattleState.InGame) return;
            
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