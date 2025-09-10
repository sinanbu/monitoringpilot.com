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


public partial class survey3 : System.Web.UI.Page
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
            LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
            LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();

                SqlCommand cmdDDLlim = new SqlCommand("SP_DDLarges", baglanti);
                cmdDDLlim.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDDLlim;
                DataSet ds = new DataSet();
                adapter.Fill(ds, "argeler");
                DDLarges.Items.Clear();
                DDLarges.DataValueField = "anketno";
                DDLarges.DataTextField = "anketadi";
                DDLarges.DataSource = ds;
                DDLarges.DataBind();
                DDLarges.ClearSelection();
                DDLarges.Items.FindByValue("3").Selected = true;
                cmdDDLlim.Dispose();
            }
            databagla();
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

        baglanti.Close();

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

    private void databagla()
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from argedetay where anketno = '3' order by varid asc, kapadi asc");
        GridView7.DataSource = DTlrsetgrid;
        GridView7.DataBind();

        string anketno = "";
        string anketadi = "";
        string secbir = "";
        string seciki = "";
        string secuc = "";
        string aktif = "";

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdkapnodize = new SqlCommand("Select * from argem where anketno = '3' ", baglanti);
        SqlDataReader kaprdr = cmdkapnodize.ExecuteReader();
        if (kaprdr.HasRows)
        {
            while (kaprdr.Read())
            {
                anketno = kaprdr["anketno"].ToString();
                anketadi = kaprdr["anketadi"].ToString();
                secbir = kaprdr["secbir"].ToString();
                seciki = kaprdr["seciki"].ToString();
                secuc = kaprdr["secuc"].ToString();
                aktif = kaprdr["aktif"].ToString();
            }
        }
        kaprdr.Close();


        TBpaddportno.Text = anketno;
        TBpaddportname.Text = anketadi;
        Lsecbir.Text = secbir;
        Lseciki.Text = seciki;
        Lsecuc.Text = secuc;
        Laktif.Text = aktif;

        SqlCommand total1 = new SqlCommand("Select count(anketno) from argedetay where anketno ='" + anketno + "' ", baglanti);
        int tamliste = Convert.ToInt32(total1.ExecuteScalar());
        SqlCommand total2 = new SqlCommand("Select count(secenek) from argedetay where anketno ='" + anketno+"' and secenek!='' ", baglanti);
        int katilan = Convert.ToInt32(total2.ExecuteScalar());
        SqlCommand total3 = new SqlCommand("Select count(secenek) from argedetay where anketno ='" + anketno + "' and secenek ='" + secbir + "' ", baglanti);
        int secbirsay = Convert.ToInt32(total3.ExecuteScalar());
        SqlCommand total4 = new SqlCommand("Select count(secenek) from argedetay where anketno ='" + anketno + "' and secenek ='" + seciki + "' ", baglanti);
        int secikisay = Convert.ToInt32(total4.ExecuteScalar());
        SqlCommand total5 = new SqlCommand("Select count(secenek) from argedetay where anketno ='" + anketno + "' and secenek ='" + secuc + "' ", baglanti);
        int secucsay = Convert.ToInt32(total5.ExecuteScalar());


        sonuc1.Text = katilan.ToString()+" pilot";//katılan
        sonuc2.Text =(tamliste-katilan).ToString() + " pilot";//bekleyen
        sonuc3.Text =secbir;
        sonuc4.Text =seciki;
        sonuc5.Text =secuc;
        sonuc6.Text =secbirsay +" pilot";
        sonuc7.Text =secikisay +" pilot";
        sonuc8.Text =secucsay +" pilot";

      

        if (Laktif.Text =="0" )
        {
            LBaddvote.Enabled = false;
            LBaddvote.Text = "Anket Kapatıldı";
            Buttonlbadd.Enabled = false;
        }

        SqlCommand cmdkapsirano = new SqlCommand("SP_seskapsirano", baglanti);
        cmdkapsirano.CommandType = CommandType.StoredProcedure;
        cmdkapsirano.Parameters.AddWithValue("@kapno", Convert.ToInt32(Session["kapno"]));
        cmdkapsirano.Parameters.Add("@kapsirano", SqlDbType.Int); // 
        cmdkapsirano.Parameters["@kapsirano"].Direction = ParameterDirection.Output;
        cmdkapsirano.ExecuteNonQuery();
        int seskapsirano = Convert.ToInt32(cmdkapsirano.Parameters["@kapsirano"].Value.ToString().Trim());
        cmdkapsirano.Dispose();

        if (seskapsirano > 999)
        {
            Buttonlbadd.Enabled = false;
        }








        baglanti.Close();
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
        if (Convert.ToInt32(Session["yetki"]) ==1 || Convert.ToInt32(Session["yetki"]) ==2)
        {

        }
        else if (Convert.ToInt32(Session["yetki"]) == 6 || Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }
    protected void GridView7_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView7.SelectedIndex)
        {
            e.Cancel = true;
            GridView7.SelectedIndex = -1;
            databagla();
        }

    }



    protected void GridView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)

        {
            Label pod = (Label)e.Row.FindControl("pilotod");
            Label psoyod = (Label)e.Row.FindControl("shipod");
            Label kapnokop = (Label)e.Row.FindControl("kapno");

            if (kapnokop.Text == (Session["kapno"]).ToString())
            {
                pod.BackColor = Color.LightSalmon;
                psoyod.BackColor = Color.LightSalmon;
                //GridView7.Rows[e.Row.RowIndex].BackColor = Color.Red;
            
            }
        }
    }

    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {

        if (TBpaddsec.SelectedItem.Value!= "0")
        {

            //if (secuc == "" || TBpaddsec3 == null)
            //{
            //    TBpaddsec3.BorderColor = System.Drawing.Color.FromName("red");
            //    this.ModalPopupExtenderlbadd.Show();
            //}


            string secenek = TBpaddsec.SelectedItem.Text;
            string sectarihi = AnaKlas.TarihSaatYaziYapDMYhm(DateTime.Now);
            string onay = "1";

            bool sonuc = addport(secenek, sectarihi, onay);

            if (sonuc)
            {
                GridView7.EditIndex = -1;
                databagla();
            }
        }
        else
        { this.ModalPopupExtenderlbadd.Show(); }
    }
    

private bool addport(string secenek, string sectarihi, string onay)
{
    bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("update argedetay set secenek=@secenek,sectarihi=@sectarihi,onay=@onay where (anketno='" + TBpaddportno.Text + "' and kapno='" + Session["kapno"].ToString() + "')", baglanti);
        cmd.Parameters.AddWithValue("secenek", secenek);
        cmd.Parameters.AddWithValue("sectarihi", sectarihi);
        cmd.Parameters.AddWithValue("onay", onay);
        cmd.Dispose();

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



    protected void LBaddvote_Click(object sender, EventArgs e)
    {
        ListItem lu0 = new ListItem("Seçiminiz?", "0");
        ListItem lu1 = new ListItem(Lsecbir.Text, "1");
        ListItem lu2 = new ListItem(Lseciki.Text, "2");
        ListItem lu3 = new ListItem(Lsecuc.Text, "3");
        TBpaddsec.Items.Clear();
        TBpaddsec.Items.Add(lu0);
        TBpaddsec.Items.Add(lu1);
        TBpaddsec.Items.Add(lu2);
        TBpaddsec.Items.Add(lu3);
        //TBpaddsec.SelectedItem.Selected = false;
        // TBpaddsec.Items[0].Selected = true;
        //TBpaddsec.SelectedItem.Value = "-1";
        this.ModalPopupExtenderlbadd.Show();
        //int satir = Convert.ToInt32((GridView7.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("pilotod") as Label).Text);


    }


    protected void DDLarges_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand onaybak = new SqlCommand("Select aciklama from argem where (anketno='" + DDLarges.SelectedItem.Value + "')  ", baglanti);
        string sayfalink = onaybak.ExecuteScalar().ToString();
        onaybak.Dispose();
      

        Response.Redirect(sayfalink);


        baglanti.Close();
    }
}



