using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;


public partial class yonet_statisticsa : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == "" || (Session["kapno"] == null) || cmdlogofbak.ExecuteScalar() == null || Session["yetki"].ToString() != "9")
        {
            Response.Redirect("../pmtr.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                Chartoranlason();
                Chartoranlaonceki();
                Chartoranlaonceki2();
            }
            Litpagebaslik.Text = "PMTR Admin Page";
        }

        baglanti.Close();
    }

    private void Chartoranlason()
    {
      SqlConnection baglanti = AnaKlas.baglan();

      string varnonow = AnaKlas.varnohesapla();

      //iş toplami al
      SqlCommand totaljobs = new SqlCommand("Select sum(toplamissayisi) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totaljobsy = "0";
        if (totaljobs.ExecuteScalar() != null)
        {
        totaljobsy = totaljobs.ExecuteScalar().ToString();
        }
        if (totaljobsy == "" || totaljobsy == null) totaljobsy = "0";


      //workhrs toplami al
      SqlCommand totalwh = new SqlCommand("Select sum(toplamissuresi) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
      string totalwhy = "0";
      if (totalwh.ExecuteScalar() != null)
        {
            totalwhy = totalwh.ExecuteScalar().ToString();
        }
      if (totalwhy == "" || totalwhy == null) totalwhy = "0";

        //owa hesabı
        string owah = "0.0000";
        if (totalwhy != "0" & totaljobsy != "0")
        {

        if (totaljobs.ExecuteScalar() != null & totalwh.ExecuteScalar() != null & totaljobsy != "0")
        {
            owah = (decimal.Parse(totalwhy)/decimal.Parse(totaljobsy)).ToString();
        } 
        }

        //pilot sayısı al
        SqlCommand totalvarpilot = new SqlCommand("Select count(varno) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totalwp = Convert.ToInt32(totalvarpilot.ExecuteScalar()).ToString();

        //opa hesabı
        string opah = "0.0000";
        if (totalwhy != "0" & totaljobsy != "0")
        {
            if (totalvarpilot.ExecuteScalar() != null & totalwh.ExecuteScalar() != null)
            {
                opah = (decimal.Parse(totalwhy) / decimal.Parse(totalwp)).ToString();
            }
        }

      Lwid.Text = AnaKlas.varidhesapla();
      Lwstart.Text = AnaKlas.varbaslangic().ToString();
      Lwfinish.Text = AnaKlas.varbitis().ToString();
      Ljobs.Text=totaljobsy;
      Lwork.Text = totalwhy;
      Lowa.Text = opah.Substring(0, 4) + " hrs.";


      baglanti.Close();

        using (PilotdbEntities entbir = new PilotdbEntities())
        {
            var veri = from b in entbir.pilotvardiyas where b.varno == varnonow orderby b.yorulma descending select b;

            int max = Convert.ToInt32(veri.Max(a => a.yorulma));
            if (max == 0) max = 1;
            foreach (var a in veri)
            {
                string ttdd = Convert.ToString((150 * a.yorulma) / max);
                decimal p = decimal.Parse(ttdd);
                a.Percentage = Convert.ToInt32(p);
            }

            foreach (var c in veri)
             { 
                decimal issuresi = decimal.Parse(c.toplamissuresi.ToString());
                    int issayisi = Convert.ToInt32(c.toplamissayisi);
                     if (issayisi == 0) issayisi = 1;
                string owa = Convert.ToString(issuresi / issayisi);
                c.owa = owa.Substring(0,4);
            }

            GridView1.DataSource = veri.ToList();
            GridView1.DataBind();
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView1.SelectedIndex)
        {
            e.Cancel = true;
            GridView1.SelectedIndex = -1;
            Chartoranlason();
            Chartoranlaonceki();
            Chartoranlaonceki2();
        }
    }

    private void Chartoranlaonceki()
    {
        SqlConnection baglanti = AnaKlas.baglan();
        string varnonow = AnaKlas.varnohesaplaonceki();

        //iş toplami al
        SqlCommand totaljobs = new SqlCommand("Select sum(toplamissayisi) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totaljobsy = "0";
        if (totaljobs.ExecuteScalar() != null)
        {
            totaljobsy = totaljobs.ExecuteScalar().ToString();
        }
        if (totaljobsy == "" || totaljobsy == null) totaljobsy = "0";

        //workhrs toplami al
        SqlCommand totalwh = new SqlCommand("Select sum(toplamissuresi) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totalwhy = "0";
        if (totalwh.ExecuteScalar() != null)
        {
            totalwhy = totalwh.ExecuteScalar().ToString();
        }
        if (totalwhy == "" || totalwhy == null) totalwhy = "0";

        //owa hesabı
        string owah = "0.0000";
        if (totalwhy != "0" & totaljobsy != "0")
        {

            if (totaljobs.ExecuteScalar() != null & totalwh.ExecuteScalar() != null & totaljobsy != "0")
            {
                owah = (decimal.Parse(totalwhy) / decimal.Parse(totaljobsy)).ToString();
            }
        }

        //pilot sayısı al
        SqlCommand totalvarpilot = new SqlCommand("Select count(varno) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totalwp = "0";
        if (totalvarpilot.ExecuteScalar() != null)
        {
            totalwp = totalvarpilot.ExecuteScalar().ToString();
        }
        if (totalwp == "" || totalwp == null) totalwp = "0";

        //opa hesabı
        string opah = "0.0000";
        if (totalwhy != "0" & totaljobsy != "0")
        {
            if (totalvarpilot.ExecuteScalar() != null & totalwh.ExecuteScalar() != null)
            {
                opah = (decimal.Parse(totalwhy) / decimal.Parse(totalwp)).ToString();
            }
        }

        Lwoid.Text = AnaKlas.varidhesaplaonceki();
        Lwstartonceki.Text = AnaKlas.varbaslangiconceki().ToString();
        Lwfinishonceki.Text = AnaKlas.varbitisonceki().ToString();
        Lojobs.Text = totaljobsy;
        Lowork.Text = totalwhy;
        Lowao.Text = opah.Substring(0, 4) + " hrs.";

        baglanti.Close();

        using (PilotdbEntities entbir = new PilotdbEntities())
        {
            var veri = from b in entbir.pilotvardiyas where b.varno == varnonow orderby b.yorulma descending select b;

            int max = Convert.ToInt32(veri.Max(a => a.yorulma));
            if (max == 0) max = 1;
            foreach (var a in veri)
            {
                string ttdd = Convert.ToString((150 * a.yorulma) / max);
                decimal p = decimal.Parse(ttdd);
                a.Percentage = Convert.ToInt32(p);
            }

            foreach (var c in veri)
            {
                decimal issuresi = decimal.Parse(c.toplamissuresi.ToString());
                int issayisi = Convert.ToInt32(c.toplamissayisi);
                if (issayisi == 0) issayisi = 1;
                string owa = Convert.ToString(issuresi / issayisi);
                c.owa = owa.Substring(0, 4);
            }

            GridView2.DataSource = veri.ToList();
            GridView2.DataBind();
        }
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView2.SelectedIndex)
        {
            e.Cancel = true;
            GridView2.SelectedIndex = -1;
            Chartoranlason();
            Chartoranlaonceki();
            Chartoranlaonceki2();
        }
    }

    private void Chartoranlaonceki2()
    {
        SqlConnection baglanti = AnaKlas.baglan();
        string varnonow = AnaKlas.varnohesaplaonceki2();

        //iş toplami al
        SqlCommand totaljobs = new SqlCommand("Select sum(toplamissayisi) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totaljobsy = "0";
        if (totaljobs.ExecuteScalar() != null)
        {
            totaljobsy = totaljobs.ExecuteScalar().ToString();
        }
        if (totaljobsy == "" || totaljobsy == null) totaljobsy = "0";


        //workhrs toplami al
        SqlCommand totalwh = new SqlCommand("Select sum(toplamissuresi) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totalwhy = "0";
        if (totalwh.ExecuteScalar() != null)
        {
            totalwhy = totalwh.ExecuteScalar().ToString();
        }
        if (totalwhy == "" || totalwhy == null) totalwhy = "0";

        //owa hesabı
        string owah = "0.0000";
        if (totalwhy != "0" & totaljobsy != "0")
        {

            if (totaljobs.ExecuteScalar() != null & totalwh.ExecuteScalar() != null & totaljobsy != "0")
            {
                owah = (decimal.Parse(totalwhy) / decimal.Parse(totaljobsy)).ToString();
            }
        }

        //pilot sayısı al
        SqlCommand totalvarpilot = new SqlCommand("Select count(varno) from pilotvardiya where varno='" + varnonow + "' ", baglanti);
        string totalwp = "0";
        if (totalvarpilot.ExecuteScalar() != null)
        {
            totalwp = totalvarpilot.ExecuteScalar().ToString();
        }
        if (totalwp == "" || totalwp == null) totalwp = "0";

        //opa hesabı
        string opah = "0.0000";
        if (totalwhy != "0" & totaljobsy != "0")
        {
            if (totalvarpilot.ExecuteScalar() != null & totalwh.ExecuteScalar() != null)
            {
                opah = (decimal.Parse(totalwhy) / decimal.Parse(totalwp)).ToString();
            }
        }

        Lwo2id.Text = AnaKlas.varidhesaplaonceki2();
        Lwstartonceki2.Text = AnaKlas.varbaslangiconceki2().ToString();
        Lwfinishonceki2.Text = AnaKlas.varbitisonceki2().ToString();
        Lo2jobs.Text = totaljobsy;
        Lo2work.Text = totalwhy;
        Lowao2.Text = opah.Substring(0, 4)+" hrs.";
        baglanti.Close();


        using (PilotdbEntities entbir = new PilotdbEntities())
        {
            var veri = from b in entbir.pilotvardiyas where b.varno == varnonow orderby b.yorulma descending select b;

            int max = Convert.ToInt32(veri.Max(a => a.yorulma));
            if (max == 0) max = 1;
            foreach (var a in veri)
            {
                string ttdd = Convert.ToString((150 * a.yorulma) / max);
                decimal p = decimal.Parse(ttdd);
                a.Percentage = Convert.ToInt32(p);
            }

            foreach (var c in veri)
            {
                decimal issuresi = decimal.Parse(c.toplamissuresi.ToString());
                int issayisi = Convert.ToInt32(c.toplamissayisi);
                if (issayisi == 0) issayisi = 1;
                string owa = Convert.ToString(issuresi / issayisi);
                c.owa = owa.Substring(0, 4);
            }

            GridView3.DataSource = veri.ToList();
            GridView3.DataBind();
        }
    }
    protected void GridView3_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView3.SelectedIndex)
        {
            e.Cancel = true;
            GridView3.SelectedIndex = -1;
            Chartoranlason();
            Chartoranlaonceki();
            Chartoranlaonceki2();
        }
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
    protected void liistatik2_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsb.aspx");
    }
    protected void liistatik3_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsc.aspx");
    }
    protected void liistatik4_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsd.aspx");
    }
    protected void liistatik5_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticse.aspx");
    }
    protected void liistatik6_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsf.aspx");
    }
    protected void liistatik7_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsg.aspx");
    }
    protected void liistatik8_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsh.aspx");
    }
    protected void liistatik9_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsi.aspx");
    }
}