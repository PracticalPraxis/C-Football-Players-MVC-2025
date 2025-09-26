using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArsenalPlayers.Data;
using ArsenalPlayers.Models;
using System.Data;
using Newtonsoft.Json.Linq;
using static ArsenalPlayers.Models.APIModels;

namespace ArsenalPlayers.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ArsenalPlayersContext _context;

        public PlayersController(ArsenalPlayersContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            ArsenalPlayers.Models.PlayerIndexModel PlayerIndexModel = new ArsenalPlayers.Models.PlayerIndexModel();

            // HC 26/09/2025 - Slghtly hacky way of error handling, but this takes care of a submitted Player Model being Invalid and helps raise a JS alert on Index.cshtml
            if (TempData["createPlayerFailureMessage"] != null)
            {
                ViewBag.Message = TempData["createPlayerFailureMessage"].ToString();
            }

            if (_context.Player != null) {
                PlayerIndexModel.PlayerIEnumerable = await _context.Player.ToListAsync();
                return View(PlayerIndexModel);
            }
            else
            {
                return View();
            }

        }

        // GET: Players
        public async Task<IActionResult> List()
        {
            return _context.Player != null ?
                       View(await _context.Player.ToListAsync()) :
                       Problem("Entity set 'ArsenalPlayersContext.Player'  is null.");
        }

        // GET: ArsenalAPI
        public async Task<IActionResult> ArsenalAPI()
        {
            List<IntermediaryAPIObject> apiObjectList = new List<IntermediaryAPIObject>();
            HttpClient client = new HttpClient();

            String apiKeyTextFileName = "apiKey.txt";
            String footballURlTextFileName = "footballURL.txt";

            //HC 26/09/2025 - This gets string values for the API call to football-data.org, please make sure you have your own API Key
            // All develoipment and testing has been done with the following URL for Arsenal: https://api.football-data.org/v4/teams/57/matches
            String apiKeyString = GetStringValueFromFile(apiKeyTextFileName);
            String footballURLString = GetStringValueFromFile(footballURlTextFileName);
            client.DefaultRequestHeaders.Add("X-Auth-Token", apiKeyString);

            //HC 26/09/2025 - All develoipment and testing has been done with the following URL for Arsenal: https://api.football-data.org/v4/teams/57/matches
            HttpResponseMessage response = await client.GetAsync(footballURLString);
            response.EnsureSuccessStatusCode();

            String responseBody = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseBody))
            {
                return View();
            }
            else
            {
                apiObjectList = GetAPIDataForView(responseBody);

                return View(apiObjectList);

            }
        }

        public List<IntermediaryAPIObject> GetAPIDataForView(string httpResponse)
        {
            //TODO - Handle cases where response structure is different/doesn't fit with this approach
            var data = JObject.Parse(httpResponse);
            var srcArray = data.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // JValue handling
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                    // JObject Handling
                    if (column.Value is JObject)
                    {
                        JObject columnObject = (JObject)row.GetValue(column.Name);
                        string columnObjectName = column.Name;

                        foreach (JProperty columnObjectProperty in columnObject.Properties())
                        {
                            if (columnObjectProperty.Value is JValue)
                            {
                                cleanRow.Add(columnObjectName + columnObjectProperty.Name, columnObjectProperty.Value);
                            }
                        }
                    }
                }

                trgArray.Add(cleanRow);
            }

            return trgArray.ToObject<List<IntermediaryAPIObject>>();
        }

        public String GetStringValueFromFile(String filename)
        {
            String returnString = "";
            String line = "";
            if (System.IO.File.Exists(filename))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                while((line = file.ReadLine()) != null)
                {
                    returnString = line;
                }
                return returnString;
            }
            else
            {
                // TODO - Add actual error handling and raising
                Console.WriteLine("No file found, please create the relevant file in root of Solution");
                returnString = "Failed to get string";
                return returnString;
            }

        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View("~/Views/Players/Index.cshtml");
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,JerseyNumber,GoalsScored")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            TempData["createPlayerFailureMessage"] = "Player Creation Failed - Please enter valid values for each field (Name - String, Position - String, JerseyNumber - Integer, GoalsScored - Integer)";
            return RedirectToAction(nameof(Index));
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,JerseyNumber,GoalsScored")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'ArsenalPlayersContext.Player'  is null.");
            }
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
          return (_context.Player?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
