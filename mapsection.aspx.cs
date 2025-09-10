using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Text;


public partial class mapsection : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {   

        SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);


        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Convert.ToInt32(Session["kapno"]) < 0 || Convert.ToInt32(Session["kapno"]) > 999)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "99")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else  //if (cmdlogofbak.ExecuteScalar() != null)
        {
            if (!IsPostBack)
            {   
            if (Session["yetki"].ToString() == "9")
            { LBonline.Enabled = true; }
            else { LBonline.Enabled = false; }


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

                LiteralYaz();

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString(); 
            }

            if (Session["yetki"].ToString() == "0")
            {
                mainmanu2.Visible = false;
                mainmanu3.Visible = false;
                mainmanu4.Visible = false;
                mainmanu6.Visible = false;
            }

            if (Session["kapno"].ToString() == "1" || Session["kapno"].ToString() == "47" || Session["kapno"].ToString() == "52" || Session["kapno"].ToString() == "95" || Session["kapno"].ToString() == "92" || Session["kapno"].ToString() == "94" || Session["kapno"].ToString() == "97" || Session["kapno"].ToString() == "98")
            {
                mainmanu2.Visible = true;
                mainmanu3.Visible = true;
                mainmanu6.Visible = true;
            }
            else if (Session["kapno"].ToString() == "96")
            {
                mainmanu2.Visible = true;
            }


            DTloading();
        }

        baglanti.Close();

    }

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        if (varbiter.Text != "")
        {
            if (AnaKlas.TarihSaatYapDMYhm(varbiter.Text) > (DateTime.Now))
            {
                decimal gzsaatd = AnaKlas.varrealtimesaat();
                int gzsaat = Convert.ToInt32(Math.Floor(Math.Abs(gzsaatd)));
                decimal gzdakikad = (gzsaatd - gzsaat) * 60;
                int gzdakika = Convert.ToInt32(Math.Floor(Math.Abs(gzdakikad)));
                string gzdakikas = gzdakika.ToString();
                if (gzdakika < 10) { gzdakikas= "0" + gzdakikas; }
                decimal gzsanid = (gzdakikad - gzdakika) * 60;
                int gzsani = Convert.ToInt32(Math.Floor(Math.Abs(gzsanid)));
                string gzsanis = gzsani.ToString();
                if (gzsani < 10) { gzsanis = "0" + gzsanis; }

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNunber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text + " / Passed Time: " + gzsaat.ToString() + ":" + gzdakikas + ":" + gzsanis;
            }
            else
            {
                LblVarid.ForeColor = System.Drawing.Color.Red;
                LblVarid.Text = "Please Change Watch ! ";
                LblVarno.Text = "";
                LblVarbasla.Text = "";
                LblVarbit.Text = "";
            }
        }
        else
        {
            LiteralYaz();
        }

    }

    private void LiteralYaz()
    {
        SqlConnection baglanti = AnaKlas.baglan();

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
        baglanti.Close();
        baglanti.Dispose();
    }

    private void DTloading()
    {

    }



    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"].ToString() == "" || (Session["kapno"] == null))
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
        //if (Convert.ToInt32(Session["yetki"]) ==1 || Convert.ToInt32(Session["yetki"]) ==2)
        //{

        //}
        //else if (Convert.ToInt32(Session["yetki"]) == 0)
        //{

        //}
        //else 
        if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }
    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        DTloading();
    }

    
}




