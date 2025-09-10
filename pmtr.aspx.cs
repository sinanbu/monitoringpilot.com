using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Management;
public partial class _Default : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {        
	//eski satır sil
		//SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmds = new SqlCommand("delete from onoffline where convert(datetime, login, 103) < convert(datetime,'" + DateTime.Now.AddDays(-365).ToString() + "',103) and kapno!='99' and kapno!='94'  and yetki!='8'", baglanti);
        //cmds.ExecuteNonQuery();
        //baglanti.Close();

        ////bir siteden bir bilgi almak örn: <title> </title> tagı arasındaki bilgiyi okumak
        //string adres = "http://www.monitoringpilot.com";
        //WebRequest istek = HttpWebRequest.Create(adres);
        //WebResponse cevap;
        //cevap = istek.GetResponse();
        //StreamReader donenbilgiler = new StreamReader(cevap.GetResponseStream());
        //string gelen = donenbilgiler.ReadToEnd();
        //int gelenbaslangic = gelen.IndexOf("<title>") + 7;
        //int gelenbitis = gelen.Substring(gelenbaslangic).IndexOf("</title>");
        //string baslik = gelen.Substring(gelenbaslangic, gelenbitis);

    }

    protected void IBE_Click(object sender, ImageClickEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmd = new SqlCommand("SP_pmtrlogin", baglanti);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@eposta", giruser.Text);
        cmd.Parameters.AddWithValue("@sifre", girpass.Text);
        cmd.Parameters.Add("@sonuc", SqlDbType.Int);
        cmd.Parameters["@sonuc"].Direction = ParameterDirection.Output;

        string pcip = "";

        try
        {
            cmd.ExecuteNonQuery();
            string dsonuc = cmd.Parameters["@sonuc"].Value.ToString();
            if (dsonuc == null || dsonuc == "") // kullanıcı yoksa hatalı bilgiyi kaydet
            {

                //deneme kayıt
                if(giruser.Text.Trim()!="")
                { 
            pcip = AnaKlas.Pcipal();
                if (pcip == "78.189.22.74") { pcip = "78.189.22.74.DO"; }
				else if (pcip=="212.156.49.226") { pcip = "212.156.49.226.DO"; }
                else if (pcip == "195.175.33.210") { pcip = "195.175.33.210.YO"; }
                else if (pcip == "78.186.197.207") { pcip = "78.186.197.207.DM"; }
                else if (pcip == "85.29.29.2") { pcip = "85.29.29.2.MED"; }
				else if (pcip == "85.97.185.243") { pcip = "85.97.185.243.MED"; }


                    //////////Anakart bilgi alma
                    //string mmodel = MotherboardInfo.Manufacturer;
                    //string mserino = MotherboardInfo.SerialNumber;
                    //////////

                    //kapnobul
                    string kapnobul = "0";

                SqlCommand cmdkapal = new SqlCommand("Select kapno from pilotlar where eposta = '" + giruser.Text.Trim() + "'  ", baglanti);
                if (cmdkapal.ExecuteScalar() != null)
                {
                    kapnobul = cmdkapal.ExecuteScalar().ToString();
                }

                string kayitzamani = AnaKlas.TarihSaatYaziYapDMYhm(DateTime.Now);
                SqlCommand cmd1 = new SqlCommand("insert into onoffline (kapno,kapadi,kapsoyadi,yetki,pcip,mmodel,mserino,login,logof,time) values(@kapno,@kapadi,@kapsoyadi,@yetki,@pcip,@mmodel,@mserino,@login,@logof,@time)", baglanti);
                cmd1.Parameters.AddWithValue("kapno", kapnobul);
                cmd1.Parameters.AddWithValue("kapadi", giruser.Text);
                cmd1.Parameters.AddWithValue("kapsoyadi", girpass.Text);
                cmd1.Parameters.AddWithValue("yetki", "8"); // olmayan kişi için
                cmd1.Parameters.AddWithValue("pcip", pcip);
                    cmd1.Parameters.AddWithValue("mmodel", "");
                    cmd1.Parameters.AddWithValue("mserino", "");
                cmd1.Parameters.AddWithValue("login", kayitzamani);
                cmd1.Parameters.AddWithValue("logof", "0");
                cmd1.Parameters.AddWithValue("time", 0.002);
                cmd1.ExecuteNonQuery();
                cmd1.Dispose();


                    if (pcip == "85.29.29.2.MED" || pcip == "85.97.185.243.MED")
                    {
                        string Subject = "MEDMARINE LogTry / " + DateTime.Now.ToString();
                        string Body = giruser.Text + " / " + girpass.Text;
                        AnaKlas.Mailtoone("info@monitoringpilot.com",Subject, Body);
                    }


                    if (kapnobul == "28")
                    {
                        string Subject = "DİKKAT! LogTry / ADMIN / " + DateTime.Now.ToString();
                        string Body = "TryPass: " + girpass.Text + " / "+pcip;
                        AnaKlas.Mailtoone("info@monitoringpilot.com",Subject, Body);
                    }

                }

                Labelgirhata.Text = "Wrong data Access Denied !";
            }
            else
            {
                Session["kapno"] = dsonuc;

                SqlCommand cmdyetki = new SqlCommand("SP_sesyetki", baglanti);
                cmdyetki.CommandType = CommandType.StoredProcedure;
                cmdyetki.Parameters.AddWithValue("@kapno", Convert.ToInt32(dsonuc));
                cmdyetki.Parameters.Add("@yetkisonuc", SqlDbType.Char, 1);
                cmdyetki.Parameters["@yetkisonuc"].Direction = ParameterDirection.Output;
                cmdyetki.ExecuteNonQuery();
                string dyetkisonuc = cmdyetki.Parameters["@yetkisonuc"].Value.ToString();
                Session["yetki"] = dyetkisonuc;
                cmdyetki.Dispose();

                //SqlCommand cmdkapadi = new SqlCommand("SP_seskapadi", baglanti);
                //cmdkapadi.CommandType = CommandType.StoredProcedure;
                //cmdkapadi.Parameters.Add("@kapno", Convert.ToInt32(dsonuc));
                //cmdkapadi.Parameters.Add("@kapadisonuc", SqlDbType.NVarChar, 30);
                //cmdkapadi.Parameters["@kapadisonuc"].Direction = ParameterDirection.Output;
                //cmdkapadi.ExecuteNonQuery();
                //string dkapadisonuc = cmdkapadi.Parameters["@kapadisonuc"].Value.ToString();
                //Session["kapadi"] = dkapadisonuc;
                //cmdkapadi.Dispose();

                //SqlCommand cmdkapsoyadi = new SqlCommand( "SP_seskapsoyadi",baglanti);
                //cmdkapsoyadi.CommandType = CommandType.StoredProcedure;
                //cmdkapsoyadi.Parameters.Add("@kapno", Convert.ToInt32(dsonuc));
                //cmdkapsoyadi.Parameters.Add("@kapsoyadisonuc", SqlDbType.NVarChar, 30);
                //cmdkapsoyadi.Parameters["@kapsoyadisonuc"].Direction = ParameterDirection.Output;
                //cmdkapsoyadi.ExecuteNonQuery();
                //string dkapsoyadisonuc = cmdkapsoyadi.Parameters["@kapsoyadisonuc"].Value.ToString();
                //Session["kapsoyadi"] = dkapsoyadisonuc;
                //cmdkapsoyadi.Dispose();

                logrec();

                if (Convert.ToInt32(Session["yetki"]) >= 0 && Convert.ToInt32(Session["yetki"]) < 9)
                {
                    if ((Session["kapno"].ToString()) == "99" || (Session["kapno"].ToString()) == "109")
                    { Response.Redirect("izmitvts.aspx"); }


                    else if (Session["kapno"].ToString() == "140")
                    {
                        if (pcip == "195.175.33.210") { Response.Redirect("sipy.aspx"); }
                        else { Response.Redirect("sipd.aspx"); }
                    }

                    else if (Session["yetki"].ToString() == "8") { Response.Redirect("stn.aspx"); }
					
                    else
                    { Response.Redirect("main.aspx"); }

                }
                else if (Convert.ToInt32(Session["yetki"]) == 9)
                {
                    Response.Redirect("yonet/yonetim.aspx");
                }
                else
                    Response.Write("Error! Unauthorized Attempt Received.");
            }
        }
        catch (Exception)
        {
            Labelgirhata.Text = "Access Denied !";
        }
        finally
        {
            cmd.Dispose();
            baglanti.Close();
            baglanti.Dispose();
        }
    }

    protected void logrec()
    {
        SqlConnection baglanti = AnaKlas.baglan();

        //kopuğu hattan silme
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);
        if (cmdlogofbak.ExecuteScalar() != null)
        {
            int ofid = Convert.ToInt32(cmdlogofbak.ExecuteScalar().ToString());
            SqlCommand cmdlogofup = new SqlCommand("update onoffline set time=@time where id ='" + ofid + "' ", baglanti);
            cmdlogofup.Parameters.AddWithValue("time", 0.002);
            cmdlogofup.ExecuteNonQuery();
            cmdlogofup.Dispose();
        }
        cmdlogofbak.Dispose();

        //hattan silme darıca
        if (Session["yetki"].ToString() == "1")
        {
            SqlCommand cmdoprdbak = new SqlCommand("Select id from onoffline where (yetki='1' and time = 0.001) ", baglanti);
            if (cmdoprdbak.ExecuteScalar() != null)
            {
                int oprdofid = Convert.ToInt32(cmdoprdbak.ExecuteScalar().ToString());
                cmdoprdbak.Dispose();

                SqlCommand cmdlogofupd = new SqlCommand("update onoffline set time=@time where id ='" + oprdofid + "' ", baglanti);
                cmdlogofupd.Parameters.AddWithValue("time", 0.002);
                cmdlogofupd.ExecuteNonQuery();
                cmdlogofupd.Dispose();
            }
        }

        //hattan silme yarımca
        else if (Session["yetki"].ToString() == "2")
        {
            SqlCommand cmdoprybak = new SqlCommand("Select id from onoffline where (yetki='2' and time = 0.001) ", baglanti);
            if (cmdoprybak.ExecuteScalar() != null)
            {
                int opryofid = Convert.ToInt32(cmdoprybak.ExecuteScalar().ToString());
                cmdoprybak.Dispose();

                SqlCommand cmdlogofupd = new SqlCommand("update onoffline set time=@time where id ='" + opryofid + "' ", baglanti);
                cmdlogofupd.Parameters.AddWithValue("time", 0.002);
                cmdlogofupd.ExecuteNonQuery();
                cmdlogofupd.Dispose();
            }
        }

        //yeni kayıt

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

        string pcip = AnaKlas.Pcipal();
                if (pcip == "78.189.22.74") { pcip = "78.189.22.74.DO"; }
				else if (pcip=="212.156.49.226") { pcip = "212.156.49.226.DO"; }
                else if (pcip == "195.175.33.210") { pcip = "195.175.33.210.YO"; }
                else if (pcip == "78.186.197.207") { pcip = "78.186.197.207.DM"; }
                else if (pcip == "85.29.29.2") { pcip = "85.29.29.2.MED"; }
				else if (pcip == "85.97.185.243") { pcip = "85.97.185.243.MED"; }

        //////////Anakart bilgi alma
        //string mmodel = MotherboardInfo.Manufacturer;
        //string mserino = MotherboardInfo.SerialNumber;
        //////////

        string kayitzamani = AnaKlas.TarihSaatYaziYapDMYhm(DateTime.Now);
        SqlCommand cmd = new SqlCommand("insert into onoffline (kapno,kapadi,kapsoyadi,yetki,pcip,mmodel,mserino,login,logof,time) values(@kapno,@kapadi,@kapsoyadi,@yetki,@pcip,@mmodel,@mserino,@login,@logof,@time)", baglanti);
        cmd.Parameters.AddWithValue("kapno", Session["kapno"]);
        cmd.Parameters.AddWithValue("kapadi", seskapadis);
        cmd.Parameters.AddWithValue("kapsoyadi", seskapsoyadis);
        cmd.Parameters.AddWithValue("yetki", Session["yetki"]);
        cmd.Parameters.AddWithValue("pcip", pcip);
        cmd.Parameters.AddWithValue("mmodel", "");
        cmd.Parameters.AddWithValue("mserino", "");
        cmd.Parameters.AddWithValue("login", kayitzamani);
        cmd.Parameters.AddWithValue("logof", kayitzamani);
        cmd.Parameters.AddWithValue("time", 0.001);
        cmd.ExecuteNonQuery();
        cmd.Dispose();

        if (Session["kapno"].ToString() == "28")
        {
            string Subject = "Login / Kpt.Sinan Buğdaycı / " + DateTime.Now.ToString();
            string Body = "PCIP : " + pcip;
            AnaKlas.Mailtoone("info@monitoringpilot.com",Subject, Body);
        }



        baglanti.Close();


    }



    public string Temizle(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace("'", "");
        deger = deger.Replace("-", "");
        deger = deger.Replace("+", "");
        deger = deger.Replace("*", "");
        deger = deger.Replace("/", "");
        deger = deger.Replace("_", "");
        deger = deger.Replace("$", "");
        deger = deger.Replace("{", "");
        deger = deger.Replace("}", "");
        deger = deger.Replace("=", "");
        deger = deger.Replace(")", "");
        deger = deger.Replace("(", "");
        deger = deger.Replace("#", "");
        deger = deger.Replace("^", "");
        deger = deger.Replace("@", "");
        deger = deger.Replace("|", "");
        deger = deger.Replace("~", "");
        deger = deger.Replace("¨", "");
        deger = deger.Replace(" ", "");
        deger = deger.Replace("<", "");
        deger = deger.Replace(">", "");
        deger = deger.Replace("&", "");
        deger = deger.Replace("[", "");
        deger = deger.Replace("]", "");
        deger = deger.Replace(";", "");
        deger = deger.Replace("?", "");
        deger = deger.Replace("%", "");
        deger = deger.Replace("!", "");
        return deger;
    }


    //protected void logrec()
    //{
    //    SqlConnection baglanti = AnaKlas.baglan();

    //    if (Session["kapno"] == "" || (Session["kapno"] == null) || Convert.ToInt32(Session["yetki"]) < 0 || Convert.ToInt32(Session["yetki"]) > 9)
    //    {

    //       

    //        //else
    //        //{
    //        //    SqlCommand cmdtwicebak = new SqlCommand("Select pcip from onoffline where (kapno='" + Session["kapno"] + "' and time = 0.001) ", baglanti);
    //        //    if (cmdtwicebak.ExecuteScalar() != null)
    //        //    {
    //        //        string ilkofip = cmdtwicebak.ExecuteScalar().ToString();
    //        //        if (ilkofip != AnaKlas.Pcipal().ToString()) // userın ilk kaydı kontrol ediliyor eğer kullanıcı ip farklı ise ilkini sil
    //        //        {
    //        //            SqlCommand cmdlogofip = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);
    //        //            if (cmdlogofip.ExecuteScalar() != null)
    //        //            {
    //        //                int ofid = Convert.ToInt32(cmdlogofip.ExecuteScalar().ToString());
    //        //                SqlCommand cmdlogofup = new SqlCommand("update onoffline set time=@time where id ='" + ofid + "' ", baglanti);
    //        //                cmdlogofup.Parameters.AddWithValue("time", 0.002);
    //        //                cmdlogofup.ExecuteNonQuery();
    //        //                cmdlogofup.Dispose();
    //        //            }
    //        //            cmdlogofip.Dispose();
    //        //        }
    //        //    }

    //        //    cmdtwicebak.Dispose();

    //        //}

    //        else if (Session["yetki"].ToString() != "1" || Session["yetki"].ToString() != "2")
    //        {
    //            //kopuğu hattan silme
    //            SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);
    //            if (cmdlogofbak.ExecuteScalar() != null)
    //            {
    //                int ofid = Convert.ToInt32(cmdlogofbak.ExecuteScalar().ToString());
    //                SqlCommand cmdlogofup = new SqlCommand("update onoffline set time=@time where id ='" + ofid + "' ", baglanti);
    //                cmdlogofup.Parameters.AddWithValue("time", 0.002);
    //                cmdlogofup.ExecuteNonQuery();
    //                cmdlogofup.Dispose();
    //            }
    //            else
    //            {
    //                //yeni kayıt
    //                string kayitzamani = AnaKlas.TarihSaatYaziYapDMYhm(DateTime.Now);
    //                SqlCommand cmd = new SqlCommand("insert into onoffline (kapno,kapadi,kapsoyadi,yetki,pcip,login,logof,time) values(@kapno,@kapadi,@kapsoyadi,@yetki,@pcip,@login,@logof,@time)", baglanti);
    //                cmd.Parameters.AddWithValue("kapno", Session["kapno"]);
    //                cmd.Parameters.AddWithValue("kapadi", Session["kapadi"]);
    //                cmd.Parameters.AddWithValue("kapsoyadi", Session["kapsoyadi"]);
    //                cmd.Parameters.AddWithValue("yetki", Session["yetki"]);
    //                cmd.Parameters.AddWithValue("pcip", AnaKlas.Pcipal());
    //                cmd.Parameters.AddWithValue("login", kayitzamani);
    //                cmd.Parameters.AddWithValue("logof", kayitzamani);
    //                cmd.Parameters.AddWithValue("time", 0.001);
    //                cmd.ExecuteNonQuery();
    //                cmd.Dispose();
    //            }
    //            cmdlogofbak.Dispose();

    //        }

    //    }



    //        baglanti.Close();


    //}


    ////////Anakart bilgi alma klası
    //static public class MotherboardInfo
    //{
    //    private static ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
    ////  private static ManagementObjectSearcher motherboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");

    //    static public string Manufacturer
    //    {
    //        get
    //        {
    //            try
    //            {
    //                foreach (ManagementObject queryObj in baseboardSearcher.Get())
    //                {
    //                    return queryObj["Manufacturer"].ToString();
    //                }
    //                return "";
    //            }
    //            catch (Exception e)
    //            {
    //                return "";
    //            }
    //        }
    //    }

    //    static public string SerialNumber
    //    {
    //        get
    //        {
    //            try
    //            {
    //                foreach (ManagementObject queryObj in baseboardSearcher.Get())
    //                {
    //                    return queryObj["SerialNumber"].ToString();
    //                }
    //                return "";
    //            }
    //            catch (Exception e)
    //            {
    //                return "";
    //            }
    //        }
    //    }
    //}


}

