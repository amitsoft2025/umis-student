using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.toml.dp.util;

namespace DirecPay
{
    public partial class _Default11 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }        

        public void encriptData() 
        {
            string MID = merchantId.Text;
            string Collaborator_Id = collaboratorId.Text;
            string Operating_Mode = operatingMode.Text;
            string Country = CountryCode.Text;
            string Currency = currency_Code.Text;
            string Amount = amt.Text;
            string Order_Number = orderNumber.Text;
            string Other_Details = otherDetails.Text;
            string Success_URL = successUrl.Text;
            string Failure_URL = failureUrl.Text;

            DateTime now = DateTime.Now;

            Order_Number = now.ToString("MdMdHHmmyyMyy");

            orderNumber.Text = Order_Number;

            string EncodedKey = "Zsg/zaGnfdaarsnvO3sF3g==";

            int keysize = 128;
            
            string Requestparameter = MID + "|" + Operating_Mode + "|" + Country + "|" + Currency + "|" + Amount + "|" + Other_Details + "|" + Success_URL + "|" + Failure_URL + "|" + Collaborator_Id + "|" + Order_Number + "|" + txtMerCustomerId.Text + "|" + txtpayMode.Text + "|" + txtAccessmedium.Text + "|" + txtTransSource.Text;

            string EncryptedParam = AES128Bit.Encrypt(Requestparameter, EncodedKey, keysize);

            requestparams.Value = EncryptedParam;            
            
            string reqBill = txtbillingUserName.Text + "|" + txtbillingCity.Text + "|" + txtbillingState.Text + "|" + txtBillingPinCode.Text + "|" + txtBillingCountry.Text + "|" + txtBillingCountryCode.Text + "|" + txtBillingAreaCode.Text + "|" + txtBillingPhoneNO.Text + "|" + txtBillingMobileNO.Text + "|" + txtBillingEmailID.Text + "|" + "N";
            billingDtls.Value = AES128Bit.Encrypt(reqBill, EncodedKey, keysize);
                       
            string reqShipping = txtShippingUserName.Text + "|" + txtShippingAddress.Text + "|" + txtShippingCity.Text + "|" + txtShippingState.Text + "|" + txtShippingCountry.Text + "|" + txtShippingPinCode.Text + "|" + txtShippingCountryCode.Text + "|" + txtShippingAreaCode.Text + "|" + txtShippingPhoneNo.Text + "|" + txtShippingMobileNo.Text + "|" + "N";
            shippingDtls.Value = AES128Bit.Encrypt(reqShipping, EncodedKey, keysize);                      

        }
        
        protected void chkConfirm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirm.Checked) 
            {
                encriptData();
            }
        }
        
    }
}
