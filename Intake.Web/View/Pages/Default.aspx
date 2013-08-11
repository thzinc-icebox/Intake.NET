<%@ Page Language="C#" MasterPageFile="~/Default.master" %>
<%@ Import Namespace="Intake.Web" %>
<asp:Content ContentPlaceHolderID="ArticleHeader" runat="server">
	<h1>Intake</h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" runat="server">
	<p>
		Intake is a simple service to allow people to track data. On anything. You could track how many miles you walk in a day. Or the number of people you shake hands with. Or anything. It's up to you.
	</p>
	
	<div class="promotion">
		<% if ((Master as Default).IsLoggedIn) { %>
			<a href="/users/<%: (Master as Default).CurrentHandle %>/data" class="data">
				View my data
			</a>
		<% } else { %>
			<a href="/users/new" class="new user">
				Sign up
			</a>
			
			<a href="/login" class="login">
				Log in
			</a>
		<% } %>
	</div>
</asp:Content>