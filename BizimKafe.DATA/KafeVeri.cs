using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizimKafe.DATA
{
    public class KafeVeri
    {
        public int MasaAdet { get; set; } = 20;//20 tane masa olsun dedik.
        public List<Urun> Urunler { get; set; } = new List<Urun>();//boş liste verdik, null olmasın diye
        public List<Siparis> AktifSiparisler { get; set; } = new List<Siparis>();
        public List<Siparis> GecmisSiparisler { get; set; } = new List<Siparis>();
    }
}
