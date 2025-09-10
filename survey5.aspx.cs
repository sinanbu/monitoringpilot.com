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


public partial class survey5 : System.Web.UI.Page
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

                SqlCommand cmdDDLlim = new SqlCommand("SP_DDLarges", baglanti);
                cmdDDLlim.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDDLlim;
                DataSet ds = new DataSet();
                adapter.Fill(ds, "argeler");
                DDLarges.Items.Clear();
                DDLarges.DataValueField = "anketno";
                DDLarges.DataTextField = "anketadi";
                DDLarges.DataSource = ds;
                DDLarges.DataBind();
                DDLarges.ClearSelection();
                DDLarges.Items.FindByValue("5").Selected = true;

                cmdDDLlim.Dispose();

                LBAdayBasvuru.Visible = true;
                LBKaydetTest.Visible = false;
                LBadayduzelt.Visible = false;

                sonuc2.Enabled = false;
                sonuc3.Enabled = false;
                sonuc4.Enabled = false;

  

            }
            databagla();
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

        baglanti.Close();

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

    private void databagla()
    {
        SqlConnection baglanti = AnaKlas.baglan();
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from adaylar where id>0 order by id asc");
        GridView7.DataSource = DTlrsetgrid;
        GridView7.DataBind();

        string id = "";
        string soru = "";
        string cevap = "";
        string goster = "";

        SqlCommand cmdkapnodize = new SqlCommand("Select * from adaylartest where id= '"+ Convert.ToInt32(Buttonlbadd.CommandArgument)+ "' and goster = '1' ", baglanti);
        SqlDataReader kaprdr = cmdkapnodize.ExecuteReader();
        if (kaprdr.HasRows)
        {
            while (kaprdr.Read())
            {
                id = kaprdr["id"].ToString();
                soru = kaprdr["soru"].ToString();
                cevap = kaprdr["cevap"].ToString();
                goster = kaprdr["goster"].ToString();
               
            }
        }
        kaprdr.Close();

            TBpaddportname.Text = Buttonlbadd.CommandArgument + ". Soru";
            Lsecbir.Text = soru;

     
        SqlCommand cmdkapsirano = new SqlCommand("SP_seskapsirano", baglanti);
        cmdkapsirano.CommandType = CommandType.StoredProcedure;
        cmdkapsirano.Parameters.AddWithValue("@kapno", Convert.ToInt32(Session["kapno"]));
        cmdkapsirano.Parameters.Add("@kapsirano", SqlDbType.Int); // 
        cmdkapsirano.Parameters["@kapsirano"].Direction = ParameterDirection.Output;
        cmdkapsirano.ExecuteNonQuery();
        int seskapsirano = Convert.ToInt32(cmdkapsirano.Parameters["@kapsirano"].Value.ToString().Trim());
        cmdkapsirano.Dispose();

        if (seskapsirano > 999)
        {
            Buttonlbadd.Enabled = false;
            ButtonTesteBasla.Enabled = false;
        }

        /// eğer aday varsa ve puan hesapla eğer ilk soru büyük -1 se oku
               //Veritabanında kayıt var mı yok mu diye kontrol ediyor...
        SqlCommand cmd = new SqlCommand("select count(kapno) from adaylar where kapno ='" + (Session["kapno"]).ToString() + "' ", baglanti);
        int param = Convert.ToInt32(cmd.ExecuteScalar());

        if(param > 0)
        { 
        string cevapanahtarlimi = "-1";
        SqlCommand adaysc = new SqlCommand("Select s1 from adaylar where kapno='" + (Session["kapno"]).ToString() + "' order by id asc", baglanti);
        cevapanahtarlimi = adaysc.ExecuteScalar().ToString();
        adaysc.Dispose();

        if (cevapanahtarlimi!=null && cevapanahtarlimi != "" &&  Convert.ToInt32(cevapanahtarlimi)> -1)
        { 

        double lsapuan = 0;
        string soru1 = "-1";
        string soru2 = "-1";
        string soru3 = "-1";
        string soru4 = "-1";
        string soru5 = "-1";
        string soru6 = "-1";
        string soru7 = "-1";
        string soru8 = "-1";
        string soru9 = "-1";
        string soru10 = "-1";
        string soru11 = "-1";
        string soru12 = "-1";
        string soru13 = "-1";
        string soru14 = "-1";
        string soru15 = "-1";
        string soru16 = "-1";
        string soru17 = "-1";
        string soru18 = "-1";
        string soru19 = "-1";
        string soru20 = "-1";
        string soru21 = "-1";
        string soru22 = "-1";
        string soru23 = "-1";
        string soru24 = "-1";
        string soru25 = "-1";
        string soru26 = "-1";
        string soru27 = "-1";
        string soru28 = "-1";
        string soru29 = "-1";
        string soru30 = "-1";
        string soru31 = "-1";
        string soru32 = "-1";
        string soru33 = "-1";
        string soru34 = "-1";
        string soru35 = "-1";
        string soru36 = "-1";
        string soru37 = "-1";
        string soru38 = "-1";
        string soru39 = "-1";
        string soru40 = "-1";

            string cevapanahtar1 = "-1";
            string cevapanahtar2 = "-1";
            string cevapanahtar3 = "-1";
            string cevapanahtar4 = "-1";
            string cevapanahtar5 = "-1";
            string cevapanahtar6 = "-1";
            string cevapanahtar7 = "-1";
            string cevapanahtar8 = "-1";
            string cevapanahtar9 = "-1";
            string cevapanahtar10 = "-1";
            string cevapanahtar11 = "-1";
            string cevapanahtar12 = "-1";
            string cevapanahtar13 = "-1";
            string cevapanahtar14 = "-1";
            string cevapanahtar15 = "-1";
            string cevapanahtar16 = "-1";
            string cevapanahtar17 = "-1";
            string cevapanahtar18 = "-1";
            string cevapanahtar19 = "-1";
            string cevapanahtar20 = "-1";
            string cevapanahtar21 = "-1";
            string cevapanahtar22 = "-1";
            string cevapanahtar23 = "-1";
            string cevapanahtar24 = "-1";
            string cevapanahtar25 = "-1";
            string cevapanahtar26 = "-1";
            string cevapanahtar27 = "-1";
            string cevapanahtar28 = "-1";
            string cevapanahtar29 = "-1";
            string cevapanahtar30 = "-1";
            string cevapanahtar31 = "-1";
            string cevapanahtar32 = "-1";
            string cevapanahtar33 = "-1";
            string cevapanahtar34 = "-1";
            string cevapanahtar35 = "-1";
            string cevapanahtar36 = "-1";
            string cevapanahtar37 = "-1";
            string cevapanahtar38 = "-1";
            string cevapanahtar39 = "-1";
            string cevapanahtar40 = "-1";

            SqlCommand aday = new SqlCommand("Select * from adaylar where kapno='" + (Session["kapno"]).ToString() + "' order by id asc", baglanti);
        SqlDataReader adayr = aday.ExecuteReader();
        if (adayr.HasRows)
        {
            while (adayr.Read())
            {
                soru1 = adayr["s1"].ToString();
                soru2 = adayr["s2"].ToString();
                soru3 = adayr["s3"].ToString();
                soru4 = adayr["s4"].ToString();
                soru5 = adayr["s5"].ToString();
                soru6 = adayr["s6"].ToString();
                soru7 = adayr["s7"].ToString();
                soru8 = adayr["s8"].ToString();
                soru9 = adayr["s9"].ToString();
                soru10 = adayr["s10"].ToString();
                soru11 = adayr["s11"].ToString();
                soru12 = adayr["s12"].ToString();
                soru13 = adayr["s13"].ToString();
                soru14 = adayr["s14"].ToString();
                soru15 = adayr["s15"].ToString();
                soru16 = adayr["s16"].ToString();
                soru17 = adayr["s17"].ToString();
                soru18 = adayr["s18"].ToString();
                soru19 = adayr["s19"].ToString();
                soru20 = adayr["s20"].ToString();
                soru21 = adayr["s21"].ToString();
                soru22 = adayr["s22"].ToString();
                soru23 = adayr["s23"].ToString();
                soru24 = adayr["s24"].ToString();
                soru25 = adayr["s25"].ToString();
                soru26 = adayr["s26"].ToString();
                soru27 = adayr["s27"].ToString();
                soru28 = adayr["s28"].ToString();
                soru29 = adayr["s29"].ToString();
                soru30 = adayr["s30"].ToString();
                soru31 = adayr["s31"].ToString();
                soru32 = adayr["s32"].ToString();
                soru33 = adayr["s33"].ToString();
                soru34 = adayr["s34"].ToString();
                soru35 = adayr["s35"].ToString();
                soru36 = adayr["s36"].ToString();
                soru37 = adayr["s37"].ToString();
                soru38 = adayr["s38"].ToString();
                soru39 = adayr["s39"].ToString();
                soru40 = adayr["s40"].ToString();
                lsapuan =Convert.ToDouble(adayr["lsapuan"].ToString());
            }
        }
            kaprdr.Close();

            if (soru40 != null && soru40 != "" && Convert.ToInt32(soru40) > -1 &&  lsapuan == 0) // lsa puan hesaplama 
        {
                SqlConnection baglanti2 = AnaKlas.baglan();
                SqlCommand scev = new SqlCommand("Select id, cevap from adaylartest where id>0 and goster='1' order by id asc", baglanti2);
                SqlDataReader kaprdrc = scev.ExecuteReader();
                if (kaprdrc.HasRows)
                {
                    while (kaprdrc.Read())
                    {
                        if (kaprdrc["id"].ToString() == "1") { cevapanahtar1 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "2") { cevapanahtar2 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "3") { cevapanahtar3 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "4") { cevapanahtar4 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "5") { cevapanahtar5 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "6") { cevapanahtar6 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "7") { cevapanahtar7 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "8") { cevapanahtar8 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "9") { cevapanahtar9 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "10") { cevapanahtar10 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "11") { cevapanahtar11 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "12") { cevapanahtar12 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "13") { cevapanahtar13 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "14") { cevapanahtar14 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "15") { cevapanahtar15 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "16") { cevapanahtar16 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "17") { cevapanahtar17 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "18") { cevapanahtar18 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "19") { cevapanahtar19 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "20") { cevapanahtar20 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "21") { cevapanahtar21 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "22") { cevapanahtar22 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "23") { cevapanahtar23 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "24") { cevapanahtar24 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "25") { cevapanahtar25 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "26") { cevapanahtar26 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "27") { cevapanahtar27 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "28") { cevapanahtar28 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "29") { cevapanahtar29 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "30") { cevapanahtar30 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "31") { cevapanahtar31 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "32") { cevapanahtar32 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "33") { cevapanahtar33 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "34") { cevapanahtar34 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "35") { cevapanahtar35 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "36") { cevapanahtar36 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "37") { cevapanahtar37 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "38") { cevapanahtar38 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "39") { cevapanahtar39 = kaprdrc["cevap"].ToString(); }
                        if (kaprdrc["id"].ToString() == "40") { cevapanahtar40 = kaprdrc["cevap"].ToString(); }


                        if (cevapanahtar1=="1") {
                            if (soru1 == "0") {lsapuan=lsapuan+2.5; }
                            else if (soru1 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru1 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru1 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru1 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar1 = "-1";
                        }
                        else if (cevapanahtar1 == "5")
                        {
                            if (soru1 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru1 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru1 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru1 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru1 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar1 = "-1";
                        }


                        if (cevapanahtar2 == "1")
                        {
                            if (soru2 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru2 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru2 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru2 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru2 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar2 = "-1";
                        }
                        else if (cevapanahtar2 == "5")
                        {
                            if (soru2 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru2 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru2 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru2 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru2 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar2 = "-1";
                        }

                        if (cevapanahtar3 == "1")
                        {
                            if (soru3 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru3 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru3 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru3 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru3 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar3 = "-1";
                        }
                        else if (cevapanahtar3 == "5")
                        {
                            if (soru3 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru3 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru3 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru3 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru3 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar3 = "-1";
                        }

                        if (cevapanahtar4 == "1")
                        {
                            if (soru4 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru4 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru4 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru4 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru4 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar4 = "-1";
                        }
                        else if (cevapanahtar4 == "5")
                        {
                            if (soru4 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru4 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru4 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru4 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru4 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar4 = "-1";
                        }

                        if (cevapanahtar5 == "1")
                        {
                            if (soru5 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru5 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru5 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru5 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru5 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar5 = "-1";
                        }
                        else if (cevapanahtar5 == "5")
                        {
                            if (soru5 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru5 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru5 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru5 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru5 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar5 = "-1";
                        }

                        if (cevapanahtar6 == "1")
                        {
                            if (soru6 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru6 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru6 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru6 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru6 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar6 = "-1";
                        }
                        else if (cevapanahtar6 == "5")
                        {
                            if (soru6 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru6 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru6 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru6 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru6 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar6 = "-1";
                        }

                        if (cevapanahtar7 == "1")
                        {
                            if (soru7 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru7 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru7 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru7 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru7 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar7 = "-1";
                        }
                        else if (cevapanahtar7 == "5")
                        {
                            if (soru7 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru7 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru7 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru7 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru7 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar7 = "-1";
                        }

                        if (cevapanahtar8 == "1")
                        {
                            if (soru8 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru8 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru8 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru8 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru8 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar8 = "-1";
                        }
                        else if (cevapanahtar8 == "5")
                        {
                            if (soru8 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru8 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru8 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru8 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru8 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar8 = "-1";
                        }

                        if (cevapanahtar9 == "1")
                        {
                            if (soru9 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru9 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru9 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru9 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru9 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar9 = "-1";
                        }
                        else if (cevapanahtar9 == "5")
                        {
                            if (soru9 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru9 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru9 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru9 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru9 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar9 = "-1";
                        }

                        if (cevapanahtar10 == "1")
                        {
                            if (soru10 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru10 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru10 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru10 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru10 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar10 = "-1";
                        }
                        else if (cevapanahtar10 == "5")
                        {
                            if (soru10 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru10 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru10 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru10 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru10 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar10 = "-1";
                        }

                        if (cevapanahtar11 == "1")
                        {
                            if (soru11 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru11 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru11 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru11 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru11 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar11 = "-1";
                        }
                        else if (cevapanahtar11 == "5")
                        {
                            if (soru11 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru11 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru11 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru11 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru11 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar11 = "-1";
                        }
                        if (cevapanahtar12 == "1")
                        {
                            if (soru12 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru12 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru12 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru12 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru12 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar12 = "-1";
                        }
                        else if (cevapanahtar12 == "5")
                        {
                            if (soru12 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru12 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru12 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru12 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru12 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar12 = "-1";
                        }
                        if (cevapanahtar13 == "1")
                        {
                            if (soru13 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru13 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru13 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru13 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru13 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar13 = "-1";
                        }
                        else if (cevapanahtar13 == "5")
                        {
                            if (soru13 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru13 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru13 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru13 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru13 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar13 = "-1";
                        }
                        if (cevapanahtar14 == "1")
                        {
                            if (soru14 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru14 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru14 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru14 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru14 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar14 = "-1";
                        }
                        else if (cevapanahtar14 == "5")
                        {
                            if (soru14 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru14 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru14 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru14 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru14 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar14 = "-1";
                        }
                        if (cevapanahtar15 == "1")
                        {
                            if (soru15 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru15 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru15 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru15 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru15 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar15 = "-1";
                        }
                        else if (cevapanahtar15 == "5")
                        {
                            if (soru15 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru15 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru15 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru15 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru15 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar15 = "-1";
                        }
                        if (cevapanahtar16 == "1")
                        {
                            if (soru16 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru16 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru16 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru16 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru16 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar16 = "-1";
                        }
                        else if (cevapanahtar16 == "5")
                        {
                            if (soru16 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru16 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru16 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru16 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru16 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar16 = "-1";
                        }
                        if (cevapanahtar17 == "1")
                        {
                            if (soru17 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru17 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru17 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru17 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru17 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar17 = "-1";
                        }
                        else if (cevapanahtar17 == "5")
                        {
                            if (soru17 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru17 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru17 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru17 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru17 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar17 = "-1";
                        }
                        if (cevapanahtar18 == "1")
                        {
                            if (soru18 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru18 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru18 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru18 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru18 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar18 = "-1";
                        }
                        else if (cevapanahtar18 == "5")
                        {
                            if (soru18 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru18 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru18 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru18 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru18 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar18 = "-1";
                        }
                        if (cevapanahtar19 == "1")
                        {
                            if (soru19 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru19 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru19 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru19 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru19 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar19 = "-1";
                        }
                        else if (cevapanahtar19 == "5")
                        {
                            if (soru19 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru19 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru19 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru19 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru19 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar19 = "-1";
                        }
                        if (cevapanahtar20 == "1")
                        {
                            if (soru20 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru20 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru20 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru20 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru20 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar20 = "-1";
                        }
                        else if (cevapanahtar20 == "5")
                        {
                            if (soru20 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru20 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru20 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru20 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru20 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar20 = "-1";
                        }
                        if (cevapanahtar21 == "1")
                        {
                            if (soru21 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru21 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru21 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru21 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru21 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar21 = "-1";
                        }
                        else if (cevapanahtar21 == "5")
                        {
                            if (soru21 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru21 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru21 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru21 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru21 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar21 = "-1";
                        }
                        if (cevapanahtar22 == "1")
                        {
                            if (soru22 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru22 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru22 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru22 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru22 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar22 = "-1";
                        }
                        else if (cevapanahtar22 == "5")
                        {
                            if (soru22 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru22 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru22 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru22 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru22 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar22 = "-1";
                        }
                        if (cevapanahtar23 == "1")
                        {
                            if (soru23 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru23 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru23 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru23 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru23 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar23 = "-1";
                        }
                        else if (cevapanahtar23 == "5")
                        {
                            if (soru23 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru23 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru23 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru23 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru23 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar23 = "-1";
                        }
                        if (cevapanahtar24 == "1")
                        {
                            if (soru24 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru24 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru24 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru24 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru24 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar24 = "-1";
                        }
                        else if (cevapanahtar24 == "5")
                        {
                            if (soru24 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru24 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru24 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru24 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru24 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar24 = "-1";
                        }
                        if (cevapanahtar25 == "1")
                        {
                            if (soru25 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru25 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru25 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru25 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru25 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar25 = "-1";
                        }
                        else if (cevapanahtar25 == "5")
                        {
                            if (soru25 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru25 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru25 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru25 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru25 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar25 = "-1";
                        }
                        if (cevapanahtar26 == "1")
                        {
                            if (soru26 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru26 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru26 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru26 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru26 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar26 = "-1";
                        }
                        else if (cevapanahtar26 == "5")
                        {
                            if (soru26 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru26 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru26 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru26 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru26 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar26 = "-1";
                        }
                        if (cevapanahtar27 == "1")
                        {
                            if (soru27 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru27 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru27 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru27 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru27 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar27 = "-1";
                        }
                        else if (cevapanahtar27 == "5")
                        {
                            if (soru27 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru27 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru27 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru27 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru27 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar27 = "-1";
                        }
                        if (cevapanahtar28 == "1")
                        {
                            if (soru28 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru28 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru28 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru28 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru28 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar28 = "-1";
                        }
                        else if (cevapanahtar28 == "5")
                        {
                            if (soru28 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru28 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru28 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru28 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru28 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar28 = "-1";
                        }
                        if (cevapanahtar29 == "1")
                        {
                            if (soru29 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru29 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru29 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru29 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru29 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar29 = "-1";
                        }
                        else if (cevapanahtar29 == "5")
                        {
                            if (soru29 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru29 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru29 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru29 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru29 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar29 = "-1";
                        }
                        if (cevapanahtar30 == "1")
                        {
                            if (soru30 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru30 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru30 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru30 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru30 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar30 = "-1";
                        }
                        else if (cevapanahtar30 == "5")
                        {
                            if (soru30 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru30 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru30 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru30 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru30 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar30 = "-1";
                        }
                        if (cevapanahtar31 == "1")
                        {
                            if (soru31 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru31 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru31 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru31 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru31 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar31 = "-1";
                        }
                        else if (cevapanahtar31 == "5")
                        {
                            if (soru31 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru31 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru31 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru31 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru31 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar31 = "-1";
                        }
                        if (cevapanahtar32 == "1")
                        {
                            if (soru32 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru32 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru32 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru32 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru32 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar32 = "-1";
                        }
                        else if (cevapanahtar32 == "5")
                        {
                            if (soru32 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru32 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru32 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru32 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru32 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar32 = "-1";
                        }
                        if (cevapanahtar33 == "1")
                        {
                            if (soru33 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru33 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru33 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru33 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru33 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar33 = "-1";
                        }
                        else if (cevapanahtar33 == "5")
                        {
                            if (soru33 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru33 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru33 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru33 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru33 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar33 = "-1";
                        }
                        if (cevapanahtar34 == "1")
                        {
                            if (soru34 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru34 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru34 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru34 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru34 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar34 = "-1";
                        }
                        else if (cevapanahtar34 == "5")
                        {
                            if (soru34 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru34 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru34 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru34 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru34 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar34 = "-1";
                        }
                        if (cevapanahtar35 == "1")
                        {
                            if (soru35 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru35 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru35 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru35 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru35 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar35 = "-1";
                        }
                        else if (cevapanahtar35 == "5")
                        {
                            if (soru35 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru35 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru35 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru35 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru35 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar35 = "-1";
                        }
                        if (cevapanahtar36 == "1")
                        {
                            if (soru36 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru36 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru36 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru36 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru36 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar36 = "-1";
                        }
                        else if (cevapanahtar36 == "5")
                        {
                            if (soru36 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru36 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru36 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru36 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru36 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar36 = "-1";
                        }
                        if (cevapanahtar37 == "1")
                        {
                            if (soru37 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru37 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru37 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru37 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru37 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar37 = "-1";
                        }
                        else if (cevapanahtar37 == "5")
                        {
                            if (soru37 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru37 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru37 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru37 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru37 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar37 = "-1";
                        }
                        if (cevapanahtar38 == "1")
                        {
                            if (soru38 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru38 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru38 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru38 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru38 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar38 = "-1";
                        }
                        else if (cevapanahtar38 == "5")
                        {
                            if (soru38 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru38 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru38 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru38 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru38 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar38 = "-1";
                        }
                        if (cevapanahtar39 == "1")
                        {
                            if (soru39 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru39 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru39 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru39 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru39 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar39 = "-1";
                        }
                        else if (cevapanahtar39 == "5")
                        {
                            if (soru39 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru39 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru39 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru39 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru39 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar39 = "-1";
                        }
                        if (cevapanahtar40 == "1")
                        {
                            if (soru40 == "0") { lsapuan = lsapuan + 2.5; }
                            else if (soru40 == "1") { lsapuan = lsapuan + 2; }
                            else if (soru40 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru40 == "3") { lsapuan = lsapuan + 1; }
                            else if (soru40 == "4") { lsapuan = lsapuan + 0.5; }
                            cevapanahtar40 = "-1";
                        }
                        else if (cevapanahtar40 == "5")
                        {
                            if (soru40 == "0") { lsapuan = lsapuan + 0.5; }
                            else if (soru40 == "1") { lsapuan = lsapuan + 1; }
                            else if (soru40 == "2") { lsapuan = lsapuan + 1.5; }
                            else if (soru40 == "3") { lsapuan = lsapuan + 2; }
                            else if (soru40 == "4") { lsapuan = lsapuan + 2.5; }
                            cevapanahtar40 = "-1";
                        }

                       
                    }
                }
                kaprdrc.Close();

           

                SqlCommand adaysok = new SqlCommand("update adaylar set lsapuan=@lsapuan where (kapno='" + Session["kapno"].ToString() + "')", baglanti2);
                adaysok.Parameters.AddWithValue("kapno", Session["kapno"].ToString());
                adaysok.Parameters.AddWithValue("lsapuan", lsapuan-10);
                adaysok.ExecuteNonQuery();
                adaysok.Dispose();
     baglanti2.Close();

            }



        }
        baglanti.Close();
        }
    }


    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {


        if (TBpaddsec.SelectedIndex > -1)
        {
            Buttonlbadd.CommandArgument = (Convert.ToInt32(Buttonlbadd.CommandArgument) + 1).ToString();

            int secenek = TBpaddsec.SelectedIndex;
            //int sid = Convert.ToInt32(Buttonlbadd.CommandArgument);
            string scol = Buttonlbadd.CommandArgument;

            bool sonuc = addport(secenek, scol);

            if (sonuc)
            {

            TBpaddsec.SelectedIndex = -1;

            if (Buttonlbadd.CommandArgument == "40")
            {
                Buttonlbadd.Text = " BİTTİ ";
            }

            if (Buttonlbadd.CommandArgument == "41")
            {
                Buttonlbadd.CommandArgument = "1";
                    Buttonlbaddcancel.Enabled = true;
                }
            else
            {
                this.ModalPopupExtenderlbadd.Show();
            }

            databagla();
            }
        }
        else
        { this.ModalPopupExtenderlbadd.Show(); }
    }
    

private bool addport(int secenek, string scol)
{
    bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();

        if (scol == "2")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s1=@s1 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s1", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "3")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s2=@s2 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s2", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "4")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s3=@s3 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s3", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "5")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s4=@s4 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s4", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "6")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s5=@s5 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s5", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "7")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s6=@s6 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s6", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "8")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s7=@s7 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s7", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "9")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s8=@s8 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s8", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "10")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s9=@s9 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s9", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "11")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s10=@s10 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s10", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "12")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s11=@s11 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s11", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "13")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s12=@s12 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s12", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "14")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s13=@s13 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s13", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "15")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s14=@s14 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s14", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "16")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s15=@s15 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s15", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "17")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s16=@s16 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s16", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "18")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s17=@s17 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s17", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "19")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s18=@s18 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s18", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "20")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s19=@s19 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s19", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "21")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s20=@s20 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s20", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "22")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s21=@s21 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s21", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "23")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s22=@s22 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s22", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "24")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s23=@s23 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s23", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "25")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s24=@s24 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s24", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "26")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s25=@s25 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s25", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "27")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s26=@s26 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s26", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "28")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s27=@s27 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s27", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "29")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s28=@s28 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s28", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "30")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s29=@s29 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s29", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "31")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s30=@s30 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s30", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "32")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s31=@s31 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s31", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "33")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s32=@s32 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s32", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "34")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s33=@s33 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s33", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "35")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s34=@s34 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s34", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "36")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s35=@s35 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s35", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "37")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s36=@s36 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s36", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "38")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s37=@s37 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s37", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "39")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s38=@s38 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s38", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "40")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s39=@s39 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s39", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }
        else if (scol == "41")
        {
            SqlCommand cmd = new SqlCommand("update adaylar set s40=@s40 where (kapno='" + Session["kapno"].ToString() + "')", baglanti);
            cmd.Parameters.AddWithValue("s40", secenek);
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            cmd.Dispose();
        }



        baglanti.Close();
    return sonuc;

}



    protected void DDLarges_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand onaybak = new SqlCommand("Select aciklama from argem where (anketno='" + DDLarges.SelectedItem.Value + "')  ", baglanti);
        string sayfalink = onaybak.ExecuteScalar().ToString();
        onaybak.Dispose();


        Response.Redirect(sayfalink);


        baglanti.Close();
    }

    protected void LBAdayBasvuru_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        string adayonay = "0";
        SqlCommand aday = new SqlCommand("Select * from adaylar where kapno='" + (Session["kapno"]).ToString() + "' order by id asc", baglanti);
        SqlDataReader adayr = aday.ExecuteReader();
            if (adayr.HasRows)
            {
                while (adayr.Read())
                {
                    sonuc1.Text = adayr["kapadisoyadi"].ToString();
                    sonuc2.Text = adayr["mezuniyet"].ToString();
                    sonuc3.Text = adayr["karatecrube"].ToString();
                    sonuc4.Text = adayr["yetenek"].ToString();
                    adayonay = adayr["adayonay"].ToString();
                    adayresim.Src = adayr["resimyolu"].ToString(); 
                }

                LBAdayBasvuru.Visible = true;
                LBKaydetTest.Visible = false;
                LBadayduzelt.Visible = false;
                sonuc2.Enabled = true;
                sonuc3.Enabled = true;
                sonuc4.Enabled = true;

                if (adayonay == "1")
                {
                    LBadayduzelt.Visible = true;
                    LBAdayBasvuru.Visible = false;
                    LBKaydetTest.Visible = false;
                }
            }    
            else
            {
                LBAdayBasvuru.Visible = false;
                LBKaydetTest.Visible = true;
                LBadayduzelt.Visible = false;
                sonuc2.Enabled = true;
                sonuc3.Enabled = true;
                sonuc4.Enabled = true;
                sonuc1.Text = LBonline.Text;
                sonuc2.Text = "";
                sonuc3.Text = "";
                sonuc4.Text = "";
                adayresim.Src = "images/adaysec/cansel.jpg";
            }

            adayr.Close();
            adayr.Dispose();
        baglanti.Close();
    }


    protected void pilotod_Click(object sender, EventArgs e)
    {

        LinkButton LBkop = sender as LinkButton;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand aday = new SqlCommand("Select * from adaylar where kapno='"+LBkop.CommandArgument+"' order by id asc", baglanti);
        SqlDataReader kaprdr = aday.ExecuteReader();
        string adayonay = "0";

        if (kaprdr.HasRows)
        {
            while (kaprdr.Read())
            {
                sonuc1.Text = kaprdr["kapadisoyadi"].ToString();
                sonuc2.Text = kaprdr["mezuniyet"].ToString();
                sonuc3.Text = kaprdr["karatecrube"].ToString();
                sonuc4.Text = kaprdr["yetenek"].ToString();
                adayonay= kaprdr["adayonay"].ToString();
            }
        }
        kaprdr.Close();
        aday.Dispose();


        if (LBkop.CommandArgument == (Session["kapno"]).ToString())
        {
            LBAdayBasvuru.Visible = true;
            LBKaydetTest.Visible = false;
            LBadayduzelt.Visible = false;
            sonuc2.Enabled = true;
            sonuc3.Enabled = true;
            sonuc4.Enabled = true;
            if (adayonay=="1")
            {
                LBadayduzelt.Visible = true;
                LBAdayBasvuru.Visible = false;
                LBKaydetTest.Visible = false;
            }
        }
        else
        {
            SqlCommand sezonkapno = new SqlCommand("Select adayonay from adaylar where kapno='" + (Session["kapno"]).ToString() + "' order by id asc", baglanti);
            if (sezonkapno.ExecuteScalar() == null)
            {
                LBAdayBasvuru.Visible = true;
            }

            LBKaydetTest.Visible = false;
            LBadayduzelt.Visible = false;
            sonuc2.Enabled = false;
            sonuc3.Enabled = false;
            sonuc4.Enabled = false;
        }

        adayresim.Src = LBkop.CommandName;


        baglanti.Close();
    }


    protected void LBKaydetTest_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        string adayonay = "0";
        SqlCommand aday = new SqlCommand("Select * from adaylar where kapno='" + (Session["kapno"]).ToString() + "' order by id asc", baglanti);
        SqlDataReader adayr = aday.ExecuteReader();
        if (adayr.HasRows)
        {
            while (adayr.Read())
            {
                sonuc1.Text = adayr["kapadisoyadi"].ToString();
                sonuc2.Text = adayr["mezuniyet"].ToString();
                sonuc3.Text = adayr["karatecrube"].ToString();
                sonuc4.Text = adayr["yetenek"].ToString();
                adayonay = adayr["adayonay"].ToString();
                adayresim.Src = adayr["resimyolu"].ToString();
            }

            LBAdayBasvuru.Visible = true;
            LBKaydetTest.Visible = false;
            LBadayduzelt.Visible = false;
            sonuc2.Enabled = true;
            sonuc3.Enabled = true;
            sonuc4.Enabled = true;

            if (adayonay == "1")
            {
                LBadayduzelt.Visible = true;
                LBAdayBasvuru.Visible = false;
                LBKaydetTest.Visible = false;
            }
            adayr.Close();
            adayr.Dispose();
        }
        else
        {
            if (sonuc2.Text != null && sonuc2.Text != "" &&  Convert.ToInt32(sonuc2.Text)>1950 && Convert.ToInt32(sonuc2.Text) < 2020 && sonuc3.Text.Length > 10 && sonuc4.Text.Length > 10)
            {
                SqlConnection baglanti2 = AnaKlas.baglan();
                SqlCommand adaysok = new SqlCommand("insert into adaylar (kapno,kapadisoyadi,lsapuan,mezuniyet,karatecrube,yetenek,adayonay,resimyolu) values (@kapno,@kapadisoyadi,@lsapuan,@mezuniyet,@karatecrube,@yetenek,@adayonay,@resimyolu)", baglanti2);
                adaysok.Parameters.AddWithValue("kapno", Session["kapno"].ToString());
                adaysok.Parameters.AddWithValue("kapadisoyadi", sonuc1.Text);
                adaysok.Parameters.AddWithValue("lsapuan", "0");
                adaysok.Parameters.AddWithValue("mezuniyet", sonuc2.Text);
                adaysok.Parameters.AddWithValue("karatecrube", sonuc3.Text);
                adaysok.Parameters.AddWithValue("yetenek", sonuc4.Text);
                adaysok.Parameters.AddWithValue("adayonay", "1");
                adaysok.Parameters.AddWithValue("resimyolu", "images/adaysec/" + TemizleINGoldu(sonuc1.Text.ToLower()));
                adaysok.ExecuteNonQuery();
                adaysok.Dispose();

                baglanti2.Close();
                Buttonlbadd.Visible = false;
                ButtonTesteBasla.Visible = true;
                sorular.Visible = false;
                cevaplar.Visible = false;
                aciklama.Visible = true;
                kayok.Visible = true;
                Buttonlbadd.CommandArgument = "1";
                Buttonlbadd.Text = " NEXT ";
                this.ModalPopupExtenderlbadd.Show();
            }
        }



        baglanti.Close();

    }


    protected void LBadayduzelt_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        string lsapuan = "0";
        SqlCommand aday = new SqlCommand("Select * from adaylar where kapno='" + (Session["kapno"]).ToString() + "' order by id asc", baglanti);
        SqlDataReader adayr = aday.ExecuteReader();
        if (adayr.HasRows)
        {
            while (adayr.Read())
            {
                //sonuc1.Text = adayr["kapadisoyadi"].ToString();
                //sonuc2.Text = adayr["mezuniyet"].ToString();
                //sonuc3.Text = adayr["karatecrube"].ToString();
                //sonuc4.Text = adayr["yetenek"].ToString();
                lsapuan = adayr["lsapuan"].ToString();
            }

            SqlConnection baglanti2 = AnaKlas.baglan();
            SqlCommand adaysok = new SqlCommand("update adaylar set kapno=@kapno,kapadisoyadi=@kapadisoyadi,mezuniyet=@mezuniyet,karatecrube=@karatecrube,yetenek=@yetenek,adayonay=@adayonay where (kapno='" + Session["kapno"].ToString() + "')", baglanti2);
            adaysok.Parameters.AddWithValue("kapno", Session["kapno"].ToString());
            adaysok.Parameters.AddWithValue("kapadisoyadi", sonuc1.Text);
            adaysok.Parameters.AddWithValue("mezuniyet", sonuc2.Text);
            adaysok.Parameters.AddWithValue("karatecrube", sonuc3.Text);
            adaysok.Parameters.AddWithValue("yetenek", sonuc4.Text);
            adaysok.Parameters.AddWithValue("adayonay", "1");
            adaysok.ExecuteNonQuery();
            adaysok.Dispose();
            baglanti2.Close();

            if (lsapuan == "0")
            {
                Buttonlbadd.Visible = false;
                ButtonTesteBasla.Visible = true;
                sorular.Visible = false;
                cevaplar.Visible = false;
                aciklama.Visible = true;
                kayok.Visible = true;
                Buttonlbadd.CommandArgument = "1";
                Buttonlbadd.Text = " NEXT ";
                this.ModalPopupExtenderlbadd.Show();
            }

            adayr.Close();
            adayr.Dispose();
            baglanti.Close();
        }

    }

    protected void ButtonTesteBasla_Click(object sender, EventArgs e)
    {
        Buttonlbadd.Visible = true;
        ButtonTesteBasla.Visible = false;
        sorular.Visible = true;
        cevaplar.Visible = true;
        aciklama.Visible = false;
        kayok.Visible = false;
        Buttonlbaddcancel.Enabled= false;

        this.ModalPopupExtenderlbadd.Show();
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
        if (Convert.ToInt32(Session["yetki"]) == 1 || Convert.ToInt32(Session["yetki"]) == 2)
        {

        }
        else if (Convert.ToInt32(Session["yetki"]) == 6 || Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }
    protected void GridView7_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView7.SelectedIndex)
        {
            e.Cancel = true;
            GridView7.SelectedIndex = -1;
            databagla();
        }

    }
    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)

        {
            LinkButton pod = (LinkButton)e.Row.FindControl("pilotod");
            //LinkButton psoyod = (LinkButton)e.Row.FindControl("shipod");
            Label kapnokop = (Label)e.Row.FindControl("kapno");

            if (kapnokop.Text == (Session["kapno"]).ToString())
            {

                pod.BackColor = Color.LightSalmon;
                //psoyod.BackColor = Color.LightSalmon;
                //GridView7.Rows[e.Row.RowIndex].BackColor = Color.Red;

            }


        }
    }

    public string TemizleINGoldu(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace(" ", "");
        deger = deger.Replace("ç", "c");
        deger = deger.Replace("ğ", "g");
        deger = deger.Replace("ı", "i");
        deger = deger.Replace("ö", "o");
        deger = deger.Replace("ş", "s");
        deger = deger.Replace("ü", "u");

        deger = deger.Trim();
        return deger;
    }

}






