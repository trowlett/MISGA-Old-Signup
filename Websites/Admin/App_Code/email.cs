using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Collections.Specialized;

/// <summary>
/// Summary description for whsmail
/// </summary>
public class email
{

    private string errorMsg;


    public string SendEMail(string to, string from, string subject, string body)
    {
        MailAddress sourceTo = new MailAddress(to);
        MailAddress sourceFrom = new MailAddress(from);
        MailMessage msg = new MailMessage(sourceFrom, sourceTo);
        msg.Subject = subject;
        msg.Body = body;

        errorMsg = string.Empty;

        try
        {
            SmtpClient sc = new SmtpClient();
            sc.Send(msg);
        }
        catch (HttpException ex)
        {
            errorMsg = ex.ToString();
        }
        return errorMsg;
    }


	public email()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}