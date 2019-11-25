using System;
using System.Collections.Generic;

namespace HOMM.BattleObjects
{
    public enum SkillSourceType
    {
        Army,
        Stack
    }
    
    public enum SkillTargetType
    {
        Battle,
        Army,
        Stacks,
        Stack
    }

    public abstract class Skill
    {
        private readonly string _key;
        private readonly SkillSourceType _sourceType;
        private readonly SkillTargetType _targetType;

        public Skill(string key, SkillSourceType sourceType, SkillTargetType targetType)
        {
            _key = key;
            _sourceType = sourceType;
            _targetType = targetType;
        }

        public virtual void Use(BattleArmy source, Battle target)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Use(BattleArmy source, BattleArmy target)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Use(BattleArmy source, IList<BattleUnitsStack> targets)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Use(BattleArmy source, BattleUnitsStack target)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Use(BattleUnitsStack source, Battle target)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Use(BattleUnitsStack source, BattleArmy target)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Use(BattleUnitsStack source, IList<BattleUnitsStack> targets)
        {
            throw new NotImplementedException();
        }
        
        public virtual void Use(BattleUnitsStack source, BattleUnitsStack target)
        {
            throw new NotImplementedException();
        }

        public string GetKey() => _key;
        
        public SkillSourceType GetSourceType() => _sourceType;
        
        public SkillTargetType GetTargetType() => _targetType;
    }
}