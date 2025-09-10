<%@ Page Language="C#" AutoEventWireup="true" CodeFile="portsandberths.aspx.cs" Inherits="yonet_portsandberths"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />

            <style type="text/css">


</style>

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
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>
    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> 
      <h3>Users</h3>
      <ul class="nav">                   <asp:Literal ID="Litmenu1" Text="" runat="server"> 
                <li><a href="pilots.aspx">User List</a></li></asp:Literal>
      </ul>

      <h3>Places</h3>
      <ul class="nav">
        <li id="ulnavlihover"><a>Ports and Berths</a></li>
        <li><a href="anchorageplaces.aspx">Anchorage Places</a></li>

      </ul>

      <h3>Distances</h3><ul class="nav">
        <li><a href="distances.aspx">Distances</a></li></ul>
      <asp:Literal ID="Litmenu4" Text="" runat="server"> 
      <h3>Flags</h3> <ul class="nav">        
        <li><a href="flags.aspx">Flags</a></li></ul></asp:Literal>
      <asp:Literal ID="Litmenu5" Text="" runat="server"> 
      <h3>Ship Types</h3><ul class="nav">
        <li><a href="shiptypes.aspx">Ship Types</a></li></ul></asp:Literal>
      <asp:Literal ID="Litmenu6" Text="" runat="server"> 
      <h3>Pilot Stations</h3><ul class="nav">
        <li><a href="pilotstations.aspx">Pilot Stations</a></li></ul></asp:Literal>

</div>






      <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
 
    <div id="center-column">
      <div class="top-bar">

          <ul>
  <li class="sagusttuslist">  <asp:LinkButton ID="LBaddnewportberth" class="sagusttuslista" runat="server" OnClick="LBaddnewportberth_Click" >Add New Port-Berth</asp:LinkButton></li>
</ul>

        <h1><a href="yonetim.aspx">Settings</a> > Ports and Berths </h1>
          
      <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar">
        <label>
            <asp:DropDownList ID="DropDownListports"  runat="server"   Height="21px" Width="154px" OnSelectedIndexChanged="DropDownListports_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
        </label>

      </div>


      <div class="table">

       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="id" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  CellPadding="2" ForeColor="#333333"  GridLines="Vertical"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  SelectText="" ItemStyle-Width="30px"   ShowSelectButton="True" />

<asp:TemplateField HeaderText="P.No"  ControlStyle-Width="30px" >
                    <EditItemTemplate ><asp:TextBox ID="TBeditportlimanno"  runat="server" Text='<%# Bind("limanno") %>' MaxLength="4"  Enabled="false">></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" TargetControlID="TBeditportlimanno" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <FooterTemplate></FooterTemplate>
                    <ItemTemplate><asp:Label ID="Litemportlimanno" runat="server" Text='<%# Bind("limanno") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Trebuchet MS" Font-Size="Small" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="Port Name"  ControlStyle-Width="150px" >
                    <EditItemTemplate><asp:TextBox ID="TBeditportlimanadi" runat="server" CssClass="kucukharf" MaxLength="20" Text='<%# Bind("limanadi") %>' ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportlimanadi" runat="server" Text='<%# Bind("limanadi") %>'></asp:Label></ItemTemplate>

                    <ControlStyle></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="Berths"   ControlStyle-Width="80px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportrihtimadi" runat="server" CssClass="kucukharf"  Text='<%# Bind("rihtimadi") %>' MaxLength="16" ></asp:TextBox></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportrihtimadi" runat="server" Text='<%# Bind("rihtimadi") %>'></asp:Label> </ItemTemplate>

                    <ControlStyle></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="Area"   ControlStyle-Width="35px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportlimanbolge" runat="server" Text='<%# Bind("limanbolge") %>' MaxLength="2" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" TargetControlID="TBeditportlimanbolge" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate> <asp:Label ID="Litemportlimanbolge" runat="server" Text='<%# Bind("limanbolge") %>'></asp:Label> </ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="Hard"  ControlStyle-Width="35px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportzorluk" runat="server" MaxLength="1" Text='<%# Bind("zorluk") %>' ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" TargetControlID="TBeditportzorluk" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportzorluk" runat="server" Text='<%# Bind("zorluk") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>

<asp:TemplateField HeaderText="Stn."   ControlStyle-Width="35px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportresp" runat="server" Text='<%# Bind("bagliistasyon") %>' MaxLength="1" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" TargetControlID="TBeditportresp" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportresp" runat="server" Text='<%# Bind("bagliistasyon") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="B.T." ControlStyle-Width="35px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportyanasmasuresi" runat="server" MaxLength="3" Text='<%# Bind("yanasmasuresi") %>' ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" TargetControlID="TBeditportyanasmasuresi" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportyanasmasuresi" runat="server" Text='<%# Bind("yanasmasuresi") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="UB.T." ControlStyle-Width="35px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportkalkissuresi" runat="server" MaxLength="3" Text='<%# Bind("kalkissuresi") %>' ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" TargetControlID="TBeditportkalkissuresi" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportkalkissuresi" runat="server" Text='<%# Bind("kalkissuresi") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Show"   ControlStyle-Width="35px">
                    <EditItemTemplate><asp:TextBox ID="TBeditportgoster" runat="server" Text='<%# Bind("goster") %>' MaxLength="1" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" TargetControlID="TBeditportgoster" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litemportgoster" runat="server" Text='<%# Bind("goster") %>'></asp:Label></ItemTemplate>

                    <ControlStyle ></ControlStyle>
                    <FooterStyle Font-Names="Trebuchet MS" Font-Size="Small" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle Font-Names="Trebuchet MS" Font-Size="Small" />
                </asp:TemplateField>

<asp:CommandField  ItemStyle-Width="45px" ShowEditButton="True"  EditImageUrl="../images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="../images/save.png"  CancelImageUrl="../images/cancel.png"   CancelText=""  ButtonType="Image" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>

<asp:CommandField  ItemStyle-Width="25px" ShowDeleteButton="True"  DeleteImageUrl="../images/cancelsil.png" DeleteText=""    ButtonType="Image">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>
                
            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

          <br />
          <div style="width:747px; height:561px; border:1px solid gray" >
              <asp:Image ID="Image1" runat="server"  />

          </div>



        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   CssClass="panellbadd" runat="server"> 

<table  class="panellbadd">                        
                     <tr>
                <td colspan="2"  class="tdorta"  >
                 <p style="box-shadow: inset; text-align: center; width: 100% auto; height: 20px; font-weight: bold; font-family: 'Trebuchet MS'">Add New Port or Berth </p></td>
            </tr>      
    
     <tr>
                <td colspan="2" style="text-align:center; font-weight:500; height:30px; vertical-align:top" >
<asp:LinkButton ID="LBaddonlyport" ToolTip="To add a new port and one berth" runat="server"  ForeColor="Blue" OnClick="LBaddonlyport_Click">Add New Port</asp:LinkButton>  /  
<asp:LinkButton ID="LBaddonlyberth" ToolTip="To add a berth into existing port" runat="server"  ForeColor="Blue"  OnClick="LBaddonlyberth_Click">Add only Berth to Existing Port</asp:LinkButton>
                </td>
            </tr>  

        
            <tr>
                <td >Port No : </td>
                <td  class="tdsag" ><div>
                    <asp:TextBox ID="TBpaddportno" Width="30px" runat="server" MaxLength="4"  Enabled="false"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="TBpaddportno" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></div>  </td>
            </tr>
            <tr>
                <td >Port Name: </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddportname" Width="150px" runat="server"  Visible="true" CssClass="kucukharf"  MaxLength="20">     </asp:TextBox>  
            <asp:DropDownList ID="DDLSelectexPort"  runat="server"  Visible="false"  Height="21px" Width="154px" OnSelectedIndexChanged="DDLSelectexPort_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >Berth Name : </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddrname" Width="150px" runat="server"  CssClass="kucukharf"  MaxLength="16"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >Port Area : </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddportarea"  runat="server" Width="150px" MaxLength="2"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="TBpaddportarea" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
                <tr>
                <td >Difficulty : </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddzorluk"  runat="server" Width="50px" MaxLength="1"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="TBpaddzorluk" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>

                <tr>
                <td >Resp.Station : </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddresp"  runat="server" Width="50px" MaxLength="1"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="TBpaddresp" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
                    <tr>
                <td >Berthing Time : </td>
                <td class="tdsag"  >
                    <asp:TextBox ID="TBpaddyanasmasuresi"  runat="server" Width="50px" MaxLength="3"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="TBpaddyanasmasuresi" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
                    <tr>
                <td >Unberthing Time : </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddkalkissuresi"  runat="server" Width="50px" MaxLength="3"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" TargetControlID="TBpaddkalkissuresi" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td >Show : </td>
                <td  class="tdsag" >
                    <asp:TextBox ID="TBpaddgoster" Width="30px" runat="server" MaxLength="1"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" TargetControlID="TBpaddgoster" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2"  class="tdorta"  style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       
                       <asp:Button ID="Buttonlbadd" runat="server" style="height:30px; Width:80px" Text="Save"  OnClick="Buttonlbadd_Click"  />&nbsp;&nbsp; 
                       <asp:Button ID="Buttonlbaddcancel" runat="server" style="height:30px; Width:80px" Text="Cancel"  OnClick="Buttonlbaddcancel_Click"  />         
                </td></tr>         
           </table>

        </asp:Panel>
           
        <asp:ModalPopupExtender 
            ID="ModalPopupExtenderlbadd" runat="server" 
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>

        <br />
          <%-- modal onay message baþlar  
    onay için bu panele kes yapýþtýr. Baccepted  butonuna onclick olayý yaz. 
     --%>
 <asp:Button id="buttononayMessagePanel" runat="server" style="display:none;" /> 
        <asp:Panel ID="panelonayMessagePanel"    CssClass="panelmessage" runat="server" >  
    <div style="text-align:center; font-weight:bold;">        
<br /> Are you sure to delete the record? <br /><br /></div>
            <asp:Button ID="Baccepted" CommandName="0" runat="server" style="height:30px; Width:80px" Text="Yes"  OnClick="Baccepted_Click" />&nbsp;&nbsp; 
            <asp:Button ID="Bclosed" runat="server" style="height:30px; Width:80px" Text="Cancel"  />  
<br /><br />
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopuponayMessage" runat="server" TargetControlID="buttononayMessagePanel" PopupControlID="panelonayMessagePanel" BackgroundCssClass="modalbodyarka" CancelControlID="Bclosed" ></asp:ModalPopupExtender>
<%-- modal onay message biter --%>

            </ContentTemplate></asp:UpdatePanel>
                   


      </div>
    </div>
    
</div>
  <div id="footer"></div>    
    
</div>





    
  
    </form>
</body>
</html>
