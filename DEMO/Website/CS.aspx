<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CS.aspx.cs" Inherits="Website.CS" %>
<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Website.WebForm1" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" Text="

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
       <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                Source Language
            </td>
            <td>
                <asp:DropDownList ID="ddlSource" runat="server" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged">
                    <asp:ListItem Value="AF" Text="Afrikanns" />
                    <asp:ListItem Value="SQ" Text="Albanian" />
                    <asp:ListItem Value="AR" Text="Arabic" />
                    <asp:ListItem Value="HY" Text="Armenian" />
                    <asp:ListItem Value="EU" Text="Basque" />
                    <asp:ListItem Value="BN" Text="Bengali" />
                    <asp:ListItem Value="BG" Text="Bulgarian" />
                    <asp:ListItem Value="CA" Text="Catalan" />
                    <asp:ListItem Value="KM" Text="Cambodian" />
                    <asp:ListItem Value="ZH" Text="Chinese (Mandarin)" />
                    <asp:ListItem Value="HR" Text="Croation" />
                    <asp:ListItem Value="CS" Text="Czech" />
                    <asp:ListItem Value="DA" Text="Danish" />
                    <asp:ListItem Value="NL" Text="Dutch" />
                    <asp:ListItem Value="EN" Text="English" Selected="True" />
                    <asp:ListItem Value="ET" Text="Estonian" />
                    <asp:ListItem Value="FJ" Text="Fiji" />
                    <asp:ListItem Value="FI" Text="Finnish" />
                    <asp:ListItem Value="FR" Text="French" />
                    <asp:ListItem Value="KA" Text="Georgian" />
                    <asp:ListItem Value="DE" Text="German" />
                    <asp:ListItem Value="EL" Text="Greek" />
                    <asp:ListItem Value="GU" Text="Gujarati" />
                    <asp:ListItem Value="HE" Text="Hebrew" />
                    <asp:ListItem Value="HI" Text="Hindi" />
                    <asp:ListItem Value="HU" Text="Hungarian" />
                    <asp:ListItem Value="IS" Text="Icelandic" />
                    <asp:ListItem Value="ID" Text="Indonesian" />
                    <asp:ListItem Value="GA" Text="Irish" />
                    <asp:ListItem Value="IT" Text="Italian" />
                    <asp:ListItem Value="JA" Text="Japanese" />
                    <asp:ListItem Value="JW" Text="Javanese" />
                    <asp:ListItem Value="KO" Text="Korean" />
                    <asp:ListItem Value="LA" Text="Latin" />
                    <asp:ListItem Value="LV" Text="Latvian" />
                    <asp:ListItem Value="LT" Text="Lithuanian" />
                    <asp:ListItem Value="MK" Text="Macedonian" />
                    <asp:ListItem Value="MS" Text="Malay" />
                    <asp:ListItem Value="ML" Text="Malayalam" />
                    <asp:ListItem Value="MT" Text="Maltese" />
                    <asp:ListItem Value="MI" Text="Maori" />
                    <asp:ListItem Value="MR" Text="Marathi" />
                    <asp:ListItem Value="MN" Text="Mongolian" />
                    <asp:ListItem Value="NE" Text="Nepali" />
                    <asp:ListItem Value="NO" Text="Norwegian" />
                    <asp:ListItem Value="FA" Text="Persian" />
                    <asp:ListItem Value="PL" Text="Polish" />
                    <asp:ListItem Value="PT" Text="Portuguese" />
                    <asp:ListItem Value="PA" Text="Punjabi" />
                    <asp:ListItem Value="QU" Text="Quechua" />
                    <asp:ListItem Value="RO" Text="Romanian" />
                    <asp:ListItem Value="RU" Text="Russian" />
                    <asp:ListItem Value="SM" Text="Samoan" />
                    <asp:ListItem Value="SR" Text="Serbian" />
                    <asp:ListItem Value="SK" Text="Slovak" />
                    <asp:ListItem Value="SL" Text="Slovenian" />
                    <asp:ListItem Value="ES" Text="Spanish" />
                    <asp:ListItem Value="SW" Text="Swahili" />
                    <asp:ListItem Value="SV" Text="Swedish " />
                    <asp:ListItem Value="TA" Text="Tamil" />
                    <asp:ListItem Value="TT" Text="Tatar" />
                    <asp:ListItem Value="TE" Text="Telugu" />
                    <asp:ListItem Value="TH" Text="Thai" />
                    <asp:ListItem Value="BO" Text="Tibetan" />
                    <asp:ListItem Value="TO" Text="Tonga" />
                    <asp:ListItem Value="TR" Text="Turkish" />
                    <asp:ListItem Value="UK" Text="Ukranian" />
                    <asp:ListItem Value="UR" Text="Urdu" />
                    <asp:ListItem Value="UZ" Text="Uzbek" />
                    <asp:ListItem Value="VI" Text="Vietnamese" />
                    <asp:ListItem Value="CY" Text="Welsh" />
                    <asp:ListItem Value="XH" Text="Xhosa" />
                </asp:DropDownList>
            </td>
            <td>
            </td>
            <td>
                Target Language
            </td>
            <td>
                <asp:DropDownList ID="ddlTarget" runat="server">
                    <asp:ListItem Value="AF" Text="Afrikanns" />
                    <asp:ListItem Value="SQ" Text="Albanian" />
                    <asp:ListItem Value="AR" Text="Arabic" />
                    <asp:ListItem Value="HY" Text="Armenian" />
                    <asp:ListItem Value="EU" Text="Basque" />
                    <asp:ListItem Value="BN" Text="Bengali" />
                    <asp:ListItem Value="BG" Text="Bulgarian" />
                    <asp:ListItem Value="CA" Text="Catalan" />
                    <asp:ListItem Value="KM" Text="Cambodian" />
                    <asp:ListItem Value="ZH" Text="Chinese (Mandarin)" />
                    <asp:ListItem Value="HR" Text="Croation" />
                    <asp:ListItem Value="CS" Text="Czech" />
                    <asp:ListItem Value="DA" Text="Danish" />
                    <asp:ListItem Value="NL" Text="Dutch" />
                    <asp:ListItem Value="EN" Text="English" />
                    <asp:ListItem Value="ET" Text="Estonian" />
                    <asp:ListItem Value="FJ" Text="Fiji" />
                    <asp:ListItem Value="FI" Text="Finnish" />
                    <asp:ListItem Value="FR" Text="French" Selected="True"/>
                    <asp:ListItem Value="KA" Text="Georgian" />
                    <asp:ListItem Value="DE" Text="German" />
                    <asp:ListItem Value="EL" Text="Greek" />
                    <asp:ListItem Value="GU" Text="Gujarati" />
                    <asp:ListItem Value="HE" Text="Hebrew" />
                    <asp:ListItem Value="HI" Text="Hindi" />
                    <asp:ListItem Value="HU" Text="Hungarian" />
                    <asp:ListItem Value="IS" Text="Icelandic" />
                    <asp:ListItem Value="ID" Text="Indonesian" />
                    <asp:ListItem Value="GA" Text="Irish" />
                    <asp:ListItem Value="IT" Text="Italian" />
                    <asp:ListItem Value="JA" Text="Japanese" />
                    <asp:ListItem Value="JW" Text="Javanese" />
                    <asp:ListItem Value="KO" Text="Korean" />
                    <asp:ListItem Value="LA" Text="Latin" />
                    <asp:ListItem Value="LV" Text="Latvian" />
                    <asp:ListItem Value="LT" Text="Lithuanian" />
                    <asp:ListItem Value="MK" Text="Macedonian" />
                    <asp:ListItem Value="MS" Text="Malay" />
                    <asp:ListItem Value="ML" Text="Malayalam" />
                    <asp:ListItem Value="MT" Text="Maltese" />
                    <asp:ListItem Value="MI" Text="Maori" />
                    <asp:ListItem Value="MR" Text="Marathi" />
                    <asp:ListItem Value="MN" Text="Mongolian" />
                    <asp:ListItem Value="NE" Text="Nepali" />
                    <asp:ListItem Value="NO" Text="Norwegian" />
                    <asp:ListItem Value="FA" Text="Persian" />
                    <asp:ListItem Value="PL" Text="Polish" />
                    <asp:ListItem Value="PT" Text="Portuguese" />
                    <asp:ListItem Value="PA" Text="Punjabi" />
                    <asp:ListItem Value="QU" Text="Quechua" />
                    <asp:ListItem Value="RO" Text="Romanian" />
                    <asp:ListItem Value="RU" Text="Russian" />
                    <asp:ListItem Value="SM" Text="Samoan" />
                    <asp:ListItem Value="SR" Text="Serbian" />
                    <asp:ListItem Value="SK" Text="Slovak" />
                    <asp:ListItem Value="SL" Text="Slovenian" />
                    <asp:ListItem Value="ES" Text="Spanish" />
                    <asp:ListItem Value="SW" Text="Swahili" />
                    <asp:ListItem Value="SV" Text="Swedish " />
                    <asp:ListItem Value="TA" Text="Tamil" />
                    <asp:ListItem Value="TT" Text="Tatar" />
                    <asp:ListItem Value="TE" Text="Telugu" />
                    <asp:ListItem Value="TH" Text="Thai" />
                    <asp:ListItem Value="BO" Text="Tibetan" />
                    <asp:ListItem Value="TO" Text="Tonga" />
                    <asp:ListItem Value="TR" Text="Turkish" />
                    <asp:ListItem Value="UK" Text="Ukranian" />
                    <asp:ListItem Value="UR" Text="Urdu" />
                    <asp:ListItem Value="UZ" Text="Uzbek" />
                    <asp:ListItem Value="VI" Text="Vietnamese" />
                    <asp:ListItem Value="CY" Text="Welsh" />
                    <asp:ListItem Value="XH" Text="Xhosa" />
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button Text="Translate" runat="server" OnClick = "Translate" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID = "txtSource" runat="server" TextMode = "MultiLine" />
            </td>
            <td>
            </td>
            <td colspan="2">
                <asp:TextBox ID = "txtTarget" runat="server" TextMode = "MultiLine" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
