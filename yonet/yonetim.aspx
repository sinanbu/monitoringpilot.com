<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yonetim.aspx.cs" Inherits="yonet_yonetim" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
   <div id="main">
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right">PMTR Admin Page</div>
    <ul id="top-navigation">
      <li class="active" ><span><span>Settings</span></span></li>
      <li><span><span><a href="Statistics.aspx">Statistics</a></span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li  runat="server" id="menuonline"><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>

    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> <div id="menuleft"  runat="server">
      <h3>Users</h3>
      <ul class="nav">
        <li><a href="pilots.aspx">User List</a></li>
      </ul>

      <h3>Places</h3>
      <ul class="nav">
        <li><a href="portsandberths.aspx">Ports and Berths</a></li>
        <li><a href="anchorageplaces.aspx">Anchorage Places</a></li>

      </ul>

      <h3>Distances</h3><ul class="nav">
        <li><a href="distances.aspx">Distances</a></li></ul>

      <h3>Flags</h3> <ul class="nav">        
        <li><a href="flags.aspx">Flags</a></li></ul>

      <h3>Ship Types</h3><ul class="nav">
        <li><a href="shiptypes.aspx">Ship Types</a></li></ul>

      <h3>Pilot Stations</h3><ul class="nav">
        <li><a href="pilotstations.aspx">Pilot Stations</a></li></ul>

</div>
</div>






    <div id="center-column">
      <div class="top-bar"> 
        <h1>Settings</h1> 
        <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar">

      </div>

        <h2>How To Use PMTR Admin interface.</h2>
        <h3>1. Settings</h3>
        All informations using in system have been listed in "Settings" tabs. Those infos can be changed simply by selection related section at left menu list. To change related data pls click the desired menu list item from the left menu. Then click edit icon in the table. After amending data don't forget to click save icon.

        <h3>2. Statistics</h3>
        Statistics informations can be monitor from top menu item "Statistics". Those datas can be save in a seperate file and printout easily via "save as" button in that page.

        <h3>3. Vacations</h3>
        Pilot's vacations dates can be monitored and updated easily via top menu item "Vacations". That page can be followed by all user. 
        
        <h3>4. Online Users</h3>
        All online user who signing in the page at the same time, easily found in that page which listed as "Online Users" in top menu item.

        <h3>5. Pilot Monitor</h3>
        Pilot watch and ship information can be monitored easily via top menu item "Pilot Moninor". That monitor page can be followed by all user. But Operation users use a detailed monitor page.

        <h3>6. Sign Off</h3>
        To leave the system and sign off to click the "Sign Off" item of top <asp:LinkButton ID="LBA" runat="server" Text="menu" Font-Underline="false" BackColor="Transparent" BorderWidth="0"  Font-Size="Small"  OnClick="LBA_Click"></asp:LinkButton>.

    </div>
    
  </div>
  <div id="footer"></div>   




</div>





    
  
    </form>
</body>
</html>
