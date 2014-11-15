<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Signups_Default" %>
<%@ import namespace="System.Net.Mail"%>
<%@ import namespace="System.Reflection"%>
<%@ import namespace="System.Collections.Specialized"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/playerslist.css" rel="stylesheet" type="text/css" /> 
<script runat="server">

	HtmlTable ShowMessage(System.Net.Mail.MailMessage msg)
	{
		HtmlTable table = new HtmlTable();
		HtmlTableRow topRow = new HtmlTableRow();
		HtmlTableCell fieldHeaderCell = new HtmlTableCell();
		HtmlTableCell valueHeaderCell = new HtmlTableCell();
		fieldHeaderCell.InnerText = "Field";
		topRow.Cells.Add(fieldHeaderCell);
		valueHeaderCell.InnerText = "Value";
		topRow.Cells.Add(valueHeaderCell);
		table.Rows.Add(topRow);

		foreach(PropertyInfo p in msg.GetType().GetProperties())
		{
			HtmlTableRow row = new HtmlTableRow();

			HtmlTableCell labelCell = new HtmlTableCell();

			HtmlTableCell valueCell = new HtmlTableCell();

			if (!((p.Name == "Headers") ||
				  (p.Name == "Fields")  ||
				  (p.Name == "Attachments")))
			{            
				labelCell.InnerText = String.Format("{0}",p.Name);
				row.Cells.Add(labelCell);

				valueCell.InnerText = String.Format("{0}",p.GetValue(msg,null));
				row.Cells.Add(valueCell);
			}

			table.Rows.Add(row);
		}

		return table;
	}
	System.Net.Mail.MailMessage CreateMessage()
	{
		MailDefinition md = new MailDefinition();
		md.CC = sourceCC.Text;
		md.From = sourceFrom.Text;

		md.Subject = sourceSubject.Text;
		md.Priority = MailPriority.Normal;

		ListDictionary replacements = new ListDictionary();
		replacements.Add("<%To%>", sourceTo.Text);
		replacements.Add("<%From%>", md.From);
		//        if (true == useFile.Checked)
		//        { 
		//            System.Net.Mail.MailMessage fileMsg;
		//            fileMsg = md.CreateMailMessage(sourceTo.Text, replacements, this); 
		//            return fileMsg;
		//        } 
		//        else
		//        {
		System.Net.Mail.MailMessage textMsg;
		textMsg = md.CreateMailMessage(sourceTo.Text, replacements, sourceBodyText.Text, this);
		return textMsg;
		//        }
	}

	void createEMail_Click(object sender, System.EventArgs e)
	{
		System.Net.Mail.MailMessage msg = CreateMessage();

		PlaceHolder1.Controls.Add(ShowMessage(msg));
	}

	void sendEMail_Click(object sender, System.EventArgs e)
	{
		System.Net.Mail.MailMessage msg = CreateMessage();

	//	PlaceHolder1.Controls.Add(ShowMessage(msg));

		errorMsg.Text = String.Empty;
		try
		{
			SmtpClient sc = new SmtpClient();
			sc.Send(msg);
		}
		catch (HttpException ex)
		{
			errorMsg.Text = ex.ToString();
		}
        emailSuccess.Visible = true;
        lbContinue.Visible = true;
        pnlList.Visible = false;
        emailSuccess.Visible = true;
        lbContinue.Visible = true;
	}

</script>

   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
	<div>
	<h2>
	Prepare Event Players List
	</h2>

   <asp:Label ID="emailSuccess" runat="server" Text="Email Sent Successfully" ForeColor="Red" Font-Size="Large" Visible="False"></asp:Label>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:HyperLink ID="lbContinue" runat="server" NavigateUrl="~/Signups/Default.aspx" Visible="False">Continue</asp:HyperLink>
        

        <asp:Panel ID="pnlList" runat="server">
<p>	
	<asp:DropDownList ID="DropDownList1" runat="server" 
		onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="700px">
	</asp:DropDownList>
	<asp:Button ID="LoadButton" runat="server" Text="Load Players List" 
		onclick="LoadButton_Click" />

</p>
<p>
		<asp:Label ID="lblEvent" runat="server" Text="Label"></asp:Label>
		<asp:Literal ID="literalOrg" runat="server" Text="<%$ AppSettings:Org %>" 
			Visible="False"></asp:Literal>
		<asp:Literal ID="literalRep" runat="server" Text="<%$ AppSettings:Rep %>" 
			Visible="False"></asp:Literal>
		<asp:Literal ID="literalRepEmail" runat="server" 
			Text="<%$ AppSettings:RepEmail %>" Visible="False"></asp:Literal>
</p>


<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="90%">
	<asp:Label ID="lblCount" runat="server" Text="Label"></asp:Label>
	<br />
<div class="players_list">
		<asp:Repeater ID="PlayersListRepeater" runat="server">
		<ItemTemplate>
		<table>
			<tr>
				<th class="seqno">No.</th>
				<th class="timestamp">Signup Date</th>
				<th class="name">Name</th>
				<th class="gender">Gender</th>
				<th class="hcp">Hcp. Index</th>
				<th class="srule">Rule</th>
<!--				<th class="action">Action</th> -->
				<th class="carpool">Carpool</th>
<!--				<th class="marked">Marked</th> -->
				<th class="name">Guest Name</th>
				<th class="hcp">Guest Hcp.</th>
				<th class="gender">Guest Gender</th>
			   </tr>

			   <asp:Repeater ID="Repeater1"  runat="server" DataSource='<%# Eval("Entries") %>'>
			   <ItemTemplate>
			   <tr>
					<td class="seqno"><%# ((SignupEntry)Container.DataItem).SeqNo %></td>
					<td class="timestamp"><%# ((SignupEntry)Container.DataItem).STDate.ToShortDateString() %></td>
					<td class="name"><%# ((SignupEntry)Container.DataItem).Splayer %></td>
					<td class="gender"><%# ((SignupEntry)Container.DataItem).Ssex %></td>
					<td class="hcp"><%# ((SignupEntry)Container.DataItem).Shcp %></td>
					<td class="srule"><%# ((SignupEntry)Container.DataItem).SspecialRule %></td>
<!--					<td class="action"><%# ((SignupEntry)Container.DataItem).Saction %></td> -->
					<td class="carpool"><%# ((SignupEntry)Container.DataItem).Scarpool %></td>
<!--					<td class="marked"><%# ((SignupEntry)Container.DataItem).Smarked %></td> -->
					<td class="name"><%#((SignupEntry)Container.DataItem).SGuestName %></td>
					<td class="hcp"><%#((SignupEntry)Container.DataItem).SgHcp %></td>
					<td class="gender"><%# ((SignupEntry)Container.DataItem).SgSex %></td>
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
			</ItemTemplate>
	</asp:Repeater>
	<br />

	</div>
 
		</asp:Panel>
		<br />
	<div>
			<table id="Table1" cellspacing="1" 
				style="padding: 1px; width:450px; text-align:center">
				<tr>
					<td align="center" colspan="3">
						<h3>Players List E-mail Message</h3>
					</td>
				</tr>
				<tr>
					<td align="right">To:</td>
					<td style="WIDTH: 10px">
					</td>
					<td>
						<asp:textbox id="sourceTo" runat="server" columns="54">

							</asp:textbox>&nbsp;
					</td>
				</tr>
				<tr>
					<td align="right">Cc:</td>
					<td style="WIDTH: 10px">
					</td>
					<td>
						<asp:textbox id="sourceCC" runat="server" columns="54">
						</asp:textbox>&nbsp;</td>
				</tr>
			  <tr>
					<td align="right">From:</td>
					<td style="WIDTH: 10px">
					</td>
					<td>
						<asp:textbox id="sourceFrom" runat="server" columns="54" 
							Text="<%$ AppSettings:FromEmailAddress %>"></asp:textbox>&nbsp;
					</td>
				</tr>

				<tr>
					<td align="right">Subject:</td>
					<td style="WIDTH: 10px">
					</td>
					<td>
						<asp:textbox id="sourceSubject" runat="server" columns="54">
						</asp:textbox>&nbsp;</td>
				</tr>

				<tr>
				<td>Message:</td><td></td><td></td></tr>
				<tr>
					<td align="center" colspan="3">
						<asp:textbox id="sourceBodyText" runat="server" columns="75" 
							textmode="MultiLine" rows="20"></asp:textbox>&nbsp;</td>
				</tr>
				<tr>
					<td align="center" colspan="3">
						
						<asp:button id="sendEMail" runat="server" 
							text="Send Email"
							onclick="sendEMail_Click">
						</asp:button></td>
				</tr>
			</table>

			<p>&nbsp;</p>
			<p>
				<asp:placeholder id="PlaceHolder1" runat="server">
				</asp:placeholder>&nbsp;
			</p>
			<p>
				<asp:literal id="errorMsg" runat="server">
				</asp:literal>
			</p>
        </div>

        </asp:Panel>
   

</div>
</asp:Content>

