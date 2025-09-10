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


public partial class Avardetup : System.Web.UI.Page
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
        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri2 = from b2 in entikib.vardiyadetay.Where(b2 => b2.grt.Contains("99999999")) select b2;
            GridView1.DataSource = veri2.ToList();
            GridView1.DataBind();
        }

        //if (DDLPilots.SelectedItem.Text!="") DDLPilots.Items.FindByText(DDLPilots.SelectedItem.Text).Selected = true;

        DataTable DTkaplar = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where  kapsirano<1000 order by kapadisoyadi asc");
        DDLPilots.Items.Clear();
        DDLPilots.DataTextField = "kapadisoyadi";
        DDLPilots.DataValueField = "kapno";
        DDLPilots.DataSource = DTkaplar;
        DDLPilots.DataBind();
        DDLPilots.Items.Insert(0, new ListItem("Select", String.Empty));
        DDLPilots.SelectedIndex = 0;

    }


    private void sirala()
    {
        int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());


        using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
        {
            entiki.Configuration.ProxyCreationEnabled = false;

            var veri = from b in entiki.vardiyadetay.Where(b => b.kapno == kapnoal).ToList() select b;

 //           foreach (var c in veri)
  //          {
   //             c.orderbay = TarihSaatYapDMYhm(c.istasyoncikis);  çok aşırı yavaş çalışıyordu
    //        }

				foreach (var c in veri)
				{
				c.orderbay = Convert.ToDateTime(c.istasyoncikis);
				}
				
				
			GridView1.DataSource = veri.OrderByDescending(x => x.orderbay).ToList().Take(50);

//            GridView1.DataSource = veri.OrderByDescending(b => b.orderbay).ToList().Take(50);  orderbay yerine id sıralaması da olur
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
        sirala();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int editisid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string gemiadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditgemi") as TextBox).Text.Trim().ToString().ToLower()));
        string binisyeri = AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditbinisyeri") as TextBox).Text.Trim().ToString());
        string inisyeri = AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditinisyeri") as TextBox).Text.Trim().ToString());
        string binisrihtim = AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditbinisrihtim") as TextBox).Text.Trim().ToString());
        string inisrihtim = AnaKlas.Temizle((GridView1.Rows[e.RowIndex].FindControl("TBeditinisrihtim") as TextBox).Text.Trim().ToString());
        string istasyoncikis = (GridView1.Rows[e.RowIndex].FindControl("TBciktime") as TextBox).Text;
        string pob = (GridView1.Rows[e.RowIndex].FindControl("TBpobtime") as TextBox).Text;
        string poff = (GridView1.Rows[e.RowIndex].FindControl("TBpofftime") as TextBox).Text;
        string istasyongelis = (GridView1.Rows[e.RowIndex].FindControl("TBontime") as TextBox).Text;
        string manevraiptal = (GridView1.Rows[e.RowIndex].FindControl("TBiptal") as TextBox).Text;

        if (gemiadi != "" && gemiadi != null && binisyeri!="" && binisyeri != null && inisyeri != "" && inisyeri != null && binisrihtim != "" && binisrihtim != null && inisrihtim != "" && inisrihtim != null && istasyoncikis != "" && istasyoncikis != null && manevraiptal != "" && manevraiptal != null)
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

                            SqlCommand issureokuc1 = new SqlCommand("Select istasyoncikis from vardiyadetay where id = " + editisid, baglanti);
                            string issureski1 = issureokuc1.ExecuteScalar().ToString();
                            issureokuc1.Dispose();
                            SqlCommand issureokuc2 = new SqlCommand("Select istasyongelis from vardiyadetay  where id = " + editisid, baglanti);
                            string issureski2 = issureokuc2.ExecuteScalar().ToString();
                            issureokuc2.Dispose();
                            decimal issuresieski = 0;
                            DateTime istasyoncikised = Convert.ToDateTime(issureski1);
                            DateTime istasyongelised = Convert.ToDateTime(issureski2);
                            TimeSpan iseski = istasyongelised - istasyoncikised;
                            issuresieski = Convert.ToDecimal(iseski.TotalHours.ToString());

                            int pilotkap =Convert.ToInt32(DDLPilots.SelectedItem.Value);


                            SqlCommand cmdvarbilgivarnookue = new SqlCommand("SP_varvarnooku", baglanti);
                            cmdvarbilgivarnookue.CommandType = CommandType.StoredProcedure;
                            cmdvarbilgivarnookue.Parameters.AddWithValue("@secilikapno", pilotkap);
                            cmdvarbilgivarnookue.Parameters.Add("@varnooku", SqlDbType.Char, 6);
                            cmdvarbilgivarnookue.Parameters["@varnooku"].Direction = ParameterDirection.Output;
                            cmdvarbilgivarnookue.ExecuteNonQuery();
                            string varnook = cmdvarbilgivarnookue.Parameters["@varnooku"].Value.ToString().Trim();
                            cmdvarbilgivarnookue.Dispose();

                            decimal issuresiy = 0;
                            DateTime istasyoncikisyd = Convert.ToDateTime(istasyoncikis);
                            DateTime istasyongelisyd = Convert.ToDateTime(istasyongelis);
                            TimeSpan tsher1 = istasyongelisyd - istasyoncikisyd;
                            issuresiy = Convert.ToDecimal(tsher1.TotalHours.ToString());

                            /* eski süreyi bulup toplamdan düş yeniyi ekle*/
                            SqlCommand topissurokuc = new SqlCommand("Select toplamissuresi from pilotvardiya where  varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
                            decimal tissureski = Convert.ToDecimal(topissurokuc.ExecuteScalar().ToString());
                            topissurokuc.Dispose();

                            decimal tissureyeni = Convert.ToDecimal(tissureski - issuresieski + issuresiy);



                            if (float.Parse(tissureyeni.ToString()) > 0.16) // iş süresi 10dk dan çoksa sisteme girer
                            {
                                SqlCommand cmdipoku = new SqlCommand("Select manevraiptal from vardiyadetay where id=" + editisid, baglanti);
                                string iporg = cmdipoku.ExecuteScalar().ToString();
                                cmdipoku.Dispose();

                                SqlCommand cmd = new SqlCommand("update vardiyadetay set gemiadi=@gemiadi, binisyeri=@binisyeri, inisyeri=@inisyeri, binisrihtim=@binisrihtim, inisrihtim=@inisrihtim, istasyoncikis=@istasyoncikis,pob=@pob,poff=@poff,istasyongelis=@istasyongelis,rangework=@rangework,manevraiptal=@manevraiptal where id=" + editisid, baglanti);
                                cmd.Parameters.AddWithValue("gemiadi", gemiadi);
                                cmd.Parameters.AddWithValue("binisyeri", binisyeri);
                                cmd.Parameters.AddWithValue("inisyeri", inisyeri);
                                cmd.Parameters.AddWithValue("binisrihtim", binisrihtim);
                                cmd.Parameters.AddWithValue("inisrihtim", inisrihtim);
                                cmd.Parameters.AddWithValue("istasyoncikis", istasyoncikis);
                                cmd.Parameters.AddWithValue("pob", pob);
                                cmd.Parameters.AddWithValue("poff", poff);
                                cmd.Parameters.AddWithValue("istasyongelis", istasyongelis);
                                cmd.Parameters.AddWithValue("rangework", issuresiy);
                                cmd.Parameters.AddWithValue("manevraiptal", manevraiptal);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();

                                SqlCommand topissuryazc = new SqlCommand("update pilotvardiya set toplamissuresi=@toplamissuresi where varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
                                topissuryazc.Parameters.AddWithValue("toplamissuresi", tissureyeni);
                                topissuryazc.ExecuteNonQuery();
                                topissuryazc.Dispose();

                                if (gemiadi != "TAKVİYE") {
                                    SqlCommand cmdistgel = new SqlCommand("update pilotlar set istasyongelis=@istasyongelis where kapno=" + pilotkap, baglanti);
                                    cmdistgel.Parameters.AddWithValue("istasyongelis", istasyongelis);
                                    cmdistgel.ExecuteNonQuery();
                                    cmdistgel.Dispose(); }

                                // iş iptal veya tekrar onaydaki ortak hesaplar..
                                /////******yorulma hesabı
                                string kalkislimani = "Yelkenkaya";
                                string yanasmalimani = "Yelkenkaya";
                                string kalkisrihtimi = "0";
                                string yanasmarihtimi = "0";
                                SqlCommand cmdgemibul = new SqlCommand("Select * from vardiyadetay where  id =" + editisid, baglanti);
                                SqlDataReader gemireader = cmdgemibul.ExecuteReader();

                                if (gemireader.HasRows)
                                {
                                    while (gemireader.Read())
                                    {
                                        kalkislimani = gemireader["binisyeri"].ToString();
                                        yanasmalimani = gemireader["inisyeri"].ToString();
                                    }
                                }
                                gemireader.Close();
                                cmdgemibul.Dispose();

                                if (kalkislimani.Substring(0, 4) == "Ters")
                                { kalkislimani = "Tersane Beşiktaş"; }
                                else if (kalkislimani.Substring(0, 4) == "Kosb")
                                { kalkislimani = "Kosbaş"; }
                                else if (kalkislimani == "Trial Voyage")
                                { kalkislimani = "Yelkenkaya"; }
                                else if (kalkislimani == "SafiPort")
                                { kalkislimani = "Demir-İzmit"; }
                                

                                if (yanasmalimani.Substring(0, 4) == "Ters")
                                { yanasmalimani = "Tersane Beşiktaş"; }
                                else if (yanasmalimani.Substring(0, 4) == "Kosb")
                                { yanasmalimani = "Kosbaş"; }
                                else if (yanasmalimani == "Trial Voyage")
                                { yanasmalimani = "Yelkenkaya"; }
                                else if (yanasmalimani == "SafiPort")
                                { yanasmalimani = "Demir-İzmit"; }

                                SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                                cmdLimannoal.CommandType = CommandType.StoredProcedure;
                                cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
                                cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int); // 
                                cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                                cmdLimannoal.ExecuteNonQuery();
                                int portnokalkis = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString().Trim());
                                cmdLimannoal.Dispose();

                                SqlCommand cmdLimannoal2 = new SqlCommand("SP_Lim_Limannoal", baglanti);
                                cmdLimannoal2.CommandType = CommandType.StoredProcedure;
                                cmdLimannoal2.Parameters.AddWithValue("@limanadi", yanasmalimani);
                                cmdLimannoal2.Parameters.Add("@limanno", SqlDbType.Int); // 
                                cmdLimannoal2.Parameters["@limanno"].Direction = ParameterDirection.Output;
                                cmdLimannoal2.ExecuteNonQuery();
                                int portnokalvar = Convert.ToInt32(cmdLimannoal2.Parameters["@limanno"].Value.ToString().Trim());
                                cmdLimannoal2.Dispose();

                                double fatikzihin = 0;

                                if (portnokalkis > 0 && portnokalkis < 900) // limandan kalkis
                                {
                                    if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                                    { fatikzihin = 1.9; }
                                    else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                                    { fatikzihin = 1.9; }
                                    else // sadece kalk
                                    { fatikzihin = 1.9; }
                                }

                                else if (portnokalkis > 1000 && portnokalkis < 1099) // demirden kalkis
                                {
                                    if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                                    { fatikzihin = 1.9; }
                                    else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                                    { fatikzihin = 1.9; }
                                    else // sadece demir al
                                    { fatikzihin = 1.9; }
                                }

                                else if (portnokalvar > 0 && portnokalvar < 900) // ykaya-limana yanaşma
                                { fatikzihin = 1.9; }

                                else if (portnokalvar > 1000 && portnokalvar < 1099) // ykaya-demir at
                                { fatikzihin = 1.9; }

                                if (yanasmarihtimi.IndexOf("Dbl.Anc") != -1)
                                { fatikzihin = 1.9; }

                                if (yanasmarihtimi.IndexOf("Dock") != -1)
                                { fatikzihin = 1.9; }

                                if (kalkisrihtimi.IndexOf("Dock") != -1)
                                { fatikzihin = 1.9; }

                                decimal fatikzihind = Convert.ToDecimal(fatikzihin.ToString());

                                SqlCommand topissurokucip = new SqlCommand("Select toplamdinlenme from pilotvardiya where  varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
                                decimal toplamdinlenmeeski = Convert.ToDecimal(topissurokucip.ExecuteScalar().ToString());
                                topissurokucip.Dispose();

                                if (manevraiptal == "0")
                                {
                                    if (iporg == "1") // iptal bir iş tekrar history e alınırsa yapılacak işlem
                                    {
                                        SqlCommand topissuryazcip = new SqlCommand("update pilotvardiya set toplamdinlenme=@toplamdinlenme where varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
                                        topissuryazcip.Parameters.AddWithValue("toplamdinlenme", toplamdinlenmeeski + fatikzihind);
                                        topissuryazcip.ExecuteNonQuery();
                                        topissuryazcip.Dispose();

                                        SqlCommand cmdipsfr = new SqlCommand("update vardiyadetay set manevraiptal=@manevraiptal where id=" + editisid, baglanti);
                                        cmdipsfr.Parameters.AddWithValue("manevraiptal", "0");
                                        cmdipsfr.ExecuteNonQuery();
                                        cmdipsfr.Dispose();
                                    }
                                }
                                else if (manevraiptal == "1")/* eski toplam zihinsel süreden sadece manevra fatiğini düş*/
                                {
                                    if (iporg == "0")
                                    {
                                    SqlCommand topissuryazcip = new SqlCommand("update pilotvardiya set toplamdinlenme=@toplamdinlenme where varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
                                    topissuryazcip.Parameters.AddWithValue("toplamdinlenme", toplamdinlenmeeski - fatikzihind);
                                    topissuryazcip.ExecuteNonQuery();
                                    topissuryazcip.Dispose();

                                    SqlCommand cmdipsfr = new SqlCommand("update vardiyadetay set manevraiptal=@manevraiptal where id=" + editisid, baglanti);
                                    cmdipsfr.Parameters.AddWithValue("manevraiptal", "1");
                                    cmdipsfr.ExecuteNonQuery();
                                    cmdipsfr.Dispose();
                                    }
                                    
                                }

                                GridView1.EditIndex = -1;
                                sirala();
                                
                            }
                            else // düzeltme 10 dk dan az ise düzeltme yapma bekle
                            {


                            }
                            baglanti.Close();

                           
                        }
                    }
                }
        }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        sirala();
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
            sirala();
        }
    }
    private bool delport(int delportno)
    {
        bool sonuc = false;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("Delete from vardiyadetay where id= " + delportno, baglanti);
        cmd.Parameters.AddWithValue("id", delportno);

        try
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            SqlCommand cmdgemibul = new SqlCommand("Select * from vardiyadetay where  id =" + delportno, baglanti);
            SqlDataReader gemireader = cmdgemibul.ExecuteReader();

            string binisyeri = "";
            string inisyeri = "";
            string binisrihtim = "";
            string inisrihtim = "";
            string istasyoncikis = "";
            string pob = "";
            string poff = "";
            string istasyongelis = "";

            if (gemireader.HasRows)
            {
                while (gemireader.Read())
                {
                    binisyeri = gemireader["binisyeri"].ToString();
                    binisrihtim = gemireader["binisrihtim"].ToString();
                    inisyeri = gemireader["inisyeri"].ToString();
                    inisrihtim = gemireader["inisrihtim"].ToString();
                    istasyoncikis = gemireader["istasyoncikis"].ToString();
                    pob = gemireader["pob"].ToString();
                    poff = gemireader["poff"].ToString();
                    istasyongelis = gemireader["istasyongelis"].ToString();

                }
            }
            gemireader.Close();
            cmdgemibul.Dispose();


            decimal issuresieski = 0;
            DateTime istasyoncikised = Convert.ToDateTime(istasyoncikis);
            DateTime istasyongelised = Convert.ToDateTime(istasyongelis);
            TimeSpan iseski = istasyongelised - istasyoncikised;
            issuresieski = Convert.ToDecimal(iseski.TotalHours.ToString());

            int pilotkap = Convert.ToInt32(DDLPilots.SelectedItem.Value);


            SqlCommand cmdvarbilgivarnookue = new SqlCommand("SP_varvarnooku", baglanti);
            cmdvarbilgivarnookue.CommandType = CommandType.StoredProcedure;
            cmdvarbilgivarnookue.Parameters.AddWithValue("@secilikapno", pilotkap);
            cmdvarbilgivarnookue.Parameters.Add("@varnooku", SqlDbType.Char, 6);
            cmdvarbilgivarnookue.Parameters["@varnooku"].Direction = ParameterDirection.Output;
            cmdvarbilgivarnookue.ExecuteNonQuery();
            string varnook = cmdvarbilgivarnookue.Parameters["@varnooku"].Value.ToString().Trim();
            cmdvarbilgivarnookue.Dispose();

            /////******manevra fatigi ve gece  hesabı

            string kalkislimani = binisyeri;
            string yanasmalimani = inisyeri;
            string kalkisrihtimi = binisrihtim;
            string yanasmarihtimi = inisrihtim;

            if (kalkislimani.Substring(0, 4) == "Ters")
            { kalkislimani = "Tersane Beşiktaş"; }
            else if (kalkislimani.Substring(0, 4) == "Kosb")
            { kalkislimani = "Kosbaş"; }
            else if (kalkislimani == "Trial Voyage")
            { kalkislimani = "Yelkenkaya"; }
            else if (kalkislimani == "SafiPort")
            { kalkislimani = "Demir-İzmit"; }


            if (yanasmalimani.Substring(0, 4) == "Ters")
            { yanasmalimani = "Tersane Beşiktaş"; }
            else if (yanasmalimani.Substring(0, 4) == "Kosb")
            { yanasmalimani = "Kosbaş"; }
            else if (yanasmalimani == "Trial Voyage")
            { yanasmalimani = "Yelkenkaya"; }
            else if (yanasmalimani == "SafiPort")
            { yanasmalimani = "Demir-İzmit"; }

            SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
            cmdLimannoal.CommandType = CommandType.StoredProcedure;
            cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
            cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int); // 
            cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
            cmdLimannoal.ExecuteNonQuery();
            int portnokalkis = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString().Trim());
            cmdLimannoal.Dispose();

            SqlCommand cmdLimannoal2 = new SqlCommand("SP_Lim_Limannoal", baglanti);
            cmdLimannoal2.CommandType = CommandType.StoredProcedure;
            cmdLimannoal2.Parameters.AddWithValue("@limanadi", yanasmalimani);
            cmdLimannoal2.Parameters.Add("@limanno", SqlDbType.Int); // 
            cmdLimannoal2.Parameters["@limanno"].Direction = ParameterDirection.Output;
            cmdLimannoal2.ExecuteNonQuery();
            int portnokalvar = Convert.ToInt32(cmdLimannoal2.Parameters["@limanno"].Value.ToString().Trim());
            cmdLimannoal2.Dispose();

            double fatikzihin = 0;

            if (portnokalkis > 0 && portnokalkis < 900) // limandan kalkis
            {
                if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                { fatikzihin = 1.9; }
                else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                { fatikzihin = 1.9; }
                else // sadece kalk
                { fatikzihin = 1.9; }
            }

            else if (portnokalkis > 1000 && portnokalkis < 1099) // demirden kalkis
            {
                if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                { fatikzihin = 1.9; }
                else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                { fatikzihin = 1.9; }
                else // sadece demir al
                { fatikzihin = 1.9; }
            }

            else if (portnokalvar > 0 && portnokalvar < 900) // ykaya-limana yanaşma
            { fatikzihin = 1.9; }

            else if (portnokalvar > 1000 && portnokalvar < 1099) // ykaya-demir at
            { fatikzihin = 1.9; }

            if (yanasmarihtimi.IndexOf("Dbl.Anc") != -1)
            { fatikzihin = 1.9; }

            if (yanasmarihtimi.IndexOf("Dock") != -1)
            { fatikzihin = 1.9; }

            if (kalkisrihtimi.IndexOf("Dock") != -1)
            { fatikzihin = 1.9; }


            //gece fatiği
            // 1. istasyoncikis 0 den önce ve istasyongelis 6 den sonra
            double gecefatiq = 0;
            float issuresigf = 0;

            TimeSpan ts = Convert.ToDateTime(istasyongelis) - Convert.ToDateTime(istasyoncikis);
            float issuresif = float.Parse(ts.TotalHours.ToString());

            // 2. istasyoncikis 0 den önce ve istasyongelis 0 ile 6 arası
            if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) > 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
            {
                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) > 14) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 24))
                {
                    TimeSpan tsgf = Convert.ToDateTime(istasyongelis) - (Convert.ToDateTime(istasyoncikis).Date.AddDays(1).AddMinutes(-1));
                    issuresigf = float.Parse(tsgf.TotalHours.ToString());
                    gecefatiq = (issuresigf * 35) / 100;
                }
            }
            // 3. istasyoncikis 0 ile 6 arası ve istasyongelis 0 ile 6 arası
            if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) > 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
            {
                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) > 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
                {
                    gecefatiq = (issuresif * 35) / 100;
                }
            }
            // 4. istasyoncikis 0 ile 6 arası ve istasyongelis 6 den sonra
            if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) > 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
            {
                if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) > 6) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 14))
                {
                    TimeSpan tsgf = Convert.ToDateTime(istasyongelis).Date.AddDays(1).AddMinutes(-1).AddHours(-18) - (Convert.ToDateTime(istasyoncikis));
                    issuresigf = float.Parse(tsgf.TotalHours.ToString());
                    gecefatiq = (issuresigf * 35) / 100;
                }
            }

            decimal fatikzihind = Convert.ToDecimal((fatikzihin + gecefatiq).ToString());

            /* eski süreyi bulup toplamdan düş */
            SqlCommand ctoplamissayisi = new SqlCommand("Select toplamissayisi from pilotvardiya where  varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
            int tissayieski = Convert.ToInt32(ctoplamissayisi.ExecuteScalar().ToString());
            ctoplamissayisi.Dispose();

            SqlCommand ctoplamissuresi = new SqlCommand("Select toplamissuresi from pilotvardiya where  varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
            decimal tissureski = Convert.ToDecimal(ctoplamissuresi.ExecuteScalar().ToString());
            ctoplamissuresi.Dispose();

            SqlCommand ctoplamdinlenme = new SqlCommand("Select toplamdinlenme from pilotvardiya where  varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
            decimal tisdineski = Convert.ToDecimal(ctoplamdinlenme.ExecuteScalar().ToString());
            ctoplamdinlenme.Dispose();

            SqlCommand topissuryazc = new SqlCommand("update pilotvardiya set toplamissuresi=@toplamissuresi, toplamdinlenme=@toplamdinlenme, toplamissayisi=@toplamissayisi where varno = '" + varnook + "' and kapno = " + pilotkap, baglanti);
            topissuryazc.Parameters.AddWithValue("toplamissuresi", tissureski - issuresieski);
            topissuryazc.Parameters.AddWithValue("toplamdinlenme", tisdineski - fatikzihind);
            topissuryazc.Parameters.AddWithValue("toplamissayisi", tissayieski - 1);
            topissuryazc.ExecuteNonQuery();
            topissuryazc.Dispose();

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
        sirala();
    }


    protected void adminsayfalar_Click(object sender, EventArgs e)
    {
        Response.Redirect("adminsayfalar.aspx");
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
}




