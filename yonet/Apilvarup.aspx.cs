using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using AjaxControlToolkit;
using System.Globalization;


public partial class Apilvarup : System.Web.UI.Page
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
       else  //if (cmdlogofbak.ExecuteScalar() != null)
        {

            SqlCommand seskapadi = new SqlCommand("SP_seskapadi", baglanti);
            seskapadi.CommandType = CommandType.StoredProcedure;
            seskapadi.Parameters.AddWithValue("@kapno", Session["kapno"].ToString());
            seskapadi.Parameters.Add("@kapadisonuc", SqlDbType.NVarChar, 30);
            seskapadi.Parameters["@kapadisonuc"].Direction = ParameterDirection.Output;
            seskapadi.ExecuteNonQuery();
            string seskapadis = seskapadi.Parameters["@kapadisonuc"].Value.ToString().Trim();
            seskapadi.Dispose();

            SqlCommand seskapsoyadi = new SqlCommand("SP_seskapsoyadi", baglanti);
            seskapsoyadi.CommandType = CommandType.StoredProcedure;
            seskapsoyadi.Parameters.AddWithValue("@kapno", Session["kapno"].ToString());
            seskapsoyadi.Parameters.Add("@kapsoyadisonuc", SqlDbType.NVarChar, 30);
            seskapsoyadi.Parameters["@kapsoyadisonuc"].Direction = ParameterDirection.Output;
            seskapsoyadi.ExecuteNonQuery();
            string seskapsoyadis = seskapsoyadi.Parameters["@kapsoyadisonuc"].Value.ToString().Trim();
            seskapsoyadi.Dispose();

            LBonline.Text = seskapadis + " " + seskapsoyadis; // +Session.Timeout.ToString();

            if (!IsPostBack)
            {            
                databagla();
            }
        }

        baglanti.Close();

    }


    private void databagla()
    {
        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri2 = from b2 in entikib.vardiyadetay.Where(b2 => b2.grt.Contains("99999999")) select b2;
            GridView1.DataSource = veri2.ToList();
            GridView1.DataBind();
        }

        //if (DDLPilots.SelectedItem.Text!="") DDLPilots.Items.FindByText(DDLPilots.SelectedItem.Text).Selected = true;

        DataTable DTkaplar = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where  kapsirano<1000 order by kapadisoyadi asc");
        DDLPilots.Items.Clear();
        DDLPilots.DataTextField = "kapadisoyadi";
        DDLPilots.DataValueField = "kapno";
        DDLPilots.DataSource = DTkaplar;
        DDLPilots.DataBind();
        DDLPilots.Items.Insert(0, new ListItem("Select", String.Empty));
        DDLPilots.SelectedIndex = 0;

    }


    private void sirala()
    {
        int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());


        using (PilotdbEntities pilvar = new PilotdbEntities())
        {
            pilvar.Configuration.ProxyCreationEnabled = false;

            var veri = from b in pilvar.pilotvardiyas.Where(b => b.kapno == kapnoal).ToList() select b;

            GridView1.DataSource = veri.OrderByDescending(b => b.varno).ToList().Take(20);
            GridView1.DataBind();


        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView1.SelectedIndex)
        {
            e.Cancel = true;
            GridView1.SelectedIndex = -1;
        }

    }

   
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala
        sirala();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editisid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string toplamissayisi = (GridView1.Rows[e.RowIndex].FindControl("TBciktime") as TextBox).Text;
        string toplamissuresi = (GridView1.Rows[e.RowIndex].FindControl("TBpobtime") as TextBox).Text;
        string toplamdinlenme = (GridView1.Rows[e.RowIndex].FindControl("TBpofftime") as TextBox).Text;
        string yorulma = (GridView1.Rows[e.RowIndex].FindControl("TBontime") as TextBox).Text;
        string yorulmalast = (GridView1.Rows[e.RowIndex].FindControl("TBiptal") as TextBox).Text;

        if (string.IsNullOrEmpty(toplamissayisi) != true && string.IsNullOrEmpty(toplamissuresi) != true && string.IsNullOrEmpty(toplamdinlenme) != true && string.IsNullOrEmpty(yorulma) != true && string.IsNullOrEmpty(yorulmalast) != true)
        {
            int toplamissayisii = Convert.ToInt32(toplamissayisi);
            decimal toplamissuresid = Convert.ToDecimal(toplamissuresi);
            decimal toplamdinlenmed = Convert.ToDecimal(toplamdinlenme);
            decimal yorulmad = Convert.ToDecimal(yorulma);
            decimal yorulmalastd = Convert.ToDecimal(yorulmalast);

                            SqlConnection baglanti = AnaKlas.baglan();
                            SqlCommand cmd = new SqlCommand("update pilotvardiya set toplamissayisi=@toplamissayisi,toplamissuresi=@toplamissuresi,toplamdinlenme=@toplamdinlenme,yorulma=@yorulma,yorulmalast=@yorulmalast where id=" + editisid, baglanti);
                            cmd.Parameters.AddWithValue("toplamissayisi", toplamissayisii);
                            cmd.Parameters.AddWithValue("toplamissuresi", toplamissuresid);
                            cmd.Parameters.AddWithValue("toplamdinlenme", toplamdinlenmed);
                            cmd.Parameters.AddWithValue("yorulma", yorulmad);
                            cmd.Parameters.AddWithValue("yorulmalast", yorulmalastd);

                            cmd.ExecuteNonQuery();

                            baglanti.Close();

                            GridView1.EditIndex = -1;
                            sirala();
             
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        sirala();
    }



    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("../main.aspx");
    }
    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"] == "" || (Session["kapno"] == null))
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
    protected void LBonline_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["yetki"]) ==1 || Convert.ToInt32(Session["yetki"]) ==2)
        {
  //          Response.Redirect("pmtr.aspx");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 0 || Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("pilots.aspx");
        }
    }



    protected void DDLPilots_SelectedIndexChanged(object sender, EventArgs e)
    {
        sirala();
    }


    protected void adminsayfalar_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminsayfalar.aspx");
    }
}




