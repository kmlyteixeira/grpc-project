using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    [HttpGet("large-text")]
    public ActionResult<string> GetLargeText()
    {
        var lorem = new Lorem();
        var loremIpsumDict = new Dictionary<string, string>();

        for (int i = 1; i <= 1000; i++)
        {
            loremIpsumDict.Add(i.ToString(), lorem.Paragraphs(1));
        }

        string jsonText = JsonSerializer.Serialize(loremIpsumDict);
        return jsonText;
    }
}
