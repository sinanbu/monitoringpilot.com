<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statistics.aspx.cs" Inherits="yonet_statistics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />


</head>

   

<body >
    <form id="form1" runat="server">
   <div id="main">
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
   <ul id="top-navigation">
      <li><span><span><a href="yonetim.aspx">Settings</a></span></span></li>
      <li  class="active" ><span><span>Statistics</span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li runat="server" id="menuonline"><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>

    </ul>
  </div>
  <div id="middle">

    <div id="left-column-aralik"> <!--  id="ulnavlihover" -->
     
   
</div>

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>

    <div id="center-column-full">       
      <div class="top-bar-full"> 
 <h1>Statistics</h1>
        <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar"> </div>
 <div class="clear"> <!--  id="ulnavlihover" -->
      <ul class="navfull">

            <li class="liistatik1">  <asp:LinkButton ID="liistatik1" runat="server"  OnClick="liistatik1_Click">L3 Summary</asp:LinkButton></li>
<%--            <li class="liistatik1">  <asp:LinkButton ID="liistatik2" runat="server"  OnClick="liistatik2_Click">2M Summary</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik3" runat="server"  OnClick="liistatik3_Click">1Y Summary</asp:LinkButton></li>--%>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik4" runat="server"  OnClick="liistatik4_Click">Per Pilot</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik5" runat="server"  OnClick="liistatik5_Click">Per Ship</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik6" runat="server"  OnClick="liistatik6_Click">Per Terminal</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik7" runat="server"  OnClick="liistatik7_Click">One Day</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik8" runat="server"  OnClick="liistatik8_Click">One Month</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik9" runat="server"  OnClick="liistatik9_Click">One Year</asp:LinkButton></li>
</ul>  </div>

        <div class="clearup">
     
         
            <br /><br />
        <h2>Introduction for Statistics Pages.</h2>

        <h3>1. Last 3 Watch Summary</h3>
        Statistics summaries of last 3 watches can be monitored in L3 Summary page.  

        <h3>2. 2 Monthly Summary</h3>
        Two monthly statistics summaries of last 3 watches can be monitored in 2M Summary page. 

        <h3>3. Annually Summary</h3>
        Annually Statistics summaries of last 3 watches can be monitored in 1Y Summary page. 

                    
        <h3>4. Per Pilot</h3>
        All maneouvers of a pilot can easily found in Per Pilot page.

        <h3>5. Per Ship</h3>
        All maneouvers of a ship, which visited to Ýzmit bay, can easily found in Per Ship page.

        <h3>6. Per Terminal</h3>
        All vessels maneouvers in Ýzmit and Yalova terminals can easily found in Per Terminal page.

        <h3>7. One Day</h3>
         All maneouver informations of a vessel and pilot in any day can easily found in One Day page.
        <h3>8. One Month</h3>
         All maneouver informations of a vessel and pilot in any month can easily found in One Month page.
        <h3>9. One Year</h3>
         All maneouver informations of a vessel and pilot in any year can easily found in One Year page.
             <br /><br /> <br />
            
            

                 </ContentTemplate></asp:UpdatePanel>

      </div>
       
    </div>
    
  </div>
  <div id="footer"></div>   




</div>





    
  
    </form>
</body>
</html>
