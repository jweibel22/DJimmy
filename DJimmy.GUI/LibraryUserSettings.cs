using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace DJimmy.GUI
{
    public class LibraryUserSettings
    {
        private readonly IDocumentStore documentStore;

        public LibraryProperties LibraryProperties { get; set; }

        public event EventHandler Changed;

        private static LibraryUserSettings instance;

        protected LibraryUserSettings(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public static LibraryUserSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Library properties not initialized");
                }
                return instance;
            }
        }

        public static void Initialize(IDocumentStore documentStore)
        {
            instance = new LibraryUserSettings(documentStore);
            instance.Load();
        }

        public void Load()
        {
            using (var session = documentStore.OpenSession())
            {
                LibraryProperties = session.Query<LibraryProperties>().SingleOrDefault();

                if (LibraryProperties == null)
                {
                    LibraryProperties = new LibraryProperties();
                }
            }
        }

        public void Save()
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(LibraryProperties);
                session.SaveChanges();
            }

            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }
    }
}
