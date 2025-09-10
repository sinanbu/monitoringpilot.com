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

public partial class yonet_distances : System.Web.UI.Page
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
                Litpagebaslik.Text = "PMTR User Page"; 
                GridView1.Columns[6].Visible = false;

                Litmenu1.Visible = false;
                if (Session["yetki"].ToString() == "0")
                {
                    Litmenu4.Visible = false;
                    Litmenu5.Visible = false;
                    Litmenu6.Visible = false;
                }
            }

            else { Litpagebaslik.Text = "PMTR Admin Page"; }

            if (!IsPostBack)
            { databaglailk(); }
        }

        else
        {
            Response.Redirect("../pmtr.aspx");
        }
        baglanti.Close();
    }
    private void databaglailk()
    {
        DataTable DTlimanlar = AnaKlas.GetDataTable("Select distinct kalkisliman from limanmesafe order by kalkisliman");
            DropDownListports.Items.Clear();
            DropDownListports.DataTextField = "kalkisliman";
            DropDownListports.DataSource = DTlimanlar;
            DropDownListports.DataBind();

            DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from limanmesafe where kalkisliman = '" + DropDownListports.SelectedItem.Text + "' order by varisliman");
            GridView1.DataSource = DTlrsetgrid;
            GridView1.DataBind();
        }

    private void databagla()
        {
            string hatirla = DropDownListports.SelectedItem.Text.ToString();
            DataTable DTlimanlar = AnaKlas.GetDataTable("Select distinct kalkisliman from limanmesafe order by kalkisliman");
            DropDownListports.Items.Clear();
          //  DropDownListports.DataValueField = "id";
            DropDownListports.DataTextField = "kalkisliman";
            DropDownListports.DataSource = DTlimanlar;
            DropDownListports.DataBind();
            DropDownListports.Items.FindByText(HttpUtility.HtmlDecode(hatirla)).Selected = true;

            DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from limanmesafe where kalkisliman = '" + DropDownListports.SelectedItem.Text + "'  order by varisliman");
            GridView1.DataSource = DTlrsetgrid;
            GridView1.DataBind();
        }


    protected void DropDownListports_SelectedIndexChanged(object sender, EventArgs e)
    {
        string seciliport = DropDownListports.SelectedItem.Text;
        DataTable DTrihtim = AnaKlas.GetDataTable("Select * from limanmesafe where kalkisliman = '" + seciliport + "' order by varisliman asc");
        GridView1.DataSource = DTrihtim;
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


    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala
        databagla();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
         int editportid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string limanbolge = (GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanbolge") as TextBox).Text;

        if (limanbolge != "" && limanbolge != null)
        {
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmd = new SqlCommand("update limanmesafe set arasure=@arasure where id=" + editportid, baglanti);
            cmd.Parameters.AddWithValue("arasure", limanbolge);

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



}