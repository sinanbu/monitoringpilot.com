using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;

public partial class yonet_pilots : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }

        else if (cmdlogofbak.ExecuteScalar() == null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }

        else if (Session["yetki"].ToString() == "9" || Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
        {
            if (!IsPostBack)
                databaglailk();
            yetkiaciklama.Visible = true;

            if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
            {
                TBPEkapadi.Enabled = false;
                TBPEkapsoyadi.Enabled = false;
                TBPEeposta.Enabled = false;
                TBPEsifre.Visible = false;
                LitPasstext.Visible = false;
                TBPEtel1.Enabled = false;
                TBPEtel2.Enabled = false;
                TBPEaddress.Enabled = false;
                TBPEdogumtar.Enabled = false;
                TBPEdogumyer.Enabled = false;
                TBPEyetki.Enabled = false;
                TBPEisegiristarihi.Enabled = false;
                TBPEkidemliolmatarihi.Enabled = false;
                TBPEemekli.Enabled = false;
                TBPEemeklitarihi.Enabled = false;
                TBPEkidem.Enabled = false;
                TBPEvarid.Enabled = false;
                TBPEgirisistasyon.Enabled = false;
                TBPEcikisistasyon.Enabled = false;

                TBEkapsirano.Enabled = false;
                DDLpostasmsal.Enabled = false;
                TBEdegismecikapno.Enabled = false;
                TBEdegismeciadi.Enabled = false;
                TBEdegismecisoyadi.Enabled = false;
                DDLdegismeciorgkidem.Enabled = false;
                TBEvarno.Enabled = false;
                DDLrespist.Enabled = false;
                DDLdurum.Enabled = false;
                TBEistasyongelis.Enabled = false;

                Baddnewpilot.Visible = false;
                LBPEditmode.Visible = false;
                ButtonPilotEDT.Visible = false;
                Litpagebaslik.Text = "PMTR User Page";
    

                Litmenu1.Visible = false;
                if (Session["yetki"].ToString() == "0")
                {
                    Litmenu4.Visible = false;
                    Litmenu5.Visible = false;
                    Litmenu6.Visible = false;
                }

            }
            else
            {
                TBPEkapadi.Enabled = false;
                TBPEkapsoyadi.Enabled = false;
                TBPEeposta.Enabled = false;
                TBPEsifre.Visible = false;
                LitPasstext.Visible = false;
                TBPEtel1.Enabled = false;
                TBPEtel2.Enabled = false;
                TBPEaddress.Enabled = false;
                TBPEdogumtar.Enabled = false;
                TBPEdogumyer.Enabled = false;
                TBPEyetki.Enabled = false;
                TBPEisegiristarihi.Enabled = false;
                TBPEkidemliolmatarihi.Enabled = false;
                TBPEemekli.Enabled = false;
                TBPEemeklitarihi.Enabled = false;
                TBPEkidem.Enabled = false;
                TBPEvarid.Enabled = false;
                TBPEgirisistasyon.Enabled = false;
                TBPEcikisistasyon.Enabled = false;

                TBEkapsirano.Enabled = false;
                DDLpostasmsal.Enabled = false;
                TBEdegismecikapno.Enabled = false;
                TBEdegismeciadi.Enabled = false;
                TBEdegismecisoyadi.Enabled = false;
                DDLdegismeciorgkidem.Enabled = false;
                TBEvarno.Enabled = false;
                DDLrespist.Enabled = false;
                DDLdurum.Enabled = false;
                TBEistasyongelis.Enabled = false;

                ButtonPilotEDT.Visible = false;
                LBPEditmode.Visible = true;
                Litpagebaslik.Text = "PMTR Admin Page";


            }
        }

        else 
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        baglanti.Close();

    }
    protected void LBPEditmode_Click(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

        if (Session["yetki"].ToString() == "9")
        {
            TBPEkapadi.Enabled = true;
            TBPEkapsoyadi.Enabled = true;
            TBPEeposta.Enabled = true;
            TBPEsifre.Visible = true;
            LitPasstext.Visible = true;
            TBPEtel1.Enabled = true;
            TBPEtel2.Enabled = true;
            TBPEaddress.Enabled = true;
            TBPEdogumtar.Enabled = true;
            TBPEdogumyer.Enabled = true;
            TBPEisegiristarihi.Enabled = true;
            TBPEkidemliolmatarihi.Enabled = true;
            TBPEemekli.Enabled = true;
            TBPEemeklitarihi.Enabled = true;
            TBPEkidem.Enabled = true;
            TBPEvarid.Enabled = true;
            TBPEgirisistasyon.Enabled = true;
            TBPEcikisistasyon.Enabled = true;

            ButtonPilotEDT.Visible = true;
            LBPEditmode.Visible = false;

            TBEkapsirano.Enabled = true;
            DDLpostasmsal.Enabled = true;
            TBEdegismecikapno.Enabled = true;
            TBEdegismeciadi.Enabled = true;
            TBEdegismecisoyadi.Enabled = true;
            DDLdegismeciorgkidem.Enabled = true;
            TBEvarno.Enabled = true;
            DDLrespist.Enabled = true;
            DDLdurum.Enabled = true;
            TBEistasyongelis.Enabled = true;

            if (Session["kapno"].ToString() != LBPEditmode.CommandArgument.ToString())
        {
            TBPEyetki.Enabled = true;
        }
            }
        else if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
        {
            TBPEeposta.Enabled = true;
            TBPEsifre.Visible = true;
            LitPasstext.Visible = true;
            TBPEtel1.Enabled = true;
            TBPEtel2.Enabled = true;
            TBPEaddress.Enabled = true;

            ButtonPilotEDT.Visible = true;
            LBPEditmode.Visible = false;
        }
        else
        { }


        this.ModalPopupExtenderPilotEdit.Show();
    }
      private void databaglailk()
    {
        yetkiaciklama.Visible = false;

        DropDownListports.Items.Clear();
        DropDownListports.Items.Add("Seçiniz..");

        DropDownListports.Items.Add("Tüm Kullanıcılar");
        DropDownListports.Items.Add("Tüm Kılavuz Kaptanlar");
        DropDownListports.Items.Add("1.Vardiya Kaptanları"); // yetki:0, kapsirano<1000
        DropDownListports.Items.Add("2.Vardiya Kaptanları"); // yetki:0, kapsirano<1000
        DropDownListports.Items.Add("3.Vardiya Kaptanları"); // yetki:0, kapsirano<1000

        DropDownListports.Items.Add("Gözcüler");          // yetki:1 veya yetki:2, kapsirano:1000-2000 
        DropDownListports.Items.Add("Şirket Müdürü");     // yetki:3, kapsirano:2000-3000
        DropDownListports.Items.Add("Şirket Çalışanı");   // yetki:4, kapsirano:3000-4000
        DropDownListports.Items.Add("Dış Firmalar");      // yetki:5, kapsirano:4000-5000
        DropDownListports.Items.Add("Stajer Pilot");      // yetki:6, kapsirano<1000
        DropDownListports.Items.Add("Stajer Gözcü");      // yetki:7, kapsirano:1000-2000
        DropDownListports.Items.Add("Pilot Botu Kaptanı");// yetki:8, kapsirano:3000-4000

        DropDownListports.Items.Add("Emekliler");         // Emekli:Yes

        DropDownListports.Items.Add("Admins");         // Yetki:9

        DropDownListports.DataBind();

    }
      private void databagla()
    {

        //if (Convert.ToInt32(Session["yetki"]) >= 0 && Convert.ToInt32(Session["yetki"]) < 9)
        //{
        // //  GridView1.FindControl("IBPilotEdit").Visible = (false);
        //  //  GridView1.NamingContainer.FindControl("IBPilotEdit").Visible = (false);
        //}

        string secililist = DropDownListports.SelectedItem.Text.ToString();
        if (secililist == "Select?")
        {
        }



         else if (secililist == "Tüm Kullanıcılar")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "Tüm Kılavuz Kaptanlar")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and yetki='0' and kapsirano<1000 order by varid, kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "1.Vardiya Kaptanları")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and kapsirano<1000 and varid='1' and (yetki='0' or yetki='9') order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "2.Vardiya Kaptanları")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and kapsirano<1000 and varid='2' and  (yetki='0' or yetki='9') order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "3.Vardiya Kaptanları")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and kapsirano<1000 and varid='3' and  (yetki='0' or yetki='9')order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }


        else if (secililist == "Gözcüler")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and (yetki='1' or yetki='2')   and kapsirano>1000 and kapsirano<2000 order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "Şirket Müdürü")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and yetki='3'   and kapsirano>2000 and kapsirano<3000 order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "Şirket Çalışanı")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and yetki='4'   and kapsirano>3000 and kapsirano<4000 order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "Dış Firmalar")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and yetki='5'   and kapsirano>4000 and kapsirano<5000 order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }

        else if (secililist == "Stajer Pilot")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and yetki='6'  and kapsirano<1000 order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "Stajer Gözcü")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and yetki='7'  and kapsirano>1000 and kapsirano<2000  order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }
        else if (secililist == "Pilot Botu Kaptanı")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='No' and yetki='8'  and kapsirano>3000 and kapsirano<4000 order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }

        else if (secililist == "Emekliler")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where emekli='Yes' order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }

        else if (secililist == "Admins")
        {
            DataTable DTrihtim = AnaKlas.GetDataTable("Select *, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where yetki='9' order by kapadisoyadi asc");
            GridView1.DataSource = DTrihtim;
        }


        GridView1.DataBind();
    }
    protected void DropDownListports_SelectedIndexChanged(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

        databagla();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        yetkiaciklama.Visible = false;

        if (e.NewSelectedIndex == GridView1.SelectedIndex)
        {
            e.Cancel = true;
            GridView1.SelectedIndex = -1;
            databagla();
        }

    }

    protected void Baddnewpilot_Click(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where kapsirano<1000", baglanti);
        int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        sonkapsirano = sonkapsirano + 1;
        baglanti.Close();

        TBPADDkapsirano.Text = sonkapsirano.ToString();
        TBPADDkapadi.Text = "";
        TBPADDkapsoyadi.Text = "";
        TBPADDeposta.Text = "";
        TBPADDsifre.Text = "";
        TBPADDtel1.Text = "";
        TBPADDtel2.Text = "";
        TBPADDaddress.Text = "";
        TBPADDdogumtar.Text = "";
        //TBPADDdogumyer.SelectedItem.Text

        TBPADDkapadi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDkapsoyadi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDeposta.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDsifre.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDtel1.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDtel2.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDdogumtar.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDaddress.BorderColor = System.Drawing.Color.FromName("gray");


        this.ModalPopupExtenderpilotadd.Show();
    }
 
    protected void Baddnewpilotsave_Click(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

         int kapsirano = Convert.ToInt32(TBPADDkapsirano.Text); 
         string kapadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBPADDkapadi.Text.ToString().Trim().ToLower())); //30
         string kapsoyadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBPADDkapsoyadi.Text.ToString().Trim().ToLower()));  //30
         string eposta = TBPADDeposta.Text.ToString().Trim().ToLower(); //30
         string sifre = TBPADDsifre.Text; //30
         string tel1 = TBPADDtel1.Text; //15
         string tel2 = TBPADDtel2.Text; //15

         string address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBPADDaddress.Text.ToString().Trim().ToLower()));
         string dogumtar = TBPADDdogumtar.Text; //1
         string dogumyer = TBPADDdogumyer.SelectedItem.Text; //

        string emeklitarihi = AnaKlas.TarihYaziYapDMY(Convert.ToDateTime(dogumtar).AddYears(65));
        string isegiristarihi = AnaKlas.TarihYaziYapDMY(DateTime.Now); //10
        string kidemliolmatarihi = AnaKlas.TarihYaziYapDMY(DateTime.Now); //10

        string postasmsal = "0";
        string kidem = "4"; //1
        string ilkkayitzamani = AnaKlas.TarihSaatYaziYapDMYhm(DateTime.Now);
        string emekli = "No"; //1
        string yetki = TBPADDyetki.SelectedItem.Value.ToString(); //1
        string durum = "0";
        string varno = AnaKlas.varnohesaplaonceki();

        string varid = "1"; //1
        string girisistasyon = "1"; //1
        string cikisistasyon = "1"; //1
        string respist = "1"; //1
        string degismeciadi = kapadi;
        string degismecisoyadi = kapsoyadi;
        int degismecikapno = kapsirano;
        string degismeciorgkidem = kidem;

        string izinde = "0";
        string izinbasla = ilkkayitzamani;
        string izinbit = ilkkayitzamani;
        string gorevde = "0";

        string gemiadi = "";
        int imono = 0;
        string bayrak = "";
        string grt = "";
        string tip = "";
        string binisyeri = "";
        string binisrihtim = "";
        string inisyeri = "";
        string inisrihtim = "";
        string istasyoncikis = "";
        string Pob = "";
        string Poff = "";
        string istasyongelis = AnaKlas.TarihSaatYaziYapDMYhm(AnaKlas.varbitisonceki());

        TBPADDkapadi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDkapsoyadi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDeposta.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDsifre.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDtel1.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDtel2.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDdogumtar.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDaddress.BorderColor = System.Drawing.Color.FromName("gray");
        TBPADDdogumyer.BorderColor = System.Drawing.Color.FromName("gray");


        if (kapadi == "" || kapadi == null)
        {
            kapadi ="";
            TBPADDkapadi.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }
        if (kapsoyadi == "" || kapsoyadi == null)
        {
            kapsoyadi = "";
            TBPADDkapsoyadi.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }
        if (eposta == "" || eposta == null)
        {
            eposta = "";
            TBPADDeposta.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }
        if (sifre == "" || sifre == null)
        {
            sifre = "";
            TBPADDsifre.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }
        if (tel1 == "" || tel1 == null || tel1 == "____-___-__-__")
        {
            tel1 = "";
            TBPADDtel1.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }
        if (tel2 == "" || tel2 == null || tel2 == "____-___-__-__")
        {
            tel2 = "";
            TBPADDtel2.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }

        if (dogumtar == "" || dogumtar == null || dogumtar == "__.__.____")
        {
            dogumtar = "";
            TBPADDdogumtar.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }
        if (dogumyer == "")
        {
            dogumyer = "";
            TBPADDdogumyer.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }
        if (address == "" || address == null)
        {
            address = "";
            TBPADDaddress.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderpilotadd.Show();
        }

       

        if (kapadi != "" && kapsoyadi != "" && eposta != "" && sifre != "" && tel1 != "" && tel2 != "" && dogumtar != "" && address != "" && dogumyer != "" )
        {   
                                              
                bool sonuc = addpilot(kapadi, kapsoyadi, eposta, sifre, tel1, tel2, emekli, dogumtar, dogumyer, address, yetki, kapsirano, isegiristarihi, kidemliolmatarihi, emeklitarihi, kidem, varid, girisistasyon, cikisistasyon, ilkkayitzamani, postasmsal, degismeciadi, degismecisoyadi, degismecikapno, degismeciorgkidem, gemiadi, imono, bayrak, grt, tip, binisyeri, binisrihtim, inisyeri, inisrihtim, istasyoncikis, Pob, Poff, istasyongelis, durum, varno, respist, izinde, izinbasla, izinbit, gorevde);
                if (sonuc)
                {
                    GridView1.EditIndex = -1;
                    GridView1.ShowFooter = false;
                    databagla();
                }

        }
        

    }
      private bool addpilot(string kapadi, string kapsoyadi, string eposta, string sifre, string tel1, string tel2, string emekli, string dogumtar, string dogumyer, string address, string yetki,  int kapsirano, string isegiristarihi, string kidemliolmatarihi, string emeklitarihi, string kidem, string varid, string girisistasyon, string cikisistasyon, string ilkkayitzamani, string postasmsal, string degismeciadi, string degismecisoyadi, int degismecikapno, string degismeciorgkidem, string gemiadi, int imono, string bayrak, string grt, string tip, string binisyeri, string binisrihtim, string inisyeri, string inisrihtim, string istasyoncikis, string Pob, string Poff, string istasyongelis, string durum, string varno, string respist,string izinde,string izinbasla,string izinbit,string gorevde)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("insert into pilotlar (kapadi, kapsoyadi, eposta, sifre, tel1, tel2, emekli, dogumtar, dogumyer, address, yetki, kapsirano, isegiristarihi, kidemliolmatarihi, emeklitarihi, kidem, varid, girisistasyon, cikisistasyon, ilkkayitzamani, postasmsal, degismeciadi, degismecisoyadi, degismecikapno, degismeciorgkidem, gemiadi, imono, bayrak, grt, tip, binisyeri, binisrihtim, inisyeri, inisrihtim, istasyoncikis, Pob, Poff, istasyongelis, durum, varno, respist, izinde, izinbasla, izinbit, gorevde) values(@kapadi, @kapsoyadi, @eposta, @sifre, @tel1, @tel2, @emekli,  @dogumtar, @dogumyer, @address,  @yetki, @kapsirano, @isegiristarihi, @kidemliolmatarihi, @emeklitarihi, @kidem,  @varid, @girisistasyon, @cikisistasyon, @ilkkayitzamani, @postasmsal, @degismeciadi, @degismecisoyadi, @degismecikapno, @degismeciorgkidem, @gemiadi, @imono, @bayrak, @grt, @tip, @binisyeri, @binisrihtim, @inisyeri, @inisrihtim, @istasyoncikis, @Pob, @Poff, @istasyongelis, @durum, @varno, @respist, @izinde, @izinbasla, @izinbit, @gorevde)", baglanti);
        cmd.Parameters.AddWithValue("kapadi", kapadi);
        cmd.Parameters.AddWithValue("kapsoyadi", kapsoyadi);
        cmd.Parameters.AddWithValue("eposta", eposta);
        cmd.Parameters.AddWithValue("sifre", sifre);
        cmd.Parameters.AddWithValue("tel1", tel1);
        cmd.Parameters.AddWithValue("tel2", tel2);
        cmd.Parameters.AddWithValue("dogumtar", dogumtar);
        cmd.Parameters.AddWithValue("dogumyer", dogumyer);
        cmd.Parameters.AddWithValue("address", address);
        cmd.Parameters.AddWithValue("yetki", yetki);
        cmd.Parameters.AddWithValue("emekli", emekli);
        cmd.Parameters.AddWithValue("kapsirano", kapsirano);
        cmd.Parameters.AddWithValue("isegiristarihi", isegiristarihi);
        cmd.Parameters.AddWithValue("kidemliolmatarihi", kidemliolmatarihi);
        cmd.Parameters.AddWithValue("emeklitarihi", emeklitarihi);
        cmd.Parameters.AddWithValue("kidem", kidem);
        cmd.Parameters.AddWithValue("varid", varid);
        cmd.Parameters.AddWithValue("girisistasyon", girisistasyon);
        cmd.Parameters.AddWithValue("cikisistasyon", cikisistasyon);
        cmd.Parameters.AddWithValue("ilkkayitzamani", ilkkayitzamani);
       cmd.Parameters.AddWithValue("varno", varno);
        cmd.Parameters.AddWithValue("durum", durum);
        cmd.Parameters.AddWithValue("respist", respist);

        cmd.Parameters.AddWithValue("postasmsal", postasmsal);
        cmd.Parameters.AddWithValue("degismeciadi", degismeciadi);
        cmd.Parameters.AddWithValue("degismecisoyadi", degismecisoyadi);
        cmd.Parameters.AddWithValue("degismecikapno", degismecikapno);
        cmd.Parameters.AddWithValue("degismeciorgkidem", degismeciorgkidem);
        cmd.Parameters.AddWithValue("gemiadi", gemiadi);
        cmd.Parameters.AddWithValue("imono", imono);
        cmd.Parameters.AddWithValue("bayrak", bayrak);
        cmd.Parameters.AddWithValue("grt", grt);
        cmd.Parameters.AddWithValue("tip", tip);
        cmd.Parameters.AddWithValue("binisyeri", binisyeri);
        cmd.Parameters.AddWithValue("binisrihtim", binisrihtim);
        cmd.Parameters.AddWithValue("inisyeri", inisyeri);
        cmd.Parameters.AddWithValue("inisrihtim", inisrihtim);
        cmd.Parameters.AddWithValue("istasyoncikis", istasyoncikis);
        cmd.Parameters.AddWithValue("Pob", Pob);
        cmd.Parameters.AddWithValue("Poff", Poff);
        cmd.Parameters.AddWithValue("istasyongelis", istasyongelis);
        cmd.Parameters.AddWithValue("izinde", izinde);
        cmd.Parameters.AddWithValue("izinbasla", izinbasla);
        cmd.Parameters.AddWithValue("izinbit", izinbit);
        cmd.Parameters.AddWithValue("gorevde", gorevde);


        try
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
        }

        catch (SqlException ex)
        {
            string hata = ex.Message;
        }
        finally
        {
            baglanti.Close();
        }
        return sonuc;



    }
    protected void Baddnewpilotcancel_Click(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

        TBPADDkapadi.Text = "";
        TBPADDkapsoyadi.Text = "";
        TBPADDeposta.Text = "";
        TBPADDsifre.Text = "";
        TBPADDtel1.Text = "";
        TBPADDtel2.Text = "";
        TBPADDdogumtar.Text = "";
        TBPADDaddress.Text = "";
    }
    protected void IBPilotEdit_Click(object sender, ImageClickEventArgs e)
    {
        yetkiaciklama.Visible = false;

        DataTable DTvaridler = AnaKlas.GetDataTable("Select distinct varid from pilotlar where kapsirano<1000 order by varid");
        TBPEvarid.Items.Clear(); // ekle
        TBPEvarid.DataTextField = "varid";
        TBPEvarid.DataSource = DTvaridler;
        TBPEvarid.DataBind();

        DataTable DTistasyonlar = AnaKlas.GetDataTable("Select distinct girisistasyon,cikisistasyon from pilotlar where kapsirano<1000 order by girisistasyon");
        TBPEcikisistasyon.Items.Clear(); // ekle
        TBPEcikisistasyon.DataTextField = "cikisistasyon";
        TBPEcikisistasyon.DataSource = DTistasyonlar;
        TBPEcikisistasyon.DataBind();
        TBPEgirisistasyon.Items.Clear(); // ekle
        TBPEgirisistasyon.DataTextField = "girisistasyon";
        TBPEgirisistasyon.DataSource = DTistasyonlar;
        TBPEgirisistasyon.DataBind();

        ImageButton ImageButtonPEditkopya = (ImageButton)sender;
        PilotEid.Text = HttpUtility.HtmlDecode(ImageButtonPEditkopya.CommandArgument).ToString();
        int PilotEidi = Convert.ToInt32(PilotEid.Text);

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("Select * from pilotlar where  kapno =" + PilotEidi, baglanti);

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
             TBPEkapsirano.Text = dr["kapsirano"].ToString();//4
             TBPEkapadi.Text = dr["kapadi"].ToString();//30
             TBPEkapsoyadi.Text = dr["kapsoyadi"].ToString();  //30
             TBPEeposta.Text = dr["eposta"].ToString(); //30
             TBPEsifre.Text = dr["sifre"].ToString(); //30
             TBPEtel1.Text = dr["tel1"].ToString(); //15
             TBPEtel2.Text = dr["tel2"].ToString(); //15
             TBPEisegiristarihi.Text = dr["isegiristarihi"].ToString(); //10
             TBPEkidemliolmatarihi.Text = dr["kidemliolmatarihi"].ToString(); //10
             TBPEemeklitarihi.Text = dr["emeklitarihi"].ToString(); //10
             TBPEdogumtar.Text = dr["dogumtar"].ToString(); //1
             TBPEaddress.Text = dr["address"].ToString(); //1
                TBEkapsirano.Text = dr["kapsirano"].ToString();
                TBEdegismecikapno.Text = dr["degismecikapno"].ToString();
                TBEdegismeciadi.Text = dr["degismeciadi"].ToString();
                TBEdegismecisoyadi.Text = dr["degismecisoyadi"].ToString();
                TBEvarno.Text = dr["varno"].ToString();
                TBEistasyongelis.Text = dr["istasyongelis"].ToString();

                TBPEyetki.SelectedItem.Selected = false;
                TBPEdogumyer.SelectedItem.Selected = false;
                TBPEemekli.SelectedItem.Selected = false;
                DDLpostasmsal.SelectedItem.Selected = false;
                DDLdegismeciorgkidem.SelectedItem.Selected = false;
                DDLrespist.SelectedItem.Selected = false;
                DDLdurum.SelectedItem.Selected = false;
                TBPEkidem.SelectedItem.Selected = false;

                TBPEkidem.Items.FindByText(dr["kidem"].ToString()).Selected = true;
                TBPEyetki.Items.FindByValue(dr["yetki"].ToString()).Selected = true;
                TBPEdogumyer.Items.FindByText(dr["dogumyer"].ToString()).Selected = true;
                TBPEvarid.Items.FindByText(dr["varid"].ToString()).Selected = true;
                TBPEgirisistasyon.Items.FindByText(dr["girisistasyon"].ToString()).Selected = true;
                TBPEcikisistasyon.Items.FindByText(dr["cikisistasyon"].ToString()).Selected = true;
                TBPEemekli.Items.FindByText(dr["emekli"].ToString()).Selected = true;

                DDLpostasmsal.Items.FindByText(dr["postasmsal"].ToString()).Selected = true; 
                DDLdegismeciorgkidem.Items.FindByText(dr["degismeciorgkidem"].ToString()).Selected = true;
                DDLrespist.Items.FindByText(dr["respist"].ToString()).Selected = true;
                DDLdurum.Items.FindByText(dr["durum"].ToString()).Selected = true;


                //ListItem c1 = TBPEkidem.Items.FindByText(dr["kidem"].ToString());
                //if (c1 != null) { TBPEkidem.Items.FindByText(dr["kidem"].ToString()).Selected = true; }
                //   ListItem c2 = TBPEyetki.Items.FindByText(dr["yetki"].ToString());
                //if (c2 != null) { TBPEyetki.Items.FindByText(dr["yetki"].ToString()).Selected = true;}
                //   ListItem c3 =  TBPEdogumyer.Items.FindByText(dr["dogumyer"].ToString());
                //if (c3 != null) { TBPEdogumyer.Items.FindByText(dr["dogumyer"].ToString()).Selected = true;}
                //   ListItem c4 = TBPEvarid.Items.FindByText(dr["varid"].ToString());
                //if (c4 != null) { TBPEvarid.Items.FindByText(dr["varid"].ToString()).Selected = true;}

                //ListItem c5 =  TBPEgirisistasyon.Items.FindByText(dr["girisistasyon"].ToString());
                //if (c5 != null) { TBPEgirisistasyon.Items.FindByText(dr["girisistasyon"].ToString()).Selected = true;}

                //ListItem c6 = TBPEcikisistasyon.Items.FindByText(dr["cikisistasyon"].ToString());
                //if (c6 != null) { TBPEcikisistasyon.Items.FindByText(dr["cikisistasyon"].ToString()).Selected = true;}

                //ListItem c7 = TBPEemekli.Items.FindByText(dr["emekli"].ToString());
                //if (c7 != null) {TBPEemekli.Items.FindByText(dr["emekli"].ToString()).Selected = true;}





            }
        }
        if (TBPEemekli.SelectedItem.Text == "Yes")
        {
            TBPEemeklitarihi.Visible = true;
        }
        else
        {
            TBPEemeklitarihi.Visible = false;
        }

        dr.Close();

        baglanti.Close();

        if (Session["kapno"].ToString() == PilotEidi.ToString())
        {
            TBPEyetki.Enabled = false;
            ButtonPilotEDT.Visible = false;
            LBPEditmode.Visible = true;
        }

        LBPEditmode.CommandArgument = PilotEidi.ToString();

        this.ModalPopupExtenderPilotEdit.Show();
    }
    protected void ButtonPilotEDT_Click(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

        string hata = "0";
        string kapadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBPEkapadi.Text.ToString().Trim().ToLower())); //30
        string kapsoyadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBPEkapsoyadi.Text.ToString().Trim().ToLower()));  //30
        string eposta = TBPEeposta.Text.ToString().Trim().ToLower(); //30
        string sifre = TBPEsifre.Text; //30
        string tel1 = TBPEtel1.Text; //15
        string tel2 = TBPEtel2.Text; //15
        string isegiristarihi = TBPEisegiristarihi.Text; //10
        string kidemliolmatarihi = TBPEkidemliolmatarihi.Text; //10
        string emeklitarihi = TBPEemeklitarihi.Text; //10
        string dogumtar = TBPEdogumtar.Text; //1
        string address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBPEaddress.Text.ToString().Trim().ToLower()));
        string emekli = TBPEemekli.SelectedItem.Text; //1
        string yetki = TBPEyetki.SelectedItem.Value; //1
        string kidem = TBPEkidem.SelectedItem.Text; //1
        string dogumyer = TBPEdogumyer.SelectedItem.Text; //
        string varid = TBPEvarid.SelectedItem.Text; //1
        string girisistasyon = TBPEgirisistasyon.SelectedItem.Text; //1
        string cikisistasyon = TBPEcikisistasyon.SelectedItem.Text; //1

        string kapsirano = TBEkapsirano.Text;
        string postasmsal = DDLpostasmsal.SelectedItem.Text;
        string degismecikapno= TBEdegismecikapno.Text;
        string degismeciadi= TBEdegismeciadi.Text;
        string degismecisoyadi = TBEdegismecisoyadi.Text;
        string degismeciorgkidem = DDLdegismeciorgkidem.SelectedItem.Text;
        string varno= TBEvarno.Text;
        string respist= DDLrespist.SelectedItem.Text;
        string durum= DDLdurum.SelectedItem.Text;
        string istasyongelis = TBEistasyongelis.Text;


        TBPEkapadi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEkapsoyadi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEeposta.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEsifre.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEtel1.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEtel2.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEkapsirano.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEisegiristarihi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEkidemliolmatarihi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEemeklitarihi.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEdogumtar.BorderColor = System.Drawing.Color.FromName("gray");
        TBPEaddress.BorderColor = System.Drawing.Color.FromName("gray");
        TBEkapsirano.BorderColor = System.Drawing.Color.FromName("gray");
        DDLpostasmsal.BorderColor = System.Drawing.Color.FromName("gray");
        TBEdegismecikapno.BorderColor = System.Drawing.Color.FromName("gray");
        TBEdegismeciadi.BorderColor = System.Drawing.Color.FromName("gray");
        TBEdegismecisoyadi.BorderColor = System.Drawing.Color.FromName("gray");
        DDLdegismeciorgkidem.BorderColor = System.Drawing.Color.FromName("gray");
        TBEvarno.BorderColor = System.Drawing.Color.FromName("gray");
        DDLrespist.BorderColor = System.Drawing.Color.FromName("gray");
        DDLdurum.BorderColor = System.Drawing.Color.FromName("gray");
        TBEistasyongelis.BorderColor = System.Drawing.Color.FromName("gray");

        if (kapadi == "" || kapadi == null)
        {
            kapadi = "";
            TBPEkapadi.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (kapsoyadi == "" || kapsoyadi == null)
        {
            kapsoyadi = "";
            TBPEkapsoyadi.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (eposta == "" || eposta == null)
        {
            eposta = "";
            TBPEeposta.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (sifre == "" || sifre == null)
        {
            sifre = "";
            TBPEsifre.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (tel1 == "" || tel1 == null || tel1 == "____-___-__-__")
        {
            tel1 = "";
            TBPEtel1.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (tel2 == "" || tel2 == null || tel2 == "____-___-__-__")
        {
            tel2 = "";
            TBPEtel2.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }

        if (dogumtar == "" || dogumtar == null || dogumtar == "__.__.____")
        {
            dogumtar = "";
            TBPEdogumtar.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }

        if (address == "" || address == null)
        {
            address = "";
            TBPEaddress.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }

        if (isegiristarihi == "" || isegiristarihi == null || isegiristarihi == "__.__.____")
        {
            isegiristarihi = "";
            TBPEisegiristarihi.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (kidemliolmatarihi == "" || kidemliolmatarihi == null || kidemliolmatarihi == "__.__.____")
        {
            kidemliolmatarihi = "";
            TBPEkidemliolmatarihi.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (TBPEemekli.SelectedItem.Text == "Yes")
        {
            if (emeklitarihi == "" || emeklitarihi == null || emeklitarihi == "__.__.____")
            {
                emeklitarihi = "";
                TBPEemeklitarihi.BorderColor = System.Drawing.Color.FromName("red");
                hata = "1";
                this.ModalPopupExtenderPilotEdit.Show();
            }
        }

        if (kapsirano == "" || kapsirano == null)
        {
            kapsirano = "";
            TBEkapsirano.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (degismecikapno == "" || degismecikapno == null)
        {
            degismecikapno = "";
            TBEdegismecikapno.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (degismeciadi == "" || degismeciadi == null)
        {
            degismeciadi = kapadi;
            TBEdegismeciadi.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (degismecisoyadi == "" || degismecisoyadi == null)
        {
            degismecisoyadi = kapsoyadi;
            TBEdegismecisoyadi.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }
        if (varno == "" || varno == null)
        {
            varno = "";
            TBEvarno.BorderColor = System.Drawing.Color.FromName("red");
            hata = "1";
            this.ModalPopupExtenderPilotEdit.Show();
        }


        if (TBEistasyongelis.Text == "" || TBEistasyongelis.Text == null || TBEistasyongelis.Text == "__.__.____ __:__")
        {
            TBEistasyongelis.BorderColor = System.Drawing.Color.Red;
            hata = "1";
            TBEistasyongelis.BorderWidth = 1;
            this.ModalPopupExtenderPilotEdit.Show();
        }
        else if (AnaKlas.IsDate2(TBEistasyongelis.Text) != true)
        {
            TBEistasyongelis.BorderColor = System.Drawing.Color.Red;
            hata = "1";
            TBEistasyongelis.BorderWidth = 1;
            this.ModalPopupExtenderPilotEdit.Show();
        }
        else if (Convert.ToDateTime(TBEistasyongelis.Text) > DateTime.Now)
        {
            TBEistasyongelis.BorderColor = System.Drawing.Color.Red;
            hata = "1";
            TBEistasyongelis.BorderWidth = 1;
            this.ModalPopupExtenderPilotEdit.Show();
        }
        //else if (Convert.ToDateTime(TBEistasyongelis.Text) < DateTime.Now.AddDays(-1))
        //{
        //    TBEistasyongelis.BorderColor = System.Drawing.Color.Red;
        //    hata = "1";
        //    TBEistasyongelis.BorderWidth = 1;
        //    this.ModalPopupExtenderPilotEdit.Show();
        //}

        if (hata == "0" )
        {           
               piloteditsave(kapadi, kapsoyadi, eposta, sifre, tel1, tel2, isegiristarihi, kidemliolmatarihi, emeklitarihi, dogumtar, address, emekli, yetki, kidem, dogumyer, varid, girisistasyon, cikisistasyon, kapsirano,postasmsal,degismecikapno,degismeciadi,degismecisoyadi,degismeciorgkidem,varno,respist,durum,istasyongelis);
           }

    }
      private void piloteditsave(string kapadi, string kapsoyadi, string eposta, string sifre, string tel1, string tel2,  string isegiristarihi, string kidemliolmatarihi, string emeklitarihi, string dogumtar, string address, string emekli, string yetki, string kidem, string dogumyer, string varid, string girisistasyon, string cikisistasyon, string kapsirano, string postasmsal, string degismecikapno, string degismeciadi, string degismecisoyadi, string degismeciorgkidem, string varno, string respist, string durum, string istasyongelis)
    {
        int PilotEidi = Convert.ToInt32(PilotEid.Text);
        string ilkkayitzamani = AnaKlas.TarihSaatYaziYapDMYhm(DateTime.Now);

        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmd = new SqlCommand("update pilotlar set kapadi=@kapadi, kapsoyadi=@kapsoyadi, eposta=@eposta, sifre=@sifre, tel1=@tel1 ,  tel2=@tel2 , dogumtar=@dogumtar, dogumyer=@dogumyer,  address=@address, yetki=@yetki, emekli=@emekli , emeklitarihi=@emeklitarihi , isegiristarihi=@isegiristarihi ,  kidemliolmatarihi=@kidemliolmatarihi ,    kidem=@kidem , varid=@varid , girisistasyon=@girisistasyon ,  cikisistasyon=@cikisistasyon, ilkkayitzamani=@ilkkayitzamani, kapsirano=@kapsirano,postasmsal=@postasmsal,degismecikapno=@degismecikapno,degismeciadi=@degismeciadi,degismecisoyadi=@degismecisoyadi,degismeciorgkidem=@degismeciorgkidem,varno=@varno,respist=@respist,durum=@durum,istasyongelis=@istasyongelis    where kapno=" + PilotEidi, baglanti);
        cmd.Parameters.AddWithValue("kapadi", kapadi);
        cmd.Parameters.AddWithValue("kapsoyadi", kapsoyadi);
        cmd.Parameters.AddWithValue("eposta", eposta);
        cmd.Parameters.AddWithValue("sifre", sifre);
        cmd.Parameters.AddWithValue("tel1", tel1);
        cmd.Parameters.AddWithValue("tel2", tel2);
        cmd.Parameters.AddWithValue("yetki", yetki);
        cmd.Parameters.AddWithValue("isegiristarihi", isegiristarihi);
        cmd.Parameters.AddWithValue("kidemliolmatarihi", kidemliolmatarihi);
        cmd.Parameters.AddWithValue("emeklitarihi", emeklitarihi);
        cmd.Parameters.AddWithValue("emekli", emekli);
        cmd.Parameters.AddWithValue("kidem", kidem);
        cmd.Parameters.AddWithValue("dogumtar", dogumtar);
        cmd.Parameters.AddWithValue("dogumyer", dogumyer);
        cmd.Parameters.AddWithValue("address", address);
        cmd.Parameters.AddWithValue("varid", varid);
        cmd.Parameters.AddWithValue("girisistasyon", girisistasyon);
        cmd.Parameters.AddWithValue("cikisistasyon", cikisistasyon);
        cmd.Parameters.AddWithValue("ilkkayitzamani", ilkkayitzamani);
        cmd.Parameters.AddWithValue("kapsirano", kapsirano);
        cmd.Parameters.AddWithValue("postasmsal", postasmsal);
        cmd.Parameters.AddWithValue("degismecikapno", degismecikapno);
        cmd.Parameters.AddWithValue("degismeciadi", degismeciadi);
        cmd.Parameters.AddWithValue("degismecisoyadi", degismecisoyadi);
        cmd.Parameters.AddWithValue("degismeciorgkidem", degismeciorgkidem);
        cmd.Parameters.AddWithValue("varno", varno);
        cmd.Parameters.AddWithValue("respist", respist);
        cmd.Parameters.AddWithValue("durum", durum);
        cmd.Parameters.AddWithValue("istasyongelis", istasyongelis);

        cmd.ExecuteNonQuery();
        baglanti.Close();
        PilotEid.Text = "";
        databagla();
    }
      protected void LBguvcik_Click(object sender, EventArgs e)
      {
        if (Session["kapno"] == null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
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
    protected void LBmainpage_Click(object sender, EventArgs e)
    {
        Response.Redirect("../main.aspx");
    }
    protected void TBPEemekli_SelectedIndexChanged(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

        if (TBPEemekli.SelectedItem.Text == "Yes")
        {
            TBPEemeklitarihi.Visible = true;
        }
        if (TBPEemekli.SelectedItem.Text == "No")
        {
            TBPEemeklitarihi.Visible = false;
        }
        TBPEkapadi.Enabled = true;
        TBPEkapsoyadi.Enabled = true;
        TBPEeposta.Enabled = true;
        TBPEsifre.Visible = true;
        LitPasstext.Visible = true;
        TBPEtel1.Enabled = true;
        TBPEtel2.Enabled = true;
        TBPEaddress.Enabled = true;
        TBPEdogumtar.Enabled = true;
        TBPEdogumyer.Enabled = true;
        TBPEisegiristarihi.Enabled = true;
        TBPEkidemliolmatarihi.Enabled = true;
        TBPEemekli.Enabled = true;
        TBPEemeklitarihi.Enabled = true;
        TBPEkidem.Enabled = true;
        TBPEvarid.Enabled = true;
        TBPEgirisistasyon.Enabled = true;
        TBPEcikisistasyon.Enabled = true;

        ButtonPilotEDT.Visible = true;
        LBPEditmode.Visible = false;

        TBEkapsirano.Enabled = true;
        DDLpostasmsal.Enabled = true;
        TBEdegismecikapno.Enabled = true;
        TBEdegismeciadi.Enabled = true;
        TBEdegismecisoyadi.Enabled = true;
        DDLdegismeciorgkidem.Enabled = true;
        TBEvarno.Enabled = true;
        DDLrespist.Enabled = true;
        DDLdurum.Enabled = true;
        TBEistasyongelis.Enabled = true;

        if (Session["kapno"].ToString() != LBPEditmode.CommandArgument.ToString())
        {
            TBPEyetki.Enabled = true;
        }

        this.ModalPopupExtenderPilotEdit.Show();
    }

    protected void TBPADDyetki_SelectedIndexChanged(object sender, EventArgs e)
    {
        yetkiaciklama.Visible = false;

        SqlConnection baglanti = AnaKlas.baglan();

        string secim = TBPADDyetki.SelectedItem.Value.ToString();
        if (secim == "0")//pilot
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='0' and kapsirano<1000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }

        else if (secim == "1")//darica gözcü
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='1' and kapsirano>1000 and kapsirano<2000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }
        else if (secim == "2")//yarımca gözcü
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='2' and kapsirano>1000 and kapsirano<2000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }
        else if (secim == "3")//şirket müdür
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='3' and kapsirano>2000 and kapsirano<3000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }
        else if (secim == "4")//şirket çalışan
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='4' and kapsirano>3000 and kapsirano<4000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }
        else if (secim == "5")//dış firma
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='5' and kapsirano>4000 and kapsirano<5000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }
        else if (secim == "6")//stajer pilot
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='0' and kapsirano<1000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }
        else if (secim == "7")//stajer gözcü
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='1' and kapsirano>1000 and kapsirano<2000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }
        else if (secim == "8")//pilot botu kpt
        {
            SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where yetki='8' and kapsirano>3000 and kapsirano<4000", baglanti);
            int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            sonkapsirano = sonkapsirano + 1;
            TBPADDkapsirano.Text = sonkapsirano.ToString();
        }

        baglanti.Close();


        this.ModalPopupExtenderpilotadd.Show();

    }
}