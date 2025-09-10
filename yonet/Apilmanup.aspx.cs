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


public partial class Apilmanup : System.Web.UI.Page
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
        using (PilotdbEntitiesPilotlar entpil = new PilotdbEntitiesPilotlar())
        {
            entpil.Configuration.ProxyCreationEnabled = false;

            var veri = from b in entpil.pilotlar.Where(b => b.durum!="0").ToList() select b;
            GridView1.DataSource = veri.OrderBy(b => b.kapadi).ThenBy(b => b.kapsoyadi);
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
        databagla();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editisid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string istasyoncikis = (GridView1.Rows[e.RowIndex].FindControl("TBistasyoncikis") as TextBox).Text;
        string pob = (GridView1.Rows[e.RowIndex].FindControl("TBpob") as TextBox).Text;
        string poff = (GridView1.Rows[e.RowIndex].FindControl("TBpoff") as TextBox).Text;
        string istasyongelis = (GridView1.Rows[e.RowIndex].FindControl("TBistasyongelis") as TextBox).Text;

        if (istasyoncikis != "" && istasyoncikis != null && pob != "" && pob != null && poff != "" && poff != null && istasyongelis != "" && istasyongelis != null)
        {
            if (IsDate2(istasyoncikis) == true && IsDate2(pob) == true && IsDate2(poff) == true && IsDate2(istasyongelis) == true)
            {
                if (TarihSaatYapDMYhm(istasyoncikis) <= TarihSaatYapDMYhm(pob))
                {
                    if (TarihSaatYapDMYhm(pob) <= TarihSaatYapDMYhm(poff))
                    {
                        if (TarihSaatYapDMYhm(poff) <= TarihSaatYapDMYhm(istasyongelis))
                        {
                            SqlConnection baglanti = AnaKlas.baglan(); 
                            SqlCommand cmdisetbup = new SqlCommand("SP_Up_PilotManevra_Time", baglanti);
                            cmdisetbup.CommandType = CommandType.StoredProcedure;
                            cmdisetbup.Parameters.AddWithValue("@secilikapno", editisid);
                            cmdisetbup.Parameters.AddWithValue("@istasyoncikis", istasyoncikis);
                            cmdisetbup.Parameters.AddWithValue("@pob", pob);
                            cmdisetbup.Parameters.AddWithValue("@poff", poff);
                            cmdisetbup.Parameters.AddWithValue("@istasyongelis", istasyongelis);
                            cmdisetbup.ExecuteNonQuery();
                            cmdisetbup.Dispose();


                            baglanti.Close();

                            GridView1.EditIndex = -1;
                            databagla();
                        }
                    }
                }
        }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        databagla();
    }


    public DateTime TarihSaatYapDMYhm(string Tarihsaaatcumlesi)
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        DateTime Tarihsaatok = DateTime.Parse(Tarihsaaatcumlesi, culture, System.Globalization.DateTimeStyles.AssumeLocal);
        // result = DateTime.TryParseExact(Tarihsaaatcumlesi, dtFormats, new CultureInfo("tr-TR"), DateTimeStyles.None, out dt);
        //DateTime Tarihsaatok = DateTime.ParseExact(Tarihsaaatcumlesi, "DD.mm.yyyy HH:mm", null);
        return Tarihsaatok;
    }
    public bool IsDate2(string tarihyazi)
    {
        DateTime Temp;
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        if (DateTime.TryParse(tarihyazi, culture, System.Globalization.DateTimeStyles.AssumeLocal, out Temp) == true)
            return true;
        else
            return false;
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
        if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("pilots.aspx");
        }
    }

    protected void adminsayfalar_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminsayfalar.aspx");
    }


}




