<%@ Page Language="C#" MasterPageFile="~/default.master" %>
<%@ Import Namespace="MPRV.Common" %>
<asp:Content ContentPlaceHolderID="ArticleHeader" ID="ArticleHeaderContent" runat="server">
	<h2>Data</h2>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" ID="ArticleBodyContent" runat="server">
	<%
		var data = Context.Items["Data"] as IPagedEnumerable<Intake.Core.Model.Datum>;
		long page;
		data.Page = long.TryParse(Context.Request.QueryString["page"], out page) ? page : 1;
		data.PerPage = 25;
	%>
	<ul class="data">
		<% foreach (var datum in data) { %>
			<li>
				<article class="datum">
					<article class="user">
						<div class="primary">
							<img src="about:blank" alt="Avatar" class="avatar"/>
							<h2>
								<a href="/users/<%: datum.User.Handle %>"><%: datum.User.Handle %></a>
							</h2>
						</div>
					</article>
					<dl>
						<dt>
							<a href="/data/<%: datum.DatumId %>">
								<%: datum.Value %>
							</a>
						</dt>
						<dd>
							<ul class="tags">
								<% foreach (var tag in datum.Tags) { %>
									<li>
										<a href="/data/tag/<%: tag %>"><%: tag %></a>
									</li>
								<% } %>
							</ul>
						</dd>
					</dl>
				</article>
			</li>
		<% } %>
	</ul>
	<nav>
		<p>Displaying max <%: data.PerPage %> data per page out of <%: data.TotalItems %> data total.</p>
		<p>Page <%: data.Page %> of <%: data.TotalPages %></p>
	</nav>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleFooter" ID="ArticleFooterContent" runat="server">
</asp:Content>
