using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Timvw.Framework.ComponentModel;
using DJimmy.Domain.LastFM;
using DJimmy.Domain.Library;
using DJimmy.Infrastructure.Indexes;
using Lucene.Net.QueryParsers;
using Raven.Client;
using Raven.Client.Linq;

namespace DJimmy.GUI
{
    public partial class Playbacks : Form
    {
        private class Query
        {
            private readonly IDocumentStore documentStore;

            private const int PageSize = 100;
            private readonly RavenQueryStatistics stats;
            private int currentPage;
            private readonly string titleTerm;
            private readonly string artistTerm;

            private IList<Playback> playbacks;

            public IEnumerable<Playback> Results
            {
                get { return playbacks; }
            }

            public Query(IDocumentStore documentStore, string title, string artist)
            {
                this.documentStore = documentStore;
                this.titleTerm = string.Format("*{0}*", QueryParser.Escape(title)).Replace(" ", "\\ ");
                this.artistTerm = string.Format("*{0}*", QueryParser.Escape(artist)).Replace(" ", "\\ ");

                using (var session = documentStore.OpenSession())
                {
                    playbacks = session.Advanced.LuceneQuery<Playback, PlaybacksIndex>()
                        .Statistics(out stats)
                        .Where("Title:" + titleTerm + " AND Artist:" + artistTerm)
                        .Take(PageSize)
                        .OrderBy(x => x.Timestamp)
                        .ToList();
                }   
            }

            public int TotalResults
            {
                get { return stats.TotalResults - stats.SkippedResults; }
            }

            public int PageStart
            {
                get { return currentPage*PageSize + 1; }
            }

            public int PageEnd
            {
                get { return Math.Min(TotalResults, (currentPage + 1)*PageSize); }
            }

            public void Next()
            {
                if (currentPage < 0)
                    return;

                if ((currentPage + 1) * PageSize > stats.TotalResults - stats.SkippedResults)
                    return;

                using (var session = documentStore.OpenSession())
                {
                    playbacks = session.Advanced.LuceneQuery<Playback, PlaybacksIndex>()
                        .Where("Title:" + titleTerm + " AND Artist:" + artistTerm)
                        .Skip((currentPage + 1) * PageSize + stats.SkippedResults)
                        .Take(PageSize)
                        .OrderBy(x => x.Timestamp)
                        .ToList();
                }

                currentPage++;                
            }

            public void Previous()
            {
                if (currentPage <= 0)
                    return;

                using (var session = documentStore.OpenSession())
                {
                    playbacks = session.Advanced.LuceneQuery<Playback, PlaybacksIndex>()
                        .Where("Title:" + titleTerm + " AND Artist:" + artistTerm)
                        .Skip((currentPage - 1) * PageSize + stats.SkippedResults)
                        .Take(PageSize)
                        .OrderBy(x => x.Timestamp)
                        .ToList();                    

                }

                currentPage--;
            }

        }


        private readonly IDocumentStore documentStore;
        private Query query;


        public Playbacks(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            query = new Query(documentStore, tbTitle.Text, tbArtist.Text);

            dgvPlaybacks.DataSource = new SortableBindingList<Playback>(query.Results);
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            lblPage.Text = String.Format("Showing {0}-{1} of {2} results", query.PageStart, query.PageEnd, query.TotalResults);
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            query.Next();
            dgvPlaybacks.DataSource = new SortableBindingList<Playback>(query.Results);
            RefreshStatus();
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            query.Previous();
            dgvPlaybacks.DataSource = new SortableBindingList<Playback>(query.Results);
            RefreshStatus();
        }
    }
}
