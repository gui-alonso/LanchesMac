using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LanchesMac.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string Email { get; set; }
        public string Conteudo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + Email);
            output.Content.SetContent(Conteudo);
            //output.Content.SetContent(Subject); opção alternativa para inglês
        }
    }

}
