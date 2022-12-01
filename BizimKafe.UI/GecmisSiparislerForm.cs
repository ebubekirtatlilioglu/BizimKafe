using BizimKafe.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BizimKafe.UI
{
    public partial class GecmisSiparislerForm : Form
    {
        private readonly KafeVeri _db;
        public GecmisSiparislerForm(KafeVeri db)
        {
            _db=db;
            InitializeComponent();
            dgvSiparisler.DataSource = _db.GecmisSiparisler;
        }

        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count==0)
            {
                dgvSiparisDetaylar.DataSource = null;
            }
            else
            {
                DataGridViewRow secilensatir = dgvSiparisler.SelectedRows[0];
                Siparis secilensiparis = (Siparis)secilensatir.DataBoundItem;//tag gibi birşey databoundıtem
                dgvSiparisDetaylar.DataSource = secilensiparis.SiparisDetaylar;
            }
        }

        private void GecmisSiparislerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
