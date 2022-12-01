using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizimKafe.DATA
{
    public class Siparis
    {
        public int MasaNo { get; set; }
        public SiparisDurumu Durum { get; set; }
        public decimal OdenenTutar { get; set; }
        public DateTime? AcilisZamani { get; set; } = DateTime.Now;
        public DateTime? KapanisZamani { get; set; }
        public List<SiparisDetay> SiparisDetaylar { get; set; } = new List<SiparisDetay>();
        public string ToplamTutarTL { get { return $"{ToplamTutar():c2}"; } }//toplamtutarı tl olarak gösteriyoruz
        public decimal ToplamTutar()
        {
            return SiparisDetaylar.Sum(x=>x.Tutar());//siparis detay içindeki tutar metodundakilerin toplamını bize veriyor.
        }
    }
}
