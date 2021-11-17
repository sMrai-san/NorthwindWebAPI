using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public partial class Documentation
    {
        internal string ErrorOccurred;

        public int DocumentationId { get; set; }
        public string AvailableRoute { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public string Keycode { get; set; }
    }
}
