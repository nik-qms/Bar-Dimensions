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
    class QmsDFQWriter
    {
        static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
        {
            DataSource = ConfigurationManager.AppSettings["gDataSource"],
            UserID = ConfigurationManager.AppSettings["gUserID"],
            Password = ConfigurationManager.AppSettings["gDbPassword"],
            InitialCatalog = ConfigurationManager.AppSettings["gInitialCatalog"]
        };

        static readonly string sqlCache = " INSERT INTO _MAIT_MM_MITTELSTAND " +
                                          " ( SPANR, SZUSINFO20, SDTSERIAL, SPTSERIAL, SFABEZ, " +
                                          " SPRUEFER, SZEICHBEZ, SARTIKELNR, SMANDBEZ, " +
                                          " DM101, DM102, DM103, DM104, DM105, DM106, DM9RMIN, DM9RMAX, DM9RAUSSEN, " +
                                          " DSM101, DSM102, DSM103, DSM104, DSM105, DSM106, DSM9RMIN, DSM9RMAX, DSM9RAUSSEN, DMMANZAHL ) " +
                                          " VALUES(@rmnummer, @auftragsnummer, @dornteil, @platte, @kunde, " +
                                          " @pruefer, @zeichnr, @wznummer, @mandBez, " +
                                          " @m101, @m102, @m103, @m104, @m105, @m106, @m9rmin, @m9rmax, @m9ra, " +
                                          " @sm101, @sm102, @sm103, @sm104, @sm105, @sm106, @sm9rmin, @sm9rmax, @sm9ra, @mmcount )";

        static readonly string sql2dbrCache = " INSERT INTO _MAIT_MM_2DB_MITTELSTAND " +
                                          " ( SPANR, SZUSINFO20, SDTSERIAL, SPTSERIAL, SFABEZ, " +
                                          " SPRUEFER, SZEICHBEZ, SARTIKELNR, SMANDBEZ, " +
                                          " DM101, DM102, DM103, DM104, DM105, DM106, DD1M9RMIN, DD1M9RMAX, DD1M9RAUSSEN, " +
                                          " DM1011, DM1012, DM1013, DM1014, DM1015, DM1016, DD2M9RMIN, DD2M9RMAX, DD2M9RAUSSEN, DM1221, " +
                                          " DSM101, DSM102, DSM103, DSM104, DSM105, DSM106, DD1SM9RMIN, DD1SM9RMAX, DD1SM9RAUSSEN, " +
                                          " DSM1011, DSM1012, DSM1013, DSM1014, DSM1015, DSM1016, DD2SM9RMIN, DD2SM9RMAX, DD2SM9RAUSSEN, DSM1221, DMMANZAHL ) " +
                                          " VALUES(@rmnummer, @auftragsnummer, @dornteil, @platte, @kunde, " +
                                          " @pruefer, @zeichnr, @wznummer, @mandBez, " +
                                          " @m101, @m102, @m103, @m104, @m105, @m106, @d1m9rmin, @d1m9rmax, @d1m9ra, " +
                                          " @m1011, @m1012, @m1013, @m1014, @m1015, @m1016, @d2m9rmin, @d2m9rmax, @d2m9ra, @m1221, " +
                                          " @sm101, @sm102, @sm103, @sm104, @sm105, @sm106, @d1sm9rmin, @d1sm9rmax, @d1sm9ra, " +
                                          " @sm1011, @sm1012, @sm1013, @sm1014, @sm1015, @sm1016, @d2sm9rmin, @d2sm9rmax, @d2sm9ra, @sm1221, @mmcount )";

        internal static void SaveCachetoDB(DataWriterHeader Header, DataWriterLine Line)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(QmsDFQWriter.sqlCache, connection);
                //heftige Kopf list
                ///Anzahl der MM
                command.Parameters.Add("@mmcount", SqlDbType.Decimal);
                command.Parameters["@mmcount"].Value = Header.k0100_anzahlmm;

                command.Parameters.Add("@rmnummer", SqlDbType.NVarChar);
                command.Parameters["@rmnummer"].Value = Header.rueckmeldenummer;

                command.Parameters.Add("@auftragsnummer", SqlDbType.NVarChar);
                command.Parameters["@auftragsnummer"].Value = Header.auftragsnummer;

                command.Parameters.Add("@dornteil", SqlDbType.NVarChar);
                command.Parameters["@dornteil"].Value = Header.k0014_folgenr1;

                command.Parameters.Add("@platte", SqlDbType.NVarChar);
                command.Parameters["@platte"].Value = Header.k0015_folgenr2;

                command.Parameters.Add("@kunde", SqlDbType.NVarChar);
                command.Parameters["@kunde"].Value = Header.k1063_kunde;

                command.Parameters.Add("@pruefer", SqlDbType.NVarChar);
                command.Parameters["@pruefer"].Value = Header.k0008_pruefer;

                command.Parameters.Add("@zeichnr", SqlDbType.NVarChar);
                command.Parameters["@zeichnr"].Value = Header.zeichnungsnummer;

                command.Parameters.Add("@wznummer", SqlDbType.NVarChar);
                command.Parameters["@wznummer"].Value = Header.k1001_werkzeugnr;

                command.Parameters.Add("@mandBez", SqlDbType.NVarChar);
                command.Parameters["@mandBez"].Value = Header.k1082_mandant;

                
                //mm
                command.Parameters.Add("@m101", SqlDbType.Decimal);
                command.Parameters["@m101"].Value = Line.m101;
                //soll
                command.Parameters.Add("@sm101", SqlDbType.Decimal);
                command.Parameters["@sm101"].Value = Line.sm101;
                //mm
                command.Parameters.Add("@m102", SqlDbType.Decimal);
                command.Parameters["@m102"].Value = Line.m102;
                //soll
                command.Parameters.Add("@sm102", SqlDbType.Decimal);
                command.Parameters["@sm102"].Value = Line.sm102;
                //mm
                command.Parameters.Add("@m103", SqlDbType.Decimal);
                command.Parameters["@m103"].Value = Line.m103;
                //soll
                command.Parameters.Add("@sm103", SqlDbType.Decimal);
                command.Parameters["@sm103"].Value = Line.sm103;
                //mm
                command.Parameters.Add("@m104", SqlDbType.Decimal);
                command.Parameters["@m104"].Value = Line.m104;
                //soll
                command.Parameters.Add("@sm104", SqlDbType.Decimal);
                command.Parameters["@sm104"].Value = Line.sm104;
                //mm
                command.Parameters.Add("@m105", SqlDbType.Decimal);
                command.Parameters["@m105"].Value = Line.m105;
                //soll
                command.Parameters.Add("@sm105", SqlDbType.Decimal);
                command.Parameters["@sm105"].Value = Line.sm105;
                //mm
                command.Parameters.Add("@m106", SqlDbType.Decimal);
                command.Parameters["@m106"].Value = Line.m106;
                //soll
                command.Parameters.Add("@sm106", SqlDbType.Decimal);
                command.Parameters["@sm106"].Value = Line.sm106;

                //Stegmaße merkmale
                //mm
                command.Parameters.Add("@m9rmin", SqlDbType.Decimal);
                command.Parameters["@m9rmin"].Value = Line.Rmin;
                //soll
                command.Parameters.Add("@sm9rmin", SqlDbType.Decimal);
                command.Parameters["@sm9rmin"].Value = Line.sRmin;
               
                //mm
                command.Parameters.Add("@m9rmax", SqlDbType.Decimal);
                command.Parameters["@m9rmax"].Value = Line.Rmax;
                //soll
                command.Parameters.Add("@sm9rmax", SqlDbType.Decimal);
                command.Parameters["@sm9rmax"].Value = Line.sRmax;
                
                //mm
                command.Parameters.Add("@m9ra", SqlDbType.Decimal);
                command.Parameters["@m9ra"].Value = Line.Raussen;
                //soll
                command.Parameters.Add("@sm9ra", SqlDbType.Decimal);
                command.Parameters["@sm9ra"].Value = Line.sRaussen;
                
                

                connection.Open();

                command.ExecuteNonQuery();


                
                connection.Close();

            }
        }

        internal static void SaveCachetoDB2DB(DataWriterHeader Header, DataWriterLine Line)
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(QmsDFQWriter.sql2dbrCache, connection);
                //heftige Kopf list
                ///Anzahl der MM
                command.Parameters.Add("@mmcount", SqlDbType.Decimal);
                command.Parameters["@mmcount"].Value = Header.k0100_anzahlmm;

                command.Parameters.Add("@rmnummer", SqlDbType.NVarChar);
                command.Parameters["@rmnummer"].Value = Header.rueckmeldenummer;

                command.Parameters.Add("@auftragsnummer", SqlDbType.NVarChar);
                command.Parameters["@auftragsnummer"].Value = Header.auftragsnummer;

                command.Parameters.Add("@dornteil", SqlDbType.NVarChar);
                command.Parameters["@dornteil"].Value = Header.k0014_folgenr1;

                command.Parameters.Add("@platte", SqlDbType.NVarChar);
                command.Parameters["@platte"].Value = Header.k0015_folgenr2;

                command.Parameters.Add("@kunde", SqlDbType.NVarChar);
                command.Parameters["@kunde"].Value = Header.k1063_kunde;

                command.Parameters.Add("@pruefer", SqlDbType.NVarChar);
                command.Parameters["@pruefer"].Value = Header.k0008_pruefer;

                command.Parameters.Add("@zeichnr", SqlDbType.NVarChar);
                command.Parameters["@zeichnr"].Value = Header.zeichnungsnummer;

                command.Parameters.Add("@wznummer", SqlDbType.NVarChar);
                command.Parameters["@wznummer"].Value = Header.k1001_werkzeugnr;

                command.Parameters.Add("@mandBez", SqlDbType.NVarChar);
                command.Parameters["@mandBez"].Value = Header.k1082_mandant;


                //mm
                command.Parameters.Add("@m101", SqlDbType.Decimal);
                command.Parameters["@m101"].Value = Line.m101;
                //soll
                command.Parameters.Add("@sm101", SqlDbType.Decimal);
                command.Parameters["@sm101"].Value = Line.sm101;
                //mm
                command.Parameters.Add("@m102", SqlDbType.Decimal);
                command.Parameters["@m102"].Value = Line.m102;
                //soll
                command.Parameters.Add("@sm102", SqlDbType.Decimal);
                command.Parameters["@sm102"].Value = Line.sm102;
                //mm
                command.Parameters.Add("@m103", SqlDbType.Decimal);
                command.Parameters["@m103"].Value = Line.m103;
                //soll
                command.Parameters.Add("@sm103", SqlDbType.Decimal);
                command.Parameters["@sm103"].Value = Line.sm103;
                //mm
                command.Parameters.Add("@m104", SqlDbType.Decimal);
                command.Parameters["@m104"].Value = Line.m104;
                //soll
                command.Parameters.Add("@sm104", SqlDbType.Decimal);
                command.Parameters["@sm104"].Value = Line.sm104;
                //mm
                command.Parameters.Add("@m105", SqlDbType.Decimal);
                command.Parameters["@m105"].Value = Line.m105;
                //soll
                command.Parameters.Add("@sm105", SqlDbType.Decimal);
                command.Parameters["@sm105"].Value = Line.sm105;
                //mm
                command.Parameters.Add("@m106", SqlDbType.Decimal);
                command.Parameters["@m106"].Value = Line.m106;
                //soll
                command.Parameters.Add("@sm106", SqlDbType.Decimal);
                command.Parameters["@sm106"].Value = Line.sm106;

                //mm
                command.Parameters.Add("@m1011", SqlDbType.Decimal);
                command.Parameters["@m1011"].Value = Line.m101;
                //soll
                command.Parameters.Add("@sm1011", SqlDbType.Decimal);
                command.Parameters["@sm1011"].Value = Line.sm101;
                //mm
                command.Parameters.Add("@m1012", SqlDbType.Decimal);
                command.Parameters["@m1012"].Value = Line.m102;
                //soll
                command.Parameters.Add("@sm1012", SqlDbType.Decimal);
                command.Parameters["@sm1012"].Value = Line.sm102;
                //mm
                command.Parameters.Add("@m1013", SqlDbType.Decimal);
                command.Parameters["@m1013"].Value = Line.m103;
                //soll
                command.Parameters.Add("@sm1013", SqlDbType.Decimal);
                command.Parameters["@sm1013"].Value = Line.sm103;
                //mm
                command.Parameters.Add("@m1014", SqlDbType.Decimal);
                command.Parameters["@m1014"].Value = Line.m104;
                //soll
                command.Parameters.Add("@sm1014", SqlDbType.Decimal);
                command.Parameters["@sm1014"].Value = Line.sm104;
                //mm
                command.Parameters.Add("@m1015", SqlDbType.Decimal);
                command.Parameters["@m1015"].Value = Line.m105;
                //soll
                command.Parameters.Add("@sm1015", SqlDbType.Decimal);
                command.Parameters["@sm1015"].Value = Line.sm105;
                //mm
                command.Parameters.Add("@m1016", SqlDbType.Decimal);
                command.Parameters["@m1016"].Value = Line.m106;
                //soll
                command.Parameters.Add("@sm1016", SqlDbType.Decimal);
                command.Parameters["@sm1016"].Value = Line.sm106;
                //mm
                command.Parameters.Add("@m1221", SqlDbType.Decimal);
                command.Parameters["@m1221"].Value = Line.m106;
                //soll
                command.Parameters.Add("@sm1221", SqlDbType.Decimal);
                command.Parameters["@sm1221"].Value = Line.sm106;



                //Stegmaße merkmale
                //mm
                command.Parameters.Add("@d1m9rmin", SqlDbType.Decimal);
                command.Parameters["@d1m9rmin"].Value = Line.Rmin;
                //soll
                command.Parameters.Add("@d1sm9rmin", SqlDbType.Decimal);
                command.Parameters["@d1sm9rmin"].Value = Line.sRmin;

                //mm
                command.Parameters.Add("@d1m9rmax", SqlDbType.Decimal);
                command.Parameters["@d1m9rmax"].Value = Line.Rmax;
                //soll
                command.Parameters.Add("@d1sm9rmax", SqlDbType.Decimal);
                command.Parameters["@d1sm9rmax"].Value = Line.sRmax;

                //mm
                command.Parameters.Add("@d1m9ra", SqlDbType.Decimal);
                command.Parameters["@d1m9ra"].Value = Line.Raussen;
                //soll
                command.Parameters.Add("@d1sm9ra", SqlDbType.Decimal);
                command.Parameters["@d1sm9ra"].Value = Line.sRaussen;

                //2DBR
                //mm
                command.Parameters.Add("@d2m9rmin", SqlDbType.Decimal);
                command.Parameters["@d2m9rmin"].Value = Line.Rmin;
                //soll
                command.Parameters.Add("@d2sm9rmin", SqlDbType.Decimal);
                command.Parameters["@d2sm9rmin"].Value = Line.sRmin;

                //mm
                command.Parameters.Add("@d2m9rmax", SqlDbType.Decimal);
                command.Parameters["@d2m9rmax"].Value = Line.Rmax;
                //soll
                command.Parameters.Add("@d2sm9rmax", SqlDbType.Decimal);
                command.Parameters["@d2sm9rmax"].Value = Line.sRmax;

                //mm
                command.Parameters.Add("@d2m9ra", SqlDbType.Decimal);
                command.Parameters["@d2m9ra"].Value = Line.Raussen;
                //soll
                command.Parameters.Add("@d2sm9ra", SqlDbType.Decimal);
                command.Parameters["@d2sm9ra"].Value = Line.sRaussen;

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }
        }

    }
}
