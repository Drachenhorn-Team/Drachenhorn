using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Drachenhorn.Xml.Sheet.Roll;
using Drachenhorn.Xml.Sheet.Skills;

namespace Drachenhorn.Xml.Sheet.CombatInfo.Magic
{
    /// <summary>
    /// Character-Spell
    /// </summary>
    [Serializable]
    public class Spell : Skill
    {
        #region Properties

        [XmlIgnore] private int _cost;

        /// <summary>
        /// The Cost of the Spell
        /// </summary>
        [XmlAttribute("Cost")]
        public int Cost
        {
            get => _cost;
            set
            {
                if (_cost == value)
                    return;
                _cost = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties


        #region Info

        /// <inheritdoc />
        public override Dictionary<string, string> GetInformation()
        {
            var result = base.GetInformation();

            result.Add("%Info.Cost", Cost.ToString());

            return result;
        }

        #endregion Info
    }
}
