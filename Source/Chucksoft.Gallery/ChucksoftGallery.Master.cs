using System;
using System.Web.UI;
using Chucksoft.Entities;

namespace Chucksoft
{
    public partial class ChucksoftGallery : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GallerySettings settings = GallerySettings.Load();

            string stylePath = "templates/" + settings.Theme + "/style.css";
            themeStyleSheetLiteral.Text = "<style type=\"text/css\" media=\"screen\">@import url(\""+ stylePath + "\");</style>";
        }
    }
}
