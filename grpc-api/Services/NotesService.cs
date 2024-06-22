using System.Text;
using System.Text.Json;
using Bogus;
using Bogus.DataSets;
using Grpc.Core;
using NotesApp;

public class NotesService : NotesApp.NotesService.NotesServiceBase
{
    private static List<string> notes = new List<string>();

    public override Task<NotesList> GetAllNotes(Empty request, ServerCallContext context)
    {
        var response = new NotesList();
        response.Notes.AddRange(notes);
        return Task.FromResult(response);
    }

    public override Task<NoteResponse> GetNoteById(NoteRequest request, ServerCallContext context)
    {
        if (request.Id >= notes.Count || request.Id < 0)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Note not found"));
        }
        var response = new NoteResponse { Content = notes[request.Id] };
        return Task.FromResult(response);
    }

    public override Task<Empty> AddNote(Note request, ServerCallContext context)
    {
        notes.Add(request.Content);
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> UpdateNoteById(NoteUpdateRequest request, ServerCallContext context)
    {
        if (request.Id >= notes.Count || request.Id < 0)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Note not found"));
        }
        notes[request.Id] = request.Content;
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> DeleteNoteById(NoteRequest request, ServerCallContext context)
    {
        if (request.Id >= notes.Count || request.Id < 0)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Note not found"));
        }
        notes.RemoveAt(request.Id);
        return Task.FromResult(new Empty());
    }

    public override async Task<LargeTextResponse> GetLargeText(Empty request, ServerCallContext context)
    {
        var lorem = new Lorem();

        var loremIpsumDict = new Dictionary<string, string>();
        for (int i = 1; i <= 1000; i++) 
        {
            loremIpsumDict.Add(i.ToString(), lorem.Paragraphs(1));
        }

        string jsonText = JsonSerializer.Serialize(loremIpsumDict);
        var response = new LargeTextResponse { LargeText = jsonText };
        return response;
    }

}
