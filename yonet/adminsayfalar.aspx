<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminsayfalar.aspx.cs" Inherits="yonet_adminsayfalar"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />


</head>

<body>
    <form id="form1" runat="server">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

   <div id="main">
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
    <ul id="top-navigation">
      <li class="active" ><span><span>Settings</span></span></li>
      <li><span><span><a href="Statistics.aspx">Statistics</a></span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LinkButton1" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>
    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> 
      <h3>Users</h3>
      <ul class="nav">
          <asp:Literal ID="Literal1" Text="" runat="server"> 
                <li><a href="pilots.aspx">User List</a></li></asp:Literal>
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


       <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    <div id="center-column">
      <div class="top-bar"> 
         
          <ul>
  <li class="sagusttuslist"><asp:LinkButton ID="LBaddnewportberth" class="sagusttuslista" runat="server" OnClick="LBaddnewportberth_Click">Add New Page</asp:LinkButton></li>
</ul>

        <h1><a href="yonetim.aspx">Settings</a> > Admin Sayfalar </h1>
          
                  <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar">
        <label>
           <%-- <asp:TextBox ID="TBtextara" runat="server"></asp:TextBox>--%>
        </label>
        <label>
           <%-- <asp:Button ID="LBSubmit" runat="server" Text="Search" OnClick="LBSubmit_Click"></asp:Button>--%>
        </label>
      </div>


      <div class="table">
        

       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px" 
            OnRowCommand="GridView1_RowCommand"  OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="2" ForeColor="#333333"  GridLines="Vertical"  AllowPaging="True" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>

<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  ItemStyle-Width="47px"  SelectText="" ShowSelectButton="True" />

<asp:TemplateField HeaderText="id" ControlStyle-Width="30px" >
                    <FooterTemplate></FooterTemplate>
                    <ItemTemplate><asp:Label ID="Litemportlimanno" runat="server" Text='<%# Bind("id") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Trebuchet MS" Font-Size="Small" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
            </asp:TemplateField>

<asp:TemplateField HeaderText="Açýklama"  ControlStyle-Width="260px" >
                    <EditItemTemplate> <asp:TextBox ID="TBeditportlimanadi" runat="server" CssClass="kucukharf"  Text='<%# Bind("aciklama") %>'></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:LinkButton ID="Litemportlimanadi" runat="server" OnClick="Litemportlimanadi_Click" Text='<%# Bind("aciklama") %>' CommandArgument="<%# Container.DataItemIndex %>" CommandName="linkle"></asp:LinkButton></ItemTemplate>

                     <ControlStyle></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
            </asp:TemplateField>

<asp:TemplateField HeaderText="Sayfa"  ControlStyle-Width="150px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportgoster" runat="server" Text='<%# Bind("sayfa") %>'  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportgoster" runat="server" Text='<%# Bind("sayfa") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>

<asp:TemplateField HeaderText="Section"  ControlStyle-Width="150px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportsec" runat="server" Text='<%# Bind("section") %>'  ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportsec" runat="server" Text='<%# Bind("section") %>'></asp:Label></ItemTemplate>

                    <ControlStyle  ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
          </asp:TemplateField>

<asp:CommandField  ItemStyle-Width="60px" ShowEditButton="True" HeaderText="Update"  EditImageUrl="../images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="../images/save.png"  CancelImageUrl="../images/cancel.png"   CancelText=""  ButtonType="Image" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Small"></ItemStyle>
                </asp:CommandField>

<asp:CommandField ItemStyle-Width="60px" ShowDeleteButton="True"  HeaderText="Delete"   DeleteImageUrl="../images/cancelsil.png" DeleteText=""    ButtonType="Image">
            <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Small"></ItemStyle>
                </asp:CommandField>

            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

        </asp:GridView>
    

        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   CssClass="panellbadd" runat="server"> 

<table  class="panellbadd">                        
                     <tr>
                <td   class="tdorta" >
                 <p >Add New Page </p></td>
            </tr>          
            <tr>
                <td  class="tdsag" ><div>ID : <br />
                    <asp:TextBox ID="TBpaddportno"  runat="server"  Enabled="false"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td  class="tdsag" ><div>Açýklama: <br />
                    <asp:TextBox ID="TBpaddportname" runat="server"   CssClass="kucukharf" ></asp:TextBox>
                </td>
            </tr>
                        <tr>
                <td  class="tdsag" ><div>Sayfa: <br />
                    <asp:TextBox ID="TBpaddgoster"  runat="server" ></asp:TextBox>
                </td>
            </tr>
                <tr>
                <td  class="tdsag" ><div>Bölüm: <br />
                    <asp:TextBox ID="TBpaddsec"  runat="server" ></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td  class="tdorta"  style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       
                       <asp:Button ID="Buttonlbadd" runat="server" style="height:30px; Width:80px" Text="Save"  OnClick="Buttonlbadd_Click"  />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:30px; Width:80px" Text="Cancel"   />         
                </td></tr>         
           </table>

        </asp:Panel>

                  <br /> <br /> 
       Açýklama:
       <br /> <br /> 
        2.Bitmiþ iþ detaylarý: manevra saatleri yanlýþ girildiyse buradan düzeltilir, girilen süre 10 dakikadan fazla ise iþlem yapar ve bedensel toplamlar da otomatik deðiþir ve 
          refresten sonra fatik düzelir, zihinsel fatig deðiþmez. Ýþ iptal edildiyse zihinsel fatig toplamdan düþülür. iþ iptalden tekrar historiye alýnýrsa zihinsel fatik geri eklenir. Eðer iþ komple silinirse toplamlardan   
          bedensel ve zihinsel fatik ler ve issayýsý eksiltilir.<br /><br />
        3.Son 20 vardiyada oluþan vardiyalýk toplamlar burada görülür ve düzeltilebilir.<br /><br /> 
        4.Staj bitirildiðinde pilot iþlem yapýldýðý andaki vardiyaya düþer. Gerekirse kýdemi ve vardiya deðiþikliði pilotlar sayfasýndan düzeltilir.<br /> <br />
                                                  <asp:ModalPopupExtender 
            ID="ModalPopupExtenderlbadd" runat="server" 
            CancelControlID="Buttonlbaddcancel"
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />


          <%-- modal onay message baþlar  
    onay için bu panele kes yapýþtýr. Baccepted  butonuna onclick olayý yaz. --%>
 <asp:Button id="buttononayMessagePanel" runat="server" style="display:none;" /> 
        <asp:Panel ID="panelonayMessagePanel"    CssClass="panelmessage" runat="server" >  
    <div style="text-align:center; font-weight:bold;">        
<br /> Are you sure to delete the record? <br /><br /></div>
            <asp:Button ID="Baccepted" CommandName="0" runat="server" style="height:30px; Width:80px" Text="Yes"  OnClick="Baccepted_Click"  />&nbsp;&nbsp; 
            <asp:Button ID="Bclosed" runat="server" style="height:30px; Width:80px" Text="Cancel" />  
<br /><br />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopuponayMessage" runat="server" TargetControlID="buttononayMessagePanel" PopupControlID="panelonayMessagePanel" BackgroundCssClass="modalbodyarka" CancelControlID="Bclosed" ></asp:ModalPopupExtender>
<%-- modal onay message biter --%>

            </ContentTemplate></asp:UpdatePanel>

      <!--
        <div class="select"> <strong>Other Pages: </strong>
          <select>
            <option>1</option>
          </select>
        </div>
      -->

      </div>
    </div>
    
  </div>
  <div id="footer"></div>   

</div>





    
  
    </form>
</body>
</html>
