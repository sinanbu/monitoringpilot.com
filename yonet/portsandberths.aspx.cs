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

public partial class yonet_portsandberths : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == "" || (Session["kapno"] == null) || cmdlogofbak.ExecuteScalar() == null)
        {
            Response.Redirect("../pmtr.aspx");
        }
        else if (Session["yetki"].ToString() == "9" || Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
        {
            if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
            {
                //for (int i = 0; i < GridView1.Rows.Count; i++)
                //{
                //    GridView1.Rows[i].Cells[10].FindControl("IBPilotEdit").Visible = false;
                //}
                Litpagebaslik.Text = "PMTR User Page";

                GridView1.Columns[10].Visible = false;
                GridView1.Columns[11].Visible = false;
                LBaddnewportberth.Visible = false;

                Litmenu1.Visible = false;
                if (Session["yetki"].ToString() == "0")
                {
                    Litmenu4.Visible = false;
                    Litmenu5.Visible = false;
                    Litmenu6.Visible = false;
                }
            }

            else {
                Litpagebaslik.Text = "PMTR Admin Page";
            }

            if (!IsPostBack)
               { databaglailk();}
        }

        else
        {
            Response.Redirect("../pmtr.aspx");
        }
        baglanti.Close();
    }
    private void databaglailk()
    {
        DataTable DTlimanlar = AnaKlas.GetDataTable("Select distinct limanadi, limanno  from limanlar where limanno<1000 order by limanadi");
        DropDownListports.Items.Clear();
        DropDownListports.DataValueField = "limanno";
        DropDownListports.DataTextField = "limanadi";
        DropDownListports.DataSource = DTlimanlar;
        DropDownListports.DataBind();

        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from limanlar where limanadi = '" + DropDownListports.SelectedItem.Text + "' and limanno<1000  order by rihtimadi");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.DataBind();
    }
    private void databagla()
    {
        string hatirla = DropDownListports.SelectedItem.Text.ToString();
        DataTable DTlimanlar = AnaKlas.GetDataTable("Select distinct limanadi, limanno  from limanlar where limanno<1000 order by limanadi");
        DropDownListports.Items.Clear();
        DropDownListports.DataValueField = "limanno";
        DropDownListports.DataTextField = "limanadi";
        DropDownListports.DataSource = DTlimanlar;
        DropDownListports.DataBind();
        DropDownListports.Items.FindByText(HttpUtility.HtmlDecode(hatirla)).Selected = true;

        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from limanlar where limanadi = '" + DropDownListports.SelectedItem.Text + "' and limanno<1000  order by rihtimadi");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.DataBind();
    }



    protected void DropDownListports_SelectedIndexChanged(object sender, EventArgs e)
    {
        string seciliport = DropDownListports.SelectedItem.Text;
        DataTable DTrihtim = AnaKlas.GetDataTable("Select * from limanlar where limanadi = '" + seciliport + "' order by rihtimadi asc");
        GridView1.DataSource = DTrihtim;
        GridView1.DataBind();


        string dogruad = "";
        dogruad = seciliport.ToLower();
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
        else if (dogruad == "gubretas" || dogruad == "turkuaz" || dogruad == "marmaratersanesi" || dogruad == "rota" ) { dogruad = "gubreturkirrota"; }
        else if (dogruad == "shell" || dogruad == "koruma" || dogruad == "aktas") { dogruad = "shelkorumaaktas"; }
        else if (dogruad == "efesan" || dogruad == "total"  || dogruad == "guzelenerji" ) { dogruad = "efesantotal"; }
		else if (dogruad == "tersanebayrak" || dogruad == "topcularferibot" ) { dogruad = "topcubayrak"; }
		else if (dogruad == "tersanegemak" || dogruad == "tersanekocatepe" )  { dogruad = "gemakkocatepe"; }
		else if (dogruad == "tersanehatsan"|| dogruad == "tersaneyasarsan"|| dogruad == "tersanekalkavan" )  { dogruad = "hatyasakalk"; }
		else if (dogruad == "tersanemardas"|| dogruad == "tersaneseltas"|| dogruad == "tersaneduzgit"|| dogruad == "tersanedenta"|| dogruad == "tersanefurtrans"|| dogruad == "tersanedogusan" )  { dogruad = "kalktobes"; }
        else if (dogruad == "tersanehurriyet" || dogruad == "tersanegisan" || dogruad == "tersaneaykin" || dogruad == "tersaneorucoglu" || dogruad == "tersanegurdesan" ) { dogruad = "hurtogur"; }
        else if (dogruad == "tersanehercelik" || dogruad == "tersaneozlemdeniz"  || dogruad == "tersaneyuksel"|| dogruad == "tersanekurbangemi"|| dogruad == "tersaneturkoglu"|| dogruad == "tersanebreko"|| dogruad == "tersanebogazici"|| dogruad == "tersanepalmali"|| dogruad == "tersaneak"|| dogruad == "tersanealtintas" ) { dogruad = "altocemre"; } 
        else if (dogruad.Substring(0, 3) == "kos"){ dogruad = "kosbas"; }		 


		
        Image1.ImageUrl="../images/limanplan/" + dogruad + ".jpg";
        Image1.Width=747;

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView1.SelectedIndex)
        {
            e.Cancel = true;
            GridView1.SelectedIndex = -1;
            databagla();
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int delportno = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value); // tıklanan satır is alır
        Baccepted.CommandName = delportno.ToString();
        this.ModalPopuponayMessage.Show();
    }
    protected void Baccepted_Click(object sender, EventArgs e)
    {
        int delportno = Convert.ToInt32(Baccepted.CommandName);
        bool sonuc = delport(delportno);
        if (sonuc)
        {
            databaglailk();
        }
    }
    private bool delport(int delportno)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("Delete from limanlar where id= " + delportno, baglanti);
        cmd.Parameters.AddWithValue("id", delportno);

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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala
        databagla();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editportid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        int limanno = Convert.ToInt32((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanno") as TextBox).Text);
        string limanadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).Text.Trim().ToString().ToLower()));
        string rihtimadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportrihtimadi") as TextBox).Text.Trim().ToString().ToLower()));
        string limanbolge = (GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanbolge") as TextBox).Text;
        string zorluk = (GridView1.Rows[e.RowIndex].FindControl("TBeditportzorluk") as TextBox).Text;
        string bagliistasyon = (GridView1.Rows[e.RowIndex].FindControl("TBeditportresp") as TextBox).Text;
        string yanasmasuresis = (GridView1.Rows[e.RowIndex].FindControl("TBeditportyanasmasuresi") as TextBox).Text;
        string kalkissuresis = (GridView1.Rows[e.RowIndex].FindControl("TBeditportkalkissuresi") as TextBox).Text;
        string goster = (GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).Text;

        if (limanadi != "" && limanadi != null && rihtimadi != "" && rihtimadi != null && limanbolge != "" && limanbolge != null && zorluk != "" && zorluk != null && bagliistasyon != "" && bagliistasyon != null && yanasmasuresis != "" && yanasmasuresis != null && kalkissuresis != "" && kalkissuresis != null && goster != "" && goster != null)
        {
        int yanasmasuresi = Convert.ToInt32(yanasmasuresis);
        int kalkissuresi = Convert.ToInt32(kalkissuresis);

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("update limanlar set limanno=@limanno, limanadi=@limanadi,rihtimadi=@rihtimadi,limanbolge=@limanbolge,zorluk=@zorluk,bagliistasyon=@bagliistasyon,yanasmasuresi=@yanasmasuresi,kalkissuresi=@kalkissuresi,goster=@goster where id=" + editportid, baglanti);
        cmd.Parameters.AddWithValue("limanno", limanno);
        cmd.Parameters.AddWithValue("limanadi", limanadi);
        cmd.Parameters.AddWithValue("rihtimadi", rihtimadi);
        cmd.Parameters.AddWithValue("limanbolge", limanbolge);
        cmd.Parameters.AddWithValue("zorluk", zorluk);
        cmd.Parameters.AddWithValue("bagliistasyon", bagliistasyon);
        cmd.Parameters.AddWithValue("yanasmasuresi", yanasmasuresi);
        cmd.Parameters.AddWithValue("kalkissuresi", kalkissuresi);
        cmd.Parameters.AddWithValue("goster", goster);

        cmd.ExecuteNonQuery();
        baglanti.Close();

        GridView1.EditIndex = -1;
        databagla();
    }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        databagla();
    }
    protected void LBaddnewportberth_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("SELECT max(limanno) FROM limanlar where limanno<700", baglanti);
        int sonlimanno = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        sonlimanno = sonlimanno + 1;
        baglanti.Close();

        TBpaddportno.Text = sonlimanno.ToString();
        TBpaddportno.Enabled = false;
        LBaddonlyport.ForeColor = System.Drawing.Color.FromName("gray");
        LBaddonlyport.Enabled = false;
        LBaddonlyberth.Enabled = true;

        DataTable DTlimanlar = AnaKlas.GetDataTable("Select distinct limanadi, limanno  from limanlar where limanno<1000 order by limanadi");
        DDLSelectexPort.Items.Clear();
        DDLSelectexPort.DataValueField = "limanno";
        DDLSelectexPort.DataTextField = "limanadi";
        DDLSelectexPort.DataSource = DTlimanlar;
        DDLSelectexPort.DataBind();

        TBpaddportname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddrname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddzorluk.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddresp.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddyanasmasuresi.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddkalkissuresi.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddgoster.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddportarea.BorderColor = System.Drawing.Color.FromName("gray");


        TBpaddportname.Text = "";
        TBpaddrname.Text = "";
        TBpaddzorluk.Text = "";
        TBpaddresp.Text = "";
        TBpaddyanasmasuresi.Text = "";
        TBpaddkalkissuresi.Text = "";
        TBpaddgoster.Text = "";
        TBpaddportarea.Text = "";


        this.ModalPopupExtenderlbadd.Show();
    }
    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {

        string limanadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportname.Text.Trim().ToString().ToLower()));
        string rihtimadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddrname.Text.Trim().ToString().ToLower()));

        if (limanadi == "" || limanadi == null)
        {
            TBpaddportname.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (rihtimadi == "" || rihtimadi == null)
        {
            TBpaddrname.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddyanasmasuresi.Text == "" || TBpaddyanasmasuresi.Text == null)
        {
            TBpaddyanasmasuresi.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddkalkissuresi.Text == "" || TBpaddkalkissuresi.Text == null)
        {
            TBpaddkalkissuresi.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddgoster.Text == "" || TBpaddgoster.Text == null)
        {
            TBpaddgoster.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddportarea.Text == "" || TBpaddportarea.Text == null)
        {
            TBpaddportarea.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        if (TBpaddzorluk.Text == "" || TBpaddzorluk.Text == null)
        {
            TBpaddzorluk.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddresp.Text == "" || TBpaddresp.Text == null)
        {
            TBpaddresp.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }



        if (limanadi != "" && limanadi != null && rihtimadi != "" && rihtimadi != null && TBpaddyanasmasuresi.Text != "" && TBpaddyanasmasuresi.Text != null && TBpaddkalkissuresi.Text != "" && TBpaddkalkissuresi.Text != null && TBpaddgoster.Text != "" && TBpaddgoster.Text != null && TBpaddresp.Text != "" && TBpaddresp.Text != null && TBpaddzorluk.Text != "" && TBpaddzorluk.Text != null && TBpaddportarea.Text != "" && TBpaddportarea.Text != null)
        {
        
        int sonlimanno = Convert.ToInt32(TBpaddportno.Text);
        string limanbolge = TBpaddportarea.Text;
        string zorluk = TBpaddzorluk.Text;
        string bagliistasyon = TBpaddresp.Text;
        int yanasmasuresi = Convert.ToInt32(TBpaddyanasmasuresi.Text);
        int kalkissuresi = Convert.ToInt32(TBpaddkalkissuresi.Text);
        string goster = TBpaddgoster.Text;


        bool sonuc = addport(sonlimanno, limanadi, rihtimadi, limanbolge, zorluk, bagliistasyon, yanasmasuresi, kalkissuresi, goster);

        if (sonuc)
        {
            GridView1.EditIndex = -1;
            GridView1.ShowFooter = false;
            databagla();
        }
    }
    }
    private bool addport(int limanno, string limanadi, string rihtimadi, string limanbolge, string zorluk, string bagliistasyon, int yanasmasuresi, int kalkissuresi, string goster)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("insert into limanlar (limanno,limanadi,rihtimadi,limanbolge,zorluk,bagliistasyon,yanasmasuresi,kalkissuresi,goster) values(@limanno,@limanadi,@rihtimadi,@limanbolge,@zorluk,@bagliistasyon,@yanasmasuresi,@kalkissuresi,@goster)", baglanti);
        cmd.Parameters.AddWithValue("limanno", limanno);
        cmd.Parameters.AddWithValue("limanadi", limanadi);
        cmd.Parameters.AddWithValue("rihtimadi", rihtimadi);
        cmd.Parameters.AddWithValue("limanbolge", limanbolge);
        cmd.Parameters.AddWithValue("zorluk", zorluk);
        cmd.Parameters.AddWithValue("bagliistasyon", bagliistasyon);
        cmd.Parameters.AddWithValue("yanasmasuresi", yanasmasuresi);
        cmd.Parameters.AddWithValue("kalkissuresi", kalkissuresi);
        cmd.Parameters.AddWithValue("goster", goster);

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
    protected void LBaddonlyberth_Click(object sender, EventArgs e)
    {
        DDLSelectexPort.Visible = true;
        TBpaddportname.Visible = false;
        LBaddonlyberth.ForeColor = System.Drawing.Color.FromName("gray");
        LBaddonlyberth.Enabled = false;
        LBaddonlyport.ForeColor = System.Drawing.Color.FromName("blue");
        LBaddonlyport.Enabled = true;
        TBpaddportname.Text = DDLSelectexPort.SelectedItem.Text.ToString();
        TBpaddportno.Text = DDLSelectexPort.SelectedValue;
        this.ModalPopupExtenderlbadd.Show();
    }
    protected void LBaddonlyport_Click(object sender, EventArgs e)
    {
        TBpaddportname.Visible = true;
        DDLSelectexPort.Visible = false;
        LBaddonlyport.ForeColor = System.Drawing.Color.FromName("gray");
        LBaddonlyport.Enabled = false;
        LBaddonlyberth.ForeColor = System.Drawing.Color.FromName("blue");
        LBaddonlyberth.Enabled = true;
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("SELECT max(limanno) FROM limanlar where limanno<700", baglanti);
        int sonlimanno = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        sonlimanno = sonlimanno + 1;
        baglanti.Close();

        TBpaddportno.Text = sonlimanno.ToString();
        TBpaddportno.Enabled = false;
        this.ModalPopupExtenderlbadd.Show();
    }
    protected void DDLSelectexPort_SelectedIndexChanged(object sender, EventArgs e)
    {
        TBpaddportno.Text = DDLSelectexPort.SelectedValue.ToString();
        TBpaddportname.Text = DDLSelectexPort.SelectedItem.Text.ToString();
        this.ModalPopupExtenderlbadd.Show();
    }
    protected void Buttonlbaddcancel_Click(object sender, EventArgs e)
    {
        ModalPopupExtenderlbadd.Controls.Clear();
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
    protected void LBmainpage_Click(object sender, EventArgs e)
    {
        Response.Redirect("../main.aspx");
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
}