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


public partial class portinfo : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                LiteralYaz();
                databagla();

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();


            }
        }

        baglanti.Close();


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
        if (Session["yetki"].ToString() != "9")
        {
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = true;
        }
        else
        {
            GridView1.Columns[8].Visible = true;
            GridView1.Columns[9].Visible = false;
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



    private void databagla()
    {

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDTlimbilgial = new SqlCommand("SP_LimanBilgiler", baglanti);
        cmdDTlimbilgial.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adaptery = new SqlDataAdapter();
        adaptery.SelectCommand = cmdDTlimbilgial;
        DataSet dsy = new DataSet();
        adaptery.Fill(dsy, "limanisimleri");
        GridView1.DataSource = dsy;
        GridView1.DataBind();
        baglanti.Close();

    }

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
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
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "linkle")
        {
            string maplink = (GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("Litemportgoster") as Label).Text;
            maplink = maplink.ToLower();

            Image1.ImageUrl = "images/limanplan/" + maplink;
            Image1.Width = 747;

            this.MPEdrawing.Show();
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala
        //GridView1.Rows[GridView1.EditIndex].FindControl("TBeditportgoster").Visible = false;
        databagla();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editportid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string rihtimadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportrihtimadi") as TextBox).Text.Trim().ToString().ToLower()));
        string limanbolge = (GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanbolge") as TextBox).Text;
        string zorluk = (GridView1.Rows[e.RowIndex].FindControl("TBeditportzorluk") as TextBox).Text;
        string bagliistasyon = (GridView1.Rows[e.RowIndex].FindControl("TBeditportresp") as TextBox).Text;
        string kalkissuresi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportkalkissuresi") as TextBox).Text.Trim().ToString().ToLower()));
        string goster = "";


        if (Session["yetki"].ToString() == "9")
        { goster = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).Text.Trim().ToString().ToLower())); }
        else
        { goster = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("Litemportgostery") as Label).Text.Trim().ToString().ToLower())); }


        //if (rihtimadi != "" && rihtimadi != null && limanbolge != "" && limanbolge != null && zorluk != "" && zorluk != null && bagliistasyon != "" && bagliistasyon != null && kalkissuresi != null && goster != "" && goster != null)

        SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmd = new SqlCommand("SP_Up_Limanbilgiler", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", editportid);
            cmd.Parameters.AddWithValue("yetkili", rihtimadi);
            cmd.Parameters.AddWithValue("telno", limanbolge);
            cmd.Parameters.AddWithValue("cepno", zorluk);
            cmd.Parameters.AddWithValue("faxno", bagliistasyon);
            cmd.Parameters.AddWithValue("uyari", kalkissuresi);
            cmd.Parameters.AddWithValue("kroki", goster);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            baglanti.Close();

            GridView1.EditIndex = -1;
            databagla();
        
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        databagla();
    }


    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
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
            Response.Redirect("yonet/pilots.aspx");
        }
    }





    public string Temizle(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace(" ", "");
        deger = deger.Replace("'", "");
        deger = deger.Replace("<", "");
        deger = deger.Replace(">", "");
        deger = deger.Replace("&", "");
        deger = deger.Replace("(", "");
        deger = deger.Replace(")", "");
        deger = deger.Replace(";", "");
        deger = deger.Replace("?", "");
        deger = deger.Replace("-", "");
        deger = deger.Replace(".", "");
        return deger;
    }

    public string TemizleINGoldu(string Metin)
    {
        string deger = Metin;

        deger = deger.Replace("ç", "c");
        deger = deger.Replace("ğ", "g");
        deger = deger.Replace("ı", "i");
        deger = deger.Replace("ö", "o");
        deger = deger.Replace("ş", "s");
        deger = deger.Replace("ü", "u");

        deger = deger.Trim();
        return deger;
    }


    protected void Buttonlbaddcancel_Click(object sender, EventArgs e)
    {
        MPEdrawing.Controls.Clear();

    }

 

  
}




