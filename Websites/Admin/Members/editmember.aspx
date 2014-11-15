<%@ Page Title="Member Edit Page" Language="C#" Debug="true" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="editmember.aspx.cs" Inherits="Members_editmember" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/memberslist.css" rel="stylesheet" type="text/css" />
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

    <asp:Panel ID="Panel1" runat="server">


    <h2>
        Edit Members Database - <asp:Label ID="lblNow" runat="server" Text="lblNow"></asp:Label></h2>
        <asp:Label ID="lblMemberCount" runat="server" ForeColor="#0066FF" Font-Bold="true"
                Text="Member Count" Font-Italic="true" Font-Size="Medium"></asp:Label>
        	   <asp:Label ID="Label3" runat="server" ForeColor="#0066FF" Font-Bold="True" 
               Text="Label3"
			Font-Italic="True" Font-Size="Medium"></asp:Label>
			<br />
    <asp:Panel ID="DisplayPanel" runat="server">
    <div class="memberslist">
        <asp:Repeater ID="MainMemberRepeater" runat="server">
        	   <ItemTemplate>
		        <table>
		        <tr>
		        <th class="pbutton"></th>
		        <th class="pid">Player ID</th>
		        <th class="mname">Name</th>
                <th class="memberID">Member ID</th>
                <th class="hcp">Hcp</th>
                <th class="hcpdate">Hcp Date</th>
                <th class="mtitle">Title</th>
                <th class="gender">Gender</th>
		        <th class="active">Active Signups</th>
                <th class="lname">Last Name</th>
                <th class="lname">First Name</th>
                <th class="mdel">X</th>
		  </tr>
			<asp:Repeater id="MembersListRepeater" runat="server" DataSource='<%# Eval("Members") %>' OnItemCommand="Member_ItemCommand">


			<ItemTemplate>
				<tr class="<%# ((MrMember)Container.DataItem).IsHandicapCurrent() %>">
					<td class="pbutton"><asp:Button ID="Button4" runat="server" Text="edit" CommandArgument='<%# ((MrMember)Container.DataItem).pID %>' /></td>
					<td class="pid"><%# ((MrMember)Container.DataItem).pID %></td>
					<td class="mname"><%# ((MrMember)Container.DataItem).name %></td>
                    <td class="memberID"><%# ((MrMember)Container.DataItem).memberNumber.Trim() %></td>
                    <td class="hcp"><%# ((MrMember)Container.DataItem).hcp %></td>
                    <td class="hcpdate"><%# ((MrMember)Container.DataItem).hdate.ToShortDateString() %></td>
                    <td class="mtitle"><%# ((MrMember)Container.DataItem).title.Trim() %></td>
                    <td class="gender"><%# ((MrMember)Container.DataItem).IsFemale() ? "F" : "M" %></td>
					<td class="active"><%# ((MrMember)Container.DataItem).active.ToString() %></td>
                    <td class="lname"><%# ((MrMember)Container.DataItem).lname %></td>
                    <td class="lname"><%# ((MrMember)Container.DataItem).fname %></td>
                    <td class="mdel"><%# ((MrMember)Container.DataItem).IsDeleted() ? "X" : "" %></td>
				</tr>
			</ItemTemplate>

			</asp:Repeater>


			</table>
			</ItemTemplate>

        </asp:Repeater>
            <br />
            </div>
          <asp:Label ID="Label2" runat="server" Text="Was change successful?" Font-Bold="True" 
			Font-Italic="True" Font-Size="Medium" ForeColor="#0066FF"></asp:Label>
	   <br /><br />
	<br />

    </asp:Panel>

    <asp:Panel ID="UpdatePanel1" runat="server">
        <asp:Label ID="lblPIDMissing" runat="server" Text="Player ID not in Database" 
            CssClass="bold" Font-Size="Large" ForeColor="#CC0000" Visible="False"></asp:Label>
			<asp:Table ID="Table1" runat="server">
			<asp:TableHeaderRow>
			<asp:TableHeaderCell Width="100px">
				Data Element</asp:TableHeaderCell>
			<asp:TableHeaderCell Width="200px">
				Contents</asp:TableHeaderCell>
			    <asp:TableCell runat="server" Width="200px"> </asp:TableCell>
			</asp:TableHeaderRow>
			    <asp:TableRow>
			        <asp:TableCell Width="100px" HorizontalAlign="Left">
						<asp:Label ID="lblPid" runat="server" Text="Player ID"></asp:Label>
                    
                    </asp:TableCell>
			        <asp:TableCell Width="200px" HorizontalAlign="Left">
						<asp:TextBox ID="tbPid" runat="server" Text="Player ID" Width="180px"></asp:TextBox>
                    
                    </asp:TableCell>
			        <asp:TableCell runat="server">
                        <asp:RangeValidator ID="rvPlayerID" runat="server" ErrorMessage="Invalid Player ID" ControlToValidate="tbPid" MaximumValue="9999" MinimumValue="1"></asp:RangeValidator>
                    </asp:TableCell>
			    </asp:TableRow>
			    <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblMName" runat="server" Text="Member Name"></asp:Label>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="tbMName" runat="server" Text="Member Name" Width="180px"></asp:TextBox>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server">
                    <asp:RequiredFieldValidator ID="rfvMName" runat="server" 
                ErrorMessage="Name is a Required Field" ControlToValidate="tbMName"></asp:RequiredFieldValidator>
                </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblMid" runat="server" Text="Member ID"></asp:Label>
                    
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="tbMid" runat="server" Text="Member ID" Width="180px"></asp:TextBox>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblHcp" runat="server" Text="Hcp"></asp:Label>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="tbHcp" runat="server" Text="Handicap" Width="180px"></asp:TextBox>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblHDate" runat="server" Text="Handicap Date"></asp:Label>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="tbHDate" runat="server" Text="Handicap Date" Width="180px"></asp:TextBox>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                    
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="tbTitle" runat="server" Text="Title" Width="180px"></asp:TextBox>
                
                </asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblGender" runat="server" Text="Gender"></asp:Label>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:DropDownList ID="ddlGender" runat="server" Width="180px">
                        <asp:ListItem Value="0">Male</asp:ListItem>                
                        <asp:ListItem Value="1">Female</asp:ListItem>          
                        </asp:DropDownList>
                    
                    </asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblLName" runat="server" Text="Last Name"></asp:Label>
                    
                </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="tbLName" runat="server" Text="Last Name" Width="180px"></asp:TextBox>
                    
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblFName" runat="server" Text="First Name"></asp:Label>
                    
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:TextBox ID="tbFName" runat="server" Text="First Name" Width="180px"></asp:TextBox>
                    
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="lblMDel" runat="server" Text="Delete"></asp:Label>
                    
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:DropDownList ID="ddlDel" runat="server" Width="180px">
                        <asp:ListItem Value="0">No</asp:ListItem>
                        <asp:ListItem Value="1">Yes</asp:ListItem>
                        </asp:DropDownList>                  
                    </asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                </asp:TableRow>

			</asp:Table>
		<br />
                <asp:ConfirmButtonExtender ID="cbe" runat="server" 
            ConfirmText="Are you sure you want delete this Member?" 
            TargetControlID="btnDelete" onclientcancel="cancelClicked" />

        <table style="width: 50%; margin-left: 5em; margin-right: auto"><tr>
        <td>
        <asp:Button ID="btnSave" runat="server" Text="Save Change" 
				onclick="btnSave_Click" />
            </td>
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Delete Member" 
                onclick="btnDelete_Click" />
            </td>
        <td>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel Change" 
				onclick="btnCancel_Click" />
            </td>
        </tr></table>
            <p id="cancelMsg"><asp:Label ID="lblErrorMsg" runat="server" Text="Error" 
                    ForeColor="Red" Visible="False"></asp:Label>
             </p>

    </asp:Panel>
    <p>
        &nbsp;</p>
        
    </asp:Panel>
</asp:Content>

