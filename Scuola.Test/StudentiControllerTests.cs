using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scuola.api.Controllers;
using Scuola.api.Data;
using Scuola.api.Models;

namespace Scuola.Test
{
    public class StudentiControllerTests
    {
        /// <summary>
        /// Crea un database InMemory per testare StudentiController.
        /// Utile per isolare i test e garantire che non dipendano da un database reale.
        /// </summary>
        public ScuolaDbContext GetInMeriyDbContetxt()
        {
            // Opzioni per usare un database in memoria con nome univoco (evita conflitti tra test)
            DbContextOptions<ScuolaDbContext> options = new DbContextOptionsBuilder<ScuolaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nome univoco
                .Options;

            // Crea il contesto con le opzioni specificate
            ScuolaDbContext context = new ScuolaDbContext(options);

            // Inserisce uno studente di test
            Studenti studente = new Studenti
            {
                Id = 1,
                Nome = "Mario",
                Cognome = "Rossi",
                Email = "mario.rossi@example.com"
            };
            context.Studentis.Add(studente);

            // Salva nel DB InMemory
            context.SaveChanges();

            // Restituisce il contesto da usare nel test
            return context;
        }

        [Fact]
        public async Task GetStudentiById_ReturnOkResult_WhentStenteExists()
        {
            // Arrange : prepara l'amiente di test
            ScuolaDbContext dbContext = GetInMeriyDbContetxt();
            ILogger<StudentiController> logStudentiController = new LoggerFactory().CreateLogger<StudentiController>();
            StudentiController controller = new(dbContext, logStudentiController);

            // Act: questo metodo verifica se esiste lo studente con Id 1 
            ActionResult<Studenti> result = await controller.GetStudentiById(1);

            // Assert: controlla che il risultato sia Ok (200)
            ObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Controllare i valori restituiti
            Studenti studente = Assert.IsType<Studenti>(okResult.Value);

            // Verifica che il nome Mario sia corretto
            Assert.Equal("Mario", studente.Nome);
        }

        [Fact]
        public async Task GetStudentiById_ReturnNotFoundResult_WhenStudenteDoesNotExist()
        {
            // Arrange : prepara l'amiente di test
            ScuolaDbContext dbContext = GetInMeriyDbContetxt();
            ILogger<StudentiController> logStudentiController = new LoggerFactory().CreateLogger<StudentiController>();
            StudentiController controller = new(dbContext, logStudentiController);

            // Act: questo metodo verifica se esiste lo studente con Id 99 (non esiste)
            ActionResult<Studenti> result = await controller.GetStudentiById(999);

            // Assert: controlla che il risultato sia NotFound (404)
            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task PostStudente_AddNewStudente_ReturnCreatedAction()
        {
            // Arrange
            ScuolaDbContext dbContext;
            StudentiController controller;
            CallDbContextWithController(out dbContext, out controller);

            // Crea nuovo studente da aggiungere
            Studenti nuovoStudente = new();
            nuovoStudente.Nome = "Luca";
            nuovoStudente.Cognome = "Bianchi";
            nuovoStudente.Email = "luca.bianchi@example.com";

            // Act: invia reschiesta Post
            ActionResult<Studenti> result = await controller.PostStudenti(nuovoStudente);

            // Assert: il risultato deve essere CreatedAction(201)
            CreatedAtActionResult createResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            //Controlla che il valore sia uno Studente
            Studenti stente = Assert.IsType<Studenti>(createResult.Value);

            // Verifica che Luca sia corretto
            Assert.Equal("Luca", stente.Nome);

            // Verifica che il database contenga 2 studenti ora
            int count = dbContext.Studentis.Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task DeleteStudente_RemovesStudente_ReturnsNoContent()
        {
            // Arrange
            ScuolaDbContext dbContext = GetInMeriyDbContetxt();
            ILogger<StudentiController> logStudentiController = new LoggerFactory().CreateLogger<StudentiController>();
            StudentiController controller = new(dbContext, logStudentiController);

            // Act: Elimina lo studente con Id 1
            IActionResult result = await controller.DeleteStudente(1);

            // Assert: deve restituire NoContent (204)
            Assert.IsType<NoContentResult>(result);

            // Verifica che lo studente sia stato effettivamente rimosso dal database
            Studenti? studenti = await dbContext.Studentis.FindAsync(1);
            Assert.Null(studenti);
        }

        private void CallDbContextWithController(out ScuolaDbContext dbContext, out StudentiController controller)
        {
            dbContext = GetInMeriyDbContetxt();
            ILogger<StudentiController> logStudentiController = new LoggerFactory().CreateLogger<StudentiController>();
            controller = new(dbContext, logStudentiController);
        }
    }
}
