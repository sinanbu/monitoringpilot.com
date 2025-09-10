using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class stations : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (Session["kapno"] == null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Convert.ToInt32(Session["kapno"]) < 0 || Convert.ToInt32(Session["kapno"]) > 999)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "99")
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }

        //SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);
        //if (cmdlogofbak.ExecuteScalar() == null)
        //{
        //    Response.Redirect("https://www.monitoringpilot.com");
        //}

        else if (Convert.ToInt32(Session["yetki"]) == 6)
        {
            Response.Redirect("stn.aspx");
        }

        else
        {


            if (Session["yetki"].ToString() == "9") { LBonline.Enabled = true; }
                else { LBonline.Enabled = false; }


            

            LiteralYaz();

            SqlCommand cmdkapsirano = new SqlCommand("SP_seskapsirano", baglanti);
            cmdkapsirano.CommandType = CommandType.StoredProcedure;
            cmdkapsirano.Parameters.AddWithValue("@kapno", Convert.ToInt32(Session["kapno"]));
            cmdkapsirano.Parameters.Add("@kapsirano", SqlDbType.Int); // 
            cmdkapsirano.Parameters["@kapsirano"].Direction = ParameterDirection.Output;
            cmdkapsirano.ExecuteNonQuery();
            int seskapsirano = Convert.ToInt32(cmdkapsirano.Parameters["@kapsirano"].Value.ToString().Trim());
            cmdkapsirano.Dispose();

            LBSurvey.Visible = false;

            if (seskapsirano < 1000)
            {
                LBmyjobs.Visible = true;
                LBSurvey.Visible =true;
            }
            if (seskapsirano > 2000 && seskapsirano < 3000)
            {
                LBSurvey.Visible = true;
            }


            if (!IsPostBack)
            {
            SqlCommand cmdlogonal1 = new SqlCommand("Select kapadi + ' ' + kapsoyadi as kapadisoyadi from onoffline where yetki='1' and time = 0.001 ", baglanti);
            SqlCommand cmdlogonal2 = new SqlCommand("Select kapadi + ' ' + kapsoyadi as kapadisoyadi from onoffline where yetki='2' and time = 0.001 ", baglanti);

            if (cmdlogonal1.ExecuteScalar() != null) { LBonlined.Text = cmdlogonal1.ExecuteScalar().ToString(); }
            if (cmdlogonal2.ExecuteScalar() != null) { LBonliney.Text = cmdlogonal2.ExecuteScalar().ToString(); }
            cmdlogonal1.Dispose();
            cmdlogonal2.Dispose();

            SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
            cmdPilotismial.CommandType = CommandType.StoredProcedure;
            cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(Session["kapno"]));
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

            LBonline.Text = kapadisoyadi; // +Session.Timeout.ToString();

            if (Session["yetki"].ToString() == "0")
            {
                mainmanu2.Visible = false;
                mainmanu3.Visible = false;
                mainmanu4.Visible = false;
                mainmanu6.Visible = false;

            }

			if (Session["kapno"].ToString()=="18" || Session["kapno"].ToString()=="70" || Session["kapno"].ToString()=="107" || Session["kapno"].ToString() == "92" || Session["kapno"].ToString() == "97" || Session["kapno"].ToString() == "98")
			{
                mainmanu2.Visible = true;
                mainmanu3.Visible = true;
                mainmanu6.Visible = true;
            }
            else if (Session["kapno"].ToString() == "96" || Session["kapno"].ToString() == "100" || Session["kapno"].ToString() == "112")
            {
                    mainmanu1.Visible = false;
                    mainmanu2.Visible = true;
                    mainmanu4.Visible = true;
                    mainmanu7.Visible = false;
                }
			

              LblVarid.Text = "Watch:" + varbilvarid.Text;
            LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
            LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
            LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
            LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();

                //anket acılış



                if (seskapsirano < 999) {
					SqlCommand cmd = new SqlCommand("select count(aktif) from argem where aktif ='1' ", baglanti);
                    int aktifargesay = Convert.ToInt32(cmd.ExecuteScalar());

                    if (aktifargesay ==1 )
                    {
                    string onaybakbir = "1";

                    SqlCommand onaybak = new SqlCommand("Select onay from argedetay where (anketno=(select max(anketno) from argedetay) and kapno='" + Convert.ToInt32(Session["kapno"]) + "')  ", baglanti);
                    if (onaybak.ExecuteScalar() != null)
                    { onaybakbir = onaybak.ExecuteScalar().ToString(); }
                        onaybak.Dispose();

					SqlCommand cmdlinkle = new SqlCommand("Select aciklama from argem where id=(select max(id) from argem) order by id desc", baglanti);
					string linkle = cmdlinkle.ExecuteScalar().ToString();
					cmdlinkle.Dispose();

					if (onaybakbir == "0") { Response.Redirect(linkle); }
					}
				}

                DTloading();
            }
        }

        baglanti.Close();

    }  

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        //SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand onaybak = new SqlCommand("Select onay from argedetay where (anketno=(select max(anketno) from argedetay) and kapno='" + Convert.ToInt32(Session["kapno"]) + "')  ", baglanti);
        //string onaybakbir = onaybak.ExecuteScalar().ToString();
        //onaybak.Dispose();
        //baglanti.Close();

        //if (onaybakbir == "0") { LBSurvey.BackColor = Color.Red; }

        LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString(); 
        LiteralYaz();
        DTloading();
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

        SqlCommand cmdJcount = new SqlCommand("SP_Toplamis_Anlik", baglanti);
        cmdJcount.CommandType = CommandType.StoredProcedure;
        cmdJcount.Parameters.AddWithValue("@varno", varbilvarno.Text);
        cmdJcount.Parameters.Add("@topissay", SqlDbType.Int); // 
        cmdJcount.Parameters["@topissay"].Direction = ParameterDirection.Output;
        cmdJcount.ExecuteNonQuery();
        LblCounter.Text = cmdJcount.Parameters["@topissay"].Value.ToString();
        cmdJcount.Dispose();

        decimal vartotfatik = 0;

        if (Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2) < 24)
        {
        decimal toptopzihinfat = 0;
        decimal toptopissure = 0;

        //_______________yaşanan vardiya toplam bilgisi okuma 

        if (LblCounter.Text != "0")
        {
            //yaşanan vardiya toplam bilgisi okuma 
            SqlCommand cmdvardegeroku = new SqlCommand("SP_Pilotvardiya_TopTop_is_sur_pilot", baglanti);
            cmdvardegeroku.CommandType = CommandType.StoredProcedure;
            cmdvardegeroku.Parameters.AddWithValue("@varno", Convert.ToInt32(varbilvarno.Text));
            SqlDataReader vardr = cmdvardegeroku.ExecuteReader();

            if (vardr.HasRows)
            {
                while (vardr.Read())
                {
                    toptopissure = Convert.ToDecimal(vardr["toptopissure"].ToString());// vardiyada yapılan işlerin toplam kaç saat sürdüğü
                    toptopzihinfat = Convert.ToDecimal(vardr["toptopzihinfat"].ToString()); // vardiyadaki toplam zihinsel fat
                }
            }
            vardr.Close();
            cmdvardegeroku.Dispose();
        }

        SqlCommand cmdPilotsaycalisan = new SqlCommand("SP_Pilotsaycalisan", baglanti);
        cmdPilotsaycalisan.CommandType = CommandType.StoredProcedure;
        cmdPilotsaycalisan.Parameters.Add("@pilotsay", SqlDbType.Int);
        cmdPilotsaycalisan.Parameters["@pilotsay"].Direction = ParameterDirection.Output;
        cmdPilotsaycalisan.ExecuteNonQuery();
        string pilotsay = cmdPilotsaycalisan.Parameters["@pilotsay"].Value.ToString();
        cmdPilotsaycalisan.Dispose();

        double reelgecen = (DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours;
        if (pilotsay.ToString() == "" || pilotsay.ToString() == "0") { pilotsay = "1"; }
        if (reelgecen.ToString() == "" || reelgecen.ToString() == "0") { reelgecen = 1; }
        vartotfatik = (toptopissure + toptopzihinfat);
        vartotfatik = vartotfatik / Convert.ToDecimal(reelgecen);
        vartotfatik = vartotfatik / Convert.ToDecimal(pilotsay);
        vartotfatik = vartotfatik * 3 / 5;
        }
        else
        { 
        //00000000000000000000000000

        SqlCommand cmdPilotavgF = new SqlCommand("SP_Pilotvardiya_calpilot_avgFatik", baglanti);
        cmdPilotavgF.CommandType = CommandType.StoredProcedure;
        cmdPilotavgF.Parameters.AddWithValue("@varno", varbilvarno.Text);
        cmdPilotavgF.Parameters.Add("@varavgF", SqlDbType.Float);
        cmdPilotavgF.Parameters["@varavgF"].Direction = ParameterDirection.Output;
        cmdPilotavgF.ExecuteNonQuery();
        string sonuc = cmdPilotavgF.Parameters["@varavgF"].Value.ToString();
        if (sonuc == null || sonuc == "")
        { vartotfatik = 0; }
        else { vartotfatik = Convert.ToDecimal(sonuc); }
        cmdPilotavgF.Dispose();
            //00000000000000000000000000
        }

        Lblindicator1.Text = "1";
        Lblindicator2.Text = "2";
        Lblindicator3.Text = "3";
        Lblindicator4.Text = "4";
        Lblindicator5.Text = "5";
        Lblindicator6.Text = "6";
        Lblindicator7.Text = "";
        Lblindicator1.Style.Add("width", "10px");
        Lblindicator2.Style.Add("width", "10px");
        Lblindicator3.Style.Add("width", "10px");
        Lblindicator4.Style.Add("width", "10px");
        Lblindicator5.Style.Add("width", "10px");
        Lblindicator6.Style.Add("width", "10px");
        Lblindicator7.Style.Add("width", "10px");
        Lblindicator1.Style.Add("background-color", "#ffb3fd");
        Lblindicator2.Style.Add("background-color", "#77ff6d");
        Lblindicator3.Style.Add("background-color", "#ffff00");
        Lblindicator4.Style.Add("background-color", "#ff7700");
        Lblindicator5.Style.Add("background-color", "#ff0000");
        Lblindicator6.Style.Add("background-color", "#505050");



        if (vartotfatik > 0 && vartotfatik < Convert.ToDecimal(0.20))
        {
            Lblindicator1.Text = " WorkLoad: " + Math.Round(vartotfatik, 4).ToString() + " :Efortless";
            Lblindicator1.Style.Add("width", "172px");
        }
        else if (vartotfatik > Convert.ToDecimal(0.20) && vartotfatik < Convert.ToDecimal(0.25))
        {
            Lblindicator2.Text = " WorkLoad: " + Math.Round(vartotfatik, 4).ToString() + " :Easy";
            Lblindicator2.Style.Add("width", "172px");
        }
        else if (vartotfatik > Convert.ToDecimal(0.25) && vartotfatik < Convert.ToDecimal(0.30))
        {
            Lblindicator3.Text = " WorkLoad: " + Math.Round(vartotfatik, 4).ToString() + " :Normal";
            Lblindicator3.Style.Add("width", "172px");
        }
        else if (vartotfatik > Convert.ToDecimal(0.30) && vartotfatik < Convert.ToDecimal(0.35))
        {
            Lblindicator4.Text = " WorkLoad: " + Math.Round(vartotfatik, 4).ToString() + " :Hard";
            Lblindicator4.Style.Add("width", "172px");
        }
        else if (vartotfatik > Convert.ToDecimal(0.35) && vartotfatik < Convert.ToDecimal(0.40))
        {
            Lblindicator5.Text = " WorkLoad: " + Math.Round(vartotfatik, 4).ToString() + " :Overload";
            Lblindicator5.Style.Add("width", "172px");
        }
        else if (vartotfatik > Convert.ToDecimal(0.40) && vartotfatik < 1)
        {
            Lblindicator6.Text = " WorkLoad: " + Math.Round(vartotfatik, 4).ToString() + " :Risky";
            Lblindicator6.Style.Add("width", "172px");
        }

        else
        {
            Lblindicator7.Text = "";
        }






        if (DateTime.Now.Hour == 11 || DateTime.Now.Hour == 23)
        {
            SqlCommand cmdmailtimeoku = new SqlCommand("SP_Up_dlsayoku", baglanti);
            cmdmailtimeoku.CommandType = CommandType.StoredProcedure;
            cmdmailtimeoku.Parameters.Add("@mailtime", SqlDbType.VarChar, 2);
            cmdmailtimeoku.Parameters["@mailtime"].Direction = ParameterDirection.Output;
            cmdmailtimeoku.ExecuteNonQuery();
            string mailtime = cmdmailtimeoku.Parameters["@mailtime"].Value.ToString().Trim();
            cmdmailtimeoku.Dispose();

            if (Convert.ToInt32(mailtime) == DateTime.Now.Hour)
            {
                mailtime = DateTime.Now.AddHours(12).Hour.ToString();

                SqlCommand cmdmailtimeup = new SqlCommand("SP_Up_dlsay", baglanti);
                cmdmailtimeup.CommandType = CommandType.StoredProcedure;
                cmdmailtimeup.Parameters.AddWithValue("@mailtime", mailtime);
                cmdmailtimeup.ExecuteNonQuery();
                cmdmailtimeup.Dispose();

                sendmail();
            }
        }


        if (DateTime.Now.Hour == 11)
        {
            SqlCommand cmdmailtimeoku = new SqlCommand("SP_Up_dlsayjsroku", baglanti);
            cmdmailtimeoku.CommandType = CommandType.StoredProcedure;
            cmdmailtimeoku.Parameters.Add("@jsrtime", SqlDbType.VarChar, 2);
            cmdmailtimeoku.Parameters["@jsrtime"].Direction = ParameterDirection.Output;
            cmdmailtimeoku.ExecuteNonQuery();
            string jsrtime = cmdmailtimeoku.Parameters["@jsrtime"].Value.ToString().Trim();
            cmdmailtimeoku.Dispose();

            if (Convert.ToInt32(jsrtime) == (DateTime.Now).Day)
            {
                jsrtime = (DateTime.Now.AddDays(1).Day).ToString();

                SqlCommand cmdmailtimeup = new SqlCommand("SP_Up_dlsayjsrup", baglanti);
                cmdmailtimeup.CommandType = CommandType.StoredProcedure;
                cmdmailtimeup.Parameters.AddWithValue("@jsrtime", jsrtime);
                cmdmailtimeup.ExecuteNonQuery();
                cmdmailtimeup.Dispose();

                upjsreport();
            }
        }


        SqlCommand cmdmailtimeoku3 = new SqlCommand("SP_Up_dlsayokuhour", baglanti);
        cmdmailtimeoku3.CommandType = CommandType.StoredProcedure;
        cmdmailtimeoku3.Parameters.Add("@saydarica", SqlDbType.VarChar, 2);
        cmdmailtimeoku3.Parameters["@saydarica"].Direction = ParameterDirection.Output;
        cmdmailtimeoku3.ExecuteNonQuery();
        string saydarica = cmdmailtimeoku3.Parameters["@saydarica"].Value.ToString().Trim();
        cmdmailtimeoku3.Dispose();

        if (Convert.ToInt32(saydarica) == DateTime.Now.Hour)   //... saatte bir kere kayıt yapar
        {
            saydarica = DateTime.Now.AddHours(1).Hour.ToString();

            SqlCommand cmdmailtimeup = new SqlCommand("SP_Up_dlsayhour", baglanti);
            cmdmailtimeup.CommandType = CommandType.StoredProcedure;
            cmdmailtimeup.Parameters.AddWithValue("@saydarica", saydarica);
            cmdmailtimeup.ExecuteNonQuery();
            cmdmailtimeup.Dispose();


            //SqlCommand cmdwlup = new SqlCommand("SP_Up_wload", baglanti);
            //cmdwlup.CommandType = CommandType.StoredProcedure;
            //cmdwlup.Parameters.AddWithValue("@varno", varbilvarno.Text);
            //cmdwlup.Parameters.AddWithValue("@dd", DateTime.Now.Day);
            //cmdwlup.Parameters.AddWithValue("@mm", DateTime.Now.Month);
            //cmdwlup.Parameters.AddWithValue("@yyyy", DateTime.Now.Year);
            //cmdwlup.Parameters.AddWithValue("@saat", DateTime.Now.Hour);
            //cmdwlup.Parameters.AddWithValue("@kayitani", DateTime.Now);
            //cmdwlup.Parameters.AddWithValue("@toptopissure", toptopissure);
            //cmdwlup.Parameters.AddWithValue("@toptopzihinfat", toptopzihinfat);
            //cmdwlup.Parameters.AddWithValue("@reelgecen", reelgecen*3600);//saniye
            //cmdwlup.Parameters.AddWithValue("@pilotsay", pilotsay);
            //cmdwlup.Parameters.AddWithValue("@wload", vartotfatik);
            //cmdwlup.ExecuteNonQuery();
            //cmdwlup.Dispose();

            sendmaillive();

        }


        baglanti.Close();
        baglanti.Dispose();
    }

    private void DTloading()
    {
    SqlConnection baglanti = AnaKlas.baglan();
   
    //DTDarica Canlı Ekran 
    SqlCommand cmdDTDaricaEkran = new SqlCommand("SP_DTDaricaCanliEkran", baglanti);
    cmdDTDaricaEkran.CommandType = CommandType.StoredProcedure;
    cmdDTDaricaEkran.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
    cmdDTDaricaEkran.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
    SqlDataAdapter adapter = new SqlDataAdapter();
    adapter.SelectCommand = cmdDTDaricaEkran;
    DataSet ds = new DataSet();
    adapter.Fill(ds, "pilotlar");
    DLDarica.DataSource = ds;
    DLDarica.DataBind();

    //DTYarimca Canlı Ekran 
    SqlCommand cmdDTYarimcaEkran = new SqlCommand("SP_DTYarimcaCanliEkran", baglanti);
    cmdDTYarimcaEkran.CommandType = CommandType.StoredProcedure;
    cmdDTYarimcaEkran.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
    cmdDTYarimcaEkran.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
    SqlDataAdapter adaptery = new SqlDataAdapter();
    adaptery.SelectCommand = cmdDTYarimcaEkran;
    DataSet dsy = new DataSet();
    adaptery.Fill(dsy, "pilotlar");
    DLDaricay.DataSource = dsy;
    DLDaricay.DataBind();


        Litizinler.Text = "";
        Litizinler.Style.Add("color", "#111111");  
        SqlCommand cmdizinalanlar = new SqlCommand("SP_Tumizinalanlar", baglanti);
        cmdizinalanlar.CommandType = CommandType.StoredProcedure;
        cmdizinalanlar.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
        string kapadisoyadi = "";
        string degismeciadisoyadi = "";
        string gorevde = "";
        SqlDataReader dr = cmdizinalanlar.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                kapadisoyadi = dr["kapadisoyadi"].ToString();
                degismeciadisoyadi = dr["degismeciadisoyadi"].ToString();
                gorevde = dr["gorevde"].ToString();


                if (gorevde == "0")        // değişmeci gelmiş (izinde=1, gorevde=0 olur)
                {
                    //if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " (" + degismeciadisoyadi + ") - "; }
                }
                if (gorevde == "1")// direkt izindedir (izinde=1, gorevde=1 olur)
                {
                    if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " - "; }
                }

                if (gorevde == "2")// hasta olmuş      (izinde=1, gorevde=2 olur)
                {
                    if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " (Off Duty) - "; }
                }

                if (gorevde == "3")// add pilot        (izinde=1, gorevde=3 olur)
                {
                    if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " (AddedPilot) - "; }
                }


            }
        }
        dr.Close();
        cmdizinalanlar.Dispose();

        if (Litizinler.Text.Length > 3)
        {
            Litizinler.Text = Litizinler.Text.Substring(0, Litizinler.Text.Length - 2);
        }

          //duyuru
        SqlCommand duyurubak = new SqlCommand("SP_duyurubak", baglanti);
        duyurubak.CommandType = CommandType.StoredProcedure;
        duyurubak.Parameters.Add("@duyurusonuc", SqlDbType.NVarChar, 700);
        duyurubak.Parameters["@duyurusonuc"].Direction = ParameterDirection.Output;
        duyurubak.ExecuteNonQuery();
        LblDuyuru.Text = "Yeni Duyuru Yok";
        if (string.IsNullOrEmpty(duyurubak.Parameters["@duyurusonuc"].Value.ToString().Trim()) != true)
        { LblDuyuru.Text = duyurubak.Parameters["@duyurusonuc"].Value.ToString().Trim(); }
        duyurubak.Dispose();

    
                baglanti.Close();       
    }
    protected void DLDarica_ItemDataBound(object sender, DataListItemEventArgs e)
    { 
        SqlConnection baglanti = AnaKlas.baglan();
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {


            //eta fat bilgisi
            if (Session["kapno"].ToString() == "28")
            {
                string istasyoncikis = "";
                string istasyongelis = "";
                decimal issuresi = 0;
                float issuresif = 0;

                Label Lblimonokop = (Label)e.Item.FindControl("LblKapno");
                string kapnobul = Lblimonokop.Text;
                Label Lblfatikkop = (Label)e.Item.FindControl("Lblyorulma");

                //===========================iş süressi gerçek başladı
                SqlCommand cmdisoku = new SqlCommand("SP_PilotGemisiIstCikisGelis2li", baglanti);
                cmdisoku.CommandType = CommandType.StoredProcedure;
                cmdisoku.Parameters.AddWithValue("@secilikapno", kapnobul);
                SqlDataReader varisreader = cmdisoku.ExecuteReader();
                if (varisreader.HasRows)
                {
                    while (varisreader.Read())
                    {
                        istasyoncikis = varisreader["istasyoncikis"].ToString();
                        istasyongelis = varisreader["istasyongelis"].ToString();
                    }
                }
                varisreader.Close();
                cmdisoku.Dispose();

                if(istasyoncikis=="") { }
                else { 

                TimeSpan ts = Convert.ToDateTime(istasyongelis) - Convert.ToDateTime(istasyoncikis);
                issuresif = float.Parse(ts.TotalHours.ToString());
                issuresi = Convert.ToDecimal(ts.TotalHours);

                /////******yorulma hesabı
                string kalkislimani = "Yelkenkaya";
                string yanasmalimani = "Yelkenkaya";
                string kalkisrihtimi = "0";
                string yanasmarihtimi = "0";

                SqlCommand cmdisokuup = new SqlCommand("SP_PilotGemisiKalkisVaris4lu", baglanti);
                cmdisokuup.CommandType = CommandType.StoredProcedure;
                cmdisokuup.Parameters.AddWithValue("@secilikapno", kapnobul);
                SqlDataReader varisreaderup = cmdisokuup.ExecuteReader();
                if (varisreaderup.HasRows)
                {
                    while (varisreaderup.Read())
                    {
                        kalkislimani = varisreaderup["binisyeri"].ToString();
                        yanasmalimani = varisreaderup["inisyeri"].ToString();
                        kalkisrihtimi = varisreaderup["binisrihtim"].ToString();
                        yanasmarihtimi = varisreaderup["inisrihtim"].ToString();
                    }
                }
                varisreaderup.Close();
                cmdisokuup.Dispose();

                if (kalkislimani.Substring(0, 4) == "Ters")
                { kalkislimani = "Tersane Beşiktaş"; }
                else if (kalkislimani.Substring(0, 4) == "Kosb")
                { kalkislimani = "Kosbaş"; }
                else if (kalkislimani == "Trial Voyage")
                { kalkislimani = "Yelkenkaya"; }
                else if (kalkislimani == "SafiPort")
                { kalkislimani = "Demir-İzmit"; }


                if (yanasmalimani.Substring(0, 4) == "Ters")
                { yanasmalimani = "Tersane Beşiktaş"; }
                else if (yanasmalimani.Substring(0, 4) == "Kosb")
                { yanasmalimani = "Kosbaş"; }
                else if (yanasmalimani == "Trial Voyage")
                { yanasmalimani = "Yelkenkaya"; }
                else if (yanasmalimani == "SafiPort")
                { yanasmalimani = "Demir-İzmit"; }

                SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                cmdLimannoal.CommandType = CommandType.StoredProcedure;
                cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
                cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int); // 
                cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                cmdLimannoal.ExecuteNonQuery();
                int portnokalkis = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString().Trim());
                cmdLimannoal.Dispose();

                SqlCommand cmdLimannoal2 = new SqlCommand("SP_Lim_Limannoal", baglanti);
                cmdLimannoal2.CommandType = CommandType.StoredProcedure;
                cmdLimannoal2.Parameters.AddWithValue("@limanadi", yanasmalimani);
                cmdLimannoal2.Parameters.Add("@limanno", SqlDbType.Int); // 
                cmdLimannoal2.Parameters["@limanno"].Direction = ParameterDirection.Output;
                cmdLimannoal2.ExecuteNonQuery();
                int portnokalvar = Convert.ToInt32(cmdLimannoal2.Parameters["@limanno"].Value.ToString().Trim());
                cmdLimannoal2.Dispose();

                double fatikzihin = 0;

                if (portnokalkis > 0 && portnokalkis < 900) // limandan kalkis
                {
                    if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                    { fatikzihin = 1.9; }
                    else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                    { fatikzihin = 1.9; }
                    else // sadece kalk
                    { fatikzihin = 1.9; }
                }

                else if (portnokalkis > 1000 && portnokalkis < 1099) // demirden kalkis
                {
                    if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                    { fatikzihin = 1.9; }
                    else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                    { fatikzihin = 1.9; }
                    else // sadece demir al
                    { fatikzihin = 1.9; }
                }

                else if (portnokalvar > 0 && portnokalvar < 900) // ykaya-limana yanaşma
                { fatikzihin = 1.9; }

                else if (portnokalvar > 1000 && portnokalvar < 1099) // ykaya-demir at
                { fatikzihin = 1.9; }

                if (yanasmarihtimi.IndexOf("Dbl.Anc") != -1)
                { fatikzihin = 1.9; }

                if (yanasmarihtimi.IndexOf("Dock") != -1)
                { fatikzihin = 1.9; }

                if (kalkisrihtimi.IndexOf("Dock") != -1)
                { fatikzihin = 1.9; }

                //gece fatiği
                // 1. istasyoncikis 0 den önce ve istasyongelis 6 den sonra
                double gecefatiq = 0;
                float issuresigf = 0;

                // 2. istasyoncikis 0 den önce ve istasyongelis 0 ile 6 arası

                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) > 12) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 24))
                {
                    if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
                    {
                        TimeSpan tsgf = Convert.ToDateTime(istasyongelis) - (Convert.ToDateTime(istasyoncikis).Date.AddDays(1).AddMinutes(-1));
                        issuresigf = float.Parse(tsgf.TotalHours.ToString());
                        gecefatiq = (issuresigf * 35) / 100;
                    }
                }
                // 3. istasyoncikis 0 ile 6 arası ve istasyongelis 0 ile 6 arası
                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
                {
                    if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
                    {
                        gecefatiq = (issuresif * 35) / 100;
                    }
                }
                // 4. istasyoncikis 0 ile 6 arası ve istasyongelis 6 den sonra
                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
                {
                    if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) > 5) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 18))
                    {
                        TimeSpan tsgf = Convert.ToDateTime(istasyongelis).Date.AddDays(1).AddMinutes(-1).AddHours(-18) - (Convert.ToDateTime(istasyoncikis));
                        issuresigf = float.Parse(tsgf.TotalHours.ToString());
                        gecefatiq = (issuresigf * 35) / 100;
                    }
                }

                fatikzihin = fatikzihin + gecefatiq;

                /////**********************************

                decimal yorulmad = (issuresi + Convert.ToDecimal(fatikzihin)) / Convert.ToDecimal((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours);
                yorulmad = (yorulmad * 3) / 5;


                    Lblfatikkop.ToolTip= Lblfatikkop.Text;
                    Lblfatikkop.Text = (Convert.ToDecimal(Lblfatikkop.Text) + Math.Round(yorulmad,4)).ToString();
                }

            }





            Label LblKapnokopya = (Label)e.Item.FindControl("LblKapno");
            String secilikapno = LblKapnokopya.Text;
            ListView LWkopy = (ListView)e.Item.FindControl("ListView1");
            if (LWkopy != null)
            {
                //DTDarica Pilot Geçmiş pgecac
                SqlCommand cmdDTDaricaEkranGecmis = new SqlCommand("SP_DTDaricaYarimcaCanliGecmis", baglanti);
                cmdDTDaricaEkranGecmis.CommandType = CommandType.StoredProcedure;
                cmdDTDaricaEkranGecmis.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdDTDaricaEkranGecmis.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDTDaricaEkranGecmis;
                DataSet ds = new DataSet();
                adapter.Fill(ds, "vardiyadetay");
                LWkopy.DataSource = ds;
                LWkopy.DataBind();

            }



            //Fatique last 24 açık gri ve total de koyu gri font color değiş
            Label lblcolor = (Label)e.Item.FindControl("LBpgecmis");
            Label lblyorul = (Label)e.Item.FindControl("Lblyorulma");
            Label lbllastdy = (Label)e.Item.FindControl("Lbllastday");
            if (float.Parse(lblyorul.Text) > 0.50) // toplamdaki yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }
            if (float.Parse(lbllastdy.Text) < 10) // last24 yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }

            //dc ve tbp yazdırma
            Label Lbldckop = (Label)e.Item.FindControl("Lbldc");
            Label Lbltbpkop = (Label)e.Item.FindControl("Lbltbp");

            Label Lblimonokopi = (Label)e.Item.FindControl("Lblimono");

            SqlCommand cmdgemibuli = new SqlCommand("SP_Isliste_gemibilgifmimo", baglanti);
            cmdgemibuli.CommandType = CommandType.StoredProcedure;
            cmdgemibuli.Parameters.AddWithValue("@imono", Convert.ToInt32(Lblimonokopi.Text));
            SqlDataReader gemireader = cmdgemibuli.ExecuteReader();

            if (gemireader.HasRows)
            {
                while (gemireader.Read())
                {
                    Lbldckop.Text = gemireader["tehlikeliyuk"].ToString();
                    Lbltbpkop.Text = gemireader["bilgi"].ToString() + "-" + gemireader["draft"].ToString();
                }
            }
            gemireader.Close();

            //yarımcaya ait gemi trunc
            LinkButton Lblgemiadicolor = (LinkButton)e.Item.FindControl("Lblgemiadi");
            Label Lblinisyerigizcolor = (Label)e.Item.FindControl("Lblinisyerigiz");

            SqlCommand cmdRespistal = new SqlCommand("SP_RespistFmPort", baglanti);
            cmdRespistal.CommandType = CommandType.StoredProcedure;
            cmdRespistal.Parameters.AddWithValue("@seciliport", Lblinisyerigizcolor.Text);
            cmdRespistal.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
            cmdRespistal.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
            cmdRespistal.ExecuteNonQuery();
            string bagliist = cmdRespistal.Parameters["@bagliistasyon"].Value.ToString().Trim();
            cmdRespistal.Dispose();

            if (Lblinisyerigizcolor.Text.ToLower() == "yelkenkaya")
            { Lblgemiadicolor.Style.Add("color", "#6632FF"); }// yelkenkaya gemi rengi farklı
            else if (bagliist == "2")
            { Lblgemiadicolor.Style.Add("color", "#b85503"); }
            else if (bagliist == "1")
            { Lblgemiadicolor.Style.Add("color", "#11255E"); }


            //darıca yarımca ait flag normal şablon
            LinkButton Lblbayrakcolor = (LinkButton)e.Item.FindControl("Lblbayrak");
            { Lblbayrakcolor.Style.Add("color", "#11255E"); }


            //terste kalan pillot renkli
            if (Convert.ToInt16(DateTime.Now.Day.ToString()) == Convert.ToInt16(varbiter.Text.Substring(0, 2))) 
            { 
                Label LblKapnocolor = (Label)e.Item.FindControl("LblKapno");
            SqlCommand cmdgiristal = new SqlCommand("SP_Pilotgirisist", baglanti);
            cmdgiristal.CommandType = CommandType.StoredProcedure;
            cmdgiristal.Parameters.AddWithValue("@kapno", LblKapnocolor.Text.Trim());
            cmdgiristal.Parameters.Add("@girisist", SqlDbType.Char, 1);
            cmdgiristal.Parameters["@girisist"].Direction = ParameterDirection.Output;
            cmdgiristal.ExecuteNonQuery();
            string girisist = cmdgiristal.Parameters["@girisist"].Value.ToString().Trim();
            cmdgiristal.Dispose();
            Label LBpgecmiscolor = (Label)e.Item.FindControl("LBpgecmis");
            if (girisist == "2")
            { LBpgecmiscolor.Style.Add("color", "#b85503"); }
            }

            // port plan için yelkenkaya bold degil
            LinkButton Lblportcolorb = (LinkButton)e.Item.FindControl("binisport");
            if (Lblportcolorb.Text.ToLower() == "yelkenkaya")
            { Lblportcolorb.Style.Add("font-weight", "normal"); 
             Lblportcolorb.Style.Add("cursor", "default"); }
            LinkButton Lblportcolori = (LinkButton)e.Item.FindControl("inisport");
            if (Lblportcolori.Text.ToLower() == "yelkenkaya")
            { Lblportcolori.Style.Add("font-weight", "normal"); 
             Lblportcolori.Style.Add("cursor", "default"); }

            // tuş göster gizle ayarlanıyor
            Label mylabel = (Label)e.Item.FindControl("lblDurum");
            String puan = mylabel.Text;

             // operator yetki 1 değilse gizle

                e.Item.FindControl("LblIstasyoncikis").Visible = (false);
                e.Item.FindControl("LblPob").Visible = (false);
                e.Item.FindControl("LblPoff").Visible = (false);
                e.Item.FindControl("LblIstasyongelis").Visible = (false);

                e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                e.Item.FindControl("LblPobeta").Visible = (false);
                e.Item.FindControl("LblPoffeta").Visible = (false);
                e.Item.FindControl("LblIstasyongeliseta").Visible = (false);

                if (puan == "1")
                {
                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (true);
                    e.Item.FindControl("LblPobeta").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                }

                else if (puan == "2")
                {
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPobeta").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);

                }

                else if (puan == "3")
                {
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPob").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                }

                else if (puan == "4")
                {
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPob").Visible = (true);
                    e.Item.FindControl("LblPoff").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                }

            if (puan == "0")
            {
                Label LblNokop = (Label)e.Item.FindControl("LblNo");
                LblNokop.Style.Add("color", "#ee1111");
                LblNokop.Style.Add("font-weight", "bold");
            }

        }
        baglanti.Close();
    }

    protected void DLDarica_ItemCommand(object source, DataListCommandEventArgs e) 
    {
            SqlConnection baglanti = AnaKlas.baglan();

        String Btnbinisportadi = "Yelkenkaya";
        String Btninisportadi = "Yelkenkaya";

        LinkButton Btnbinisport = (LinkButton)e.Item.FindControl("binisport");
        if (string.IsNullOrEmpty(Btnbinisport.Text) != true)
        {
            Btnbinisportadi = (Btnbinisport.Text).ToString();
        }

        LinkButton Btninisport = (LinkButton)e.Item.FindControl("inisport");
        if (string.IsNullOrEmpty(Btninisport.Text) != true)
        {
            Btninisportadi = (Btninisport.Text).ToString();
        }

        if (e.CommandName == "linkleb")
        {

            if (dogrulinkyap(Btnbinisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btnbinisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btnbinisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }
        if (e.CommandName == "linklei")
        {

            if (dogrulinkyap(Btninisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btninisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btninisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }


        Label Lblimonokop = (Label)e.Item.FindControl("Lblimono");
        if (e.CommandName == "gemilybd")
        {
            //yol oku
            SqlCommand lybbak = new SqlCommand("SP_lybbakimo", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@imono", Convert.ToInt32(Lblimonokop.Text));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            string lybyol = lybbak.Parameters["@lybyol"].Value.ToString().Trim();
            lybbak.Dispose();

            if (string.IsNullOrEmpty(lybyol) != true)
            {
                Response.Redirect(lybyol, false);
            }
        }

        if (e.CommandName == "gemiord")
        {
            //yol oku
            SqlCommand orbak = new SqlCommand("SP_orbakimo", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@imono", Convert.ToInt32(Lblimonokop.Text));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            string oryol = orbak.Parameters["@oryol"].Value.ToString().Trim();
            orbak.Dispose();

            if (string.IsNullOrEmpty(oryol) != true)
            {
                Response.Redirect(oryol, false);
            }
        }


        baglanti.Close();
        
    }

    protected void Buttonlbaddcancel_Click(object sender, EventArgs e)
    {
        MPEdrawing.Controls.Clear();
        MPEdrawing2.Controls.Clear();


    }
  

    protected void Buttonlbaddnotes_Click(object sender, EventArgs e)
    {
        MPEdrawing.Controls.Clear();

        string yetkili = "";
        string telno = "";
        string cepno = "";
        string uyari = "";
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdisokuup = new SqlCommand("SP_LimanBilgilerSec", baglanti);
        cmdisokuup.CommandType = CommandType.StoredProcedure;
        cmdisokuup.Parameters.AddWithValue("@limanadi", Buttonlbaddnotes.CommandName);
        SqlDataReader dr = cmdisokuup.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                yetkili = dr["yetkili"].ToString();
                telno = dr["telno"].ToString();
                cepno = dr["cepno"].ToString();
                uyari = dr["uyari"].ToString();
            }
        }
        dr.Close();
        cmdisokuup.Dispose();
        baglanti.Close();

        Lblnote0.Text = Buttonlbaddnotes.CommandName;
        Lblnote1.Text = yetkili;
        Lblnote2.Text = telno;
        Lblnote3.Text = cepno;
        Lblnote4.Text = uyari;

this.MPEdrawing2.Show();
    }

    protected void Buttonlbaddplan_Click(object sender, EventArgs e)
    {

        MPEdrawing2.Controls.Clear();
        this.MPEdrawing.Show();
    }
    protected void DLDaricay_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {

            //eta fat bilgisi
            if (Session["kapno"].ToString() == "28")
            {
                string istasyoncikis = "";
                string istasyongelis = "";
                decimal issuresi = 0;
                float issuresif = 0;

                Label Lblimonokop = (Label)e.Item.FindControl("LblKapnoy");
                string kapnobul = Lblimonokop.Text;
                Label Lblfatikkop = (Label)e.Item.FindControl("Lblyorulmay");

                //===========================iş süressi gerçek başladı
                SqlCommand cmdisoku = new SqlCommand("SP_PilotGemisiIstCikisGelis2li", baglanti);
                cmdisoku.CommandType = CommandType.StoredProcedure;
                cmdisoku.Parameters.AddWithValue("@secilikapno", kapnobul);
                SqlDataReader varisreader = cmdisoku.ExecuteReader();
                if (varisreader.HasRows)
                {
                    while (varisreader.Read())
                    {
                        istasyoncikis = varisreader["istasyoncikis"].ToString();
                        istasyongelis = varisreader["istasyongelis"].ToString();
                    }
                }
                varisreader.Close();
                cmdisoku.Dispose();

                if (istasyoncikis == "") { }
                else
                {

                    TimeSpan ts = Convert.ToDateTime(istasyongelis) - Convert.ToDateTime(istasyoncikis);
                    issuresif = float.Parse(ts.TotalHours.ToString());
                    issuresi = Convert.ToDecimal(ts.TotalHours);

                    /////******yorulma hesabı
                    string kalkislimani = "Yelkenkaya";
                    string yanasmalimani = "Yelkenkaya";
                    string kalkisrihtimi = "0";
                    string yanasmarihtimi = "0";

                    SqlCommand cmdisokuup = new SqlCommand("SP_PilotGemisiKalkisVaris4lu", baglanti);
                    cmdisokuup.CommandType = CommandType.StoredProcedure;
                    cmdisokuup.Parameters.AddWithValue("@secilikapno", kapnobul);
                    SqlDataReader varisreaderup = cmdisokuup.ExecuteReader();
                    if (varisreaderup.HasRows)
                    {
                        while (varisreaderup.Read())
                        {
                            kalkislimani = varisreaderup["binisyeri"].ToString();
                            yanasmalimani = varisreaderup["inisyeri"].ToString();
                            kalkisrihtimi = varisreaderup["binisrihtim"].ToString();
                            yanasmarihtimi = varisreaderup["inisrihtim"].ToString();
                        }
                    }
                    varisreaderup.Close();
                    cmdisokuup.Dispose();

                    if (kalkislimani.Substring(0, 4) == "Ters")
                    { kalkislimani = "Tersane Beşiktaş"; }
                    else if (kalkislimani.Substring(0, 4) == "Kosb")
                    { kalkislimani = "Kosbaş"; }
                    else if (kalkislimani == "Trial Voyage")
                    { kalkislimani = "Yelkenkaya"; }
                    else if (kalkislimani == "SafiPort")
                    { kalkislimani = "Demir-İzmit"; }


                    if (yanasmalimani.Substring(0, 4) == "Ters")
                    { yanasmalimani = "Tersane Beşiktaş"; }
                    else if (yanasmalimani.Substring(0, 4) == "Kosb")
                    { yanasmalimani = "Kosbaş"; }
                    else if (yanasmalimani == "Trial Voyage")
                    { yanasmalimani = "Yelkenkaya"; }
                    else if (yanasmalimani == "SafiPort")
                    { yanasmalimani = "Demir-İzmit"; }

                    SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                    cmdLimannoal.CommandType = CommandType.StoredProcedure;
                    cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
                    cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int); // 
                    cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                    cmdLimannoal.ExecuteNonQuery();
                    int portnokalkis = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString().Trim());
                    cmdLimannoal.Dispose();

                    SqlCommand cmdLimannoal2 = new SqlCommand("SP_Lim_Limannoal", baglanti);
                    cmdLimannoal2.CommandType = CommandType.StoredProcedure;
                    cmdLimannoal2.Parameters.AddWithValue("@limanadi", yanasmalimani);
                    cmdLimannoal2.Parameters.Add("@limanno", SqlDbType.Int); // 
                    cmdLimannoal2.Parameters["@limanno"].Direction = ParameterDirection.Output;
                    cmdLimannoal2.ExecuteNonQuery();
                    int portnokalvar = Convert.ToInt32(cmdLimannoal2.Parameters["@limanno"].Value.ToString().Trim());
                    cmdLimannoal2.Dispose();

                    double fatikzihin = 0;

                    if (portnokalkis > 0 && portnokalkis < 900) // limandan kalkis
                    {
                        if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                        { fatikzihin = 1.9; }
                        else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                        { fatikzihin = 1.9; }
                        else // sadece kalk
                        { fatikzihin = 1.9; }
                    }

                    else if (portnokalkis > 1000 && portnokalkis < 1099) // demirden kalkis
                    {
                        if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                        { fatikzihin = 1.9; }
                        else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                        { fatikzihin = 1.9; }
                        else // sadece demir al
                        { fatikzihin = 1.9; }
                    }

                    else if (portnokalvar > 0 && portnokalvar < 900) // ykaya-limana yanaşma
                    { fatikzihin = 1.9; }

                    else if (portnokalvar > 1000 && portnokalvar < 1099) // ykaya-demir at
                    { fatikzihin = 1.9; }

                    if (yanasmarihtimi.IndexOf("Dbl.Anc") != -1)
                    { fatikzihin = 1.9; }

                    if (yanasmarihtimi.IndexOf("Dock") != -1)
                    { fatikzihin = 1.9; }

                    if (kalkisrihtimi.IndexOf("Dock") != -1)
                    { fatikzihin = 1.9; }

                    //gece fatiği
                    // 1. istasyoncikis 0 den önce ve istasyongelis 6 den sonra
                    double gecefatiq = 0;
                    float issuresigf = 0;

                    // 2. istasyoncikis 0 den önce ve istasyongelis 0 ile 6 arası

                    if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) > 12) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 24))
                    {
                        if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
                        {
                            TimeSpan tsgf = Convert.ToDateTime(istasyongelis) - (Convert.ToDateTime(istasyoncikis).Date.AddDays(1).AddMinutes(-1));
                            issuresigf = float.Parse(tsgf.TotalHours.ToString());
                            gecefatiq = (issuresigf * 35) / 100;
                        }
                    }
                    // 3. istasyoncikis 0 ile 6 arası ve istasyongelis 0 ile 6 arası
                    if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
                    {
                        if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
                        {
                            gecefatiq = (issuresif * 35) / 100;
                        }
                    }
                    // 4. istasyoncikis 0 ile 6 arası ve istasyongelis 6 den sonra
                    if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
                    {
                        if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) > 5) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 18))
                        {
                            TimeSpan tsgf = Convert.ToDateTime(istasyongelis).Date.AddDays(1).AddMinutes(-1).AddHours(-18) - (Convert.ToDateTime(istasyoncikis));
                            issuresigf = float.Parse(tsgf.TotalHours.ToString());
                            gecefatiq = (issuresigf * 35) / 100;
                        }
                    }

                    fatikzihin = fatikzihin + gecefatiq;

                    /////**********************************

                    decimal yorulmad = (issuresi + Convert.ToDecimal(fatikzihin)) / Convert.ToDecimal((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours);
                    yorulmad = (yorulmad * 3) / 5;



                    Lblfatikkop.ToolTip = Lblfatikkop.Text;
                    Lblfatikkop.Text = (Convert.ToDecimal(Lblfatikkop.Text) + Math.Round(yorulmad, 4)).ToString();

                }

            }


            Label LblKapnokopya = (Label)e.Item.FindControl("LblKapnoy");
            String secilikapno = LblKapnokopya.Text;
            ListView LWkopy = (ListView)e.Item.FindControl("ListView1y");
            if (LWkopy != null)
            {
                //DTDarica Pilot Geçmiş 
                SqlCommand cmdDTDaricaEkranGecmis = new SqlCommand("SP_DTDaricaYarimcaCanliGecmis", baglanti);
                cmdDTDaricaEkranGecmis.CommandType = CommandType.StoredProcedure;
                cmdDTDaricaEkranGecmis.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdDTDaricaEkranGecmis.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDTDaricaEkranGecmis;
                DataSet ds = new DataSet();
                adapter.Fill(ds, "vardiyadetay");
                LWkopy.DataSource = ds;
                LWkopy.DataBind();

            }

            //Fatique last 24 açık gri ve total de koyu gri font color değiş
            Label lblcolor = (Label)e.Item.FindControl("LBpgecmisy");
            Label lblyorul = (Label)e.Item.FindControl("Lblyorulmay");
            Label lbllastdy = (Label)e.Item.FindControl("Lbllastdayy");
            if (float.Parse(lblyorul.Text) > 0.50) // toplamdaki yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }
            if (float.Parse(lbllastdy.Text) < 10) // last24 yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }


            //dc ve tbp yazdırma
            Label Lbldcykop = (Label)e.Item.FindControl("Lbldcy");
            Label Lbltbpykop = (Label)e.Item.FindControl("Lbltbpy");

            Label Lblimonokopiy = (Label)e.Item.FindControl("Lblimonoy");

            SqlCommand cmdgemibuli = new SqlCommand("SP_Isliste_gemibilgifmimo", baglanti);
            cmdgemibuli.CommandType = CommandType.StoredProcedure;
            cmdgemibuli.Parameters.AddWithValue("@imono", Convert.ToInt32(Lblimonokopiy.Text));
            SqlDataReader gemireader = cmdgemibuli.ExecuteReader();

            if (gemireader.HasRows)
            {
                while (gemireader.Read())
                {
                    Lbldcykop.Text = gemireader["tehlikeliyuk"].ToString();
                    Lbltbpykop.Text = gemireader["bilgi"].ToString() + "-" + gemireader["draft"].ToString();
                }
            }
            gemireader.Close();


            //darıcaya ait gemi mavi
            LinkButton Lblgemiadicolor = (LinkButton)e.Item.FindControl("Lblgemiadiy");
            Label Lblinisyerigizcolor = (Label)e.Item.FindControl("Lblinisyerigizy");

            SqlCommand cmdRespistal = new SqlCommand("SP_RespistFmPort", baglanti);
            cmdRespistal.CommandType = CommandType.StoredProcedure;
            cmdRespistal.Parameters.AddWithValue("@seciliport", Lblinisyerigizcolor.Text);
            cmdRespistal.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
            cmdRespistal.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
            cmdRespistal.ExecuteNonQuery();
            string bagliist = cmdRespistal.Parameters["@bagliistasyon"].Value.ToString().Trim();
            cmdRespistal.Dispose();

            if (Lblinisyerigizcolor.Text.ToLower() == "yelkenkaya")
            { Lblgemiadicolor.Style.Add("color", "#6632FF"); }// yelkenkaya gemi rengi farklı
            else if (bagliist == "1")
            { Lblgemiadicolor.Style.Add("color", "#11255E"); }
            else if (bagliist == "2")
            { Lblgemiadicolor.Style.Add("color", "#b85503"); }


            //darıca yarımca ait flag normal şablon
            LinkButton Lblbayrakcolor = (LinkButton)e.Item.FindControl("Lblbayraky");
            { Lblbayrakcolor.Style.Add("color", "#b85503"); }



            //terste kalan pillot renkli
            if (Convert.ToInt16(DateTime.Now.Day.ToString()) == Convert.ToInt16(varbiter.Text.Substring(0, 2)))
            {
                Label LblKapnocolor = (Label)e.Item.FindControl("LblKapnoy");
                SqlCommand cmdgiristal = new SqlCommand("SP_Pilotgirisist", baglanti);
                cmdgiristal.CommandType = CommandType.StoredProcedure;
                cmdgiristal.Parameters.AddWithValue("@kapno", LblKapnocolor.Text.Trim());
                cmdgiristal.Parameters.Add("@girisist", SqlDbType.Char, 1);
                cmdgiristal.Parameters["@girisist"].Direction = ParameterDirection.Output;
                cmdgiristal.ExecuteNonQuery();
                string girisist = cmdgiristal.Parameters["@girisist"].Value.ToString().Trim();
                cmdgiristal.Dispose();
                Label LBpgecmiscolor = (Label)e.Item.FindControl("LBpgecmisy");
                if (girisist == "1")
                { LBpgecmiscolor.Style.Add("color", "#11255E"); }
            }


            // port plan için yelkenkaya bold degil
            LinkButton Lblportcolorb = (LinkButton)e.Item.FindControl("binisport");
            if (Lblportcolorb.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolorb.Style.Add("font-weight", "normal");
                Lblportcolorb.Style.Add("cursor", "default");
            }
            LinkButton Lblportcolori = (LinkButton)e.Item.FindControl("inisport");
            if (Lblportcolori.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolori.Style.Add("font-weight", "normal");
                Lblportcolori.Style.Add("cursor", "default");
            }


            // tuş göster gizle ayarlanıyor
            Label mylabel = (Label)e.Item.FindControl("lblDurumy");
            String puan = mylabel.Text;
           // operator yetki 2 değilse
            
                e.Item.FindControl("LblIstasyoncikisy").Visible = (false);
                e.Item.FindControl("LblPoby").Visible = (false);
                e.Item.FindControl("LblPoffy").Visible = (false);
                e.Item.FindControl("LblIstasyongelisy").Visible = (false);

                e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                e.Item.FindControl("LblPobetay").Visible = (false);
                e.Item.FindControl("LblPoffetay").Visible = (false);
                e.Item.FindControl("LblIstasyongelisetay").Visible = (false);

                if (puan == "1")
                {
                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (true);
                    e.Item.FindControl("LblPobetay").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                }

                else if (puan == "2")
                {
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPobetay").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);

                }

                else if (puan == "3")
                {
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPoby").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                }

                else if (puan == "4")
                {
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPoby").Visible = (true);
                    e.Item.FindControl("LblPoffy").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                }

            if (puan == "0")
            {
                Label LblNokop = (Label)e.Item.FindControl("LblNoy");
                LblNokop.Style.Add("color", "#ee1111");
                LblNokop.Style.Add("font-weight", "bold");
            }
        }
        baglanti.Close();
    }

    protected void DLDaricay_ItemCommand(object source, DataListCommandEventArgs e) 
    {

            SqlConnection baglanti = AnaKlas.baglan();
        String Btnbinisportadi = "Yelkenkaya";
        String Btninisportadi = "Yelkenkaya";

        LinkButton Btnbinisport = (LinkButton)e.Item.FindControl("binisport");
        if (string.IsNullOrEmpty(Btnbinisport.Text) != true)
        {
            Btnbinisportadi = (Btnbinisport.Text).ToString();
        }

        LinkButton Btninisport = (LinkButton)e.Item.FindControl("inisport");
        if (string.IsNullOrEmpty(Btninisport.Text) != true)
        {
            Btninisportadi = (Btninisport.Text).ToString();
        }

        if (e.CommandName == "linkleb")
        {

            if (dogrulinkyap(Btnbinisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btnbinisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btnbinisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }
        if (e.CommandName == "linklei")
        {

            if (dogrulinkyap(Btninisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btninisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btninisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }

        Label Lblimonokop = (Label)e.Item.FindControl("Lblimonoy");
        if (e.CommandName == "gemilybd")
        {
            //yol oku
            SqlCommand lybbak = new SqlCommand("SP_lybbakimo", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@imono", Convert.ToInt32(Lblimonokop.Text));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            string lybyol = lybbak.Parameters["@lybyol"].Value.ToString().Trim();
            lybbak.Dispose();

            if (string.IsNullOrEmpty(lybyol) != true)
            {
                Response.Redirect(lybyol, false);
            }
        }

        if (e.CommandName == "gemiord")
        {
            //yol oku
            SqlCommand orbak = new SqlCommand("SP_orbakimo", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@imono", Convert.ToInt32(Lblimonokop.Text));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            string oryol = orbak.Parameters["@oryol"].Value.ToString().Trim();
            orbak.Dispose();

            if (string.IsNullOrEmpty(oryol) != true)
            {
                Response.Redirect(oryol, false);
            }
        }


        baglanti.Close();
        
    }

    public string dogrulinkyap(string Metin)
    {
        string dogruad = Metin;
        dogruad = Temizle(dogruad);
        dogruad = TemizleINGoldu(dogruad);

        if (dogruad == "tersanecemre") { dogruad = "cemre"; }
        else if (dogruad == "topcularferibot" || dogruad == "yalovaroro") { dogruad = "topcuroro"; }
        else if (dogruad == "tuprasfaz1") { dogruad = "tuprasf1"; }
        else if (dogruad == "tuprasfaz2") { dogruad = "tuprasf2"; }
        else if (dogruad == "tuprasfaz3") { dogruad = "tuprasf3"; }
        else if (dogruad == "tuprasplatform") { dogruad = "tuprasplt"; }
        else if (dogruad == "safiport") { dogruad = "safi"; }
        else if (dogruad == "tersanebesiktas") { dogruad = "besiktas"; }
        else if (dogruad == "tersaneozata") { dogruad = "ozata"; }
        else if (dogruad == "tersanesefine") { dogruad = "sefine"; }
        else if (dogruad == "tersanetersan" || dogruad == "tersanenaveks" || dogruad == "tersaneernese" || dogruad == "tersanegirgin" || dogruad == "tersanecakirlar" || dogruad == "tersanegemdok" || dogruad == "tersanekinsizler" || dogruad == "tersaneistanbul") { dogruad = "tersantillnaveks"; }
        else if (dogruad == "opay") { dogruad = "opayopet"; }
        else if (dogruad == "petline") { dogruad = "camar"; }
        else if (dogruad == "gubretas" || dogruad == "turkuaz" || dogruad == "marmaratersanesi" || dogruad == "rota") { dogruad = "gubreturkirrota"; }
        else if (dogruad == "shell" || dogruad == "koruma" || dogruad == "aktas") { dogruad = "shelkorumaaktas"; }
        else if (dogruad == "efesan" || dogruad == "total") { dogruad = "efesantotal"; }
        else if (dogruad == "tersanebayrak" || dogruad == "topcularferibot") { dogruad = "topcubayrak"; }
        else if (dogruad == "tersanegemak" || dogruad == "tersanekocatepe") { dogruad = "gemakkocatepe"; }
        else if (dogruad == "tersanehatsan" || dogruad == "tersaneyasarsan" || dogruad == "tersanekalkavan") { dogruad = "hatyasakalk"; }
        else if (dogruad == "tersanemardas" || dogruad == "tersaneseltas" || dogruad == "tersaneduzgit" || dogruad == "tersanedenta" || dogruad == "tersanefurtrans" || dogruad == "tersanedogusan") { dogruad = "kalktobes"; }
        else if (dogruad == "tersanehurriyet" || dogruad == "tersanegisan" || dogruad == "tersaneaykin" || dogruad == "tersaneorucoglu" || dogruad == "tersanegurdesan") { dogruad = "hurtogur"; }
        else if (dogruad == "tersanehercelik" || dogruad == "tersaneozlemdeniz" || dogruad == "tersaneyuksel" || dogruad == "tersanekurbangemi" || dogruad == "tersaneturkoglu" || dogruad == "tersanebreko" || dogruad == "tersanebogazici" || dogruad == "tersanepalmali" || dogruad == "tersaneak" || dogruad == "tersanealtintas") { dogruad = "altocemre"; }
        else if (dogruad.Substring(0, 3) == "kos") { dogruad = "kosbas"; }



        return dogruad;

    }
    public string Temizle(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace(" ", "");
        deger = deger.Replace("'", "");
        deger = deger.Replace("<", "");
        deger = deger.Replace(">", "");
        deger = deger.Replace("&", "");
        deger = deger.Replace("(", "");
        deger = deger.Replace(")", "");
        deger = deger.Replace(";", "");
        deger = deger.Replace("?", "");
        deger = deger.Replace("-", "");
        deger = deger.Replace(".", "");
        return deger;
    }
    public string TemizleINGoldu(string Metin)
    {
        string deger = Metin;

        deger = deger.Replace("ç", "c");
        deger = deger.Replace("ğ", "g");
        deger = deger.Replace("ı", "i");
        deger = deger.Replace("ö", "o");
        deger = deger.Replace("ş", "s");
        deger = deger.Replace("ü", "u");

        deger = deger.Trim();
        return deger;
    }

    protected void upjsreport()
    {

            SqlConnection baglanti = AnaKlas.baglan();
        
        SqlCommand cmdPilotsaycalisan = new SqlCommand("SP_Pilotsaycalisan", baglanti);
        cmdPilotsaycalisan.CommandType = CommandType.StoredProcedure;
        cmdPilotsaycalisan.Parameters.Add("@pilotsay", SqlDbType.Int);
        cmdPilotsaycalisan.Parameters["@pilotsay"].Direction = ParameterDirection.Output;
        cmdPilotsaycalisan.ExecuteNonQuery();
        string pilotsay = cmdPilotsaycalisan.Parameters["@pilotsay"].Value.ToString();
        cmdPilotsaycalisan.Dispose();

        SqlCommand cmdmansayLimana = new SqlCommand("SP_Canli_ManevraSay_Limana", baglanti);
        cmdmansayLimana.CommandType = CommandType.StoredProcedure;
        cmdmansayLimana.Parameters.Add("@mansayLimana", SqlDbType.Int);
        cmdmansayLimana.Parameters["@mansayLimana"].Direction = ParameterDirection.Output;
        cmdmansayLimana.ExecuteNonQuery();
        string mansayLimana = cmdmansayLimana.Parameters["@mansayLimana"].Value.ToString();
        cmdmansayLimana.Dispose();

        
        SqlCommand cmdmansayDemEskYal = new SqlCommand("SP_Canli_ManevraSay_DemEskYal", baglanti);
        cmdmansayDemEskYal.CommandType = CommandType.StoredProcedure;
        cmdmansayDemEskYal.Parameters.Add("@mansayDemEskYal", SqlDbType.Int);
        cmdmansayDemEskYal.Parameters["@mansayDemEskYal"].Direction = ParameterDirection.Output;
        cmdmansayDemEskYal.ExecuteNonQuery();
        string mansayDemEskYal = cmdmansayDemEskYal.Parameters["@mansayDemEskYal"].Value.ToString();
        cmdmansayDemEskYal.Dispose();


        SqlCommand cmdmansayDemHer = new SqlCommand("SP_Canli_ManevraSay_DemHer", baglanti);
        cmdmansayDemHer.CommandType = CommandType.StoredProcedure;
        cmdmansayDemHer.Parameters.Add("@mansayDemHer", SqlDbType.Int);
        cmdmansayDemHer.Parameters["@mansayDemHer"].Direction = ParameterDirection.Output;
        cmdmansayDemHer.ExecuteNonQuery();
        string mansayDemHer = cmdmansayDemHer.Parameters["@mansayDemHer"].Value.ToString();
        cmdmansayDemHer.Dispose();


        SqlCommand cmdmansayDemYariz = new SqlCommand("SP_Canli_ManevraSay_DemYariz", baglanti);
        cmdmansayDemYariz.CommandType = CommandType.StoredProcedure;
        cmdmansayDemYariz.Parameters.Add("@mansayDemYariz", SqlDbType.Int);
        cmdmansayDemYariz.Parameters["@mansayDemYariz"].Direction = ParameterDirection.Output;
        cmdmansayDemYariz.ExecuteNonQuery();
        string mansayDemYariz = cmdmansayDemYariz.Parameters["@mansayDemYariz"].Value.ToString();
        cmdmansayDemYariz.Dispose();

        SqlCommand cmdmansayHerSay = new SqlCommand("SP_Isliste_NSL_HerSay", baglanti);
        cmdmansayHerSay.CommandType = CommandType.StoredProcedure;
        cmdmansayHerSay.Parameters.Add("@HerDemSay", SqlDbType.Int);
        cmdmansayHerSay.Parameters["@HerDemSay"].Direction = ParameterDirection.Output;
        cmdmansayHerSay.ExecuteNonQuery();
        string HerDemSay = cmdmansayHerSay.Parameters["@HerDemSay"].Value.ToString();
        cmdmansayHerSay.Dispose();


        SqlCommand cmdjsrokuA = new SqlCommand("SP_jsrAtis", baglanti);//vardetaydan iş sayısı al
        cmdjsrokuA.CommandType = CommandType.StoredProcedure;
        cmdjsrokuA.Parameters.AddWithValue("@tarih", TarihYaziYapDMY(DateTime.Now.AddDays(-1)));
        cmdjsrokuA.Parameters.Add("@tis", SqlDbType.Int);
        cmdjsrokuA.Parameters["@tis"].Direction = ParameterDirection.Output;
        cmdjsrokuA.ExecuteNonQuery();
        string tis = cmdjsrokuA.Parameters["@tis"].Value.ToString().Trim();//  oncegunissay;
        cmdjsrokuA.Dispose();

        SqlCommand cmdjsrokuB = new SqlCommand("SP_jsrBsis", baglanti);//vardetaydan safi iş sayısı al
        cmdjsrokuB.CommandType = CommandType.StoredProcedure;
        cmdjsrokuB.Parameters.AddWithValue("@tarih", TarihYaziYapDMY(DateTime.Now.AddDays(-1)));
        cmdjsrokuB.Parameters.Add("@sis", SqlDbType.Int);
        cmdjsrokuB.Parameters["@sis"].Direction = ParameterDirection.Output;
        cmdjsrokuB.ExecuteNonQuery();
        string sis = cmdjsrokuB.Parameters["@sis"].Value.ToString().Trim();//  oncegunissaysafi;
        cmdjsrokuB.Dispose();

        SqlCommand cmdnot1 = new SqlCommand("SP_Isliste_NSL", baglanti);
        cmdnot1.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr1 = cmdnot1.ExecuteReader();
        int no1t = dr1.Cast<object>().Count();
        dr1.Close();
        cmdnot1.Dispose();


        SqlCommand cmdnot2 = new SqlCommand("SP_Isliste_NSL_vipC", baglanti);
        cmdnot2.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr2 = cmdnot2.ExecuteReader();
        int no2t = dr2.Cast<object>().Count();
        dr2.Close();
        cmdnot2.Dispose();


        SqlCommand cmdnot4 = new SqlCommand("SP_Isliste_NSL_ASL", baglanti);
        cmdnot4.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr4 = cmdnot4.ExecuteReader();
        int no4t = dr4.Cast<object>().Count();
        dr4.Close();
        cmdnot4.Dispose();


        SqlCommand cmdnot5 = new SqlCommand("SP_Isliste_NSL_VIP", baglanti);
        cmdnot5.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr5 = cmdnot5.ExecuteReader();
        int no5t = dr5.Cast<object>().Count();
        dr5.Close();
        cmdnot5.Dispose();


        SqlCommand cmdnot6 = new SqlCommand("SP_Isliste_NSL_vipT", baglanti);
        cmdnot6.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr6 = cmdnot6.ExecuteReader();
        int no6t = dr6.Cast<object>().Count();
        dr6.Close();
        cmdnot6.Dispose();

        SqlCommand cmdnot6a = new SqlCommand("SP_Isliste_NSL_YalPort", baglanti);
        cmdnot6a.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr6a = cmdnot6a.ExecuteReader();
        int no6at = dr6a.Cast<object>().Count();
        dr6a.Close();
        cmdnot6a.Dispose();

        SqlCommand cmdnot7 = new SqlCommand("SP_Isliste_NSL_her", baglanti);
        cmdnot7.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr7 = cmdnot7.ExecuteReader();
        int no7t = dr7.Cast<object>().Count();
        dr7.Close();
        cmdnot7.Dispose();


        SqlCommand cmdnot8 = new SqlCommand("SP_Isliste_NSL_YA", baglanti);
        cmdnot8.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr8 = cmdnot8.ExecuteReader();
        int no8t = dr8.Cast<object>().Count();
        dr8.Close();
        cmdnot8.Dispose();


        SqlCommand cmdnot9 = new SqlCommand("SP_Isliste_NSL_YP", baglanti);
        cmdnot9.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr9 = cmdnot9.ExecuteReader();
        int no9t = dr9.Cast<object>().Count();
        dr9.Close();
        cmdnot9.Dispose();


        SqlCommand cmdnot10 = new SqlCommand("SP_Isliste_NSL_Yk", baglanti);
        cmdnot10.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr10 = cmdnot10.ExecuteReader();
        int no10t = dr10.Cast<object>().Count();
        dr10.Close();
        cmdnot10.Dispose();

        ////ok son talep oku
        ////SqlCommand cmdjsrokuiz = new SqlCommand("SP_jsreport_talepiz", baglanti);//vardetaydan son iz al
        ////cmdjsrokuiz.CommandType = CommandType.StoredProcedure;
        ////cmdjsrokuiz.Parameters.AddWithValue("@tarih", TarihYaziYapDMY(DateTime.Now));
        ////cmdjsrokuiz.Parameters.Add("@sontalepiz", SqlDbType.VarChar,16);
        ////cmdjsrokuiz.Parameters["@sontalepiz"].Direction = ParameterDirection.Output;
        ////cmdjsrokuiz.ExecuteNonQuery();
        ////string sontalepiz = cmdjsrokuiz.Parameters["@sontalepiz"].Value.ToString().Trim();
        ////cmdjsrokuiz.Dispose();

        ////SqlCommand cmdjsrokuyal = new SqlCommand("SP_jsreport_talepyal", baglanti);//vardetaydan son al safi
        ////cmdjsrokuyal.CommandType = CommandType.StoredProcedure;
        ////cmdjsrokuyal.Parameters.AddWithValue("@tarih", TarihYaziYapDMY(DateTime.Now));
        ////cmdjsrokuyal.Parameters.Add("@sontalepyal", SqlDbType.VarChar, 16);
        ////cmdjsrokuyal.Parameters["@sontalepyal"].Direction = ParameterDirection.Output;
        ////cmdjsrokuyal.ExecuteNonQuery();
        ////string sontalepyal = cmdjsrokuyal.Parameters["@sontalepyal"].Value.ToString().Trim();
        ////cmdjsrokuyal.Dispose();

        //pilotsay // canlı çalışan izinsizler
        // mansayLimana// canlı limana
        // mansayDemEskYal // canlı demire eskihisr yalova
        // mansayDemHer  // canlı demire hereke
        // mansayDemYariz // canlı demire yar ve izmit
        // HerDemSay // hereke demir sayısı
        // no1t = ; //incoming vessel
        // no2t = ; //contacted vessel
        //no3t = ; //maneuvering vessel
        // no4t = ; //eskihisr yalova demir
        // no5t = ; //dilovası port
        // no6t = ; //yalova tersane
        // no7t = ; //hereke port
        // no8t = ; //hereke yarımca izmit demir
        // no9t = ; //yarımca ports
        // no10t = ; //kosbaş tersaneler

        int yanasiklar = no5t + no6t + no6at + no7t + no9t + no10t + Convert.ToInt32(mansayLimana);
        int herekedemirdekiler = Convert.ToInt32(HerDemSay) + Convert.ToInt32(mansayDemHer);
        int yarizmitdemirdekiler = no8t  + Convert.ToInt32(mansayDemYariz) - Convert.ToInt32(HerDemSay);
        int eskiyalovademirdekiler = no4t + Convert.ToInt32(mansayDemEskYal);
        int gelecekler = no1t + no2t;     

        SqlCommand cmdjsrup = new SqlCommand("SP_jsr_UpAll", baglanti);
        cmdjsrup.CommandType = CommandType.StoredProcedure;
        cmdjsrup.Parameters.AddWithValue("@tarih", TarihYaziYapDMY(DateTime.Now));
        cmdjsrup.Parameters.AddWithValue("@oncegunissay", tis);
        cmdjsrup.Parameters.AddWithValue("@yanasiklar", yanasiklar.ToString());
        cmdjsrup.Parameters.AddWithValue("@eskiyalodemir", eskiyalovademirdekiler.ToString());
        cmdjsrup.Parameters.AddWithValue("@herekedemir", herekedemirdekiler.ToString());
        cmdjsrup.Parameters.AddWithValue("@yarizdemir", yarizmitdemirdekiler.ToString());
        cmdjsrup.Parameters.AddWithValue("@incoming", gelecekler.ToString());
        cmdjsrup.Parameters.AddWithValue("@sontalepiz", "");
        cmdjsrup.Parameters.AddWithValue("@sontalepyal", "");
        cmdjsrup.Parameters.AddWithValue("@sontalepsaf", "");
        cmdjsrup.Parameters.AddWithValue("@dekasis", (Convert.ToInt32(tis) - Convert.ToInt32(sis)).ToString());
        cmdjsrup.Parameters.AddWithValue("@safis", sis);
        cmdjsrup.Parameters.AddWithValue("@pilotsay", pilotsay);
        cmdjsrup.ExecuteNonQuery();
        cmdjsrup.Dispose();

        baglanti.Close();
        
    }

    protected void sendmail()
    {
        SqlConnection baglanti = AnaKlas.baglan();

        string Subject = "Daily Job list / " + DateTime.Now.ToString();
        string Body = "";

        string gemiadi = "";
        string kalkislimani = "";
        string kalkisrihtimi = "";
        string yanasmalimani = "";
        string yanasmarihtimi = "";
        string demiryeri = "";
        string bayrak = "";
        string tip = "";
        string grt = "";
        string acente = "";
        string fatura = "";
        string bowt = "";
        string strnt = "";
        string loa = "";
        string tehlikeliyuk = "";
        string draft = "";
        string bilgi = "";
        string eta = "";
        string notlar = "";
        string talepno = "";

        Body = Body + "<table  style='width:1340px; border:1px solid black; background-color:lightyellow; font-family:Arial; font-size:10px;' ><tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> INCOAMING VESSELS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        //1.TABLO
        SqlCommand cmdgemibilgi1 = new SqlCommand("SP_Isliste_NSL", baglanti);
        cmdgemibilgi1.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr1 = cmdgemibilgi1.ExecuteReader();
        int no1 = 1;

        if (dr1.HasRows)
        {
            while (dr1.Read())
            {
                    gemiadi = dr1["gemiadi"].ToString();
                    kalkislimani = dr1["kalkislimani"].ToString();
                    kalkisrihtimi = dr1["kalkisrihtimi"].ToString();
                    yanasmalimani = dr1["yanasmalimani"].ToString();
                    yanasmarihtimi = dr1["yanasmarihtimi"].ToString();
                    demiryeri = dr1["demiryeri"].ToString();
                    bayrak = dr1["bayrak"].ToString();
                    tip = dr1["tip"].ToString();
                    grt = dr1["grt"].ToString();
                    acente = dr1["acente"].ToString();
                    fatura = dr1["fatura"].ToString();
                    bowt = dr1["bowt"].ToString();
                    strnt = dr1["strnt"].ToString();
                    loa = dr1["loa"].ToString();
                    tehlikeliyuk = dr1["tehlikeliyuk"].ToString();
                    draft = dr1["draft"].ToString();
                    bilgi = dr1["bilgi"].ToString();
                    eta = dr1["eta"].ToString();
                    notlar = dr1["notlar"].ToString();
                    talepno = dr1["talepno"].ToString();

                    Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no1 + "</td>";
                                    Body = Body + "<td style='width: 140px'>"+gemiadi+"</td>";
                                    Body = Body + "<td style='width: 160px'>"+kalkislimani+"/"+kalkisrihtimi+"</td>";
                                    Body = Body + "<td style='width: 160px'>"+yanasmalimani+"/"+yanasmarihtimi+"</td>";
                                    Body = Body + "<td style='width: 100px'>"+demiryeri+"</td>";
                                    Body = Body + "<td style='width: 80px'>"+bayrak+"</td>";
                                    Body = Body + "<td style='width: 30px'>"+tip+"</td>";
                                    Body = Body + "<td style='width: 50px'>"+grt+"</td>";
                                    Body = Body + "<td style='width: 95px'>"+acente+"/"+fatura+"</td>";
                                    Body = Body + "<td style='width: 65px'>"+bowt+"-"+strnt+"</td>";
                                    Body = Body + "<td style='width: 30px'>"+loa+"</td>";
                                    Body = Body + "<td style='width: 30px'>"+tehlikeliyuk+"</td>";
                                    Body = Body + "<td style='width: 40px'>"+draft+"</td>";
                                    Body = Body + "<td style='width: 40px'>"+bilgi+"</td>";
                                    Body = Body + "<td style='width: 100px'>"+eta+"</td>";
                                    Body = Body + "<td style='width: 140px'>"+notlar+"</td>";
                                    Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                                    no1 = no1 + 1;
            }
            
        }
        dr1.Close();

        //2.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> CONTACTED VESSELS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi2 = new SqlCommand("SP_Isliste_NSL_vipC", baglanti);
        cmdgemibilgi2.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr2 = cmdgemibilgi2.ExecuteReader();
        int no2 = 1;

        if (dr2.HasRows)
        {
            while (dr2.Read())
            {
                gemiadi = dr2["gemiadi"].ToString();
                kalkislimani = dr2["kalkislimani"].ToString();
                kalkisrihtimi = dr2["kalkisrihtimi"].ToString();
                yanasmalimani = dr2["yanasmalimani"].ToString();
                yanasmarihtimi = dr2["yanasmarihtimi"].ToString();
                demiryeri = dr2["demiryeri"].ToString();
                bayrak = dr2["bayrak"].ToString();
                tip = dr2["tip"].ToString();
                grt = dr2["grt"].ToString();
                acente = dr2["acente"].ToString();
                fatura = dr2["fatura"].ToString();
                bowt = dr2["bowt"].ToString();
                strnt = dr2["strnt"].ToString();
                loa = dr2["loa"].ToString();
                tehlikeliyuk = dr2["tehlikeliyuk"].ToString();
                draft = dr2["draft"].ToString();
                bilgi = dr2["bilgi"].ToString();
                eta = dr2["eta"].ToString();
                notlar = dr2["notlar"].ToString();
                talepno = dr2["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no2 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no2= no2 + 1;
            }
            
        }
        dr2.Close();



        //3.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> MANEUVERING VESSELS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi23 = new SqlCommand("SP_Isliste_NSL_Canli_Maneuvering", baglanti);
        cmdgemibilgi23.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr23 = cmdgemibilgi23.ExecuteReader();
        int no3 = 1;

        if (dr23.HasRows)
        {
            while (dr23.Read())
            {
                gemiadi = dr23["gemiadi"].ToString();
                kalkislimani = dr23["kalkislimani"].ToString();
                kalkisrihtimi = dr23["kalkisrihtimi"].ToString();
                yanasmalimani = dr23["yanasmalimani"].ToString();
                yanasmarihtimi = dr23["yanasmarihtimi"].ToString();
                demiryeri = dr23["demiryeri"].ToString();
                bayrak = dr23["bayrak"].ToString();
                tip = dr23["tip"].ToString();
                grt = dr23["grt"].ToString();
                acente = dr23["acente"].ToString();
                fatura = dr23["fatura"].ToString();
                bowt = dr23["bowt"].ToString();
                strnt = dr23["strnt"].ToString();
                loa = dr23["loa"].ToString();
                tehlikeliyuk = dr23["tehlikeliyuk"].ToString();
                draft = dr23["draft"].ToString();
                bilgi = dr23["bilgi"].ToString();
                eta = dr23["eta"].ToString();
                notlar = dr23["notlar"].ToString();
                talepno = dr23["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no3 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no3 = no3 + 1;
            }
            
        }
        dr23.Close();






        //4.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> ESKİHİSAR & YALOVA ANCHORAGE </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi3 = new SqlCommand("SP_Isliste_NSL_ASL", baglanti);
        cmdgemibilgi3.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr3 = cmdgemibilgi3.ExecuteReader();
        int no4 = 1;

        if (dr3.HasRows)
        {
            while (dr3.Read())
            {
                gemiadi = dr3["gemiadi"].ToString();
                kalkislimani = dr3["kalkislimani"].ToString();
                kalkisrihtimi = dr3["kalkisrihtimi"].ToString();
                yanasmalimani = dr3["yanasmalimani"].ToString();
                yanasmarihtimi = dr3["yanasmarihtimi"].ToString();
                demiryeri = dr3["demiryeri"].ToString();
                bayrak = dr3["bayrak"].ToString();
                tip = dr3["tip"].ToString();
                grt = dr3["grt"].ToString();
                acente = dr3["acente"].ToString();
                fatura = dr3["fatura"].ToString();
                bowt = dr3["bowt"].ToString();
                strnt = dr3["strnt"].ToString();
                loa = dr3["loa"].ToString();
                tehlikeliyuk = dr3["tehlikeliyuk"].ToString();
                draft = dr3["draft"].ToString();
                bilgi = dr3["bilgi"].ToString();
                eta = dr3["eta"].ToString();
                notlar = dr3["notlar"].ToString();
                talepno = dr3["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no4 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no4 = no4 + 1;
            }
            
        }
        dr3.Close();


        //5.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> DILOVASI PORTS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi4 = new SqlCommand("SP_Isliste_NSL_VIP", baglanti);
        cmdgemibilgi4.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr4 = cmdgemibilgi4.ExecuteReader();
        int no5 = 1;

        if (dr4.HasRows)
        {
            while (dr4.Read())
            {
                gemiadi = dr4["gemiadi"].ToString();
                kalkislimani = dr4["kalkislimani"].ToString();
                kalkisrihtimi = dr4["kalkisrihtimi"].ToString();
                yanasmalimani = dr4["yanasmalimani"].ToString();
                yanasmarihtimi = dr4["yanasmarihtimi"].ToString();
                demiryeri = dr4["demiryeri"].ToString();
                bayrak = dr4["bayrak"].ToString();
                tip = dr4["tip"].ToString();
                grt = dr4["grt"].ToString();
                acente = dr4["acente"].ToString();
                fatura = dr4["fatura"].ToString();
                bowt = dr4["bowt"].ToString();
                strnt = dr4["strnt"].ToString();
                loa = dr4["loa"].ToString();
                tehlikeliyuk = dr4["tehlikeliyuk"].ToString();
                draft = dr4["draft"].ToString();
                bilgi = dr4["bilgi"].ToString();
                eta = dr4["eta"].ToString();
                notlar = dr4["notlar"].ToString();
                talepno = dr4["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no5 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no5 = no5 + 1;
            }
            
        }
        dr4.Close();



        //6.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> YALOVA SHIPYARD </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi5 = new SqlCommand("SP_Isliste_NSL_vipT", baglanti);
        cmdgemibilgi5.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr5 = cmdgemibilgi5.ExecuteReader();
        int no6 = 1;

        if (dr5.HasRows)
        {
            while (dr5.Read())
            {
                gemiadi = dr5["gemiadi"].ToString();
                kalkislimani = dr5["kalkislimani"].ToString();
                kalkisrihtimi = dr5["kalkisrihtimi"].ToString();
                yanasmalimani = dr5["yanasmalimani"].ToString();
                yanasmarihtimi = dr5["yanasmarihtimi"].ToString();
                demiryeri = dr5["demiryeri"].ToString();
                bayrak = dr5["bayrak"].ToString();
                tip = dr5["tip"].ToString();
                grt = dr5["grt"].ToString();
                acente = dr5["acente"].ToString();
                fatura = dr5["fatura"].ToString();
                bowt = dr5["bowt"].ToString();
                strnt = dr5["strnt"].ToString();
                loa = dr5["loa"].ToString();
                tehlikeliyuk = dr5["tehlikeliyuk"].ToString();
                draft = dr5["draft"].ToString();
                bilgi = dr5["bilgi"].ToString();
                eta = dr5["eta"].ToString();
                notlar = dr5["notlar"].ToString();
                talepno = dr5["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no6 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no6 = no6 + 1;
            }
            
        }
        dr5.Close();



        //6a.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> YALOVA PORTS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi5a = new SqlCommand("SP_Isliste_NSL_YalPort", baglanti);
        cmdgemibilgi5a.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr5a = cmdgemibilgi5a.ExecuteReader();
        int no6a = 1;

        if (dr5a.HasRows)
        {
            while (dr5a.Read())
            {
                gemiadi = dr5a["gemiadi"].ToString();
                kalkislimani = dr5a["kalkislimani"].ToString();
                kalkisrihtimi = dr5a["kalkisrihtimi"].ToString();
                yanasmalimani = dr5a["yanasmalimani"].ToString();
                yanasmarihtimi = dr5a["yanasmarihtimi"].ToString();
                demiryeri = dr5a["demiryeri"].ToString();
                bayrak = dr5a["bayrak"].ToString();
                tip = dr5a["tip"].ToString();
                grt = dr5a["grt"].ToString();
                acente = dr5a["acente"].ToString();
                fatura = dr5a["fatura"].ToString();
                bowt = dr5a["bowt"].ToString();
                strnt = dr5a["strnt"].ToString();
                loa = dr5a["loa"].ToString();
                tehlikeliyuk = dr5a["tehlikeliyuk"].ToString();
                draft = dr5a["draft"].ToString();
                bilgi = dr5a["bilgi"].ToString();
                eta = dr5a["eta"].ToString();
                notlar = dr5a["notlar"].ToString();
                talepno = dr5a["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no6a + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no6a = no6a + 1;
            }

        }
        dr5a.Close();


        //7.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> HEREKE PORTS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi6 = new SqlCommand("SP_Isliste_NSL_her", baglanti);
        cmdgemibilgi6.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr6 = cmdgemibilgi6.ExecuteReader();
        int no7 = 1;

        if (dr6.HasRows)
        {
            while (dr6.Read())
            {
                gemiadi = dr6["gemiadi"].ToString();
                kalkislimani = dr6["kalkislimani"].ToString();
                kalkisrihtimi = dr6["kalkisrihtimi"].ToString();
                yanasmalimani = dr6["yanasmalimani"].ToString();
                yanasmarihtimi = dr6["yanasmarihtimi"].ToString();
                demiryeri = dr6["demiryeri"].ToString();
                bayrak = dr6["bayrak"].ToString();
                tip = dr6["tip"].ToString();
                grt = dr6["grt"].ToString();
                acente = dr6["acente"].ToString();
                fatura = dr6["fatura"].ToString();
                bowt = dr6["bowt"].ToString();
                strnt = dr6["strnt"].ToString();
                loa = dr6["loa"].ToString();
                tehlikeliyuk = dr6["tehlikeliyuk"].ToString();
                draft = dr6["draft"].ToString();
                bilgi = dr6["bilgi"].ToString();
                eta = dr6["eta"].ToString();
                notlar = dr6["notlar"].ToString();
                talepno = dr6["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no7 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no7 = no7 + 1;
            }
            
        }
        dr6.Close();



        //8.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> YARIMCA & HEREKE & İZMİT ANCHORAGE </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi8 = new SqlCommand("SP_Isliste_NSL_YA", baglanti);
        cmdgemibilgi8.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr8 = cmdgemibilgi8.ExecuteReader();
        int no8 = 1;

        if (dr8.HasRows)
        {
            while (dr8.Read())
            {
                gemiadi = dr8["gemiadi"].ToString();
                kalkislimani = dr8["kalkislimani"].ToString();
                kalkisrihtimi = dr8["kalkisrihtimi"].ToString();
                yanasmalimani = dr8["yanasmalimani"].ToString();
                yanasmarihtimi = dr8["yanasmarihtimi"].ToString();
                demiryeri = dr8["demiryeri"].ToString();
                bayrak = dr8["bayrak"].ToString();
                tip = dr8["tip"].ToString();
                grt = dr8["grt"].ToString();
                acente = dr8["acente"].ToString();
                fatura = dr8["fatura"].ToString();
                bowt = dr8["bowt"].ToString();
                strnt = dr8["strnt"].ToString();
                loa = dr8["loa"].ToString();
                tehlikeliyuk = dr8["tehlikeliyuk"].ToString();
                draft = dr8["draft"].ToString();
                bilgi = dr8["bilgi"].ToString();
                eta = dr8["eta"].ToString();
                notlar = dr8["notlar"].ToString();
                talepno = dr8["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no8 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no8 = no8 + 1;
            }
            
        }
        dr8.Close();


        //9.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> YARIMCA PORTS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi9 = new SqlCommand("SP_Isliste_NSL_YP", baglanti);
        cmdgemibilgi9.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr9 = cmdgemibilgi9.ExecuteReader();
        int no9 = 1;

        if (dr9.HasRows)
        {
            while (dr9.Read())
            {
                gemiadi = dr9["gemiadi"].ToString();
                kalkislimani = dr9["kalkislimani"].ToString();
                kalkisrihtimi = dr9["kalkisrihtimi"].ToString();
                yanasmalimani = dr9["yanasmalimani"].ToString();
                yanasmarihtimi = dr9["yanasmarihtimi"].ToString();
                demiryeri = dr9["demiryeri"].ToString();
                bayrak = dr9["bayrak"].ToString();
                tip = dr9["tip"].ToString();
                grt = dr9["grt"].ToString();
                acente = dr9["acente"].ToString();
                fatura = dr9["fatura"].ToString();
                bowt = dr9["bowt"].ToString();
                strnt = dr9["strnt"].ToString();
                loa = dr9["loa"].ToString();
                tehlikeliyuk = dr9["tehlikeliyuk"].ToString();
                draft = dr9["draft"].ToString();
                bilgi = dr9["bilgi"].ToString();
                eta = dr9["eta"].ToString();
                notlar = dr9["notlar"].ToString();
                talepno = dr9["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no9 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no9 = no9 + 1;
            }
            
        }
        dr9.Close();


        //10.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> KOSBAŞ SHIPYARD </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Departure Place </td>";
        Body = Body + "<td> Destination Place </td>";
        Body = Body + "<td> Anch.Place </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Typ </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Agent/Bill </td>";
        Body = Body + "<td> Bow-St </td>";
        Body = Body + "<td> Loa </td>";
        Body = Body + "<td> DC </td>";
        Body = Body + "<td> D(m) </td>";
        Body = Body + "<td> TPP </td>";
        Body = Body + "<td> POB Time</td>";
        Body = Body + "<td> Note </td>";
        Body = Body + "<td> Req.No </td></tr>";

        SqlCommand cmdgemibilgi10 = new SqlCommand("SP_Isliste_NSL_Yk", baglanti);
        cmdgemibilgi10.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr10 = cmdgemibilgi10.ExecuteReader();
        int no10 = 1;

        if (dr10.HasRows)
        {
            while (dr10.Read())
            {
                gemiadi = dr10["gemiadi"].ToString();
                kalkislimani = dr10["kalkislimani"].ToString();
                kalkisrihtimi = dr10["kalkisrihtimi"].ToString();
                yanasmalimani = dr10["yanasmalimani"].ToString();
                yanasmarihtimi = dr10["yanasmarihtimi"].ToString();
                demiryeri = dr10["demiryeri"].ToString();
                bayrak = dr10["bayrak"].ToString();
                tip = dr10["tip"].ToString();
                grt = dr10["grt"].ToString();
                acente = dr10["acente"].ToString();
                fatura = dr10["fatura"].ToString();
                bowt = dr10["bowt"].ToString();
                strnt = dr10["strnt"].ToString();
                loa = dr10["loa"].ToString();
                tehlikeliyuk = dr10["tehlikeliyuk"].ToString();
                draft = dr10["draft"].ToString();
                bilgi = dr10["bilgi"].ToString();
                eta = dr10["eta"].ToString();
                notlar = dr10["notlar"].ToString();
                talepno = dr10["talepno"].ToString();

                Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no10 + "</td>";
                Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
                Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
                Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
                Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 30px'>" + tip + "</td>";
                Body = Body + "<td style='width: 50px'>" + grt + "</td>";
                Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
                Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
                Body = Body + "<td style='width: 30px'>" + loa + "</td>";
                Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
                Body = Body + "<td style='width: 40px'>" + draft + "</td>";
                Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
                Body = Body + "<td style='width: 100px'>" + eta + "</td>";
                Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
                Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

                no10 = no10 + 1;
            }
            
        }
        dr10.Close();










        Body = Body + "</table></br></br>DENİZ KILAVUZLUK A.Ş.</br>Send by Pilot Monitoring System.</br></br>";


        AnaKlas.MailSending(Subject, Body);

    }

    protected void sendmaillive()

         {
        SqlConnection baglanti = AnaKlas.baglan();

        string Subject = "Live Screen / " + DateTime.Now.ToString();
        string Body = "";

        string degismeciadisoyadi = "";
        string toplamissuresi = "";
        string toplamissayisi = "";
        string gemiadi = "";
        string bayrak = "";
        string grt = "";
        string tip = "";
        string binisyeri = "";
        string inisyeri = "";
        string istasyoncikis = "";
        string pob = "";
        string Poff = "";
        string istasyongelis = "";
        string yorulma = "";
        
        Body = Body + "<table  style='width:1340px; border:1px solid black; background-color:lightyellow; font-family:Arial; font-size:10px;' ><tr><td style='width:1340px; border:1px;' colspan=15> <font color='red'> DARICA PILOTS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightblue; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Pilot Name </td>";
        Body = Body + "<td> W.Hrs </td>";
        Body = Body + "<td> ToW </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Tp </td>";
        Body = Body + "<td> Departure </td>";
        Body = Body + "<td> Arrival </td>";
        Body = Body + "<td> OffStation </td>";
        Body = Body + "<td> POB </td>";
        Body = Body + "<td> POff </td>";
        Body = Body + "<td> OnStation </td>";
        Body = Body + "<td> Fatigue</td></tr>";

        //DTDarica Canlı Ekran 
        SqlCommand cmdgemibilgi1 = new SqlCommand("SP_DTDaricaCanliEkran", baglanti);
        cmdgemibilgi1.CommandType = CommandType.StoredProcedure;
        cmdgemibilgi1.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
        cmdgemibilgi1.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
        SqlDataReader dr1 = cmdgemibilgi1.ExecuteReader();
        int no1 = 1;

        if (dr1.HasRows)
        {
            while (dr1.Read())
            {
                degismeciadisoyadi = dr1["degismeciadisoyadi"].ToString();
                toplamissuresi = dr1["toplamissuresi"].ToString();
                toplamissayisi = dr1["toplamissayisi"].ToString();
                gemiadi = dr1["gemiadi"].ToString();
                bayrak = dr1["bayrak"].ToString();
                grt = dr1["grt"].ToString();
                tip = dr1["tip"].ToString();
                binisyeri = dr1["binisyeri"].ToString();
                inisyeri = dr1["inisyeri"].ToString();
                istasyoncikis = dr1["istasyoncikis"].ToString();
                pob = dr1["pob"].ToString();
                Poff = dr1["Poff"].ToString();
                istasyongelis = dr1["istasyongelis"].ToString();
                yorulma = dr1["yorulma"].ToString();


                Body = Body + "<tr  style='border:1px solid gray;'><td style='width:2%'>" + no1 + "</td>";
                Body = Body + "<td style='width: 12%'>" + degismeciadisoyadi + "</td>";
                Body = Body + "<td style='width: 4%'>" + toplamissuresi + "</td>";
                Body = Body + "<td style='width: 3%'>" + toplamissayisi + "</td>";
                Body = Body + "<td style='width: 12%'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 6%'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 4%'>" + grt + "</td>";
                Body = Body + "<td style='width: 3%'>" + tip + "</td>";
                Body = Body + "<td style='width: 12%'>" + binisyeri + "</td>";
                Body = Body + "<td style='width: 12%'>" + inisyeri + "</td>";
                Body = Body + "<td style='width: 6%'>" + istasyoncikis + "</td>";
                Body = Body + "<td style='width: 6%'>" + pob + "</td>";
                Body = Body + "<td style='width: 6%'>" + Poff + "</td>";
                Body = Body + "<td style='width: 6%'>" + istasyongelis + "</td>";
                Body = Body + "<td style='width: 6%'>" + yorulma + "</td>";

                   no1 = no1 + 1;
            }
        }

        dr1.Close();

        //yarımca
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> YARIMCA PILOTS </font ></td></tr>";
        Body = Body + "<tr style='background-color:lightblue; font-weight:bold;'><td> No </td>";
        Body = Body + "<td> Pilot Name </td>";
        Body = Body + "<td> W.Hrs </td>";
        Body = Body + "<td> ToW </td>";
        Body = Body + "<td> Ship Name </td>";
        Body = Body + "<td> Flag </td>";
        Body = Body + "<td> Grt </td>";
        Body = Body + "<td> Tp </td>";
        Body = Body + "<td> Departure </td>";
        Body = Body + "<td> Arrival </td>";
        Body = Body + "<td> OffStation </td>";
        Body = Body + "<td> POB </td>";
        Body = Body + "<td> POff </td>";
        Body = Body + "<td> OnStation </td>";
        Body = Body + "<td> Fatigue</td></tr>";

        //DTDarica Canlı Ekran 
        SqlCommand cmdgemibilgi2 = new SqlCommand("SP_DTYarimcaCanliEkran", baglanti);
        cmdgemibilgi2.CommandType = CommandType.StoredProcedure;
        cmdgemibilgi2.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
        cmdgemibilgi2.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
        SqlDataReader dr2 = cmdgemibilgi2.ExecuteReader();
        int no2 = 1;

        if (dr2.HasRows)
        {
            while (dr2.Read())
            {
                degismeciadisoyadi = dr2["degismeciadisoyadi"].ToString();
                toplamissuresi = dr2["toplamissuresi"].ToString();
                toplamissayisi = dr2["toplamissayisi"].ToString();
                gemiadi = dr2["gemiadi"].ToString();
                bayrak = dr2["bayrak"].ToString();
                grt = dr2["grt"].ToString();
                tip = dr2["tip"].ToString();
                binisyeri = dr2["binisyeri"].ToString();
                inisyeri = dr2["inisyeri"].ToString();
                istasyoncikis = dr2["istasyoncikis"].ToString();
                pob = dr2["pob"].ToString();
                Poff = dr2["Poff"].ToString();
                istasyongelis = dr2["istasyongelis"].ToString();
                yorulma = dr2["yorulma"].ToString();


                Body = Body + "<tr  style='border:1px solid gray;'><td style='width:2%'>" + no2+ "</td>";
                Body = Body + "<td style='width: 12%'>" + degismeciadisoyadi + "</td>";
                Body = Body + "<td style='width: 4%'>" + toplamissuresi + "</td>";
                Body = Body + "<td style='width: 3%'>" + toplamissayisi + "</td>";
                Body = Body + "<td style='width: 12%'>" + gemiadi + "</td>";
                Body = Body + "<td style='width: 6%'>" + bayrak + "</td>";
                Body = Body + "<td style='width: 4%'>" + grt + "</td>";
                Body = Body + "<td style='width: 3%'>" + tip + "</td>";
                Body = Body + "<td style='width: 12%'>" + binisyeri + "</td>";
                Body = Body + "<td style='width: 12%'>" + inisyeri + "</td>";
                Body = Body + "<td style='width: 6%'>" + istasyoncikis + "</td>";
                Body = Body + "<td style='width: 6%'>" + pob + "</td>";
                Body = Body + "<td style='width: 6%'>" + Poff + "</td>";
                Body = Body + "<td style='width: 6%'>" + istasyongelis + "</td>";
                Body = Body + "<td style='width: 6%'>" + yorulma + "</td>";

                no2 = no2 + 1;
            }
        }
        dr2.Close();
        

        Body = Body + "</table></br>DENİZ KILAVUZLUK A.Ş.</br>Send by Pilot Monitoring System.</br></br>";

       AnaKlas.MailSendinglive(Subject, Body);

    }


    protected void ButtonJobList_Click(object sender, EventArgs e)
    {
        Response.Redirect("joblist.aspx");
    }
    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"].ToString() == "" || (Session["kapno"] == null))
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else
        {

            AnaKlas.logrecup(Session["kapno"].ToString());
            Session.Abandon();
            Response.Redirect("https://www.monitoringpilot.com");
        }
    }

    protected void LBonline_Click(object sender, EventArgs e)
    {
     if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }


    private string TarihSaatYaziYapDMYhm(DateTime TarihsaatDMYhms)
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

    private string TarihYaziYapDMY(DateTime TarihsaatDMYhms)
    {
        string TarihYaziok = TarihSaatYaziYapDMYhm(TarihsaatDMYhms).Substring(0, 10);
        return TarihYaziok;
    }




    protected void LBmyjobs_Click(object sender, EventArgs e)
    {
        Response.Redirect("myjobs.aspx");
    }



    protected void LBSurvey_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdkapsiranoal = new SqlCommand("Select aciklama from argem where id=(select max(id) from argem) order by id desc", baglanti);
        string linkle = cmdkapsiranoal.ExecuteScalar().ToString();
        cmdkapsiranoal.Dispose();
        baglanti.Close();

        Response.Redirect(linkle);


    }


    protected void siptus_Click(object sender, EventArgs e)
    {
Response.Redirect("sipd.aspx");
    }

    protected void sipyar_Click(object sender, EventArgs e)
    {
Response.Redirect("sipy.aspx");
    }
}


