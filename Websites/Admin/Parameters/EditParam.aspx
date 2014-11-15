<%@ Page Title="Parameter Edit Page" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="EditParam.aspx.cs" Inherits="Parameters_EditParam" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/param.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<script type="text/jscript">
    function cancelClicked() {
        var label = $get('cancelMsg');
        label.innerText = 'You canceled the delete at ' + (new Date()).localeFormat("T") + '.';
        label.style.color = "red";
    }
</script>


	<div class="paramslist">
	<h2>
		
		Change or Edit Parameters Page - <asp:Label ID="lblNow" runat="server" Text="lblNow"></asp:Label></h2>

	   <asp:Label ID="Label3" runat="server" ForeColor="#0066FF" Font-Bold="True" 
            Text="Label3"
			Font-Italic="True" Font-Size="Medium"></asp:Label>
			<br />
	<asp:Panel ID="DisplayPanel" runat="server" Visible="true">
	<asp:Repeater ID="ParamMainRepeater1" runat="server">
	   <ItemTemplate>
		  <table>
		  <tr>
		  <th class="pbutton"></th>
		  <th class="pkey">Key</th>
		  <th class="pvalue">Value</th>
		  <th class="pchgdate">Change Date</th>
		  </tr>
			<asp:Repeater id="ParamsListRepeater" runat="server" DataSource='<%# Eval("Params") %>' OnItemCommand="Param_ItemCommand">


			<ItemTemplate>
				<tr style="background-color: #CCCCCC">
					<td class="pbutton"><asp:Button ID="Button4" runat="server" Text="select" CommandArgument='<%# ((Param)Container.DataItem).Key %>' /></td>
					<td class="pkey" style="width:200px"><%# ((Param)Container.DataItem).Key %></td>
					<td class="pvalue" style="width:200px"><%# ((Param)Container.DataItem).Value %></td>
					<td class="pchgdate" style="width:200px"><%# ((Param)Container.DataItem).ChangeDate.ToString() %></td>
				</tr>
			</ItemTemplate>

			</asp:Repeater>


			</table>
			</ItemTemplate>
	   </asp:Repeater>
	   <br />
	   <asp:Label ID="Label2" runat="server" Text="Was change successful?" Font-Bold="True" 
			Font-Italic="True" Font-Size="Medium" ForeColor="#0066FF"></asp:Label>
	   <br /><br />
	<br />
	</asp:Panel>
	</div>

	<div>
	<asp:Panel ID="UpdatePanel1" runat="server" Visible="true">
		
			<asp:Table ID="Table1" runat="server">
			<asp:TableHeaderRow>
			<asp:TableHeaderCell Width="200px">
				Key</asp:TableHeaderCell>
			<asp:TableHeaderCell Width="200px">
				Value</asp:TableHeaderCell>
			<asp:TableHeaderCell Width="200px">
				Change Date</asp:TableHeaderCell>
			</asp:TableHeaderRow>
			<asp:TableRow>
			<asp:TableCell Width="200px" HorizontalAlign="Center">
						<asp:Label ID="lblKey" runat="server" Text="Key = "></asp:Label>
			</asp:TableCell>
			<asp:TableCell Width="200px" HorizontalAlign="Center">
						<asp:TextBox ID="TBValue" runat="server" Text="Value" Width="180px"></asp:TextBox>
				</asp:TableCell>
			<asp:TableCell Width="200px" HorizontalAlign="Center">
						<asp:Label ID="lblChangeDate" runat="server" Text="Date Changed">
					</asp:Label>
				</asp:TableCell>
			</asp:TableRow>
			</asp:Table>
		<br />
                <asp:ConfirmButtonExtender ID="cbe" runat="server" 
            ConfirmText="Are you sure you want delete this Parameter Key and Value?" 
            TargetControlID="btnDelete" onclientcancel="cancelClicked" />

        <table style="width: 50%; margin-left: 5em; margin-right: auto"><tr>
        <td>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit Change" 
				onclick="btnSubmit_Click" />
            </td>
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Delete Key" 
                onclick="btnDelete_Click" />
            </td>
        <td>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel Change" 
				onclick="btnCancel_Click" />
            </td>
        </tr></table>
            <p id="cancelMsg"></p>

		

	</asp:Panel>		





</div>	
</asp:Content>

