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
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;
        public SiparisForm(KafeVeri db,Siparis siparis)
        {
            _db = db;
            _siparis = siparis;
            InitializeComponent();
            BilgileriGüncelle();
            UrunleriListele();
        }

        private void UrunleriListele()
        {
            cboUrun.DataSource = _db.Urunler;
        }

        private void BilgileriGüncelle()
        {
            Text = $"Masa{_siparis.MasaNo}";//formun başlığına masa adını ve nosunu yazdırdık
            lblMasaNO.Text = _siparis.MasaNo.ToString("00");//renkli buyük masa no gösteren lbl ye secilen masanoyu yazdık
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;//ödeme tutarını gösteren lblye siparis clasındaki toplamtutartl yi yazdırdık.
            dgvSiparisDetaylar.DataSource = _siparis.SiparisDetaylar.ToList();//datagridin datasource özelliği ile sipariş detaylarını datagiridde gösterd,k. tolist dedik ki her defasında liisteye dönüşsün 
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Urun urun = (Urun)cboUrun.SelectedItem;//combourunlerden seçilen ürünleri urun listemize ekledik
            if (urun==null)//eğer urun boşsa eklekme dedik
            {
                return;
            }

            SiparisDetay sd=_siparis.SiparisDetaylar.FirstOrDefault(x=>x.UrunAd==urun.UrunAd);
            if (sd!=null)
            {
                sd.Adet += (int)nudAdet.Value;
            }
            else
            {
                sd = new SiparisDetay()
                {
                    UrunAd = urun.UrunAd,
                    BirimFiyat = urun.BirimFiyat,
                    Adet = (int)nudAdet.Value,

                };
                _siparis.SiparisDetaylar.Add(sd);

            }
            BilgileriGüncelle();
        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            SiparisiKapat(_siparis.ToplamTutar(), SiparisDurumu.Odendi);

        }

        private void btnSiparisIptal_Click(object sender, EventArgs e)
        {
            SiparisiKapat(0, SiparisDurumu.Iptal);
        }
        private void SiparisiKapat(decimal odenentutar,SiparisDurumu yenidurum)
        {
            _siparis.KapanisZamani = DateTime.Now;
            _siparis.OdenenTutar = odenentutar;
            _siparis.Durum = yenidurum;
            _db.AktifSiparisler.Remove(_siparis);
            _db.GecmisSiparisler.Add(_siparis);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
