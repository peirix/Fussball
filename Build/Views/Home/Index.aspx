<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Fussball.Models.Player>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bennett Fussball
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a href="<%= Url.Content("~/Statistics/") %>">Statistikk</a> | <a href="<%= Url.Content("~/Player/") %>">Spillere</a>

    <h2>Velg spillere</h2>
    <form action="<%= Url.Content("~/Home/Play/") %>" method="post" name="StartupForm">
        <div id="BlueTeam">
            <select id="BlueDef" name="BlueDef">
                <option value="0">Velg blå forsvarsspiller</option>
                <%  foreach (var player in Model)
                    { %>
                        <option value="<%= player.ID %>"><%= player.Name %></option>
                <%  } %>
            </select>
            <select id="BlueOff" name="BlueOff">
                <option value="0">Velg blå angrepsspiller</option>
                <%  foreach (var player in Model)
                    { %>
                        <option value="<%= player.ID %>"><%= player.Name %></option>
                <%  } %>
            </select>
        </div>

        <div id="RedTeam">
            <select id="RedDef" name="RedDef">
                <option value="0">Velg rød forsvarsspiller</option>
                <%  foreach (var player in Model)
                    { %>
                        <option value="<%= player.ID %>"><%= player.Name %></option>
                <%  } %>
            </select>
            <select id="RedOff" name="RedOff">
                <option value="0">Velg rød angrepsspiller</option>
                <%  foreach (var player in Model)
                    { %>
                        <option value="<%= player.ID %>"><%= player.Name %></option>
                <%  } %>
            </select>
        </div>
        <button type="submit" style="padding: 10px;">Play game!</button>
    </form>

    <form method="post" action="">
        <h4>Opprett ny spiller</h4>
        <input type="text" name="name"><input type="submit" value="Opprett">
    </form>
</asp:Content>
