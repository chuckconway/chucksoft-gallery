using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Chucksoft.Silverlight.Controls.UI.Browser
{
    public class BrowserCookie
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value pairs.
        /// </summary>
        /// <value>The value pairs.</value>
        public List<KeyValuePair<string,string>> ValuePairs { get; set; }

        /// <summary>
        /// Gets the value by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string GetValueByKey(string key)
        {
            string value = string.Empty;

            for (int index = 0; index < ValuePairs.Count; index++)
            {
                if(ValuePairs[index].Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    value = ValuePairs[index].Value;
                    break;
                }
            }

            return value;
        }
    }
}
