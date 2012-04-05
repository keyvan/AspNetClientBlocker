using System;
using System.Web;

namespace AspNetClientBlocker
{
    public class Blocker : IHttpModule
    {
        /// <summary>
        /// Public constructor.
        /// </summary>
        public Blocker()
        {
        }

        /// <summary>
        /// Initialization.
        /// </summary>
        /// <param name="context">HttpApplication instance.</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        /// <summary>
        /// Disposing.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Event handler for BeginRequest.
        /// </summary>
        /// <param name="sender">Sender object instance.</param>
        /// <param name="e">Event arguments.</param>
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;

            bool block = false;

            try
            {
                string clientIP = application.Request.UserHostAddress;

                CheckIPs objChecker = new CheckIPs();

                if (objChecker.CheckIP(clientIP) == false)
                {
                    // If client is blocked
                    block = true;
                }
            }
            catch
            {
                // On any unexpected error we let visitor to visit website
            }

            if (block)
            {
                // Blocking process
                application.Response.StatusCode = 200;
                application.Response.SuppressContent = true;
                application.Response.End();
            }
        }
    }
}