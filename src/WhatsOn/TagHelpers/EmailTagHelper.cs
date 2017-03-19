using Microsoft.AspNetCore.Razor.TagHelpers;

//TagHelper inherits from the base TagHelper class
//When you create an EmailTagHelper you can use the TagHelper by just writing Email in the razor code
namespace WhatsOn.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        //Add Properties for email. Addressyou want to sen the email to and the content you want the link you display
        public string Address { get; set; }
        public string Content { get; set; }

        //Override the process
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Generate an AnchorTag 'a' so the output of the TagHelper should be an AnchorTag
            output.TagName = "a";
            //Add an attribute to the tag. Set the 'href' attribute to 'mailto' and the value of the address property that will be passed in
            output.Attributes.SetAttribute("href", "mailto:" + Address);
            //Set the tags content to the content property that you pass a value to
            output.Content.SetContent(Content);
        }
    }
}
