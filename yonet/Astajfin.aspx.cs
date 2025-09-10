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


public partial class Astajfin : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();
    string kapadis = "";
    string kapsoyadis = "";
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

        //if (DDLPilots.SelectedItem.Text!="") DDLPilots.Items.FindByText(DDLPilots.SelectedItem.Text).Selected = true;

        DataTable DTkaplar = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where kapsirano<1000 order by kapadisoyadi asc");
        DDLPilots.Items.Clear();
        DDLPilots.DataTextField = "kapadisoyadi";
        DDLPilots.DataValueField = "kapno";
        DDLPilots.DataSource = DTkaplar;
        DDLPilots.DataBind();
        DDLPilots.Items.Insert(0, new ListItem("Select", String.Empty));
        DDLPilots.SelectedIndex = 0;

    }

    private void databagla2()
    {

        Label2.Text = "Kaptan " + DDLPilots.SelectedItem.Text;

    }






    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("../main.aspx");
    }
    protected void LBonlineoff_Click(object sender, EventArgs e)
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
        databagla2();
    }


    protected void adminsayfalar_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminsayfalar.aspx");
    }

    protected void Bonay_Click(object sender, EventArgs e)
    {

      this.ModalPopuponayMessage.Show();
    }

    protected void Baccepted_Click(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmd = new SqlCommand("SELECT max(kapsirano) FROM pilotlar where kapsirano<1000", baglanti);
        //int sonkapsirano = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        //int kapsirano = sonkapsirano + 1;

        SqlCommand cmdvarbilvarno = new SqlCommand("SP_varbilgivarnooku", baglanti);
        cmdvarbilvarno.CommandType = CommandType.StoredProcedure;
        cmdvarbilvarno.Parameters.Add("@bilgivarnooku", SqlDbType.VarChar, 6);
        cmdvarbilvarno.Parameters["@bilgivarnooku"].Direction = ParameterDirection.Output;
        cmdvarbilvarno.ExecuteNonQuery();
        string varno = cmdvarbilvarno.Parameters["@bilgivarnooku"].Value.ToString().Trim();
        cmdvarbilvarno.Dispose();


        SqlCommand seskapadi = new SqlCommand("SP_seskapadi", baglanti);
        seskapadi.CommandType = CommandType.StoredProcedure;
        seskapadi.Parameters.AddWithValue("@kapno", Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString()));
        seskapadi.Parameters.Add("@kapadisonuc", SqlDbType.NVarChar, 30);
        seskapadi.Parameters["@kapadisonuc"].Direction = ParameterDirection.Output;
        seskapadi.ExecuteNonQuery();
        kapadis = seskapadi.Parameters["@kapadisonuc"].Value.ToString().Trim();
        seskapadi.Dispose();

        SqlCommand seskapsoyadi = new SqlCommand("SP_seskapsoyadi", baglanti);
        seskapsoyadi.CommandType = CommandType.StoredProcedure;
        seskapsoyadi.Parameters.AddWithValue("@kapno", Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString()));
        seskapsoyadi.Parameters.Add("@kapsoyadisonuc", SqlDbType.NVarChar, 30);
        seskapsoyadi.Parameters["@kapsoyadisonuc"].Direction = ParameterDirection.Output;
        seskapsoyadi.ExecuteNonQuery();
        kapsoyadis = seskapsoyadi.Parameters["@kapsoyadisonuc"].Value.ToString().Trim();
        seskapsoyadi.Dispose();

        SqlCommand takcikisonay = new SqlCommand("update pilotlar set yetki=@yetki,kapsirano=@kapsirano,varno=@varno, degismeciadi=@degismeciadi, degismecisoyadi=@degismecisoyadi, degismecikapno=@degismecikapno,imono=@imono   where kapno =" + Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString()), baglanti);
        takcikisonay.Parameters.AddWithValue("yetki", "0");
        takcikisonay.Parameters.AddWithValue("kapsirano", Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString()));
        takcikisonay.Parameters.AddWithValue("varno", varno);
        takcikisonay.Parameters.AddWithValue("degismeciadi", kapadis);
        takcikisonay.Parameters.AddWithValue("degismecisoyadi", kapsoyadis);
        takcikisonay.Parameters.AddWithValue("degismecikapno", Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString()));
        takcikisonay.Parameters.AddWithValue("imono", 0);
        takcikisonay.ExecuteNonQuery();
        takcikisonay.Dispose();


        // pilotvardiyaya satır eklemesi

        SqlCommand cmdkapnoadd = new SqlCommand("insert into pilotvardiya (kapno,kapadisoyadi,varno,toplamissayisi,toplamissuresi,toplamdinlenme,toplamzihyor,lastday,lastseven,sevensayi,daysayi,yorgungitteni,yorgungitalli,yorulma,yorulmalast) values (@kapno,@kapadisoyadi,@varno,@toplamissayisi,@toplamissuresi,@toplamdinlenme,@toplamzihyor,@lastday,@lastseven,@sevensayi,@daysayi,@yorgungitteni,@yorgungitalli,@yorulma,@yorulmalast)", baglanti);
        cmdkapnoadd.Parameters.AddWithValue("kapno", Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString()));
        cmdkapnoadd.Parameters.AddWithValue("kapadisoyadi", kapadis + ' ' + kapsoyadis);
        cmdkapnoadd.Parameters.AddWithValue("varno", varno);
        cmdkapnoadd.Parameters.AddWithValue("toplamissayisi", 0);
        cmdkapnoadd.Parameters.AddWithValue("toplamissuresi", 0);
        cmdkapnoadd.Parameters.AddWithValue("toplamdinlenme", 0); //toplam zihinsel yorulma
        cmdkapnoadd.Parameters.AddWithValue("toplamzihyor", 0); // kullanılmıyor
        cmdkapnoadd.Parameters.AddWithValue("lastday", 24);
        cmdkapnoadd.Parameters.AddWithValue("lastseven", 0);
        cmdkapnoadd.Parameters.AddWithValue("sevensayi", 0);
        cmdkapnoadd.Parameters.AddWithValue("daysayi", 0);
        cmdkapnoadd.Parameters.AddWithValue("yorgungitteni", 0);
        cmdkapnoadd.Parameters.AddWithValue("yorgungitalli", 0);
        cmdkapnoadd.Parameters.AddWithValue("yorulma", 0);
        cmdkapnoadd.Parameters.AddWithValue("yorulmalast", 0);
        cmdkapnoadd.ExecuteNonQuery();
        cmdkapnoadd.Dispose();





        baglanti.Close();

        databagla();
        
    }

}




