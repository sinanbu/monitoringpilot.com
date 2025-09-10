<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Astajfin.aspx.cs" Inherits="Astajfin" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot</title>
 <link href="../css/stil.css" rel="stylesheet" />
    <style type="text/css">
   
      
        .auto-style1 {
            width: 100%; 
                 }

        .selected_row
    {
        background-color:#252a44;
        border-bottom-color:gray;
    }


.kucukharf {
    text-transform:lowercase;
    direction:ltr;
    overflow:auto;
}

        .panelmessage {
            width: 300px;
            height: 150px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: small;
            text-align: center;
            color: black;
        }
    </style>

    <script type="text/javascript" src="../js/jquery-1.11.2.js"></script>


</head>
<body onkeydown = "return (event.keyCode!=13)" >
    <form id="form1" runat="server" >

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

<asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Conditional" ><ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
           <table  style="width:1340px; height:25px; color:white; background-image:url(../images/boslukaltcizgi.png)"><tr  style="height:25px">
            <td > 
          <asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label> <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal> 
                          
</td></tr>
</table>   </asp:Panel> </ContentTemplate></asp:UpdatePanel>
 <div >
 <asp:Panel ID="summariall" runat="server" >


<table  style="width:1340px; float:right;   color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left;">
        <asp:Button ID="ButtonLiveScreen" style="height:25px; Width:80px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;
        <asp:Button ID="adminsayfalar" style="height:25px; Width:120px;  font-size:x-small; text-align:center" runat="server" Text="ADMIN SAYFALAR"   OnClick="adminsayfalar_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;
     </td>
        <td style="text-align:right">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="white" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="white"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  



<table  style="width:100% ;"> <tr><td style="text-align:left; width:100% ; height:25px"></td></tr>
<tr><td style="text-align:left; width:100% ;"><span style="font-family:'Trebuchet MS'; color:#222; font-size:small; margin:0; "><b>AKTİF VARDİYAYA PİLOT EKLE</b><br /><br /></span></td>
</tr></table>  


     
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>

   
      <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 
                 <asp:DropDownList ID="DDLPilots"   runat="server" Height="24px" Width="200px"  AutoPostBack="true" OnSelectedIndexChanged="DDLPilots_SelectedIndexChanged"  ></asp:DropDownList> </br></br>

 <p>

 <asp:Button id="Bonay" style="height:25px; Width:80px; font-size:small;  text-align:center;"  runat="server" text="Stajı Bitir !" OnClick="Bonay_Click"/>
 </p>


       </div>
                    <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                 </ContentTemplate></asp:UpdatePanel>


              <%-- modal onay message başlar  
    onay için bu panele kes yapıştır. Baccepted  butonuna onclick olayı yaz. --%>
 <asp:Button id="buttononayMessagePanel" runat="server" style="display:none;" /> 
        <asp:Panel ID="Onay"    CssClass="panelmessage" runat="server" >  
    <div style="text-align:center; font-weight:bold;">        
<br /> <asp:Label ID="Label2" runat="server" Text="" ForeColor="Black" Height="30px"></asp:Label>Staj bitti, Pilot Göreve Başlayacak! <br />Eminmisin?<br /><br /></div>
            <asp:Button ID="Baccepted"  runat="server" style="height:30px; Width:80px" Text="Yes"  OnClick="Baccepted_Click"  />&nbsp;&nbsp; 
            <asp:Button ID="Bclosed" runat="server" style="height:30px; Width:80px" Text="Cancel" />  
<br /><br />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopuponayMessage" runat="server" TargetControlID="buttononayMessagePanel" PopupControlID="Onay" BackgroundCssClass="modalbodyarka" CancelControlID="Bclosed" ></asp:ModalPopupExtender>
<%-- modal onay message biter --%>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
