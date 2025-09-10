using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Text;


public partial class watchsummary : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);


        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Convert.ToInt32(Session["kapno"]) < 0 || Convert.ToInt32(Session["kapno"]) > 999)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "99")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else  //if (cmdlogofbak.ExecuteScalar() != null)
        {
            if (!IsPostBack)
            {
                if (Session["yetki"].ToString() == "9")
                { LBonline.Enabled = true; }
                else { LBonline.Enabled = false; }


                SqlCommand seskapadi = new SqlCommand("SP_seskapadi", baglanti);
                seskapadi.CommandType = CommandType.StoredProcedure;
                seskapadi.Parameters.AddWithValue("@kapno", Session["kapno"].ToString());
                seskapadi.Parameters.Add("@kapadisonuc", SqlDbType.NVarChar, 30);
                seskapadi.Parameters["@kapadisonuc"].Direction = ParameterDirection.Output;
                seskapadi.ExecuteNonQuery();
                string seskapadis = seskapadi.Parameters["@kapadisonuc"].Value.ToString().Trim();
                seskapadi.Dispose();

                SqlCommand seskapsoyadi = new SqlCommand("SP_seskapsoyadi", baglanti);
                seskapsoyadi.CommandType = CommandType.StoredProcedure;
                seskapsoyadi.Parameters.AddWithValue("@kapno", Session["kapno"].ToString());
                seskapsoyadi.Parameters.Add("@kapsoyadisonuc", SqlDbType.NVarChar, 30);
                seskapsoyadi.Parameters["@kapsoyadisonuc"].Direction = ParameterDirection.Output;
                seskapsoyadi.ExecuteNonQuery();
                string seskapsoyadis = seskapsoyadi.Parameters["@kapsoyadisonuc"].Value.ToString().Trim();
                seskapsoyadi.Dispose();

                LBonline.Text = seskapadis + " " + seskapsoyadis; // +Session.Timeout.ToString();

                LiteralYaz();

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
            }

            if (Session["yetki"].ToString() == "0")
            {
                mainmanu2.Visible = false;
                mainmanu3.Visible = false;
                mainmanu4.Visible = false;
                mainmanu6.Visible = false;
            }

            if (Session["kapno"].ToString() == "1" || Session["kapno"].ToString() == "47" || Session["kapno"].ToString() == "52" || Session["kapno"].ToString() == "95" || Session["kapno"].ToString() == "92" || Session["kapno"].ToString() == "94" || Session["kapno"].ToString() == "97" || Session["kapno"].ToString() == "98")
            {
                mainmanu2.Visible = true;
                mainmanu3.Visible = true;
                mainmanu6.Visible = true;
            }
            else if (Session["kapno"].ToString() == "96")
            {
                mainmanu2.Visible = true;
            }


            DTloading();
        }

        baglanti.Close();

    }

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        if (varbiter.Text != "")
        {
            if (AnaKlas.TarihSaatYapDMYhm(varbiter.Text) > (DateTime.Now))
            {
                //decimal gzsaatd = AnaKlas.varrealtimesaat();
                //int gzsaat = Convert.ToInt32(Math.Floor(Math.Abs(gzsaatd)));
                //decimal gzdakikad = (gzsaatd - gzsaat) * 60;
                //int gzdakika = Convert.ToInt32(Math.Floor(Math.Abs(gzdakikad)));
                //string gzdakikas = gzdakika.ToString();
                //if (gzdakika < 10) { gzdakikas = "0" + gzdakikas; }
                //decimal gzsanid = (gzdakikad - gzdakika) * 60;
                //int gzsani = Convert.ToInt32(Math.Floor(Math.Abs(gzsanid)));
                //string gzsanis = gzsani.ToString();
                //if (gzsani < 10) { gzsanis = "0" + gzsanis; }

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNunber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
                //LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text + " / Passed Time: " + gzsaat.ToString() + ":" + gzdakikas + ":" + gzsanis;
            }
            else
            {
                LblVarid.ForeColor = System.Drawing.Color.Red;
                LblVarid.Text = "Please Change Watch ! ";
                LblVarno.Text = "";
                LblVarbasla.Text = "";
                LblVarbit.Text = "";
            }
        }
        else
        {
            LiteralYaz();
        }

    }

    private void LiteralYaz()
    {
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmdvarbilvarid = new SqlCommand("SP_varbilgivaridoku", baglanti);
        cmdvarbilvarid.CommandType = CommandType.StoredProcedure;
        cmdvarbilvarid.Parameters.Add("@bilgivaridoku", SqlDbType.Char, 1);
        cmdvarbilvarid.Parameters["@bilgivaridoku"].Direction = ParameterDirection.Output;
        cmdvarbilvarid.ExecuteNonQuery();
        varbilvarid.Text = cmdvarbilvarid.Parameters["@bilgivaridoku"].Value.ToString().Trim();
        cmdvarbilvarid.Dispose();

        SqlCommand cmdvarbilvarno = new SqlCommand("SP_varbilgivarnooku", baglanti);
        cmdvarbilvarno.CommandType = CommandType.StoredProcedure;
        cmdvarbilvarno.Parameters.Add("@bilgivarnooku", SqlDbType.Char, 6);
        cmdvarbilvarno.Parameters["@bilgivarnooku"].Direction = ParameterDirection.Output;
        cmdvarbilvarno.ExecuteNonQuery();
        varbilvarno.Text = cmdvarbilvarno.Parameters["@bilgivarnooku"].Value.ToString().Trim();
        cmdvarbilvarno.Dispose();

        SqlCommand cmdvarbaslar = new SqlCommand("SP_Vardiyabaslangic", baglanti);
        cmdvarbaslar.CommandType = CommandType.StoredProcedure;
        cmdvarbaslar.Parameters.Add("@sonuc", SqlDbType.Char, 16);
        cmdvarbaslar.Parameters["@sonuc"].Direction = ParameterDirection.Output;
        cmdvarbaslar.ExecuteNonQuery();
        varbaslar.Text = cmdvarbaslar.Parameters["@sonuc"].Value.ToString().Trim();
        cmdvarbaslar.Dispose();

        SqlCommand cmdvarbiter = new SqlCommand("SP_Vardiyabitisi", baglanti);
        cmdvarbiter.CommandType = CommandType.StoredProcedure;
        cmdvarbiter.Parameters.Add("@sonuc", SqlDbType.Char, 16);
        cmdvarbiter.Parameters["@sonuc"].Direction = ParameterDirection.Output;
        cmdvarbiter.ExecuteNonQuery();
        varbiter.Text = cmdvarbiter.Parameters["@sonuc"].Value.ToString().Trim();
        cmdvarbiter.Dispose();
        baglanti.Close();
        baglanti.Dispose();
    }

    private void DTloading()
    {
        Chartoranlason();
        Chartoranlaonceki();
        Chartoranlaonceki2();
    }


    private void Chartoranlason()
    {
        SqlConnection baglanti = AnaKlas.baglan();


        SqlCommand varokucmd = new SqlCommand("Select max(varno) from pilotlar ", baglanti);
        string varnonow = varokucmd.ExecuteScalar().ToString();
        varokucmd.Dispose();



        //string varnonow = AnaKlas.varnohesapla();
        //string varidnow = AnaKlas.varidhesapla();

        int toptopissay = 0;
        decimal toptopissure = 0;
        decimal toptoppilot = 0;
        int bak = 0;

        //yaşanan vardiya toplam bilgisi okuma 
        SqlCommand cmdvarcek = new SqlCommand("Select COUNT(*) from pilotvardiya where varno =" + varnonow, baglanti);
        bak = Convert.ToInt32(cmdvarcek.ExecuteScalar().ToString());
        if (bak > 0)
        {
            //yaşanan vardiya toplam bilgisi okuma 
            SqlCommand cmdvardegeroku = new SqlCommand("SP_Pilotvardiya_TopTop_is_sur_pilot", baglanti);
            cmdvardegeroku.CommandType = CommandType.StoredProcedure;
            cmdvardegeroku.Parameters.AddWithValue("@varno", Convert.ToInt32(varnonow));
            SqlDataReader vardr = cmdvardegeroku.ExecuteReader();

            if (vardr.HasRows)
            {
                while (vardr.Read())
                {
                    toptopissay = Convert.ToInt32(vardr["toptopissay"].ToString());
                    toptopissure = Convert.ToDecimal(vardr["toptopissure"].ToString());
                }
            }

            vardr.Close();
            cmdvardegeroku.Dispose();
        }
        cmdvarcek.Dispose();

        SqlCommand cmdPilotsaycalismis = new SqlCommand("SP_Pilotsaycalismis", baglanti);
        cmdPilotsaycalismis.CommandType = CommandType.StoredProcedure;
        cmdPilotsaycalismis.Parameters.AddWithValue("@varno", Convert.ToInt32(varnonow));
        cmdPilotsaycalismis.Parameters.Add("@pilotsaycalismis", SqlDbType.Int);
        cmdPilotsaycalismis.Parameters["@pilotsaycalismis"].Direction = ParameterDirection.Output;
        cmdPilotsaycalismis.ExecuteNonQuery();
        string pilotsaycalismis = cmdPilotsaycalismis.Parameters["@pilotsaycalismis"].Value.ToString();
        cmdPilotsaycalismis.Dispose();
        if (pilotsaycalismis.ToString() == "" || pilotsaycalismis.ToString() == "0") { pilotsaycalismis = "1"; }
        toptoppilot =  Convert.ToDecimal(pilotsaycalismis);

        //owa hesabı
        string owah = "0.0000";
        if (toptopissure != 0 & toptopissay != 0)
        {
            owah = (toptopissure / decimal.Parse(toptopissay.ToString())).ToString();
        }


        //opa hesabı
        string opah = "0.0000";
        if (toptopissure != 0 & toptopissay != 0)
        {
            opah = (toptopissure / toptoppilot).ToString();
        }

        Lwid.Text = AnaKlas.varidhesapla();
        Lwstart.Text = AnaKlas.varbaslangic().ToString();
        Lwfinish.Text = AnaKlas.varbitis().ToString();
        Ljobs.Text = toptopissay.ToString();
        Lwork.Text = toptopissure.ToString();
        Lowa.Text = opah.Substring(0, 4) + " hrs.";




        using (PilotdbEntities entbir = new PilotdbEntities())  // [daysayi] asc, [sevensayi] desc,.OrderBy(b=> b.daysayi).ThenByDescending(b=> b.sevensayi).ThenBy(b => b.yorulma)
        {
            var veri = from b in entbir.pilotvardiyas.ToList().Where(b => b.varno == varnonow).OrderBy(b => b.yorulma).ThenBy(b => b.yorulmalast) select b;

            int max = Convert.ToInt32(veri.Max(a => a.yorulma));
            if (max == 0) max = 1;
            foreach (var a in veri)
            {
                string ttdd = Convert.ToString((150 * a.yorulma) / max);
                decimal p = decimal.Parse(ttdd);
                a.Percentage = Convert.ToInt32(p);

                decimal issuresi = decimal.Parse(a.toplamissuresi.ToString());
                int issayisi = Convert.ToInt32(a.toplamissayisi);
                if (issayisi == 0) issayisi = 1;
                string owa = Convert.ToString(issuresi / issayisi);
                a.owa = owa.Substring(0, 4);

                int isimkno = Convert.ToInt32(a.kapno.ToString());
                SqlCommand cmdisimal = new SqlCommand("Select girisistasyon from pilotlar where kapno =" + isimkno, baglanti);
                a.girisist = cmdisimal.ExecuteScalar().ToString();
                cmdisimal.Dispose();

                SqlCommand cmdvaridbul = new SqlCommand("Select varid from pilotlar where kapno =" + isimkno, baglanti);
                a.varidbul = cmdvaridbul.ExecuteScalar().ToString();
                cmdvaridbul.Dispose();
				
				SqlCommand cmdembul = new SqlCommand("Select emekli from pilotlar where kapno =" + isimkno, baglanti);
                a.embul = cmdembul.ExecuteScalar().ToString();
                cmdembul.Dispose();
            }


            GridView1.DataSource = veri.ToList();
            GridView1.DataBind();



            var veridarica = from d in veri.Where(d => d.girisist == "1").Where(f => f.embul == "No").Where(e => e.varidbul == AnaKlas.varidhesapla()).ToList() select d;
            List<pilotvardiya> daricasort = new List<pilotvardiya>();
            daricasort = veridarica.ToList();

            //if (varidnow == "2")
            //{

            //    int i = 0;
            //    int k = 0;
            //    foreach (var e in veridarica)
            //    {
            //        if (k < 3)// 3 kişi için silme işlemi, darıcadan siliniyor
            //        {
            //            if (e.kapno == 31)
            //            {
            //                i = i + 1;
            //            }
            //            else
            //            {
            //                //e.kapno aktarılan kapno bunun respist ini değiştir ki haftaya adı doğru yere çıksın ama bu vardiya değişim tuşuna basılınca olacak.


            //                daricasort.RemoveAt(i);

            //                k = k + 1;

            //            }

            //        }
            //    }
            //}
            //int k = 0;
            //foreach (var f in veridarica) 
            //{
            //    if (k < daricasort.Count)
            //    {
            //        if (f.varidbul != "1")
            //        {
            //            daricasort.RemoveAt(k);
            //        }
            //    }
            //    k = k + 1;
            //}

            GridView1d.DataSource = daricasort; // darica sort list
            GridView1d.DataBind();

            var veriyarimca = from g in veri.Where(g => g.girisist == "2").Where(f => f.embul == "No").Where(e => e.varidbul == AnaKlas.varidhesapla()).ToList() select g;
            List<pilotvardiya> yarimcasort = new List<pilotvardiya>();
            yarimcasort = veriyarimca.ToList();
            //if (varidnow == "2")
            //{ // yarımcaya ekleniyor sayılar aynı olacak
            //    yarimcasort.AddRange(veridarica.Where(h => h.kapno != 31).Take(3));
            //}
            //int kk = 0;
            //foreach (var f in veriyarimca)
            //{
            //    if (kk < yarimcasort.Count)
            //    {
            //        if (f.varidbul != "1")
            //        {
            //            yarimcasort.RemoveAt(kk);
            //        }
            //    }
            //    kk = kk + 1;
            //}

            GridView1y.DataSource = yarimcasort.ToList().OrderBy(b => b.yorulma).ThenBy(b => b.yorulmalast);// yarimca sort list
            GridView1y.DataBind();

        }
        baglanti.Close();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView1.SelectedIndex)
        {
            e.Cancel = true;
            GridView1.SelectedIndex = -1;
            Chartoranlason();
            Chartoranlaonceki();
            Chartoranlaonceki2();
        }
    }

    private void Chartoranlaonceki()
    {
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand varokucmd = new SqlCommand("Select max(varno) from pilotlar where varno <(select max(varno) from pilotlar) ", baglanti);
        string varnonow = varokucmd.ExecuteScalar().ToString();
        varokucmd.Dispose();


        //string varnonow = AnaKlas.varnohesaplaonceki();
        //string varidnow = AnaKlas.varidhesaplaonceki();

        int toptopissay = 0;
        decimal toptopissure = 0;
        decimal toptoppilot = 0;
        int bak = 0;

        //yaşanan vardiya toplam bilgisi okuma 
        SqlCommand cmdvarcek = new SqlCommand("Select COUNT(*) from pilotvardiya where varno =" + varnonow, baglanti);
        bak = Convert.ToInt32(cmdvarcek.ExecuteScalar().ToString());
        if (bak > 0)
        {
            SqlCommand cmdvardegeroku = new SqlCommand("SP_Pilotvardiya_TopTop_is_sur_pilot", baglanti);
            cmdvardegeroku.CommandType = CommandType.StoredProcedure;
            cmdvardegeroku.Parameters.AddWithValue("@varno", Convert.ToInt32(varnonow));
            SqlDataReader vardr = cmdvardegeroku.ExecuteReader();

            if (vardr.HasRows)
            {
                while (vardr.Read())
                {
                    toptopissay = Convert.ToInt32(vardr["toptopissay"].ToString());
                    toptopissure = Convert.ToDecimal(vardr["toptopissure"].ToString());
                }
            }

            vardr.Close();
            cmdvardegeroku.Dispose();

        }
        cmdvarcek.Dispose();

        SqlCommand cmdPilotsaycalismis = new SqlCommand("SP_Pilotsaycalismis", baglanti);
        cmdPilotsaycalismis.CommandType = CommandType.StoredProcedure;
        cmdPilotsaycalismis.Parameters.AddWithValue("@varno", Convert.ToInt32(varnonow));
        cmdPilotsaycalismis.Parameters.Add("@pilotsaycalismis", SqlDbType.Int);
        cmdPilotsaycalismis.Parameters["@pilotsaycalismis"].Direction = ParameterDirection.Output;
        cmdPilotsaycalismis.ExecuteNonQuery();
        string pilotsaycalismis = cmdPilotsaycalismis.Parameters["@pilotsaycalismis"].Value.ToString();
        cmdPilotsaycalismis.Dispose();
        if (pilotsaycalismis.ToString() == "" || pilotsaycalismis.ToString() == "0") { pilotsaycalismis = "1"; }
        toptoppilot = Convert.ToDecimal(pilotsaycalismis);


        //owa hesabı
        string owah = "0.0000";
        if (toptopissure != 0 & toptopissay != 0)
        {
            owah = (toptopissure / decimal.Parse(toptopissay.ToString())).ToString();
        }


        //opa hesabı
        string opah = "0.0000";
        if (toptopissure != 0 & toptopissay != 0)
        {
            opah = (toptopissure / toptoppilot).ToString();
        }

        Lwoid.Text = AnaKlas.varidhesaplaonceki();
        Lwstartonceki.Text = AnaKlas.varbaslangiconceki().ToString();
        Lwfinishonceki.Text = AnaKlas.varbitisonceki().ToString();
        Lojobs.Text = toptopissay.ToString();
        Lowork.Text = toptopissure.ToString();
        Lowao.Text = opah.Substring(0, 4) + " hrs.";



        using (PilotdbEntities entbir = new PilotdbEntities())
        {
            var veri = from b in entbir.pilotvardiyas.ToList().Where(b => b.varno == varnonow).OrderBy(b => b.yorulma).ThenBy(b => b.yorulmalast) select b;

            int max = Convert.ToInt32(veri.Max(a => a.yorulma));
            if (max == 0) max = 1;
            foreach (var a in veri)
            {
                string ttdd = Convert.ToString((150 * a.yorulma) / max);
                decimal p = decimal.Parse(ttdd);
                a.Percentage = Convert.ToInt32(p);

                decimal issuresi = decimal.Parse(a.toplamissuresi.ToString());
                int issayisi = Convert.ToInt32(a.toplamissayisi);
                if (issayisi == 0) issayisi = 1;
                string owa = Convert.ToString(issuresi / issayisi);
                a.owa = owa.Substring(0, 4);

                int isimkno = Convert.ToInt32(a.kapno.ToString());
                SqlCommand cmdisimal = new SqlCommand("Select girisistasyon from pilotlar where kapno =" + isimkno, baglanti);
                a.girisist = cmdisimal.ExecuteScalar().ToString();
                cmdisimal.Dispose();

                SqlCommand cmdvaridbul = new SqlCommand("Select varid from pilotlar where kapno =" + isimkno, baglanti);
                a.varidbul = cmdvaridbul.ExecuteScalar().ToString();
                cmdvaridbul.Dispose();
				
				SqlCommand cmdembul = new SqlCommand("Select emekli from pilotlar where kapno =" + isimkno, baglanti);
                a.embul = cmdembul.ExecuteScalar().ToString();
                cmdembul.Dispose();
            }

            GridView2.DataSource = veri.ToList();
            GridView2.DataBind();

            var veridarica = from d in veri.Where(d => d.girisist == "1").Where(f => f.embul == "No").Where(e => e.varidbul == AnaKlas.varidhesaplaonceki()).ToList() select d;
            List<pilotvardiya> daricasort = new List<pilotvardiya>();
            daricasort = veridarica.ToList();

            //if (varidnow == "2")
            //{
            //    int i = 0;
            //    int k = 0;
            //    foreach (var e in veridarica)
            //    {
            //        if (k <  3)// 3 kişi için silme işlemi, darıcadan siliniyor
            //        {
            //            if (e.kapno == 31)
            //            {
            //                i = i + 1;
            //            }
            //            else
            //            {
            //                daricasort.RemoveAt(i);

            //                k = k + 1;
            //            }
            //        }
            //    }
            //}

            GridView2d.DataSource = daricasort;// darica sort list
            GridView2d.DataBind();

            var veriyarimca = from g in veri.Where(g => g.girisist == "2").Where(f => f.embul == "No").Where(e => e.varidbul == AnaKlas.varidhesaplaonceki()).ToList() select g;
            List<pilotvardiya> yarimcasort = new List<pilotvardiya>();
            yarimcasort = veriyarimca.ToList();

            GridView2y.DataSource = yarimcasort.ToList().OrderBy(b => b.yorulma).ThenBy(b => b.yorulmalast);// yarimca sort list
            GridView2y.DataBind();

        }
        baglanti.Close();
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView2.SelectedIndex)
        {
            e.Cancel = true;
            GridView2.SelectedIndex = -1;
            Chartoranlason();
            Chartoranlaonceki();
            Chartoranlaonceki2();
        }
    }

    private void Chartoranlaonceki2()
    {
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand varokucmd = new SqlCommand("Select max(varno) from pilotlar where varno <(select max(varno) from pilotlar where varno <(select max(varno) from pilotlar)) ", baglanti);
        string varnonow = varokucmd.ExecuteScalar().ToString();
        varokucmd.Dispose();

        //string varnonow = AnaKlas.varnohesaplaonceki2();
        //string varidnow = AnaKlas.varidhesaplaonceki2();
        int toptopissay = 0;
        decimal toptopissure = 0;
        decimal toptoppilot = 0;
        int bak = 0;

        //yaşanan vardiya toplam bilgisi okuma 
        SqlCommand cmdvarcek = new SqlCommand("Select COUNT(*) from pilotvardiya where varno =" + varnonow, baglanti);
        bak = Convert.ToInt32(cmdvarcek.ExecuteScalar().ToString());
        if (bak > 0)
        {
            //yaşanan vardiya toplam bilgisi okuma 
            SqlCommand cmdvardegeroku = new SqlCommand("SP_Pilotvardiya_TopTop_is_sur_pilot", baglanti);
            cmdvardegeroku.CommandType = CommandType.StoredProcedure;
            cmdvardegeroku.Parameters.AddWithValue("@varno", Convert.ToInt32(varnonow));
            SqlDataReader vardr = cmdvardegeroku.ExecuteReader();


            if (vardr.HasRows)
            {
                while (vardr.Read())
                {
                    toptopissay = Convert.ToInt32(vardr["toptopissay"].ToString());
                    toptopissure = Convert.ToDecimal(vardr["toptopissure"].ToString());
                }
            }
            vardr.Close();
            cmdvardegeroku.Dispose();
        }
        cmdvarcek.Dispose();

        SqlCommand cmdPilotsaycalismis = new SqlCommand("SP_Pilotsaycalismis", baglanti);
        cmdPilotsaycalismis.CommandType = CommandType.StoredProcedure;
        cmdPilotsaycalismis.Parameters.AddWithValue("@varno", Convert.ToInt32(varnonow));
        cmdPilotsaycalismis.Parameters.Add("@pilotsaycalismis", SqlDbType.Int);
        cmdPilotsaycalismis.Parameters["@pilotsaycalismis"].Direction = ParameterDirection.Output;
        cmdPilotsaycalismis.ExecuteNonQuery();
        string pilotsaycalismis = cmdPilotsaycalismis.Parameters["@pilotsaycalismis"].Value.ToString();
        cmdPilotsaycalismis.Dispose();
        if (pilotsaycalismis.ToString() == "" || pilotsaycalismis.ToString() == "0") { pilotsaycalismis = "1"; }
        toptoppilot = Convert.ToDecimal(pilotsaycalismis);


        //owa hesabı
        string owah = "0.0000";
        if (toptopissure != 0 & toptopissay != 0)
        {
            owah = (toptopissure / decimal.Parse(toptopissay.ToString())).ToString();
        }


        //opa hesabı
        string opah = "0.0000";
        if (toptopissure != 0 & toptopissay != 0)
        {
            opah = (toptopissure / toptoppilot).ToString();
        }

        Lwo2id.Text = AnaKlas.varidhesaplaonceki2();
        Lwstartonceki2.Text = AnaKlas.varbaslangiconceki2().ToString();
        Lwfinishonceki2.Text = AnaKlas.varbitisonceki2().ToString();
        Lo2jobs.Text = toptopissay.ToString();
        Lo2work.Text = toptopissure.ToString();
        Lowao2.Text = opah.Substring(0, 4) + " hrs.";



        using (PilotdbEntities entbir = new PilotdbEntities())
        {
            var veri = from b in entbir.pilotvardiyas.ToList().Where(b => b.varno == varnonow).OrderBy(b => b.yorulma).ThenBy(b => b.yorulmalast) select b;

            int max = Convert.ToInt32(veri.Max(a => a.yorulma));
            if (max == 0) max = 1;
            foreach (var a in veri)
            {
                string ttdd = Convert.ToString((150 * a.yorulma) / max);
                decimal p = decimal.Parse(ttdd);
                a.Percentage = Convert.ToInt32(p);

                decimal issuresi = decimal.Parse(a.toplamissuresi.ToString());
                int issayisi = Convert.ToInt32(a.toplamissayisi);
                if (issayisi == 0) issayisi = 1;
                string owa = Convert.ToString(issuresi / issayisi);
                a.owa = owa.Substring(0, 4);

                int isimkno = Convert.ToInt32(a.kapno.ToString());
                SqlCommand cmdisimal = new SqlCommand("Select girisistasyon from pilotlar where kapno =" + isimkno, baglanti);
                 a.girisist = cmdisimal.ExecuteScalar().ToString();
                cmdisimal.Dispose();

                SqlCommand cmdvaridbul = new SqlCommand("Select varid from pilotlar where kapno =" + isimkno, baglanti);
                a.varidbul = cmdvaridbul.ExecuteScalar().ToString();
                cmdvaridbul.Dispose();
				
				SqlCommand cmdembul = new SqlCommand("Select emekli from pilotlar where kapno =" + isimkno, baglanti);
                a.embul = cmdembul.ExecuteScalar().ToString();
                cmdembul.Dispose();
            }

            GridView3.DataSource = veri.ToList();
            GridView3.DataBind();

            var veridarica = from d in veri.Where(d => d.girisist == "1").Where(f => f.embul == "No").Where(e => e.varidbul == AnaKlas.varidhesaplaonceki2()).ToList() select d;
            List<pilotvardiya> daricasort = new List<pilotvardiya>();
            daricasort = veridarica.ToList();

            GridView3d.DataSource = daricasort;// darica sort list
            GridView3d.DataBind();



            var veriyarimca = from g in veri.Where(g => g.girisist == "2").Where(f => f.embul == "No").Where(e => e.varidbul == AnaKlas.varidhesaplaonceki2()).ToList() select g;
            List<pilotvardiya> yarimcasort = new List<pilotvardiya>();
            yarimcasort = veriyarimca.ToList();

            GridView3y.DataSource = yarimcasort.ToList().OrderBy(b => b.yorulma).ThenBy(b => b.yorulmalast);// yarimca sort list
            GridView3y.DataBind();

        }
        baglanti.Close();

    }
    protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView3.SelectedIndex)
        {
            e.Cancel = true;
            GridView3.SelectedIndex = -1;
            Chartoranlason();
            Chartoranlaonceki();
            Chartoranlaonceki2();
        }
    }


    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"].ToString() == "" || (Session["kapno"] == null))
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else
        {
            AnaKlas.logrecup(Session["kapno"].ToString());
            Session.Abandon();
            Response.Redirect("http://www.monitoringpilot.com");
        }
    }
    protected void LBonline_Click(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(Session["yetki"]) ==1 || Convert.ToInt32(Session["yetki"]) ==2)
        //{

        //}
        //else if (Convert.ToInt32(Session["yetki"]) == 0)
        //{

        //}
        //else 
        if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }
    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        DTloading();
    }


    protected void kapadi_Click(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();
        LinkButton LinkButtonisler = (LinkButton)sender;
        PilotEid.Text = HttpUtility.HtmlDecode(LinkButtonisler.CommandArgument).ToString();
        int PilotEidi = Convert.ToInt32(PilotEid.Text);

        string kapadi = HttpUtility.HtmlDecode(LinkButtonisler.Text).ToString();
        string varno = HttpUtility.HtmlDecode(LinkButtonisler.CommandName).ToString();

        Lblpilotname.Text = kapadi;

        //DTDarica Pilot Geçmiş 
        SqlCommand cmdDTDaricaEkranGecmis = new SqlCommand("SP_DTDaricaYarimcaCanliGecmis", baglanti);
        cmdDTDaricaEkranGecmis.CommandType = CommandType.StoredProcedure;
        cmdDTDaricaEkranGecmis.Parameters.AddWithValue("@secilikapno", PilotEidi);
        cmdDTDaricaEkranGecmis.Parameters.AddWithValue("@varbilvarno", varno);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDTDaricaEkranGecmis;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "vardiyadetay");
        ListView1.DataSource = ds;
        ListView1.DataBind();

        baglanti.Close();

        this.ModalPopupExtenderPilotEdit.Show();
    }

}




