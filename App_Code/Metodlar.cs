using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;



    public class Metodlar
    {

        public SqlConnection baglan()
        {
        SqlConnection baglanti = new SqlConnection("Server=SIBUGPC;Database=izmi-DB41-A;User Id=sas;Password=123; Integrated Security=true;");
        baglanti.Open();
            return (baglanti);
        }
        public SqlConnection baglan2()
        {
        SqlConnection baglanti2 = new SqlConnection("Server=SIBUGPC;Database=izmi-DB41-A;User Id=sas;Password=123; Integrated Security=true;");
        baglanti2.Open();
            return (baglanti2);
        }


	
        public int cmd(string sqlcumle)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand sorgu = new SqlCommand(sqlcumle, baglan);
            int sonuc = 0;

            try
            {
                sonuc = sorgu.ExecuteNonQuery();
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "(" + sqlcumle + ")");
            }

            sorgu.Dispose();
            baglan.Close();
            baglan.Dispose();
            return (sonuc);
        }
        public DataTable GetDataTable(string sqlcumle)
        {
            SqlConnection baglan = this.baglan();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcumle, baglan);
            DataTable dt = new DataTable();

            try
            {
                adapter.Fill(dt);
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "(" + sqlcumle + ")");
            }

            adapter.Dispose();
            baglan.Close();
            baglan.Dispose();
            return dt;
        }
        public DataSet GetDataSet(string sqlcumle)
        {
            SqlConnection baglan = this.baglan();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcumle, baglan);
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds);
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Message + "(" + sqlcumle + ")");
            }

            adapter.Dispose();
            baglan.Close();
            baglan.Dispose();
            return (ds);
        }
        public DataRow GetDataRow(string sqlcumle)
        {
            DataTable table = GetDataTable(sqlcumle);
            if (table.Rows.Count == 0) return null;
            return table.Rows[0];
        }
        public string GetDataCell(string sqlcumle)
        {
            DataTable table = GetDataTable(sqlcumle);
            if (table.Rows.Count == 0) return null;
            return table.Rows[0][0].ToString();
        }
        public float Tarih1eksiTarih2SaatfarkiFloat(string TarihsaaatcumlesiB, string TarihsaaatcumlesiK)
        {
            TimeSpan ts = Convert.ToDateTime(TarihsaaatcumlesiB) - Convert.ToDateTime(TarihsaaatcumlesiK);
            float farktoplamsaat = float.Parse(ts.TotalHours.ToString());
            return farktoplamsaat;
        }
        public DateTime TarihSaatYapDMYhm(string Tarihsaaatcumlesi)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
            DateTime Tarihsaatok = DateTime.Parse(Tarihsaaatcumlesi, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            // result = DateTime.TryParseExact(Tarihsaaatcumlesi, dtFormats, new CultureInfo("tr-TR"), DateTimeStyles.None, out dt);
            //DateTime Tarihsaatok = DateTime.ParseExact(Tarihsaaatcumlesi, "DD.mm.yyyy HH:mm", null);
            return Tarihsaatok;
        }

        public bool IsDate2(string tarihyazi)
        {
            DateTime Temp;
            IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
            if (DateTime.TryParse(tarihyazi, culture, System.Globalization.DateTimeStyles.AssumeLocal, out Temp) == true)
                return true;
            else
                return false;
        }

        public string TarihSaatYaziYapDMYhm(DateTime TarihsaatDMYhms)
        {
            string TarihsaatYaziok,moys,doms,hhs,mms;

            int dayofmonth = TarihsaatDMYhms.Day;
            int monthofyear = TarihsaatDMYhms.Month;
            int yearofdate = TarihsaatDMYhms.Year;
            int saatofnow = TarihsaatDMYhms.Hour;
            int dakikaofnow = TarihsaatDMYhms.Minute;

            if (monthofyear < 10)
            {
                moys = "0" + monthofyear.ToString();
            }
            else
            {
                moys = monthofyear.ToString();
            }

            if (dayofmonth < 10)
            {
                doms = "0" + dayofmonth.ToString();
            }
            else
            {
                doms = dayofmonth.ToString();
            }

            if (saatofnow < 10)
            {
                hhs = "0" + saatofnow.ToString();
            }
            else
            {
                hhs = saatofnow.ToString();
            }

            if (dakikaofnow < 10)
            {
                mms = "0" + dakikaofnow.ToString();
            }
            else
            {
                mms = dakikaofnow.ToString();
            }

            TarihsaatYaziok = doms + "." + moys + "." + yearofdate.ToString() + " " + hhs + ":" + mms;
            
            return TarihsaatYaziok;
        }
        public string TarihYaziYapDMY(DateTime TarihsaatDMYhms)
        {
            string TarihYaziok = TarihSaatYaziYapDMYhm(TarihsaatDMYhms).Substring(0, 10);
            return TarihYaziok;
        }
        public string SaatYaziYapHM(DateTime TarihsaatDMYhms)
        {
            string SaatYaziok = TarihSaatYaziYapDMYhm(TarihsaatDMYhms).Substring(11, 5);
            return SaatYaziok;
        }

		
        public string varidhesapla()
        {
            string varidh = "";
            DateTime varbaslar = new DateTime(2020, 08, 31, 11, 00, 00);
            TimeSpan ts2 = DateTime.Now - varbaslar;
            double farktoplam = ts2.TotalDays;
            double farkmod = farktoplam % 15;

            if (farkmod >= 0 && farkmod < 5)
            {
                varidh = "1";
            }
            else if (farkmod >= 5 && farkmod < 10)
            {
                varidh = "2";
            }
            else if (farkmod >= 10 && farkmod < 15)
            {
                varidh = "3";
            }
            
            return varidh;
        }
        public string varidhesaplaonceki()
        {
            string varidh = "";
            DateTime varbaslar = new DateTime(2020, 08, 31, 11, 00, 00);
            TimeSpan ts2 = DateTime.Now.AddDays(-5) - varbaslar;
            double farktoplam = ts2.TotalDays;
            double farkmod = farktoplam % 15;

            if (farkmod >= 0 && farkmod < 5)
            {
                varidh = "1";
            }
            else if (farkmod >= 5 && farkmod < 10)
            {
                varidh = "2";
            }
            else if (farkmod >= 10 && farkmod < 15)
            {
                varidh = "3";
            }

            return varidh;
        }
        public string varidhesaplaonceki2()
        {
            string varidh = "";
            DateTime varbaslar = new DateTime(2020, 08, 31, 11, 00, 00);
            TimeSpan ts2 = DateTime.Now.AddDays(-10) - varbaslar;
            double farktoplam = ts2.TotalDays;
            double farkmod = farktoplam % 15;

            if (farkmod >= 0 && farkmod < 5)
            {
                varidh = "1";
            }
            else if (farkmod >= 5 && farkmod < 10)
            {
                varidh = "2";
            }
            else if (farkmod >= 10 && farkmod < 15)
            {
                varidh = "3";
            }

            return varidh;
        }

        public string varnohesapla()
        {
            string varnoh = "";
            DateTime varnobasla = varbaslangic();

            string dayofvarnogun = varnobasla.Day.ToString();
            string monthofyearay = varnobasla.Month.ToString();
            string yearofyear = varnobasla.Year.ToString();

            if (Convert.ToInt32(dayofvarnogun) < 10)
            {
                dayofvarnogun = "0" + dayofvarnogun;
            }

            if (Convert.ToInt32(monthofyearay) < 10)
            {
                monthofyearay = "0" + monthofyearay;
            }

            varnoh = (yearofyear).Substring(2, 2) + monthofyearay + dayofvarnogun;

            return varnoh;
        }
        public string varnohesaplaonceki()
        {
            string varnohonceki = "";
            DateTime varnobasla = varbaslangic().AddDays(-5);

            string dayofvarnogun = varnobasla.Day.ToString();
            string monthofyearay = varnobasla.Month.ToString();
            string yearofyear = varnobasla.Year.ToString();

            if (Convert.ToInt32(dayofvarnogun) < 10)
            {
                dayofvarnogun = "0" + dayofvarnogun;
            }

            if (Convert.ToInt32(monthofyearay) < 10)
            {
                monthofyearay = "0" + monthofyearay;
            }

            varnohonceki = (yearofyear).Substring(2, 2) + monthofyearay + dayofvarnogun;

            return varnohonceki;

        }
        public string varnohesaplaonceki2()
        {
            string varnohonceki = "";
            DateTime varnobasla = varbaslangic().AddDays(-10);

            string dayofvarnogun = varnobasla.Day.ToString();
            string monthofyearay = varnobasla.Month.ToString();
            string yearofyear = varnobasla.Year.ToString();

            if (Convert.ToInt32(dayofvarnogun) < 10)
            {
                dayofvarnogun = "0" + dayofvarnogun;
            }

            if (Convert.ToInt32(monthofyearay) < 10)
            {
                monthofyearay = "0" + monthofyearay;
            }

            varnohonceki = (yearofyear).Substring(2, 2) + monthofyearay + dayofvarnogun;

            return varnohonceki;

        }
        public string varnohesaplaonceki3()
        {
            string varnohsfsv = "";
            DateTime varnobasla = varbaslangic().AddDays(-15);

            string dayofvarnogun = varnobasla.Day.ToString();
            string monthofyearay = varnobasla.Month.ToString();
            string yearofyear = varnobasla.Year.ToString();

            if (Convert.ToInt32(dayofvarnogun) < 10)
            {
                dayofvarnogun = "0" + dayofvarnogun;
            }

            if (Convert.ToInt32(monthofyearay) < 10)
            {
                monthofyearay = "0" + monthofyearay;
            }

            varnohsfsv = (yearofyear).Substring(2, 2) + monthofyearay + dayofvarnogun;

            return varnohsfsv;

        }

        public DateTime varbaslangic()
        {
            DateTime varbaslar = new DateTime(2020, 08, 31, 11, 00, 00);

            TimeSpan ts2 = DateTime.Now - varbaslar;
            double farktoplam = ts2.TotalDays;
            double farkmod = farktoplam % 5;
            farktoplam = farktoplam - farkmod;
            varbaslar = varbaslar.AddDays(farktoplam);
            return varbaslar;

        }
        public DateTime varbaslangiconceki()
        {
            DateTime varbaslar = varbaslangic().AddDays(-5);
            return varbaslar;
        }
        public DateTime varbaslangiconceki2()
        {
            DateTime varbaslar = varbaslangic().AddDays(-10);
            return varbaslar;
        }
        public DateTime varbaslangiconceki3()
        {
            DateTime varbaslar = varbaslangic().AddDays(-15);
            return varbaslar;
        }
        public DateTime varbitis()
        {
            DateTime varbiter = varbaslangic().AddDays(5);
            return varbiter;
        }
        public DateTime varbitisonceki()
        {
            DateTime varbiter = varbaslangic();
            return varbiter;
        }
        public DateTime varbitisonceki2()
        {
            DateTime varbiter = varbaslangic().AddDays(-5);
            return varbiter;
        }
        public DateTime varbitisonceki3()
        {
            DateTime varbiter = varbaslangic().AddDays(-10);
            return varbiter;
        }

        public decimal varrealtimesaatbilgiden()
        {
            TimeSpan ts2 = DateTime.Now - varbaslangiconceki();
            decimal farktoplamsaatd = Convert.ToDecimal(ts2.TotalHours);
            return farktoplamsaatd;


        }
        public decimal varrealtimesaat()
        {
            TimeSpan ts2 = DateTime.Now - varbaslangic();
            decimal farktoplamsaatdo = Convert.ToDecimal(ts2.TotalHours);
            return farktoplamsaatdo;

        }
        public decimal varrealtimesaatflex(string baslar)
        {
            TimeSpan ts2 = DateTime.Now - Convert.ToDateTime(baslar);
            decimal farktoplamsaatd = Convert.ToDecimal(ts2.TotalHours);
            return farktoplamsaatd;

        }



        public string Temizle(string Metin)
        {
            string deger = Metin;
            deger = deger.Replace("'", "");
            deger = deger.Replace("<", "");
            deger = deger.Replace(">", "");
            deger = deger.Replace("&", "");
            deger = deger.Replace("[", "");
            deger = deger.Replace("]", "");
            deger = deger.Replace(";", "");
            deger = deger.Replace("?", "");
            deger = deger.Replace("%", "");
            deger = deger.Replace("!", "");
            return deger;
        }
        public string TemizleNokta2NoktaliRakam(string Metin)
        {
            string deger = kucukharf(Metin);
            deger = deger.Replace("'", "");
            deger = deger.Replace("-", "");
            deger = deger.Replace("+", "");
            deger = deger.Replace("*", "");
            deger = deger.Replace("/", "");
            deger = deger.Replace("_", "");
            deger = deger.Replace("$", "");
            deger = deger.Replace("{", "");
            deger = deger.Replace("}", "");
            deger = deger.Replace("=", "");
            deger = deger.Replace(")", "");
            deger = deger.Replace("(", "");
            deger = deger.Replace("#", "");
            deger = deger.Replace("^", "");
            deger = deger.Replace("@", "");
            deger = deger.Replace("|", "");
            deger = deger.Replace("~", "");
            deger = deger.Replace("¨", "");
            deger = deger.Replace(" ", "");


            deger = deger.Replace("<", "");
            deger = deger.Replace(">", "");
            deger = deger.Replace("&", "");
            deger = deger.Replace("[", "");
            deger = deger.Replace("]", "");
            deger = deger.Replace(";", "");
            deger = deger.Replace("?", "");
            deger = deger.Replace("%", "");
            deger = deger.Replace("!", "");

            deger = deger.Replace("a", "");
            deger = deger.Replace("b", "");
            deger = deger.Replace("c", "");
            deger = deger.Replace("ç", "");
            deger = deger.Replace("d", "");
            deger = deger.Replace("e", "");
            deger = deger.Replace("f", "");
            deger = deger.Replace("g", "");
            deger = deger.Replace("ğ", "");
            deger = deger.Replace("h", "");
            deger = deger.Replace("ı", "");
            deger = deger.Replace("i", "");
            deger = deger.Replace("j", "");
            deger = deger.Replace("k", "");
            deger = deger.Replace("l", "");
            deger = deger.Replace("m", "");
            deger = deger.Replace("n", "");
            deger = deger.Replace("o", "");
            deger = deger.Replace("ö", "");
            deger = deger.Replace("p", "");
            deger = deger.Replace("q", "");
            deger = deger.Replace("r", "");
            deger = deger.Replace("s", "");
            deger = deger.Replace("ş", "");
            deger = deger.Replace("t", "");
            deger = deger.Replace("u", "");
            deger = deger.Replace("ü", "");
            deger = deger.Replace("v", "");
            deger = deger.Replace("w", "");
            deger = deger.Replace("x", "");
            deger = deger.Replace("y", "");
            deger = deger.Replace("z", "");

            return deger;
        }
        public string TemizleRakamoldu(string Metin)
        {
            string deger = kucukharf(Metin);
            deger = deger.Replace("'", "");
            deger = deger.Replace("-", "");
            deger = deger.Replace("+", "");
            deger = deger.Replace("*", "");
            deger = deger.Replace("/", "");
            deger = deger.Replace("_", "");
            deger = deger.Replace("$", "");
            deger = deger.Replace("{", "");
            deger = deger.Replace("}", "");
            deger = deger.Replace("=", "");
            deger = deger.Replace(")", "");
            deger = deger.Replace("(", "");
            deger = deger.Replace("#", "");
            deger = deger.Replace("^", "");
            deger = deger.Replace("@", "");
            deger = deger.Replace("|", "");
            deger = deger.Replace("~", "");
            deger = deger.Replace("¨", "");
            deger = deger.Replace(" ", "");
            deger = deger.Replace(".", "");
            deger = deger.Replace(":", "");

            deger = deger.Replace("<", "");
            deger = deger.Replace(">", "");
            deger = deger.Replace("&", "");
            deger = deger.Replace("[", "");
            deger = deger.Replace("]", "");
            deger = deger.Replace(";", "");
            deger = deger.Replace("?", "");
            deger = deger.Replace("%", "");
            deger = deger.Replace("!", "");

            deger = deger.Replace("a", "");
            deger = deger.Replace("b", "");
            deger = deger.Replace("c", "");
            deger = deger.Replace("ç", "");
            deger = deger.Replace("d", "");
            deger = deger.Replace("e", "");
            deger = deger.Replace("f", "");
            deger = deger.Replace("g", "");
            deger = deger.Replace("ğ", "");
            deger = deger.Replace("h", "");
            deger = deger.Replace("ı", "");
            deger = deger.Replace("i", "");
            deger = deger.Replace("j", "");
            deger = deger.Replace("k", "");
            deger = deger.Replace("l", "");
            deger = deger.Replace("m", "");
            deger = deger.Replace("n", "");
            deger = deger.Replace("o", "");
            deger = deger.Replace("ö", "");
            deger = deger.Replace("p", "");
            deger = deger.Replace("q", "");
            deger = deger.Replace("r", "");
            deger = deger.Replace("s", "");
            deger = deger.Replace("ş", "");
            deger = deger.Replace("t", "");
            deger = deger.Replace("u", "");
            deger = deger.Replace("ü", "");
            deger = deger.Replace("v", "");
            deger = deger.Replace("w", "");
            deger = deger.Replace("x", "");
            deger = deger.Replace("y", "");
            deger = deger.Replace("z", "");
            deger = deger.Trim();
            return deger;
        }
        public string Altcizgisil(string Metin)
        {
            string deger = Metin;
            deger = deger.Replace("_", "");
            return deger;
        }
        public string ilkharfbuyuk(string Metin)
    	{
            if (string.IsNullOrEmpty(Metin)) return Metin;
            char[] dizi = Metin.ToLower().ToCharArray();
	        dizi[0] = char.ToUpper(dizi[0]);
	        return new string(dizi);
        }
        public string kucukharf(string Metin)
        {
            if (string.IsNullOrEmpty(Metin)) return Metin;
            char[] dizi = Metin.ToLower().ToCharArray();

            return new string(dizi);
        }
        public string Pcipal()
        {
            var context = System.Web.HttpContext.Current;
            string ip = String.Empty;

            if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                ip = context.Request.UserHostAddress;

            if (ip == "::1")
                ip = "127.0.0.1";

            return ip;
        }




        public string[] tpphesapla(string grt, string tip, string loa, string bowt, string strnt, string tehlikeliyuk, string kalkisrihtimi, string yanasmarihtimi)
        {
            string tpp = "0";
            string romsayisi = "0";
            string romsayisindirim = "0";
            double tppisiz = 0;
            double tppin = 0;
            int grti = Convert.ToInt32(grt);
            int loai = Convert.ToInt32(loa);
            int bowi = Convert.ToInt32(bowt);
            int sterni = Convert.ToInt32(strnt);
            kalkisrihtimi = kalkisrihtimi + "uzatma";
            yanasmarihtimi = yanasmarihtimi + "uzatma";

            if (Convert.ToInt32(grt) < 2000)
        {
            tpp = "0";
            romsayisi = "0";
        }
        else if (tehlikeliyuk.ToLower() == "yes" && (tip.ToLower() == "tanker" || tip.ToLower() == "lpg" || tip.ToLower() == "lng"))
            {
                //dg li tanker de thrs indirimi yok
                tpp = tppindirimsiz(grt, tip);
                romsayisi = tppromsayisi(grt, tip);
            }
            else if (loai > 200)
            {
                //200.01 ve üstü gemiler de thrs indirimi yok
                tpp = tppindirimsiz(grt, tip);
            romsayisi = tppromsayisi(grt,tip);
        }
            else if (kalkisrihtimi.ToLower().Substring(0, 4) == "dock")
            {
                //tersanelerde havuza giren çıkan gemilerde de thrs indirimi yok
                tpp = tppindirimsiz(grt, tip);
            romsayisi = tppromsayisi(grt,tip);
        }
            else if (yanasmarihtimi.ToLower().Substring(0, 4) == "dock")
            {
                //tersanelerde havuza giren çıkan gemilerde de thrs indirimi yok
                tpp = tppindirimsiz(grt, tip);
            romsayisi = tppromsayisi(grt,tip);
        }

        else if (bowt == "0")
            {
                // bow thrs bozuksa indirimi yok
                tpp = tppindirimsiz(grt, tip);
            romsayisi = tppromsayisi(grt,tip);
        }

        // else if( yolcu gemisi belgelerini ibraz etmediyse )
        // {   tpp = tppindirimsiz(grt, tip);
        // }   55-125 ise enaz 30t, 126-200 ise enaz 45t, 201-300 ise enaz 60t, 300 üzeri ise enaz 90t,  
        else  // indirimi yap
            {
                tppisiz = Convert.ToDouble(tppindirimsiz(grt, tip));
                tppin = Convert.ToDouble(tppindirim(grt, bowt, strnt));

                double tpphesap = (tppisiz - (tppisiz * tppin / 10));

                tpp = Math.Round(tpphesap).ToString();
                if (Math.Round(tpphesap) < 16) { tpp = "16"; }

            //int tppih = Convert.ToInt32(Math.Floor(Math.Abs(tpphesap)));
            //if (tppih < 16) { tpp = "16"; }
            //else
            //{
                    //tpp = (tpphesap + 0.0001).ToString();
                    //tpp = tpp.Substring(0, 4);
            //  }

            romsayisindirim = tppindirimsayisi(grt, bowt, strnt);
            romsayisi = (Convert.ToInt32(tppromsayisi(grt,tip)) - Convert.ToInt32(romsayisindirim)).ToString();
        }

            return new string[2] { tpp, romsayisi};

    }
        public string tppindirimsiz(string grt, string tip)
        {
            //2000 5000 ise             16 ton
            //5001 15000 ise            32 ton
            //15001 30000 ise           60 ton
            //30001 45000 ise           75 ton
            //kuruyuk 45001 ve üstü     90 ton 
            //tanker 45001 75000 ise    90 ton
            //tanker 75001 ve üstü ise 120 ton
            //lpg,lng 45000 üstü       150 ton
            string sonuc = "0";

            if (Convert.ToInt32(grt) > 1999 && Convert.ToInt32(grt) < 5001)
            {
                sonuc = "16";
            }
            else if (Convert.ToInt32(grt) > 5000 && Convert.ToInt32(grt) < 15001)
            {
                sonuc = "32";
            }
            else if (Convert.ToInt32(grt) > 15000 && Convert.ToInt32(grt) < 30001)
            {
                sonuc = "60";
            }
            else if (Convert.ToInt32(grt) > 30000 && Convert.ToInt32(grt) < 45001)
            {
                sonuc = "75";
            }
            else if (Convert.ToInt32(grt) > 45000  && (tip.ToLower() != "tanker" && tip.ToLower() != "lpg" && tip.ToLower() != "lng"))
            {
                sonuc = "90"; // 2 rom
        }
            else if (Convert.ToInt32(grt) > 45000 && Convert.ToInt32(grt) < 75001 && (tip.ToLower() == "tanker" || tip.ToLower() == "lpg" ))
            {
                sonuc = "90"; // 3 rom
            }
            else if (Convert.ToInt32(grt) > 75000 && (tip.ToLower() == "tanker" || tip.ToLower() == "lpg"))
            {
                sonuc = "120"; // 3 rom
        }
            else if (Convert.ToInt32(grt) > 45000 && (tip.ToLower() == "lng"))
            {
                sonuc = "150"; // 3 rom
        }
            else sonuc = "0";

            return sonuc;
        }
        public int tppindirim(string grt, string bowt, string strnt)
        {
        //B+K toplamda %40 indirim yapar
        //B  sadece %30 indirim yapar


        //5-10  375 kw
        //10-15 750 kw
        //15-30 1000 kw
        //30-45 1250 kw
        //45-60 1500 kw
        //60++  1750 kw

        //indirim sonucu 1 Romorkorun altına düşülemez. değer 16 ton un altında ise 16 yazmalı.
        //tpp	B%30	BS%40
        //32	22,4	19,2
        //60	42	    36
        //75	52,5	45
        //90	63	    54
        //90T	xxx
        //120T	xxx

        int sonuc = 0;
            int bowi = Convert.ToInt32(bowt);
            int sterni = Convert.ToInt32(strnt);

            if (Convert.ToInt32(grt) > 5000 && Convert.ToInt32(grt) <= 10000)
            {
                if (sterni > 0 && (bowi + sterni) >= 375) { sonuc = 4; }
                else if ((bowi) >= 375) { sonuc = 3; }
                else { sonuc = 0; }
            }
            else if (Convert.ToInt32(grt) > 10000 && Convert.ToInt32(grt) <= 15000)
            {
                if (sterni > 0 && (bowi + sterni) >= 750) { sonuc = 4; }
                else if ((bowi) >= 750) { sonuc = 3; }
                else { sonuc = 0; }
            }
            else if (Convert.ToInt32(grt) > 15000 && Convert.ToInt32(grt) <= 30000)
            {
                if (sterni > 0 && (bowi + sterni) >= 1000) { sonuc = 4; }
                else if ((bowi) >= 1000) { sonuc = 3; }
                else { sonuc = 0; }
            }
            else if (Convert.ToInt32(grt) > 30000 && Convert.ToInt32(grt) <= 45000)
            {
                if (sterni > 0 && (bowi + sterni) >= 1250) { sonuc = 4; }
                else if ((bowi) >= 1250) { sonuc = 3; }
                else { sonuc = 0; }
            }
            else if (Convert.ToInt32(grt) > 45000 && Convert.ToInt32(grt) <= 60000)
            {
                if (sterni > 0 && (bowi + sterni) >= 1500) { sonuc = 4; }
                else if ((bowi) >= 1500) { sonuc = 3; }
                else { sonuc = 0; }
            }
            else if (Convert.ToInt32(grt) > 60000)
            {
                if (sterni > 0 && (bowi + sterni) >= 1750) { sonuc = 4; }
                else if ((bowi) >= 1750) { sonuc = 3; }
                else { sonuc = 0; }
            }

            return sonuc;
        }	
        public string tppindirimsayisi(string grt, string bowt, string strnt)
        {

            int bowi = Convert.ToInt32(bowt);
            int sterni = Convert.ToInt32(strnt);
            int bost = bowi + sterni;
            string indirim = "0";

            if (Convert.ToInt32(grt) > 5000 && Convert.ToInt32(grt) <= 10000)
            {
                if ((bost) >= 375) { indirim = "1"; }
            }
            else if (Convert.ToInt32(grt) > 10000 && Convert.ToInt32(grt) <= 15000)
            {
                if ((bost) >= 750) { indirim = "1"; }
            }
            else if (Convert.ToInt32(grt) > 15000 && Convert.ToInt32(grt) <= 30000)
            {
                if ((bost) >= 1000) { indirim = "1"; }
                }
            else if (Convert.ToInt32(grt) > 30000 && Convert.ToInt32(grt) <= 45000)
            {
                if ((bost) >= 1250) { indirim = "1"; }
            }
            else if (Convert.ToInt32(grt) > 45000 && Convert.ToInt32(grt) <= 60000)
            {
                if ((bost) >= 1500) { indirim = "1"; }
            }
            else if (Convert.ToInt32(grt) > 60000)
            {
                if ((bost) >= 1750) { indirim = "1"; }
            }


            return indirim;
        }
        public string tppromsayisi(string grt, string tip)
    { // bilgi: tpp,    draft:tug sayısı

        string romsay = "0";

        if (Convert.ToInt32(grt) < 2000)
        {
            romsay = "0";
        }
        else if(Convert.ToInt32(grt) > 1999 && Convert.ToInt32(grt) < 5001)
        {
            romsay = "1";
        }
        else if (Convert.ToInt32(grt) > 5000 && Convert.ToInt32(grt) < 15001)
        {
            romsay = "2";
        }
        else if (Convert.ToInt32(grt) > 15000 && Convert.ToInt32(grt) < 30001)
        {
            romsay = "2";
        }
        else if (Convert.ToInt32(grt) > 30000 && Convert.ToInt32(grt) < 45001)
        {
            romsay = "2";
        }
        else if (Convert.ToInt32(grt) > 45000 && (tip.ToLower() != "tanker" && tip.ToLower() != "lpg" && tip.ToLower() != "lng"))
        {
            romsay = "2";
        }
        else if (Convert.ToInt32(grt) > 45000 && Convert.ToInt32(grt) < 75001 && (tip.ToLower() == "tanker" || tip.ToLower() == "lpg"))
        {
            romsay = "3"; 
        }
        else if (Convert.ToInt32(grt) > 75000 && (tip.ToLower() == "tanker" || tip.ToLower() == "lpg"))
        {
            romsay = "3"; 
        }
        else if (Convert.ToInt32(grt) > 45000 && (tip.ToLower() == "lng"))
        {
            romsay = "3"; 
        }
        else romsay = "0";

        return romsay;
    }




        public bool MailSending(string Subject, string Body)
        {
             try
            {
                MailMessage mesaj = new MailMessage();

                mesaj.From = new MailAddress("info@monitoringpilot.com");
                mesaj.To.Add(new MailAddress("dekasmonitoringpilot@gmail.com"));
                mesaj.To.Add(new MailAddress("daricapilot@dekaspilot.com"));
                mesaj.To.Add(new MailAddress("yarimcapilot@ankaspilot.com"));
                mesaj.To.Add(new MailAddress("akolcuoglu@dekaspilot.com"));
                mesaj.To.Add(new MailAddress("operasyon@marintug.com"));
                mesaj.To.Add(new MailAddress("vardiyakaptani@marintug.com"));
                mesaj.To.Add(new MailAddress("info@marintug.com"));
                mesaj.To.Add(new MailAddress("izmitliman@sanmar.com.tr"));
                mesaj.To.Add(new MailAddress("operasyon@sanmar.com.tr"));
                mesaj.To.Add(new MailAddress("emregurel@sanmar.com.tr"));
                mesaj.To.Add(new MailAddress("cemseven@sanmar.com.tr"));
                mesaj.To.Add(new MailAddress("sinanseven@sanmar.com.tr"));
//                mesaj.To.Add(new MailAddress("aligurun@sanmar.com.tr"));
                mesaj.Subject = Subject;// Mailinizin Konusunu, başlığını giriyorsunuz			
                mesaj.Body = Body;	// Göndereceğiniz mailin içeriğini girin, IsBodyHtml = true yaptıysanız html etiketleri ok
                mesaj.IsBodyHtml = true;// Mail içeriğinde html kullanılacaksa true, mail içereğinde htmli engellemek için false giriniz.
                mesaj.BodyEncoding = System.Text.Encoding.UTF8;//bu da olabilir : mesaj.BodyEncoding = UTF8Encoding.UTF8;
                mesaj.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                string host = "smtp.gmail.com";
                string smtpUser = "info@monitoringpilot.com";
                string smtpPassword = "Dekas1996!!";

                SmtpClient smtp = new SmtpClient(host, 25);// Genelde mail.domain.com şeklinde olan smtp mail sunucu adresinizi girmelisiniz.
                smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);  // mail adresinizi ve şifrenizi giriyorsunuz 		//	smtp.UseDefaultCredentials = false;
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.EnableSsl = true; // Sunucunuz mail göndermek için ssl gerektiriyorsa true, gerektirmiyorsa false girin. 
                smtp.Send(mesaj);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

		
        public bool MailSendinglive(string Subject, string Body)
        {
            try
            {
                MailMessage mesaj = new MailMessage();

                mesaj.From = new MailAddress("info@monitoringpilot.com");
                mesaj.To.Add(new MailAddress("info@monitoringpilot.com"));
                mesaj.To.Add(new MailAddress("dekasmonitoringpilot@gmail.com"));
                mesaj.Subject = Subject;// Mailinizin Konusunu, başlığını giriyorsunuz			
                mesaj.Body = Body;	// Göndereceğiniz mailin içeriğini girin, IsBodyHtml = true yaptıysanız html etiketleri ok
                mesaj.IsBodyHtml = true;// Mail içeriğinde html kullanılacaksa true, mail içereğinde htmli engellemek için false giriniz.
                mesaj.BodyEncoding = System.Text.Encoding.UTF8;//bu da olabilir : mesaj.BodyEncoding = UTF8Encoding.UTF8;
                mesaj.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                string host = "smtp.gmail.com";
                string smtpUser = "info@monitoringpilot.com";
                string smtpPassword = "Dekas1996!!";

                SmtpClient smtp = new SmtpClient(host, 25);// Genelde mail.domain.com şeklinde olan smtp mail sunucu adresinizi girmelisiniz.
                smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);  // mail adresinizi ve şifrenizi giriyorsunuz 		//	smtp.UseDefaultCredentials = false;
                smtp.Timeout = 10000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.EnableSsl = true; // Sunucunuz mail göndermek için ssl gerektiriyorsa true, gerektirmiyorsa false girin. 
                smtp.Send(mesaj);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

		public bool Mailtoone(string toadres, string Subject, string Body)
    {
        try
        {
            MailMessage mesaj = new MailMessage();

            mesaj.From = new MailAddress("info@monitoringpilot.com");
            mesaj.To.Add(new MailAddress(toadres));
            mesaj.Subject = Subject;// Mailinizin Konusunu, başlığını giriyorsunuz			
            mesaj.Body = Body;  // Göndereceğiniz mailin içeriğini girin, IsBodyHtml = true yaptıysanız html etiketleri ok
            mesaj.IsBodyHtml = true;// Mail içeriğinde html kullanılacaksa true, mail içereğinde htmli engellemek için false giriniz.
            mesaj.BodyEncoding = System.Text.Encoding.UTF8;//bu da olabilir : mesaj.BodyEncoding = UTF8Encoding.UTF8;
            mesaj.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            string host = "smtp.gmail.com";
            string smtpUser = "info@monitoringpilot.com";
            string smtpPassword = "Dekas1996!!";

            SmtpClient smtp = new SmtpClient(host, 25);// Genelde mail.domain.com şeklinde olan smtp mail sunucu adresinizi girmelisiniz.
            smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);  // mail adresinizi ve şifrenizi giriyorsunuz 		//	smtp.UseDefaultCredentials = false;
            smtp.Timeout = 10000;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.EnableSsl = true; // Sunucunuz mail göndermek için ssl gerektiriyorsa true, gerektirmiyorsa false girin. 
            smtp.Send(mesaj);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

		
       public bool KayitTarihCek(string kayittarihkontrol)
        {
            string eta = kayittarihkontrol;

            if (eta == "" || eta == null || eta == "__.__.____ __:__")
            {
                return false;
            }
            else if (IsDate2(eta) != true)
            {
                return false;
            }
            else if (Convert.ToDateTime(eta) < DateTime.Now.AddMinutes(-46))
            {
                return false;
            }
            else if (Convert.ToDateTime(eta) > DateTime.Now.AddMinutes(16))
            {
                return false;
            }


            else
            {
                return true;
            }
        }	
		
        public void logrecup(string Sessionkapno)
        {
            SqlConnection baglanti = baglan();

            SqlCommand logintime = new SqlCommand("SP_Onof_logintime", baglanti);
            logintime.CommandType = CommandType.StoredProcedure;
            logintime.Parameters.AddWithValue("@sessionkapno", Sessionkapno);
            logintime.Parameters.Add("@sonuc", SqlDbType.VarChar, 16);
            logintime.Parameters["@sonuc"].Direction = ParameterDirection.Output;
            logintime.ExecuteNonQuery();

            if (string.IsNullOrEmpty(logintime.Parameters["@sonuc"].Value.ToString()) != true)
            {
                string logontime = logintime.Parameters["@sonuc"].Value.ToString();
                decimal farktime = Convert.ToDecimal(Tarih1eksiTarih2SaatfarkiFloat(DateTime.Now.ToString(), logontime));
                string cikzamani = TarihSaatYaziYapDMYhm(DateTime.Now);

                SqlCommand cmdisnedup = new SqlCommand("SP_Up_Onof_offandtime", baglanti);
                cmdisnedup.CommandType = CommandType.StoredProcedure;
                cmdisnedup.Parameters.AddWithValue("@sessionkapno", Sessionkapno);
                cmdisnedup.Parameters.AddWithValue("@logof", cikzamani);
                cmdisnedup.Parameters.AddWithValue("@time", farktime);
                cmdisnedup.ExecuteNonQuery();
                cmdisnedup.Dispose();

                return;
            }
            logintime.Dispose();
            baglanti.Close();

        }


        //public void hattamiyim(string Sessionkapno)
        //{
        //        SqlConnection baglanti = baglan();
        //        SqlCommand cmdtwicebak = new SqlCommand("Select pcip from onoffline where (kapno='" + Sessionkapno + "' and time = 0.001) ", baglanti);

        //        if (cmdtwicebak.ExecuteScalar() != null)
        //        {
        //            string ilkofip = cmdtwicebak.ExecuteScalar().ToString();

        //            if (ilkofip != Pcipal().ToString()) // userın ilk kaydı kontrol ediliyor eğer kullanıcı ip farklı ise ilkini sil
        //            {
        //                SqlCommand cmdlogofip = new SqlCommand("Select id from onoffline where kapno='" + Sessionkapno + "' and time = 0.001 ", baglanti);
        //                if (cmdlogofip.ExecuteScalar() != null)
        //                {
        //                    int ofid = Convert.ToInt32(cmdlogofip.ExecuteScalar().ToString());
        //                    SqlCommand cmdlogofup = new SqlCommand("update onoffline set time=@time where id ='" + ofid + "' ", baglanti);
        //                    cmdlogofup.Parameters.AddWithValue("time", 0.002);
        //                    cmdlogofup.ExecuteNonQuery();
        //                    cmdlogofup.Dispose();
        //                }
        //                cmdlogofip.Dispose();
        //            }

        //            return;
        //        }  
        //         cmdtwicebak.Dispose();
        //        baglanti.Close();               

        //}
   	
		
		
    }
