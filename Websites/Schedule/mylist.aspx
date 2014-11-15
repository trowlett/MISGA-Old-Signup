<%@ Page Title="" Language="C#" MasterPageFile="~/MRMisga.master" AutoEventWireup="true" CodeFile="mylist.aspx.cs" Inherits="mylist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
	<link href="Styles/mrschedule.css" rel="stylesheet" type="text/css" /> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	<h2>
		Show My List of Events</h2>
		<table><caption style="font-style: italic; font-weight: bold; text-align: left">Show the events that I have signed up for.</caption><tr>
		<td style="width: 350px">

		<asp:Label ID="Label1" runat="server" Text="Name:  "></asp:Label>
		<asp:DropDownList ID="ddlName" runat="server" DataSourceID="SqlDataSource1" 
			DataTextField="Name" DataValueField="PlayerID" Height="22px" Width="248px">
		</asp:DropDownList> 
		</td><td style="width: 250px; text-align: left">       

		<asp:RadioButtonList ID="rblEvents" runat="server" Font-Bold="True" 
			Font-Italic="True">
			<asp:ListItem Value="0">All My Events</asp:ListItem>
			<asp:ListItem Value="1" Selected="True">Only My Remaining Events</asp:ListItem>
		</asp:RadioButtonList>
		</td><td>
		<asp:Button ID="btnSelect" runat="server" Text="Show Events" 
			onclick="btnSelect_Click" />
				

		</td></tr></table>
				<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
			ConnectionString="<%$ ConnectionStrings:MRMISGADBConnect %>" 
			SelectCommand="SELECT Name, PlayerID FROM Players ORDER BY Name">
		</asp:SqlDataSource>

	<asp:Panel ID="Panel1" runat="server">
	<div class="mr_schedule">
	<asp:Label ID="lblPID" runat="server"></asp:Label>
		<asp:Repeater ID="PlayerScheduleRepeater" runat="server">
		<ItemTemplate>
		<table>
			<tr>
			<th class="futurecol">Cmpl</th>
				<th class="datecol">Date</th>
				<th class="hacol">H/A</th>
				<th class="clubcol">Title</th>
				<th class="costcol">Event Fee*</th>
				<th class="timecol">Tee Time</th>
				<th class="deadlinecol">Deadline</th>
				<th class="carpool">Carpool</th>
			</tr>
			<asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("PEvents") %>'>
			<ItemTemplate>

			<tr id="Tr1" class='<%# ((PrEvent)(Container.DataItem)).PrType.ToLower().Trim() %>' runat="server" visible='<%# !((PrEvent)Container.DataItem).PrCompleted %>'>
				<td class="futurecol"><%# ((PrEvent)Container.DataItem).PrCompleted == true ? "Y" : "" %></td>

					<td class="datecol"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrDate.ToString("ddd, MMM d") %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrType %></td>
					<td class="clubcol"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrTitle %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrCost %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrTime %></td>
					<td class="deadlinecol"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrDeadline.ToString("MMM d") %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrCarpool %></td>
				</tr>
			<tr id="Tr2" class='<%# ((PrEvent)(Container.DataItem)).PrType.ToLower().Trim() %>' runat="server" visible='<%# ((PrEvent)Container.DataItem).PrCompleted %>'>
				<td class="futurecol"><%# ((PrEvent)Container.DataItem).PrCompleted == true ? "Y" : "" %></td>

					<td class="datecol"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrDate.ToString("ddd, MMM d") %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrType %></td>
					<td class="clubcol"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrTitle %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrCost %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrTime %></td>
					<td class="deadlinecol"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrDeadline.ToString("MMM d") %></td>
					<td class="center"><a href='Signup.aspx?ID=<%# Eval("PrEvID") %>'><%# ((PrEvent)Container.DataItem).PrCarpool %></td>
				</tr>
			   </ItemTemplate>
			</asp:Repeater>
		</table>
		<p>* Cash or Check Only, NO CREDIT CARDS ACCEPTED.</p> 
		</ItemTemplate>

		</asp:Repeater>
	</div>
	</asp:Panel>
</asp:Content>

