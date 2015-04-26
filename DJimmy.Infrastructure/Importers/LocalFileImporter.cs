using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DJimmy.Domain;
using DJimmy.Domain.LocalFiles;
using DJimmy.Infrastructure.Events;
using DJimmy.Infrastructure.Integrations;
using MoreLinq;
using Raven.Client;

namespace DJimmy.Infrastructure.Importers
{
    public class LocalFileImporter
    {
        private readonly IDocumentStore documentStore;
        private static readonly int Limit = 10000;
        private readonly IEnumerable<LocalFile> existingFiles;

        public LocalFileImporter(IEnumerable<LocalFile> existingFiles, IDocumentStore documentStore)
        {
            this.existingFiles = existingFiles;
            this.documentStore = documentStore;
        }

        public void Import(IEnumerable<LocalFile> newFiles)
        {
            using (var session = documentStore.OpenSession())
            {                                
                var now = DateTime.Now;

                newFiles.ExceptBy(existingFiles, s => s.Path)
                    .Select(s =>
                    {
                        var e = Mapper.Map<LocalFileEvent>(s);
                        e.Timestamp = now;
                        e.EventType = EventType.Added;
                        return e;
                    }).ForEach(e => session.Store(e));

                existingFiles.ExceptBy(newFiles, s => s.Path)
                    .Select(s =>
                    {
                        var e = new LocalFileEvent();
                        e.Path = s.Path;
                        e.Timestamp = now;
                        e.EventType = EventType.Removed;
                        return e;
                    }).ForEach(e => session.Store(e));

                session.SaveChanges();
            }
        }
    }
}