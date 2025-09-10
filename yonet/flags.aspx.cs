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

public partial class yonet_flags : System.Web.UI.Page
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
        else if (Session["yetki"].ToString() == "9" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
        {
            if (Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2")
            {
                GridView1.Columns[5].Visible = false;
                GridView1.Columns[4].Visible = false;
                LBaddnewportberth.Visible = false;
                Litmenu1.Visible = false;
            }

            if (!IsPostBack)
                databagla();
        }

        else
        {
            Response.Redirect("../pmtr.aspx");
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
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from flaglist order by flag");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from flaglist order by flag");
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
        string flag = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).Text.Trim().ToString().ToLower()));
        string goster = (GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).Text;

        if (flag != "" && flag != null && goster != "" && goster != null)
        {
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmd = new SqlCommand("update flaglist set flag=@flag, goster=@goster where id=" + editportid, baglanti);
            cmd.Parameters.AddWithValue("flag", flag);
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
        SqlCommand cmd = new SqlCommand("Delete from flaglist where id= " + delportno, baglanti);
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
        SqlCommand cmd = new SqlCommand("SELECT max(id) FROM flaglist", baglanti);
        int sonlimanno = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        sonlimanno = sonlimanno + 1;
        baglanti.Close();

        TBpaddportno.Text = sonlimanno.ToString();

        TBpaddportname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddgoster.BorderColor = System.Drawing.Color.FromName("gray");

        TBpaddportname.Text = "";
        TBpaddgoster.Text = "";

        this.ModalPopupExtenderlbadd.Show();

        //GridView1.ShowFooter = true;
        //GridView1.Columns[6].Visible = false;
        //databagla();
    }
    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {

        string flag = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportname.Text).Trim().ToString().ToLower());
        string goster = TBpaddgoster.Text;

        if (flag == "" || flag == null)
        {
            TBpaddportname.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        if (goster == "" || goster == null)
        {
            TBpaddgoster.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        if (flag != "" && flag != null && goster != "" && goster != null)
        {
            bool sonuc = addport(flag, goster);

            if (sonuc)
            {
                GridView1.EditIndex = -1;
                databagla();
            }
        }
    }
    private bool addport(string flag, string goster)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("insert into flaglist (flag,goster) values(@flag,@goster)", baglanti);
        cmd.Parameters.AddWithValue("flag", flag);
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

    //protected void LBSubmit_Click(object sender, EventArgs e)
    //{
    //    SqlConnection baglanti = AnaKlas.baglan();
    //    SqlCommand cmd = new SqlCommand("Select * from flaglist where (flag like '%' + @search + '%')" , baglanti);
    //    cmd.Parameters.AddWithValue("@search", SqlDbType.NVarChar).Value=TBtextara.Text;

    //    cmd.ExecuteNonQuery();

    //    SqlDataAdapter da = new SqlDataAdapter();
    //    da.SelectCommand = cmd;
    //    DataSet ds = new DataSet();
    //    da.Fill(ds, "flag");
    //    GridView1.DataSource = ds;
    //    GridView1.DataBind();

    //    baglanti.Close();

    //}



}