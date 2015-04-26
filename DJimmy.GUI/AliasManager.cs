using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DJimmy.Domain.Library;
using Raven.Client;

namespace DJimmy.GUI
{
    public partial class AliasManager : Form
    {
        private readonly Library library;
        private readonly IDocumentStore documentStore;

        private readonly DataTable titleAliasTable = new DataTable();
        private readonly DataTable artistAliasTable = new DataTable();

        public AliasManager(Library library, IDocumentStore documentStore)
        {
            this.library = library;
            this.documentStore = documentStore;

            InitializeComponent();

            BuildTableStructure(artistAliasTable);
            BuildTableStructure(titleAliasTable);
        }

        private void BuildTableStructure(DataTable table)
        {
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Was", typeof(string));
            table.Columns.Add("Is", typeof(string));            
        }

        private DataRow BuildRow(DataTable table, Alias alias)
        {
            var row = table.NewRow();
            row["Id"] = alias.Id;
            row["Was"] = alias.Was;
            row["Is"] = alias.Is;

            return row;
        }

        private void AliasManager_Load(object sender, EventArgs e)
        {
            using (var session = documentStore.OpenSession())
            {
                var aliases = session.Query<Alias>().ToList();

                foreach (var artistAlias in aliases.Where(a => a.Type == AliasType.Artist))
                {
                    artistAliasTable.Rows.Add(BuildRow(artistAliasTable, artistAlias));
                }

                dgvArtistAlias.DataSource = artistAliasTable;

                foreach (var titleAlias in aliases.Where(a => a.Type == AliasType.Title))
                {
                    titleAliasTable.Rows.Add(BuildRow(titleAliasTable, titleAlias));
                }

                dgvTitleAlias.DataSource = titleAliasTable;
            }
        }

        private Alias FromDataRow(DataRow row, AliasType type)
        {
            var alias = new Alias
            {
                Id = row["Id"] == DBNull.Value ? null : (string)row["Id"],
                Was = (string)row["Was"],
                Is = (string)row["Is"],
                Type = type
            };

            return alias;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var session = documentStore.OpenSession())
            {
                foreach (DataRow row in artistAliasTable.Rows)
                {
                    session.Store(FromDataRow(row, AliasType.Artist));
                }

                foreach (DataRow row in titleAliasTable.Rows)
                {
                    session.Store(FromDataRow(row, AliasType.Title));
                }

                session.SaveChanges();
            }
        }
    }
}
