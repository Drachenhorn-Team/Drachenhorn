using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DSACharacterSheet.FileReader.Skills;

namespace DSACharacterSheet.FileReader.Calculation
{
    public class Formula : BindableBase
    {
        #region Properties

        [XmlIgnore]
        private string _expression;
        [XmlAttribute("Expression")]
        public string Expression
        {
            get { return _expression; }
            set
            {
                if (_expression == value)
                    return;
                _expression = value;
                OnPropertyChanged("Expression");
            }
        }

        #endregion Properties


        #region Static

        public static double Execute(string expression, CharacterAttributes attributes)
        {
            expression = expression.Replace("Courage", attributes.Courage.CurrentValue.ToString());
            expression = expression.Replace("Wisdom", attributes.Wisdom.CurrentValue.ToString());
            expression = expression.Replace("Intuition", attributes.Intuition.CurrentValue.ToString());
            expression = expression.Replace("Charisma", attributes.Charisma.CurrentValue.ToString());
            expression = expression.Replace("Prestidigitation", attributes.Prestidigitation.CurrentValue.ToString());
            expression = expression.Replace("Finesse", attributes.Finesse.CurrentValue.ToString());
            expression = expression.Replace("Constitution", attributes.Constitution.CurrentValue.ToString());
            expression = expression.Replace("PhysicalStrength", attributes.PhysicalStrength.CurrentValue.ToString());
            expression = expression.Replace("Speed", attributes.Speed.CurrentValue.ToString());


            DataTable dt = new DataTable();
            return (int)dt.Compute(expression, "");
        }

        #endregion Static
    }
}
