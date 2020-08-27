<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="webservice_livsaude._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 290px;
            height: 180px;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <tale>
        <tr>
            <td><img alt="LivSaude" class="auto-style1" src="images/unnamed.png" /></td>
            <td>
        <br />
        <br />
        WEBSERVICE LIVSAUDE</td>

        </tr>
    </tale>
    <p style="text-align: center">

        Digite ou cole a(s) remessas na caixa abaixo e click no botão baixar<p style="text-align: center">

            <asp:TextBox ID="TextBox1" runat="server" Height="160px" TextMode="MultiLine" Width="543px"></asp:TextBox>
        <p style="text-align: center">

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Baixar" Width="99px" />

    <p>
        &nbsp;</p>
    </form>
</body>
</html>
