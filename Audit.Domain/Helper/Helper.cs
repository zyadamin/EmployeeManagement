using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Domain.Helper
{
    public static class Helper
    {
        public static readonly IReadOnlyList<string> ActionTypes = new List<string> { "Add", "Update", "Delete" }.AsReadOnly();
    }
}
