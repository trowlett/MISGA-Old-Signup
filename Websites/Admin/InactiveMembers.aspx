<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="InactiveMembers.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
	<link href="Styles/memberslist.css" rel="stylesheet" type="text/css" /> 
	<title>Members List</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="memberslist">
	<h3>Members and Their Number of Active Signup Records </h3>

	   
	   <asp:Label ID="Label3" runat="server"></asp:Label>

	   
	   <br />
	   <asp:Repeater ID="MembersListMainRepeater" runat="server">
	   <ItemTemplate>
		  <table>
		  <tr>
		  <th class="pid">ID</th>
		  <th class="name">Member</th>
		  <th class="mtitle">Title</th>
		  <th class="memberID">Member #</th>
		  <th class="gender">Gender</th>
		  <th class="hcp">Hcp Index</th>
		  <th class="pactive">Active</th>
		  </tr>
			<asp:Repeater id="MembersListRepeater" runat="server" DataSource='<%# Eval("Members") %>' OnItemCommand="Member_ItemCommand">

			<AlternatingItemTemplate>
				<tr style="background-color: #FFFFCC">
					<td class="pid"><asp:Button ID="button3" runat="server" Text='<%# ((MrMember)Container.DataItem).pID %>' Width="30px" /></td>
					<td class="name"><%# ((MrMember)Container.DataItem).name %></td>
					<td class="mtitle"><%# ((MrMember)Container.DataItem).title %></td>
					<td class="memberID"><%# ((MrMember)Container.DataItem).memberNumber %></td>
					<td class="gender"><%# ((MrMember)Container.DataItem).IsFemale()?"F":"" %></td>					
					<td class="hcp"><%# ((MrMember)Container.DataItem).hcp %></td>
					<td class="pactive"><%# ((MrMember)Container.DataItem).active.ToString("##,##0") %></td>

				 </tr>
			</AlternatingItemTemplate>

			<ItemTemplate>
				<tr style="background-color:Silver">
					<td class="pid"><asp:Button ID="Button4" runat="server" Text='<%# ((MrMember)Container.DataItem).pID %>' Width="30px" /></td>
					<td class="name"><%# ((MrMember)Container.DataItem).name %></td>
					<td class="mtitle"><%# ((MrMember)Container.DataItem).title %></td>
					<td class="memberID"><%# ((MrMember)Container.DataItem).memberNumber %></td>
					<td class="gender"><%# ((MrMember)Container.DataItem).IsFemale()?"F":"" %></td>					
					<td class="hcp"><%# ((MrMember)Container.DataItem).hcp %></td>
					<td class="pactive"><%# ((MrMember)Container.DataItem).active.ToString("##,##0") %></td>
				</tr>
			</ItemTemplate>

			</asp:Repeater>


			</table>
			</ItemTemplate>
	   </asp:Repeater>
	   <asp:Label ID="Label2" runat="server"></asp:Label>
	   </div>  <!-- End of Div MembersList -->
	   <br />
</asp:Content>

