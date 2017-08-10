<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<PartyInvites.Models.GuestResponse>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RsvpForm</title>
	<link rel="stylesheet" href="~/Content/Site.css"/>
</head>
<body>
	<h1>RSVP</h1>
	<% using (Html.BeginForm())
	   { 
		%>
		<%: Html.ValidationSummary() %>

	   <p>Your name: <%: Html.TextBoxFor(m => m.Name) %></p>

	   <p>Email address: <%: Html.TextBoxFor(m => m.Email) %></p>

	   <p>Contact phone: <%: Html.TextBoxFor(m => m.Phone) %></p>

	   <p>Will you attend? <%: Html.DropDownListFor(
						m => m.WillAttend,
						new[] {
							new SelectListItem()
								{
									Text = "Yes, I will attend",
									Value = Boolean.TrueString
								},
							new SelectListItem()
								{
									Text = "No, I will not attend",
									Value = Boolean.FalseString
								}
						},
						"Choose an option") %>
		</p>
		<input type="submit" value="Submit RSVP" />
	<% } %>
</body>
</html>
