<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="signupDB.aspx.cs" Inherits="Signups_Carpool" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/playerslist.css" rel="stylesheet" type="text/css" /> 
<style type="text/css">
#MainContent_UpdatePanel1, #MainContent_UpdatePanel2, #MainContent_UpdateProgress1 { 
      border-right: gray 1px solid; border-top: gray 1px solid; 
      border-left: gray 1px solid; border-bottom: gray 1px solid;
    }
#MainContent_UpdatePanel1, #MainContent_UpdatePanel2 { 
      width:200px; height:50px; position: relative;
      float: left; margin-left: 10px; margin-top: 10px;
     }
#MainContent_UpdateProgress1 {
      width: 400px; background-color: #FFC080; 
      bottom: 10%; left: 20px; position: absolute;
     }
</style>

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

    <h1>
    Sign Up Database</h1>

    <asp:Panel ID="Panel1" runat="server" Width="90%">
    <h2>
        <%: DBCount==0?"No":DBCount.ToString() %> entr<%: DBCount==1?"y":"ies" %>in the Sign Up Database
    </h2>

<div class="players_list">
    <p>
        <asp:LinkButton ID="lbDeleteAll" runat="server" onclick="lbDeleteAll_Click">Click here to delete all <%: DBCount %> entries </asp:LinkButton>
             <br /><span id="cancelMsg"><asp:Label ID="lblErrorMsg" runat="server" Text="Error" 
                    ForeColor="Red" Visible="false"></asp:Label>
             </span>
        </p>

		<asp:Repeater ID="PlayersListRepeater" runat="server">
		<ItemTemplate>
		<table>
			<tr>
                <th class="select">
                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_OnCheckedChanged" /></th>
				<th class="seqno">No.</th>
				<th class="timestamp">Signup Timestamp</th>
                <th class="eventid">Event ID</th>
                <th class="playerid">Player ID</th>
				<th class="name">Name</th>
<!--				<th class="gender">Gender</th> -->
<!--				<th class="hcp">Hcp. Index</th> -->
				<th class="srule">Rule</th>
				<th class="action">Action</th> 
				<th class="carpool">Carpool</th>
				<th class="marked">Marked</th> 
                <th class="playerid">Guest ID</th>
<!--				<th class="name">Guest Name</th> -->
<!--				<th class="hcp">Guest Hcp.</th> -->
<!--				<th class="gender">Guest Gender</th> -->
			   </tr>

			   <asp:Repeater ID="Repeater1"  runat="server" DataSource='<%# Bind("Entries") %>'>
			   <ItemTemplate>
			   <tr>
                    <td class="select">
                        <asp:CheckBox ID="CheckBox2" runat="server" Checked="<%# ((SignupEntry)Container.DataItem).SSelected %>"/>
                    </td>
					<td class="seqno"><%# ((SignupEntry)Container.DataItem).SeqNo %></td>
					<td class="timestamp"><%# ((SignupEntry)Container.DataItem).STDate.ToString() %></td>
                    <td class="eventid"><%# ((SignupEntry)Container.DataItem).SeventId %></td>
                    <td class="playerid"><%# ((SignupEntry)Container.DataItem).SPlayerID %></td>
					<td class="name"><%# ((SignupEntry)Container.DataItem).Splayer %></td>
<!--					<td class="gender"><%# ((SignupEntry)Container.DataItem).Ssex %></td> -->
<!--					<td class="hcp"><%# ((SignupEntry)Container.DataItem).Shcp %></td> -->
					<td class="srule"><%# ((SignupEntry)Container.DataItem).SspecialRule %></td>
					<td class="action"><%# ((SignupEntry)Container.DataItem).Saction %></td> 
					<td class="carpool"><%# ((SignupEntry)Container.DataItem).Scarpool %></td>
					<td class="marked"><%# ((SignupEntry)Container.DataItem).Smarked %></td> 
                    <td class="playerid"><%# ((SignupEntry)Container.DataItem).SGuest %></td>
<!--					<td class="name"><%#((SignupEntry)Container.DataItem).SGuestName %></td> -->
<!--					<td class="hcp"><%#((SignupEntry)Container.DataItem).SgHcp %></td> -->
<!--					<td class="gender"><%# ((SignupEntry)Container.DataItem).SgSex %></td> -->
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
			</ItemTemplate>
	</asp:Repeater>
	<br />
                <asp:ConfirmButtonExtender ID="cbe" runat="server" 
            ConfirmText="You are deleting ALL the entries in the Sign Up Database?" 
            TargetControlID="lbDeleteAll" onclientcancel="cancelClicked" />

	</div>
  

    </asp:Panel> 
</asp:Content>

