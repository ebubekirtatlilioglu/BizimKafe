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
    public partial class UrunlerForm : Form
    {
        private readonly KafeVeri _db;
        public UrunlerForm(KafeVeri db)//constoctorda kafe veriyi çağırdık ki içerisindeki propertyleri kullanabilelim
        {

            InitializeComponent();
            _db = db;

            UrunleriListele();
        }

        private void UrunleriListele()
        {
            dgvUrunler.DataSource = _db.Urunler.ToList();//urunler listesindeki verileri liste şeklinde dgvurunlere ata
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string ad = txtUrunAd.Text.Trim();//txt girilen ürün ad sağ sol boşluklar alınır 
            if (string.IsNullOrEmpty(ad))//eğer boş veya hiç girilmediyse txt 
            {
                MessageBox.Show("ürün adı zorunlu");
                return;
            }
            if (btnEkle.Text == "EKLE")
            {

                _db.Urunler.Add(new Urun()//kafe veri deki urunlere yani urun listesine ekle 
                {
                    UrunAd = ad,
                    BirimFiyat = nudBirimFiyat.Value
                });
                UrunleriListele();//bu eklenenleri dgvurunlerde liste şeklinde göster
                txtUrunAd.Text = "";
                nudBirimFiyat.Value = 0;
            }
            else if (btnEkle.Text == "KAYDET")
            {

                DataGridViewRow satir = dgvUrunler.SelectedRows[0];
                Urun urun = (Urun)satir.DataBoundItem;
                urun.UrunAd = txtUrunAd.Text;
                UrunleriListele();
                btnEkle.Text = "EKLE";
                txtUrunAd.Clear();
                nudBirimFiyat.Value = 0;
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)//data grid deki seçili satır sayısı 0 ise
            {
                MessageBox.Show("önce ürün seçiniz");
                return;//buton clickten çık
            }

            DialogResult dr = MessageBox.Show("seçili ürünü silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr==DialogResult.No)
            {
                return;//metoddan çıkar,metoddan kaçış sağlar yani
            }
            DataGridViewRow satir = dgvUrunler.SelectedRows[0];
            Urun urun = (Urun)satir.DataBoundItem;//datasource kullanılınca data bount item alınır.databounditem tag gibi herşeyi saklar.
            _db.Urunler.Remove(urun);
            UrunleriListele();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            btnEkle.Text = "KAYDET";
            btnIptal.Visible = true;
            DataGridViewRow satir = dgvUrunler.SelectedRows[0];
            Urun urun = (Urun)satir.DataBoundItem;
            txtUrunAd.Text = urun.UrunAd;
            nudBirimFiyat.Value = urun.BirimFiyat;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            btnEkle.Text = "EKLE";
            btnIptal.Visible = false;
            txtUrunAd.Text = "";
            nudBirimFiyat.Value = 0;
        }
    }
}
