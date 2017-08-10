<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NorthwindMVC.Models.Country>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Countries
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Countries</h2>
	
	<% 
		using (Html.BeginForm())
		{
			int i = 0;
			foreach (var item in Model) 
			{
				string prefix = String.Format("countries[{0}].", i++);
	%>
		<div style="margin-top: 5px">
			<fieldset>
				<legend>Country info</legend>

				<label for="<%: prefix %>Name">Name:</label>
				<input type="text" name="<%: prefix %>Name" value="<%: item.Name %>" />
				<br />

				<label for="<%: prefix %>Detail.Capital">Capital:</label>
				<input type="text" name="<%: prefix %>Detail.Capital" value="<%: item.Detail.Capital %>" />
				<br />

				<label for="<%: prefix %>Detail.Continent">Continent:</label>
				<%: Html.TextBox(prefix + "Detail.Continent", item.Detail.Continent) %>
				<br />

			</fieldset>
		</div>
	<%
			} 
	%>
		<input type="submit" value="Save" />
	<%
		}
	%>

</asp:Content>

