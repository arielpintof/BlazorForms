using BlazorForms.Data.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.EntityFrameworkCore;
using Directory = Lucene.Net.Store.Directory;
using Field = Lucene.Net.Documents.Field;

namespace BlazorForms.Data.Services;

public class LuceneIndexService
{
    private readonly string _indexPath = Path.Combine(Environment.CurrentDirectory, "templates");
    private readonly Directory _indexDirectory;
    private readonly ApplicationDbContext _context;
    private readonly Analyzer _analyzer;
    private readonly IndexWriter _writer;

    public LuceneIndexService(ApplicationDbContext context)
    {
        _context = context;
        _analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        var directory = FSDirectory.Open(_indexPath);
        var config = new IndexWriterConfig(LuceneVersion.LUCENE_48, _analyzer);
        _writer = new IndexWriter(directory, config);
        _indexDirectory = FSDirectory.Open(new DirectoryInfo(_indexPath));
    }

    public void AddOrUpdateTemplate(Guid templateId, string? name)
    {
        if (name is null) return;
        var doc = new Document
        {
            new StringField("id", templateId.ToString(), Field.Store.YES),
            new TextField("name", name, Field.Store.YES),
        };
        _writer.UpdateDocument(new Term("id", templateId.ToString()), doc);
        _writer.Commit();
    }

    public void DeleteTemplate(int templateId)
    {
        _writer.DeleteDocuments(new Term("id", templateId.ToString()));
        _writer.Flush(triggerMerge: false, applyAllDeletes: false);
    }
    
    public async Task InitializeIndexAsync()
    {
        var templates = await _context.Templates.ToListAsync();
        templates.ForEach(e => AddOrUpdateTemplate(e.Id, e.Name));
        _writer.Commit();
        
    }
    
    public IEnumerable<Template> SearchTemplates(string searchTerm)
    {
        using var reader = DirectoryReader.Open(_indexDirectory);
        var searcher = new IndexSearcher(reader);

        // Crea la consulta
        var parser = new QueryParser(LuceneVersion.LUCENE_48, "name", new StandardAnalyzer(LuceneVersion.LUCENE_48));
        var query = parser.Parse(searchTerm);

        // Ejecuta la búsqueda
        var hits = searcher.Search(query, 10).ScoreDocs;

        // Procesa los resultados
        var results = new List<Template>();
        foreach (var hit in hits)
        {
            var document = searcher.Doc(hit.Doc);
            var template = new Template
            {
                Id = Guid.Parse(document.Get("Id")),
                Name = document.Get("name"),
                // Otros campos según sea necesario...
            };
            results.Add(template);
        }
        return results;
    }
}