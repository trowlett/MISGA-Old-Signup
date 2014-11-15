<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="AddParam.aspx.cs" Inherits="Parameters_AddParam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<h2>
	Add Parameter Page</h2>	
	<asp:Panel ID="AddPanel" runat="server">
	<br />
	<h2>Key and Value to be added</h2>
		<asp:Table ID="Table1" runat="server">
		<asp:TableHeaderRow>
		<asp:TableCell HorizontalAlign="Center">KEY</asp:TableCell>
		<asp:TableCell HorizontalAlign="Center">Value</asp:TableCell>
		</asp:TableHeaderRow>
		<asp:TableRow>
		<asp:TableCell Width="200px">
			<asp:TextBox ID="tbKeyToAdd" runat="server" Width="195px"></asp:TextBox>
		</asp:TableCell>
		<asp:TableCell Width="200px">
			<asp:TextBox ID="tbValueToAdd" runat="server" Width="200px"></asp:TextBox>
		</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
		<asp:TableCell Width="200px">
		<asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Submit" />     
		</asp:TableCell>
		 <asp:TableCell Width="200px">
		<asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" />
		</asp:TableCell>
	   </asp:TableRow>
		</asp:Table>
		<br />
		<br />
		<asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
		<br />
	</asp:Panel>
	<asp:Panel ID="Parametrs" runat="server">
	<h2>Current Parameters</h2>

	<asp:Repeater ID="ParamMainRepeater1" runat="server">
	   <ItemTemplate>
		  <table>
		  <tr>
		  <th class="pid">Key</th>
		  <th class="name">Value</th>
		  <th class="mtitle">Change Date</th>
		  </tr>
			<asp:Repeater id="ParamsListRepeater" runat="server" DataSource='<%# Eval("Params") %>'>


			<ItemTemplate>
				<tr style="background-color: #CCCCCC">
					<td class="key" style="width:200px"><%# ((Param)Container.DataItem).Key %></td>
					<td class="pvalue" style="width:200px"><%# ((Param)Container.DataItem).Value %></td>
					<td class="pchgdate" style="width:200px"><%# ((Param)Container.DataItem).ChangeDate.ToString() %></td>
				</tr>
			</ItemTemplate>

			</asp:Repeater>


			</table>
			</ItemTemplate>
	   </asp:Repeater>
	   <br />
	</asp:Panel>
</asp:Content>

