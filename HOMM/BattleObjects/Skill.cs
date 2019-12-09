using System;
using System.Collections.Generic;
using OneOf;

namespace HOMM.BattleObjects
{
    using SkillSource = OneOf<BattleArmy, BattleUnitsStack>;
    using SkillTarget = OneOf<Battle, BattleArmy, IList<BattleUnitsStack>, BattleUnitsStack>;

    public enum SkillSourceType
    {
        Army,
        Stack
    }

    public enum SkillTargetType
    {
        Battle,
        BattleArmy,
        List,
        Stack
    }

    public abstract class Skill
    {
        private readonly SkillSourceType _sourceType;
        private readonly SkillTargetType _targetType;

        public Skill(SkillSourceType sourceType, SkillTargetType targetType)
        {
            _sourceType = sourceType;
            _targetType = targetType;
        }

        public bool Use(SkillSource source, SkillTarget target)
        {
            return source.Match(
                army => Use(army, target),
                stack => Use(stack, target)
            );
        }

        public bool Use(BattleArmy source, SkillTarget target)
        {
            return target.Match(
                battle => Use(source, battle),
                army => Use(source, army),
                list => Use(source, list),
                stack => Use(source, stack)
            );
        }

        public bool Use(BattleUnitsStack source, SkillTarget target)
        {
            return target.Match(
                battle => Use(source, battle),
                army => Use(source, army),
                list => Use(source, list),
                stack => Use(source, stack)
            );
        }

        public virtual bool Use(BattleArmy source, Battle target) => false;

        public virtual bool Use(BattleArmy source, BattleArmy target) => false;

        public virtual bool Use(BattleArmy source, IList<BattleUnitsStack> targets) => false;

        public virtual bool Use(BattleArmy source, BattleUnitsStack target) => false;

        public virtual bool Use(BattleUnitsStack source, Battle target) => false;

        public virtual bool Use(BattleUnitsStack source, BattleArmy target) => false;

        public virtual bool Use(BattleUnitsStack source, IList<BattleUnitsStack> targets) => false;

        public virtual bool Use(BattleUnitsStack source, BattleUnitsStack target) => false;

        public SkillSourceType GetSourceType() => _sourceType;
        
        public SkillTargetType GetTargetType() => _targetType;
    }
}