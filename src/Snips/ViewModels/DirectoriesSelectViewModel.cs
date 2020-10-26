using System;
using System.Collections.Generic;

namespace Snips.ViewModels
{
    public class DirectoriesSelectViewModel
    {
        public IList<TreeviewItem> Tree { get; set; }
        public string SelectId { get; set; }
        public Guid? SelectedValueId { get; set; }
    }
}