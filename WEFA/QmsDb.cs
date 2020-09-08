using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Wefa
{
    class QmsDb
    {
        static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
        {
            DataSource = ConfigurationManager.AppSettings["gDataSource"],
            UserID = ConfigurationManager.AppSettings["gUserID"],
            Password = ConfigurationManager.AppSettings["gDbPassword"],
            InitialCatalog = ConfigurationManager.AppSettings["gInitialCatalog"]
        };

        static readonly string sqlKopfDaten = " SELECT DISTINCT" +
                                              " SPA_KOPF.SPANR AS Rückmeldenummer, " +
                                              " ARTIKEL.SARTIKELBEZ AS Werkzeugnummer, " +
                                              " SPA_KOPF.SZUSINFO20 as Auftragsnummer, " +
                                              " ARTIKEL.SZEICHNR as Zeichnungsnummer, " +
                                              " FA_STAMM.SFANR as KUNDE, " +
                                              " MANDANT.SMANDBEZ as MANDANT, " +
                                              " MM_KOPF.SMMBEZ as MMBEZ, " +
                                              " MM_KOPF.SMMNR AS MMNR, " +
                                              " MM_REV.DSOLL AS SOLLMASS, " +
                                              " MM_REV.NEINHEIT, " +
                                              " ARTIKEL.SZEICHBEZ, " +
                                              " MM_REV.DTO AS OT, " +
                                              " MM_REV.DTU AS UT " +
                                              " FROM SPA_KOPF " +
                                              " INNER JOIN SPCIDREF ON SPA_KOPF.NLFDPANR = SPCIDREF.NLFDPANR " +
                                              " INNER JOIN PP_PAB_PP_REV ON PP_PAB_PP_REV.NLFDPABNR = SPCIDREF.NLFDPABNR " +
                                              " INNER JOIN MM_REV ON MM_REV.NLFDMMNR = PP_PAB_PP_REV.NLFDMMNR " +
                                              " AND MM_REV.NLFDMMSPEZNR = PP_PAB_PP_REV.NLFDMMSPEZNR " +
                                              " INNER JOIN MM_KOPF ON MM_REV.NLFDMMNR = MM_KOPF.NLFDMMNR " +
                                              " INNER JOIN PP_KOPF ON SPA_KOPF.NLFDPPLNR = PP_KOPF.NLFDPPLNR " +
                                              " INNER JOIN ARTIKEL ON PP_KOPF.NLFDARTIKELNR = ARTIKEL.NLFDARTIKELNR " +
                                              " INNER JOIN MANDANT ON SPA_KOPF.NLFDMANDNR = MANDANT.NLFDMANDNR  " +
                                              " INNER JOIN ART_FA ON ART_FA.NLFDARTIKELNR = ARTIKEL.NLFDARTIKELNR" +
                                              " INNER JOIN FA_STAMM ON ART_FA.NLFDFANR = FA_STAMM.NLFDFANR " +
                                              " WHERE(SPA_KOPF.SPANR = @spaNummer)";


        static readonly string sqlPlattenDaten = " SELECT  DISTINCT" +
                                                " SPA_KOPF.SPANR," +
                                                " ARTIKEL.SARTIKELBEZ," +
                                                " ART_FA.SZUSINFOFELD1," +
                                                " ARTIKEL.SZEICHNR," +
                                                " FA_STAMM.SFANR," +
                                                " ART_FA.SSERIAL" +
                                                " FROM   MM_REV" +
                                                " INNER JOIN PP_PAB_PP_REV ON MM_REV.NLFDMMNR = PP_PAB_PP_REV.NLFDMMNR" +
                                                " INNER JOIN SPA_KOPF" +
                                                " INNER JOIN ARTIKEL ON SPA_KOPF.NLFDARTIKELNR = ARTIKEL.NLFDARTIKELNR" +
                                                " INNER JOIN PP_REV ON SPA_KOPF.NLFDPPLNR = PP_REV.NLFDPPLNR" +
                                                " INNER JOIN PP_PAB_REF ON PP_REV.NLFDPPLNR = PP_PAB_REF.NLFDPPLNR ON PP_PAB_PP_REV.NLFDPABNR = PP_PAB_REF.NLFDPABNR" +
                                                " INNER JOIN ART_FA ON ART_FA.NLFDARTIKELNR = ARTIKEL.NLFDARTIKELNR" +
                                                " INNER JOIN FA_STAMM ON ART_FA.NLFDFANR = FA_STAMM.NLFDFANR" +
                                                " INNER JOIN MANDANT ON SPA_KOPF.NLFDMANDNR = MANDANT.NLFDMANDNR" +
                                                " INNER JOIN ZUSINFOPA ON SPA_KOPF.NLFDPANR = ZUSINFOPA.NLFDPANR" +
                                                " INNER JOIN MM_KOPF ON MM_REV.NLFDMMNR = MM_KOPF.NLFDMMNR" +
                                                " WHERE (SPA_KOPF.SPANR = @spaNummer) AND (ARTIKEL.SZEICHNR = @sZeichnungsNummer) AND (PP_REV.NREVFLAG ='1') AND (ART_FA.SZUSINFOFELD1 LIKE CONCAT(ZUSINFOPA.SINFO,'%')) ";

        static readonly string sqlSerienDaten = " SELECT  DISTINCT" +
                                                " SPA_KOPF.SPANR," +
                                                " ARTIKEL.SARTIKELBEZ," +
                                                " ART_FA.SZUSINFOFELD1," +
                                                " ARTIKEL.SZEICHNR," +
                                                " FA_STAMM.SFANR," +
                                                " ART_FA.SSERIAL" +
                                                " FROM   MM_REV" +
                                                " INNER JOIN PP_PAB_PP_REV ON MM_REV.NLFDMMNR = PP_PAB_PP_REV.NLFDMMNR" +
                                                " INNER JOIN SPA_KOPF" +
                                                " INNER JOIN ARTIKEL ON SPA_KOPF.NLFDARTIKELNR = ARTIKEL.NLFDARTIKELNR" +
                                                " INNER JOIN PP_REV ON SPA_KOPF.NLFDPPLNR = PP_REV.NLFDPPLNR" +
                                                " INNER JOIN PP_PAB_REF ON PP_REV.NLFDPPLNR = PP_PAB_REF.NLFDPPLNR ON PP_PAB_PP_REV.NLFDPABNR = PP_PAB_REF.NLFDPABNR" +
                                                " INNER JOIN ART_FA ON ART_FA.NLFDARTIKELNR = ARTIKEL.NLFDARTIKELNR" +
                                                " INNER JOIN FA_STAMM ON ART_FA.NLFDFANR = FA_STAMM.NLFDFANR" +
                                                " INNER JOIN MANDANT ON SPA_KOPF.NLFDMANDNR = MANDANT.NLFDMANDNR" +
                                                " INNER JOIN ZUSINFOPA ON SPA_KOPF.NLFDPANR = ZUSINFOPA.NLFDPANR" +
                                                " INNER JOIN MM_KOPF ON MM_REV.NLFDMMNR = MM_KOPF.NLFDMMNR" +
                                                " WHERE (SPA_KOPF.SPANR = @spaNummer) AND (PP_REV.NREVFLAG ='1') AND (ART_FA.SZUSINFOFELD1 LIKE CONCAT(ZUSINFOPA.SINFO,'%')) ";


        static readonly string sqlFolgeNummer = "select ARTIKEL.SMATART, " +
                                                "spa_kopf.spanr, " +
                                                "art_fa.SSERIAL " +                                               
                                                "from ARTIKEL " +
                                                "INNER JOIN art_fa on ART_FA.NLFDARTIKELNR = artikel.NLFDARTIKELNR " +
                                                "INNER JOIN zusinfopa on art_fa.SZUSINFOFELD1 like concat (zusinfopa.SINFO,'%') " +
                                                "INNER JOIN spa_kopf on ZUSINFOPA.NLFDPANR=SPA_KOPF.NLFDPANR " +
                                                "where artikel.SZEICHNR =  @sZeichnungsNummer and spa_kopf.spanr = @spaNummer ";

        static readonly string getCurrentHistory1dbr =  "SELECT SDTSERIAL, SPTSERIAL, " +
                                                        " DM101, DM102, DM103, DM104, DM105, DM106, DM9RMIN, DM9RMAX, DM9RAUSSEN " +
                                                        " FROM _MAIT_MM_MITTELSTAND " +
                                                        " where DTLAENDERUNG = (select MAX(DTLAENDERUNG) from _MAiT_MM_MITTELSTAND where SPANR = @spaNummer and SDTSERIAL = @dorn and SPTSERIAL = @platte)";

        static readonly string getCurrentHistory2dbr = "SELECT sdtserial, SPTSERIAL, DM101, DM102, DM103,  " +
                                                        "DM104, DM105, DM106, DM1221, DM1011, " +
                                                        "DM1012, DM1013, DM1014, DM1015, DM1016, " +
                                                        "DD1M9RMIN, DD1M9RMAX, DD1M9RAUSSEN, DD2M9RMIN, DD2M9RMAX, DD2M9RAUSSEN " +
                                                        "FROM _MAiT_MM_2DB_MITTELSTAND " +
                                                        "WHERE DTLAENDERUNG = (select MAX(DTLAENDERUNG) FROM  _MAiT_MM_2DB_MITTELSTAND where SPANR = @spaNummer and SDTSERIAL = @dorn and SPTSERIAL = @platte)";

        
        internal static Dbr2HistorieDto GetCurrentRecord2Dbr (string rueckmeldenummer, string dornNummer, string platteNummer) 
        {
            Dbr2HistorieDto historieobject = new Dbr2HistorieDto();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(QmsDb.getCurrentHistory2dbr, connection);
                command.Parameters.Add("@spaNummer", SqlDbType.NVarChar);
                command.Parameters["@spaNummer"].Value = rueckmeldenummer;

                command.Parameters.Add("@dorn", SqlDbType.NVarChar);
                command.Parameters["@dorn"].Value = dornNummer;

                command.Parameters.Add("@platte", SqlDbType.NVarChar);
                command.Parameters["@platte"].Value = platteNummer;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ////column
                        if (!reader.IsDBNull(0))
                        {
                            historieobject.sdtserial = reader.GetString(0);
                        }
                        else
                        {
                            historieobject.sdtserial = "NULL";
                        }
                        ////column
                        if (!reader.IsDBNull(1))
                        {
                            historieobject.sptserial = reader.GetString(1);
                        }
                        else
                        {
                            historieobject.sptserial = "NULL";
                        }
                        ////column
                        if (!reader.IsDBNull(2))
                        {
                            historieobject.dm101 = reader.GetDecimal(2);
                        }
                        ////column
                        if (!reader.IsDBNull(3))
                        {
                            historieobject.dm102 = reader.GetDecimal(3);
                        }
                        ////column
                        if (!reader.IsDBNull(4))
                        {
                            historieobject.dm103 = reader.GetDecimal(4);
                        }
                        ////column
                        if (!reader.IsDBNull(5))
                        {
                            historieobject.dm104 = reader.GetDecimal(5);
                        }
                        ////column
                        if (!reader.IsDBNull(6))
                        {
                            historieobject.dm105 = reader.GetDecimal(6);
                        }
                        ////column
                        if (!reader.IsDBNull(7))
                        {
                            historieobject.dm106 = reader.GetDecimal(7);
                        }
                        ////column
                        if (!reader.IsDBNull(8))
                        {
                            historieobject.dm1221 = reader.GetDecimal(8);
                        }
                        ////column
                        if (!reader.IsDBNull(9))
                        {
                            historieobject.dm1011 = reader.GetDecimal(9);
                        }
                        ////column
                        if (!reader.IsDBNull(10))
                        {
                            historieobject.dm1012 = reader.GetDecimal(10);
                        }
                        ////column
                        if (!reader.IsDBNull(11))
                        {
                            historieobject.dm1013 = reader.GetDecimal(11);
                        }
                        ////column
                        if (!reader.IsDBNull(12))
                        {
                            historieobject.dm1014 = reader.GetDecimal(12);
                        }
                        ////column
                        if (!reader.IsDBNull(13))
                        {
                            historieobject.dm1015 = reader.GetDecimal(13);
                        }
                        ////column
                        if (!reader.IsDBNull(14))
                        {
                            historieobject.dm1016 = reader.GetDecimal(14);
                        }
                        ////column
                        if (!reader.IsDBNull(15))
                        {
                            historieobject.dm9rmin = reader.GetDecimal(15);
                        }
                        ////column
                        if (!reader.IsDBNull(16))
                        {
                            historieobject.dm9rmax = reader.GetDecimal(16);
                        }
                        ////column
                        if (!reader.IsDBNull(17))
                        {
                            historieobject.dm9raussen = reader.GetDecimal(17);
                        }
                        ////column
                        if (!reader.IsDBNull(18))
                        {
                            historieobject.dd2m9rmin = reader.GetDecimal(18);
                        }
                        ////column
                        if (!reader.IsDBNull(19))
                        {
                            historieobject.dd2m9rmax = reader.GetDecimal(19);
                        }
                        ////column
                        if (!reader.IsDBNull(20))
                        {
                            historieobject.dd2m9raussen = reader.GetDecimal(20);
                        }
                    }
                }
            }
            return historieobject;
        }

        internal static Dbr1HistorieDto GetCurrentRecord1Dbr(string rueckmeldenummer, string dornNummer, string platteNummer)
        {
            Dbr1HistorieDto historieobject = new Dbr1HistorieDto();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(QmsDb.getCurrentHistory1dbr, connection);
                command.Parameters.Add("@spaNummer", SqlDbType.NVarChar);
                command.Parameters["@spaNummer"].Value = rueckmeldenummer;

                command.Parameters.Add("@dorn", SqlDbType.NVarChar);
                command.Parameters["@dorn"].Value = dornNummer;

                command.Parameters.Add("@platte", SqlDbType.NVarChar);
                command.Parameters["@platte"].Value = platteNummer;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ////column
                        if (!reader.IsDBNull(0))
                        {
                            historieobject.sdtserial = reader.GetString(0);
                        }
                        else
                        {
                            historieobject.sdtserial = "NULL";
                        }
                        ////column
                        if (!reader.IsDBNull(1))
                        {
                            historieobject.sptserial = reader.GetString(1);
                        }
                        else
                        {
                            historieobject.sptserial = "NULL";
                        }
                        ////column
                        if (!reader.IsDBNull(2))
                        {
                            historieobject.dm101 = reader.GetDecimal(2);
                        }
                        ////column
                        if (!reader.IsDBNull(3))
                        {
                            historieobject.dm102 = reader.GetDecimal(3);
                        }
                        ////column
                        if (!reader.IsDBNull(4))
                        {
                            historieobject.dm103 = reader.GetDecimal(4);
                        }
                        ////column
                        if (!reader.IsDBNull(5))
                        {
                            historieobject.dm104 = reader.GetDecimal(5);
                        }
                        ////column
                        if (!reader.IsDBNull(6))
                        {
                            historieobject.dm105 = reader.GetDecimal(6);
                        }
                        ////column
                        if (!reader.IsDBNull(7))
                        {
                            historieobject.dm106 = reader.GetDecimal(7);
                        }
                        ////column
                        if (!reader.IsDBNull(8))
                        {
                            historieobject.dm9rmin = reader.GetDecimal(8);
                        }
                        ////column
                        if (!reader.IsDBNull(9))
                        {
                            historieobject.dm9rmax = reader.GetDecimal(9);
                        }
                        ////column
                        if (!reader.IsDBNull(10))
                        {
                            historieobject.dm9raussen = reader.GetDecimal(10);
                        }
                    }
                }
            }
            return historieobject;
        }

        //abfrage seriendaten
        internal static List<FolgeNummerDto> GetFolgeNummersResultsSet(string rueckmeldenummer, string zeichnungsNummer)
        {
            List<FolgeNummerDto> FolgeNummerList = new List<FolgeNummerDto>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(QmsDb.sqlFolgeNummer, connection);
                command.Parameters.Add("@sZeichnungsNummer", SqlDbType.NVarChar);
                command.Parameters["@sZeichnungsNummer"].Value = zeichnungsNummer;

                command.Parameters.Add("@spaNummer", SqlDbType.NVarChar);                
                command.Parameters["@spaNummer"].Value = rueckmeldenummer;
                

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FolgeNummerDto FolgeNummerObj = new FolgeNummerDto();

                        ///////////
                        if (!reader.IsDBNull(0))
                        {
                            FolgeNummerObj.materialArt = reader.GetString(0);
                        }
                        else
                        {
                            FolgeNummerObj.materialArt = "NULL";
                        }

                        ///////////
                        if (!reader.IsDBNull(1))
                        {
                            FolgeNummerObj.rueckmeldeNummer = reader.GetString(1);
                        }
                        else
                        {
                            FolgeNummerObj.rueckmeldeNummer = "NULL";
                        }

                        //////////
                        if (!reader.IsDBNull(2))
                        {
                            FolgeNummerObj.folgeNummer = reader.GetString(2);
                        }
                        else
                        {
                            FolgeNummerObj.folgeNummer = "NULL";
                        }
                       

                        FolgeNummerList.Add(FolgeNummerObj);

                    }
                }
            }
            return FolgeNummerList;
        }

        //abfrage seriendaten
        internal static List<SerienNrAbfrageDto> GetSerienResultsSet(string rueckmeldenummer)
        {
            List<SerienNrAbfrageDto> SerienDatenList = new List<SerienNrAbfrageDto>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(QmsDb.sqlSerienDaten, connection);
                command.Parameters.Add("@spaNummer", SqlDbType.NVarChar);
                command.Parameters["@spaNummer"].Value = rueckmeldenummer;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SerienNrAbfrageDto serienDtObj = new SerienNrAbfrageDto();

                        ///////////
                        if (!reader.IsDBNull(0))
                        {
                            serienDtObj.rueckMeldeNummer = reader.GetString(0);
                        }
                        else
                        {
                            serienDtObj.rueckMeldeNummer = "NULL";
                        }

                        ///////////
                        if (!reader.IsDBNull(1))
                        {
                            serienDtObj.werkZeugNummer = reader.GetString(1);
                        }
                        else
                        {
                            serienDtObj.werkZeugNummer = "NULL";
                        }

                        //////////
                        if (!reader.IsDBNull(2))
                        {
                            serienDtObj.auftragsNummer = reader.GetString(2);
                        }
                        else
                        {
                            serienDtObj.auftragsNummer = "NULL";
                        }

                        //////////
                        if (!reader.IsDBNull(3))
                        {
                            serienDtObj.zeichnungsNummer = reader.GetString(3);
                        }
                        else
                        {
                            serienDtObj.zeichnungsNummer = "NULL";
                        }

                        ///////////
                        if (!reader.IsDBNull(4))
                        {
                            serienDtObj.kunde = reader.GetString(4);
                        }
                        else
                        {
                            serienDtObj.kunde = "NULL";
                        }

                        ////////////
                        if (!reader.IsDBNull(5))
                        {
                            serienDtObj.serial = reader.GetString(5);
                        }
                        else
                        {
                            serienDtObj.serial = "NULL";
                        }
                        
                        SerienDatenList.Add(serienDtObj);

                    }
                }
            }
            return SerienDatenList;
        }

        //abfrage kopfdaten
        internal static List<KopfDatenAbfrageDto> GetKopfResultsSet(string rueckmeldenummer)
        {
            List<KopfDatenAbfrageDto> KopfDatenList = new List<KopfDatenAbfrageDto>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(QmsDb.sqlKopfDaten, connection);

                command.Parameters.Add("@spaNummer", SqlDbType.NVarChar);
                command.Parameters["@spaNummer"].Value = rueckmeldenummer;

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        KopfDatenAbfrageDto kopfDtObj = new KopfDatenAbfrageDto();

                        ///////////
                        if (!reader.IsDBNull(0))
                        {
                            kopfDtObj.rueckMeldeNummer = reader.GetString(0);
                        }
                        else
                        {
                            kopfDtObj.rueckMeldeNummer = "NULL";
                        }

                        ///////////
                        if (!reader.IsDBNull(1))
                        {
                            kopfDtObj.werkZeugNummer = reader.GetString(1);
                        }
                        else
                        {
                            kopfDtObj.werkZeugNummer = "NULL";
                        }

                        //////////
                        if (!reader.IsDBNull(2))
                        {
                            kopfDtObj.auftragsNummer = reader.GetString(2);
                        }
                        else
                        {
                            kopfDtObj.auftragsNummer = "NULL";
                        }

                        //////////
                        if (!reader.IsDBNull(3))
                        {
                            kopfDtObj.zeichnungsNummer = reader.GetString(3);
                        }
                        else
                        {
                            kopfDtObj.zeichnungsNummer = "NULL";
                        }

                        ///////////
                        if (!reader.IsDBNull(4))
                        {
                            kopfDtObj.kunde = reader.GetString(4);
                        }
                        else
                        {
                            kopfDtObj.kunde = "NULL";
                        }

                        ////////////
                        if (!reader.IsDBNull(5))
                        {
                            kopfDtObj.mandant = reader.GetString(5);
                        }
                        else
                        {
                            kopfDtObj.mandant = "NULL";
                        }

                        ////////////
                        if (!reader.IsDBNull(6))
                        {
                            kopfDtObj.mmBez = reader.GetString(6);
                        }
                        else
                        {
                            kopfDtObj.mmBez = "NULL";
                        }

                        ////////////
                        if (!reader.IsDBNull(7))
                        {
                            kopfDtObj.mmNr = reader.GetString(7);
                        }
                        else
                        {
                            kopfDtObj.mmNr = "NULL";
                        }

                        ////////////
                        if (!reader.IsDBNull(8))
                        {
                            kopfDtObj.sollMass = reader.GetDecimal(8);
                        }
                        else
                        {
                            kopfDtObj.sollMass = -1;
                        }

                        ////////////
                        if (!reader.IsDBNull(9))
                        {
                            kopfDtObj.neinHeit = reader.GetDecimal(9);
                        }
                        else
                        {
                            kopfDtObj.neinHeit = -1;
                        }

                        ////////////
                        if (!reader.IsDBNull(10))
                        {
                            kopfDtObj.szeichBez = reader.GetString(10);
                        }
                        else
                        {
                            kopfDtObj.szeichBez = "NULL";
                        }

                        if (!reader.IsDBNull(11))
                        {
                            kopfDtObj.obertol = reader.GetDecimal(11);
                        }
                        else
                        {
                            kopfDtObj.obertol = -1;
                        }

                        if (!reader.IsDBNull(12))
                        {
                            kopfDtObj.untertol = reader.GetDecimal(12);
                        }
                        else
                        {
                            kopfDtObj.untertol = -1;
                        }

                        KopfDatenList.Add(kopfDtObj);

                    }
                }
            }
            return KopfDatenList;
        }


        
    }
}
