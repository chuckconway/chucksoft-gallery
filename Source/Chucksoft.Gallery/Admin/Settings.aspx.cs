using System;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml;
using Chucksoft.Entities;
using Conway.Utilities;
using Chucksoft.Web.Pages;

namespace Chucksoft.Admin
{
    public partial class Settings : ManageBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                LoadSettings();
                
            }
        }

        /// <summary>
        /// Retrieve the current themes...
        /// </summary>
        private void BindData()
        {
            string path = Server.MapPath("~/Templates/");

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] directories = dirInfo.GetDirectories();

            themeDropdown.DataSource = directories;
            themeDropdown.DataTextField = "Name";
            themeDropdown.DataValueField = "Name";
            themeDropdown.DataBind();
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            GallerySettings settings = GallerySettings.Load();
            galleryTitle.Text = settings.GalleryTitle;

            //Set selected mode
            ListItem presentationMode = presentationModeRadioButtonList.Items.FindByText(settings.PresentationMode.ToString());
            presentationMode.Selected = true;

            //Set Storage Type
            ListItem dataStorage = storageSelection.Items.FindByValue(settings.DataStorage.ToString());
            dataStorage.Selected = true;

            ListItem saveItem = themeDropdown.Items.FindByValue(settings.Theme);
            saveItem.Selected = true;

            //set thumbnail height and Width
            thumbnailHeight.Text = settings.ThumbnailDimensions.Height.ToString();
            thumbnailWidth.Text = settings.ThumbnailDimensions.Width.ToString();

            //set fullsize height and Width
            fullsizeHeight.Text = settings.FullsizeDimensions.Height.ToString();
            fullsizeWidth.Text = settings.FullsizeDimensions.Width.ToString();
        }

        /// <summary>
        /// Handles the Click event of the saveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void saveButton_Click(object sender, EventArgs e)
        {
            GallerySettings settings = new GallerySettings();

            try
            {
                settings.GalleryTitle = galleryTitle.Text.Trim();
                
                //set thumbnail height and Width
                settings.ThumbnailDimensions.Height = Convert.ToInt32(thumbnailHeight.Text.Trim());
                settings.ThumbnailDimensions.Width = Convert.ToInt32(thumbnailWidth.Text.Trim());

                //Set method of storage
                settings.DataStorage = EnumParse<DataStorage>.Parse(storageSelection.SelectedValue);

                //Set presentation mode
                settings.PresentationMode = EnumParse<PresentationMode>.Parse(presentationModeRadioButtonList.SelectedItem.Text);

                //Set current theme
                settings.Theme = themeDropdown.SelectedValue;

                //set fullsize height and Width
                settings.FullsizeDimensions.Height = Convert.ToInt32(fullsizeHeight.Text.Trim());
                settings.FullsizeDimensions.Width = Convert.ToInt32(fullsizeWidth.Text.Trim());

                //Sets the crossdomain file to the correct hosts.
                SaveCrossDomainXml();

                settings.Save();
                message.Text = AdminResources.SettingsSaved;
            }
            catch (Exception ex)
            {
                message.Text = ex.Message;
            }
        }

        /// <summary>
        /// Saves the cross domain XML.
        /// </summary>
        private void SaveCrossDomainXml()
        {
            string path = Server.MapPath("~/crossdomain.xml");
            XmlDocument crossdomain = new XmlDocument();
            crossdomain.Load(path);

            XmlNode accessNode =  BuildAccessNode(crossdomain, Request.Url.Host);
            crossdomain.FirstChild.AppendChild(accessNode);

            //Add the www, the hostname was not already prepended with www.
            if(!Request.Url.Host.StartsWith("www", StringComparison.InvariantCultureIgnoreCase))
            {
                //Add Node with www / Check for preexisiting node.
                string domainValue = "www." + Request.Url.Host;
                AddNodeIfItDoesntExisit(crossdomain, domainValue);
            }
            else
            {
                //remove the prepended www
                string withoutWWW = Request.Url.Host.Replace("www.", "");
                AddNodeIfItDoesntExisit(crossdomain, withoutWWW);
            }

            //Remove wildcard from list
            RemoveWildcard(crossdomain);

            //Save new CrossDomain File
            crossdomain.Save(path);
        }

        /// <summary>
        /// Adds the node if it doesnt exisit.
        /// </summary>
        /// <param name="crossdomain">The crossdomain.</param>
        /// <param name="domainValue">The domain value.</param>
        private static void AddNodeIfItDoesntExisit(XmlDocument crossdomain, string domainValue)
        {
            XmlNode node = crossdomain.SelectSingleNode(@"/cross-domain-policy/allow-access-from[@domain='" + domainValue + "']");
                
            //If it's not found add the node
            if (node == null)
            {
                XmlNode accessNode = BuildAccessNode(crossdomain, domainValue);
                crossdomain.FirstChild.AppendChild(accessNode);
            }
        }

        /// <summary>
        /// Remove wildcard domain from crossdomain.xml
        /// </summary>
        /// <param name="crossdomain"></param>
        private static void RemoveWildcard(XmlNode crossdomain)
        {
            //Find element with wildcard domain.
            XmlNode node = crossdomain.SelectSingleNode(@"/cross-domain-policy/allow-access-from[@domain='*']");

            if(node != null)
            {
                crossdomain.FirstChild.RemoveChild(node);
            }
        }

        /// <summary>
        /// Builds the new nodes.
        /// </summary>
        /// <param name="crossdomain"> XmlDocument loaded with the crossdomain file</param>
        /// <param name="value">new domain value</param>
        /// <returns></returns>
        private static XmlNode BuildAccessNode(XmlDocument crossdomain, string value)
        {
            XmlNode accessNode = crossdomain.CreateNode( XmlNodeType.Element, "allow-access-from", null);
            XmlAttribute domainAttribute = crossdomain.CreateAttribute("domain");
            domainAttribute.Value = value;
            accessNode.Attributes.Append(domainAttribute);

            return accessNode;
        }
    }
}