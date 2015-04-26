using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DJimmy.Domain.LocalFiles;
using DJimmy.Domain.Spotify;
using DJimmy.Infrastructure;
using log4net.Config;

namespace DJimmy.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            AutoMapper.Mapper
                .CreateMap<SpotifySongEvent, SpotifySong>();

            AutoMapper.Mapper
                .CreateMap<SpotifySong, SpotifySongEvent>()
                .ForMember(x => x.Id, o => o.Ignore());

            AutoMapper.Mapper
                .CreateMap<LocalFileEvent, Domain.LocalFiles.LocalFile>();

            AutoMapper.Mapper
                .CreateMap<Domain.LocalFiles.LocalFile, LocalFileEvent>()
                .ForMember(x => x.Id, o => o.Ignore());

            XmlConfigurator.Configure();
            Log4NetConfiguration.ConfigureFileAppender(@"c:\dc\logs\DJimmy.txt");

            Application.ThreadException += (sender, args) =>
            {
                if (args.Exception is UnauthorizedAccessException)
                {
                    MessageBox.Show(args.Exception.Message, "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                }
                else
                {
                    MessageBox.Show(args.Exception.Message, "Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                }
            };

            AppDomain.CurrentDomain.UnhandledException +=            
                (sender, args) => MessageBox.Show(args.ExceptionObject.ToString(), "Unhandled exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
