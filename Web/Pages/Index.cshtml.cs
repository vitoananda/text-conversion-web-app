using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Repositories;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConversionService _conversionService;

        [BindProperty]
        public string Text { get; set; } = string.Empty;

        public ConversionResponse? ConversionResult { get; set; }

        public IndexModel(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostConvert()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                ModelState.AddModelError(string.Empty, "Please enter some text");
                return Page();
            }

            ConversionResult = _conversionService.convertText(Text);

            return Page();
        }
    }
}
