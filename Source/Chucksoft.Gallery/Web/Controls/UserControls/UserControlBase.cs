using System.Threading;
using System.Web.UI;
using Chucksoft.Entities;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Chucksoft.Web.Controls.UserControls
{
    public class UserControlBase : UserControl
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public User CurrentUser
        {
            get
            {
                return ((User) Thread.CurrentPrincipal);
            }
        }

        /// <summary>
        /// Retrieves the checked rows from gridview.
        /// </summary>
        /// <param name="checkboxId">The checkbox id.</param>
        /// <param name="grid">The grid.</param>
        /// <returns></returns>
        public List<GridViewRow> RetrieveCheckedRowsFromGridview(string checkboxId, GridView grid)
        {
            List<GridViewRow> checkedRows = new List<GridViewRow>();

            foreach (GridViewRow row in grid.Rows)
            {
                CheckBox checkBox = (CheckBox)row.FindControl(checkboxId);

                if (checkBox.Checked)
                {
                    checkedRows.Add(row);
                }
            }

            return checkedRows;
        }
    }
}