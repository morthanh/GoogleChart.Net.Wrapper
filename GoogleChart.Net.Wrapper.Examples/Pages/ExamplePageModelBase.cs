using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleChart.Net.Wrapper.Examples.Pages
{
    public class ExamplePageModelBase : PageModel
    {

        public string DataJson { get; set; }

        public string OptionsJson { get; set; }
    }
}
