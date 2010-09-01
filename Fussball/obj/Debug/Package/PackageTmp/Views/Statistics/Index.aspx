<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Dictionary<string, Fussball.Models.Player>>" %>
<%@ Register Assembly="System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" TagPrefix="Test" Namespace="System.Data.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <ul>
    <%  foreach (var player in Model) 
        { %>
            <li><%= player.Key %> - <%= player.Value.Name %>
    <%  } %>
    </ul>

</asp:Content>

