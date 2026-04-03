<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default1.aspx.cs" Inherits="DirecPay._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Order Now</title>    
     <script>
			function generateAction()
			{
			    debugger;
			    var chkStatus1 = document.getElementById("chkConfirm");
			    if (chkStatus1.checked)
			    {
			        var EncryptTrans = document.getElementById("requestparams").value;
			        var merchIdVal = document.getElementById("merchantId").value;
			        document.frmPost.EncryptTrans.value = EncryptTrans;
				document.frmPost.merchIdVal.value = merchIdVal;
				document.frmPost.action = "https://www.sbiepay.sbi/secure/AggregatorHostedListener";
				document.frmPost.submit();
				}
				else
                {  
                   alert('please select the checkbox below');              
                   chkStatus1.setAttribute("ForeColor","Red");
                }
			}			
		</script>
</head>
<body>
    <form id="frmPost" runat="server"  name="frmPost">    
    <div>
    <div>
       <table width="300px" style="display:block">				
				<tr>
					<td><asp:Label id="lblMerchantId" runat="server">Merchant Id</asp:Label></td>
					<td width="20px"><b>:</b></td>
					<td><asp:TextBox id="merchantId" Text="1000914" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblColloboratorId" runat="server">Aggregator Id</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="collaboratorId" Text="SBIEPAY" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblOperatingMode" runat="server">Operating Mode</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="operatingMode" Text="DOM" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblCountry" runat="server">Country</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="CountryCode" Text="IN" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblCurrency" runat="server">Currency</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="currency_Code" Text="INR" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblAmount" runat="server">Amount</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="amt" Text="2" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblOrderNumber" runat="server">Order Number</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="orderNumber" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblOtherDetails" runat="server">Other Details</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="otherDetails" Text="Other" runat="server"></asp:TextBox></td>
				</tr>
				<%--<tr>
					<td><asp:Label id="lblSuccessUrl" runat="server">Success URL</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="successUrl" Text="http://localhost:30166/student/home/PGSucess" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblFailureUrl" runat="server">Failure URL</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="failureUrl" Text="http://localhost:30166/student/home/PGFailed" runat="server"></asp:TextBox></td>
				</tr>--%>
           <tr>
					<td><asp:Label id="lblSuccessUrl" runat="server">Success URL</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="successUrl" Text="https://test.sbiepay.com/secure/sucess.jsp" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="lblFailureUrl" runat="server">Failure URL</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="failureUrl" Text="https://test.sbiepay.com/secure/fail.jsp" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="Label3" runat="server">Customer ID</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="txtMerCustomerId" Text="2" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="Label4" runat="server">PayMode</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="txtpayMode" Text="NB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="Label5" runat="server">Accessmedium</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="txtAccessmedium" Text="ONLINE" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><asp:Label id="Label6" runat="server">Transaction Source</asp:Label></td>
					<td><b>:</b></td>
					<td><asp:TextBox id="txtTransSource" Text="ONLINE" runat="server"></asp:TextBox></td>
				</tr>7
				<tr>
               <td colspan="3">
               <asp:HiddenField id="requestparams" runat="server"></asp:HiddenField>
               <asp:HiddenField id="billingDtls" runat="server"></asp:HiddenField>
               <asp:HiddenField id="shippingDtls" runat="server"></asp:HiddenField>
               <asp:TextBox id="HiddenField1" runat="server"></asp:TextBox>
               <asp:TextBox id="HiddenField2" runat="server"></asp:TextBox>
               <asp:TextBox id="HiddenField3" runat="server"></asp:TextBox>
               </td>
           </tr>
			</table>
			<br />
			<asp:Label ID="Label2" runat="server" Text="Billing Details" Font-Bold="True" Font-Size="Larger"></asp:Label>
			<br />
        <table style="display:none">       
            <tr>
                <td>Name
                </td>
                <td>
                    <asp:TextBox ID="txtbillingUserName" Text="Ajay Khatri" runat="server"></asp:TextBox>
                </td>                
                <td>City
                </td>
                <td><asp:TextBox ID="txtbillingCity" Text="Mumbai" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>State
                </td>
                <td ><asp:TextBox ID="txtbillingState" Text="Maharashtra" runat="server"></asp:TextBox>
                </td>
                <td>Pincode
                </td>
                <td><asp:TextBox ID="txtBillingPinCode" Text="403706" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>Country
                </td>
                <td ><asp:TextBox ID="txtBillingCountry" Text="India" runat="server"></asp:TextBox>
                </td>                
                <td>CountryCode
                </td>
                <td><asp:TextBox ID="txtBillingCountryCode" Text="+91" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>AreaCode
                </td>
                <td ><asp:TextBox ID="txtBillingAreaCode" Text="222" runat="server"></asp:TextBox>
                </td>                
                <td>PhoneNo
                </td>
                <td><asp:TextBox ID="txtBillingPhoneNO" Text="28000000" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>MobileNo
                </td>
                <td ><asp:TextBox ID="txtBillingMobileNO" Text="9820000000" runat="server"></asp:TextBox>
                </td>                
                <td>EmailId
                </td>
                <td><asp:TextBox ID="txtBillingEmailID" Text="ajaykhatri@timesofmoney.com" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Shipping Details" Font-Bold="True" Font-Size="Larger"></asp:Label>
        <br />
        <table style="display:none">
            <tr>
                <td>Name
                </td>
                <td>
                    <asp:TextBox ID="txtShippingUserName" Text="Himanshu Thosar" runat="server"></asp:TextBox>
                </td>                
                <td>Address
                </td>
                <td><asp:TextBox ID="txtShippingAddress" Text="Mayuresh Enclave, Sector 20, Plat A-211, Nerul(w),Navi-Mumbai,403706" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>City
                </td>
                <td><asp:TextBox ID="txtShippingCity" Text="Mumbai" runat="server"></asp:TextBox>
                </td>
                <td>State
                </td>
                <td><asp:TextBox ID="txtShippingState" Text="Maharashtra" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>Country
                </td>
                <td><asp:TextBox ID="txtShippingCountry" Text="India" runat="server"></asp:TextBox>
                </td>                
                <td>Pincode
                </td>
                <td><asp:TextBox ID="txtShippingPinCode" Text="403706" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>CountryCode
                </td>
                <td><asp:TextBox ID="txtShippingCountryCode" Text="+91" runat="server"></asp:TextBox>
                </td>                
                <td>AreaCode
                </td>
                <td><asp:TextBox ID="txtShippingAreaCode" Text="222" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>PhoneNo
                </td>
                <td><asp:TextBox ID="txtShippingPhoneNo" Text="30988373" runat="server"></asp:TextBox>
                </td>                
                <td>MobileNo
                </td>
                <td><asp:TextBox ID="txtShippingMobileNo" Text="9812345678" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <asp:CheckBox ID="chkConfirm" Text="The above entered details are correct." runat="server" AutoPostBack="True" OnCheckedChanged="chkConfirm_CheckedChanged"/>	
    </div>
    </div>     <script type="text/javascript">
        //window.onload = function () {
        //    document.frmPost.Submit1.click();
        //}
        </script>

    
    
			<table width="300px">
				<tr>
					<td width="120px"></td>
					<td width="20px">
					    <input type="hidden" name="EncryptTrans" value="">
					    <input type="hidden" name="merchIdVal" value="">
					</td>
					
					<td><input type="submit" value="Pay Now" runat="server" onclick="generateAction();" id="Submit1">
                       
					</td>
				</tr>
                <tr>
                    <td colspan="3">
                        </td>
                </tr>
			</table>
	
   <%-- <script src="JS/googletralsteuds.js"></script>
    <script src="JS/googletranslatejspai.js"></script>
    <script src="JS/googletranslateextra.js"></script>
    <script type="text/javascript" src="main.js"></script>
    <script type="text/javascript" src="extra.js"></script>--%>
    <%--<script type="text/javascript">

      // Load the Google Transliterate API
      google.load("elements", "1", {
          packages: "transliteration"
      });

      function onLoad() {
          var options = {
              sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
              destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
              shortcutKey: 'ctrl+g',
              transliterationEnabled: true
          };

          // Create an instance on TransliterationControl with the required
          // options.
          var control = new google.elements.transliteration.TransliterationControl(options);

          // Enable transliteration in the textbox with id
          // 'transliterateTextarea'.
          var ids = ["middlenameHindi"];
          control.makeTransliteratable(ids);
      }
      google.setOnLoadCallback(onLoad);
  </script>--%>
                 <input name="middlename" id="middlenameHindi" type="text" class="form-control" placeholder="मध्य नाम" title="Middle Name"/>
                 <b><br /></b>   <b><br /></b>   <b><br /></b>   <b><br /></b>   <b><br /></b>   <b><br /></b>   <b><br /></b>                                                                      
    <table>
        <tr>
            <td> <asp:TextBox ID="txt_enterenykey" runat="server"> </asp:TextBox> PDF KEy</td>
                 <td> <asp:TextBox ID="txt_enterenykey2" runat="server"> </asp:TextBox>Table key</td>
                <td> <asp:Button ID="btn_decrypt" runat="server" OnClick="btn_decrypt_Click" Text="decrypt"> </asp:Button></td>
        </tr>
    </table>
        	</form>
</body>
</html>
