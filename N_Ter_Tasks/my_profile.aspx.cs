using System;
using System.Web.UI;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter.Base;

namespace N_Ter_Tasks
{
    public partial class my_profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                SessionObject objSes = (SessionObject)Session["dt"];
                LoadPasswordPolicy(objSes);
                LoadTempUsers(objSes);
                LoadProfile(objSes);
            }

            cmdYes.OnClientClick = "javascript:return ValidateFileUpload('" + fulLogo.ClientID + "');";
            cmdEditPin.Attributes.Add("onClick", "LoadPIN();");
            cmdEditToken.Attributes.Add("onClick", "LoadToken();");
            Btnsubmit.OnClientClick = "return ValidateChangePW('" + txtcurrntpasswrd.ClientID + "', '" + txtnewPassword.ClientID + "', '" + txtRePassword.ClientID + "');";
        }

        private void LoadPasswordPolicy(SessionObject objSes)
        {
            N_Ter.Common.Common_Task_Actions objCommAct = new N_Ter.Common.Common_Task_Actions();
            divPasswordPolicy.InnerHtml = objCommAct.ReadPasswordPolicy(ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type));
        }

        private void LoadTempUsers(SessionObject objSes)
        {
            Users objUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUsers.ReadAll();
            cboTempUser.DataSource = ds.tblusers;
            cboTempUser.DataTextField = "Full_Name";
            cboTempUser.DataValueField = "User_ID";
            cboTempUser.DataBind();
            cboTempUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("[Not Assigned]", "0"));
        }

        private void LoadProfile(SessionObject objSes)
        {
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUser.Read(objSes.UserID);
            DS_Users.tblusersRow dr = ds.tblusers[0];
            lblUsername.Text = dr.Username;
            lblCode.Text = dr.User_Code;
            txtFirstName.Text = dr.First_Name;
            txtLastName.Text = dr.Last_Name;
            txtPhone.Text = dr.Phone;
            cboDefaultTaskList.SelectedValue = Convert.ToString(dr.Default_Task_List);
            cboTaskListView.SelectedValue = Convert.ToString(dr.Task_List_View);
            cboTLTaskListView.SelectedValue = Convert.ToString(dr.TL_Task_List_View);
            cboTaskScreen.SelectedValue = Convert.ToString(dr.Task_Screen_Type);
            cboPageHelp.SelectedValue = Convert.ToString(dr.Show_Help);
            try
            {
                cboTempUser.SelectedValue = dr.Temp_User.ToString();
            }
            catch (Exception)
            {
                cboTempUser.SelectedValue = "0";
            }
            if (dr.IsImage_PathNull() || dr.Image_Path.Trim() == "")
            {
                LoadImage("assets/images/user.png");
            }
            else
            {
                LoadImage(dr.Image_Path);
            }
            if (dr.IsSignature_PathNull() || dr.Signature_Path.Trim() == "")
            {
                LoadSignature("assets/images/signature.png");
            }
            else
            {
                LoadSignature(dr.Signature_Path);
            }

            DS_Users dsAltUsers = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type).ReadAssignedUsers(objSes.UserID);
            if (dsAltUsers.tblusers.Count > 0)
            {
                ltrAssignedFrom.Text = "";
                foreach (DS_Users.tblusersRow row in dsAltUsers.tblusers)
                {
                    ltrAssignedFrom.Text = ltrAssignedFrom.Text + row.First_Name + " " + row.Last_Name + "<br />";
                }
                ltrAssignedFrom.Text = ltrAssignedFrom.Text.Substring(0, ltrAssignedFrom.Text.Length - 6);
            }
            else
            {
                ltrAssignedFrom.Text = "[None]";
            }
        }

        private void LoadImage(string ImagePath)
        {
            imgProfileImage.ImageUrl = ImagePath;
        }

        private void LoadSignature(string ImagePath)
        {
            imgProfileSignature.ImageUrl = ImagePath;
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            LoadProfile(objSes);
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            string ErrorText = "";
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            if (txtFirstName.Text.Trim() == "")
            {
                ErrorText = ErrorText + "* First Name is Required <br>";
            }
            if (ErrorText.Trim() == "")
            {
                string ProfileImage = "";
                if (imgProfileImage.ImageUrl != "assets/images/user.png")
                {
                    ProfileImage = imgProfileImage.ImageUrl;
                }
                string ProfileSignature = "";
                if (imgProfileSignature.ImageUrl != "assets/images/signature.png")
                {
                    ProfileSignature = imgProfileSignature.ImageUrl;
                }
                if (objUser.Update(objSes.UserID, lblUsername.Text, txtFirstName.Text, txtLastName.Text, txtPhone.Text, ProfileImage, ProfileSignature, Convert.ToInt32(cboTempUser.SelectedItem.Value), Convert.ToInt32(cboDefaultTaskList.SelectedItem.Value), Convert.ToInt32(cboTaskListView.SelectedItem.Value), Convert.ToInt32(cboTLTaskListView.SelectedItem.Value), Convert.ToInt32(cboPageHelp.SelectedItem.Value), Convert.ToInt32(cboTaskScreen.SelectedItem.Value)) == true)
                {
                    DS_Settings dsSett = ObjectCreator.GetSettings(objSes.Connection, objSes.DB_Type).Read();
                    objSes.Task_List = dsSett.tblsettings[0].Default_Task_List;
                    objSes.Task_List_View = dsSett.tblsettings[0].Task_List_View;
                    objSes.TL_Task_List_View = dsSett.tblsettings[0].TL_Task_List_View;
                    objSes.Show_Help = dsSett.tblsettings[0].Show_Help;
                    objSes.Task_Screen_Type = dsSett.tblsettings[0].Task_Screen_Type;

                    new N_Ter.Common.Common_Actions().AdjustUserSessionParams(Convert.ToInt32(cboDefaultTaskList.SelectedItem.Value), Convert.ToInt32(cboTaskListView.SelectedItem.Value), Convert.ToInt32(cboTLTaskListView.SelectedItem.Value), Convert.ToInt32(cboPageHelp.SelectedItem.Value), Convert.ToInt32(cboTaskScreen.SelectedItem.Value), ref objSes);

                    Session["dt"] = objSes;

                    n_ter_base_loggedin pg = (n_ter_base_loggedin)this.Master;
                    pg.ShowHelp = objSes.Show_Help;

                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User successfully Saved');", true);
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('User could not be saved');", true);
                }
            }

            if (ErrorText.Trim() != "")
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('" + ErrorText.Substring(0, ErrorText.Length - 4) + "');", true);
            }
        }

        protected void cmdYes_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            string fn = System.IO.Path.GetFileName(fulLogo.PostedFile.FileName);
            string[] fnSP = fn.Split('.');

            string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\profile_images";
            SaveLocation = SaveLocation + "\\profile_" + objSes.UserID;

            if (System.IO.Directory.Exists(SaveLocation) == false)
            {
                System.IO.Directory.CreateDirectory(SaveLocation);
            }

            string NewFileName = "cmp" + N_Ter.Common.Common_Actions.TimeStampForFileName();
            SaveLocation = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            string SaveLocationDB = "nter_app_uploads\\profile_images\\profile_" + objSes.UserID + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            try
            {
                fulLogo.PostedFile.SaveAs(SaveLocation);
                N_Ter.Common.Create_Images objImage = new N_Ter.Common.Create_Images();
                System.Drawing.Bitmap bmp = objImage.CreateThumbnail(SaveLocation, 400, 300);
                if ((bmp != null))
                {
                    System.IO.File.Delete(SaveLocation);
                    SaveLocation = SaveLocation.Substring(0, SaveLocation.LastIndexOf(".")) + ".jpg";
                    SaveLocationDB = SaveLocationDB.Substring(0, SaveLocationDB.LastIndexOf(".")) + ".jpg";
                    bmp.Save(SaveLocation, System.Drawing.Imaging.ImageFormat.Jpeg);
                    LoadImage(SaveLocationDB);
                }
            }
            catch (Exception)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Image could not be uploaded');", true);
            }
        }

        protected void cmdYesS_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            string fn = System.IO.Path.GetFileName(fulSignature.PostedFile.FileName);
            string[] fnSP = fn.Split('.');

            string SaveLocation = objSes.PhysicalRoot + "\\nter_app_uploads\\profile_images";
            SaveLocation = SaveLocation + "\\profile_" + objSes.UserID;

            if (System.IO.Directory.Exists(SaveLocation) == false)
            {
                System.IO.Directory.CreateDirectory(SaveLocation);
            }

            string NewFileName = "cmp" + N_Ter.Common.Common_Actions.TimeStampForFileName();
            SaveLocation = SaveLocation + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            string SaveLocationDB = "nter_app_uploads\\profile_images\\profile_" + objSes.UserID + "\\" + NewFileName + "." + fnSP[fnSP.Length - 1];
            try
            {
                fulSignature.PostedFile.SaveAs(SaveLocation);
                N_Ter.Common.Create_Images objImage = new N_Ter.Common.Create_Images();
                System.Drawing.Bitmap bmp = objImage.CreateThumbnail(SaveLocation, 400, 300);
                if ((bmp != null))
                {
                    System.IO.File.Delete(SaveLocation);
                    SaveLocation = SaveLocation.Substring(0, SaveLocation.LastIndexOf(".")) + ".jpg";
                    SaveLocationDB = SaveLocationDB.Substring(0, SaveLocationDB.LastIndexOf(".")) + ".jpg";
                    bmp.Save(SaveLocation, System.Drawing.Imaging.ImageFormat.Jpeg);
                    LoadSignature(SaveLocationDB);
                }
            }
            catch (Exception)
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Image could not be uploaded');", true);
            }
        }

        protected void Btnsubmit_Click(object sender, EventArgs e)
        {
            SessionObject objSes = (SessionObject)Session["dt"];
            Users objUser = ObjectCreator.GetUsers(objSes.Connection, objSes.DB_Type);
            DS_Users ds = objUser.Read(objSes.UserID);
            DS_Users.tblusersRow dr = ds.tblusers[0];

            if (txtcurrntpasswrd.Text.Trim() == "")
            {
                ClientScriptManager cs = Page.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Current Password is required');", true);
            }
            else
            {
                if (objUser.ValidateUser(dr.Username.ToString().Trim(), txtcurrntpasswrd.Text.Trim()))
                {
                    if (txtRePassword.Text.Trim() == "")
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError(' Retype Password is required');", true);
                    }
                    if (!txtnewPassword.Text.Trim().Equals(txtRePassword.Text.Trim()))
                    {
                        ClientScriptManager cs = Page.ClientScript;
                        cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Password doesn't match');", true);
                    }
                    else
                    {
                        if (!objUser.ChangePassword(objSes.UserID, txtnewPassword.Text))
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('User Password could not be saved');", true);
                        }
                        else
                        {
                            ClientScriptManager cs = Page.ClientScript;
                            cs.RegisterStartupScript(this.GetType(), "AKey", "ShowSuccess('User Password successfully Saved');", true);
                        }
                    }
                }
                else
                {
                    ClientScriptManager cs = Page.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Current Password is invalid');", true);
                }
            }
        }
    }
}