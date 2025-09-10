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

public partial class yonet_anchorageplaces : System.Web.UI.Page
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
                GridView1.Columns[8].Visible = false;
                GridView1.Columns[9].Visible = false;
                LBaddnewportberth.Visible = false;

                Litmenu1.Visible = false;
                if (Session["yetki"].ToString() == "0")
                {
                    Litmenu4.Visible = false;
                    Litmenu5.Visible = false;
                    Litmenu6.Visible = false;
                }
            }

            else
            { Litpagebaslik.Text = "PMTR Admin Page"; }

            if (!IsPostBack)
            { databagla(); }
        }

        else
        {
            Response.Redirect("../pmtr.aspx");
        }
        baglanti.Close();
    }
    private void databagla()
    {
        DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select * from limanlar where limanno>1000 and limanno<1100  order by limanadi");
        GridView1.DataSource = DTlrsetgrid;
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
        SqlCommand cmd = new SqlCommand("Delete from limanlar where id= " + delportno, baglanti);
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;//secili satır editlenecekse yakala
        databagla();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editportid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        int limanno = Convert.ToInt32((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanno") as TextBox).Text);
        string limanadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanadi") as TextBox).Text.Trim().ToString().ToLower()));
        string limanbolge = (GridView1.Rows[e.RowIndex].FindControl("TBeditportlimanbolge") as TextBox).Text;
        string bagliistasyon = (GridView1.Rows[e.RowIndex].FindControl("TBeditportresp") as TextBox).Text;
        string yanasmasuresis = (GridView1.Rows[e.RowIndex].FindControl("TBeditportyanasmasuresi") as TextBox).Text;
        string kalkissuresis = (GridView1.Rows[e.RowIndex].FindControl("TBeditportkalkissuresi") as TextBox).Text;
        string goster = (GridView1.Rows[e.RowIndex].FindControl("TBeditportgoster") as TextBox).Text;

        if (limanadi != "" && limanadi != null  && limanbolge != "" && limanbolge != null &&  bagliistasyon != "" && bagliistasyon != null && yanasmasuresis != "" && yanasmasuresis != null && kalkissuresis != "" && kalkissuresis != null && goster != "" && goster != null)
        {
            int yanasmasuresi = Convert.ToInt32(yanasmasuresis);
            int kalkissuresi = Convert.ToInt32(kalkissuresis);
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("update limanlar set limanno=@limanno, limanadi=@limanadi,rihtimadi=@rihtimadi,limanbolge=@limanbolge,zorluk=@zorluk,bagliistasyon=@bagliistasyon,yanasmasuresi=@yanasmasuresi,kalkissuresi=@kalkissuresi,goster=@goster where id=" + editportid, baglanti);
        cmd.Parameters.AddWithValue("limanno", limanno);
        cmd.Parameters.AddWithValue("limanadi", limanadi);
        cmd.Parameters.AddWithValue("rihtimadi", "0");
        cmd.Parameters.AddWithValue("limanbolge", limanbolge);
        cmd.Parameters.AddWithValue("zorluk", "1");
        cmd.Parameters.AddWithValue("bagliistasyon", bagliistasyon);
        cmd.Parameters.AddWithValue("yanasmasuresi", yanasmasuresi);
        cmd.Parameters.AddWithValue("kalkissuresi", kalkissuresi);
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
    protected void LBaddnewportberth_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("SELECT max(limanno) FROM limanlar where limanno>1000 and limanno<1100  ", baglanti);
        int sonlimanno = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        sonlimanno = sonlimanno + 1;
        baglanti.Close();

        TBpaddportno.Text = sonlimanno.ToString();

        TBpaddportname.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddyanasmasuresi.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddkalkissuresi.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddgoster.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddportarea.BorderColor = System.Drawing.Color.FromName("gray");
        TBpaddresp.BorderColor = System.Drawing.Color.FromName("gray");

        TBpaddportname.Text = "";
        TBpaddyanasmasuresi.Text = "";
        TBpaddkalkissuresi.Text = "";
        TBpaddgoster.Text = "";
        TBpaddportarea.Text = "";
        TBpaddresp.Text = "";

        this.ModalPopupExtenderlbadd.Show();

        //GridView1.ShowFooter = true;
        //GridView1.Columns[6].Visible = false;
        //databagla();
    }
    protected void Buttonlbadd_Click(object sender, EventArgs e)
    {
        string limanadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle(TBpaddportname.Text.Trim().ToString().ToLower()));

        if (limanadi == "" || limanadi == null)
        {
            TBpaddportname.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddyanasmasuresi.Text == "" || TBpaddyanasmasuresi.Text == null)
        {
            TBpaddyanasmasuresi.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddkalkissuresi.Text == "" || TBpaddkalkissuresi.Text == null)
        {
            TBpaddkalkissuresi.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddgoster.Text == "" || TBpaddgoster.Text == null)
        {
            TBpaddgoster.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }
        if (TBpaddportarea.Text == "" || TBpaddportarea.Text == null)
        {
            TBpaddportarea.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }

        if (TBpaddresp.Text == "" || TBpaddresp.Text == null)
        {
            TBpaddresp.BorderColor = System.Drawing.Color.FromName("red");
            this.ModalPopupExtenderlbadd.Show();
        }


        if (limanadi != "" && limanadi != null && TBpaddyanasmasuresi.Text != "" && TBpaddyanasmasuresi.Text != null && TBpaddkalkissuresi.Text != "" && TBpaddkalkissuresi.Text != null && TBpaddgoster.Text != "" && TBpaddgoster.Text != null && TBpaddresp.Text != "" && TBpaddresp.Text != null && TBpaddportarea.Text != "" && TBpaddportarea.Text != null)
        {
        int sonlimanno = Convert.ToInt32(TBpaddportno.Text);
        string rihtimadi = "0"; 
        string limanbolge = TBpaddportarea.Text;
        string zorluk = "1";
        string bagliistasyon = TBpaddresp.Text;
        int yanasmasuresi = Convert.ToInt32(TBpaddyanasmasuresi.Text);
        int kalkissuresi = Convert.ToInt32(TBpaddkalkissuresi.Text);
        string goster = TBpaddgoster.Text;


        bool sonuc = addport(sonlimanno, limanadi, rihtimadi, limanbolge, zorluk, bagliistasyon, yanasmasuresi, kalkissuresi, goster);

        if (sonuc)
        {
            GridView1.EditIndex = -1;
            GridView1.ShowFooter = false;
            databagla();
        }
        }
    }
    private bool addport(int limanno, string limanadi, string rihtimadi, string limanbolge, string zorluk, string bagliistasyon, int yanasmasuresi, int kalkissuresi, string goster)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("insert into limanlar (limanno,limanadi,rihtimadi,limanbolge,zorluk,bagliistasyon,yanasmasuresi,kalkissuresi,goster) values(@limanno,@limanadi,@rihtimadi,@limanbolge,@zorluk,@bagliistasyon,@yanasmasuresi,@kalkissuresi,@goster)", baglanti);
        cmd.Parameters.AddWithValue("limanno", limanno);
        cmd.Parameters.AddWithValue("limanadi", limanadi);
        cmd.Parameters.AddWithValue("rihtimadi", rihtimadi);
        cmd.Parameters.AddWithValue("limanbolge", limanbolge);
        cmd.Parameters.AddWithValue("zorluk", zorluk);
        cmd.Parameters.AddWithValue("bagliistasyon", bagliistasyon);
        cmd.Parameters.AddWithValue("yanasmasuresi", yanasmasuresi);
        cmd.Parameters.AddWithValue("kalkissuresi", kalkissuresi);
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
}