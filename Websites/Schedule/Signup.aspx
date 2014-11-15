<%@ Page Title="Musket Ridge MISGA Sign Up Form" Language="C#" Debug="true" MasterPageFile="~/MRMisga.master" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="Schedule_Signup" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/SignupStyles.css" rel="stylesheet" type="text/css" />
	<link href="Styles/playerslist.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		.style1
		{
			width: 190px;
			height: 25px;
            border: none;
		}
		.style2
		{
			height: 25px;
            border: none;
		}
	</style>
	<script type="text/C#">
	int count = 5;
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="signup">
	<div class="signuphdr">
		<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
		</asp:ToolkitScriptManager>
	<h2 style="margin: 0px 0px 0px 0px">Mixer Information and Sign Up<br /><span style="font-size: medium">(for 
		<asp:Literal ID="litOrg1" runat="server" Text="[Org]"></asp:Literal> Members ONLY)</span></h2>
	<p style="margin: 0px 0px 0px 0px"><asp:Label ID="lbl01" runat="server" Text="Mixer ID: "></asp:Label>
		<asp:Label ID="lblEventID" runat="server" Text="xxxxxxxxx"></asp:Label></p>
	 </div> 
			<div id="mixerinfo">
			<asp:Literal ID="litInfo" runat="server" Mode="PassThrough"></asp:Literal>
			</div>

        <table style="width:100%;"><tr>
            <td style="text-align: left;border: none;"><asp:Label ID="lblAction" runat="server" 
                    Visible="False">&nbsp;</asp:Label>
                    <asp:LinkButton ID="linkbuttonSR" runat="server" Visible="False">
                    Click here for Help Computing Your Course Handicap</asp:LinkButton></td>
            <td style="width: 50%;text-align: right;border: none;"><asp:LinkButton ID="LinkButton1" runat="server">
                Click here for Sign-up Instructions</asp:LinkButton></td>  
          </tr></table>                      

		<asp:ModalPopupExtender ID="MPE1" runat="server"
            Enabled="True" TargetControlID="LinkButton1" 
			OkControlID="btnDone" PopupControlID="Panel2">
		</asp:ModalPopupExtender>
        <asp:ModalPopupExtender ID="MPESR" runat="server"
            enabled="true" TargetControlID="linkbuttonSR"
            OkControlID="btnOK" PopupControlID="PanelSR">
        </asp:ModalPopupExtender>
<asp:panel id="Panel2" style="display: none" runat="server" BackColor="#FFFFCC" 
			BorderColor="#6699FF" BorderStyle="Double" BorderWidth="2px" 
			HorizontalAlign="Justify" ScrollBars="Auto" Width="600px" 
			CssClass="instruction_modal">
	<div class="signupPopup">
				<div class="PopupHeader" id="PopupHeader" 
					style="font-family: Cambria; font-size: large; font-weight: bold; font-style: normal; font-variant: small-caps; text-transform: none; color: #6699FF" 
					title="Sign Up Instructions">Signup Instructions</div>
				<div class="PopupBody">
					<p>To have your name placed on the Players List for this event:</p>
					<ol>
					<li>enter your First Name and Last Name in the boxes on the SIGN-UP FORM below;</li>
					<li>select either "Sign-up" to put your name on the players list or "Cancel" if you 
						signed up previously and want to take your name off the players list; and</li>
					<li>select whether you want to carpool or not (most carpools meet at the Frederick VFW parking lot).</li></ol>
					<p>When you complete the form, click on the SUBMIT button.  If you selected "Sign-up", 
						your name will appear at the top of the list on the right under "Who's Signed Up". All previous entries you have made will 
						be eliminated. If you selected "Cancel", all of your previous entries will be eliminated and the "Cancel" entry will not 
						be displayed.</p>
					<p>If at a later time, you decide you want to change your submission, just complete another SIGN-UP FORM and your previous entry will be 
						changed.  Make sure that the spelling of your name is the same for all entries; otherwise, you will have an entry for 
						each way you spelled your name.</p>

					<p>If you do not want to sign-up for this event, select 
						one of the menu items above to leave this page.
						</p>
                    <p>For events that are closed, you cannot CANCEL here within 
                        24 hours of the event. Call the Host Club's Golf Shop to cancel.</p>
				</div>
				<div class="PopupControls">
					<input id="btnDone" type="button" value="Done" />
<!--					<input id="btnCancel" type="button" value="Cancel" /> -->
		</div>
		</div>

        </asp:panel>

<asp:panel id="PanelSR" style="display: none" runat="server" BackColor="#FFFFCC" 
			BorderColor="#6699FF" BorderStyle="Double" BorderWidth="2px" 
			HorizontalAlign="Justify" ScrollBars="Auto" Width="600px" 
			CssClass="instruction_modal">
	<div class="signupPopup">
				<div class="PopupHeader" id="Div1" 
					style="font-family: Cambria; font-size: large; font-weight: bold; font-style: normal; font-variant: small-caps; text-transform: none; color: #6699FF" 
					title="Sign Up Instructions">Help for Computing Your Course Handicap</div>
				<div class="PopupBody">
					<p></p>
					<ol>
					<li>multiply your current handicap index by the slope of the regular MISGA tees of the host course;</li>
					<li>divide that number by 113; and</li>
					<li>the result is your handicap for the regular MISGA Tees at the host course.</li></ol>
					<p></p>
					<p></p>

					<p>
						</p>
                    <p></p>
				</div>
				<div class="PopupControls">
					<input id="btnOK" type="button" value="OK" />
<!--					<input id="btnCancel" type="button" value="Cancel" /> -->
		</div>
		</div>

        </asp:panel>
<!-- ------------------------------------------------------------------------------------------------------------------- -->

   <table style="border-collapse:separate; table-layout: fixed; border-spacing: 2px; empty-cells: show; caption-side: bottom; width: 100%" 
		title="Signup Form">
		  <tr>         
   <td class="signupform">
          <asp:Panel ID="SignupPanel" runat="server">


   <table><tr><td style="width: 175px;border: none;">
   <h2><asp:Literal ID="lblSignupForm" runat="server" Text="Sign-up Form"></asp:Literal>
	   </h2>
	   </td>
       <td style="border: none;">
		<asp:RadioButtonList ID="rblAction" runat="server"
			RepeatDirection="Horizontal" BorderStyle="None">
			<asp:ListItem Selected="True">Sign-up</asp:ListItem>
			<asp:ListItem>Cancel</asp:ListItem>
			</asp:RadioButtonList>
		</td></tr>
       </table>
       </asp:Panel>
              <asp:Panel ID="ClosedPanel" runat="server">
                <table><tr>
                   <td style="width:175px;border: none;"><h2>Sign-up Closed</h2></td>
                    <td style="text-align: center;">
                           <asp:RadioButton ID="rbClosedCancel" runat="server" Text="Cancel Me" 
                               Checked="False" Font-Bold="True" AutoPostBack="True" 
                               OnCheckedChanged="rbClosedCancel_CheckedChanged" Visible="False" />
                    </td>
                 </tr></table>
        </asp:Panel>
       <asp:Panel ID="ClosedMessagePanel" runat="server" Visible="False">
                <asp:Label ID="lblClosed" runat="server" Text="Closed Message"></asp:Label>

        </asp:Panel>
   <asp:Panel ID="PlayerPanel" runat="server">


	<asp:Label ID="lblFN" runat="server" Text="First Name:  " Width="100px"></asp:Label>
	   <asp:TextBox ID="FirstNameTextBox" runat="server" Height="21px" Width="230px"></asp:TextBox>

	   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
		   ControlToValidate="FirstNameTextBox" Display="Dynamic" 
		   ErrorMessage="* Please enter your First Name before clicking Submit" 
		   Font-Bold="True" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
	   <br />
	   <asp:Label ID="lblLN" runat="server" Text="Last Name:  " Width="100px"></asp:Label>
	   <asp:TextBox ID="LastNameTextBox" runat="server" Height="21px" Width="230px"></asp:TextBox>

	   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
		   ControlToValidate="LastNameTextBox" Display="Dynamic" 
		   ErrorMessage="* Please enter your Last Name before clicking Submit" 
		   Font-Bold="True" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator><br />
       <asp:Label ID="lblAccessCode" runat="server" Text="Access Code: " Width="100px"></asp:Label>
       <asp:TextBox ID="tbAccessCode" runat="server" ToolTip="Enter Access Code" TextMode="Password"></asp:TextBox> 
         
	<asp:table ID="tblGenderPool" runat="server" style="margin-top: 10px" BorderStyle="None">
        <asp:TableRow ID="tblRowGenderPool" runat="server">
            <asp:TableCell ID="tblCellGender" runat="server" Width="186">
		        <asp:Label ID="lblgender" runat="server" Text="Gender:"></asp:Label>
		        <asp:RadioButtonList ID="rblgender" runat="server"
			        RepeatDirection="Horizontal" CellPadding="2" CellSpacing="2" RepeatLayout="Flow" ToolTip="Genter selection">
			    <asp:ListItem Selected="True">Male</asp:ListItem>
			    <asp:ListItem>Female</asp:ListItem>
			        </asp:RadioButtonList>
        </asp:TableCell>
            <asp:TableCell ID="tblCellCarpool" runat="server" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="152px" HorizontalAlign="Right">
				<asp:CheckBox ID="cbCarpool" runat="server" Text="Carpool" />
            </asp:TableCell>
		</asp:TableRow>
    </asp:table>	
       </asp:Panel>	
	   <asp:Panel ID="SpecialRulePanel" runat="server" Direction="LeftToRight">
			<asp:CheckBox ID="cbGuestRule" runat="server" Text="cbGuestRule" BackColor="#FFFFCC" 
				   BorderColor="#FFFFCC" ForeColor="#660033" Visible="False" />

           <asp:Panel ID="SRCBLPanel" runat="server" Visible="False">
           <asp:Label ID="Label1" runat="server" Text="Special Choices:  "  BackColor="#FFFFCC" 
				BorderColor="#FFFFCC" ForeColor="#660033" ToolTip="Select 'Default' to reset your Special Choices selection"></asp:Label>
           <asp:RadioButtonList ID="SRCBKL" runat="server" Visible="False" BackColor="#FFFFCC"
               BorderColor="#FFFFCC" ForeColor="#660033" RepeatLayout="Flow" RepeatDirection="Horizontal" ToolTip="Select 'Default' to reset your Special Choices selection">
               <asp:ListItem Selected="True" Text="Test" Value="Test"></asp:ListItem>
           </asp:RadioButtonList>


           </asp:Panel>	
	
	   </asp:Panel>

	   <asp:Panel ID="GuestPanel" runat="server" Visible="false" BorderColor="Blue" 
		   BorderStyle="Solid" BorderWidth="1px">

	   <h4>&nbsp;Partner Information</h4>
	   <asp:Label ID="lblGFN" runat="server" Text="&nbsp;First Name:  " Width="100px"></asp:Label>
	   <asp:TextBox ID="tbGFN" runat="server" Width="230px" Height="21px"></asp:TextBox>
	   <br />
	   <asp:Label ID="lblGLN" runat="server" Text="&nbsp;Last Name:  " Width="100px"></asp:Label>
	   <asp:TextBox ID="tbGLN" runat="server" Width="230px" Height="21px"></asp:TextBox>
	   <br />
	   <asp:Label ID="lblGHcp" runat="server" Text="&nbsp;Handicap:  " Width="100px"></asp:Label>
	   <asp:TextBox ID="tbGHcp" runat="server" Width="230px" Height="21px"></asp:TextBox>
	   <table style="margin-top: 10px"><tr>
		<td class="style1">
		<asp:Label ID="lblGgender" runat="server" Text="Gender:"></asp:Label>
			<asp:RadioButtonList ID="rblGgender" runat="server"
			RepeatDirection="Horizontal" CellPadding="2" CellSpacing="2" RepeatLayout="Flow">
			<asp:ListItem Selected="True">Male</asp:ListItem>
			<asp:ListItem>Female</asp:ListItem>
			</asp:RadioButtonList>
			</td>
			<td class="style2"></td>
		</tr></table>
		   </asp:Panel>
	   <br />

       <asp:Panel ID="SubmitPanel" runat="server">       
       <table><tr><td style="width: 70px;border: none;">

	   <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
		   onclick="btnSubmit_Click" />
		   </td>
		   <td style="border: none;"><p class="nomrmisga">Entries made by anyone other than 
			   <asp:Literal ID="litOrg2" runat="server" Text="[Org]"></asp:Literal> members will be deleted.</p>

                </td></tr></table>

           </asp:Panel>
       <br />

   </td>
   <td id="whoisgoing">

   <h2 style="margin-bottom: 3px">Who's Signed Up?</h2>
       <p class="limit">
		<asp:Label ID="lblPlayerLimit" runat="server" Text="Label"></asp:Label></p>

<asp:Panel ID="NormalPanel" runat="server" HorizontalAlign="Center" Width="100%">
<div class="players_list">
		<asp:Repeater ID="PlayersListRepeater" runat="server">
		<ItemTemplate>
		<table style="margin-left: auto; margin-right: auto">
			<tr>
				<th class="seqno"></th>
				<th class="name">Name</th>
				<th class="srule"></th>
				<th class="carpool">Ride</th>
				<th class="timestamp">Date</th> 
			   </tr>

			   <asp:Repeater ID="Repeater1"  runat="server" DataSource='<%# Eval("Entries") %>'>
			   <ItemTemplate>
			   <tr>
					<td class="seqno"><%# ((SignupEntry)Container.DataItem).SeqNo %></td>
					<td class="name"><%# ((SignupEntry)Container.DataItem).Splayer %></td>
					<td class="srule"><%# ((SignupEntry)Container.DataItem).SspecialRule %></td>
					<td class="carpool"><%# ((SignupEntry)Container.DataItem).Scarpool %></td>
					<td class="timestampitem"><%# ((SignupEntry)Container.DataItem).STDate.ToString("MM/d h:mm t") %></td>
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
			</ItemTemplate>
	</asp:Repeater>

	</div>
 
		</asp:Panel>

<asp:Panel ID="GuestListPanel" runat="server" HorizontalAlign="Center" Width="100%">
<div class="players_list">
		<asp:Repeater ID="GuestListRepeater" runat="server">
		<ItemTemplate>
		<table style="margin-left: auto; margin-right: auto">
			<tr>
				<th class="seqno"></th>
				<th class="name">Name</th>
				<th class="srule"></th>
				<th class="guest">Partner</th>
				<th class="carpool">Ride</th>
				<th class="timestamp">Date</th> 
			   </tr>

			   <asp:Repeater ID="Repeater1"  runat="server" DataSource='<%# Eval("Entries") %>'>
			   <ItemTemplate>
			   <tr>
					<td class="seqno"><%# ((SignupEntry)Container.DataItem).SeqNo %></td>
					<td class="name"><%# ((SignupEntry)Container.DataItem).Splayer %></td>
					<td class="srule"><%# ((SignupEntry)Container.DataItem).SspecialRule %></td>
					<td class="guest"><%# ((SignupEntry)Container.DataItem).SGuestName %></td>
					<td class="carpool"><%# ((SignupEntry)Container.DataItem).Scarpool %></td>
					<td class="timestamp"><%# ((SignupEntry)Container.DataItem).STDate.ToString("MM/d h:mm t") %></td>
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
			</ItemTemplate>
	</asp:Repeater>

	</div>	
	</asp:Panel>
   </td>
   </tr>

   </table>
	</div>
   
	</asp:Content>

