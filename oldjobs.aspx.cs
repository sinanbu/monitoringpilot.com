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


public partial class oldjobs : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2" && Session["yetki"].ToString() != "9"  && Session["yetki"].ToString() != "4" && Session["kapno"].ToString() != "112" && Session["kapno"].ToString() != "122" && Session["kapno"].ToString() != "124" && Session["kapno"].ToString() != "143")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else //if (cmdlogofbak.ExecuteScalar() != null)
        {

            if (!Page.IsPostBack)
            {
                this.Page.Form.DefaultButton = this.LBgetist3.UniqueID;

                if (Session["yetki"].ToString() == "9")
                { LBonline.Enabled = true; 
                    Bdaricaships.Visible = false;
                    Byarimcaships.Visible = false;              
                }
                else if (Session["kapno"].ToString() == "112" || Session["kapno"].ToString() == "122" || Session["kapno"].ToString() == "124" || Session["kapno"].ToString() == "143")
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

                DataTable DTkaplar = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where  (kapsirano<1000 and emekli='No') order by kapadisoyadi asc");
                DDLPilots.Items.Clear();
                DDLPilots.DataTextField = "kapadisoyadi";
                DDLPilots.DataValueField = "kapno";
                DDLPilots.DataSource = DTkaplar;
                DDLPilots.DataBind();
                DDLPilots.Items.Insert(0, new ListItem("All Pilots", String.Empty));
                DDLPilots.SelectedIndex = 0;


            }
            gridload();

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

        if (Session["kapno"].ToString() == "122" || Session["kapno"].ToString() == "124" || Session["kapno"].ToString() == "143")
        {
            menumenu.Visible = false;
            Bmain.Visible = false;
            Bdaricaships.Visible = false;
            Byarimcaships.Visible = false;
        }
        else if (Session["kapno"].ToString() == "96" || Session["kapno"].ToString() == "100" || Session["kapno"].ToString() == "112")
        {
            mainmanu1.Visible = false;
            mainmanu2.Visible = true;
            mainmanu4.Visible = true;
            mainmanu7.Visible = false;
            Bdaricaships.Visible = false;
            Byarimcaships.Visible = false;
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
            GridView7.DataSource = veri2.ToList();
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
        LBgetist3.Visible = true;
        Lwoidgunluk.Visible = true;
    }

    protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView7.PageIndex = e.NewPageIndex;
        string shipal = AnaKlas.Temizle(TBshipname.Text.ToString());
        if (!String.IsNullOrEmpty(shipal) && shipal.Length > 1)
        { //isim arra


            if (DDLPilots.SelectedItem.Value == null || DDLPilots.SelectedItem.Value == "" || DDLPilots.SelectedItem.Value == "0")
            {
                using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
                {

                    shipal = shipal.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ");
                    var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ").Contains(shipal)).ToList() select b;

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
                        c.orderbay = Convert.ToDateTime(c.pob);
                    }

                    GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView7.DataBind();
                    Lwoidgunluk.Text = veri.Count() + " movements of " + shipal;
                }
            }
            else
            {
                int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());
                using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
                {
                    shipal = shipal.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ");
                    var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ").Contains(shipal) & b.kapno == kapnoal).ToList() select b;

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
                        c.orderbay = Convert.ToDateTime(c.pob);
                    }

                    GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView7.DataBind();
                    Lwoidgunluk.Text = veri.Count() + " movements of " + shipal;
                }
            }


        }


        else
        {

            if (DDLPilots.SelectedItem.Value == null || DDLPilots.SelectedItem.Value == "" || DDLPilots.SelectedItem.Value == "0")
            {
                Lwoidgunluk.Text = "Please Select a Pilot or Enter min. 2 characters for ships name.";
            }
            else
            {
                int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());
                using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
                {
                    var veri = from b in entikib.vardiyadetay.Where(b => b.kapno == kapnoal).ToList() select b;

                    SqlConnection baglanti = AnaKlas.baglan();
                    SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                    cmdPilotismial.CommandType = CommandType.StoredProcedure;
                    cmdPilotismial.Parameters.AddWithValue("@secilikapno", kapnoal);
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


                    foreach (var c in veri)
                    {
                        c.orderbay = Convert.ToDateTime(c.pob);
                        c.pilotismi = isimal;
                    }

                    GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView7.DataBind();
                    Lwoidgunluk.Text = veri.Count() + " movements of " + shipal;
                }
            }

        }



    }

    protected void LBgetist3_Click(object sender, EventArgs e)
    {
        string shipal = AnaKlas.Temizle(TBshipname.Text.ToString());

        if (!String.IsNullOrEmpty(shipal) && shipal.Length > 1)
        { //isim arra


            if (DDLPilots.SelectedItem.Value == null || DDLPilots.SelectedItem.Value == "" || DDLPilots.SelectedItem.Value == "0" )
            {
                using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
                {

                    shipal = shipal.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ");
                    var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ").Contains(shipal)).ToList() select b;

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
                        c.orderbay = Convert.ToDateTime(c.pob);
                    }

                    GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView7.DataBind();
                    Lwoidgunluk.Text = veri.Count() + " movements of " + shipal;
                }
            }
            else
            {
            int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());
                using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
                {
                    shipal = shipal.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ");
                    var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ").Contains(shipal) & b.kapno==kapnoal).ToList() select b;

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
                        c.orderbay = Convert.ToDateTime(c.pob);
                    }

                    GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView7.DataBind();
                    Lwoidgunluk.Text = veri.Count() + " movements of " + shipal;
                }
            }


        }


        else
        {

            if (DDLPilots.SelectedItem.Value == null || DDLPilots.SelectedItem.Value == "" || DDLPilots.SelectedItem.Value == "0")
            {
                Lwoidgunluk.Text = "Please Select a Pilot or Enter min. 2 characters for ships name.";
            }
            else
            {
                int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());
                using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
                {
                    var veri = from b in entikib.vardiyadetay.Where(b => b.kapno == kapnoal).ToList() select b;

                    SqlConnection baglanti = AnaKlas.baglan();
                    SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                    cmdPilotismial.CommandType = CommandType.StoredProcedure;
                    cmdPilotismial.Parameters.AddWithValue("@secilikapno", kapnoal);
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


                    foreach (var c in veri)
                    {
                        c.orderbay = Convert.ToDateTime(c.pob);
                        c.pilotismi = isimal;
                    }

                    GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView7.DataBind();
                    Lwoidgunluk.Text = veri.Count() + " movements of " + shipal;
                }
            }






            //Lwoidgunluk.Text = "Please Enter min. 2 characters.";
            //TBshipname.Text = "";
            //DDLPilots.SelectedIndex = 0;
            //using (PilotdbEntities2 isliste = new PilotdbEntities2())
            //{
            //    var veri = from b in isliste.isliste.Where(x => x.grt == "99999999").Take(20) select b;
            //    GridView7.DataSource = veri.ToList();
            //    GridView7.DataBind();
            //}
        }

    }

    //private void tusklik()
    //{
    //    string shipal = AnaKlas.Temizle(TBshipname.Text.ToString());

    //    if (!String.IsNullOrEmpty(shipal) && shipal.Length > 1)
    //    { //isim arra
    //        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
    //        {

    //            shipal = shipal.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ");
    //            var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi.ToLower().Replace("ı", "i").Replace("o", "ö").Replace("u", "ü").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("-", " ").Contains(shipal)).ToList() select b;

    //            foreach (var c in veri)
    //            {
    //                SqlConnection baglanti = AnaKlas.baglan();
    //                SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
    //                cmdPilotismial.CommandType = CommandType.StoredProcedure;
    //                cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(c.degismecikapno));
    //                string isimal = "";
    //                SqlDataReader dr = cmdPilotismial.ExecuteReader();
    //                if (dr.HasRows)
    //                {
    //                    while (dr.Read())
    //                    {
    //                        isimal = dr["kapadisoyadi"].ToString();
    //                    }
    //                }
    //                dr.Close();
    //                cmdPilotismial.Dispose();

    //                baglanti.Close();
    //                c.pilotismi = isimal;
    //                c.orderbay = Convert.ToDateTime(c.pob);
    //            }

    //            GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
    //            GridView7.DataBind();
    //            Lwoidgunluk.Text = veri.Count() + " movements of " + shipal;
    //        }
    //    }


    //    else
    //    {
    //        Lwoidgunluk.Text = "Please Enter min. 2 characters.";
    //        TBshipname.Text = "";
    //        using (PilotdbEntities2 isliste = new PilotdbEntities2())
    //        {
    //            var veri = from b in isliste.isliste.Where(x => x.grt == "99999999").Take(20) select b;
    //            GridView7.DataSource = veri.ToList();
    //            GridView7.DataBind();
    //        }
    //    }

    //}

    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if ((Session["kapno"] == null) || (Session["kapno"].ToString() == "")  )
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
        if (Convert.ToInt32(Session["yetki"]) == 1 || Convert.ToInt32(Session["yetki"]) == 2)
        {
            //          Response.Redirect("pmtr.aspx");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 0)
        {
            Response.Redirect("stn.aspx");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }



//    protected void DDLPilots_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        if ((DDLPilots.SelectedItem.Value) != "") { 

//        int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());

//        if(kapnoal == 0) {
//            tusklik();
//        }
//        else { 
//        using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
//        {
//            entiki.Configuration.ProxyCreationEnabled = false; // bütün entitilere eklenecek.
//                                                               //           System.Globalization.CultureInfo trTR = new System.Globalization.CultureInfo("tr-TR");
//                                                               //            .OrderByDescending(b => Convert.ToDateTime(b.istasyoncikis.ToString(),trTR))
//            var veri = from b in entiki.vardiyadetay.Where(b => b.kapno == kapnoal || b.degismecikapno == kapnoal).ToList() select b;

//            foreach (var c in veri)
//            {
//                string tipi = "";
//                string tipik = "";
//                if (c.tip.ToString() == "" || c.tip.ToString() == null)
//                {
//                    tipi = tipik;
//                }
//                else
//                {
//                    tipi = c.tip.ToString();
//                    c.tipi = tipi.Substring(0, 1);
//                }
//                c.orderbay = Convert.ToDateTime(c.istasyoncikis);

//                string kapadisoyadi = "";

//                if (c.kapno == c.degismecikapno)
//                { c.pilotismi = DDLPilots.SelectedItem.Text.ToString(); }
//                else
//                {

//                    if (c.kapno.ToString() == DDLPilots.SelectedItem.Value.ToString())
//                    {
//                        SqlConnection baglanti = AnaKlas.baglan();
//                        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
//                        cmdPilotismial.CommandType = CommandType.StoredProcedure;
//                        cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.degismecikapno);
//                        SqlDataReader dr = cmdPilotismial.ExecuteReader();
//                        if (dr.HasRows)
//                        {
//                            while (dr.Read())
//                            {
//                                kapadisoyadi = dr["kapadisoyadi"].ToString();
//                            }
//                        }
//                        dr.Close();
//                        cmdPilotismial.Dispose();
//                        baglanti.Close();

//                        c.pilotismi = DDLPilots.SelectedItem.Text.ToString() + "(" + kapadisoyadi + ")";
//                    }

//                    else if (c.degismecikapno.ToString() == DDLPilots.SelectedItem.Value.ToString())
//                    {
//                        SqlConnection baglanti = AnaKlas.baglan();
//                        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
//                        cmdPilotismial.CommandType = CommandType.StoredProcedure;
//                        cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.kapno);
//                        SqlDataReader dr = cmdPilotismial.ExecuteReader();
//                        if (dr.HasRows)
//                        {
//                            while (dr.Read())
//                            {
//                                kapadisoyadi = dr["kapadisoyadi"].ToString();
//                            }
//                        }
//                        dr.Close();
//                        cmdPilotismial.Dispose();
//                        baglanti.Close();
//                        c.pilotismi = kapadisoyadi + "(" + DDLPilots.SelectedItem.Text.ToString() + ")";
//                    }
//                }




//            }

           
//            GridView7.DataSource = veri.OrderByDescending(b => b.orderbay).ToList();
//            GridView7.DataBind();

//            }
//        }
//    }
//}

}