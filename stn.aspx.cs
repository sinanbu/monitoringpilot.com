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
using System.Media;


public partial class stn : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();


        if (AnaKlas.Pcipal() == "78.189.22.74" || AnaKlas.Pcipal() == "195.175.33.210" || AnaKlas.Pcipal() == "78.186.197.207" || AnaKlas.Pcipal() == "212.156.49.226" || Session["kapno"] != null)
        {

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
				
                    SqlCommand mailsaatoku = new SqlCommand("SP_Up_dlsayokuhour", baglanti);
                mailsaatoku.CommandType = CommandType.StoredProcedure;
                mailsaatoku.Parameters.Add("@saydarica", SqlDbType.VarChar, 2);
                mailsaatoku.Parameters["@saydarica"].Direction = ParameterDirection.Output;
                mailsaatoku.ExecuteNonQuery();
                    string saydarica = mailsaatoku.Parameters["@saydarica"].Value.ToString().Trim();
                mailsaatoku.Dispose();

                    if (Convert.ToInt32(saydarica) == DateTime.Now.Hour)
                    {
                        saydarica = DateTime.Now.AddHours(1).Hour.ToString();

                        SqlCommand mailsaatup = new SqlCommand("SP_Up_dlsayhour", baglanti);
                    mailsaatup.CommandType = CommandType.StoredProcedure;
                    mailsaatup.Parameters.AddWithValue("@saydarica", saydarica);
                    mailsaatup.ExecuteNonQuery();
                    mailsaatup.Dispose();

                        sendmaillive();

                    }


                LiteralYaz();

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

                    LblVarid.Text = "Watch:" + varbilvarid.Text;
                    LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                    LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                    LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                    LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();

                    DTloading();

                //java
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append(@" <script type='text/javascript'>");
                //sb.Append(" $('document').ready(function () {");
                //sb.Append(" $('#gizle').fadeOut('slow');");
                //sb.Append("   });   ");
                //sb.Append(@"</script>");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kaybol", sb.ToString(), false);

               // string alert = "<script language=JavaScript>alert(`ASP.NET Messagebox`)</script>";
                //                Page.RegisterStartupScript("x", alert);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SomestartupScript", alert, True);
                //ClientScript.RegisterStartupScript(Type.GetType("System.String"), "messagebox", "<script type=\"text/javascript\">alert('File not Found');</script>");

                //ScriptManager.RegisterStartupScript(this, GetType(), "select", alert, true);

                //System.Media.SoundPlayer zil = new System.Media.SoundPlayer();  //SoundPlayer zil = new SoundPlayer();
                //zil.SoundLocation = @"C:\Users\Sinan Buğdaycı\Desktop\mptrv2\alert9.wav";
                ////zil.SoundLocation = @"C:\inetpub\wwwroot\www.monitoringpilot.com\alert9.wav";
                //zil.Load();
                //zil.PlaySync();  



            }

        }
        else  //if (cmdlogofbak.ExecuteScalar() != null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }

        baglanti.Close();

    }  

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        //string pcip = AnaKlas.Pcipal();
        //if (pcip == "212.156.49.226")
        //{ ScriptManager.RegisterStartupScript(this, GetType(), "zilcal", "zilcal();", true); }

            Lblsaat.Text = DateTime.Now.ToShortTimeString();
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

		
     //   decimal toptopzihinfat = 0;
     //   decimal toptopissure = 0;
     //   double toptopissay = 0;

     //   //_______________yaşanan vardiya toplam bilgisi okuma 

     //   if (LblCounter.Text != "0")
     //   {
     //       //yaşanan vardiya toplam bilgisi okuma 
     //       SqlCommand cmdvardegeroku = new SqlCommand("SP_Pilotvardiya_TopTop_is_sur_pilot", baglanti);
     //       cmdvardegeroku.CommandType = CommandType.StoredProcedure;
     //       cmdvardegeroku.Parameters.AddWithValue("@varno", Convert.ToInt32(varbilvarno.Text));
     //       SqlDataReader vardr = cmdvardegeroku.ExecuteReader();

     //       if (vardr.HasRows)
     //       {
     //           while (vardr.Read())
     //           {
     //               toptopissure = Convert.ToDecimal(vardr["toptopissure"].ToString());// vardiyada yapılan işlerin toplam kaç saat sürdüğü
     //               toptopzihinfat = Convert.ToDecimal(vardr["toptopzihinfat"].ToString()); // vardiyadaki toplam zihinsel fat
     //               toptopissay = Convert.ToDouble(vardr["toptopissay"].ToString()); // vardiyadaki toplam is sayisi

     //           }
     //       }

     //       vardr.Close();
     //       cmdvardegeroku.Dispose();
     //   }

     //   //çalışan pilot sayısını ortalama ile bulma

     //   SqlCommand cmdcalpilotavg = new SqlCommand("SP_Pilotsaycalisan_4luortaissayisi", baglanti);
     //   cmdcalpilotavg.CommandType = CommandType.StoredProcedure;
     //   //cmdcalpilotavg.Parameters.AddWithValue("@varno", Convert.ToInt32(varbilvarno.Text));
     //   SqlDataReader vardra = cmdcalpilotavg.ExecuteReader();

     //   double topissayavg = 0;

     //   if (vardra.HasRows)
     //   {
     //       while (vardra.Read())
     //       {
     //           topissayavg = topissayavg + Convert.ToDouble(vardra["toplamissayisi"].ToString());
     //       }
     //   }
        
     //   topissayavg = topissayavg/4;
       

     //   vardra.Close();
     //   cmdcalpilotavg.Dispose();



     //   string pilotsay = "0";
     //   double reelgecen = (DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours;

     //   if (toptopissay != 0)
     //   {
     //       pilotsay = (Math.Floor((toptopissay / topissayavg))+1).ToString();
     //   }
     //   else
     //   { 
     //   SqlCommand cmdPilotsaycalisan = new SqlCommand("SP_Pilotsaycalisan", baglanti);
     //   cmdPilotsaycalisan.CommandType = CommandType.StoredProcedure;
     //   cmdPilotsaycalisan.Parameters.Add("@pilotsay", SqlDbType.Int);
     //   cmdPilotsaycalisan.Parameters["@pilotsay"].Direction = ParameterDirection.Output;
     //   cmdPilotsaycalisan.ExecuteNonQuery();
     //   pilotsay = cmdPilotsaycalisan.Parameters["@pilotsay"].Value.ToString();
     //   cmdPilotsaycalisan.Dispose();
     //   }


     //if (pilotsay.ToString() == "" || pilotsay.ToString() == "0") { pilotsay = "1"; }
     //   if (reelgecen.ToString() == "" || reelgecen.ToString() == "0") { reelgecen = 1; }
     //   decimal vartotfatik = (toptopissure + toptopzihinfat);
     //   vartotfatik = vartotfatik / Convert.ToDecimal(reelgecen);
     //   vartotfatik = vartotfatik / Convert.ToDecimal(pilotsay);
     //   vartotfatik = vartotfatik * 3 / 5;

        //00000000000000000000000000
        // decimal vartotfatik = 0;
        // SqlCommand cmdPilotavgF = new SqlCommand("SP_Pilotvardiya_calpilot_avgFatik", baglanti);
        // cmdPilotavgF.CommandType = CommandType.StoredProcedure;
        // cmdPilotavgF.Parameters.AddWithValue("@varno", varbilvarno.Text);
        // cmdPilotavgF.Parameters.Add("@varavgF", SqlDbType.Float);
        // cmdPilotavgF.Parameters["@varavgF"].Direction = ParameterDirection.Output;
        // cmdPilotavgF.ExecuteNonQuery();
        // string sonuc = cmdPilotavgF.Parameters["@varavgF"].Value.ToString();



        // if (sonuc == null || sonuc == "")
        // { vartotfatik = 0; }

        // else { vartotfatik = Convert.ToDecimal(sonuc); }
        // cmdPilotavgF.Dispose();

        //00000000000000000000000000

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
    { SqlConnection baglanti = AnaKlas.baglan();
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
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


            //yarımcaya ait turuncu mavi
            Label Lblgemiadicolor = (Label)e.Item.FindControl("Lblgemiadi");
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

            Label LblKapnocolor = (Label)e.Item.FindControl("LblKapno");
            //terste kalan pillot renkli
            if (Convert.ToInt16(DateTime.Now.Day.ToString()) == Convert.ToInt16(varbiter.Text.Substring(0, 2))) 
            { 
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

            //gemi atama zili kayıt
            { 
            Label imonosonkop = (Label)e.Item.FindControl("Lblimono");

            SqlCommand cmdziloku = new SqlCommand("SP_zilokuilk", baglanti);
            cmdziloku.CommandType = CommandType.StoredProcedure;
            cmdziloku.Parameters.AddWithValue("@kapno", LblKapnocolor.Text.Trim());
            cmdziloku.Parameters.Add("@imonoilk", SqlDbType.Int);
            cmdziloku.Parameters["@imonoilk"].Direction = ParameterDirection.Output;
            cmdziloku.ExecuteNonQuery();
            string imonoilk = cmdziloku.Parameters["@imonoilk"].Value.ToString().Trim();
            cmdziloku.Dispose();
            //eşit değilse zil çal
            if (imonoilk=="0" && imonosonkop.Text!= "0")
            {
                    //string pcip = AnaKlas.Pcipal();
                    //if (pcip == "78.189.22.74")
                    { 
                        ScriptManager.RegisterStartupScript(this, GetType(), "zilcal", "zilcal();", true);
                //System.Media.SoundPlayer zil = new System.Media.SoundPlayer();  //SoundPlayer zil = new SoundPlayer();
                //zil.SoundLocation = @"C:\Users\Sinan Buğdaycı\Desktop\mptrv2\alert9.wav";
                //zil.Load();
                //zil.PlaySync();  // @"C:\inetpub\wwwroot\www.monitoringpilot.com\alert9.wav";   // 
            }
                }
                //çıkışta eşitle
                SqlCommand cmdzilupilk = new SqlCommand("SP_zilupilk", baglanti);
            cmdzilupilk.CommandType = CommandType.StoredProcedure;
            cmdzilupilk.Parameters.AddWithValue("@imonoilk", Convert.ToInt32(imonosonkop.Text.Trim()));
            cmdzilupilk.Parameters.AddWithValue("@kapno", LblKapnocolor.Text.Trim());
            cmdzilupilk.ExecuteNonQuery();
            cmdzilupilk.Dispose();
            }

            // tuş göster gizle ayarlanıyor
            Label mylabel = (Label)e.Item.FindControl("lblDurum");
            String puan = mylabel.Text;

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
            LinkButton BtnProcessiptalkopya = (LinkButton)e.Item.FindControl("BtnProcessiptal");
            String secilikapno = (BtnProcessiptalkopya.CommandArgument).ToString();

            SqlCommand cmdPilotDegisismial = new SqlCommand("SP_PilotDegismeciismial", baglanti);
            cmdPilotDegisismial.CommandType = CommandType.StoredProcedure;
            cmdPilotDegisismial.Parameters.AddWithValue("@secilikapno", secilikapno);
            string degismeciadisoyadi = "";
            SqlDataReader dr = cmdPilotDegisismial.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    degismeciadisoyadi = dr["degismeciadisoyadi"].ToString();
                }
            }
            dr.Close();
            cmdPilotDegisismial.Dispose();

            baglanti.Close();
        
    }

    protected void DLDaricay_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
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
                    Lbltbpykop.Text = gemireader["bilgi"].ToString()+"-"+ gemireader["draft"].ToString();
                }
            }
            gemireader.Close();

            //darıcaya ait gemi mavi
            Label Lblgemiadicolor = (Label)e.Item.FindControl("Lblgemiadiy");
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

            Label LblKapnocolory = (Label)e.Item.FindControl("LblKapnoy");

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

            //gemi atama zili kayıt
            
            {
                Label imonosonkopy = (Label)e.Item.FindControl("Lblimonoy");

                SqlCommand cmdziloku = new SqlCommand("SP_zilokuilk", baglanti);
                cmdziloku.CommandType = CommandType.StoredProcedure;
                cmdziloku.Parameters.AddWithValue("@kapno", LblKapnocolory.Text.Trim());
                cmdziloku.Parameters.Add("@imonoilk", SqlDbType.Int);
                cmdziloku.Parameters["@imonoilk"].Direction = ParameterDirection.Output;
                cmdziloku.ExecuteNonQuery();
                string imonoilk = cmdziloku.Parameters["@imonoilk"].Value.ToString().Trim();
                cmdziloku.Dispose();
                //eşit değilse zil çal
                if (imonoilk == "0" && imonosonkopy.Text != "0")
                {
                    //if (AnaKlas.Pcipal() == "195.175.33.210")
                    { ScriptManager.RegisterStartupScript(this, GetType(), "zilcal", "zilcal();", true); }
                }
                //çıkışta eşitle
                SqlCommand cmdzilupilk = new SqlCommand("SP_zilupilk", baglanti);
                cmdzilupilk.CommandType = CommandType.StoredProcedure;
                cmdzilupilk.Parameters.AddWithValue("@imonoilk", Convert.ToInt32(imonosonkopy.Text.Trim()));
                cmdzilupilk.Parameters.AddWithValue("@kapno", LblKapnocolory.Text.Trim());
                cmdzilupilk.ExecuteNonQuery();
                cmdzilupilk.Dispose();

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
            LinkButton BtnProcessiptalkopya = (LinkButton)e.Item.FindControl("BtnProcessiptaly");
            String secilikapno = (BtnProcessiptalkopya.CommandArgument).ToString();

            SqlCommand cmdPilotDegisismial = new SqlCommand("SP_PilotDegismeciismial", baglanti);
            cmdPilotDegisismial.CommandType = CommandType.StoredProcedure;
            cmdPilotDegisismial.Parameters.AddWithValue("@secilikapno", secilikapno);
            string degismeciadisoyadi = "";
            SqlDataReader dr = cmdPilotDegisismial.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    degismeciadisoyadi = dr["degismeciadisoyadi"].ToString();
                }
            }
            dr.Close();
            cmdPilotDegisismial.Dispose();

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
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> DARICA ANCHORAGE </font ></td></tr>";
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
        int no3 = 1;

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
        dr3.Close();


        //4.TABLO
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
        int no4 = 1;

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
        dr4.Close();



        ////5.TABLO
        //Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> YALOVA SHIPYARD </font ></td></tr>";
        //Body = Body + "<tr style='background-color:lightgray; font-weight:bold;'><td> No </td>";
        //Body = Body + "<td> Ship Name </td>";
        //Body = Body + "<td> Departure Place </td>";
        //Body = Body + "<td> Destination Place </td>";
        //Body = Body + "<td> Anch.Place </td>";
        //Body = Body + "<td> Flag </td>";
        //Body = Body + "<td> Typ </td>";
        //Body = Body + "<td> Grt </td>";
        //Body = Body + "<td> Agent/Bill </td>";
        //Body = Body + "<td> Bow-St </td>";
        //Body = Body + "<td> Loa </td>";
        //Body = Body + "<td> DC </td>";
        //Body = Body + "<td> D(m) </td>";
        //Body = Body + "<td> TPP </td>";
        //Body = Body + "<td> POB Time</td>";
        //Body = Body + "<td> Note </td>";
        //Body = Body + "<td> Req.No </td></tr>";

        //SqlCommand cmdgemibilgi5 = new SqlCommand("SP_Isliste_NSL_vipT", baglanti);
        //cmdgemibilgi5.CommandType = CommandType.StoredProcedure;
        //SqlDataReader dr5 = cmdgemibilgi5.ExecuteReader();
        //int no5 = 1;

        //if (dr5.HasRows)
        //{
        //    while (dr5.Read())
        //    {
        //        gemiadi = dr5["gemiadi"].ToString();
        //        kalkislimani = dr5["kalkislimani"].ToString();
        //        kalkisrihtimi = dr5["kalkisrihtimi"].ToString();
        //        yanasmalimani = dr5["yanasmalimani"].ToString();
        //        yanasmarihtimi = dr5["yanasmarihtimi"].ToString();
        //        demiryeri = dr5["demiryeri"].ToString();
        //        bayrak = dr5["bayrak"].ToString();
        //        tip = dr5["tip"].ToString();
        //        grt = dr5["grt"].ToString();
        //        acente = dr5["acente"].ToString();
        //        fatura = dr5["fatura"].ToString();
        //        bowt = dr5["bowt"].ToString();
        //        strnt = dr5["strnt"].ToString();
        //        loa = dr5["loa"].ToString();
        //        tehlikeliyuk = dr5["tehlikeliyuk"].ToString();
        //        draft = dr5["draft"].ToString();
        //        bilgi = dr5["bilgi"].ToString();
        //        eta = dr5["eta"].ToString();
        //        notlar = dr5["notlar"].ToString();
        //        talepno = dr5["talepno"].ToString();

        //        Body = Body + "<tr  style='border:1px solid gray;'><td style='width: 25px'>" + no5 + "</td>";
        //        Body = Body + "<td style='width: 140px'>" + gemiadi + "</td>";
        //        Body = Body + "<td style='width: 160px'>" + kalkislimani + "/" + kalkisrihtimi + "</td>";
        //        Body = Body + "<td style='width: 160px'>" + yanasmalimani + "/" + yanasmarihtimi + "</td>";
        //        Body = Body + "<td style='width: 100px'>" + demiryeri + "</td>";
        //        Body = Body + "<td style='width: 80px'>" + bayrak + "</td>";
        //        Body = Body + "<td style='width: 30px'>" + tip + "</td>";
        //        Body = Body + "<td style='width: 50px'>" + grt + "</td>";
        //        Body = Body + "<td style='width: 95px'>" + acente + "/" + fatura + "</td>";
        //        Body = Body + "<td style='width: 65px'>" + bowt + "-" + strnt + "</td>";
        //        Body = Body + "<td style='width: 30px'>" + loa + "</td>";
        //        Body = Body + "<td style='width: 30px'>" + tehlikeliyuk + "</td>";
        //        Body = Body + "<td style='width: 40px'>" + draft + "</td>";
        //        Body = Body + "<td style='width: 40px'>" + bilgi + "</td>";
        //        Body = Body + "<td style='width: 100px'>" + eta + "</td>";
        //        Body = Body + "<td style='width: 140px'>" + notlar + "</td>";
        //        Body = Body + "<td style='width: 55px'>" + talepno + "</td></tr>";

        //        no5 = no5 + 1;
        //    }
        //}
        //dr5.Close();

        //6.TABLO
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
        int no6 = 1;

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
        dr6.Close();



        //8.TABLO
        Body = Body + "<tr><td style='width:1340px; border:1px;' colspan=20> <font color='red'> YARIMCA ANCHORAGE </font ></td></tr>";
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

        Body = Body + "</table></br>DENİZ KILAVUZLUK A.Ş.</br>Send by Pilot Monitoring System.</br></br>";


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


                Body = Body + "<tr  style='border:1px solid gray;'><td style='width:2%'>" + no2 + "</td>";
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




    protected void siptus_Click(object sender, EventArgs e)
    {
        Response.Redirect("sipd.aspx");
    }

    protected void sipyar_Click(object sender, EventArgs e)
    {
        Response.Redirect("sipy.aspx");
    }
}


