using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Base;
using Publiparking.Web.Classes;
using Publiparking.Web.Classes.Consts;
using Publiparking.Web.Classes.Enumerator;
using Publiparking.Web.Configuration;
using Publiparking.Web.Configuration.ValueProvider;
using Publiparking.Web.Models;
using Publiparking.Web.Models.Account;
using Publiparking.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AbbonamentiPeriodiciController : LoggedBaseController
    {
        #region Private Members
        private readonly ILogger<AbbonamentiPeriodiciController> _logger;
        private CryptoParamsProtector _protector;
        #endregion Private Members

        #region Costructor
        public AbbonamentiPeriodiciController(ILogger<AbbonamentiPeriodiciController> logger,
            CryptoParamsProtector protector)
        {
            _protector = protector;
            _logger = logger;
        }
        #endregion Costructor
        [Authorize()]
        public async Task<IActionResult> Index(int? pageNumber, string currentFilter, string sortOrder = "", string searchString = "")
        {
            ViewData["idAbbonamentoPeriodicoSortParm"] = String.IsNullOrEmpty(sortOrder) ? "idAbbonamentoPeriodico_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["TargaSortParm"] = sortOrder == "Targa" ? "targa_desc" : "Targa";
            ViewData["CurrentFilter"] = searchString;

            IQueryable<AbbonamentiPeriodici> results = await AbbonamentiPeriodiciBD.GetListAsync(dbContext);
            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(s => s.codice.Contains(searchString)
                                       || s.email.Contains(searchString)
                                       || s.targa.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "idAbbonamentoPeriodico_desc":
                    results = results.OrderByDescending(s => s.idAbbonamentoPeriodico);
                    break;
                case "Date":
                    results = results.OrderBy(s => s.validoDal);
                    break;
                case "date_desc":
                    results = results.OrderByDescending(s => s.validoDal);
                    break;
                case "Targa":
                    results = results.OrderBy(s => s.targa);
                    break;
                case "targa_desc":
                    results = results.OrderByDescending(s => s.targa);
                    break;
                default:
                    results = results.OrderBy(s => s.idAbbonamentoPeriodico);
                    break;
            }
            int pageSize = 10;
            return View(await Publisoftware.Publiparking.Web.Classes.Pagination.PaginatedList<AbbonamentiPeriodici>.CreateAsync(results, pageNumber ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            AbbonamentiPeriodiciViewModel  model = new AbbonamentiPeriodiciViewModel();
            List<TariffeAbbonamenti> listTariffe = TariffeAbbonamentiBD.GetList(dbContext).ToList();
            model.TariffeAbbonamenti = listTariffe;
            model.validoDal = DateTime.Now;
            model.validoAl = DateTime.Now.AddYears(1);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idAbbonamentoPeriodico,codice,idTariffaAbbonamento,validoDal,targa,validoAl,cognome,nome,telefono,email")] AbbonamentiPeriodiciViewModel AbbonamentiPeriodici)
        {
            if (ModelState.IsValid)
            {
                AbbonamentiPeriodici abb = new AbbonamentiPeriodici
                {
                    idTariffaAbbonamento = AbbonamentiPeriodici.idTariffaAbbonamento,
                    validoAl= AbbonamentiPeriodici.validoAl,
                    validoDal= AbbonamentiPeriodici.validoDal,
                    codice= AbbonamentiPeriodici.codice,
                    targa= AbbonamentiPeriodici.targa,
                    cognome= AbbonamentiPeriodici.cognome,
                    nome= AbbonamentiPeriodici.nome,
                    email= AbbonamentiPeriodici.email,
                    telefono= AbbonamentiPeriodici.telefono

                };
                dbContext.AbbonamentiPeriodici.Add(abb);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(AbbonamentiPeriodici);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abbonamento = await dbContext.AbbonamentiPeriodici.FindAsync(id);
            if (abbonamento == null)
            {
                return NotFound();
            }
            List<TariffeAbbonamenti> listTariffe = TariffeAbbonamentiBD.GetList(dbContext).ToList();
            AbbonamentiPeriodiciViewModel model = new AbbonamentiPeriodiciViewModel
            {
                IdAbbonamentoPeriodico=abbonamento.idAbbonamentoPeriodico,
                targa= abbonamento.targa,
                idTariffaAbbonamento= abbonamento.idTariffaAbbonamento,
                TariffeAbbonamenti=listTariffe,
                validoAl= abbonamento.validoAl,
                validoDal= abbonamento.validoDal,
                codice= abbonamento.codice,
                cognome= abbonamento.cognome,
                email= abbonamento.email,
                nome= abbonamento.nome ,
                telefono= abbonamento.telefono
            };
            return View(model);
        }

        // POST: tab_carrello/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAbbonamentoPeriodico,codice,idTariffaAbbonamento,validoDal,targa,validoAl,cognome,nome,telefono,email")] AbbonamentiPeriodiciViewModel AbbonamentiPeriodici)
        {
            if (id != AbbonamentiPeriodici.IdAbbonamentoPeriodico)
            {
                return NotFound();
            }
            AbbonamentiPeriodici abb = await dbContext.AbbonamentiPeriodici.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    abb.idTariffaAbbonamento = AbbonamentiPeriodici.idTariffaAbbonamento;
                    abb.validoAl = AbbonamentiPeriodici.validoAl;
                    abb.validoDal = AbbonamentiPeriodici.validoDal;
                    abb.codice = AbbonamentiPeriodici.codice;
                    abb.targa = AbbonamentiPeriodici.targa;
                    abb.cognome = AbbonamentiPeriodici.cognome;
                    abb.nome = AbbonamentiPeriodici.nome;
                    abb.email = AbbonamentiPeriodici.email;
                    abb.telefono = AbbonamentiPeriodici.telefono;

                    dbContext.Entry(abb).State= EntityState.Modified;
                    dbContext.SaveChanges();
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
                {
                    if (!AbbonamentoPeriodicoExists(AbbonamentiPeriodici.IdAbbonamentoPeriodico))
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
            return View(AbbonamentiPeriodici);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abbonamento = await dbContext.AbbonamentiPeriodici.FindAsync(id);
            if (abbonamento == null)
            {
                return NotFound();
            }

            return View(abbonamento);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abbonamento = await dbContext.AbbonamentiPeriodici.FindAsync(id);
            dbContext.Entry(abbonamento).State = EntityState.Deleted;
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        private bool AbbonamentoPeriodicoExists(int id)
        {
            return dbContext.AbbonamentiPeriodici.Any(e => e.idAbbonamentoPeriodico == id);
        }
    }
}
