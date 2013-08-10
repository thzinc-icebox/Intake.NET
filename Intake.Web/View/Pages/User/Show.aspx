<%@ Page Language="C#" MasterPageFile="~/Default.master" %>
<%@ Import Namespace="MPRV.Common" %>
<%@ Register TagPrefix="user" TagName="Result" Src="~/View/Pages/User/Result.ascx" %>

<asp:Content ContentPlaceHolderID="ArticleHeader" ID="ArticleHeaderContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" ID="ArticleBodyContent" runat="server">
	<user:Result runat="server"/>
	<article class="data">
		<%
			var user = Context.Items["User"] as Intake.Core.Model.User;
			var data = Context.Items["Data"] as IPagedEnumerable<Intake.Core.Model.Datum>;
			data.PerPage = 5;
		%>
		<ul>
			<% foreach (var datum in data) { %>
				<li>
					<div class="value"><%: datum.Value %></div>
				</li>
			<% } %>
		</ul>
		<nav>
			<a href="/users/<%: user.Handle %>/data">More</a>
		</nav>
	</article>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleFooter" ID="ArticleFooterContent" runat="server">
</asp:Content>


