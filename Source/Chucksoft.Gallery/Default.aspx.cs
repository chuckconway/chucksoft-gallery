using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using Chucksoft.Entities;
using Chucksoft.Web.Presentation;
using System.Collections;
using Conway.Reflection;
using Conway.Threading;

namespace Chucksoft
{
    public partial class Default : Page
    {
        //Cacheing container.
        private static readonly Hashtable view = new Hashtable();

        //locking object
        private static readonly object _lock = new object();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Retreieve settings and set the page title
            GallerySettings settings = GallerySettings.Load();
            Title = settings.GalleryTitle;
            //Get the controllers
            
            ControllerBase controller;

            //Check for cached type
            if (view.ContainsKey(settings.PresentationMode))
            {
               controller = ReflectionHelper.CreateInstance<ControllerBase>((Type)view[settings.PresentationMode]);
            }
            else
            {
                //Find the type and save it in the cache hashtable
                controller = FindController(settings);
                LockingHelper.SaveTypeInHashTable(controller.GetType(), settings.PresentationMode, view, _lock);
            }

            Control control = controller.GenerateView();

            //Add the control to the PlaceHolder
            photoView.Controls.Add(control);
        }

        /// <summary>
        /// Finds the controller.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        private static ControllerBase FindController(GallerySettings settings)
        {
            List<ControllerBase> controllers = GetControllers();
            ControllerBase controllerBase = null;

            //Find which Controller to execute
            foreach (ControllerBase controller in controllers)
            {
                if (controller.View == settings.PresentationMode)
                {
                    //retreive the contorl
                    controllerBase = controller;                    
                    break;
                }
            }

            return controllerBase;
        }

        /// <summary>
        /// Gets the controllers.
        /// </summary>
        /// <returns></returns>
        private static List<ControllerBase> GetControllers()
        {
            //Get the executing assembly and declare controllerType collection
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<ControllerBase> controllerTypes = new List<ControllerBase>();

            Type[] types = assembly.GetTypes();

            //Find the types that implement ControllerBase
            foreach (Type type in types)
            {
                if(type.IsSubclassOf(typeof(ControllerBase)))
                {
                    ControllerBase baseController = ReflectionHelper.CreateInstance<ControllerBase>(type);
                    controllerTypes.Add(baseController);
                }
            }

            return controllerTypes;
        }
    }
}
