<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.ViewModels.PlayerViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Spillerdetaljer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="PlayerCard">
        <h1><%= Model.Player.Name %></h1>
        [BILDE INN HER]<br />
        <p>Mål pr kamp: <%= Model.AvgGoals %></p>
    </div>

</asp:Content>