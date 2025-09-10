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
using System.Globalization;

public partial class yonet_statisticsc3 : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
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
                baslikyilyaz.Text = "2023";
                basliktop1.Text = Ljobs3w.ToString("0,0", elGR);
                basliktop2.Text = Lwork3w.ToString("0,0", elGR);
                if (Ljobs3w == 0) Ljobs3w = 1;
                basliktop3.Text = ((Lwork3w / Ljobs3w) + 0.00001).ToString("0.00", elGR);

                //uzakyol pilotu: select count(kapno) from vardiyadetay where ((binisyeri = 'Yelkenkaya' and(inisyeri = 'Limas' or inisyeri = 'Demir-İzmit' or inisyeri = 'Autoport')) or(inisyeri = 'Yelkenkaya' and(binisyeri = 'Limas' or binisyeri = 'Demir-İzmit' or binisyeri = 'Autoport')))  and kapno = '28' and istasyongelis like '%2017%'
                //diler 4-5 pilotu : select count(kapno) from vardiyadetay where ((binisyeri = 'Diler' and (binisrihtim = 'Q.5.in' or binisrihtim = 'Q.4.in')) or (inisyeri = 'Diler' and(inisrihtim = 'Q.5.in' or inisrihtim = 'Q.4.in'))) and kapno = '28' and istasyongelis like '%2017%'

                TopList();
                TopListw();

            }
            Litpagebaslik.Text = "PMTR Admin Page";
        }

   
        baglanti.Close();
    }

    private void TopList()
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdtoppil = new SqlCommand("select  top 3  count(kapno) as say,kapno from vardiyadetay where (istasyongelis like '%2023%' and grt!='' and manevraiptal='0') group by kapno order by count(kapno) desc", baglanti);
        SqlDataReader dr = cmdtoppil.ExecuteReader();

        Reptoppilot.DataSource = dr;
        Reptoppilot.DataBind();
        dr.Close();
        cmdtoppil.Dispose();
        baglanti.Close();
    }

    private void TopListw()
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdtopw = new SqlCommand("select  TOP 3  count(varno) as say,varno from vardiyadetay where (istasyongelis like '%2023%' and grt!='' and manevraiptal='0') group by varno order by count(varno) desc ", baglanti);
        SqlDataReader dr = cmdtopw.ExecuteReader();

        Reptopwatch.DataSource = dr;
        Reptopwatch.DataBind();
        dr.Close();
        cmdtopw.Dispose();
        baglanti.Close();
    }


    protected void Reptoppilot_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label padikop = (Label)e.Item.FindControl("padi");

            SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
            cmdPilotismial.CommandType = CommandType.StoredProcedure;
            cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(padikop.Text));
            string kapadisoyadi = "";
            SqlDataReader dr = cmdPilotismial.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    kapadisoyadi = dr["kapadisoyadi"].ToString();
                }
            }
            dr.Close();
            cmdPilotismial.Dispose();


            padikop.Text = kapadisoyadi;

        }
        baglanti.Close();

    }

    protected void Reptopwatch_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {

            Label widkop = (Label)e.Item.FindControl("wid");
            Label wdatekop2 = (Label)e.Item.FindControl("wdate");
            Label wdatekop = (Label)e.Item.FindControl("padi");

            if (String.IsNullOrEmpty(wdatekop.Text)) { }
                else
            {
               wdatekop2.Text = wdatekop.Text.Substring(4, 2) + "." +  wdatekop.Text.Substring(2, 2) +".20" +wdatekop.Text.Substring(0, 2);
            }


            DateTime varbaslar = new DateTime(2014, 01, 02, 09, 00, 00);
            TimeSpan ts2 = Convert.ToDateTime(wdatekop2.Text+" 12:00:00") - varbaslar;
            double farktoplam = ts2.TotalDays;
            double farkmod = farktoplam % 12;

            if (farkmod >= 0 && farkmod < 4)
            {
                widkop.Text = "Watch.1";
            }
            else if (farkmod >= 4 && farkmod < 8)
            {
                widkop.Text = "Watch.2";
            }
            else if (farkmod >= 8 && farkmod < 12)
            {
                widkop.Text = "Watch.3";
            }





        }
        baglanti.Close();

        
    }

    private void DDLDoldur()
    {

        DDLyilal.Items.Clear();

        for (int i = DateTime.Now.Year; i > 2010; i -= 1)
        {
            DDLyilal.Items.Add(i.ToString());
        }
        DDLyilal.Items[0].Selected = true;
    }

    private void ikiaylikvarbir()
    {
        SqlConnection baglanti = AnaKlas.baglan();
        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
        //iş saysısı toplami al 
        SqlCommand totaljobs = new SqlCommand("Select count(istasyongelis) from vardiyadetay where istasyongelis like '%2023%' and manevraiptal ='0' and varid='1'  and grt!=''  ", baglanti);
        int count = Convert.ToInt32(totaljobs.ExecuteScalar());

        if (count != 0)
        {
            using (PilotdbEntitiesvardetay entuc = new PilotdbEntitiesvardetay())
            { 
                entuc.Configuration.ProxyCreationEnabled = false;
                var veri2 = entuc.vardiyadetay
                    .ToList()
                    .Where(c => c.manevraiptal == "0" && c.varid == "1" && c.istasyongelis.Contains("2023") && c.grt != "");

                using (PilotdbEntitiesPilotlar entpilotlar = new PilotdbEntitiesPilotlar())
                {
                    entpilotlar.Configuration.ProxyCreationEnabled = false;
                    var butunpilotlar = from b in entpilotlar.pilotlar where b.kapsirano < 1000 select b;

                    List<pilotlar> pilotisimduzenle = new List<pilotlar>();
                    pilotisimduzenle = butunpilotlar.ToList();
                    int listindex = 0;
                    int vardiyapilotsayisi = 0;
                    float vardiyatoplamissaati = 0;
                    int tottotgun1 = 0;
                    int tottotgrt1 = 0;
                    foreach (var a in butunpilotlar)
                    {
                        SqlCommand totaljobsgun = new SqlCommand("Select count(DISTINCT(Substring(istasyongelis,0,6))) from vardiyadetay where istasyongelis like '%2023%' and manevraiptal ='0' and kapno='" + a.kapno + "'  and grt!='' ", baglanti);
                        int totalgun1 = Convert.ToInt32(totaljobsgun.ExecuteScalar());
                        totaljobsgun.Dispose();



                        var manevrasirala = from f in veri2 where (f.kapno == a.kapno) select f;
                        int sayim = manevrasirala.Count();
                        int totalgrt1 = 0;
                        if (sayim > 0)
                        {
                            listindex = listindex + 1;
                            float toplam = 0;

                            int vargunsay = 0;
                            string vargunal = "";
                            string vargunbak = "";
                    foreach (var a2 in manevrasirala)
                            {
                                float fark = Tarih1eksiTarih2SaatfarkiFloat(a2.istasyongelis, a2.istasyoncikis);
                                toplam = toplam + fark;
                                vargunbak = a2.istasyongelis.Substring(0, 10);
                                if (vargunal != vargunbak)
                                {
                                    vargunal = vargunbak;
                                    vargunsay = vargunsay + 1;
                                }

                               int grt = Convert.ToInt32(a2.grt);
                                totalgrt1 = totalgrt1 + grt;
                            }

                    vardiyapilotsayisi = vardiyapilotsayisi + 1;
                    vardiyatoplamissaati = vardiyatoplamissaati + toplam;
                            tottotgun1 = tottotgun1 + totalgun1;
                            tottotgrt1 = tottotgrt1 + totalgrt1; 
                            //string ttdd = ((toplam / 4) + 0.00001).ToString();
                            //p = decimal.Parse(ttdd);
                            //a.Percentage = Convert.ToInt32(p);
                            a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                            a.toplamissayisi = sayim;
                            a.toplamissuresi = (toplam + 0.0001).ToString().Substring(0, 5);
                            a.toplamdinlenme = totalgun1.ToString();
                            a.owa = ((toplam / sayim) + 0.0001).ToString().Substring(0, 5);
                            a.yorulma = decimal.Parse((decimal.Parse(totalgrt1.ToString()) / 1000000).ToString());
                            a.Percentage = Convert.ToInt32(a.yorulma*10);

                            //a.yorulma = decimal.Parse(((toplam / (vargunsay * 24)) + 0.0001).ToString().Substring(0, 5));
                            //a.Percentage = Convert.ToInt32(a.yorulma * 100);
                        }

                        else
                        {
                            pilotisimduzenle.RemoveAt(listindex);
                        }
                    }
                    if (vardiyapilotsayisi == 0) vardiyapilotsayisi = 1;
                    Lwork.Text = ((vardiyatoplamissaati + 0.000001).ToString("0,0", elGR));
                    Ldays.Text = (tottotgun1.ToString("0,0", elGR)).ToString();
                    Lgrt.Text = (tottotgrt1.ToString("0,0", elGR)).ToString();


                    GridView2a.DataSource = pilotisimduzenle.OrderByDescending(x=> x.toplamissayisi);
                    GridView2a.DataBind();

                }
            }
        }
        else
        {
            using (PilotdbEntitiesPilotlar entpilotlar2 = new PilotdbEntitiesPilotlar())
            {
                var veri = from b in entpilotlar2.pilotlar where b.kapsirano < 1000 & b.varid == "1" orderby b.kapadi + " " + b.kapsoyadi ascending select b;
                foreach (var a in veri)
                {
                    a.Percentage = 0;
                    a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                    a.toplamissayisi = 0;
                    a.toplamissuresi = "0";
                    a.toplamdinlenme = "0";
                    a.owa = "0";
                    a.yorulma = 0;
                    Ldays.Text = "0";
                    Lwork.Text = "0";
                    Lgrt.Text = "0";
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
        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

        //iş saysısı toplami al 
        SqlCommand totaljobs = new SqlCommand("Select count(istasyongelis) from vardiyadetay where istasyongelis like '%2023%' and manevraiptal ='0' and varid='2'  and grt!='' ", baglanti);

        int count = Convert.ToInt32(totaljobs.ExecuteScalar());

        if (count != 0)
        {
            using (PilotdbEntitiesvardetay entuc = new PilotdbEntitiesvardetay())
            {
                entuc.Configuration.ProxyCreationEnabled = false;
                var veri2 = entuc.vardiyadetay
                    .ToList()
                    .Where(c => c.manevraiptal == "0" && c.varid == "2" && c.istasyongelis.Contains("2023") && c.grt != "");

                using (PilotdbEntitiesPilotlar entpilotlar = new PilotdbEntitiesPilotlar())
                {
                    entpilotlar.Configuration.ProxyCreationEnabled = false;
                    var butunpilotlar = from b in entpilotlar.pilotlar where b.kapsirano < 1000  select b;

                       List<pilotlar> pilotisimduzenle = new List<pilotlar>();
                    pilotisimduzenle = butunpilotlar.ToList();
                    int listindex = 0;
                    int vardiyapilotsayisi = 0;
                    float vardiyatoplamissaati = 0;
                    int tottotgun2 = 0;
                    int tottotgrt2 = 0;
                    foreach (var a in butunpilotlar)
                    {
                        SqlCommand totaljobsgun = new SqlCommand("Select count(DISTINCT(Substring(istasyongelis,0,6))) from vardiyadetay where istasyongelis like '%2023%' and manevraiptal ='0' and kapno='" + a.kapno + "'  and grt!=''  ", baglanti);
                        int totalgun2 = Convert.ToInt32(totaljobsgun.ExecuteScalar());
                        totaljobsgun.Dispose();



                        var manevrasirala = from f in veri2 where f.kapno == a.kapno select f;
                        int sayim = manevrasirala.ToList().Count();
                        int totalgrt2 = 0;
                        if (sayim > 0)
                        {
                           
                            listindex = listindex + 1;
                            float toplam = 0;

                            int vargunsay = 0;
                            string vargunal = "";
                            string vargunbak = "";
                            foreach (var a2 in manevrasirala)
                            {
                                float fark = Tarih1eksiTarih2SaatfarkiFloat(a2.istasyongelis, a2.istasyoncikis);
                                toplam = toplam + fark;
                                vargunbak = a2.istasyongelis.Substring(0, 10);
                                if (vargunal != vargunbak)
                                {
                                    vargunal = vargunbak;
                                    vargunsay = vargunsay + 1;
                                }
                                int grt = Convert.ToInt32(a2.grt);
                                totalgrt2 = totalgrt2 + grt;
                            }

                            vardiyapilotsayisi = vardiyapilotsayisi + 1;
                            vardiyatoplamissaati = vardiyatoplamissaati + toplam;
                            tottotgun2 = tottotgun2 + totalgun2;
                            tottotgrt2 = tottotgrt2 + totalgrt2;
                            //string ttdd = ((toplam / 4) + 0.00001).ToString();
                            //p = decimal.Parse(ttdd);
                            //a.Percentage = Convert.ToInt32(p);
                            a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                            a.toplamissayisi = sayim;
                            a.toplamissuresi = (toplam + 0.0001).ToString().Substring(0, 5);
                            a.toplamdinlenme = totalgun2.ToString();
                            a.owa = ((toplam / sayim) + 0.0001).ToString().Substring(0, 5);
                            a.yorulma = decimal.Parse((decimal.Parse(totalgrt2.ToString()) / 1000000).ToString());
                            a.Percentage = Convert.ToInt32(a.yorulma * 10);
                        }

                        else
                        {
                            pilotisimduzenle.RemoveAt(listindex);
                        }
                    }
                    if (vardiyapilotsayisi == 0) vardiyapilotsayisi = 1;
                    Lwork2b.Text = (vardiyatoplamissaati + 0.000001).ToString("0,0", elGR);
                    Ldays2.Text = tottotgun2.ToString("0,0", elGR);
                    Lgrt2.Text = tottotgrt2.ToString("0,0", elGR);

                    GridView2b.DataSource = pilotisimduzenle.OrderByDescending(x => x.toplamissayisi);
                    GridView2b.DataBind();

                }
            }
        }
        else
        {
            using (PilotdbEntitiesPilotlar entpilotlar2 = new PilotdbEntitiesPilotlar())
            {
                var veri = from b in entpilotlar2.pilotlar where b.kapsirano < 1000 & b.varid == "2" orderby b.kapadi + " " + b.kapsoyadi ascending select b;
                foreach (var a in veri)
                {
                    a.Percentage = 0;
                    a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                    a.toplamissayisi = 0;
                    a.toplamissuresi = "0";
                    a.toplamdinlenme = "0";
                    a.owa = "0";
                    a.yorulma = 0;
                    Ldays2.Text = "0";
                    Lwork2b.Text = "0";
                    Lgrt2.Text = "0";
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
        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

        //iş saysısı toplami al 
        SqlCommand totaljobs = new SqlCommand("Select count(istasyongelis) from vardiyadetay where istasyongelis like '%2023%' and manevraiptal ='0' and varid='3'  and grt!='' ", baglanti);

        int count = Convert.ToInt32(totaljobs.ExecuteScalar());

        if (count != 0)
        {
            using (PilotdbEntitiesvardetay entuc = new PilotdbEntitiesvardetay())
            {
                entuc.Configuration.ProxyCreationEnabled = false;
                var veri2 = entuc.vardiyadetay
                    .ToList()
                    .Where(c => c.manevraiptal == "0" && c.varid == "3" && c.istasyongelis.Contains("2023") && c.grt!="");



                using (PilotdbEntitiesPilotlar entpilotlar = new PilotdbEntitiesPilotlar())
                {
                    entpilotlar.Configuration.ProxyCreationEnabled = false;
                    var butunpilotlar = from b in entpilotlar.pilotlar where b.kapsirano < 1000  select b;
                    
                    List<pilotlar> pilotisimduzenle = new List<pilotlar>();
                    pilotisimduzenle = butunpilotlar.ToList();
                    int listindex = 0;
                    int vardiyapilotsayisi = 0;
                    float vardiyatoplamissaati = 0;
                    int tottotgun3 = 0;
                    int tottotgrt3 = 0;
                    foreach (var a in butunpilotlar)
                    {
                        SqlCommand totaljobsgun = new SqlCommand("Select count(DISTINCT(Substring(istasyongelis,0,6))) from vardiyadetay where istasyongelis like '%2023%' and manevraiptal ='0' and kapno='"+a.kapno+ "'  and grt!='' ", baglanti);
                        int totalgun3 = Convert.ToInt32(totaljobsgun.ExecuteScalar());
                        totaljobsgun.Dispose();


                        var manevrasirala = from f in veri2 where f.kapno == a.kapno select f;
                        int sayim = manevrasirala.Count();
                        int totalgrt3 = 0;
                        if (sayim > 0)
                        {
                           
                            listindex = listindex + 1;
                            float toplam = 0;

                            int vargunsay = 0;
                            string vargunal = "";
                            string vargunbak = "";
                            foreach (var a2 in manevrasirala)
                            {
                                float fark = Tarih1eksiTarih2SaatfarkiFloat(a2.istasyongelis, a2.istasyoncikis);
                                toplam = toplam + fark;
                                vargunbak = a2.istasyongelis.Substring(0, 10);
                                if (vargunal != vargunbak)
                                {
                                    vargunal = vargunbak;
                                    vargunsay = vargunsay + 1;
                                }
                                int grt = Convert.ToInt32(a2.grt);
                                totalgrt3 = totalgrt3 + grt;
                            }

                            vardiyapilotsayisi = vardiyapilotsayisi + 1;
                            vardiyatoplamissaati = vardiyatoplamissaati + toplam;
                            tottotgun3 = tottotgun3 + totalgun3;
                            tottotgrt3 = tottotgrt3 + totalgrt3;
                            //string ttdd = ((toplam / 4) + 0.00001).ToString();
                            //p = decimal.Parse(ttdd);
                            //a.Percentage = Convert.ToInt32(p);
                            a.pilotismi = a.kapadi + " " + a.kapsoyadi;
                            a.toplamissayisi = sayim;
                            a.toplamissuresi = (toplam + 0.0001).ToString().Substring(0, 5);
                            a.toplamdinlenme = totalgun3.ToString();
                            a.owa = ((toplam / sayim) + 0.0001).ToString().Substring(0, 5);
                            a.yorulma = decimal.Parse((decimal.Parse(totalgrt3.ToString()) / 1000000).ToString());
                            a.Percentage = Convert.ToInt32(a.yorulma * 10);
                        }

                        else
                        {
                            pilotisimduzenle.RemoveAt(listindex);
                        }
                    }
                    if (vardiyapilotsayisi == 0) vardiyapilotsayisi = 1;
                    Lwork2c.Text = (vardiyatoplamissaati + 0.000001).ToString("0,0", elGR);
                    Ldays3.Text = tottotgun3.ToString("0,0", elGR);
                    Lgrt3.Text = tottotgrt3.ToString("0,0", elGR);
                    //List<pilotlar> pilotisimduzenleorder = new List<pilotlar>();
                    //pilotisimduzenleorder = pilotisimduzenle.ToList();

                    GridView2c.DataSource = pilotisimduzenle.OrderByDescending(x => x.toplamissayisi);
                    GridView2c.DataBind();

                }
            }
        }
        else
        {
            using (PilotdbEntitiesPilotlar entpilotlar2 = new PilotdbEntitiesPilotlar())
            {
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
                    Ldays3.Text = "0";
                    Lwork2c.Text = "0";
                    Lgrt3.Text = "0";
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
        ikiaylikvarbir();
        ikiaylikvariki();
        ikiaylikvaruc();

        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

        int Ljobs3w = Convert.ToInt32(Ljobs.Text.ToString()) + Convert.ToInt32(Ljobs2b.Text.ToString()) + Convert.ToInt32(Ljobs2c.Text.ToString());
        float Lwork3w = float.Parse(Lwork.Text.ToString()) + float.Parse(Lwork2b.Text.ToString()) + float.Parse(Lwork2c.Text.ToString());
        baslikyilyaz.Text = "2023";
        basliktop1.Text = Ljobs3w.ToString("0,0", elGR);
        basliktop2.Text = Lwork3w.ToString("0,0", elGR);
        if (Ljobs3w == 0) Ljobs3w = 1;
        basliktop3.Text = ((Lwork3w / Ljobs3w) + 0.00001).ToString("0.00", elGR);

        TopList();
        TopListw();

    }

    private float Tarih1eksiTarih2SaatfarkiFloat(string TarihsaaatcumlesiB, string TarihsaaatcumlesiK)
    {
        TimeSpan ts = Convert.ToDateTime(TarihsaaatcumlesiB) - Convert.ToDateTime(TarihsaaatcumlesiK);
        float farktoplamsaat = float.Parse(ts.TotalHours.ToString());
        return farktoplamsaat;
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
    protected void liistatik2_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsb.aspx");
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






}