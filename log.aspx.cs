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


public partial class log : System.Web.UI.Page
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

        else if (Session["kapno"].ToString() != "122" && Session["kapno"].ToString() != "1" && Session["kapno"].ToString() != "24" && Session["kapno"].ToString() != "56" && Session["kapno"].ToString() != "65" && Session["kapno"].ToString() != "96" && Session["kapno"].ToString() != "100" && Session["kapno"].ToString() != "112" && Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2" && Session["yetki"].ToString() != "9" && Session["kapno"].ToString() != "95" && Session["kapno"].ToString() != "92" && Session["kapno"].ToString() != "94" && Session["kapno"].ToString() != "97" && Session["kapno"].ToString() != "98"  && Session["yetki"].ToString() != "4")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
			
       else  //if (cmdlogofbak.ExecuteScalar() != null)
        {
            if (!Page.IsPostBack)
            {
                if (Session["yetki"].ToString() == "9")
                { LBonline.Enabled = true; }
                else { LBonline.Enabled = false; }

                if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2" && Session["yetki"].ToString() != "9")
                {
                    Bdaricaships.Visible = false;
                    Byarimcaships.Visible = false;
                }

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
                TextBox7.Text = DateTime.Now.ToShortDateString();

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
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
            else if (Session["kapno"].ToString() == "96" || Session["kapno"].ToString() == "100" || Session["kapno"].ToString() == "112")
            {
                mainmanu1.Visible = false;
                mainmanu2.Visible = true;
                mainmanu4.Visible = true;
                mainmanu7.Visible = false;
            }



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
        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri2 = from b2 in entikib.vardiyadetay.Where(b2 => b2.grt.Contains("99999999")) select b2;
            Replog.DataSource = veri2.ToList();
            Replog.DataBind();
            Lwoidgunluk.Text = "";
        }
    }

    //protected void GridView7_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    if (e.NewSelectedIndex == Replog.SelectedIndex)
    //    {
    //        e.Cancel = true;
    //        Replog.SelectedIndex = -1;
    //    }
    //    TextBox7.Visible = true;
    //    LBgetist3.Visible = true;
    //}

    private void prosit()
    {
        string gunal = TextBox7.Text.ToString().Trim();


        ////duyuru daily
        SqlConnection baglanti2 = AnaKlas.baglan();

        string jnotdaily = "";
        string kayittarihi = "";
        string iptaltarihi = "";
        LBLjdaily.Text = "";

        SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyokuall", baglanti2); //enson duyuru oku
        cmdduydayoku.CommandType = CommandType.StoredProcedure;
        cmdduydayoku.Parameters.AddWithValue("@aktif", "2");
        cmdduydayoku.Parameters.AddWithValue("@kayittarihi", gunal);
        cmdduydayoku.Parameters.AddWithValue("@iptaltarihi", gunal);
        SqlDataReader limread = cmdduydayoku.ExecuteReader();

        if (limread.HasRows)
        {
            while (limread.Read())
            {
                jnotdaily = limread["duyuru"].ToString();
                kayittarihi = limread["kayittarihi"].ToString();
                iptaltarihi = limread["iptaltarihi"].ToString();

                LBLjdaily.Text = LBLjdaily.Text + kayittarihi +" / "+ iptaltarihi  +" => "+ jnotdaily + "<br/>";
            }
        }
        limread.Close();
        cmdduydayoku.Dispose();


        //if (iptaltarihi.Trim() == null || iptaltarihi.Trim() == "" || iptaltarihi.Trim().Length == 0)
        //{
        //}
        //else
        //{
        //    LBLjdaily.Text = jnotdaily;
        //}

        baglanti2.Close();


        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri = from b in entikib.vardiyadetay.Where(b => b.poff.Contains(gunal) && b.manevraiptal=="0").ToList() select b;

            if (gunal.Length == 0 || AnaKlas.Altcizgisil(gunal).Length != 10 || AnaKlas.IsDate2(gunal) != true )
            {
                Lwoidgunluk.Text = "Date is not corret format";
                Replog.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                Replog.DataBind();
            }
            else
            {
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

                    baglanti.Close();
                    c.pilotismi = isimal;
                    c.orderbay = Convert.ToDateTime(c.poff);

                    if(c.rom5=="") { c.rom1 = c.rom1 + " " + c.rom2 + " " + c.rom3 + " " + c.rom4; }
                    else           { c.rom1 = c.rom1 + " " + c.rom2 + " " + c.rom3 + " " + c.rom4 + " " + "[" + c.rom5 + "]"; }
                }
                Replog.DataSource = veri.Where(h => h.gemiadi.ToLower() != "takviye").OrderByDescending(x => x.orderbay).ToList();
                Replog.DataBind();
                Lwoidgunluk.Text = veri.Where(h => h.gemiadi.ToLower() != "takviye").Count() + " vessel traffics in " + gunal;
            }
        }
    }

    protected void LBgetist3_Click(object sender, EventArgs e)
    {
        prosit();

    }

    //protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //    Replog.PageIndex = e.NewPageIndex;

    //    prosit();

    //}



    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"].ToString() == "" || (Session["kapno"].ToString() == null))
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
            //       Response.Redirect("yonet/yonetim.aspx");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }



    protected void Replog_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("logedit").Visible = (false);
                e.Item.FindControl("logeditL").Visible = (true);


            }
            else {
                e.Item.FindControl("logedit").Visible = (true);
                e.Item.FindControl("logeditL").Visible = (false);

                if (Convert.ToDateTime(TextBox7.Text + " 00:00:01") < DateTime.Now.AddDays(-2))
                {
                    e.Item.FindControl("logedit").Visible = (false);
                    e.Item.FindControl("logeditL").Visible = (true);
                }
            }



        }

            

    }

    protected void logedit_Click(object sender, EventArgs e)
    {
        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
        TBJEetadt.BorderWidth = 1;

        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
        TBJEgrt.BorderWidth = 1;

        TBJEarhr1.BorderColor = System.Drawing.Color.Gray;
        TBJEarhr1.BorderWidth = 1;

        TBJEarhr2.BorderColor = System.Drawing.Color.Gray;
        TBJEarhr2.BorderWidth = 1;

        LinkButton ButtonEditkopya = (LinkButton)sender;
        string jid = HttpUtility.HtmlDecode(ButtonEditkopya.CommandArgument).ToString();
        Bacceptedjur.CommandName = jid;

        SqlConnection baglanti = AnaKlas.baglan();

        string gemiadi = "";
        string jnot = "";
        string rom1 = "";
        string rom2 = "";
        string rom3 = "";
        string rom4 = "";
        string rom5 = "";
        string mboat = "";
        string calsaat1 = "";
        string calsaat2 = "";
        string calsaat3 = "";
        string calsaat4 = "";
        string acente = "";
        string oper = "";

        SqlCommand cmdpilotokufull = new SqlCommand("SP_vardetayFulloku_fmid", baglanti);
        cmdpilotokufull.CommandType = CommandType.StoredProcedure;
        cmdpilotokufull.Parameters.AddWithValue("@id", jid);
        SqlDataReader readpilotfull = cmdpilotokufull.ExecuteReader();
        if (readpilotfull.HasRows)
        {
            while (readpilotfull.Read())
            {
                gemiadi = readpilotfull["gemiadi"].ToString();
                jnot = readpilotfull["jnot"].ToString();
                rom1 = readpilotfull["rom1"].ToString();
                rom2 = readpilotfull["rom2"].ToString();
                rom3 = readpilotfull["rom3"].ToString();
                rom4 = readpilotfull["rom4"].ToString();
                rom5 = readpilotfull["rom5"].ToString();
                mboat = readpilotfull["mboat"].ToString();
                calsaat1 = readpilotfull["calsaat1"].ToString();
                calsaat2 = readpilotfull["calsaat2"].ToString();
                calsaat3 = readpilotfull["calsaat3"].ToString();
                calsaat4 = readpilotfull["calsaat4"].ToString();
                acente = readpilotfull["acente"].ToString();
                oper = readpilotfull["oper"].ToString();
            }
        }
        readpilotfull.Close();
        cmdpilotokufull.Dispose();

        LBJEsn.Text = gemiadi;
        TBJEagency.Text = acente;
        TBJEetadt.Text = calsaat1;
        TBJEgrt.Text = calsaat2;
        TBJEarhr1.Text = calsaat3;
        TBJEarhr2.Text = calsaat4;

        TBJEloa.Text = LBonline.Text;
        TBjurnot.Text = jnot;
        TBjurnoty.Text = "";


        //SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyoku", baglanti); //enson duyuru oku
        //cmdduydayoku.CommandType = CommandType.StoredProcedure;
        //cmdduydayoku.Parameters.AddWithValue("@aktif", "2");
        //SqlDataReader limread = cmdduydayoku.ExecuteReader();

        //string kayittarihi = "";
        //string iptaltarihi = "";
        //string jnotdaily = "";
        //if (limread.HasRows)
        //{
        //    while (limread.Read())
        //    {
        //        jnotdaily = limread["duyuru"].ToString();
        //        kayittarihi = limread["kayittarihi"].ToString();
        //        iptaltarihi = limread["iptaltarihi"].ToString();
        //    }
        //}
        //limread.Close();
        //cmdduydayoku.Dispose();


        //if (iptaltarihi.Trim() == null || iptaltarihi.Trim() == "" || iptaltarihi.Trim().Length == 0)
        //{
        //}
        //else
        //{
        //    jnotdaily = "";
        //}
        //TBjurnotdaily.Text = jnotdaily;
        //TBjurnotdaily.ToolTip = jnotdaily;



        SqlCommand cmdtugs = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtugs.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterr = new SqlDataAdapter();
        adapterr.SelectCommand = cmdtugs;
        DataSet dsr = new DataSet();
        adapterr.Fill(dsr, "tuglar");
        cmdtugs.Dispose();

        DDLJEap.Items.Clear();
        DDLJEap.DataValueField = "id";
        DDLJEap.DataTextField = "short";
        DDLJEap.DataSource = dsr;
        DDLJEap.DataBind();
        DDLJEap.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEap.SelectedIndex = 0;
        DDLJEap.Visible = true;
        DDLJEap.Enabled = true;
        DDLJEap.ClearSelection();
        if (rom1 == "" || rom1 == null)
        { }
        else
        {
            ListItem cdpn = DDLJEap.Items.FindByText(rom1);

            if (cdpn != null)
            {
                DDLJEap.Items.FindByText(rom1).Selected = true;
            }
        }

        DDLJEdp.Items.Clear();
        DDLJEdp.DataValueField = "id";
        DDLJEdp.DataTextField = "short";
        DDLJEdp.DataSource = dsr;
        DDLJEdp.DataBind();
        DDLJEdp.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEdp.SelectedIndex = 0;
        DDLJEdp.Visible = true;
        DDLJEdp.Enabled = true;
        DDLJEdp.ClearSelection();
        if (rom2 == "" || rom2 == null)
        { }
        else
        {
            ListItem cdpn2 = DDLJEdp.Items.FindByText(rom2);

            if (cdpn2 != null)
            {
                DDLJEdp.Items.FindByText(rom2).Selected = true;


            }
        }

        DDLJEdb.Items.Clear();
        DDLJEdb.DataValueField = "id";
        DDLJEdb.DataTextField = "short";
        DDLJEdb.DataSource = dsr;
        DDLJEdb.DataBind();
        DDLJEdb.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEdb.SelectedIndex = 0;
        DDLJEdb.Visible = true;
        DDLJEdb.Enabled = true;
        DDLJEdb.ClearSelection();
        if (rom3 == "" || rom3 == null)
        { }
        else
        {
            ListItem cdpn3 = DDLJEdb.Items.FindByText(rom3);

            if (cdpn3 != null)
            {
                DDLJEdb.Items.FindByText(rom3).Selected = true;

            }
        }

        DDLJEflag.Items.Clear();
        DDLJEflag.DataValueField = "id";
        DDLJEflag.DataTextField = "short";
        DDLJEflag.DataSource = dsr;
        DDLJEflag.DataBind();
        DDLJEflag.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEflag.SelectedIndex = 0;
        DDLJEflag.Visible = true;
        DDLJEflag.Enabled = true;
        DDLJEflag.ClearSelection();
        if (rom4 == "" || rom4 == null)
        { }
        else
        {
            ListItem cdpn4 = DDLJEflag.Items.FindByText(rom4);

            if (cdpn4 != null)
            {
                DDLJEflag.Items.FindByText(rom4).Selected = true;
                //if (DDLJEap.SelectedItem.Text != "...")
                //{ DDLJEflag.Items.FindByText(DDLJEap.SelectedItem.Text).Enabled = false; }
                //if (DDLJEdp.SelectedItem.Text != "...")
                //{ DDLJEflag.Items.FindByText(DDLJEdp.SelectedItem.Text).Enabled = false; }
                //if (DDLJEdb.SelectedItem.Text != "...")
                //{ DDLJEflag.Items.FindByText(DDLJEdb.SelectedItem.Text).Enabled = false; }
            }
        }

        DDLJEextra.Items.Clear();
        DDLJEextra.DataValueField = "id";
        DDLJEextra.DataTextField = "short";
        DDLJEextra.DataSource = dsr;
        DDLJEextra.DataBind();
        DDLJEextra.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEextra.SelectedIndex = 0;
        DDLJEextra.Visible = true;
        DDLJEextra.Enabled = true;
        DDLJEextra.ClearSelection();
        if (rom5 == "" || rom5 == null)
        { }
        else
        {
            ListItem cdpn = DDLJEextra.Items.FindByText(rom5);

            if (cdpn != null)
            {
                DDLJEextra.Items.FindByText(rom5).Selected = true;
            }
        }

        /// Romorkör satır renklendirme
        SqlCommand cmdtdoku = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtdoku.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdt = cmdtdoku.ExecuteReader();
        string bolge = "";
        int i = 1;

        if (drdt.HasRows)
        {
            while (drdt.Read())
            {
                bolge = drdt["bolge"].ToString();
                if (bolge == "2")
                {
                    DDLJEap.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdp.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdb.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEflag.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEextra.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                }
                i++;
            }
        }
        drdt.Close();
        cmdtdoku.Dispose();


        SqlCommand cmdtugs2 = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtugs2.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterr2 = new SqlDataAdapter();
        adapterr2.SelectCommand = cmdtugs2;
        DataSet dsr2 = new DataSet();
        adapterr2.Fill(dsr2, "tuglar2");
        cmdtugs2.Dispose();

        DDLJEtip.Items.Clear();
        DDLJEtip.DataValueField = "id";
        DDLJEtip.DataTextField = "short";
        DDLJEtip.DataSource = dsr2;
        DDLJEtip.DataBind();

        DDLJEtip.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEtip.SelectedIndex = 0;
        DDLJEtip.Visible = true;
        DDLJEtip.Enabled = true;
        DDLJEtip.ClearSelection();
        if (mboat == "" || mboat == null)
        { }
        else
        {
            ListItem cdpn5 = DDLJEtip.Items.FindByText(mboat);

            if (cdpn5 != null)
            {
                DDLJEtip.Items.FindByText(mboat).Selected = true;
            }
        }

        /// Palamar satır renklendirme
        SqlCommand cmdtdokumoor = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtdokumoor.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdtm = cmdtdokumoor.ExecuteReader();
        string bolgem = "";
        int im = 1;

        if (drdtm.HasRows)
        {
            while (drdtm.Read())
            {
                bolgem = drdtm["bolge"].ToString();
                if (bolgem == "2")
                {
                    DDLJEtip.Items[im].Attributes.Add("style", "background-color: Orange !important;");
                }
                im++;
            }
        }
        drdtm.Close();
        cmdtdokumoor.Dispose();

        if (DDLJEap.SelectedItem.Text == "...") { DDLJEdp.Enabled = false; }
        //if (DDLJEdp.SelectedItem.Text == "...") { DDLJEdb.Enabled = false; }
        if (DDLJEdb.SelectedItem.Text == "...") { DDLJEflag.Enabled = false; }


        this.ModalPopupjurnot.Show();

        baglanti.Close();
    }

    protected void Bacceptedjur_Click(object sender, EventArgs e)
    {
       
        TBJEagency.BorderColor = System.Drawing.Color.Gray;
        TBJEagency.BorderWidth = 1;
        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
        TBJEetadt.BorderWidth = 1;

        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
        TBJEgrt.BorderWidth = 1;

        TBJEarhr1.BorderColor = System.Drawing.Color.Gray;
        TBJEarhr1.BorderWidth = 1;

        TBJEarhr2.BorderColor = System.Drawing.Color.Gray;
        TBJEarhr2.BorderWidth = 1;

        string kapno = LJEid.Text;
        string gemiadi = LBJEsn.Text;
        string rom1 = HttpUtility.HtmlDecode(DDLJEap.SelectedItem.Text);
        string rom2 = HttpUtility.HtmlDecode(DDLJEdp.SelectedItem.Text);
        string rom3 = HttpUtility.HtmlDecode(DDLJEdb.SelectedItem.Text);
        string rom4 = HttpUtility.HtmlDecode(DDLJEflag.SelectedItem.Text);
        string rom5 = HttpUtility.HtmlDecode(DDLJEextra.SelectedItem.Text);
        string mboat = HttpUtility.HtmlDecode(DDLJEtip.SelectedItem.Text);
        string wh1 = TBJEetadt.Text; // wh1
        string wh2 = TBJEgrt.Text;  // wh2
        string wh3 = TBJEarhr1.Text; // wh1
        string wh4 = TBJEarhr2.Text;  // wh2
        //string oper = TBJEloa.Text;
        string acente = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBJEagency.Text.ToString().Trim().ToLower());
        string jnot = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBjurnot.Text.ToString().Trim().ToLower());
        string jnoty = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBjurnoty.Text.ToString().Trim().ToLower());

        string hata = "0";

        if (rom1 == "...") { rom1 = ""; }
        if (rom2 == "...") { rom2 = ""; }
        if (rom3 == "...") { rom3 = ""; }
        if (rom4 == "...") { rom4 = ""; }
        if (rom5 == "...") { rom5 = ""; }
        if (mboat == "...") { mboat = ""; }

        if (acente == "" || acente == null)
        {
            acente = "";
            TBJEagency.BorderColor = System.Drawing.Color.Red;
            TBJEagency.BorderWidth = 1;
            hata = "1";
            this.ModalPopupjurnot.Show();
        }




                if (wh1 != "" && wh1 != null && wh1 != "__:__")
                {
                    if (IsDate2(wh1) != true)
                    {
                        TBJEetadt.BorderColor = System.Drawing.Color.Red;
                        TBJEetadt.BorderWidth = 1;
                        hata = "1";
                        this.ModalPopupjurnot.Show();
                    }
                }
                else
                {
                wh1 = "";
                }


        if (wh2 != "" && wh2 != null && wh2 != "__:__")
                {

                    if (IsDate2(wh2) != true)
                    {
                        TBJEgrt.BorderColor = System.Drawing.Color.Red;
                        TBJEgrt.BorderWidth = 1;
                        hata = "1";
                        this.ModalPopupjurnot.Show();
                    }
                }
        else
        {
            wh2 = "";
        }




                if (wh3 != "" && wh3 != null && wh3 != "__:__")
                {
                    if (IsDate2(wh3) != true)
                    {
                        TBJEarhr1.BorderColor = System.Drawing.Color.Red;
                        TBJEarhr1.BorderWidth = 1;
                        hata = "1";
                        this.ModalPopupjurnot.Show();
                    }
                }
        else
        {
            wh3 = "";
        }



        if (wh4 != "" && wh4 != null && wh4 != "__:__")
                {

                    if (IsDate2(wh4) != true)
                    {
                        TBJEarhr2.BorderColor = System.Drawing.Color.Red;
                        TBJEarhr2.BorderWidth = 1;
                        hata = "1";
                        this.ModalPopupjurnot.Show();
                    }
                }
        else
        {
            wh4 = "";
        }


        if (wh1 != "" && wh2 == "")
        {
            TBJEgrt.BorderColor = System.Drawing.Color.Red;
            TBJEgrt.BorderWidth = 1;
            hata = "1";
            this.ModalPopupjurnot.Show();
        }

        if (wh3 != "" && wh4 == "")
        {
            TBJEarhr2.BorderColor = System.Drawing.Color.Red;
            TBJEarhr2.BorderWidth = 1;
            hata = "1";
            this.ModalPopupjurnot.Show();
        }



        if (hata == "0")
        {
            SqlConnection baglanti = AnaKlas.baglan();
            //SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
            //string isimbul = cmdisimbul.ExecuteScalar().ToString();
            //cmdisimbul.Dispose();
            //SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
            //string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
            //if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
            //cmdsoyisimbul.Dispose();

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

            string operkisa = seskapadis.Substring(0, 2) + "." + seskapsoyadis.Substring(0, 2);

            if (jnoty != "")
            {  
            jnot = jnot + " " + jnoty + " [" + DateTime.Now.ToShortTimeString().Substring(0, 5) + "-" + operkisa + "]";
            }

            SqlCommand cmd = new SqlCommand("SP_Up_vardet_jurnal", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("id", Convert.ToInt32(Bacceptedjur.CommandName));
            cmd.Parameters.AddWithValue("rom1", rom1);
            cmd.Parameters.AddWithValue("rom2", rom2);
            cmd.Parameters.AddWithValue("rom3", rom3);
            cmd.Parameters.AddWithValue("rom4", rom4);
            cmd.Parameters.AddWithValue("rom5", rom5);
            cmd.Parameters.AddWithValue("mboat", mboat);
            cmd.Parameters.AddWithValue("calsaat1", wh1);
            cmd.Parameters.AddWithValue("calsaat2", wh2);
            cmd.Parameters.AddWithValue("calsaat3", wh3);
            cmd.Parameters.AddWithValue("calsaat4", wh4);

            cmd.Parameters.AddWithValue("acente", acente);
            cmd.Parameters.AddWithValue("oper", operkisa);
            cmd.Parameters.AddWithValue("jnot", jnot);
            cmd.ExecuteNonQuery();
            cmd.Dispose();



          


            baglanti.Close();
            prosit();
        }
    }

    private bool IsDate2(string tarihyazi)
    {
        DateTime Temp;
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        if (DateTime.TryParse(tarihyazi, culture, System.Globalization.DateTimeStyles.AssumeLocal, out Temp) == true)
            return true;
        else
            return false;
    }


    protected void DDLJEap_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLJEap.SelectedItem.Text == "...")
        {
            DDLJEdp.ClearSelection();
            DDLJEdp.Items.FindByText("...").Selected = true;
            DDLJEdp.Enabled = false;

            //DDLJEdb.ClearSelection();
            //DDLJEdb.Items.FindByText("...").Selected = true;
            //DDLJEdb.Enabled = false;

            //DDLJEflag.ClearSelection();
            //DDLJEflag.Items.FindByText("...").Selected = true;
            //DDLJEflag.Enabled = false;

        }
        else
        {
            DDLJEdp.Enabled = true;

            //if (DDLJEdb.SelectedItem.Text != "...")
            //{
            //    DDLJEdb.Enabled = true;
            //    DDLJEdb.SelectedIndex = -1;
            //}

            //if (DDLJEflag.SelectedItem.Text != "...")
            //{
            //    DDLJEflag.Enabled = true;
            //    DDLJEflag.SelectedIndex = -1;
            //}
        }

        /// Romorkör satır renklendirme
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdtdoku = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtdoku.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdt = cmdtdoku.ExecuteReader();
        string bolge = "";
        int i = 1;

        if (drdt.HasRows)
        {
            while (drdt.Read())
            {
                bolge = drdt["bolge"].ToString();
                if (bolge == "2")
                {
                    DDLJEap.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdp.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdb.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEflag.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEextra.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                }
                i++;
            }
        }
        drdt.Close();
        cmdtdoku.Dispose();


        /// Palamar satır renklendirme
        SqlCommand cmdtdokumoor = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtdokumoor.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdtm = cmdtdokumoor.ExecuteReader();
        string bolgem = "";
        int im = 1;

        if (drdtm.HasRows)
        {
            while (drdtm.Read())
            {
                bolgem = drdtm["bolge"].ToString();
                if (bolgem == "2")
                {
                    DDLJEtip.Items[im].Attributes.Add("style", "background-color: Orange !important;");
                }
                im++;
            }
        }
        drdtm.Close();
        cmdtdokumoor.Dispose();


        baglanti.Close();

        this.ModalPopupjurnot.Show();
    }

    //protected void DDLJEdp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (DDLJEdp.SelectedItem.Text == "...")
    //    {
    //        DDLJEdb.ClearSelection();
    //        DDLJEdb.Items.FindByText("...").Selected = true;
    //        DDLJEdb.Enabled = false;

    //        DDLJEflag.ClearSelection();
    //        DDLJEflag.Items.FindByText("...").Selected = true;
    //        DDLJEflag.Enabled = false;

    //    }
    //    else
    //    {
    //        DDLJEdb.Enabled = true;
    //        //if (DDLJEdb.SelectedItem.Text != "...")
    //        //{
    //        //    
    //        //    DDLJEdb.SelectedIndex = -1;
    //        //}

    //        if (DDLJEflag.SelectedItem.Text != "...")
    //        {
    //            DDLJEflag.Enabled = true;
    //            DDLJEflag.SelectedIndex = -1;
    //        }
    //    }
    //    this.ModalPopupjurnot.Show();
    //}

    protected void DDLJEdb_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLJEdb.SelectedItem.Text == "...")
        {
            DDLJEflag.ClearSelection();
            DDLJEflag.Items.FindByText("...").Selected = true;
            DDLJEflag.Enabled = false;
        }
        else
        {
            DDLJEflag.Enabled = true;

        }



        /// Romorkör satır renklendirme
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdtdoku = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtdoku.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdt = cmdtdoku.ExecuteReader();
        string bolge = "";
        int i = 1;

        if (drdt.HasRows)
        {
            while (drdt.Read())
            {
                bolge = drdt["bolge"].ToString();
                if (bolge == "2")
                {
                    DDLJEap.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdp.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdb.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEflag.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEextra.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                }
                i++;
            }
        }
        drdt.Close();
        cmdtdoku.Dispose();


        /// Palamar satır renklendirme
        SqlCommand cmdtdokumoor = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtdokumoor.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdtm = cmdtdokumoor.ExecuteReader();
        string bolgem = "";
        int im = 1;

        if (drdtm.HasRows)
        {
            while (drdtm.Read())
            {
                bolgem = drdtm["bolge"].ToString();
                if (bolgem == "2")
                {
                    DDLJEtip.Items[im].Attributes.Add("style", "background-color: Orange !important;");
                }
                im++;
            }
        }
        drdtm.Close();
        cmdtdokumoor.Dispose();


        baglanti.Close();


        this.ModalPopupjurnot.Show();

    }

    private string TarihSaatYaziYapDMYhm(DateTime TarihsaatDMYhms)
    {
        string TarihsaatYaziok, moys, doms, hhs, mms;

        int dayofmonth = TarihsaatDMYhms.Day;
        int monthofyear = TarihsaatDMYhms.Month;
        int yearofdate = TarihsaatDMYhms.Year;
        int saatofnow = TarihsaatDMYhms.Hour;
        int dakikaofnow = TarihsaatDMYhms.Minute;

        if (monthofyear < 10)
        {
            moys = "0" + monthofyear.ToString();
        }
        else
        {
            moys = monthofyear.ToString();
        }

        if (dayofmonth < 10)
        {
            doms = "0" + dayofmonth.ToString();
        }
        else
        {
            doms = dayofmonth.ToString();
        }

        if (saatofnow < 10)
        {
            hhs = "0" + saatofnow.ToString();
        }
        else
        {
            hhs = saatofnow.ToString();
        }

        if (dakikaofnow < 10)
        {
            mms = "0" + dakikaofnow.ToString();
        }
        else
        {
            mms = dakikaofnow.ToString();
        }

        TarihsaatYaziok = doms + "." + moys + "." + yearofdate.ToString() + " " + hhs + ":" + mms;

        return TarihsaatYaziok;
    }


}




