using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SICREE
{
    public class UIStyle
    {
        public Color DGVBackColor1 = Color.Black;
        public Color DGVBackColor2 = Color.FromArgb(40, 52, 100);

        public Color DGVForeColor = Color.White;

        public Color DGVBackColorSelecionado = Color.White;

        public Color DGVForeColorSelecionado = Color.Black;

        public void MaxLength(Bunifu.Framework.UI.BunifuMetroTextbox Metro, int Length)
        {
            foreach(Control ctl in Metro.Controls)
            {
                if(ctl.GetType()==typeof(TextBox))
                {
                    var Texto = (TextBox)ctl;
                    Texto.MaxLength = Length;
                }
            }
        }
    }
}
