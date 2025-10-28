using System;
using System.Collections.Generic;

namespace Attributes
{
    public class AttributeSet
    {
        readonly Dictionary<Attribute, GameplayAttribute> set;

        public AttributeSet()
        {
            set = new Dictionary<Attribute, GameplayAttribute>();
        }

        public void Init(Attribute attribute)
        {
            if (set.ContainsKey(attribute))
            {
                throw new Exception($"<AttributeSet::Init>: Attribute {attribute} already exists.");
            }

            set[attribute] = new GameplayAttribute();
        }

        public void Init(Attribute attribute, float value)
        {
            if (set.ContainsKey(attribute))
            {
                throw new Exception($"<AttributeSet::Init>: Attribute {attribute} already exists.");
            }

            set[attribute] = new GameplayAttribute(value);
        }

        public void Init(Attribute attribute, float baseValue, float additiveValue, float multiplicativeValue)
        {
            if (set.ContainsKey(attribute))
            {
                throw new Exception($"<AttributeSet::Init>: Attribute {attribute} already exists.");
            }

            set[attribute] = new GameplayAttribute(baseValue, additiveValue, multiplicativeValue);
        }

        public GameplayAttribute Get(Attribute attribute)
        {
            if (!set.ContainsKey(attribute))
            {
                throw new Exception($"<AttributeSet::Get>: Attribute {attribute} does not exist.");
            }

            return set[attribute];
        }

        public bool Has(Attribute attribute)
        {
            return set.ContainsKey(attribute);
        }
    }
}
