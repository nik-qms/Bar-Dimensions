using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wefa
{
    //1DBR Historie DTO
    public class Dbr1HistorieDto
    {
        public string sdtserial { get; set; }
        public string sptserial { get; set; }
        public decimal dm101 { get; set; }
        public decimal dm102 { get; set; }
        public decimal dm103 { get; set; }
        public decimal dm104 { get; set; }
        public decimal dm105 { get; set; }
        public decimal dm106 { get; set; }
        public decimal dm9rmin { get; set; }
        public decimal dm9rmax { get; set; }
        public decimal dm9raussen { get; set; }
    }


    public class Dbr2HistorieDto
    {
        public string sdtserial { get; set; }
        public string sptserial { get; set; }
        public decimal dm101 { get; set; }
        public decimal dm102 { get; set; }
        public decimal dm103 { get; set; }
        public decimal dm104 { get; set; }
        public decimal dm105 { get; set; }
        public decimal dm106 { get; set; }
        public decimal dm1221 { get; set; }
        public decimal dm1011 { get; set; }
        public decimal dm1012 { get; set; }
        public decimal dm1013 { get; set; }
        public decimal dm1014 { get; set; }
        public decimal dm1015 { get; set; }
        public decimal dm1016 { get; set; }
        public decimal dm9rmin { get; set; }
        public decimal dm9rmax { get; set; }
        public decimal dm9raussen { get; set; }
        public decimal dd2m9rmin { get; set; }
        public decimal dd2m9rmax { get; set; }
        public decimal dd2m9raussen { get; set; }
    }


    // Dornteil und Folge Set
    public class FolgeNummerDto
    {
        public string materialArt { get; set; }
        public string rueckmeldeNummer { get; set; }
        public string folgeNummer { get; set; }      
        
    }

    //DTO seriennummer
    public class SerienNrAbfrageDto
    {
        public string rueckMeldeNummer { get; set; }
        public string werkZeugNummer { get; set; }
        public string auftragsNummer { get; set; }
        public string zeichnungsNummer { get; set; }
        public string kunde { get; set; }
        public string serial { get; set; }
    }

    //DTO Kopfdaten
    public class KopfDatenAbfrageDto
    {
        public string rueckMeldeNummer { get; set; }
        public string werkZeugNummer { get; set; }
        public string auftragsNummer { get; set; }
        public string zeichnungsNummer { get; set; }
        public string kunde { get; set; }
        public string mandant { get; set; }
        public string mmBez { get; set; }
        public string mmNr { get; set; }
        public decimal sollMass { get; set; }
        public decimal neinHeit { get; set; }
        public string szeichBez { get; set; }
        public decimal obertol { get; set; }
        public decimal untertol { get; set; }
    }

    public class HistorieDto
    {                                 
       public string  spanr        {get; set;}
       public string  auftragsnummer {get; set;}
       public string  sdtserial   {get; set;}
       public string  sptserial   {get; set;}
       public string  sfabez      {get; set;}
       public string  spruefer    {get; set;}
       public string  szeichnr    {get; set;}
       public string  sartikelnr  {get; set;}
       public string smandbez     {get; set;}
       public decimal dm101       {get; set;}
       public decimal dm102       {get; set;}
       public decimal dm103       {get; set;}
       public decimal dm104       {get; set;}
       public decimal dm105       {get; set;}
       public decimal dm106       {get; set;}
       public decimal dm9rmin     {get; set;}
       public decimal dm9rmax     {get; set;}
       public decimal dm9raussen  {get; set;}
       public decimal dsm101      {get; set;}
       public decimal dsm102      {get; set;}
       public decimal dsm103      {get; set;}
       public decimal dsm104      {get; set;}
       public decimal dsm105      {get; set;}
       public decimal dsm106      {get; set;}
       public decimal dsm9rmin    {get; set;}
       public decimal dsm9rmax    {get; set;}
       public decimal dsm9raussen {get; set;}
       public decimal dmmanzahl   {get; set;}
       public DateTime dtlaenderung { get; set;}

    }

}
