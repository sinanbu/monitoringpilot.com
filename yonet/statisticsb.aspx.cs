using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;


public partial class yonet_statisticsb : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == "" || (Session["kapno"] == null) || cmdlogofbak.ExecuteScalar() == null || Session["yetki"].ToString() != "9")
        {
            Response.Redirect("../pmtr.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                DDLDoldur();

                ikiaylikvarbir();
                ikiaylikvariki();
                ikiaylikvaruc();

                int Ljobs3w = Convert.ToInt32(Ljobs.Text.ToString()) + Convert.ToInt32(Ljobs2b.Text.ToString()) + Convert.ToInt32(Ljobs2c.Text.ToString());
                float Lwork3w = float.Parse(Lwork.Text.ToString()) + float.Parse(Lwork2b.Text.ToString()) + float.Parse(Lwork2c.Text.ToString());
                basliktop1.Text = Ljobs3w.ToString();
                basliktop2.Text = Lwork3w.ToString();
                if (Ljobs3w == 0) Ljobs3w = 1;
                basliktop3.Text = ((Lwork3w / Ljobs3w) + 0.00001).ToString().Substring(0,4);
            }
            Litpagebaslik.Text = "PMTR Admin Page";
        }

        
        baglanti.Close();
    }
    private void DDLDoldur()
    {

        DDLTextBox2.Items.Clear();
        DateTime tarihbase = TarihSaatYapDMYhm("01." + DateTime.Now.AddMonths(-2).Month.ToString() + "." + DateTime.Now.AddMonths(-2).Year.ToString() + " 00:00:01");
        for (int i = 0; i <= 11; i += 1)
        {
            DDLTextBox2.Items.Add(TarihSaatYaziYapDMYhm(tarihbase.AddMonths(-i)).Substring(0, 10));
        }      
        DDLTextBox2.Items[0].Selected = true;

        TextBox2a.Text = TarihYaziYapDMY(TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text).AddDays(59));
        TextBox2ag.Text = TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text + " 00:00:01").AddDays(59).ToString();

    }
    private void ikiaylikvarbir()
    {
        SqlConnection baglanti = AnaKlas.baglan();
 
        // string tarih = Convert.ToDateTime(datetimepicker1.Value).ToString("yyyy-MM-dd");    CONVERT(DATETIME,inputdate) AS inputdate   " + TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text).ToString("yyyy-MM-dd") + "
        // and convert(datetime,pob,103)<='" + TarihSaatYapDMYhm(TextBox2ag.Text.ToString()) + "' and manevraiptal ='0'
        // new DateTime(DateTime.Now.AddMonths(-2).Year,DateTime.Now.AddMonths(-2).Month,1,0,0,1); 

        //iş saysısı toplami al 
        SqlCommand totaljobs = new SqlCommand("Select count(istasyoncikis) from vardiyadetay where convert(datetime, pob, 103) >= convert(datetime, '" + DDLTextBox2.SelectedItem.Text.ToString() + "', 103) and convert(datetime, pob, 103) <= convert(datetime, '" + TextBox2ag.Text + "', 103)  and manevraiptal ='0' and varid='1'  ", baglanti);
        int count = Convert.ToInt32(totaljobs.ExecuteScalar());
        
        if (count != 0)
        {
                    using (PilotdbEntitiesvardetay entuc = new PilotdbEntitiesvardetay())
                    {
                        entuc.Configuration.ProxyCreationEnabled = false;
                        var veri2 = entuc.vardiyadetay
                            .ToList()
                            .Where(c => c.manevraiptal == "0" && c.varid == "1"); // && TarihSaatYapDMYhm(c.pob) >= TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text) && TarihSaatYapDMYhm(c.pob) <= TarihSaatYapDMYhm(TextBox2ag.Text))

                        DateTime Secilitarih1 = TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text);
                        DateTime Secilitarih2 = TarihSaatYapDMYhm(TextBox2ag.Text);

                        foreach (var s in veri2)
                        {
                            s.DTpob = TarihSaatYapDMYhm(s.pob);
                        }

                        var veri2s = entuc.vardiyadetay.ToList().Where(c => c.manevraiptal == "0" && c.varid == "1" && c.DTpob >= Secilitarih1 && c.DTpob <= Secilitarih2);


                        using (PilotdbEntitiesPilotlar entpilotlar = new PilotdbEntitiesPilotlar())
                        {
                            var butunpilotlar = from b in entpilotlar.pilotlar where b.kapsirano < 1000 select b;

                            List<pilotlar> pilotisimduzenle  = new List<pilotlar>();
                            pilotisimduzenle=butunpilotlar.ToList();

                            int listindex = 0; 
                            int vardiyapilotsayisi = 0;
                            float vardiyatoplamissaati = 0;

                            foreach (var a in butunpilotlar)
                            {
                                var manevrasirala = from f in veri2s where f.kapno == a.kapno select f;
                                int sayim = manevrasirala.Count();
                                if (sayim > 0)
                                {
                                    listindex = listindex + 1;
                                    float toplam = 0;
                                    decimal p = 0;
                                    foreach (var a2 in manevrasirala)
                                    {
                                        float fark = Tarih1eksiTarih2SaatfarkiFloat(a2.istasyongelis, a2.istasyoncikis);
                                        toplam = toplam + fark;
                                    }
                                    string ttdd = ((toplam / 4 )+0.00001).ToString();
                                    p = decimal.Parse(ttdd);

                                    a.Percentage = Convert.ToInt32(p);
                                    a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                                    a.toplamissayisi = sayim;
                                    a.toplamissuresi = (toplam + 0.0001).ToString().Substring(0, 5);
                                    a.toplamdinlenme = ((480 - toplam) + 0.0001).ToString().Substring(0, 5);
                                    a.owa = ((toplam / sayim) + 0.0001).ToString().Substring(0, 5);
                                    a.yorulma = decimal.Parse((toplam / 480).ToString().Substring(0,4)) ;

                                    vardiyapilotsayisi = vardiyapilotsayisi + 1;
                                    vardiyatoplamissaati = vardiyatoplamissaati + toplam;
                                }

                                else
                                {
                                   pilotisimduzenle.RemoveAt(listindex);
                                }
                            }
                            if (vardiyapilotsayisi == 0) vardiyapilotsayisi = 1;
                OPAwt.Text = ((vardiyatoplamissaati / vardiyapilotsayisi)+0.00001).ToString().Substring(0, 5) + " hrs.";
                Lwork.Text = (vardiyatoplamissaati+0.000001).ToString().Substring(0, 6);

                //List<pilotlar> pilotisimduzenleorder = new List<pilotlar>();
                //pilotisimduzenleorder = pilotisimduzenle.ToList();

                GridView2a.DataSource = pilotisimduzenle.OrderByDescending(x => x.Percentage);
                GridView2a.DataBind();

                        }
                    }
        }
            else
        {
            using (PilotdbEntitiesPilotlar entpilotlar2 = new PilotdbEntitiesPilotlar())
            {
                entpilotlar2.Configuration.ProxyCreationEnabled = false;
                var veri = from b in entpilotlar2.pilotlar where b.kapsirano < 1000 && b.varid == "1" orderby b.kapadi + " " + b.kapsoyadi ascending select b;
                foreach (var a in veri)
                {
                    a.Percentage = 0;
                    a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                    a.toplamissayisi = 0;
                    a.toplamissuresi = "0";
                    a.toplamdinlenme = "0";
                    a.owa = "0";
                    a.yorulma = 0;

                    Lwork.Text = "0";
                    OPAwt.Text = "0";

                    GridView2a.DataSource = veri.ToList();
                    GridView2a.DataBind();
                }
            }
        }
        Ljobs.Text = count.ToString();
        baglanti.Close();

        }
    protected void GridView2a_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView2a.SelectedIndex)
        {
            e.Cancel = true;
            GridView2a.SelectedIndex = -1;
            ikiaylikvarbir();
            ikiaylikvariki();
            ikiaylikvaruc();
        }
    }

    private void ikiaylikvariki()
    {
        SqlConnection baglanti = AnaKlas.baglan();

        //iş saysısı toplami al 
        SqlCommand totaljobs = new SqlCommand("Select count(istasyoncikis) from vardiyadetay where convert(datetime, pob, 103) >= convert(datetime, '" + DDLTextBox2.SelectedItem.Text.ToString() + "', 103) and convert(datetime, pob, 103) <= convert(datetime, '" + TextBox2ag.Text + "', 103)  and manevraiptal ='0' and varid='2'  ", baglanti);

        int count = Convert.ToInt32(totaljobs.ExecuteScalar());

        if (count != 0)
        {
            using (PilotdbEntitiesvardetay entuc = new PilotdbEntitiesvardetay())
            {
                entuc.Configuration.ProxyCreationEnabled = false;
                var veri2 = entuc.vardiyadetay
                    .ToList()
                    .Where(c => c.manevraiptal == "0" && c.varid == "2"); //&& TarihSaatYapDMYhm(c.pob) >= TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text) && TarihSaatYapDMYhm(c.pob) <= TarihSaatYapDMYhm(TextBox2ag.Text)

                DateTime Secilitarih1 = TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text);
                DateTime Secilitarih2 = TarihSaatYapDMYhm(TextBox2ag.Text);

                foreach (var s in veri2)
                {
                    s.DTpob = TarihSaatYapDMYhm(s.pob);
                }

                var veri2s = entuc.vardiyadetay.ToList().Where(c => c.manevraiptal == "0" && c.varid == "2" && c.DTpob >= Secilitarih1 && c.DTpob <= Secilitarih2);


                using (PilotdbEntitiesPilotlar entpilotlar = new PilotdbEntitiesPilotlar())
                {
                    var butunpilotlar = from b in entpilotlar.pilotlar where b.kapsirano < 1000  select b;

                    List<pilotlar> pilotisimduzenle = new List<pilotlar>();
                    pilotisimduzenle = butunpilotlar.ToList();

                    int listindex = 0;
                    int vardiyapilotsayisi = 0;
                    float vardiyatoplamissaati = 0;

                    foreach (var a in butunpilotlar)
                    {
                        var manevrasirala = from f in veri2s where f.kapno == a.kapno select f;
                        int sayim = manevrasirala.Count();
                        if (sayim > 0)
                        {
                            listindex = listindex + 1;
                            float toplam = 0;
                            decimal p = 0;
                            foreach (var a2 in manevrasirala)
                            {
                                float fark = Tarih1eksiTarih2SaatfarkiFloat(a2.istasyongelis, a2.istasyoncikis);
                                toplam = toplam + fark;
                            }
                            string ttdd = ((toplam / 4) + 0.00001).ToString();
                            p = decimal.Parse(ttdd);

                            a.Percentage = Convert.ToInt32(p);
                            a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                            a.toplamissayisi = sayim;
                            a.toplamissuresi = (toplam + 0.0001).ToString().Substring(0, 5);
                            a.toplamdinlenme = ((480 - toplam) + 0.0001).ToString().Substring(0, 5);
                            a.owa = ((toplam / sayim) + 0.0001).ToString().Substring(0, 5);
                            a.yorulma = decimal.Parse((toplam / 480).ToString().Substring(0, 4));

                            vardiyapilotsayisi = vardiyapilotsayisi + 1;
                            vardiyatoplamissaati = vardiyatoplamissaati + toplam;
                        }

                        else
                        {
                            pilotisimduzenle.RemoveAt(listindex);
                        }
                    }
                    if (vardiyapilotsayisi == 0) vardiyapilotsayisi = 1;
                    OPAwt2b.Text = ((vardiyatoplamissaati / vardiyapilotsayisi) + 0.00001).ToString().Substring(0, 5) + " hrs.";
                    Lwork2b.Text = (vardiyatoplamissaati + 0.000001).ToString().Substring(0, 6);

                    GridView2b.DataSource = pilotisimduzenle.OrderByDescending(x => x.Percentage);
                    GridView2b.DataBind();

                }
            }
        }
        else
        {
            using (PilotdbEntitiesPilotlar entpilotlar2 = new PilotdbEntitiesPilotlar())
            {
                entpilotlar2.Configuration.ProxyCreationEnabled = false;
                var veri = from b in entpilotlar2.pilotlar where b.kapsirano < 1000 && b.varid == "2" orderby b.kapadi + " " + b.kapsoyadi ascending select b;
                foreach (var a in veri)
                {
                    a.Percentage = 0;
                    a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                    a.toplamissayisi = 0;
                    a.toplamissuresi = "0";
                    a.toplamdinlenme = "0";
                    a.owa = "0";
                    a.yorulma = 0;

                    Lwork2b.Text = "0";
                    OPAwt2b.Text = "0";

                    GridView2b.DataSource = veri.ToList();
                    GridView2b.DataBind();
                }
            }
        }
        Ljobs2b.Text = count.ToString();
        baglanti.Close();

    }
    protected void GridView2b_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView2b.SelectedIndex)
        {
            e.Cancel = true;
            GridView2b.SelectedIndex = -1;
            ikiaylikvarbir();
            ikiaylikvariki();
            ikiaylikvaruc();
        }
    }

    private void ikiaylikvaruc()
    {
        SqlConnection baglanti = AnaKlas.baglan();

        //iş saysısı toplami al 
        SqlCommand totaljobs = new SqlCommand("Select count(istasyoncikis) from vardiyadetay where convert(datetime, pob, 103) >= convert(datetime, '" + DDLTextBox2.SelectedItem.Text.ToString() + "', 103) and convert(datetime, pob, 103) <= convert(datetime, '" + TextBox2ag.Text + "', 103)  and manevraiptal ='0' and varid='3'  ", baglanti);

        int count = Convert.ToInt32(totaljobs.ExecuteScalar());

        if (count != 0)
        {
            using (PilotdbEntitiesvardetay entuc = new PilotdbEntitiesvardetay())
            {entuc.Configuration.ProxyCreationEnabled = false;
                
                var veri2 = entuc.vardiyadetay
                    .ToList()
                    .Where(c => c.manevraiptal == "0" && c.varid == "3" ); //&& TarihSaatYapDMYhm(c.pob) >= TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text) && TarihSaatYapDMYhm(c.pob) <= TarihSaatYapDMYhm(TextBox2ag.Text)

                DateTime Secilitarih1 = TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text);
                DateTime Secilitarih2 = TarihSaatYapDMYhm(TextBox2ag.Text);

                foreach (var s in veri2)
                {
                    s.DTpob = TarihSaatYapDMYhm(s.pob);
                }

                var veri2s = entuc.vardiyadetay.ToList().Where(c => c.manevraiptal == "0" && c.varid == "3" && c.DTpob >= Secilitarih1 && c.DTpob <= Secilitarih2);

                using (PilotdbEntitiesPilotlar entpilotlar = new PilotdbEntitiesPilotlar())
                {
                    var butunpilotlar = from b in entpilotlar.pilotlar where b.kapsirano < 1000  select b;

                    List<pilotlar> pilotisimduzenle = new List<pilotlar>();
                    pilotisimduzenle = butunpilotlar.ToList();
                    int listindex = 0;
                    int vardiyapilotsayisi = 0;
                    float vardiyatoplamissaati = 0;

                    foreach (var a in butunpilotlar)
                    {
                        var manevrasirala = from f in veri2s where f.kapno == a.kapno select f;
                        int sayim = manevrasirala.Count();
                        if (sayim > 0)
                        {
                            listindex = listindex + 1;
                            float toplam = 0;
                            decimal p = 0;
                            foreach (var a2 in manevrasirala)
                            {
                                float fark = Tarih1eksiTarih2SaatfarkiFloat(a2.istasyongelis, a2.istasyoncikis);
                                toplam = toplam + fark;
                            }
                            string ttdd = ((toplam / 4) + 0.00001).ToString();
                            p = decimal.Parse(ttdd);

                            a.Percentage = Convert.ToInt32(p);
                            a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                            a.toplamissayisi = sayim;
                            a.toplamissuresi = (toplam + 0.0001).ToString().Substring(0, 5);
                            a.toplamdinlenme = ((480 - toplam) + 0.0001).ToString().Substring(0, 5);
                            a.owa = ((toplam / sayim) + 0.0001).ToString().Substring(0, 5);
                            a.yorulma = decimal.Parse((toplam / 480).ToString().Substring(0, 4));

                            vardiyapilotsayisi = vardiyapilotsayisi + 1;
                            vardiyatoplamissaati = vardiyatoplamissaati + toplam;

                        }

                        else
                        {
                            pilotisimduzenle.RemoveAt(listindex);
                        }
                    }
                    if (vardiyapilotsayisi == 0) vardiyapilotsayisi = 1;
                    OPAwt2c.Text = ((vardiyatoplamissaati / vardiyapilotsayisi) + 0.00001).ToString().Substring(0, 5) + " hrs.";
                    Lwork2c.Text = (vardiyatoplamissaati + 0.000001).ToString().Substring(0, 6);


                    GridView2c.DataSource = pilotisimduzenle.OrderByDescending(x => x.Percentage);
                    GridView2c.DataBind();

                }
            }
        }
        else
        {
            using (PilotdbEntitiesPilotlar entpilotlar2 = new PilotdbEntitiesPilotlar())
            {
                entpilotlar2.Configuration.ProxyCreationEnabled = false;
                var veri = from b in entpilotlar2.pilotlar where b.kapsirano < 1000 && b.varid == "3" orderby b.kapadi + " " + b.kapsoyadi ascending select b;
                foreach (var a in veri)
                {
                    a.Percentage = 0;
                    a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                    a.toplamissayisi = 0;
                    a.toplamissuresi = "0";
                    a.toplamdinlenme = "0";
                    a.owa = "0";
                    a.yorulma = 0;

                    Lwork2c.Text = "0";
                    OPAwt2c.Text = "0";

                    GridView2c.DataSource = veri.ToList();
                    GridView2c.DataBind();
                }
            }
        }
        Ljobs2c.Text = count.ToString();
        baglanti.Close();
    }
    protected void GridView2c_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView2c.SelectedIndex)
        {
            e.Cancel = true;
            GridView2c.SelectedIndex = -1;
            ikiaylikvarbir();
            ikiaylikvariki();
            ikiaylikvaruc();
        }
    }

    protected void LBgetist3_Click(object sender, EventArgs e)
    {
        TextBox2a.Text = TarihYaziYapDMY(TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text).AddDays(59));
        TextBox2ag.Text = TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text + " 00:00:01").AddDays(59).ToString();

        ikiaylikvarbir();
        ikiaylikvariki();
        ikiaylikvaruc();

        int Ljobs3w = Convert.ToInt32(Ljobs.Text.ToString()) + Convert.ToInt32(Ljobs2b.Text.ToString()) + Convert.ToInt32(Ljobs2c.Text.ToString());
        float Lwork3w = float.Parse(Lwork.Text.ToString()) + float.Parse(Lwork2b.Text.ToString()) + float.Parse(Lwork2c.Text.ToString());
        basliktop1.Text = Ljobs3w.ToString();
        basliktop2.Text = Lwork3w.ToString();
        if (Ljobs3w == 0) Ljobs3w=1;
        basliktop3.Text = ((Lwork3w / Ljobs3w) + 0.00001).ToString().Substring(0, 4);
    }

    public DateTime TarihSaatYapDMYhm(string Tarihsaaatcumlesi)
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        DateTime Tarihsaatok = DateTime.Parse(Tarihsaaatcumlesi, culture, System.Globalization.DateTimeStyles.AssumeLocal);
        // result = DateTime.TryParseExact(Tarihsaaatcumlesi, dtFormats, new CultureInfo("tr-TR"), DateTimeStyles.None, out dt);
        //DateTime Tarihsaatok = DateTime.ParseExact(Tarihsaaatcumlesi, "DD.mm.yyyy HH:mm", null);
        return Tarihsaatok;
    }
    public string TarihYaziYapDMY(DateTime TarihsaatDMYhms)
    {
        string TarihYaziok = TarihSaatYaziYapDMYhm(TarihsaatDMYhms).Substring(0, 10);
        return TarihYaziok;
    }
    public float Tarih1eksiTarih2SaatfarkiFloat(string TarihsaaatcumlesiB, string TarihsaaatcumlesiK)
    {
        TimeSpan ts = Convert.ToDateTime(TarihsaaatcumlesiB) - Convert.ToDateTime(TarihsaaatcumlesiK);
        float farktoplamsaat = float.Parse(ts.TotalHours.ToString());
        return farktoplamsaat;
    }
    public string TarihSaatYaziYapDMYhm(DateTime TarihsaatDMYhms)
    {
        string TarihsaatYaziok, moys, doms, hhs, mms;

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
    protected void LBguvcik_Click(object sender, EventArgs e)
    {
        if (Session["kapno"] == "" || (Session["kapno"] == null))
        {
            Response.Redirect("../pmtr.aspx");
        }
        else
        {
            AnaKlas.logrecup(Session["kapno"].ToString());
            Session.Abandon();
            Response.Redirect("../pmtr.aspx");
        }
    }

    protected void liistatik1_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsa.aspx");
    }
    protected void liistatik3_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsc.aspx");
    }
    protected void liistatik4_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsd.aspx");
    }
    protected void liistatik5_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticse.aspx");
    }
    protected void liistatik6_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsf.aspx");
    }
    protected void liistatik7_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsg.aspx");
    }
    protected void liistatik8_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsh.aspx");
    }
    protected void liistatik9_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsi.aspx");
    }



    protected void DDLTextBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox2a.Text =TarihYaziYapDMY(TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text).AddDays(59));
        TextBox2ag.Text = TarihSaatYapDMYhm(DDLTextBox2.SelectedItem.Text).AddDays(59).ToString();
    }
}