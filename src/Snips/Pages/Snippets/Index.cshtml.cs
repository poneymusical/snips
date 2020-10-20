using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snips.Domain.BusinessObjects;
using Snips.Domain.Enums;
using Snips.ViewModels;

namespace Snips.Pages.Snippets
{
    public class Index : PageModel
    {
        public Snippet Snippet { get; set; }

        public List<TreeViewNode> Tree { get; set; }

        public void OnGet()
        {
            /**
             * dir1
             *     snippet11
             *     dir11
             *         snippet111
             *         snippet112
             *     dir12
             *         snippet121
             * dir2
             *     snippet21
             *     snippet22
             */

            var dir1 = new TreeViewNode(Guid.NewGuid(), "Directory 1", NodeType.Directory);
            dir1.AddChild(new TreeViewNode(Guid.NewGuid(), "Snippet 1_1", NodeType.File));

            var dir11 = new TreeViewNode(Guid.NewGuid(), "Directory 1_1", NodeType.Directory);
            dir1.AddChild(dir11);
            dir11.AddChild(new TreeViewNode(Guid.NewGuid(), "Snippet 1_1_1", NodeType.File));
            dir11.AddChild(new TreeViewNode(Guid.NewGuid(), "Snippet 1_1_2", NodeType.File));

            var dir12 = new TreeViewNode(Guid.NewGuid(), "Directory 1_2", NodeType.Directory);
            dir1.AddChild(dir12);
            dir12.AddChild(new TreeViewNode(Guid.NewGuid(), "Snippet 1_2_1", NodeType.File));

            var dir2 = new TreeViewNode(Guid.NewGuid(), "Directory 2", NodeType.Directory);
            // dir2.AddChild(new TreeViewNode(Guid.NewGuid(), "Snippet 2_1", NodeType.File));
            // dir2.AddChild(new TreeViewNode(Guid.NewGuid(), "Snippet 2_2", NodeType.File));

            for (var i = 0; i < 100; i++)
                dir2.AddChild(new TreeViewNode(Guid.NewGuid(), $"Snippet 2_{i + 1}", NodeType.File));

            Tree = new List<TreeViewNode> { dir1, dir2 };

            Snippet = new Snippet
            {
                Id = Guid.NewGuid(),
                Content = string.Join('\n',
                    "## Titre de section",
                    "Contenu du snippet",
                    "* Liste",
                    "* à",
                    "* puces"
                ),
                Title = "Le titre du snippet"
            };
        }

        public JsonResult OnGetSnippetData([FromQuery] Guid snippetId)
        {
            return new JsonResult(new Snippet
            {
                Id = snippetId,
                Content = string.Join('\n',
                    "## Titre de section",
                    $"Contenu du snippet #{snippetId}"
                ),
                Title = $"Le titre du snippet #{snippetId}, titre super long qui remplit bien plus qu'une ligne de texte voilà voilà"
            });
        }
    }
}