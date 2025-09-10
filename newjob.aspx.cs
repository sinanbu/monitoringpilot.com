using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;




public partial class newjob : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["kapno"] == null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else //if (cmdlogofbak.ExecuteScalar() != null)
        {

            if (!Page.IsPostBack)
            {
                this.Page.Form.DefaultButton = this.LBfindves.UniqueID;

                if (Session["yetki"].ToString() == "9")
                { LBonline.Enabled = true; }
                else { LBonline.Enabled = false; }

                SqlConnection baglanti = AnaKlas.baglan();
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

                LBonline.Text = kapadisoyadi;

                LitYaz(baglanti);

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();

                NewShipListc();

                SqlCommand cmd = new SqlCommand("Delete From log_newjob Where id Not In (select TOP 10000 id FROM log_newjob where id > 1 order by id desc)", baglanti);
                cmd.ExecuteNonQuery();

                baglanti.Close();

            }


        }

      
    }

    private void LitYaz(SqlConnection baglanti)
    {
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
    }

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
    }


    
    private void NewShipListc()
    {
        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            var veri2 = from b2 in isliste.isliste.ToList() select b2;
                foreach (var c in veri2)
                {
                    c.orderbay = Convert.ToDateTime(c.kayitzamani);
                }
            DLNewjob.DataSource = veri2.OrderByDescending(x => x.orderbay).Take(20);
            DLNewjob.DataBind();
        }
    }


    protected void DLNewjob_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {   //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Convert.ToInt32(Session["yetki"]) < 1 || Convert.ToInt32(Session["yetki"]) > 5)
            {
                e.Item.FindControl("ImageButtonJobEdit6").Visible = (false);
            }

            Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5ckop.Text = "Ordino-Talep Yok";
                Lbl5ckop.Style.Add("font-style", "normal");
                Lbl5ckop.Style.Add("color", "#ee1111");

            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }

            Label Lbl15dkop = (Label)e.Item.FindControl("Lbl15d");
            if (string.IsNullOrEmpty(Lbl15dkop.Text) == true)
            {
                demirkisakop.Text = "PRY";
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warngemitd");
                td.Attributes.Add("style", "background-color:#ffcccc");
            }

            Button Btngemiupkop = (Button)e.Item.FindControl("Btngemiup");
            if (Btngemiupkop.CommandName == "8" || Btngemiupkop.Text.ToLower()=="takviye")
                {
                Btngemiupkop.Visible = false;
                e.Item.FindControl("ImageButtonissilc").Visible = false;
            }
            else
            {
                e.Item.FindControl("Labelgemiup").Visible = false;
                e.Item.FindControl("ImageButtonissilc").Visible = true;
            }
        }

    }


    protected void ButtonANJshowpopup_Click(object sender, EventArgs e)
    {
        TextBoxaddimo.Text = "";
        TextBoxisadd1.Text = "";
        TextBoxisadd4.Text = TarihSaatYaziYapDMYhm(DateTime.Now.Date.AddDays(1).AddMinutes(-1));
        TextBoxisadd7.Text = "0";
        TextBoxisadd8.Text = "0";
        TextBoxisadd10.Text = "";
        TextBoxisadd11.Text = "";
        TextBoxisadd13.Text = "";
        TextBoxisadd14.Text = "";
        TextBoxisadd16.Text = "";
        TextBoxisadd17.Text = "";
        TBpratikano.Text = "";
        TextBoxlcbno.Text = "";
        TextBoxlcbdate.Text = "";

        TextBoxaddimo.BackColor = System.Drawing.Color.White;
        TextBoxisadd1.BackColor = System.Drawing.Color.White;
        DDLdestport.BackColor = System.Drawing.Color.White;
        DDLdepberth.BackColor = System.Drawing.Color.White;
        DDLdestberth.BackColor = System.Drawing.Color.White;
        TextBoxaddimo.BorderColor = System.Drawing.Color.Gray;
        TextBoxaddimo.BorderWidth = 1;
        TextBoxisadd1.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd1.BorderWidth = 1;
        TextBoxisadd4.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd4.BorderWidth = 1;
        TextBoxisadd7.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd7.BorderWidth = 1;
        TextBoxisadd8.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd8.BorderWidth = 1;
        TextBoxisadd10.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd10.BorderWidth = 1;
        TextBoxisadd11.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd11.BorderWidth = 1;
        TextBoxisadd13.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd13.BorderWidth = 1;
        TextBoxisadd14.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd14.BorderWidth = 1;
        TextBoxisadd16.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd16.BorderWidth = 1;
        TextBoxisadd17.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd17.BorderWidth = 1;
        TBpratikano.BorderColor = System.Drawing.Color.Gray;
        TBpratikano.BorderWidth = 1;
        TextBoxlcbno.BorderColor = System.Drawing.Color.Gray;
        TextBoxlcbno.BorderWidth = 1;
        TextBoxlcbdest.BorderColor = System.Drawing.Color.Gray;
        TextBoxlcbdest.BorderWidth = 1;        
        TextBoxlcbdate.BorderColor = System.Drawing.Color.Gray;
        TextBoxlcbdate.BorderWidth = 1;

        DDLanchorplace.BorderColor = System.Drawing.Color.Gray;
        DDLdepport.BorderColor = System.Drawing.Color.Gray;
        DDLdestport.BorderColor = System.Drawing.Color.Gray;
        DDLflag.BorderColor = System.Drawing.Color.Gray;
        DDLtip.BorderColor = System.Drawing.Color.Gray;
        DDL12.BorderColor = System.Drawing.Color.Gray;
        DDLdepberth.BorderColor = System.Drawing.Color.Gray;
        DDLdestberth.BorderColor = System.Drawing.Color.Gray;
        DDLpratika.BorderColor = System.Drawing.Color.Gray;
        DDLanchorplace.BorderWidth = 1;
        DDLdepport.BorderWidth = 1;
        DDLdestport.BorderWidth = 1;
        DDLflag.BorderWidth = 1;
        DDLtip.BorderWidth = 1;
        DDL12.BorderWidth = 1;
        DDLdepberth.BorderWidth = 1;
        DDLdestberth.BorderWidth = 1;
        DDLpratika.BorderWidth = 1;

        DDLdepport.Items.Clear();
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLlimanal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLdepport.DataValueField = "limanno";
        DDLdepport.DataTextField = "limanadi";
        DDLdepport.DataSource = ds;
        DDLdepport.DataBind();
        DDLdepport.Items.FindByText("Yelkenkaya").Selected = true;


        DDLdepberth.Items.Clear();
        DDLdepberth.Items.Insert(0, new ListItem("0", String.Empty));
        DDLdepberth.SelectedItem.Text = "0";
        if (DDLdepberth.SelectedItem.Text == "0") { DDLdepberth.Visible = false; }

        DDLdestport.Items.Clear(); // ekle
        DDLdestport.DataValueField = "limanno";
        DDLdestport.DataTextField = "limanadi";
        DDLdestport.DataSource = ds;
        DDLdestport.DataBind();
        DDLdestport.Items.Insert(0, new ListItem("Select Port?", String.Empty));
        DDLdestport.SelectedIndex = 0;

        DDLdestberth.Items.Clear();
        DDLdestberth.Items.Insert(0, new ListItem("Select", String.Empty));
        DDLdestberth.SelectedIndex = 0;

        SqlCommand cmdDDLdemir = new SqlCommand("SP_DDLdemiral", baglanti);
        cmdDDLdemir.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterdem = new SqlDataAdapter();
        adapterdem.SelectCommand = cmdDDLdemir;
        DataSet dsdem = new DataSet();
        adapterdem.Fill(dsdem, "limanlar");
        DDLanchorplace.Items.Clear(); //ekle 
        DDLanchorplace.DataValueField = "limanno";
        DDLanchorplace.DataTextField = "limanadi";
        DDLanchorplace.DataSource = dsdem;
        DDLanchorplace.DataBind();
        DDLanchorplace.Items.Insert(0, new ListItem("Select Anchorage?", String.Empty));
        DDLanchorplace.SelectedIndex = 0;

        SqlCommand cmdDDLflag = new SqlCommand("SP_DDLflagal", baglanti);
        cmdDDLflag.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterflag = new SqlDataAdapter();
        adapterflag.SelectCommand = cmdDDLflag;
        DataSet dsflag = new DataSet();
        adapterflag.Fill(dsflag, "flaglist");
        DDLflag.Items.Clear();// ekle 
        DDLflag.DataValueField = "id";
        DDLflag.DataTextField = "flag";
        DDLflag.DataSource = dsflag;
        DDLflag.DataBind();
        DDLflag.Items.Insert(0, new ListItem("Select Flag?", String.Empty));
        DDLflag.SelectedIndex = 0;

        SqlCommand cmdDDLtip = new SqlCommand("SP_DDLshiptip", baglanti);
        cmdDDLtip.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adaptertip = new SqlDataAdapter();
        adaptertip.SelectCommand = cmdDDLtip;
        DataSet dstip = new DataSet();
        adaptertip.Fill(dstip, "shiptip");
        DDLtip.Items.Clear();// ekle 
        DDLtip.DataValueField = "id";
        DDLtip.DataTextField = "tip";
        DDLtip.DataSource = dstip;
        DDLtip.DataBind();
        DDLtip.Items.Insert(0, new ListItem("Select Type?", String.Empty));
        DDLtip.SelectedIndex = 0;
        baglanti.Close();


        if (DDLpratika.SelectedItem.Text == "Yes")
        {
            TBpratikano.Visible = true;
            
        }
        else
        {
            TBpratikano.Visible = false;
        }

        this.ModalPopupyeniisekle.Show();
    }
    protected void Yeniisekle_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        TextBoxaddimo.BorderColor = System.Drawing.Color.Gray;
        TextBoxaddimo.BorderWidth = 1;
        TextBoxisadd1.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd1.BorderWidth = 1;
        TextBoxisadd4.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd4.BorderWidth = 1;
        TextBoxisadd7.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd7.BorderWidth = 1;
        TextBoxisadd8.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd8.BorderWidth = 1;
        TextBoxisadd10.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd10.BorderWidth = 1;
        TextBoxisadd11.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd11.BorderWidth = 1;
        TextBoxisadd13.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd13.BorderWidth = 1;
        TextBoxisadd14.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd14.BorderWidth = 1;
        TextBoxisadd16.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd16.BorderWidth = 1;
        TextBoxisadd17.BorderColor = System.Drawing.Color.Gray;
        TextBoxisadd17.BorderWidth = 1;
        TBpratikano.BorderColor = System.Drawing.Color.Gray;
        TBpratikano.BorderWidth = 1;
        TextBoxlcbno.BorderColor = System.Drawing.Color.Gray;
        TextBoxlcbno.BorderWidth = 1;
        TextBoxlcbdest.BorderColor = System.Drawing.Color.Gray;
        TextBoxlcbdest.BorderWidth = 1;
        TextBoxlcbdate.BorderColor = System.Drawing.Color.Gray;
        TextBoxlcbdate.BorderWidth = 1;
        DDLanchorplace.BorderColor = System.Drawing.Color.Gray;
        DDLflag.BorderColor = System.Drawing.Color.Gray;
        DDLtip.BorderColor = System.Drawing.Color.Gray;
        DDL12.BorderColor = System.Drawing.Color.Gray;
        DDLdepport.BorderColor = System.Drawing.Color.Gray;
        DDLdestport.BorderColor = System.Drawing.Color.Gray;
        DDLdepberth.BorderColor = System.Drawing.Color.Gray;
        DDLdestberth.BorderColor = System.Drawing.Color.Gray;
        DDLpratika.BorderColor = System.Drawing.Color.Gray;
        DDLflag.BorderWidth = 1;
        DDLtip.BorderWidth = 1;
        DDL12.BorderWidth = 1;
        DDLanchorplace.BorderWidth = 1;
        DDLdepport.BorderWidth = 1;
        DDLdestport.BorderWidth = 1;
        DDLdepberth.BorderWidth = 1;
        DDLdestberth.BorderWidth = 1;
        DDLpratika.BorderWidth = 1;

        string imono = TextBoxaddimo.Text;

        string gemiadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxisadd1.Text.ToString().Trim().ToLower()));
        string kalkislimani = HttpUtility.HtmlDecode(DDLdepport.SelectedItem.Text);
        string kalkisrihtimi = HttpUtility.HtmlDecode(DDLdepberth.SelectedItem.Text);
        string yanasmalimani = HttpUtility.HtmlDecode(DDLdestport.SelectedItem.Text);
        string yanasmarihtimi = HttpUtility.HtmlDecode(DDLdestberth.SelectedItem.Text);
        string demiryeri = HttpUtility.HtmlDecode(DDLanchorplace.SelectedItem.Text);
        string eta = TextBoxisadd4.Text;
        string bowt = TextBoxisadd7.Text;
        string strnt = TextBoxisadd8.Text;
        string bayrak = HttpUtility.HtmlDecode(DDLflag.SelectedItem.Text);
        string grt = TextBoxisadd10.Text;
        string tip = HttpUtility.HtmlDecode(DDLtip.SelectedItem.Text);
        string loa = TextBoxisadd11.Text;
        string tehlikeliyuk = HttpUtility.HtmlDecode(DDL12.SelectedItem.Text);
        string acente = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxisadd13.Text.ToString().Trim().ToLower()));
        string fatura = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxisadd14.Text.ToString().Trim().ToLower()));
        string notlar = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxisadd16.Text.ToString().Trim().ToLower()));
        string talepno = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxisadd17.Text.ToString().Trim().ToLower()));
        string kayitzamani = TarihSaatYaziYapDMYhm(DateTime.Now);
        string pratika = HttpUtility.HtmlDecode(DDLpratika.SelectedItem.Text);
        string pratikano = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBpratikano.Text.ToString().Trim().ToLower()));
        string lcbno = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxlcbno.Text.ToString().Trim().ToLower()));
        string lcbdest = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxlcbno.Text.ToString().Trim().ToLower()));
        string lcbdate = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TextBoxlcbdate.Text.ToString().Trim().ToLower()));

        string hata = "yok";

        if(lcbdate.Trim() == "__.__.____ __:__") { lcbdate = ""; }

        if (imono == ""  || imono == "9999999" || imono == "8888888" || imono == null || imono.Length != 7 || imono.Substring(0, 1) == "0")
        {
            hata = "var";
            TextBoxaddimo.BorderColor = System.Drawing.Color.Red;
            TextBoxaddimo.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }

        if (gemiadi == "" || gemiadi == null || gemiadi.ToLower() == "takviye")
        {
            hata = "var";
            TextBoxisadd1.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd1.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }

        if (demiryeri == "Select Anchorage?")
        {
            hata = "var";
            DDLanchorplace.BorderColor = System.Drawing.Color.Red;
            DDLanchorplace.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (kalkislimani == "Select Port?")
        {
            hata = "var";
            DDLdepport.BorderColor = System.Drawing.Color.Red;
            DDLdepport.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (yanasmalimani == "Select Port?")
        {
            hata = "var";
            DDLdestport.BorderColor = System.Drawing.Color.Red;
            DDLdestport.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }

        if (yanasmarihtimi == "Select")
        {
            hata = "var";
            DDLdestberth.BorderColor = System.Drawing.Color.Red;
            DDLdestberth.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }

        if (kalkisrihtimi == "Select")
        {
            hata = "var";
            DDLdepberth.BorderColor = System.Drawing.Color.Red;
            DDLdepberth.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }

        if (bowt == "" || bowt == null)
        {
            hata = "var";
            TextBoxisadd7.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd7.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (strnt == "" || strnt == null)
        {
            hata = "var";
            TextBoxisadd8.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd8.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (bayrak == "Select Flag?")
        {
            hata = "var";
            DDLflag.BorderColor = System.Drawing.Color.Red;
            DDLflag.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (grt == "" || grt == null)
        {
            hata = "var";
            TextBoxisadd10.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd10.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (tip == "Select Type?")
        {
            hata = "var";
            DDLtip.BorderColor = System.Drawing.Color.Red;
            DDLtip.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (loa == "" || loa == null)
        {
            hata = "var";
            TextBoxisadd11.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd11.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }

        if (acente == "" || acente == null)
        {
            hata = "var";
            TextBoxisadd13.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd13.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (fatura == "" || fatura == null)
        {
            hata = "var";
            TextBoxisadd14.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd14.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (notlar == "" || notlar == null)
        {
            hata = "var";
            TextBoxisadd16.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd16.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        if (talepno == "" || talepno == null)
        {
            hata = "var";
            TextBoxisadd17.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd17.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }


        if (eta == "" || eta == null || eta == "__.__.____ __:__")
        {
            hata = "var";
            TextBoxisadd4.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd4.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        else if (IsDate2(eta) != true)
        {
            hata = "var";
            TextBoxisadd4.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd4.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }
        else if (Convert.ToDateTime(eta) < DateTime.Now)
        {
            hata = "var";
            TextBoxisadd4.BorderColor = System.Drawing.Color.Red;
            TextBoxisadd4.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
        }


        if (TextBoxlcbno.Text.Trim() != "" || TextBoxlcbdest.Text.Trim() != "" || lcbdate.Trim() != "")
        {
            if (TextBoxlcbno.Text == "")
            {
                hata = "var";
                TextBoxlcbno.BorderColor = System.Drawing.Color.Red;
                TextBoxlcbno.BorderWidth = 1;
                this.ModalPopupyeniisekle.Show();
            }
            else if (TextBoxlcbdest.Text == "")
            {
                hata = "var";
                TextBoxlcbdest.BorderColor = System.Drawing.Color.Red;
                TextBoxlcbdest.BorderWidth = 1;
                this.ModalPopupyeniisekle.Show();
            }

            else if (lcbdate == "" || lcbdate == null || lcbdate == "__.__.____ __:__")
            {
                hata = "var";
                TextBoxlcbdate.BorderColor = System.Drawing.Color.Red;
                TextBoxlcbdate.BorderWidth = 1;
                this.ModalPopupyeniisekle.Show();
            }
            else if (IsDate2(lcbdate) != true)
            {
                hata = "var";
                TextBoxlcbdate.BorderColor = System.Drawing.Color.Red;
                TextBoxlcbdate.BorderWidth = 1;
                this.ModalPopupyeniisekle.Show();
            }
            else if (Convert.ToDateTime(lcbdate) < DateTime.Now.AddHours(-6))
            {
                hata = "var";
                TextBoxlcbdate.BorderColor = System.Drawing.Color.Red;
                TextBoxlcbdate.BorderWidth = 1;
                this.ModalPopupyeniisekle.Show();
            }

        }



        if (pratika == "Yes")
        {
            if (pratikano == "" || pratikano == null)
            {
            hata = "var";
                TBpratikano.BorderColor = System.Drawing.Color.Red;
                TBpratikano.BorderWidth = 1;
            this.ModalPopupyeniisekle.Show();
            }

        }



        if (hata == "yok")
        {

                string nedurumda = "4";//gelen gemi
                string nedurumdaopr = "4";

                SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                cmdLimannoal.CommandType = CommandType.StoredProcedure;
                cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
                cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int);
                cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                cmdLimannoal.ExecuteNonQuery();
                int portno = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString());
                cmdLimannoal.Dispose();
                if (portno > 0 && portno < 900) // limanda
                {
                    nedurumda = "2";
                    nedurumdaopr = "2";
                }

                else if (portno > 1000 && portno < 1099) // demirde
                {
                    nedurumda = "1";
                    nedurumdaopr = "1";
                }


            //else if (portno == 998) // çıkışa gitmiş
            //{
            //    nedurumda = "0";
            //    nedurumdaopr = "0";
            //}

            //else if (portno == 999) // to order
            //{                }


            string bilgi = "0";
            string draft = "0";
            bilgi = AnaKlas.tpphesapla(grt, tip, loa, bowt, strnt, tehlikeliyuk, kalkisrihtimi, yanasmarihtimi)[0];
            draft = AnaKlas.tpphesapla(grt, tip, loa, bowt, strnt, tehlikeliyuk, kalkisrihtimi, yanasmarihtimi)[1];



            SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string isimbul = cmdisimbul.ExecuteScalar().ToString();
                cmdisimbul.Dispose();
                SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
				//if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
                cmdsoyisimbul.Dispose();
                string isimkisalt = isimbul.Substring(0, 1) + soyisimbul.Substring(0, 1);

                SqlCommand cmd = new SqlCommand("insert into isliste (imono,gemiadi,demiryeri,kalkislimani,kalkisrihtimi,yanasmalimani,yanasmarihtimi,eta,bowt,strnt,bayrak,grt,tip,loa,draft,bilgi,tehlikeliyuk,acente,fatura,notlar,talepno,pratika,pratikano,lcbno,lcbdest,lcbdate,nedurumda,nedurumdaopr,gizlegoster,kayitzamani,kaydeden) values(@imono,@gemiadi,@demiryeri,@kalkislimani,@kalkisrihtimi,@yanasmalimani,@yanasmarihtimi,@eta,@bowt,@strnt,@bayrak,@grt,@tip,@loa,@draft,@bilgi,@tehlikeliyuk,@acente,@fatura,@notlar,@talepno,@pratika,@pratikano,@lcbno,@lcbdest,@lcbdate,@nedurumda,@nedurumdaopr,@gizlegoster,@kayitzamani,@kaydeden)", baglanti);
                    cmd.Parameters.AddWithValue("imono", imono);
                    cmd.Parameters.AddWithValue("gemiadi", gemiadi);
                    cmd.Parameters.AddWithValue("demiryeri", demiryeri);
                    cmd.Parameters.AddWithValue("kalkislimani", kalkislimani);
                    cmd.Parameters.AddWithValue("kalkisrihtimi", kalkisrihtimi);
                    cmd.Parameters.AddWithValue("yanasmalimani", yanasmalimani);
                    cmd.Parameters.AddWithValue("yanasmarihtimi", yanasmarihtimi);
                    cmd.Parameters.AddWithValue("eta", eta);
                    cmd.Parameters.AddWithValue("bowt", bowt);
                    cmd.Parameters.AddWithValue("strnt", strnt);
                    cmd.Parameters.AddWithValue("bayrak", bayrak);
                    cmd.Parameters.AddWithValue("grt", grt);
                    cmd.Parameters.AddWithValue("tip", tip);
                    cmd.Parameters.AddWithValue("loa", loa);
                    cmd.Parameters.AddWithValue("draft", draft);
                    cmd.Parameters.AddWithValue("bilgi", bilgi);
                    cmd.Parameters.AddWithValue("tehlikeliyuk", tehlikeliyuk);
                    cmd.Parameters.AddWithValue("acente", acente);
                    cmd.Parameters.AddWithValue("fatura", fatura);
                    cmd.Parameters.AddWithValue("notlar", notlar);
                cmd.Parameters.AddWithValue("pratika", pratika);
                cmd.Parameters.AddWithValue("pratikano", pratikano);
            cmd.Parameters.AddWithValue("lcbno", lcbno);
            cmd.Parameters.AddWithValue("lcbdest", lcbdest);
            cmd.Parameters.AddWithValue("lcbdate", lcbdate);
                    cmd.Parameters.AddWithValue("talepno", talepno);
                    cmd.Parameters.AddWithValue("nedurumda", nedurumda);
                    cmd.Parameters.AddWithValue("nedurumdaopr", nedurumdaopr);
                    cmd.Parameters.AddWithValue("gizlegoster", "1");
                    cmd.Parameters.AddWithValue("kayitzamani", kayitzamani);
                    cmd.Parameters.AddWithValue("kaydeden", isimkisalt + " " + DateTime.Now.ToShortDateString().Substring(0, 2) + "|" + DateTime.Now.ToShortTimeString().Substring(0, 2));
                    cmd.ExecuteNonQuery();

            //log_newjob

            SqlCommand cmdlognj = new SqlCommand("SP_log_newjob_ekle", baglanti);
            cmdlognj.CommandType = CommandType.StoredProcedure;
            cmdlognj.Parameters.AddWithValue("imono", imono);
            cmdlognj.Parameters.AddWithValue("gemiadi", gemiadi);
            cmdlognj.Parameters.AddWithValue("demiryeri", demiryeri);
            cmdlognj.Parameters.AddWithValue("kalkislimani", kalkislimani);
            cmdlognj.Parameters.AddWithValue("kalkisrihtimi", kalkisrihtimi);
            cmdlognj.Parameters.AddWithValue("yanasmalimani", yanasmalimani);
            cmdlognj.Parameters.AddWithValue("yanasmarihtimi", yanasmarihtimi);
            cmdlognj.Parameters.AddWithValue("eta", eta);
            cmdlognj.Parameters.AddWithValue("bowt", bowt);
            cmdlognj.Parameters.AddWithValue("strnt", strnt);
            cmdlognj.Parameters.AddWithValue("bayrak", bayrak);
            cmdlognj.Parameters.AddWithValue("grt", grt);
            cmdlognj.Parameters.AddWithValue("tip", tip);
            cmdlognj.Parameters.AddWithValue("loa", loa);
            cmdlognj.Parameters.AddWithValue("draft", draft);
            cmdlognj.Parameters.AddWithValue("bilgi", bilgi);
            cmdlognj.Parameters.AddWithValue("tehlikeliyuk", tehlikeliyuk);
            cmdlognj.Parameters.AddWithValue("acente", acente);
            cmdlognj.Parameters.AddWithValue("fatura", fatura);
            cmdlognj.Parameters.AddWithValue("notlar", notlar);
            cmdlognj.Parameters.AddWithValue("pratika", pratika);
            cmdlognj.Parameters.AddWithValue("pratikano", pratikano);
            cmdlognj.Parameters.AddWithValue("lcbno", lcbno);
            cmdlognj.Parameters.AddWithValue("lcbdest", lcbdest);
            cmdlognj.Parameters.AddWithValue("lcbdate", lcbdate);
            cmdlognj.Parameters.AddWithValue("talepno", talepno);
            cmdlognj.Parameters.AddWithValue("nedurumda", Lblnedurumda.Text);
            cmdlognj.Parameters.AddWithValue("nedurumdaopr", Lblnedurumdaopr.Text);
            cmdlognj.Parameters.AddWithValue("gizlegoster", "1");
            cmdlognj.Parameters.AddWithValue("kayitzamani", kayitzamani);
            cmdlognj.Parameters.AddWithValue("kaydeden", isimbul + " " + soyisimbul);
            cmdlognj.ExecuteNonQuery();
            cmdlognj.Dispose();



        }

        baglanti.Close();
        NewShipListc();
    }


    protected void Btngemiup_Click(object sender, EventArgs e)
    {
        TBJEsn.BorderColor = System.Drawing.Color.Gray;
        TBJEsn.BorderWidth = 1;
        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
        TBJEetadt.BorderWidth = 1;
        TBJEbt.BorderColor = System.Drawing.Color.Gray;
        TBJEbt.BorderWidth = 1;
        TBJEst.BorderColor = System.Drawing.Color.Gray;
        TBJEst.BorderWidth = 1;
        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
        TBJEgrt.BorderWidth = 1;
        TBJEloa.BorderColor = System.Drawing.Color.Gray;
        TBJEloa.BorderWidth = 1;
        TBJEagency.BorderColor = System.Drawing.Color.Gray;
        TBJEagency.BorderWidth = 1;
        TBJEinvoice.BorderColor = System.Drawing.Color.Gray;
        TBJEinvoice.BorderWidth = 1;
        TBJEnotes.BorderColor = System.Drawing.Color.Gray;
        TBJEnotes.BorderWidth = 1;
        TBJEreqno.BorderColor = System.Drawing.Color.Gray;
        TBJEreqno.BorderWidth = 1;
        TBJEpratikano.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderWidth = 1;
        DDLJEpratika.BorderColor = System.Drawing.Color.Gray;
        DDLJEpratika.BorderWidth = 1;
       TBJElcbno.BorderColor = System.Drawing.Color.Gray;
       TBJElcbno.BorderWidth = 1;
        TBJElcbdest.BorderColor = System.Drawing.Color.Gray;
        TBJElcbdest.BorderWidth = 1;
       TBJElcbdate.BorderColor = System.Drawing.Color.Gray;
       TBJElcbdate.BorderWidth = 1;

        DDLJEap.BorderColor = System.Drawing.Color.Gray;
        DDLJEdepp.BorderColor = System.Drawing.Color.Gray;
        DDLJEdp.BorderColor = System.Drawing.Color.Gray;
        DDLJEflag.BorderColor = System.Drawing.Color.Gray;
        DDLJEtip.BorderColor = System.Drawing.Color.Gray;
        DDLJEdepb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderColor = System.Drawing.Color.Gray;

        DDLJEdc.BorderWidth = 1;
        DDLJEap.BorderWidth = 1;
        DDLJEdepp.BorderWidth = 1;
        DDLJEdp.BorderWidth = 1;
        DDLJEflag.BorderWidth = 1;
        DDLJEtip.BorderWidth = 1;
        DDLJEdepb.BorderWidth = 1;
        DDLJEdb.BorderWidth = 1;



        DDLJEdepb.Visible = true;
        DDLJEdb.Visible = true;

        Button ButtonJobEditkopya = (Button)sender;
        string shipid = ButtonJobEditkopya.CommandArgument.ToString();
        LJEid.Text = shipid;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdgemibul = new SqlCommand("SP_Isliste_gemibilgi", baglanti);
        cmdgemibul.CommandType = CommandType.StoredProcedure;
        cmdgemibul.Parameters.AddWithValue("@id", Convert.ToInt32(shipid));
        SqlDataReader gemireader = cmdgemibul.ExecuteReader();

        string demiryeri = "";
        string bayrak = "";
        string tip = "";
        string tehlikeliyuk = "";
        string kalkislimani = "";
        string kalkisrihtimi = "";
        string yanasmalimani = "";
        string yanasmarihtimi = "";
        string pratika = "";


        if (gemireader.HasRows)
        {
            while (gemireader.Read())
            {
                TBJEimo.Text = gemireader["imono"].ToString();
                TBJEsn.Text = gemireader["gemiadi"].ToString();
                demiryeri = gemireader["demiryeri"].ToString();
                bayrak = gemireader["bayrak"].ToString();
                tip = gemireader["tip"].ToString();
                TBJEetadt.Text = gemireader["eta"].ToString();
                tehlikeliyuk = gemireader["tehlikeliyuk"].ToString();
                TBJEbt.Text = gemireader["bowt"].ToString();
                TBJEst.Text = gemireader["strnt"].ToString();
                TBJEgrt.Text = gemireader["grt"].ToString();
                TBJEloa.Text = gemireader["loa"].ToString();
                LblJEdrft.Text = gemireader["draft"].ToString();
                Labeltpp.Text = gemireader["bilgi"].ToString();
                TBJEagency.Text = gemireader["acente"].ToString();
                TBJEinvoice.Text = gemireader["fatura"].ToString();
                TBJEnotes.Text = gemireader["notlar"].ToString();
                TBJEreqno.Text = gemireader["talepno"].ToString();
                Lblnedurumda.Text = gemireader["nedurumda"].ToString();
                Lblnedurumdaopr.Text = gemireader["nedurumdaopr"].ToString();
                pratika = gemireader["pratika"].ToString();
                TBJEpratikano.Text = gemireader["pratikano"].ToString();
                TBJElcbdate.Text = gemireader["lcbdate"].ToString();
                TBJElcbdest.Text = gemireader["lcbdest"].ToString();
                TBJElcbno.Text =  gemireader["lcbno"].ToString();


                if (TBJElcbdate.Text == "" || TBJElcbdate.Text == null || TBJElcbdate.Text == "__.__.____ __:__") { }
                else if (IsDate2(TBJElcbdate.Text) == true && Convert.ToDateTime(TBJElcbdate.Text) < DateTime.Now.AddHours(-6))
                {
                    SqlConnection baglanti2 = AnaKlas.baglan();
                    SqlCommand cmdisetbup1 = new SqlCommand("SP_Up_Isliste_lcb", baglanti2);
                    cmdisetbup1.CommandType = CommandType.StoredProcedure;
                    cmdisetbup1.Parameters.AddWithValue("@id", shipid);
                    cmdisetbup1.Parameters.AddWithValue("@lcbno", "");
                    cmdisetbup1.Parameters.AddWithValue("@lcbdest", "");
                    cmdisetbup1.Parameters.AddWithValue("@lcbdate", "");
                    cmdisetbup1.ExecuteNonQuery();
                    cmdisetbup1.Dispose();
                    TBJElcbdate.Text = "";
                    TBJElcbdest.Text = "";
                    TBJElcbno.Text = "";
                    baglanti2.Close();
                }


                if (gemireader["kalkislimani"].ToString() == "Yelkenkaya" || gemireader["nedurumda"].ToString()!="9")
                {
                    yanasmalimani = gemireader["yanasmalimani"].ToString();
                    yanasmarihtimi = gemireader["yanasmarihtimi"].ToString();
                    kalkislimani = gemireader["kalkislimani"].ToString();
                    kalkisrihtimi = gemireader["kalkisrihtimi"].ToString();
                }
                else
                {
                    kalkislimani = gemireader["yanasmalimani"].ToString();
                    kalkisrihtimi = gemireader["yanasmarihtimi"].ToString();
                    yanasmalimani = gemireader["kalkislimani"].ToString();
                    yanasmarihtimi = gemireader["kalkisrihtimi"].ToString();
                }
            }
        }
        gemireader.Close();

        if (Convert.ToDateTime(TBJEetadt.Text) < DateTime.Now)
        {
            TBJEetadt.Text = TarihSaatYaziYapDMYhm(DateTime.Now);
        }

        DDLJEdepp.Items.Clear();
        SqlCommand cmdDDLliman = new SqlCommand("SP_DDLlimanal", baglanti);
        cmdDDLliman.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterp = new SqlDataAdapter();
        adapterp.SelectCommand = cmdDDLliman;
        DataSet dsp = new DataSet();
        adapterp.Fill(dsp, "limanlar");
        DDLJEdepp.DataValueField = "limanno";
        DDLJEdepp.DataTextField = "limanadi";
        DDLJEdepp.DataSource = dsp;
        DDLJEdepp.DataBind();
        DDLJEdepp.ClearSelection();
        if (kalkislimani != "") { DDLJEdepp.Items.FindByText(kalkislimani).Selected = true; }

        //kalkış rihtim
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLJEdepp.SelectedItem.Text);
        SqlDataAdapter adapterr = new SqlDataAdapter();
        adapterr.SelectCommand = cmdDDLrihtim;
        DataSet dsr = new DataSet();
        adapterr.Fill(dsr, "limanlar");
        DDLJEdepb.Items.Clear();
        DDLJEdepb.DataValueField = "id";
        DDLJEdepb.DataTextField = "rihtimadi";
        DDLJEdepb.DataSource = dsr;
        DDLJEdepb.DataBind();
        DDLJEdepb.Items.FindByText(kalkisrihtimi).Selected = true;
        if (kalkisrihtimi == "0") { DDLJEdepb.Visible = false; }

        //variş liman
        DDLJEdp.Items.Clear();
        DDLJEdp.DataValueField = "limanno";
        DDLJEdp.DataTextField = "limanadi";
        DDLJEdp.DataSource = dsp;
        DDLJEdp.DataBind();
        DDLJEdp.ClearSelection();
        if (yanasmalimani != "") { DDLJEdp.Items.FindByText(yanasmalimani).Selected = true; }

        //varış rihtim
        SqlCommand cmdDDLrihtim2 = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim2.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim2.Parameters.AddWithValue("@limanadi", DDLJEdp.SelectedItem.Text);
        SqlDataAdapter adapterr2 = new SqlDataAdapter();
        adapterr2.SelectCommand = cmdDDLrihtim2;
        DataSet dsr2 = new DataSet();
        adapterr2.Fill(dsr2, "limanlar");
        DDLJEdb.Items.Clear();
        DDLJEdb.DataValueField = "id";
        DDLJEdb.DataTextField = "rihtimadi";
        DDLJEdb.DataSource = dsr2;
        DDLJEdb.DataBind();
        DDLJEdb.Items.FindByText(yanasmarihtimi).Selected = true;
        if (yanasmarihtimi == "0") { DDLJEdb.Visible = false; }

        SqlCommand cmdDDLdemir = new SqlCommand("SP_DDLdemiral", baglanti);
        cmdDDLdemir.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterdem = new SqlDataAdapter();
        adapterdem.SelectCommand = cmdDDLdemir;
        DataSet dsdem = new DataSet();
        adapterdem.Fill(dsdem, "limanlar");
        DDLJEap.Items.Clear(); //edit
        DDLJEap.DataValueField = "limanno";
        DDLJEap.DataTextField = "limanadi";
        DDLJEap.DataSource = dsdem;
        DDLJEap.DataBind();
        //        DDLJEap.ClearSelection();
        DDLJEap.Items.Insert(0, new ListItem("Select Anchorage?", String.Empty));
        DDLJEap.SelectedIndex = 0;
        //        if (demiryeri != "") { DDLJEap.Items.FindByText(demiryeri).Selected = true; }

        SqlCommand cmdDDLflag = new SqlCommand("SP_DDLflagal", baglanti);
        cmdDDLflag.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterflag = new SqlDataAdapter();
        adapterflag.SelectCommand = cmdDDLflag;
        DataSet dsflag = new DataSet();
        adapterflag.Fill(dsflag, "flaglist");
        DDLJEflag.Items.Clear();//edit
        DDLJEflag.DataValueField = "id";
        DDLJEflag.DataTextField = "flag";
        DDLJEflag.DataSource = dsflag;
        DDLJEflag.DataBind();
        DDLJEflag.ClearSelection();
        if (bayrak != "") { DDLJEflag.Items.FindByText(bayrak).Selected = true; }

        SqlCommand cmdDDLtip = new SqlCommand("SP_DDLshiptip", baglanti);
        cmdDDLtip.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adaptertip = new SqlDataAdapter();
        adaptertip.SelectCommand = cmdDDLtip;
        DataSet dstip = new DataSet();
        adaptertip.Fill(dstip, "shiptip");
        DDLJEtip.Items.Clear();
        DDLJEtip.DataValueField = "id";
        DDLJEtip.DataTextField = "tip";
        DDLJEtip.DataSource = dstip;
        DDLJEtip.DataBind();
        DDLJEtip.ClearSelection();
        if (tip != "") { DDLJEtip.Items.FindByText(tip).Selected = true; }

        DDLJEdc.ClearSelection();
        DDLJEdc.Items.FindByText(tehlikeliyuk).Selected = true;

        if (string.IsNullOrEmpty(pratika) != true)
        { 
            DDLJEpratika.ClearSelection();
            DDLJEpratika.Items.FindByText(pratika).Selected = true;
        }


        if (DDLJEpratika.SelectedItem.Text == "Yes")
        {
            TBJEpratikano.Visible = true;

        }
        else
        {
            TBJEpratikano.Visible = false;
        }

        baglanti.Close();
       

        this.ModalPopupExtenderjobedit.Show();
    }
    protected void Buttonisedit_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        TBJEsn.BorderColor = System.Drawing.Color.Gray;
        TBJEsn.BorderWidth = 1;
        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
        TBJEetadt.BorderWidth = 1;
        TBJEbt.BorderColor = System.Drawing.Color.Gray;
        TBJEbt.BorderWidth = 1;
        TBJEst.BorderColor = System.Drawing.Color.Gray;
        TBJEst.BorderWidth = 1;
        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
        TBJEgrt.BorderWidth = 1;
        TBJEloa.BorderColor = System.Drawing.Color.Gray;
        TBJEloa.BorderWidth = 1;
        TBJEagency.BorderColor = System.Drawing.Color.Gray;
        TBJEagency.BorderWidth = 1;
        TBJEinvoice.BorderColor = System.Drawing.Color.Gray;
        TBJEinvoice.BorderWidth = 1;
        TBJEnotes.BorderColor = System.Drawing.Color.Gray;
        TBJEnotes.BorderWidth = 1;
        TBJEreqno.BorderColor = System.Drawing.Color.Gray;
        TBJEreqno.BorderWidth = 1;
        TBJEpratikano.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderWidth = 1;
        DDLJEpratika.BorderColor = System.Drawing.Color.Gray;
        DDLJEpratika.BorderWidth = 1;
       TBJElcbno.BorderColor = System.Drawing.Color.Gray;
       TBJElcbno.BorderWidth = 1;
        TBJElcbdest.BorderColor = System.Drawing.Color.Gray;
        TBJElcbdest.BorderWidth = 1;
       TBJElcbdate.BorderColor = System.Drawing.Color.Gray;
       TBJElcbdate.BorderWidth = 1;

        DDLJEap.BorderColor = System.Drawing.Color.Gray;
        DDLJEdp.BorderColor = System.Drawing.Color.Gray;
        DDLJEdepp.BorderColor = System.Drawing.Color.Gray;
        DDLJEflag.BorderColor = System.Drawing.Color.Gray;
        DDLJEtip.BorderColor = System.Drawing.Color.Gray;
        DDLJEdb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdepb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderWidth = 1;
        DDLJEap.BorderWidth = 1;
        DDLJEdp.BorderWidth = 1;
        DDLJEdepp.BorderWidth = 1;
        DDLJEflag.BorderWidth = 1;
        DDLJEtip.BorderWidth = 1;
        DDLJEdb.BorderWidth = 1;
        DDLJEdepb.BorderWidth = 1;

        string imono = TBJEimo.Text;
        string gemiadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEsn.Text.ToString().Trim().ToLower()));
        string demiryeri = HttpUtility.HtmlDecode(DDLJEap.SelectedItem.Text);
        string kalkislimani = HttpUtility.HtmlDecode(DDLJEdepp.SelectedItem.Text);
        string kalkisrihtimi = HttpUtility.HtmlDecode(DDLJEdepb.SelectedItem.Text);
        string yanasmalimani = HttpUtility.HtmlDecode(DDLJEdp.SelectedItem.Text);
        string yanasmarihtimi = HttpUtility.HtmlDecode(DDLJEdb.SelectedItem.Text);
        string eta = TBJEetadt.Text;
        string bowt = TBJEbt.Text;
        string strnt = TBJEst.Text;
        string bayrak = HttpUtility.HtmlDecode(DDLJEflag.SelectedItem.Text);
        string grt = TBJEgrt.Text;
        string draft = LblJEdrft.Text;
        string tip = HttpUtility.HtmlDecode(DDLJEtip.SelectedItem.Text);
        string loa = TBJEloa.Text;
        string tehlikeliyuk = HttpUtility.HtmlDecode(DDLJEdc.SelectedItem.Text);
        string acente = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEagency.Text.ToString().Trim().ToLower()));
        string fatura = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEinvoice.Text.ToString().Trim().ToLower()));
        string notlar = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEnotes.Text.ToString().Trim().ToLower()));
        string talepno = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEreqno.Text.ToString().Trim().ToLower()));
        string kayitzamani = TarihSaatYaziYapDMYhm(DateTime.Now);
        string pratika = HttpUtility.HtmlDecode(DDLJEpratika.SelectedItem.Text);
        string pratikano = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEpratikano.Text.ToString().Trim().ToLower()));
       string lcbno = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJElcbno.Text.ToString().Trim().ToLower()));
       string lcbdest = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJElcbdest.Text.ToString().Trim().ToLower()));
       string lcbdate = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJElcbdate.Text.ToString().Trim().ToLower()));

        string hata = "yok";

        if (lcbdate == "__.__.____ __:__") { lcbdate = ""; }

        if (gemiadi == "" || gemiadi == null || gemiadi == "takviye")
        {
            hata = "var";
            TBJEsn.BorderColor = System.Drawing.Color.Red;
            TBJEsn.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (demiryeri == "Select Anchorage?")
        {
            hata = "var";
            DDLJEap.BorderColor = System.Drawing.Color.Red;
            DDLJEap.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (eta == "" || eta == null || eta == "__.__.____ __:__")
        {
            hata = "var";
            TBJEetadt.BorderColor = System.Drawing.Color.Red;
            TBJEetadt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        else if (IsDate2(eta) != true)
        {
            hata = "var";
            TBJEetadt.BorderColor = System.Drawing.Color.Red;
            TBJEetadt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (bowt == "" || bowt == null)
        {
            hata = "var";
            TBJEbt.BorderColor = System.Drawing.Color.Red;
            TBJEbt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (strnt == "" || strnt == null)
        {
            hata = "var";
            TBJEst.BorderColor = System.Drawing.Color.Red;
            TBJEst.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (grt == "" || grt == null)
        {
            hata = "var";
            TBJEgrt.BorderColor = System.Drawing.Color.Red;
            TBJEgrt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (loa == "" || loa == null)
        {
            hata = "var";
            TBJEloa.BorderColor = System.Drawing.Color.Red;
            TBJEloa.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (acente == "" || acente == null)
        {
            hata = "var";
            TBJEagency.BorderColor = System.Drawing.Color.Red;
            TBJEagency.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (fatura == "" || fatura == null)
        {
            hata = "var";
            TBJEinvoice.BorderColor = System.Drawing.Color.Red;
            TBJEinvoice.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (notlar == "" || notlar == null)
        {
            hata = "var";
            TBJEnotes.BorderColor = System.Drawing.Color.Red;
            TBJEnotes.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (talepno == "" || talepno == null)
        {
            hata = "var";
            TBJEreqno.BorderColor = System.Drawing.Color.Red;
            TBJEreqno.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }


        if (TBJElcbno.Text.Trim() != "" || TBJElcbdest.Text.Trim() != "" || lcbdate.Trim() != "" )
        {
            if (TBJElcbno.Text == "")
            {
                hata = "var";
                TBJElcbno.BorderColor = System.Drawing.Color.Red;
                TBJElcbno.BorderWidth = 1;
                this.ModalPopupExtenderjobedit.Show();
            }
            else if (TBJElcbdest.Text == "")
            {
                hata = "var";
                TBJElcbdest.BorderColor = System.Drawing.Color.Red;
                TBJElcbdest.BorderWidth = 1;
                this.ModalPopupExtenderjobedit.Show();
            }

            else if (lcbdate == "" || lcbdate == null || lcbdate == "__.__.____ __:__")
            {
                hata = "var";
                TBJElcbdate.BorderColor = System.Drawing.Color.Red;
                TBJElcbdate.BorderWidth = 1;
                this.ModalPopupExtenderjobedit.Show();
            }
            else if (IsDate2(lcbdate) != true)
            {
                hata = "var";
                TBJElcbdate.BorderColor = System.Drawing.Color.Red;
                TBJElcbdate.BorderWidth = 1;
                this.ModalPopupExtenderjobedit.Show();
            }
            else if (Convert.ToDateTime(lcbdate) < DateTime.Now.AddHours(-6))
            {
                hata = "var";
                TBJElcbdate.BorderColor = System.Drawing.Color.Red;
                TBJElcbdate.BorderWidth = 1;
                this.ModalPopupExtenderjobedit.Show();
            }

        }



        if (pratika == "Yes")
        {
            if (pratikano == "" || pratikano == null)
            {
                hata = "var";
                TBJEpratikano.BorderColor = System.Drawing.Color.Red;
                TBJEpratikano.BorderWidth = 1;
                this.ModalPopupExtenderjobedit.Show();
            }

        }

        if (hata == "yok")
        {
                int imonoi = Convert.ToInt32(imono);

                string nedurumda = Lblnedurumda.Text;
                string nedurumdaopr = Lblnedurumdaopr.Text;

                    SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                    cmdLimannoal.CommandType = CommandType.StoredProcedure;
                    cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
                    cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int);
                    cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                    cmdLimannoal.ExecuteNonQuery();
                    int portno = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString());
                    cmdLimannoal.Dispose();
                    if (portno > 0 && portno < 900) // limanda
                    {
                        nedurumda = "2";
                        nedurumdaopr = "2";
                    }

                    else if (portno > 1000 && portno < 1099) // demirde
                    {
                        nedurumda = "1";
                        nedurumdaopr = "1";
                    }


                    else if (portno == 998) // yelkenkaya
                    {
                        nedurumda = "4";
                        nedurumdaopr = "4";
                    }

                    //else if (portno == 999) // to order
                    //{                }


                if (Convert.ToDateTime(eta) < DateTime.Now)
                {
                    eta = kayitzamani;
                }


            string bilgi = "0";
            bilgi = AnaKlas.tpphesapla(grt, tip, loa, bowt, strnt, tehlikeliyuk, kalkisrihtimi, yanasmarihtimi)[0];
            draft = AnaKlas.tpphesapla(grt, tip, loa, bowt, strnt, tehlikeliyuk, kalkisrihtimi, yanasmarihtimi)[1];

            SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string isimbul = cmdisimbul.ExecuteScalar().ToString();
                cmdisimbul.Dispose();
                SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
                //if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
                cmdsoyisimbul.Dispose();
                string isimkisalt = isimbul.Substring(0, 1) + soyisimbul.Substring(0, 1);

            if (notlar.EndsWith(")") && notlar.Substring(notlar.Length - 3, 1) == "-")
            { notlar = notlar.Substring(0, notlar.Length - 15); }

            int secimedit = Convert.ToInt32(LJEid.Text);
            SqlCommand cmd = new SqlCommand("update isliste set imono=@imono, gemiadi=@gemiadi,demiryeri=@demiryeri,kalkislimani=@kalkislimani,kalkisrihtimi=@kalkisrihtimi,yanasmalimani=@yanasmalimani,yanasmarihtimi=@yanasmarihtimi,eta=@eta,bowt=@bowt,strnt=@strnt,bayrak=@bayrak,grt=@grt,tip=@tip,loa=@loa,draft=@draft,bilgi=@bilgi,tehlikeliyuk=@tehlikeliyuk,acente=@acente,fatura=@fatura,notlar=@notlar,nedurumda=@nedurumda,nedurumdaopr=@nedurumdaopr,talepno=@talepno,pratika=@pratika,pratikano=@pratikano,lcbno=@lcbno,lcbdest=@lcbdest,lcbdate=@lcbdate,kayitzamani=@kayitzamani, kaydeden=@kaydeden where id=" + secimedit, baglanti);
                cmd.Parameters.AddWithValue("imono", imonoi);
                cmd.Parameters.AddWithValue("gemiadi", gemiadi);
                cmd.Parameters.AddWithValue("demiryeri", demiryeri);
                cmd.Parameters.AddWithValue("kalkislimani", kalkislimani);
                cmd.Parameters.AddWithValue("kalkisrihtimi", kalkisrihtimi);
                cmd.Parameters.AddWithValue("yanasmalimani", yanasmalimani);
                cmd.Parameters.AddWithValue("yanasmarihtimi", yanasmarihtimi);
                cmd.Parameters.AddWithValue("eta", eta);
                //cmd.Parameters.AddWithValue("etb", etb);
                cmd.Parameters.AddWithValue("bowt", bowt);
                cmd.Parameters.AddWithValue("strnt", strnt);
                cmd.Parameters.AddWithValue("bayrak", bayrak);
                cmd.Parameters.AddWithValue("grt", grt);
                cmd.Parameters.AddWithValue("tip", tip);
                cmd.Parameters.AddWithValue("loa", loa);
                cmd.Parameters.AddWithValue("draft", draft);
                cmd.Parameters.AddWithValue("bilgi", bilgi);
                cmd.Parameters.AddWithValue("tehlikeliyuk", tehlikeliyuk);
                cmd.Parameters.AddWithValue("acente", acente);
                cmd.Parameters.AddWithValue("fatura", fatura);
                cmd.Parameters.AddWithValue("notlar", notlar);
            cmd.Parameters.AddWithValue("pratika", pratika);
            cmd.Parameters.AddWithValue("pratikano", pratikano);
            cmd.Parameters.AddWithValue("lcbno", lcbno);
            cmd.Parameters.AddWithValue("lcbdest", lcbdest);
            cmd.Parameters.AddWithValue("lcbdate", lcbdate);
                cmd.Parameters.AddWithValue("nedurumda", nedurumda);
                cmd.Parameters.AddWithValue("nedurumdaopr", nedurumdaopr);
                cmd.Parameters.AddWithValue("talepno", talepno);
                cmd.Parameters.AddWithValue("kayitzamani", kayitzamani);
                cmd.Parameters.AddWithValue("kaydeden", isimkisalt + " " + DateTime.Now.ToShortDateString().Substring(0, 2) + "|" + DateTime.Now.ToShortTimeString().Substring(0, 2));
                cmd.ExecuteNonQuery();
                cmd.Dispose();


            //log_newjob

            SqlCommand cmdlognj = new SqlCommand("SP_log_newjob_ekle", baglanti);
            cmdlognj.CommandType = CommandType.StoredProcedure;
            cmdlognj.Parameters.AddWithValue("imono", imono);
            cmdlognj.Parameters.AddWithValue("gemiadi", gemiadi);
            cmdlognj.Parameters.AddWithValue("demiryeri", demiryeri);
            cmdlognj.Parameters.AddWithValue("kalkislimani", kalkislimani);
            cmdlognj.Parameters.AddWithValue("kalkisrihtimi", kalkisrihtimi);
            cmdlognj.Parameters.AddWithValue("yanasmalimani", yanasmalimani);
            cmdlognj.Parameters.AddWithValue("yanasmarihtimi", yanasmarihtimi);
            cmdlognj.Parameters.AddWithValue("eta", eta);
            cmdlognj.Parameters.AddWithValue("bowt", bowt);
            cmdlognj.Parameters.AddWithValue("strnt", strnt);
            cmdlognj.Parameters.AddWithValue("bayrak", bayrak);
            cmdlognj.Parameters.AddWithValue("grt", grt);
            cmdlognj.Parameters.AddWithValue("tip", tip);
            cmdlognj.Parameters.AddWithValue("loa", loa);
            cmdlognj.Parameters.AddWithValue("draft", draft);
            cmdlognj.Parameters.AddWithValue("bilgi", bilgi);
            cmdlognj.Parameters.AddWithValue("tehlikeliyuk", tehlikeliyuk);
            cmdlognj.Parameters.AddWithValue("acente", acente);
            cmdlognj.Parameters.AddWithValue("fatura", fatura);
            cmdlognj.Parameters.AddWithValue("notlar", notlar);
            cmdlognj.Parameters.AddWithValue("pratika", pratika);
            cmdlognj.Parameters.AddWithValue("pratikano", pratikano);
            cmdlognj.Parameters.AddWithValue("lcbno", lcbno);
            cmdlognj.Parameters.AddWithValue("lcbdest", lcbdest);
            cmdlognj.Parameters.AddWithValue("lcbdate", lcbdate);
            cmdlognj.Parameters.AddWithValue("talepno", talepno);
            cmdlognj.Parameters.AddWithValue("nedurumda", Lblnedurumda.Text);
            cmdlognj.Parameters.AddWithValue("nedurumdaopr", Lblnedurumdaopr.Text);
            cmdlognj.Parameters.AddWithValue("gizlegoster", "1");
            cmdlognj.Parameters.AddWithValue("kayitzamani", kayitzamani);
            cmdlognj.Parameters.AddWithValue("kaydeden", isimbul + " " + soyisimbul);
            cmdlognj.ExecuteNonQuery();
            cmdlognj.Dispose();



        }
        baglanti.Close();
        NewShipListc();

    }


    protected void LBfindves_Click(object sender, EventArgs e)
    {
        string shipimoal = TBimono.Text;
        string shipal = Temizle(TBshipname.Text.ToString());

        if (!String.IsNullOrEmpty(shipal) && shipal.Length > 1)
        { //isim arra
            TBimono.Text = "";
            Lbltwice.Text = "";
            using (PilotdbEntities2 isliste = new PilotdbEntities2())
            {
                shipal = shipal.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ");
                var veri = from b in isliste.isliste.Where(b => b.gemiadi.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ").Contains(shipal)).OrderBy(b => b.gemiadi).Take(20) select b;
                DLNewjob.DataSource = veri.ToList();
                DLNewjob.DataBind();

                if (veri.ToList().Count() > 0)
                { Lbltwice.Text = "Please Delete one vessel which was Dublicated."; }
            }
        }

        else if (!String.IsNullOrEmpty(shipimoal) && shipimoal.Length == 7)
        { //imo arra 
            TBshipname.Text = "";
            Lbltwice.Text = "";
            int shipimoali = Convert.ToInt32(shipimoal);
            using (PilotdbEntities2 isliste = new PilotdbEntities2())
            {
                var veri = from b in isliste.isliste.Where(b => b.imono == shipimoali).Take(20) select b;
                DLNewjob.DataSource = veri.ToList();
                DLNewjob.DataBind();

                if (veri.Count() > 0)
                { Lbltwice.Text = "Please Delete one vessel which was Dublicated."; }
            }
        }

        else if (String.IsNullOrEmpty(shipimoal) && String.IsNullOrEmpty(shipal))
        {
            NewShipListc();
        }

        else
        {
            TBimono.Text = "";
            TBshipname.Text = "";
            using (PilotdbEntities2 isliste = new PilotdbEntities2())
            {
                var veri = from b in isliste.isliste.Where(x => x.grt == "99999999").Take(20) select b;
                DLNewjob.DataSource = veri.ToList();
                DLNewjob.DataBind();
            }
        }
        //twice cek
        Lbltwice.Text = "";
        //SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmdgemitcek = new SqlCommand("SP_isliste_twicecek", baglanti);
        //cmdgemitcek.CommandType = CommandType.StoredProcedure;
        //SqlDataReader varisreader = cmdgemitcek.ExecuteReader();
        //if (varisreader.HasRows)
        //{
        //    Lbltwice.Text = "Dublicated Vessels : ";
        //    while (varisreader.Read())
        //    {
        //        Lbltwice.Text += varisreader["gemiadi"] + ", ";
        //    }
        //}

        //varisreader.Close();
        //cmdgemitcek.Dispose();
        //baglanti.Close();



    }
    protected void TBimono_TextChanged(object sender, EventArgs e)
    {
        string shipimoal = TBimono.Text;

        if (!String.IsNullOrEmpty(shipimoal) && shipimoal.Length == 7)
        { //imo arra 
            TBshipname.Text = "";
            int shipimoali = Convert.ToInt32(shipimoal);
            using (PilotdbEntities2 isliste = new PilotdbEntities2())
            {
                var veri = from b in isliste.isliste.Where(b => b.imono == shipimoali).Take(20) select b;
                DLNewjob.DataSource = veri.ToList();
                DLNewjob.DataBind();
            }
        }
    }

    protected void TextBoxaddimo_TextChanged(object sender, EventArgs e)
    {
        string shipimoal = TextBoxaddimo.Text;

        if (!String.IsNullOrEmpty(shipimoal) && shipimoal.Length == 7)
        { //imo ara varsa editi aç 
            int shipimoali = Convert.ToInt32(shipimoal);
            using (PilotdbEntities2 isliste = new PilotdbEntities2())
            {
                var veri = from b in isliste.isliste.Where(b => b.imono == shipimoali).Take(1) select b;
                var verinedurumda = from c in veri.Where(c => c.nedurumda == "8") select c;

                if (veri.Count() > 0)
                {
                    if (shipimoal == "9999999" || shipimoal == "8888888")
                    {
                        issilmes.Text = "THIS IMO NUMBER CANNOT BE USE";
                        Literal1.Visible = false;
                        CBdeleteis.Visible = false;
                        Bacceptedok.Visible = false;
                        this.ModalPopupMessageok.Show();
                    }
                    else
                    {
                        if (verinedurumda.Count() == 0)
                    {
                            SqlConnection baglanti = AnaKlas.baglan();

                            TBJEsn.BorderColor = System.Drawing.Color.Gray;
                        TBJEsn.BorderWidth = 1;
                        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
                        TBJEetadt.BorderWidth = 1;
                        TBJEbt.BorderColor = System.Drawing.Color.Gray;
                        TBJEbt.BorderWidth = 1;
                        TBJEst.BorderColor = System.Drawing.Color.Gray;
                        TBJEst.BorderWidth = 1;
                        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
                        TBJEgrt.BorderWidth = 1;
                        TBJEloa.BorderColor = System.Drawing.Color.Gray;
                        TBJEloa.BorderWidth = 1;
                        TBJEagency.BorderColor = System.Drawing.Color.Gray;
                        TBJEagency.BorderWidth = 1;
                        TBJEinvoice.BorderColor = System.Drawing.Color.Gray;
                        TBJEinvoice.BorderWidth = 1;
                        TBJEnotes.BorderColor = System.Drawing.Color.Gray;
                        TBJEnotes.BorderWidth = 1;
                        TBJEreqno.BorderColor = System.Drawing.Color.Gray;
                        TBJEreqno.BorderWidth = 1;
                            TBJEpratikano.BorderColor = System.Drawing.Color.Gray;
                            TBJEpratikano.BorderWidth = 1;
                            DDLJEpratika.BorderColor = System.Drawing.Color.Gray;
                            DDLJEpratika.BorderWidth = 1;
                            TBJElcbno.BorderColor = System.Drawing.Color.Gray;
                            TBJElcbno.BorderWidth = 1;
                            TBJElcbdest.BorderColor = System.Drawing.Color.Gray;
                            TBJElcbdest.BorderWidth = 1;
                            TBJElcbdate.BorderColor = System.Drawing.Color.Gray;
                            TBJElcbdate.BorderWidth = 1;
                            DDLJEap.BorderColor = System.Drawing.Color.Gray;
                        DDLJEdepp.BorderColor = System.Drawing.Color.Gray;
                        DDLJEdp.BorderColor = System.Drawing.Color.Gray;
                        DDLJEflag.BorderColor = System.Drawing.Color.Gray;
                        DDLJEtip.BorderColor = System.Drawing.Color.Gray;
                        DDLJEdepb.BorderColor = System.Drawing.Color.Gray;
                        DDLJEdb.BorderColor = System.Drawing.Color.Gray;
                        DDLJEdc.BorderColor = System.Drawing.Color.Gray;
                        DDLJEdc.BorderWidth = 1;
                        DDLJEap.BorderWidth = 1;
                        DDLJEdepp.BorderWidth = 1;
                        DDLJEdp.BorderWidth = 1;
                        DDLJEflag.BorderWidth = 1;
                        DDLJEtip.BorderWidth = 1;
                        DDLJEdepb.BorderWidth = 1;
                        DDLJEdb.BorderWidth = 1;

                        DDLJEdepb.Visible = true;
                        DDLJEdb.Visible = true;

                        SqlCommand cmdgemibuli = new SqlCommand("SP_Isliste_gemibilgifmimo", baglanti);
                        cmdgemibuli.CommandType = CommandType.StoredProcedure;
                        cmdgemibuli.Parameters.AddWithValue("@imono", shipimoali);
                        SqlDataReader gemireader = cmdgemibuli.ExecuteReader();

                        string demiryeri = "";
                        string bayrak = "";
                        string tip = "";
                        string tehlikeliyuk = "";
                        string kalkislimani = "";
                        string kalkisrihtimi = "";
                        string yanasmalimani = "";
                        string yanasmarihtimi = "";
                            string pratika = "";

                            if (gemireader.HasRows)
                        {
                            while (gemireader.Read())
                            {
                                LJEid.Text = gemireader["id"].ToString();
                                TBJEimo.Text = gemireader["imono"].ToString();
                                TBJEsn.Text = gemireader["gemiadi"].ToString();
                                demiryeri = gemireader["demiryeri"].ToString();
                                bayrak = gemireader["bayrak"].ToString();
                                tip = gemireader["tip"].ToString();
                                TBJEetadt.Text = gemireader["eta"].ToString();
                                tehlikeliyuk = gemireader["tehlikeliyuk"].ToString();
                                TBJEbt.Text = gemireader["bowt"].ToString();
                                TBJEst.Text = gemireader["strnt"].ToString();
                                TBJEgrt.Text = gemireader["grt"].ToString();
                                TBJEloa.Text = gemireader["loa"].ToString();
                                    LblJEdrft.Text = gemireader["draft"].ToString();
                                Labeltpp.Text = gemireader["bilgi"].ToString();
                                TBJEagency.Text = gemireader["acente"].ToString();
                                TBJEinvoice.Text = gemireader["fatura"].ToString();
                                TBJEnotes.Text = gemireader["notlar"].ToString();
                                TBJEreqno.Text = gemireader["talepno"].ToString();
                                Lblnedurumda.Text = gemireader["nedurumda"].ToString();
                                Lblnedurumdaopr.Text = gemireader["nedurumdaopr"].ToString();
                                    pratika = gemireader["pratika"].ToString();
                                    TBJEpratikano.Text = gemireader["pratikano"].ToString();
                                    TBJElcbdate.Text = gemireader["lcbdate"].ToString();
                                    TBJElcbdest.Text = gemireader["lcbdate"].ToString();
                                    TBJElcbno.Text = gemireader["lcbno"].ToString();


                                    if (TBJElcbdate.Text == "" || TBJElcbdate.Text == null || TBJElcbdate.Text == "__.__.____ __:__") { }
                                    else if (IsDate2(TBJElcbdate.Text) == true && Convert.ToDateTime(TBJElcbdate.Text) < DateTime.Now.AddHours(-6))
                                    {
                                        SqlConnection baglanti2 = AnaKlas.baglan();
                                        SqlCommand cmdisetbup = new SqlCommand("SP_Up_Isliste_lcb_imo", baglanti2);
                                        cmdisetbup.CommandType = CommandType.StoredProcedure;
                                        cmdisetbup.Parameters.AddWithValue("@imono", shipimoali);
                                        cmdisetbup.Parameters.AddWithValue("@lcbno", "");
                                        cmdisetbup.Parameters.AddWithValue("@lcbdest", "");
                                        cmdisetbup.Parameters.AddWithValue("@lcbdate", "");
                                        cmdisetbup.ExecuteNonQuery();
                                        cmdisetbup.Dispose();
                                        TBJElcbdate.Text = "";
                                        TBJElcbdest.Text = "";
                                        TBJElcbno.Text = "";
                                        baglanti2.Close();
                                    }



                                    if (gemireader["kalkislimani"].ToString() == "Yelkenkaya" || gemireader["nedurumda"].ToString() != "9")
                                {
                                    yanasmalimani = gemireader["yanasmalimani"].ToString();
                                    yanasmarihtimi = gemireader["yanasmarihtimi"].ToString();
                                    kalkislimani = gemireader["kalkislimani"].ToString();
                                    kalkisrihtimi = gemireader["kalkisrihtimi"].ToString();
                                }
                                else
                                {
                                    kalkislimani = gemireader["yanasmalimani"].ToString();
                                    kalkisrihtimi = gemireader["yanasmarihtimi"].ToString();
                                    yanasmalimani = gemireader["kalkislimani"].ToString();
                                    yanasmarihtimi = gemireader["kalkisrihtimi"].ToString();
                                }
                            }
                        }
                        gemireader.Close();

                        if (Convert.ToDateTime(TBJEetadt.Text) < DateTime.Now)
                        {
                            TBJEetadt.Text = TarihSaatYaziYapDMYhm(DateTime.Now);
                        }

                        DDLJEdepp.Items.Clear();
                        SqlCommand cmdDDLliman = new SqlCommand("SP_DDLlimanal", baglanti);
                        cmdDDLliman.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapterp = new SqlDataAdapter();
                        adapterp.SelectCommand = cmdDDLliman;
                        DataSet dsp = new DataSet();
                        adapterp.Fill(dsp, "limanlar");
                        DDLJEdepp.DataValueField = "limanno";
                        DDLJEdepp.DataTextField = "limanadi";
                        DDLJEdepp.DataSource = dsp;
                        DDLJEdepp.DataBind();
                        DDLJEdepp.ClearSelection();
                        if (kalkislimani != "") { DDLJEdepp.Items.FindByText(kalkislimani).Selected = true; }

                        //kalkış rihtim
                        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
                        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
                        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLJEdepp.SelectedItem.Text);
                        SqlDataAdapter adapterr = new SqlDataAdapter();
                        adapterr.SelectCommand = cmdDDLrihtim;
                        DataSet dsr = new DataSet();
                        adapterr.Fill(dsr, "limanlar");
                        DDLJEdepb.Items.Clear();
                        DDLJEdepb.DataValueField = "id";
                        DDLJEdepb.DataTextField = "rihtimadi";
                        DDLJEdepb.DataSource = dsr;
                        DDLJEdepb.DataBind();
                        DDLJEdepb.Items.FindByText(kalkisrihtimi).Selected = true;
                        if (kalkisrihtimi == "0") { DDLJEdepb.Visible = false; }

                        //variş liman
                        DDLJEdp.Items.Clear();
                        DDLJEdp.DataValueField = "limanno";
                        DDLJEdp.DataTextField = "limanadi";
                        DDLJEdp.DataSource = dsp;
                        DDLJEdp.DataBind();
                        DDLJEdp.ClearSelection();
                        if (yanasmalimani != "") { DDLJEdp.Items.FindByText(yanasmalimani).Selected = true; }

                        //varış rihtim
                        SqlCommand cmdDDLrihtim2 = new SqlCommand("SP_DDLrihtimal", baglanti);
                        cmdDDLrihtim2.CommandType = CommandType.StoredProcedure;
                        cmdDDLrihtim2.Parameters.AddWithValue("@limanadi", DDLJEdp.SelectedItem.Text);
                        SqlDataAdapter adapterr2 = new SqlDataAdapter();
                        adapterr2.SelectCommand = cmdDDLrihtim2;
                        DataSet dsr2 = new DataSet();
                        adapterr2.Fill(dsr2, "limanlar");
                        DDLJEdb.Items.Clear();
                        DDLJEdb.DataValueField = "id";
                        DDLJEdb.DataTextField = "rihtimadi";
                        DDLJEdb.DataSource = dsr2;
                        DDLJEdb.DataBind();
                        DDLJEdb.Items.FindByText(yanasmarihtimi).Selected = true;
                        if (yanasmarihtimi == "0") { DDLJEdb.Visible = false; }

                        SqlCommand cmdDDLdemir = new SqlCommand("SP_DDLdemiral", baglanti);
                        cmdDDLdemir.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapterdem = new SqlDataAdapter();
                        adapterdem.SelectCommand = cmdDDLdemir;
                        DataSet dsdem = new DataSet();
                        adapterdem.Fill(dsdem, "limanlar");
                        DDLJEap.Items.Clear(); //edit
                        DDLJEap.DataValueField = "limanno";
                        DDLJEap.DataTextField = "limanadi";
                        DDLJEap.DataSource = dsdem;
                        DDLJEap.DataBind();
                        DDLJEap.ClearSelection();
                        if (demiryeri != "") { DDLJEap.Items.FindByText(demiryeri).Selected = true; }

                        SqlCommand cmdDDLflag = new SqlCommand("SP_DDLflagal", baglanti);
                        cmdDDLflag.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapterflag = new SqlDataAdapter();
                        adapterflag.SelectCommand = cmdDDLflag;
                        DataSet dsflag = new DataSet();
                        adapterflag.Fill(dsflag, "flaglist");
                        DDLJEflag.Items.Clear();//edit
                        DDLJEflag.DataValueField = "id";
                        DDLJEflag.DataTextField = "flag";
                        DDLJEflag.DataSource = dsflag;
                        DDLJEflag.DataBind();
                        DDLJEflag.ClearSelection();
                        if (bayrak != "") { DDLJEflag.Items.FindByText(bayrak).Selected = true; }

                        SqlCommand cmdDDLtip = new SqlCommand("SP_DDLshiptip", baglanti);
                        cmdDDLtip.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptertip = new SqlDataAdapter();
                        adaptertip.SelectCommand = cmdDDLtip;
                        DataSet dstip = new DataSet();
                        adaptertip.Fill(dstip, "shiptip");
                        DDLJEtip.Items.Clear();
                        DDLJEtip.DataValueField = "id";
                        DDLJEtip.DataTextField = "tip";
                        DDLJEtip.DataSource = dstip;
                        DDLJEtip.DataBind();
                        DDLJEtip.ClearSelection();
                        if (tip != "") { DDLJEtip.Items.FindByText(tip).Selected = true; }

                        DDLJEdc.ClearSelection();
                        DDLJEdc.Items.FindByText(tehlikeliyuk).Selected = true;


                            if (string.IsNullOrEmpty(pratika) != true)
                            {
                                DDLJEpratika.ClearSelection();
                                DDLJEpratika.Items.FindByText(pratika).Selected = true;
                            }


                            if (DDLJEpratika.SelectedItem.Text == "Yes")
                            {
                                TBJEpratikano.Visible = true;

                            }
                            else
                            {
                                TBJEpratikano.Visible = false;
                            }

                            baglanti.Close();


                          

                            this.ModalPopupExtenderjobedit.Show();
                    }
                    else
                    {

                        SqlConnection baglanti = AnaKlas.baglan();
                        SqlCommand cmdgemibulie = new SqlCommand("SP_Isliste_gemibilgifmimo", baglanti);
                        cmdgemibulie.CommandType = CommandType.StoredProcedure;
                        cmdgemibulie.Parameters.AddWithValue("@imono", shipimoali);
                        SqlDataReader gemireaderi = cmdgemibulie.ExecuteReader();
                        string gemiimo = "";
                        string gemiadi = "";
                        if (gemireaderi.HasRows)
                        {
                            while (gemireaderi.Read())
                            {
                                gemiimo = gemireaderi["imono"].ToString();
                                gemiadi = gemireaderi["gemiadi"].ToString();

                            }
                        }

                        gemireaderi.Close();
                        baglanti.Close();

                        issilmes.Text = "Vessel :" + gemiadi + ". NOW IN MANEUVRING, CANNOT BE EDITED. Imo:" + gemiimo;
                        Literal1.Visible = false;
                        CBdeleteis.Visible = false;
                        Bacceptedok.Visible = false;
                        this.ModalPopupMessageok.Show();
                    }
                }
                }
                else
                {
                    this.ModalPopupyeniisekle.Show();


                }

            }

        }
    }

    protected void ImageButtonissil_Click(object sender, ImageClickEventArgs e)
    {
        Literal1.Visible = true;
        CBdeleteis.Visible = true;
        Bacceptedok.Visible = true;

        ImageButton ImageButtonissilkopya = (ImageButton)sender;
        int seciliidi = Convert.ToInt32(ImageButtonissilkopya.CommandArgument.ToString());
        Bacceptedok.CommandArgument = seciliidi.ToString();

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("select gemiadi from isliste where id=" + seciliidi, baglanti);
        string gemial = cmd.ExecuteScalar().ToString();
        cmd.Dispose();
        baglanti.Close();

        issilmes.Text = "The job record for " + gemial + " will be deleted, <br/> Please Confirm!";
        this.ModalPopupMessageok.Show();
    }

    protected void DDLdepport_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLdepberth.Visible = true;
        DDLdepberth.Items.Clear();
        this.ModalPopupyeniisekle.Show();

        //DTDoldur sp li
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLdepport.SelectedItem.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLdepberth.DataValueField = "id";
        DDLdepberth.DataTextField = "rihtimadi";
        DDLdepberth.DataSource = ds;
        DDLdepberth.DataBind();

        baglanti.Close();

        if (DDLdepberth.SelectedItem.Text == "0") { DDLdepberth.Visible = false; }
    }
    protected void DDLdestport_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLdestberth.Visible = true;
        DDLdestberth.Items.Clear();
        this.ModalPopupyeniisekle.Show();
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLdestport.SelectedItem.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLdestberth.DataValueField = "id";
        DDLdestberth.DataTextField = "rihtimadi";
        DDLdestberth.DataSource = ds;
        DDLdestberth.DataBind();
        baglanti.Close();
        if (DDLdestberth.SelectedItem.Text == "0") { DDLdestberth.Visible = false; }





    }
    protected void DDLJEdepp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLJEdepb.Visible = true;
        DDLJEdepb.Items.Clear();
        this.ModalPopupExtenderjobedit.Show();

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLJEdepp.SelectedItem.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLJEdepb.DataValueField = "id";
        DDLJEdepb.DataTextField = "rihtimadi";
        DDLJEdepb.DataSource = ds;
        DDLJEdepb.DataBind();
        baglanti.Close();

        if (DDLJEdepb.SelectedItem.Text == "0") { DDLJEdepb.Visible = false; }
    }
    protected void DDLJEdp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLJEdb.Visible = true;
        DDLJEdb.Items.Clear();
        this.ModalPopupExtenderjobedit.Show();



        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLJEdp.SelectedItem.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLJEdb.DataValueField = "id";
        DDLJEdb.DataTextField = "rihtimadi";
        DDLJEdb.DataSource = ds;
        DDLJEdb.DataBind();
        baglanti.Close();

        if (DDLJEdb.SelectedItem.Text == "0") { DDLJEdb.Visible = false; }
    }

    protected void DDLanchorplace_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLanchorplace.SelectedItem.Text == "Ordino-Talep Yok")
        {
            TextBoxisadd4.Text = TarihSaatYaziYapDMYhm(DateTime.Now.Date.AddDays(1).AddMinutes(-1));
            TextBoxisadd7.Text = "0";
            TextBoxisadd8.Text = "0";
            TextBoxisadd10.Text = "0";
            TextBoxisadd11.Text = "0";
            TextBoxisadd13.Text = "-";
            TextBoxisadd14.Text = "-";
            TextBoxisadd16.Text = "-";
            TextBoxisadd17.Text = "-";

            DDLtip.ClearSelection();
            DDLtip.Items.FindByText("Gnr.Cargo").Selected = true;
            DDLflag.ClearSelection();
            DDLflag.Items.FindByText("UNKNOWN").Selected = true;

            TextBoxaddimo.BackColor = System.Drawing.Color.Yellow;
            TextBoxisadd1.BackColor = System.Drawing.Color.Yellow;
            DDLdestport.BackColor = System.Drawing.Color.Yellow;
            DDLdepberth.BackColor = System.Drawing.Color.Yellow;
            DDLdestberth.BackColor = System.Drawing.Color.Yellow;
        }
        this.ModalPopupyeniisekle.Show();
    }
    
    // Kayıt silme işlemi
    protected void Bacceptedok_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        int seciliidi = Convert.ToInt32(Bacceptedok.CommandArgument.ToString());

        if (CBdeleteis.Checked == true)
        {
            SqlCommand cmd = new SqlCommand("update isliste set nedurumda=@nedurumda, kayitzamani=@kayitzamani where id=" + seciliidi, baglanti);
            cmd.Parameters.AddWithValue("kayitzamani", "01.01.2016 00:01"); 
            cmd.Parameters.AddWithValue("nedurumda", "9"); //   silinen gemi dışarıda pozisyonuna alınıyor
            cmd.ExecuteNonQuery();
        }
        else
        {
            SqlCommand cmd = new SqlCommand("delete from isliste where id=" + seciliidi, baglanti);
            cmd.Parameters.AddWithValue("id", seciliidi);
            cmd.ExecuteNonQuery();
        }
        baglanti.Close();
        NewShipListc();
    }

    protected void LBonlineoff_Click(object sender, EventArgs e)
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
    protected void LBonline_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "9")
        {
            Response.Redirect("yonet/pilots.aspx");
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

    private bool IsDate2(string tarihyazi)
    {
        DateTime Temp;
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        if (DateTime.TryParse(tarihyazi, culture, System.Globalization.DateTimeStyles.AssumeLocal, out Temp) == true)
            return true;
        else
            return false;
    }

    private string Temizle(string Metin)
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








    protected void DDLpratika_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLpratika.SelectedItem.Text == "Yes")
        {
            TBpratikano.Visible = true;
            TBpratikano.Text = "";
        }
        else if (DDLpratika.SelectedItem.Text == "No")
        {
            TBpratikano.Visible = false;
            TBpratikano.Text = "";
        }
        this.ModalPopupyeniisekle.Show();
    }

    protected void Yeniisekleiptal_Click(object sender, EventArgs e)
    {
        DDLpratika.Items.FindByText("No").Selected = true;

    }

    protected void DDLJEpratika_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLJEpratika.SelectedItem.Text == "Yes")
        {
            TBJEpratikano.Visible = true;
            TBJEpratikano.Text = "";
        }
        else if (DDLJEpratika.SelectedItem.Text == "No")
        {
            TBJEpratikano.Visible = false;
            TBJEpratikano.Text = "";
        }
        this.ModalPopupExtenderjobedit.Show();
    }
}