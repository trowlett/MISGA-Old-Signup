<%@ Page Title="Add Event" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="addevent.aspx.cs" Inherits="Events_addevent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Add Event to Club&nbsp;
    <asp:Label ID="lblClub" runat="server" Text="XXX"></asp:Label>
        's&nbsp;Schedule
        </h2>
    <asp:Panel ID="panel_event" runat="server">
        <asp:Label ID="lblHdr" runat="server" Text="Event Information" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <div class="event_info">
        <table>
            <tr>
                <td>Place:</td>
                <td>
                    <asp:DropDownList ID="ddlPlace" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPlace_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Text="select one" value="select"></asp:ListItem>
                        <asp:ListItem Text="Away" Value="Away"></asp:ListItem>
                        <asp:ListItem Text="Home" Value="Home"></asp:ListItem>
                        <asp:ListItem Text="MISGA" Value="MISGA"></asp:ListItem>
                    </asp:DropDownList>
                  </td>
                <td style="width: 70px">&nbsp;</td>
                <td>Event Date</td>
                <td>
                    <asp:TextBox ID="tbDate" runat="server" AutoPostBack="True"></asp:TextBox>
                </td>
                <td style="width: 70px">&nbsp;</td>
                <td>Time:</td>
                <td>
                    <asp:TextBox ID="tbTeeTime" runat="server"></asp:TextBox>                 
                 </td>
            </tr>
            <tr>
                <td>Player Limit:</td>
                <td><asp:TextBox ID="tbPlayerLimit" runat="server"></asp:TextBox></td>
                <td></td>
                <td>Deadline Date:</td>
                <td><asp:TextBox ID="tbDeadLine" runat="server"></asp:TextBox></td>
                <td></td>
                <td>Post Date:</td>
                <td><asp:TextBox ID="tbPostDate" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Cost:</td>
                <td><asp:TextBox ID="tbCost" runat="server" Width="50px"></asp:TextBox>
                    <asp:CheckBox ID="cbCash" runat="server" Text="Cash Only" TextAlign="Left" /></td>
                <td></td>
                <td>Special Rule:</td>
                <td><asp:TextBox ID="tbSpecialRule" runat="server"></asp:TextBox></td>
                <td></td>
                <td></td>
                <td><asp:CheckBox ID="cbGuest" runat="server" Text="Guest OK" TextAlign="Left" /></td>

                
            </tr>
        </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="AwayPanel" runat="server" Visible="false">
        <asp:Label ID="Label1" runat="server" Text="Away Event Details" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <br /><br />
        <div class="event_info">
        <asp:Button ID="btnAwaySelect" runat="server" Text="Select Host Club" OnClick="btnAwaySelect_Click" />
        <asp:DropDownList ID="ddlAwayHost" runat="server" DataSourceID="SqlDataSource1" DataTextField="ClubName" DataValueField="ClubID" Width="250px"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ClubsConnect %>" SelectCommand="SELECT [ClubID], [ClubName] FROM [Clubs] ORDER BY [ClubName]"></asp:SqlDataSource>
        </div>
    </asp:Panel>
    <asp:Panel ID="HomePanel" runat="server" Visible="false">
        <asp:Label ID="Label2" runat="server" Text="Home Event Details" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <br /><br />
        <div class="event_info">
            <table>
                <tr>
                    <td><asp:Label ID="lblVisitingClubs" runat="server" Text="Visiting Clubs: "></asp:Label>&nbsp;&nbsp;</td>
                    <td></td>
                    <td><asp:TextBox ID="tbHomeTitle" runat="server" Width="600px"></asp:TextBox></td>
                </tr>
         
                <tr>
                    <td><asp:Button ID="btnHomeSelect" runat="server" Text="Add Visiting Club" OnClick="btnHomeSelect_Click" /></td>
                    <td></td>
                    <td><asp:DropDownList ID="ddlVisit1" runat="server" DataSourceID="SqlDataSource1" DataTextField="ClubName" DataValueField="ClubID" Width="250px">
                        <asp:ListItem Selected="True">select club</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="MISGAPanel" runat="server" Visible="false">
        <asp:Label ID="Label3" runat="server" Text="MISGA Event Details" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <br /><br />
        <div class="event_info">
            <asp:Label ID="lblMISGATitle" runat="server" Text="Event Title:"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="tbMISGATitle" runat="server" Width="600px"></asp:TextBox>
            <br />
            <br />
        <asp:Button ID="btnMISGASelect" runat="server" Text="Select Host Club" />
        <asp:DropDownList ID="ddlMISGAHost" runat="server" DataSourceID="SqlDataSource1" DataTextField="ClubName" DataValueField="ClubID" Width="250px"></asp:DropDownList>
            <br />

        </div>
    </asp:Panel>
    <br /><br />
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="buttons" runat="server">
            <asp:TableCell ID="sp5" runat="server" Width="200px">&nbsp;</asp:TableCell>
            <asp:TableCell ID="idSave" runat="server" Width="200px">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="80px" />
            </asp:TableCell>
            <asp:TableCell ID="idCancel" runat="server" Width="200px">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Width="80px" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

