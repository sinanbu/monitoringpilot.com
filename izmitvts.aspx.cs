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

public partial class izmitvts : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();
    int showdurum = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();

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
        else if (Session["kapno"].ToString() != "99" && Session["kapno"].ToString() != "109")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else  //if (cmdlogofbak.ExecuteScalar() != null)
        {

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

              LblVarid.Text = "Dekaş Pilot Watch." + varbilvarid.Text;
            LblVarbit.Text = " / Change Time:" + ' ' + varbiter.Text;

            DTloading();
            }
        }

        baglanti.Close();

    }  

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        LiteralYaz();
        DTloading();

        if (showdurum == 1)
        { this.MPEdrawing.Show(); }
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

          //duyuru
        SqlCommand duyurubak = new SqlCommand("SP_duyurubak", baglanti);
        duyurubak.CommandType = CommandType.StoredProcedure;
        duyurubak.Parameters.Add("@duyurusonuc", SqlDbType.Char, 500);
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


            //demir yeri okuyup çekmek için
            Label Lblimonokop = (Label)e.Item.FindControl("Lblimono");
            string imonobul = Lblimonokop.Text;

            if (imonobul != "0")
            {
                SqlCommand cmdisupbak = new SqlCommand("SP_Isliste_AnchPlace_Fmimo", baglanti);
                cmdisupbak.CommandType = CommandType.StoredProcedure;
                cmdisupbak.Parameters.AddWithValue("@imono", imonobul);
                cmdisupbak.Parameters.Add("@demiryerial", SqlDbType.VarChar, 20);
                cmdisupbak.Parameters["@demiryerial"].Direction = ParameterDirection.Output;
                cmdisupbak.ExecuteNonQuery();
                string demiryerim = cmdisupbak.Parameters["@demiryerial"].Value.ToString().Trim();
                cmdisupbak.Dispose();

                // demirler kısaltma
                Label demirkisakop = (Label)e.Item.FindControl("Ldemiryeri");
                if (demiryerim.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
                else if (demiryerim.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
                else if (demiryerim.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
                else if (demiryerim.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
                else if (demiryerim.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
                else if (demiryerim.ToLower() == "ordino-talep yok")
                {
                    demirkisakop.Text = "OTY";
                    demirkisakop.Text = "Ordino-Talep Yok";
                    demirkisakop.Style.Add("font-style", "normal");
                    demirkisakop.Style.Add("color", "#ee1111");
                }
                else if (demiryerim.ToLower() == "demir-izni yok")
                {
                    demirkisakop.Text = "DİY";
                    demirkisakop.Style.Add("font-style", "normal");
                    demirkisakop.Style.Add("color", "#ee1111");
                }
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
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btninisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }





        baglanti.Close();



    }

    protected void DLDaricay_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            
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

            // port plan için yelkenkaya bold degil
            LinkButton Lblportcolorb = (LinkButton)e.Item.FindControl("binisport");
            if (Lblportcolorb.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolorb.Style.Add("font-weight", "normal");
                Lblportcolorb.Style.Add("cursor", "default");
          
            
        }
            Lblportcolorb.Style.Add("color", "#b85503");

            LinkButton Lblportcolori = (LinkButton)e.Item.FindControl("inisport");
            if (Lblportcolori.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolori.Style.Add("font-weight", "normal");
                Lblportcolori.Style.Add("cursor", "default");
            
        }
            Lblportcolori.Style.Add("color", "#b85503");

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

            //demir yeri okuyup çekmek için
            Label Lblimonokop = (Label)e.Item.FindControl("Lblimonoy");
            string imonobul = Lblimonokop.Text;

            if (imonobul != "0")
            {
                SqlCommand cmdisupbak = new SqlCommand("SP_Isliste_AnchPlace_Fmimo", baglanti);
                cmdisupbak.CommandType = CommandType.StoredProcedure;
                cmdisupbak.Parameters.AddWithValue("@imono", imonobul);
                cmdisupbak.Parameters.Add("@demiryerial", SqlDbType.VarChar, 20);
                cmdisupbak.Parameters["@demiryerial"].Direction = ParameterDirection.Output;
                cmdisupbak.ExecuteNonQuery();
                string demiryerim = cmdisupbak.Parameters["@demiryerial"].Value.ToString().Trim();
                cmdisupbak.Dispose();

                // demirler kısaltma
                Label demirkisakop = (Label)e.Item.FindControl("Ldemiryeriy");
                if (demiryerim.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
                else if (demiryerim.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
                else if (demiryerim.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
                else if (demiryerim.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
                else if (demiryerim.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
                else if (demiryerim.ToLower() == "ordino-talep yok")
                {
                    demirkisakop.Text = "OTY";
                    demirkisakop.Text = "Ordino-Talep Yok";
                    demirkisakop.Style.Add("font-style", "normal");
                    demirkisakop.Style.Add("color", "#ee1111");
                }
                else if (demiryerim.ToLower() == "demir-izni yok")
                {
                    demirkisakop.Text = "DİY";
                    demirkisakop.Style.Add("font-style", "normal");
                    demirkisakop.Style.Add("color", "#ee1111");
                }
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
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btninisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
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



    protected void Buttonlbaddcancel_Click(object sender, EventArgs e)
    {
        showdurum = 0;
        MPEdrawing.Controls.Clear();


    }


    protected void Buttonlbaddplan_Click(object sender, EventArgs e)
    {
        showdurum = 1;
        this.MPEdrawing.Show();

    }

}


