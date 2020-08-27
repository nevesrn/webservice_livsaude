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

        Digite ou cole a remessa na caixa abaixo e click no botão baixar<p style="text-align: center">

            <asp:TextBox ID="TextBox1" runat="server" Height="160px" TextMode="MultiLine" Width="543px"></asp:TextBox>
        <p style="text-align: center">

            <asp:Button ID="Button1" runat="server" Text="Baixar" Width="99px" OnClick="Button1_Click1" />

        <p style="text-align: center">

            &nbsp;<p style="text-align: center">

            Baixar de uma planilha (arquivo .csv - campos obrigatórios: CODIGO, DATA)<p style="text-align: center">

            <asp:FileUpload ID="FileUpload1" runat="server" Width="454px" />
&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Baixar CSV" />
        <p style="text-align: center">

            <asp:Label ID="UploadStatusLabel" runat="server" ForeColor="#FF3300"></asp:Label>

    <p>
        &nbsp;</p>
        <p>
            <asp:Label ID="lblerror" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
