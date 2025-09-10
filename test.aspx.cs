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
using System.IO;
//using OfficeExcel = Microsoft.Office.Interop.Excel;


public partial class test : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {   

        SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"]=="" || (Session["kapno"]==null)  || Convert.ToInt32(Session["yetki"]) == 0 )
        {
            Response.Redirect("main.aspx");
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
                LiteralYaz();
                
            }

            if (Convert.ToInt32(Session["yetki"]) == 9)
            {
                btnExport.Visible = true;
                btnExportdown.Visible = true;
            }

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

        if (varbiter.Text != "")
        {
            if (AnaKlas.TarihSaatYapDMYhm(varbiter.Text) > (DateTime.Now))
            {
                decimal gzsaatd = AnaKlas.varrealtimesaat();
                int gzsaat = Convert.ToInt32(Math.Floor(Math.Abs(gzsaatd)));
                decimal gzdakikad = (gzsaatd - gzsaat) * 60;
                int gzdakika = Convert.ToInt32(Math.Floor(Math.Abs(gzdakikad)));
                string gzdakikas = gzdakika.ToString();
                if (gzdakika < 10) { gzdakikas = "0" + gzdakikas; }
                decimal gzsanid = (gzdakikad - gzdakika) * 60;
                int gzsani = Convert.ToInt32(Math.Floor(Math.Abs(gzsanid)));
                string gzsanis = gzsani.ToString();
                if (gzsani < 10) { gzsanis = "0" + gzsanis; }

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNunber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text + " / Passed Time: " + gzsaat.ToString() + ":" + gzdakikas + ":" + gzsanis;
            }
            else
            {
                LblVarid.ForeColor = System.Drawing.Color.Red;
                LblVarid.Text = "Please Change Watch ! ";
                LblVarno.Text = "";
                LblVarbasla.Text = "";
                LblVarbit.Text = "";
            }
        }
        else
        {
            LiteralYaz();
        }

    }




    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("main.aspx");
    }
    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"] == "" || (Session["kapno"] == null))
        {
            Response.Redirect("pmtr.aspx");
        }
        else
        {
            AnaKlas.logrecup(Session["kapno"].ToString());
            Session.Abandon();
            Response.Redirect("pmtr.aspx");
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






    protected void btnExport_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdNewShipListc = new SqlCommand("SP_Isliste_List_NewShipListc", baglanti);
        cmdNewShipListc.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdNewShipListc;
        DataSet ds = new DataSet();
        adapter.Fill(ds, DateTime.Now.ToString().Replace(':', '.'));

        baglanti.Close();

//        ExportDataSetToExcel(ds, Server.MapPath("~/"));
    }

    //protected void ExportDataSetToExcel(DataSet ds, string strPath)
    //{
    //    int inHeaderLength = 2, inColumn = 0, inRow = 0;
    //    System.Reflection.Missing Default = System.Reflection.Missing.Value;
    //    //Create Excel File
    //    //delete last Excel 
    //  //  var fi = new FileInfo(Server.MapPath("~/joblist.xlsx"));
    //  //  if (fi.Exists) File.Delete(Server.MapPath("~/joblist.xlsx"));

    //    strPath += @"joblist.xlsx";
    //    OfficeExcel.Application excelApp = new OfficeExcel.Application();
    //    OfficeExcel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);
    //    foreach (DataTable dtbl in ds.Tables)
    //    {
    //        //Create Excel WorkSheet
    //        OfficeExcel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets.Add
    //                (System.Reflection.Missing.Value, excelWorkBook.Worksheets[excelWorkBook.Worksheets.Count],
    //                System.Reflection.Missing.Value, System.Reflection.Missing.Value);
    //        //Create Excel WorkSheet other way
    //        //    OfficeExcel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets.Add(Default, excelWorkBook.Sheets[excelWorkBook.Sheets.Count], 1, Default);
    //        excelWorkSheet.Name = dtbl.TableName;//Name worksheet

    //        //Write Column Name
    //        for (int i = 0; i < dtbl.Columns.Count; i++)
    //            excelWorkSheet.Cells[inHeaderLength + 1, i + 1] = dtbl.Columns[i].ColumnName;

    //        //Write Rows
    //        for (int m = 0; m < dtbl.Rows.Count; m++)
    //        {
    //            for (int n = 0; n < dtbl.Columns.Count; n++)
    //            {
    //                inColumn = n + 1;
    //                inRow = inHeaderLength + 2 + m;
    //                excelWorkSheet.Cells[inRow, inColumn] = dtbl.Rows[m].ItemArray[n].ToString();
    //                //if (m % 2 == 0) //   tümtablo seçimi 
    //                //    excelWorkSheet.get_Range("A" + dtbl.Rows.Count.ToString(), "G" + dtbl.Columns.Count.ToString()).Interior.Color = System.Drawing.ColorTranslator.FromHtml("#FCE4D6");
    //            }
    //        }

    //        ////Excel Header
    //        OfficeExcel.Range cellRang = excelWorkSheet.get_Range("A3", "T3");
    //        cellRang.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#fD9Da1");
    //        cellRang.Font.Color = System.Drawing.Color.Black;
    //        cellRang.Font.Bold = true;
    //        cellRang.Font.Size = 12;
    //        excelWorkSheet.Cells[1, 1] = "JOB LIST / DATE: " + DateTime.Now.ToString();
    //        excelWorkSheet.get_Range("A1", "A1").Font.Bold = true;
    //        excelWorkSheet.get_Range("A1", "A1").Font.Size = 15;
    //        excelWorkSheet.get_Range("A1", "D1").MergeCells = true;
    //        excelWorkSheet.Columns.AutoFit();
    //        excelWorkSheet.get_Range("A1", "T" + (dtbl.Rows.Count + 4).ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
    //        //cellRang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
    //        //excelWorkSheet.get_Range("F5").EntireColumn.NumberFormat = "0.00";

    //    }

    //    //Delete First Page
    //    excelApp.DisplayAlerts = false;
    //    Microsoft.Office.Interop.Excel.Worksheet lastWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];
    //    lastWorkSheet.Delete();
    //    excelApp.DisplayAlerts = true;

    //    //Set Defualt Page
    //    excelApp.DisplayAlerts = false;

    //    (excelWorkBook.Sheets[1] as OfficeExcel._Worksheet).Activate();
    //    excelWorkBook.SaveAs(strPath, Default, Default, Default, false, Default, OfficeExcel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
    //    excelWorkBook.Close();
    //    excelApp.Quit();


    //}

    protected void btnExportdown_Click(object sender, ImageClickEventArgs e)
    {
        var fi = new FileInfo(Server.MapPath("~/joblist.xlsx"));
        if (fi.Exists)
        {
            Response.Redirect("joblist.xlsx");
        }

    }



}




