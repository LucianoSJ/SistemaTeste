using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaLoja.Relatorios
{
    public partial class Frm_Rel_Produtos : Form
    {
        public Frm_Rel_Produtos()
        {
            InitializeComponent();
        }

        private void Frm_Rel_Produtos_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
