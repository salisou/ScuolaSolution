using Scuola.Ui.Dtos;

namespace Scuola.Ui.Services
{
    public class StudenteService
    {
        private readonly HttpClient _http;

        public StudenteService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Scuola.api");
        }

        public async Task<List<StudentiDto>> GetStudentiAsync() =>
            await _http.GetFromJsonAsync<List<StudentiDto>>("studenti") ?? new();

        public async Task AddStudenteAsync(StudentiDto studente) =>
            await _http.PostAsJsonAsync("studenti", studente);

        public async Task UpdateStudenteAsync(StudentiDto studente) =>
            await _http.PutAsJsonAsync($"studenti/{studente.Id}", studente);

        public async Task DeleteStudenteAsync(int id) =>
            await _http.DeleteAsync($"studenti/{id}");
    }
}
