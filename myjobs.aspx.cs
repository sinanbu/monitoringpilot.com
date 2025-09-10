using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;
using System.Linq;


public partial class myjobs : System.Web.UI.Page
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
        else  //if (cmdlogofbak.ExecuteScalar() != null)
        {
            SqlCommand cmdkapsiranoal = new SqlCommand("Select kapsirano from pilotlar where kapno='" + Convert.ToInt32(Session["kapno"]) + "'  ", baglanti);
            string kapsirano = cmdkapsiranoal.ExecuteScalar().ToString();
            cmdkapsiranoal.Dispose();

            if (Convert.ToInt32(kapsirano) > 999)
            {
                Response.Redirect("http://www.monitoringpilot.com");
            }
            else
            {

                if (!Page.IsPostBack)
                {
                    if (Session["yetki"].ToString() == "9")
                    { LBonline.Enabled = true; }
                    else { LBonline.Enabled = false; }


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

                    LiteralYaz();

                    LblVarid.Text = "Watch:" + varbilvarid.Text;
                    LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                    LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                    LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                    LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
                }
            }

            if (Session["yetki"].ToString() == "0")
            {
                mainmanu2.Visible = false;
                mainmanu3.Visible = false;
                mainmanu4.Visible = false;
                mainmanu6.Visible = false;
            }

            if (Session["kapno"].ToString() == "1" || Session["kapno"].ToString() == "47" || Session["kapno"].ToString() == "52")
            {
                mainmanu2.Visible = true;
                mainmanu3.Visible = true;
                mainmanu6.Visible = true;
            }
            else if (Session["kapno"].ToString() == "96")
            {
                mainmanu2.Visible = true;
            }

            gridload();
        }

        baglanti.Close();

    }

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
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

    private void gridload()
    {
        int kapnoal = Convert.ToInt32(Session["kapno"].ToString());


        using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
        {
            entiki.Configuration.ProxyCreationEnabled = false;
            var veri = from b in entiki.vardiyadetay.Where(b => b.kapno == kapnoal || b.degismecikapno == kapnoal).ToList() select b;

            foreach (var c in veri)
            {
                
                SqlConnection baglanti = AnaKlas.baglan();
                SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                cmdPilotismial.CommandType = CommandType.StoredProcedure;
                cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(c.degismecikapno));
                string isimal = "";
                SqlDataReader dr = cmdPilotismial.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        isimal = dr["kapadisoyadi"].ToString();
                    }
                }
                dr.Close();
                cmdPilotismial.Dispose();

                SqlCommand cmdPilotismiald = new SqlCommand("SP_Pilotismial", baglanti);
                cmdPilotismiald.CommandType = CommandType.StoredProcedure;
                cmdPilotismiald.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(c.kapno));
                string isimald = "";
                SqlDataReader drd = cmdPilotismiald.ExecuteReader();
                if (drd.HasRows)
                {
                    while (drd.Read())
                    {
                        isimald = drd["kapadisoyadi"].ToString();
                    }
                }
                drd.Close();
                cmdPilotismiald.Dispose();

                baglanti.Close();

                if(c.kapno.ToString()== Session["kapno"].ToString() && c.degismecikapno.ToString() == Session["kapno"].ToString())
                { c.pilotismi = " -"; }
                else if(c.kapno.ToString() != Session["kapno"].ToString() && c.degismecikapno.ToString() == Session["kapno"].ToString())
                { c.pilotismi = isimald+"("+isimal+")"; }
                else if (c.kapno.ToString() == Session["kapno"].ToString() && c.degismecikapno.ToString() != Session["kapno"].ToString())
                { c.pilotismi = isimald + "(" + isimal + ")"; }





                    
                c.orderbay = Convert.ToDateTime(c.pob);

            }

            Lwoidgunluk.Text = " Total Signed Pilot Bill : " + veri.Where(b => b.degismecikapno == kapnoal && b.gemiadi.ToLower()!="takviye").Count() ;

            GridView7.DataSource = veri.OrderByDescending(b => b.orderbay).ToList();
            GridView7.DataBind();


        }
    }

    protected void GridView7_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView7.SelectedIndex)
        {
            e.Cancel = true;
            GridView7.SelectedIndex = -1;
        }
    }

    protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7.PageIndex = e.NewPageIndex;
        gridload();

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
        else if (Convert.ToInt32(Session["yetki"]) == 0)
        {
            //       Response.Redirect("yonet/pilots.aspx");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/pilots.aspx");
        }
    }


}




