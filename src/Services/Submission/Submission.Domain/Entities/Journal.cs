using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Submission.Domain.Entities;

public class Journal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abreviation { get; set; }

    private readonly List<Article> _articles = new();
    public IList<Article> Articles => _articles.AsReadOnly();
}
