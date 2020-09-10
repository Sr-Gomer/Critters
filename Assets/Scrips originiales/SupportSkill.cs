using System;
using System.Collections.Generic;
using System.Text;

namespace CRITTERS_
{
    class SupportSkill : Skill
    {
        public enum ESuppType
        {
            AtkUp,
            DefUp,
            SpeedDown,
        }

        public ESuppType SuppType { get; private set; }
        public SupportSkill(int name,int affinity, int power, int type) : base(name, power,affinity)
        {
            SetType(type);

        }
        public void SetType(int settedType)
        {
            switch (settedType)
            {
                case (1):
                    SuppType = ESuppType.AtkUp;
                    break;
                case (2):
                    SuppType = ESuppType.DefUp;
                    break;
                case (3):
                    SuppType = ESuppType.SpeedDown;
                    break;

            }
        }
    }
}
