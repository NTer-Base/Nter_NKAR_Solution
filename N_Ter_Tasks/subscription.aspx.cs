using N_Ter.Common;
using N_Ter.Licence;
using System;
using System.Web.UI;

namespace N_Ter_Tasks
{
    public partial class subscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            n_ter_base pg = (n_ter_base)this.Master;
            pg.PageClass = "page-signin";

            ValidateSubscription();
        }

        private void ValidateSubscription()
        {
            string strBasePath = Server.MapPath("~/assets");
            strBasePath = strBasePath.Substring(0, strBasePath.Length - 7);

            string SubscriptionKey = "";
            if (N_Ter.Licence.Validate_N_Ter.IsValidLicenceKey(strBasePath, ref SubscriptionKey) == false)
            {
                Response.Redirect("invalid_license.aspx?");
            }
            else
            {
                if (SubscriptionKey.Trim() != "")
                {
                    strBasePath = Server.MapPath("~/nter_app_uploads");
                    DateTime dtExpDate = DateTime.Today;
                    if (N_Ter.Licence.Validate_N_Ter.IsValidSubscriptionKey(strBasePath, SubscriptionKey, ref dtExpDate) == false)
                    {
                        ltrExpDate.Text = string.Format("{0:" + Constants.DateFormat + "}", dtExpDate.Date);
                        divExpired.Visible = true;
                        divNotExpired.Visible = false;
                        divButtons.Attributes["style"] = "margin-top: 17px";
                        cmdLogin.Visible = false;
                    }
                    else
                    {
                        ltrExpDate2.Text = string.Format("{0:" + Constants.DateFormat + "}", dtExpDate.Date);
                        divExpired.Visible = false;
                        divNotExpired.Visible = true;
                        divButtons.Attributes["style"] = "margin-top: 34px";
                        cmdLogin.Visible = true;
                    }
                }
                else
                {
                    Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
                }
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            if (txtSubscriptionCode.Text.Trim() == "")
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Subscription Code is Required');", true);
            }
            else
            {
                N_Ter.Licence.Generate_Keys objKeys = new N_Ter.Licence.Generate_Keys();
                string SubscriptionKey = "";
                DateTime dtExpDate = new DateTime();
                if (objKeys.ExtractSubscriptionCode(txtSubscriptionCode.Text.Trim(), ref SubscriptionKey, ref dtExpDate))
                {
                    string strBasePath = Server.MapPath("~/assets");
                    strBasePath = strBasePath.Substring(0, strBasePath.Length - 7);

                    string RootSubscriptionKey = "";
                    if (Validate_N_Ter.IsValidLicenceKey(strBasePath, ref RootSubscriptionKey) == false)
                    {
                        Response.Redirect("invalid_license.aspx?");
                    }
                    else
                    {
                        if (RootSubscriptionKey.Trim() != "")
                        {
                            int intSubscriptionKey = 0;
                            if (RootSubscriptionKey.Trim() == SubscriptionKey && int.TryParse(SubscriptionKey, out intSubscriptionKey))
                            {
                                if (dtExpDate.Date < DateTime.Today.Date)
                                {
                                    ClientScriptManager cs = Page.ClientScript;
                                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Date Associated to the Subscription Code has already expired');", true);
                                }
                                else
                                {
                                    strBasePath = Server.MapPath("~/nter_app_uploads");
                                    SubscriptionKey = new Generate_Keys().GenerateSubscriptionKeySubFile(intSubscriptionKey);
                                    string DateCode = new Date_Codes().GetDateInt(dtExpDate.Date).ToString();

                                    System.IO.StreamWriter sw = System.IO.File.CreateText(strBasePath + "\\Subscription.mns");
                                    N_Ter.Security.N_Ter_Crypto objCrypto = new N_Ter.Security.N_Ter_Crypto();
                                    sw.Write(objCrypto.psEncrypt(SubscriptionKey + "|" + DateCode));
                                    sw.Close();
                                    ValidateSubscription();
                                    ClientScriptManager cs = Page.ClientScript;
                                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('Subscription Code Successfully Applied');", true);
                                }
                            }
                            else
                            {
                                ClientScriptManager cs = Page.ClientScript;
                                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Subscription Code');", true);
                            }
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Application does not need a Subscription');", true);
                        }
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid Subscription Code');", true);
                }
            }
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath));
        }
    }
}