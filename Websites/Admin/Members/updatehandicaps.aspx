<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="updatehandicaps.aspx.cs" Inherits="Members_updatehandicaps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/memberslist.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
		Update Members Handicaps</h2>
		<asp:Label ID="lblFileName" runat="server" Text="FileName"></asp:Label>            
		<table style="border: none;"><tr style="border: none;"><td style="width: 14em; border: none;">
		<asp:Label ID="lblUpdateCount" runat="server"></asp:Label>

			</td>
			<td style="width: 14em; border: none;">
			    <asp:Label ID="lblHandicapDate" runat="server" Text="Handicap Date:"></asp:Label>&nbsp;&nbsp;
                <asp:TextBox ID="tbHcpDate" runat="server" Width="60px"></asp:TextBox>
			</td>
		<td style="border: none;">
			<asp:Button ID="btnUpdate" runat="server" Text="Update Handicaps" 
				onclick="btnUpdate_Click"  />
		</td></tr></table>
	<asp:Panel ID="Panel1" runat="server">
	<div id="members_handicaps">
		<asp:Repeater ID="MembersListMainRepeater" runat="server">

		<ItemTemplate>
		<table>
		<tr>
		<th class="hid">MSGA Network ID</th>
		<th class="hname">Name</th>
		<th class="hindex">Index</th>
		<th class="hdate">Date</th>
		</tr>
		<asp:Repeater ID="MembersListRepeater" runat="server" DataSource='<%# Eval("Members") %>'>
		<ItemTemplate>
			<tr>
			<td class="hid"><%# ((MrMember)Container.DataItem).memberNumber %> </td>
			<td class="hname"><%# ((MrMember)Container.DataItem).name %> </td>
			<td class="hindex"><%# ((MrMember)Container.DataItem).hcp %></td>
			<td class="hdate"><asp:Label ID="Label1"
                    runat="server" Text="<%# ((MrMember)Container.DataItem).hdate.ToShortDateString() %>" 
                    Forecolor='<%# ((MrMember)Container.DataItem).IsUpdated(hcpDate)?System.Drawing.Color.Green:System.Drawing.Color.Red %>'>
                    </asp:Label></td>
			</tr>
		</ItemTemplate>
		</asp:Repeater>
		</table>
		</ItemTemplate>
		</asp:Repeater>
		</div>
		<br /><br /><br />
					</asp:Panel> 

	 
</asp:Content>

