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

public partial class yonet_arge : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"]==null || (Session["kapno"].ToString()=="")  || (Convert.ToInt32(Session["kapno"]) != 28  && Session["kapno"].ToString() != "135"))
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else
        {

            if (!IsPostBack)
            { databagla(); }
        }
        baglanti.Close();
    }

    private void databagla()
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from argem order by anketno");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from argem order by anketno");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
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
        int delportno = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value); // tıklanan satır id sini alır
        Baccepted.CommandName = delportno.ToString();
        this.ModalPopuponayMessage.Show();
    }
 
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala  id=  TBeditportlimanno.Text 
        databagla();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editportid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string anketadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).Text.Trim().ToString().ToLower()));
        string aciklama = (GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).Text.Trim();
        string secbir = (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec") as TextBox).Text.Trim();
        string seciki = (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec2") as TextBox).Text.Trim();
        string secuc = (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec3") as TextBox).Text.Trim();
        string aktif = (GridView1.Rows[e.RowIndex].FindControl("TBeditportsecaktif") as TextBox).Text.Trim();


        if (anketadi == "" || anketadi == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }

        else if (aciklama == "" || aciklama == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }

        else if (secbir == "" || secbir == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }
        else if (seciki == "" || seciki == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec2") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }
        else if (secuc == "" || secuc == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec3") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }
        else if (aktif == "" || aktif == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportsecaktif") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }
        else
        { 
 
        SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmd = new SqlCommand("update argem set anketadi=@anketadi, aciklama=@aciklama, secbir=@secbir, seciki=@seciki, secuc=@secuc, aktif=@aktif  where id=" + editportid, baglanti);
            cmd.Parameters.AddWithValue("anketadi", anketadi);
            cmd.Parameters.AddWithValue("aciklama", aciklama);
            cmd.Parameters.AddWithValue("secbir", secbir);
            cmd.Parameters.AddWithValue("seciki", seciki);
            cmd.Parameters.AddWithValue("secuc", secuc);
            cmd.Parameters.AddWithValue("aktif", aktif);

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

    protected void Baccepted_Click(object sender, EventArgs e)
    {
        int delportno = Convert.ToInt32(Baccepted.CommandName);
        bool sonuc = delport(delportno);
        if (sonuc)
        {
            databagla();
        }
    }
    private bool delport(int delportno)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("Delete from argem where id= " + delportno, baglanti);
        cmd.Parameters.AddWithValue("id", delportno);

        try
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            Baccepted.CommandName = "";
        }

        catch (SqlException ex)
        {
            string hata = ex.Message;
            Baccepted.CommandName = "";
        }
        finally
        {
            baglanti.Close();
        }
        return sonuc;
    }
    protected void LBaddnewportberth_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("SELECT max(anketno) FROM argem", baglanti);
        int anketno = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        anketno = anketno + 1;
        baglanti.Close();

        TBpaddportno.Text = anketno.ToString();

        TBpaddportname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddgoster.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddsec.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddsec2.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddsec3.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddsecaktif.BorderColor = System.Drawing.Color.FromName("gray");


        TBpaddportname.Text = "";
        TBpaddgoster.Text = "";
        TBpaddsec.Text = "";
        TBpaddsec2.Text = "";
        TBpaddsec3.Text = "";
        TBpaddsecaktif.Text = "";

        this.ModalPopupExtenderlbadd.Show();

        //GridView1.ShowFooter = true;
        //GridView1.Columns[6].Visible = false;
        //databagla();
    }
    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {
        string anketno = TBpaddportno.Text;
        string anketadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportname.Text).Trim().ToString().ToLower());
        string aciklama = TBpaddgoster.Text.Trim();
        string secbir = TBpaddsec.Text.Trim();
        string seciki = TBpaddsec2.Text.Trim();
        string secuc = TBpaddsec3.Text.Trim();
        string aktif = TBpaddsecaktif.Text.Trim();
        string ankettarihi = AnaKlas.TarihSaatYaziYapDMYhm(DateTime.Now);

        TBpaddportno.Text="";
        TBpaddportname.Text = "";
        TBpaddgoster.Text = "";
        TBpaddsec.Text = "";
        TBpaddsec2.Text = "";
        TBpaddsec3.Text = "";
        TBpaddsecaktif.Text = "";

        if (anketadi == "" || anketadi == null )
        {
            TBpaddportname.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        else if (aciklama == "" || aciklama == null)
        {
            TBpaddgoster.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        else if (secbir == "" || TBpaddsec == null)
        {
            TBpaddsec.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        else if (seciki == "" || TBpaddsec2 == null)
        {
            TBpaddsec2.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        else if (secuc == "" || TBpaddsec3 == null)
        {
            TBpaddsec3.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        else if (aktif == "" || aktif == null)
        {
            TBpaddsecaktif.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        else {
            bool sonuc = addport(anketno, anketadi, aciklama, secbir,seciki,secuc, ankettarihi, aktif);

            if (sonuc)
            {
                SqlConnection baglanti = AnaKlas.baglan();
                SqlConnection baglanti2 = AnaKlas.baglan();
                SqlCommand cmdoku = new SqlCommand("select * from argedetay where tamliste = '1' ", baglanti);
                SqlDataReader dr = cmdoku.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        SqlCommand cmdkapnoa = new SqlCommand("insert into argedetay (tamliste,kapno,varid,kapadi,kapsoyadi,anketno,secenek,sectarihi,onay) values (@tamliste,@kapno,@varid,@kapadi,@kapsoyadi,@anketno,@secenek,@sectarihi,@onay)", baglanti2);
                        cmdkapnoa.Parameters.AddWithValue("tamliste", "0");
                        cmdkapnoa.Parameters.AddWithValue("kapno", dr["kapno"]);
                        cmdkapnoa.Parameters.AddWithValue("varid", dr["varid"]);
                        cmdkapnoa.Parameters.AddWithValue("kapadi", dr["kapadi"]);
                        cmdkapnoa.Parameters.AddWithValue("kapsoyadi", dr["kapsoyadi"]);
                        cmdkapnoa.Parameters.AddWithValue("anketno", (anketno));
                        cmdkapnoa.Parameters.AddWithValue("secenek", "");
                        cmdkapnoa.Parameters.AddWithValue("sectarihi", "");
                        cmdkapnoa.Parameters.AddWithValue("onay", 0);
                        cmdkapnoa.ExecuteNonQuery();
                        cmdkapnoa.Dispose();
                    }
                }
                dr.Close();

                cmdoku.Dispose();
                baglanti.Close();
                baglanti2.Close();
                GridView1.EditIndex = -1;
                databagla();
            }
        }
    }
    private bool addport (string anketno, string anketadi, string aciklama, string secbir, string seciki, string secuc, string ankettarihi, string aktif )
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("insert into argem (anketno,anketadi,aciklama,secbir,seciki,secuc,ankettarihi,aktif) values(@anketno,@anketadi,@aciklama,@secbir,@seciki,@secuc,@ankettarihi,@aktif)", baglanti);
        cmd.Parameters.AddWithValue("anketno", anketno);
        cmd.Parameters.AddWithValue("anketadi", anketadi);
        cmd.Parameters.AddWithValue("aciklama", aciklama);
        cmd.Parameters.AddWithValue("secbir", secbir);
        cmd.Parameters.AddWithValue("seciki", seciki);
        cmd.Parameters.AddWithValue("secuc", secuc);
        cmd.Parameters.AddWithValue("ankettarihi", ankettarihi);
        cmd.Parameters.AddWithValue("aktif", aktif);

        try
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            //oy kullanmayan hakkını kaybetti
            SqlCommand cmdu = new SqlCommand("update argedetay set onay=@onay where onay='0' ", baglanti);
            cmdu.Parameters.AddWithValue("onay","1" );
            cmdu.ExecuteNonQuery();

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
    protected void LBguvcik_Click(object sender, EventArgs e)
    {
        if (Session["kapno"].ToString() == "" || (Session["kapno"] == null))
        {
            Response.Redirect("http://www.monitoringpilot.com");
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



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "linkle")
        {
            string sayfa = "../" + (GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Litemportgoster") as Label).Text;
            Response.Redirect(sayfa);
        }
    }
}