<%@ Page Language="C#" MasterPageFile="~/Default.master"%>
<%@ Register TagPrefix="user" TagName="Result" Src="~/View/Pages/User/Result.ascx" %>

<asp:Content ContentPlaceHolderID="ArticleHeader" ID="ArticleHeaderContent" runat="server">
	<user:Result id="UserResult" runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" ID="ArticleBodyContent" runat="server">
	<% Intake.Core.Model.Datum datum = Context.Items["Datum"] as Intake.Core.Model.Datum;%>
	<article class="datum">
		<dl>
			<dt>
				<%: datum.Value %>
			</dt>
			<dd>
				<p>
					<%: datum.Description %>
				</p>
				<span class="date">
					<%: datum.Date.ToLocalTime() %>
				</span>
				<ul class="tags">
					<% foreach (var tag in datum.Tags) { %>
						<li>
							<a href="/data/tag/<%: tag %>"><%: tag %></a>
						</li>
					<% } %>
				</ul>
			</dd>
		</dl>
		<div class="location">
			<% if (datum.Latitude.HasValue && datum.Longitude.HasValue) { %>
				<em>Map</em>
				<div class="coordinates">
					<span class="latitude"><%: datum.Latitude %></span>
					<span class="longitude"><%: datum.Longitude %></span>
					<% if (datum.Accuracy.HasValue) { %>
						<span class="accuracy"><%: datum.Accuracy %> meters</span>
					<% } %>
				</div>
			<% } %>
		</div>
	</article>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleFooter" ID="ArticleFooterContent" runat="server">
</asp:Content>