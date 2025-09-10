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

public partial class yonet_adminsayfalar : System.Web.UI.Page
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
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from sayfayonet order by section");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from sayfayonet order by section");
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
        string aciklama = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).Text.Trim().ToString().ToLower()));
        string sayfa = (GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).Text;
        string section = (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec") as TextBox).Text;


        if (aciklama == "" || aciklama == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
           
        }

        else if (sayfa == "" || sayfa == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }

        else if (section == "" || section == null)
        {
            (GridView1.Rows[e.RowIndex].FindControl("TBeditportsec") as TextBox).BorderColor = System.Drawing.Color.FromName("red");
        }

        else
        { 
 
        SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmd = new SqlCommand("update sayfayonet set aciklama=@aciklama, sayfa=@sayfa, section=@section  where id=" + editportid, baglanti);
            cmd.Parameters.AddWithValue("aciklama", aciklama);
            cmd.Parameters.AddWithValue("sayfa", sayfa);
            cmd.Parameters.AddWithValue("section", section);

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
        SqlCommand cmd = new SqlCommand("Delete from sayfayonet where id= " + delportno, baglanti);
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
        SqlCommand cmd = new SqlCommand("SELECT max(id) FROM sayfayonet", baglanti);
        int sonlimanno = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        sonlimanno = sonlimanno + 1;
        baglanti.Close();

        TBpaddportno.Text = sonlimanno.ToString();

        TBpaddportname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddgoster.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddsec.BorderColor = System.Drawing.Color.FromName("gray");

        TBpaddportname.Text = "";
        TBpaddgoster.Text = "";
        TBpaddsec.Text = "";

        this.ModalPopupExtenderlbadd.Show();

        //GridView1.ShowFooter = true;
        //GridView1.Columns[6].Visible = false;
        //databagla();
    }
    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {

        string aciklama = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportname.Text).Trim().ToString().ToLower());
        string sayfa = TBpaddgoster.Text;
        string section = TBpaddsec.Text;

        if (aciklama == "" || aciklama == null )
        {
            TBpaddportname.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        else if (sayfa == "" || sayfa == null)
        {
            TBpaddgoster.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        else if (section == "" || section == null)
        {
            TBpaddsec.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

       else {
            bool sonuc = addport(aciklama, sayfa,section);

            if (sonuc)
            {
                GridView1.EditIndex = -1;
                databagla();
            }
        }
    }
    private bool addport(string aciklama, string sayfa, string section)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("insert into sayfayonet (aciklama,sayfa,section) values(@aciklama,@sayfa,@section)", baglanti);
        cmd.Parameters.AddWithValue("aciklama", aciklama);
        cmd.Parameters.AddWithValue("sayfa", sayfa);
        cmd.Parameters.AddWithValue("section", section);

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



    protected void Litemportlimanadi_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "linkle")
        {
            string sayfa = (GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Litemportgoster") as Label).Text;
            Response.Redirect(sayfa);
        }
    }
}