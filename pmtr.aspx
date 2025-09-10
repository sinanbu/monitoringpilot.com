<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pmtr.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" type="text/css" href="css/stil.css" />
 <%--   <link rel="stylesheet" type="text/css" href="css/reset.css" />--%>
   <style type="text/css">
       html {
              background:#fff;
       }

       Table1 {
       width:340px;
       height:160px;
       border-color:#ffffff;
       text-align:center;
        border:solid 2px; 
        padding:2px;
       }
       .td {

       text-align:center;
       color:#eee;
       }
       .textbox {
                  margin-left:40px;
       background:#333;
       border-color:#aaa;
       font:10px;
       font-family:'Trebuchet MS';
       color:#aaa;
       }
           .textbox:focus {
           background-color:#555;
           }
       .ImageButton:hover {
           background-color:#333;
         -moz-box-shadow:1px 1px 5px #fff;
	-webkit-box-shadow:1px 1px 5px #fff;
	box-shadow:1px 1px 5px #fff;
       }
       .labelcenter {
        margin-left: auto; 
        margin-right: auto;
        text-align:center;
       }

      </style>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    

 <div class="ustcizgi" ></div>
<div class="wtabandiv" >


<div class="wustmenu" >
<div class="wustmenuic" >
    <table style="width:300px; height:180px; border-color:#555555; text-align:center; border-width:0px; padding:1px;">
  <tr>
    <td style="height:90px; text-align:center;" colspan="2"><img src="images/acbgyazibaslik2.png"   alt=""/></td>
  </tr>

  <tr>
    <td  class="td"><asp:TextBox ID="giruser" Width="200px" placeholder="UserName"  CssClass="textbox" runat="server"></asp:TextBox></td>
      <th rowspan="2" style="width:100px; text-align:center;" >
          <asp:ImageButton ImageUrl="~/images/logok.png" ImageAlign="Right" Width="64px" Height="64px" CssClass="ImageButton" ID="IBE" OnClick="IBE_Click" runat="server" BorderWidth="0" BorderStyle="None" /></th>
  </tr>
  <tr>
    <td  class="td"><asp:TextBox ID="girpass"  Width="200px" CssClass="textbox" placeholder="Password"  runat="server" TextMode="Password"></asp:TextBox></td>
  </tr>

</table>

    <asp:Label CssClass="labelcenter"  ID="Labelgirhata" ForeColor="Red" runat="server" Text="" Width="360px"></asp:Label>



</div></div>

<div class="waltmenu clear" ><img src="images/acbgyazi.png" width="898" height="211"  alt=""/></div>

</div>

<div class="altcizgi clear" >     </div>
<div class="clear" style="text-align:center; color:gray; height:30px; margin-top:150px;" >
    <asp:Label runat="server"> © 1996, This page is owned by Deniz Kılavuzluk A.Ş. <br/> 
        All rights reserved. Developed and supported by Capt.Sinan Buğdaycı</asp:Label></div>




    </div>
    </form>
</body>
</html>
