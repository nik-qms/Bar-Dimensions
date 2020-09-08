using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wefa
{
    //DFQ Header Data 
    public class DataWriterHeader
    {
        public string rueckmeldenummer { get; set; }
        public string auftragsnummer { get; set; }
        public string zeichnungsnummer { get; set; }
        public decimal k0100_anzahlmm { get; set; }
        public string k1082_mandant { get; set; }
        public string k0014_folgenr1 { get; set; }
        public string k0015_folgenr2 { get; set; }
        public string k1001_werkzeugnr { get; set; }
        public string k0008_pruefer { get; set; }
        public string k1063_kunde { get; set; }
    }

    //DFQ Data Writer Line 
    public class DataWriterLine
    {
        public decimal m101 { get; set; }
        public decimal m102 { get; set; }
        public decimal m103 { get; set; }
        public decimal m104 { get; set; }
        public decimal m105 { get; set; }
        public decimal m106 { get; set; }
        public decimal m1011 { get; set; }
        public decimal m1012 { get; set; }
        public decimal m1013 { get; set; }
        public decimal m1014 { get; set; }
        public decimal m1015 { get; set; }
        public decimal m1016 { get; set; }
        public decimal m1221 { get; set; }
        public decimal Rmin { get; set; }
        public decimal Rmax { get; set; }
        public decimal Raussen { get; set; }
        public decimal Rmin2 { get; set; }
        public decimal Rmax2 { get; set; }
        public decimal Raussen2 { get; set; }
        public decimal sm101 { get; set; }
        public decimal sm102 { get; set; }
        public decimal sm103 { get; set; }
        public decimal sm104 { get; set; }
        public decimal sm105 { get; set; }
        public decimal sm106 { get; set; }
        public decimal sm1011 { get; set; }
        public decimal sm1012 { get; set; }
        public decimal sm1013 { get; set; }
        public decimal sm1014 { get; set; }
        public decimal sm1015 { get; set; }
        public decimal sm1016 { get; set; }
        public decimal sm1221 { get; set; }
        public decimal sRmin { get; set; }
        public decimal sRmax { get; set; }
        public decimal sRaussen { get; set; }
        public decimal sRmin2 { get; set; }
        public decimal sRmax2 { get; set; }
        public decimal sRaussen2 { get; set; }


    }



}
