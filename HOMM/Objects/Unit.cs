using System.Collections.Generic;
using System.Linq;
using HOMM.BattleObjects;

namespace HOMM.Objects
{
    public class Unit
    {
        private readonly string _name;
        private readonly int _hitPoints;
        private readonly int _attack;
        private readonly int _defence;
        private readonly (int, int) _damage;
        private readonly float _initiative;
        private readonly ISet<UnitProperty> _properties;

        public Unit(string name, int hitPoints, int attack, int defence, (int, int) damage, float initiative, params UnitProperty[] properties)
        {
            _name = name;
            _hitPoints = hitPoints;
            _attack = attack;
            _defence = defence;
            _damage = damage;
            _initiative = initiative;
            _properties = properties.ToHashSet();
        }

        public ISet<string> GetSkillNames()
            => _properties
                .Select(prop => prop.GetSkill())
                .Where(skillName => skillName != null)
                .ToHashSet();

        public bool ContainsSkill(string skillName)
            => _properties
                .Select(prop => prop.GetSkill())
                .Contains(skillName);

        public bool ContainsSkill(Skill skill)
            => ContainsSkill(skill.GetName());

        public string GetName() => _name;

        public int GetHitPoints() => _hitPoints;

        public int GetAttack() => _attack;

        public int GetDefence() => _defence;

        public (int, int) GetDamage() => _damage;

        public float GetInitiative() => _initiative;

        public ISet<UnitProperty> GetProperties() => _properties;
    }
}