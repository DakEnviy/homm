using System.Collections.Generic;
using System.Linq;
using HOMM.Objects;

namespace HOMM.BattleObjects
{
    public enum BattleState
    {
        None,
        InGame,
        Ended
    }

    public class InitiativeComparer : IComparer<BattleUnitsStack>
    {
        private static InitiativeComparer _instance;

        public static InitiativeComparer GetInstance() =>
            _instance ?? (_instance = new InitiativeComparer());
        
        public int Compare(BattleUnitsStack x, BattleUnitsStack y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (x.IsWaiting() && !y.IsWaiting()) return 1;
            if (!x.IsWaiting() && y.IsWaiting()) return -1;

            var delta = x.GetInitiative() - y.GetInitiative();
            var factor = x.IsWaiting() ? 1 : -1;

            var res = Math.Abs(delta) > 0.001F ? delta > 0 ? 1 : -1 : 0;
            if (res == 0) res = x.GetArmy().IsAttacker() ? 1 : -1;

            return res * factor;
        }
    }

    public class Battle
    {
        public static IList<BattleUnitsStack> SortStacks(IList<BattleUnitsStack> stacks) =>
            stacks
                .OrderBy(stack => stack, InitiativeComparer.GetInstance())
                .ToList();

        private readonly Random _random = new Random();

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
                stack.NotDefended();
                stack.NotWaiting();
            }

            _currentStacks = SortStacks(aliveStacks);

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

            var amount = source.GetAmount();
            var (minDamage, maxDamage) = source.GetDamage();
            var attack = source.GetAttack();
            var defence = target.GetDefence();

            var minHitPoints = attack > defence
                ? amount * minDamage * (1 + 0.05 * (attack - defence))
                : amount * minDamage / (1 + 0.05 * (defence - attack));
            var maxHitPoints = attack > defence
                ? amount * maxDamage * (1 + 0.05 * (attack - defence))
                : amount * maxDamage / (1 + 0.05 * (defence - attack));

            var hitPoints = (ushort) Math.Round(minHitPoints + _random.NextDouble() * (maxHitPoints - minHitPoints));

            target.Damage(hitPoints);

            NextTurn();
        }

        public void UseSkill( /* Skill */) {}

        public void Wait()
        {
            var stack = GetCurrentStack();

            stack.Waiting();
            _currentStacks.Add(stack);

            NextTurn();
        }

        public void Defend()
        {
            var stack = GetCurrentStack();

            stack.Defended();
            // TODO: to manager
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