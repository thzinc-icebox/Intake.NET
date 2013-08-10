<%@ Page Language="C#" MasterPageFile="~/Default.master" %>
<asp:Content ContentPlaceHolderID="ArticleHeader" ID="ArticleHeaderContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" ID="ArticleBodyContent" runat="server">
	<% Intake.Core.Model.User user = Context.Items["User"] as Intake.Core.Model.User; %>
	<article class="user">
		<div class="primary">
			<img src="about:blank" alt="Avatar" class="avatar"/>
			<h2>
				<a href="/users/<%= user.Handle %>"><%= user.Handle %></a>
			</h2>
		</div>
		<div class="secondary">
			<h3>
				<%= user.Name %>
			</h3>
		</div>
	</article>
	<%--
	<article class="data">
		<ul>
			<% foreach (var datum in user.Data) { %>
				<li>
					<div class="value"><%= datum.Value %></div>
				</li>
			<% } %>
		</ul>
		<nav>
			<a href="/users/<%= user.Handle %>/data">View <%= user.Data.TotalItems - user.Data.Count %> more</a>
		</nav>
	</article>
	--%>
	<article class="locations">
		<ul>
			<li>location...</li>
		</ul>
	</article>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleFooter" ID="ArticleFooterContent" runat="server">
</asp:Content>


