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

public partial class yonet_vacations : System.Web.UI.Page
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
        else if (Session["yetki"].ToString() != "9")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (cmdlogofbak.ExecuteScalar() == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }

        else
        {
        
            if (Session["yetki"].ToString() != "9")
            {
            Litpagebaslik.Text = "PMTR User Page";
            GridView1.Columns[5].Visible = false;
            }
            else
            { Litpagebaslik.Text = "PMTR Admin Page"; }

            if (!IsPostBack)
                databagla();
        }




        cmdlogofbak.Dispose();

        baglanti.Close();
    }

    private void databagla()
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select kapadi +' '+ kapsoyadi as kapadisoyadi, kapno, kapsirano, izinbasla, izinbit from pilotlar where kapsirano>0 and kapsirano<1000 and emekli='No' order by kapadisoyadi asc");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.DataBind();
        LBistna.Visible = false;
        LBistda.Visible = true;

    }

    private void databagladate()
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select kapadi +' '+ kapsoyadi as kapadisoyadi, kapno, kapsirano, izinbasla, Convert(date,izinbasla,104) as basla, izinbit from pilotlar where kapsirano>0 and kapsirano<1000  and  emekli='No'  order by basla asc, kapadisoyadi asc");
        GridView1.DataSource = DTlrsetgrid;
        GridView1.DataBind();
        LBistda.Visible = false;
        LBistna.Visible = true;

    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView1.SelectedIndex)
        {
            e.Cancel = true;
            GridView1.SelectedIndex = -1;
            if (LBistna.Visible == false)
            { databagla(); }
            else
            { databagladate(); }
            
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala  id=  TBeditportlimanno.Text 
        if (LBistna.Visible == false)
        { databagla(); }
        else
        { databagladate(); }
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editkapno = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string start = (GridView1.Rows[e.RowIndex].FindControl("TBeditstarti") as TextBox).Text; 
        string finish = (GridView1.Rows[e.RowIndex].FindControl("TBeditfinishi") as TextBox).Text;

        if (start == "__.__.____" || finish == "__.__.____" )
        {
            start = "";
            finish = "";
            GridView1.EditIndex = -1;
            if (LBistna.Visible == false)
            { databagla(); }
            else
            { databagladate(); }
        }

        if (start != "" && start != null && finish != "" && finish != null && AnaKlas.IsDate2(start) == true && AnaKlas.IsDate2(finish) == true)
        {
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmd = new SqlCommand("update pilotlar set izinbasla=@izinbasla, izinbit=@izinbit where kapno=" + editkapno, baglanti);
            cmd.Parameters.AddWithValue("izinbasla", start);
            cmd.Parameters.AddWithValue("izinbit", finish);

            cmd.ExecuteNonQuery();
            baglanti.Close();

            GridView1.EditIndex = -1;
            if (LBistna.Visible == false)
            { databagla(); }
            else
            { databagladate(); }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        if (LBistna.Visible == false)
        { databagla(); }
        else
        { databagladate(); }
    }

 
    protected void LBistda_Click(object sender, EventArgs e)
    {
        GridView1.Dispose();
        databagladate();
    }
    protected void LBistna_Click(object sender, EventArgs e)
    {
        GridView1.Dispose();
        databagla();
    }

    protected void LBguvcik_Click(object sender, EventArgs e)
    {
        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
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






}