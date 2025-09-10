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

public partial class yonet_tugs : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (cmdlogofbak.ExecuteScalar() == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["yetki"].ToString() == "9" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
        {
            if (Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
            {
                //GridView1.Columns[4].Visible = false;
                //GridView1.Columns[5].Visible = false;
                LBaddnewportberth.Visible = false;
                Litmenu1.Visible = false;
            }
            if (!IsPostBack)
                databagla();
        }

        else
        {
            Response.Redirect("http://www.monitoringpilot.com");

            //  Lblonline.Text = Session["kapno"].ToString() + ' ' + Session["kapadi"].ToString() + ' ' + Session["kapsoyadi"].ToString() + ' ' + Session["Yetki"].ToString();
        }

        if (Session["yetki"].ToString() == "9")
        {
            Litpagebaslik.Text = "PMTR Admin Page";
        }
        else
        { Litpagebaslik.Text = "PMTR User Page"; }
        baglanti.Close();
    }
    private void databagla()
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from tugs order by id");
        GridView1.DataSource = DTlrsetgrid;
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
        int deltug = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value); // tıklanan satır id sini alır
        Baccepted.CommandName = deltug.ToString();
        this.ModalPopuponayMessage.Show();
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
        SqlCommand cmd = new SqlCommand("Delete from tugs where id= " + delportno, baglanti);
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala
        databagla();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string boatname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).Text.Trim().ToString().ToLower()));
        string shortname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportshort") as TextBox).Text.Trim().ToString().ToLower()));
        string type = (GridView1.Rows[e.RowIndex].FindControl("TBeditporttype") as TextBox).Text;
        string tons = (GridView1.Rows[e.RowIndex].FindControl("TBeditporttons") as TextBox).Text;
        string bolge = (GridView1.Rows[e.RowIndex].FindControl("TBeditportbolge") as TextBox).Text;

        if (boatname != null && boatname != "" && shortname != null && shortname != "" && type != null && type != "" && tons != null && tons != "" && bolge != null && bolge != "")
           {
            SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("update tugs set boatname=@boatname, short=@short, type=@type, tons=@tons, bolge=@bolge  where id=" + id, baglanti);
        cmd.Parameters.AddWithValue("boatname", boatname);
        cmd.Parameters.AddWithValue("short", shortname.ToUpper());
        cmd.Parameters.AddWithValue("type", type);
        cmd.Parameters.AddWithValue("tons", tons);
        cmd.Parameters.AddWithValue("bolge", bolge);
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
        SqlCommand cmd = new SqlCommand("SELECT max(id) FROM tugs", baglanti);
        int sonlimanno = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        sonlimanno = sonlimanno + 1;
        baglanti.Close();

        TBpaddportno.Text = sonlimanno.ToString();

        TBpaddportname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddportshort.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddporttype.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddporttons.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddportbolge.BorderColor = System.Drawing.Color.FromName("gray");

        TBpaddportname.Text = "";
        TBpaddportshort.Text = "";
        TBpaddporttype.Text = "";
        TBpaddporttons.Text = "";
        TBpaddportbolge.Text = "";

        this.ModalPopupExtenderlbadd.Show();

        //GridView1.ShowFooter = true;
        //GridView1.Columns[6].Visible = false;
        //databagla();
    }
    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(TBpaddportno.Text);
        string TBname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportname.Text).Trim().ToString().ToLower());
        string TBshort = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportshort.Text).Trim().ToString().ToLower());
        string TBtype = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddporttype.Text).Trim().ToString().ToLower());
        string TBbolge = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportbolge.Text).Trim().ToString().ToLower());


        TBpaddportname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddportshort.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddporttype.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddporttons.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddportbolge.BorderColor = System.Drawing.Color.FromName("gray");



        if (TBname == "" || TBname == null)
        {
            TBpaddportname.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        if (TBshort == "" || TBshort == null)
        {
            TBpaddportshort.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBtype == "" || TBtype == null)
        {
            TBpaddporttype.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddporttons.Text == "" || TBpaddporttons.Text == null)
        {
            TBpaddporttons.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddportbolge.Text == "" || TBpaddportbolge.Text == null)
        {
            TBpaddportbolge.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        if (TBname != "" && TBname != null && TBshort != "" && TBshort != null && TBtype != "" && TBtype != null && TBpaddporttons.Text != ""  && TBpaddporttons.Text != null && TBpaddportbolge.Text != "" && TBpaddportbolge.Text != null)
        { 

        bool sonuc = addport(id, TBname, TBshort, TBtype, Convert.ToInt32(TBpaddporttons.Text), TBpaddportbolge.Text);
        if (sonuc)
        {
            GridView1.EditIndex = -1;
            databagla();
        }
        }
    }
    private bool addport(int id, string TBname, string TBshort, string TBtype, int TBtons, string TBbolge)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("insert into tugs (boatname,short,type,tons,bolge) values(@boatname,@short,@type,@tons,@bolge)", baglanti);
        cmd.Parameters.AddWithValue("boatname", TBname);
        cmd.Parameters.AddWithValue("short", TBshort.ToUpper());
        cmd.Parameters.AddWithValue("type", TBtype);
        cmd.Parameters.AddWithValue("tons", TBtons);
        cmd.Parameters.AddWithValue("bolge", TBbolge);

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
    protected void LBguvcik_Click(object sender, EventArgs e)
    {
        if ((Session["kapno"] == null ||  Session["kapno"].ToString() == "" ))
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
    protected void LBmainpage_Click(object sender, EventArgs e)
    {
        Response.Redirect("../main.aspx");
    }

}