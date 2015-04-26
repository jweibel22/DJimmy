using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DJimmy.Infrastructure;
using DJimmy.Infrastructure.Integrations;
using log4net;

namespace DJimmy.GUI
{
    class SpotifyAuthorizationHttpServer
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HttpListener listener = new HttpListener();
        private Thread runThread;
        private readonly ISpotifyAuthorizationService spotifyAuthorizationService;

        public SpotifyAuthorizationHttpServer(ISpotifyAuthorizationService spotifyAuthorizationService)
        {
            this.spotifyAuthorizationService = spotifyAuthorizationService;
            listener.Prefixes.Add("http://localhost:8889/");
        }

        public void Start()
        {
            listener.Start();

            runThread = new Thread(Run);
            runThread.Start();
        }

        private void Run()
        {
            while (listener.IsListening)
            {
                try
                {
                    var context = listener.GetContext(); //Block until a connection comes in
                    //ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
                    HandleRequest(context);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }            
        }


        private void HandleRequest(HttpListenerContext context)
        {
            try
            {
                string code = context.Request.QueryString["code"];
                string error = context.Request.QueryString["error"];

                if (error != null)
                {
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        writer.Write("<html><body><h1>Authorization with Spotify failed. {0}</h1></body></html>", error);
                    }
                    context.Response.StatusCode = 401;
                    context.Response.OutputStream.Close();
                }
                else if (code == null)
                {
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        writer.Write("<html><body><h1>Authorization with Spotify failed. No authorization code was returned</h1></body></html>");
                    }
                    context.Response.StatusCode = 401;
                    context.Response.OutputStream.Close();
                }
                else
                {
                    try
                    {
                        spotifyAuthorizationService.Authorize(code);

                        using (var writer = new StreamWriter(context.Response.OutputStream))
                        {
                            writer.Write("<html><body><h1>Login to Spotify completed</h1></body></html>");
                        }
                        context.Response.StatusCode = 200;
                        context.Response.OutputStream.Close();
                    }
                    catch (Exception ex)
                    {
                        using (var writer = new StreamWriter(context.Response.OutputStream))
                        {
                            writer.Write("<html><body><h1>Authorization with Spotify failed. {0}</h1></body></html>", ex.Message);
                        }
                        context.Response.StatusCode = 500;
                        context.Response.OutputStream.Close();                        
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }            
        }

        public void Stop()
        {
            listener.Stop();
            runThread.Join(10000);
        }
    }
}
