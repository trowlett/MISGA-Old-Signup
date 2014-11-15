<%@ Page Title="Modify the Events Database" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="modifyEvents.aspx.cs" Inherits="Events_modifyEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    		<link href="../Styles/mrschedule.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Modify the Events Database</h2>
    <asp:Panel ID="Panel1" runat="server">
	<div class="mr_schedule">
<!--		<p>Sign up pages are available for now through&nbsp;&nbsp;		
			<span style="font-weight: bold"><%= this.displayDate.ToLongDateString()%></span>
		</p>
        -->

		<asp:Repeater runat="server" ID="ScheduleRepeater">
		<ItemTemplate>
		<table>
			<tr>
				<th class="ctrlcol">&nbsp;</th>
                <th class="ctrlcol">&nbsp;</th>
				<th class="datecol">Date</th>
				<th class="hacol">H/A</th>
				<th class="titlecol">Title / Club</th>
				<th class="costcol">Event Fee*</th>
				<th class='timecol'>Tee Time</th>
				<th class="playerlimit">Player Limit</th>
				<th class="deadlinecol">Deadline</th>
                <th class="postdatecol">Posting Date**</th>
			</tr>

			<asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Events") %>'>
			<ItemTemplate>
				<tr id="Tr1" class='<%# ((SysEvent)(Container.DataItem)).EType.ToLower() %>' runat="server" visible='<%# ((SysEvent)(Container.DataItem)).CanSignUp(((Events_modifyEvents)Page).displayDate) %>'>
                    <td class="ctrlcol"><a href="editevent.aspx?ID=<%# Eval("Id") %>&element=delete" title="Delete this Event">delete</a></td>
					<td class="ctrlcol"><a href="editevent.aspx?ID=<%# Eval("Id") %>&element=all" title="Edit all the fields in the Event">edit</a></td>
					<td class='datecol'><a href='editevent.aspx?ID=<%# Eval("Id") %>&element=date' title="Modify the Event Date"><%# ((SysEvent)Container.DataItem).EDate.ToString("ddd, MMM d") %></></td>
					<td class='hacol'><a href='editevent.aspx?ID=<%# Eval("Id") %>&element=type' title="Modify to either Home, Away, or MISGA"><%# ((SysEvent)Container.DataItem).EType %></a></td>
					<td class='titlecol'><a href='editevent.aspx?ID=<%# Eval("Id") %>&element=title' title="Modify the Title / Club"><%# ((SysEvent)Container.DataItem).ETitle %></a></td>
					<td class='costcol'><a href='editevent.aspx?ID=<%# Eval("Id") %>&element=fee' title="Modify the Event Fee"><%# ((SysEvent)Container.DataItem).ECost %></a></td>
					<td class='timecol'><a href='editevent.aspx?ID=<%# Eval("Id") %>&element=time' title="Modify the Tee Time"><%# ((SysEvent)Container.DataItem).ETime %></a></td>
					<td class='playerlimit'><a href='editevent.aspx?ID=<%# Eval("Id") %>&element=limit' title="Modify the Player Limit"><%# ((SysEvent)Container.DataItem).EPlayerLimit %></a></td>                   
					<td class='deadlinecol'><a href='editevent.aspx?ID=<%# Eval("Id") %>&element=deadline' title="Modify the Deadline Date"><%# ((SysEvent)Container.DataItem).EDeadline.ToString("MMM d") %></a></td>
                    <td class="postdatecol"><a href='editevent.aspx?ID=<%# Eval("id") %>&element=post' title="Modify the Post Date"><%# ((SysEvent)Container.DataItem).EPostDate.ToString("MMM d") %></a></td>
				</tr>
			</ItemTemplate>
			</asp:Repeater>
		</table>
		<p>* Payment options are on the SIGNUP PAGE.
		<br />
        ** Posting Date is when SIGNUPS are enabled for this mixer.
		   </p> 
		</ItemTemplate>
		</asp:Repeater>
	</div>  <!-- mr_schedule -->

    </asp:Panel>
</asp:Content>

